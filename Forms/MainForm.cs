using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BettyMailZoom.Helpers;
using BettyMailZoom.Models;
using BettyMailZoom.Services;

namespace BettyMailZoom.Forms
{
    public partial class MainForm : Form
    {
        private AppSettings _settings;
        private SearchIndexDatabase _database;
        private OutlookService _outlookService;
        private IndexManager _indexManager;

        private List<EmailItemModel> _currentResults = new List<EmailItemModel>();
        private EmailItemModel _selectedEmail = null;
        private bool _isFiltersExpanded = true;
        private bool _isHtmlView = true;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _settings = AppSettings.Load();
            _database = new SearchIndexDatabase();
            _outlookService = new OutlookService();
            _indexManager = new IndexManager(_outlookService, _database, _settings);

            _indexManager.ProgressChanged += OnIndexProgressChanged;

            InitializeUiDefaults();
            ApplySettings();
            LoadFolderFilterList();
            UpdateIndexStatusLabel();

            // Perform initial search
            ExecuteSearch();

            // Auto-sync on startup if enabled
            if (_settings.AutoSyncOnStartup)
            {
                BeginIndexing(false);
            }
        }

        private void InitializeUiDefaults()
        {
            cmbImportance.SelectedIndex = 0; // All
            cmbAttachment.SelectedIndex = 0; // All
            cmbDateRange.SelectedIndex = 0;  // Anytime
            dtpDateFrom.Value = DateTime.Today.AddDays(-30);
            dtpDateTo.Value = DateTime.Today;

            // Set grid columns
            dgvResults.AutoGenerateColumns = false;
            colImportance.DataPropertyName = "ImportanceDisplayName";
            colAttachment.DataPropertyName = "HasAttachments";
            colSender.DataPropertyName = "SenderName";
            colSubject.DataPropertyName = "Subject";
            colDate.DataPropertyName = "ReceivedTime";
            colFolder.DataPropertyName = "FolderPath";
            colSize.DataPropertyName = "SizeFormatted";

            // Configure auto-sync timer
            UpdateAutoSyncTimer();
        }

        private void ApplySettings()
        {
            if (_settings.PreviewPanePosition == "Bottom")
            {
                splitContainerMain.Orientation = Orientation.Horizontal;
                splitContainerMain.Panel2Collapsed = false;
            }
            else if (_settings.PreviewPanePosition == "Hidden")
            {
                splitContainerMain.Panel2Collapsed = true;
            }
            else
            {
                splitContainerMain.Orientation = Orientation.Vertical;
                splitContainerMain.Panel2Collapsed = false;
            }
        }

        private void UpdateAutoSyncTimer()
        {
            if (_settings.AutoSyncMinutes > 0)
            {
                timerAutoSync.Interval = _settings.AutoSyncMinutes * 60 * 1000;
                timerAutoSync.Start();
            }
            else
            {
                timerAutoSync.Stop();
            }
        }

        private void LoadFolderFilterList()
        {
            try
            {
                var folders = _database.GetDistinctFolderPaths();
                cmbFolder.Items.Clear();
                cmbFolder.Items.Add("All Folders");
                foreach (var f in folders)
                {
                    cmbFolder.Items.Add(f);
                }
                cmbFolder.SelectedIndex = 0;
            }
            catch { }
        }

        private void UpdateIndexStatusLabel()
        {
            try
            {
                int count = _database.GetTotalEmailCount();
                var lastSync = _settings.LastSyncTime;

                if (count == 0)
                {
                    statusLblIndexState.Text = "⚠️ No Emails Indexed. Click 'Rebuild Index'";
                    statusLblIndexState.ForeColor = Color.DarkOrange;
                }
                else
                {
                    string syncStr = lastSync.HasValue ? $" (Synced: {lastSync.Value:MM-dd HH:mm})" : "";
                    statusLblIndexState.Text = $"🟢 Index Ready: {count:N0} emails{syncStr}";
                    statusLblIndexState.ForeColor = Color.FromArgb(0, 120, 60);
                }
            }
            catch
            {
                statusLblIndexState.Text = "⚪ Index Offline";
                statusLblIndexState.ForeColor = Color.Gray;
            }
        }

        #region Search Execution

