using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BettyMailZoom.Models;

namespace BettyMailZoom.Services
{
    public class IndexManager
    {
        private readonly OutlookService _outlookService;
        private readonly SearchIndexDatabase _database;
        private readonly AppSettings _settings;
        private CancellationTokenSource _cts;
        private bool _isBusy = false;

        public event Action<IndexProgress> ProgressChanged;

        public IndexManager(OutlookService outlookService, SearchIndexDatabase database, AppSettings settings)
        {
            _outlookService = outlookService;
            _database = database;
            _settings = settings;
        }

        public bool IsIndexing => _isBusy;

        public void Cancel()
        {
            _cts?.Cancel();
        }

        public async Task<IndexProgress> StartIndexingAsync(bool isFullRebuild)
        {
            if (_isBusy)
            {
                throw new InvalidOperationException("An indexing operation is already in progress.");
            }

            _isBusy = true;
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;

            var progress = new IndexProgress
            {
                StatusMessage = isFullRebuild ? "Starting Full Index Rebuild..." : "Starting Incremental Sync..."
            };
            ProgressChanged?.Invoke(progress);

            return await Task.Run(() =>
            {
                var stopwatch = Stopwatch.StartNew();

                try
                {
                    if (isFullRebuild)
                    {
                        progress.StatusMessage = "Clearing local search index...";
                        ProgressChanged?.Invoke(progress);
                        _database.ClearAll();
                    }

                    progress.StatusMessage = "Connecting to Outlook and scanning mail folders...";
                    ProgressChanged?.Invoke(progress);

                    var allFolders = _outlookService.GetFolders();
                    var targetFolders = new List<FolderInfo>();

                    if (_settings.SelectedFolderPaths != null && _settings.SelectedFolderPaths.Count > 0)
                    {
                        targetFolders = allFolders.Where(f => _settings.SelectedFolderPaths.Contains(f.FolderPath)).ToList();
                    }

                    if (targetFolders.Count == 0)
                    {
                        // Default to all selected folders
                        targetFolders = allFolders.Where(f => f.IsSelected).ToList();
                    }

                    int totalItemsToProcess = targetFolders.Sum(f => f.ItemCount);
                    progress.TotalDiscovered = totalItemsToProcess;
                    progress.StatusMessage = $"Found {targetFolders.Count} folders with ~{totalItemsToProcess} items.";
                    ProgressChanged?.Invoke(progress);

                    DateTime? syncSince = isFullRebuild ? (DateTime?)null : _settings.LastSyncTime;

                    foreach (var folder in targetFolders)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            progress.IsCancelled = true;
                            progress.StatusMessage = "Indexing cancelled by user.";
                            break;
                        }

                        progress.CurrentFolder = folder.FolderPath;
                        progress.CurrentAccount = folder.StoreName;
                        progress.StatusMessage = $"Indexing: {folder.FolderName} ({folder.StoreName})...";
                        ProgressChanged?.Invoke(progress);

                        _outlookService.FetchEmailsFromFolder(
                            folder.EntryId,
                            folder.StoreId,
                            syncSince,
                            _settings.IndexBodyContent,
                            _settings.MaxBodyIndexLength,
                            batch =>
                            {
                                if (batch.Count > 0)
                                {
                                    _database.UpsertBatch(batch);
                                    progress.IndexedCount += batch.Count;
                                    progress.ProcessedCount += batch.Count;

                                    double elapsedSec = stopwatch.Elapsed.TotalSeconds;
                                    if (elapsedSec > 0)
                                    {
                                        progress.ItemsPerSecond = Math.Round(progress.ProcessedCount / elapsedSec, 1);
                                    }

                                    progress.StatusMessage = $"Indexed {progress.ProcessedCount:N0} emails ({progress.ItemsPerSecond:N0} emails/sec)...";
                                    ProgressChanged?.Invoke(progress);
                                }
                            },
                            () => ct.IsCancellationRequested,
                            (folderPath, current, total) =>
                            {
                                // fine-grained progress update
                            });
                    }

                    if (!progress.IsCancelled)
                    {
                        _settings.LastSyncTime = DateTime.Now;
                        _settings.Save();

                        stopwatch.Stop();
                        progress.IsFinished = true;
                        progress.StatusMessage = $"Completed! Indexed {progress.IndexedCount:N0} emails in {stopwatch.Elapsed.TotalSeconds:F1}s ({progress.ItemsPerSecond:N0} items/sec).";
                        ProgressChanged?.Invoke(progress);
                    }
                }
                catch (Exception ex)
                {
                    progress.Error = ex;
                    progress.StatusMessage = $"Indexing error: {ex.Message}";
                    ProgressChanged?.Invoke(progress);
                }
                finally
                {
                    _isBusy = false;
                }

                return progress;
            });
        }
    }
}