        private void ExecuteSearch()
        {
            var sw = Stopwatch.StartNew();

            var query = BuildSearchQuery();

            try
            {
                int totalMatches;
                _currentResults = _database.Search(query, out totalMatches);

                dgvResults.SuspendLayout();
                dgvResults.Rows.Clear();

                foreach (var item in _currentResults)
                {
                    int rowIndex = dgvResults.Rows.Add(
                        item.Importance == 2 ? "🔴" : (item.Importance == 0 ? "🔵" : ""),
                        item.HasAttachments ? "📎" : "",
                        string.IsNullOrWhiteSpace(item.SenderName) ? item.SenderEmail : item.SenderName,
                        item.Subject,
                        item.ReceivedTime > DateTime.MinValue ? item.ReceivedTime.ToString("yyyy-MM-dd HH:mm") : "",
                        ExtractSimpleFolderName(item.FolderPath),
                        item.SizeFormatted
                    );

                    dgvResults.Rows[rowIndex].Tag = item;

                    // Unread styling
                    if (!item.IsRead)
                    {
                        dgvResults.Rows[rowIndex].DefaultCellStyle.Font = new Font(dgvResults.Font, FontStyle.Bold);
                    }
                }
                dgvResults.ResumeLayout();

                sw.Stop();
                statusLblResults.Text = $"Showing {_currentResults.Count:N0} of {totalMatches:N0} emails ({sw.ElapsedMilliseconds} ms)";

                if (dgvResults.Rows.Count > 0)
                {
                    dgvResults.Rows[0].Selected = true;
                    ShowEmailPreview((EmailItemModel)dgvResults.Rows[0].Tag);
                }
                else
                {
                    ClearPreview();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Search error: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SearchQuery BuildSearchQuery()
        {
            var query = new SearchQuery
            {
                Keyword = txtSearchKeyword.Text.Trim(),
                Sender = txtSender.Text.Trim(),
                Recipient = txtRecipient.Text.Trim(),
                Subject = txtSubject.Text.Trim(),
                ExcludeTerms = txtExclude.Text.Trim(),
                AttachmentFilter = cmbAttachment.SelectedIndex,
                AttachmentExtension = txtAttachmentExt.Text.Trim(),
                FolderPath = cmbFolder.SelectedItem?.ToString(),
                UnreadOnly = chkUnreadOnly.Checked ? (bool?)true : null,
                Limit = _settings.MaxResultsLimit
            };

            // Importance
            switch (cmbImportance.SelectedIndex)
            {
                case 1: query.ImportanceFilter = 2; break; // High
                case 2: query.ImportanceFilter = 1; break; // Normal
                case 3: query.ImportanceFilter = 0; break; // Low
                default: query.ImportanceFilter = -1; break; // All
            }

            // Date Range
            switch (cmbDateRange.SelectedIndex)
            {
                case 1: // Today
                    query.DateFrom = DateTime.Today;
                    query.DateTo = DateTime.Today;
                    break;
                case 2: // Past 7 Days
                    query.DateFrom = DateTime.Today.AddDays(-7);
                    query.DateTo = DateTime.Today;
                    break;
                case 3: // Past 30 Days
                    query.DateFrom = DateTime.Today.AddDays(-30);
                    query.DateTo = DateTime.Today;
                    break;
                case 4: // Past 6 Months
                    query.DateFrom = DateTime.Today.AddMonths(-6);
                    query.DateTo = DateTime.Today;
                    break;
                case 5: // This Year
                    query.DateFrom = new DateTime(DateTime.Today.Year, 1, 1);
                    query.DateTo = DateTime.Today;
                    break;
                case 6: // Custom Range
                    query.DateFrom = dtpDateFrom.Value.Date;
                    query.DateTo = dtpDateTo.Value.Date;
                    break;
                default: // Anytime
                    query.DateFrom = null;
                    query.DateTo = null;
                    break;
            }

            return query;
        }

        private string ExtractSimpleFolderName(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath)) return "";
            var parts = folderPath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            return parts.Length > 0 ? parts[parts.Length - 1] : folderPath;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        private void txtSearchKeyword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ExecuteSearch();
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchKeyword.Clear();
            ExecuteSearch();
            txtSearchKeyword.Focus();
        }

        private void FilterControl_Changed(object sender, EventArgs e)
        {
            // Execute search on filter change
            ExecuteSearch();
        }

        private void cmbDateRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isCustom = cmbDateRange.SelectedIndex == 6;
            lblDateFrom.Visible = isCustom;
            dtpDateFrom.Visible = isCustom;
            lblDateTo.Visible = isCustom;
            dtpDateTo.Visible = isCustom;

            ExecuteSearch();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSender.Clear();
            txtRecipient.Clear();
            txtSubject.Clear();
            txtExclude.Clear();
            txtAttachmentExt.Clear();
            cmbAttachment.SelectedIndex = 0;
            cmbImportance.SelectedIndex = 0;
            cmbDateRange.SelectedIndex = 0;
            cmbFolder.SelectedIndex = 0;
            chkUnreadOnly.Checked = false;

            ExecuteSearch();
        }

        private void btnToggleFilters_Click(object sender, EventArgs e)
        {
            _isFiltersExpanded = !_isFiltersExpanded;
            panelFilters.Visible = _isFiltersExpanded;
            btnToggleFilters.Text = _isFiltersExpanded ? "🔍 Filters (Hide ▲)" : "🔍 Filters (Show ▼)";
        }

        #endregion

        #region Email Preview & Details

        private void dgvResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvResults.SelectedRows.Count > 0)
            {
                var item = dgvResults.SelectedRows[0].Tag as EmailItemModel;
                ShowEmailPreview(item);
            }
        }

        private void ShowEmailPreview(EmailItemModel item)
        {
            _selectedEmail = item;
            if (item == null)
            {
                ClearPreview();
                return;
            }

            lblPreviewSubject.Text = string.IsNullOrWhiteSpace(item.Subject) ? "(No Subject)" : item.Subject;
            lblPreviewSender.Text = $"From: {item.DisplaySender}";
            lblPreviewRecipients.Text = $"To: {item.ToAddresses}" + (string.IsNullOrWhiteSpace(item.CcAddresses) ? "" : $" | Cc: {item.CcAddresses}");
            lblPreviewDate.Text = item.ReceivedTime > DateTime.MinValue ? item.ReceivedTime.ToString("dddd, MMMM dd, yyyy HH:mm") : "";
            lblPreviewFolder.Text = $"📁 {item.FolderPath} ({item.SizeFormatted})";

            // Attachments
            flowAttachments.SuspendLayout();
            flowAttachments.Controls.Clear();
            var attachments = item.GetAttachmentList();
            if (attachments.Count > 0)
            {
                var lblHeader = new Label
                {
                    Text = $"📎 {attachments.Count} Attachment(s):",
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(70, 70, 70),
                    AutoSize = true,
                    Margin = new Padding(0, 4, 6, 0)
                };
                flowAttachments.Controls.Add(lblHeader);

                foreach (var att in attachments)
                {
                    var btnAtt = new Button
                    {
                        Text = $"📎 {att}",
                        Font = new Font("Segoe UI", 8f),
                        AutoSize = true,
                        BackColor = Color.FromArgb(235, 240, 248),
                        FlatStyle = FlatStyle.Flat,
                        Cursor = Cursors.Hand,
                        Margin = new Padding(2, 0, 4, 2),
                        Tag = att
                    };
                    btnAtt.FlatAppearance.BorderColor = Color.FromArgb(190, 210, 235);
                    btnAtt.Click += BtnAttachment_Click;
                    flowAttachments.Controls.Add(btnAtt);
                }
                flowAttachments.Visible = true;
            }
            else
            {
                flowAttachments.Visible = false;
            }
            flowAttachments.ResumeLayout();

            // Render body
            RenderEmailBody(item);
        }

        private void RenderEmailBody(EmailItemModel item)
        {
            if (item == null) return;

            txtPlainTextPreview.Text = item.BodyText ?? "";

            if (_isHtmlView)
            {
                webBrowserPreview.Visible = true;
                txtPlainTextPreview.Visible = false;
                btnToggleHtmlText.Text = "Plain Text";

                string html = item.BodyHtml;
                // If HTML wasn't cached in DB, try fetching full HTML on the fly
                if (string.IsNullOrWhiteSpace(html) && !string.IsNullOrWhiteSpace(item.EntryId))
                {
                    try
                    {
                        html = _outlookService.GetFullHtmlBody(item.EntryId, item.StoreId);
                    }
                    catch { }
                }

                string wrappedHtml = HtmlHelper.WrapEmailHtml(html, item.BodyText);
                webBrowserPreview.DocumentText = wrappedHtml;
            }
            else
            {
                webBrowserPreview.Visible = false;
                txtPlainTextPreview.Visible = true;
                btnToggleHtmlText.Text = "HTML View";
            }
        }

        private void ClearPreview()
        {
            _selectedEmail = null;
            lblPreviewSubject.Text = "(No email selected)";
            lblPreviewSender.Text = "From: -";
            lblPreviewRecipients.Text = "To: -";
            lblPreviewDate.Text = "";
            lblPreviewFolder.Text = "";
            flowAttachments.Controls.Clear();
            flowAttachments.Visible = false;
            webBrowserPreview.DocumentText = "<html><body></body></html>";
            txtPlainTextPreview.Text = "";
        }

        private void btnToggleHtmlText_Click(object sender, EventArgs e)
        {
            _isHtmlView = !_isHtmlView;
            if (_selectedEmail != null)
            {
                RenderEmailBody(_selectedEmail);
            }
        }

        private void BtnAttachment_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is string attName && _selectedEmail != null)
            {
                var sfd = new SaveFileDialog
                {
                    FileName = attName,
                    Title = "Save Attachment As"
                };

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        bool success = _outlookService.SaveAttachment(_selectedEmail.EntryId, _selectedEmail.StoreId, attName, sfd.FileName);
                        if (success)
                        {
                            var open = MessageBox.Show(this, $"Saved attachment to:\r\n{sfd.FileName}\r\n\r\nWould you like to open it now?", "Attachment Saved", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (open == DialogResult.Yes)
                            {
                                Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Could not extract attachment from Outlook.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, $"Failed to save attachment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        #endregion

        #region Email Actions (Open, Delete, Copy)

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                OpenSelectedEmailInOutlook();
            }
        }

        private void dgvResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                OpenSelectedEmailInOutlook();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                DeleteSelectedEmail();
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                e.Handled = true;
                CopyAllEmailInfo();
            }
        }

        private void btnOpenInOutlook_Click(object sender, EventArgs e)
        {
            OpenSelectedEmailInOutlook();
        }

        private void OpenSelectedEmailInOutlook()
        {
            if (_selectedEmail == null) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                bool opened = _outlookService.OpenEmail(_selectedEmail.EntryId, _selectedEmail.StoreId);
                if (!opened)
                {
                    MessageBox.Show(this, "Could not open email. It may have been moved or deleted.", "Outlook Item Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to open email in Outlook:\r\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnDeleteEmail_Click(object sender, EventArgs e)
        {
            DeleteSelectedEmail();
        }

        private void DeleteSelectedEmail()
        {
            if (_selectedEmail == null) return;

            var confirm = MessageBox.Show(this, $"Are you sure you want to delete this email?\r\n\r\nSubject: {_selectedEmail.Subject}\r\nFrom: {_selectedEmail.DisplaySender}", "Confirm Delete Email", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                // Delete from Outlook
                _outlookService.DeleteEmail(_selectedEmail.EntryId, _selectedEmail.StoreId);

                // Delete from local SQLite index
                _database.DeleteEmail(_selectedEmail.EntryId);

                // Remove row from Grid
                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    if (row.Tag is EmailItemModel item && item.EntryId == _selectedEmail.EntryId)
                    {
                        dgvResults.Rows.Remove(row);
                        break;
                    }
                }

                ClearPreview();
                UpdateIndexStatusLabel();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to delete email: {ex.Message}", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            CopyAllEmailInfo();
        }

        private void CopyAllEmailInfo()
        {
            if (_selectedEmail == null) return;

            var sb = new StringBuilder();
            sb.AppendLine($"Subject: {_selectedEmail.Subject}");
            sb.AppendLine($"From: {_selectedEmail.DisplaySender}");
            sb.AppendLine($"To: {_selectedEmail.ToAddresses}");
            if (!string.IsNullOrWhiteSpace(_selectedEmail.CcAddresses))
                sb.AppendLine($"Cc: {_selectedEmail.CcAddresses}");
            sb.AppendLine($"Date: {_selectedEmail.ReceivedTime:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Folder: {_selectedEmail.FolderPath}");
            if (!string.IsNullOrWhiteSpace(_selectedEmail.AttachmentNames))
                sb.AppendLine($"Attachments: {_selectedEmail.AttachmentNames}");
            sb.AppendLine();
            sb.AppendLine("------------------ Body ------------------");
            sb.AppendLine(_selectedEmail.BodyText);

            Clipboard.SetText(sb.ToString());
            ShowTemporaryStatus("Copied full email details to clipboard!");
        }

        private void btnCopyBody_Click(object sender, EventArgs e)
        {
            if (_selectedEmail == null) return;
            if (!string.IsNullOrWhiteSpace(_selectedEmail.BodyText))
            {
                Clipboard.SetText(_selectedEmail.BodyText);
                ShowTemporaryStatus("Copied email body to clipboard!");
            }
        }

        private void menuItemCopySubject_Click(object sender, EventArgs e)
        {
            if (_selectedEmail != null && !string.IsNullOrWhiteSpace(_selectedEmail.Subject))
            {
                Clipboard.SetText(_selectedEmail.Subject);
                ShowTemporaryStatus("Copied subject to clipboard!");
            }
        }

        private void menuItemCopySender_Click(object sender, EventArgs e)
        {
            if (_selectedEmail != null)
            {
                string textToCopy = !string.IsNullOrWhiteSpace(_selectedEmail.SenderEmail) ? _selectedEmail.SenderEmail : _selectedEmail.SenderName;
                if (!string.IsNullOrWhiteSpace(textToCopy))
                {
                    Clipboard.SetText(textToCopy);
                    ShowTemporaryStatus("Copied sender email to clipboard!");
                }
            }
        }

        private void ShowTemporaryStatus(string message)
        {
            statusLblResults.Text = message;
        }

        private void dgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Optional formatting
        }

        #endregion

        #region Indexing Controls & Background Worker

        private void btnRefreshIndex_Click(object sender, EventArgs e)
        {
            BeginIndexing(false);
        }

        private void btnRebuildIndex_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(this, "A Full Rebuild will re-scan all selected Outlook folders from scratch.\r\n\r\nDo you want to proceed?", "Confirm Full Rebuild", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                BeginIndexing(true);
            }
        }

        private async void BeginIndexing(bool isFullRebuild)
        {
            if (_indexManager.IsIndexing)
            {
                MessageBox.Show(this, "An indexing task is already running in the background.", "Indexing Busy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            btnRefreshIndex.Enabled = false;
            btnRebuildIndex.Enabled = false;
            statusProgressBar.Visible = true;
            statusProgressBar.Value = 0;
            statusBtnCancelIndex.Visible = true;
            statusLblIndexProgress.Text = isFullRebuild ? "Starting full rebuild..." : "Checking for new emails...";

            try
            {
                var result = await _indexManager.StartIndexingAsync(isFullRebuild);
                if (result.Error != null)
                {
                    MessageBox.Show(this, $"Indexing completed with notice:\r\n{result.Error.Message}", "Index Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to complete indexing: {ex.Message}", "Index Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRefreshIndex.Enabled = true;
                btnRebuildIndex.Enabled = true;
                statusProgressBar.Visible = false;
                statusBtnCancelIndex.Visible = false;
                statusLblIndexProgress.Text = "";

                UpdateIndexStatusLabel();
                LoadFolderFilterList();
                ExecuteSearch();
            }
        }

        private void OnIndexProgressChanged(IndexProgress progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<IndexProgress>(OnIndexProgressChanged), progress);
                return;
            }

            statusLblIndexProgress.Text = progress.StatusMessage;
            if (progress.PercentComplete > 0 && progress.PercentComplete <= 100)
            {
                statusProgressBar.Style = ProgressBarStyle.Continuous;
                statusProgressBar.Value = progress.PercentComplete;
            }
            else
            {
                statusProgressBar.Style = ProgressBarStyle.Marquee;
            }
        }

        private void statusBtnCancelIndex_Click(object sender, EventArgs e)
        {
            _indexManager.Cancel();
            statusLblIndexProgress.Text = "Cancelling index sync...";
        }

        private void timerAutoSync_Tick(object sender, EventArgs e)
        {
            if (!_indexManager.IsIndexing)
            {
                BeginIndexing(false);
            }
        }

        #endregion

        #region Navigation & Settings Dialogs

        private void btnFolders_Click(object sender, EventArgs e)
        {
            using (var form = new FolderSelectionForm(_outlookService, _settings))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    var reindex = MessageBox.Show(this, "Folder selection updated.\r\n\r\nWould you like to refresh the index now to apply changes?", "Refresh Index?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (reindex == DialogResult.Yes)
                    {
                        BeginIndexing(false);
                    }
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm(_settings, _database))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    ApplySettings();
                    UpdateAutoSyncTimer();
                    ExecuteSearch();
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_indexManager != null && _indexManager.IsIndexing)
            {
                _indexManager.Cancel();
            }

            _database?.Dispose();
        }

        #endregion
    }
}
