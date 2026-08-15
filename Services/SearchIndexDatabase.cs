using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using BettyMailZoom.Models;

namespace BettyMailZoom.Services
{
    public class SearchIndexDatabase : IDisposable
    {
        private readonly string _dbPath;
        private readonly string _connectionString;
        private SQLiteConnection _connection;
        private readonly object _lock = new object();
        private bool _fts5Supported = true;

        public SearchIndexDatabase(string customPath = null)
        {
            if (string.IsNullOrWhiteSpace(customPath))
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BettyMailZoom");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                _dbPath = Path.Combine(folder, "mail_index.db");
            }
            else
            {
                _dbPath = customPath;
            }

            _connectionString = $"Data Source={_dbPath};Version=3;Journal Mode=WAL;Synchronous=Normal;BusyTimeout=5000;";
            InitializeDatabase();
        }

        public string DatabasePath => _dbPath;

        private void InitializeDatabase()
        {
            lock (_lock)
            {
                _connection = new SQLiteConnection(_connectionString);
                _connection.Open();

                using (var cmd = _connection.CreateCommand())
                {
                    // Core emails table
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Emails (
                            EntryId TEXT PRIMARY KEY,
                            StoreId TEXT,
                            Subject TEXT,
                            SenderName TEXT,
                            SenderEmail TEXT,
                            ToAddresses TEXT,
                            CcAddresses TEXT,
                            ReceivedTime INTEGER,
                            HasAttachments INTEGER,
                            AttachmentNames TEXT,
                            AttachmentCount INTEGER,
                            Importance INTEGER,
                            Size INTEGER,
                            BodySnippet TEXT,
                            BodyText TEXT,
                            BodyHtml TEXT,
                            FolderPath TEXT,
                            StoreName TEXT,
                            IsRead INTEGER,
                            Categories TEXT,
                            LastModifiedTime INTEGER
                        );

                        CREATE INDEX IF NOT EXISTS idx_emails_received ON Emails(ReceivedTime DESC);
                        CREATE INDEX IF NOT EXISTS idx_emails_sender ON Emails(SenderEmail);
                        CREATE INDEX IF NOT EXISTS idx_emails_folder ON Emails(FolderPath);
                        CREATE INDEX IF NOT EXISTS idx_emails_hasattach ON Emails(HasAttachments);
                        CREATE INDEX IF NOT EXISTS idx_emails_importance ON Emails(Importance);
                        CREATE INDEX IF NOT EXISTS idx_emails_isread ON Emails(IsRead);
                    ";
                    cmd.ExecuteNonQuery();

                    // Try creating FTS5 virtual table for full-text search
                    try
                    {
                        cmd.CommandText = @"
                            CREATE VIRTUAL TABLE IF NOT EXISTS Emails_FTS USING fts5(
                                EntryId UNINDEXED,
                                Subject,
                                SenderName,
                                SenderEmail,
                                ToAddresses,
                                BodyText,
                                AttachmentNames,
                                tokenize = 'unicode61 remove_diacritics 2'
                            );
                        ";
                        cmd.ExecuteNonQuery();
                        _fts5Supported = true;
                    }
                    catch
                    {
                        // Fallback to FTS4 if FTS5 is not loaded
                        try
                        {
                            cmd.CommandText = @"
                                CREATE VIRTUAL TABLE IF NOT EXISTS Emails_FTS USING fts4(
                                    EntryId,
                                    Subject,
                                    SenderName,
                                    SenderEmail,
                                    ToAddresses,
                                    BodyText,
                                    AttachmentNames
                                );
                            ";
                            cmd.ExecuteNonQuery();
                            _fts5Supported = true;
                        }
                        catch
                        {
                            _fts5Supported = false;
                        }
                    }
                }
            }
        }

        public void UpsertBatch(IEnumerable<EmailItemModel> emails)
        {
            if (emails == null) return;

            lock (_lock)
            {
                using (var transaction = _connection.BeginTransaction())
                {
                    using (var cmd = _connection.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = @"
                            INSERT OR REPLACE INTO Emails (
                                EntryId, StoreId, Subject, SenderName, SenderEmail, ToAddresses, CcAddresses,
                                ReceivedTime, HasAttachments, AttachmentNames, AttachmentCount, Importance,
                                Size, BodySnippet, BodyText, BodyHtml, FolderPath, StoreName, IsRead, Categories, LastModifiedTime
                            ) VALUES (
                                @EntryId, @StoreId, @Subject, @SenderName, @SenderEmail, @ToAddresses, @CcAddresses,
                                @ReceivedTime, @HasAttachments, @AttachmentNames, @AttachmentCount, @Importance,
                                @Size, @BodySnippet, @BodyText, @BodyHtml, @FolderPath, @StoreName, @IsRead, @Categories, @LastModifiedTime
                            );
                        ";

                        var pEntryId = cmd.Parameters.Add("@EntryId", DbType.String);
                        var pStoreId = cmd.Parameters.Add("@StoreId", DbType.String);
                        var pSubject = cmd.Parameters.Add("@Subject", DbType.String);
                        var pSenderName = cmd.Parameters.Add("@SenderName", DbType.String);
                        var pSenderEmail = cmd.Parameters.Add("@SenderEmail", DbType.String);
                        var pToAddresses = cmd.Parameters.Add("@ToAddresses", DbType.String);
                        var pCcAddresses = cmd.Parameters.Add("@CcAddresses", DbType.String);
                        var pReceivedTime = cmd.Parameters.Add("@ReceivedTime", DbType.Int64);
                        var pHasAttachments = cmd.Parameters.Add("@HasAttachments", DbType.Int32);
                        var pAttachmentNames = cmd.Parameters.Add("@AttachmentNames", DbType.String);
                        var pAttachmentCount = cmd.Parameters.Add("@AttachmentCount", DbType.Int32);
                        var pImportance = cmd.Parameters.Add("@Importance", DbType.Int32);
                        var pSize = cmd.Parameters.Add("@Size", DbType.Int64);
                        var pBodySnippet = cmd.Parameters.Add("@BodySnippet", DbType.String);
                        var pBodyText = cmd.Parameters.Add("@BodyText", DbType.String);
                        var pBodyHtml = cmd.Parameters.Add("@BodyHtml", DbType.String);
                        var pFolderPath = cmd.Parameters.Add("@FolderPath", DbType.String);
                        var pStoreName = cmd.Parameters.Add("@StoreName", DbType.String);
                        var pIsRead = cmd.Parameters.Add("@IsRead", DbType.Int32);
                        var pCategories = cmd.Parameters.Add("@Categories", DbType.String);
                        var pLastModifiedTime = cmd.Parameters.Add("@LastModifiedTime", DbType.Int64);

                        SQLiteCommand ftsDeleteCmd = null;
                        SQLiteCommand ftsInsertCmd = null;

                        if (_fts5Supported)
                        {
                            ftsDeleteCmd = _connection.CreateCommand();
                            ftsDeleteCmd.Transaction = transaction;
                            ftsDeleteCmd.CommandText = "DELETE FROM Emails_FTS WHERE EntryId = @EntryId;";
                            ftsDeleteCmd.Parameters.Add("@EntryId", DbType.String);

                            ftsInsertCmd = _connection.CreateCommand();
                            ftsInsertCmd.Transaction = transaction;
                            ftsInsertCmd.CommandText = @"
                                INSERT INTO Emails_FTS (EntryId, Subject, SenderName, SenderEmail, ToAddresses, BodyText, AttachmentNames)
                                VALUES (@EntryId, @Subject, @SenderName, @SenderEmail, @ToAddresses, @BodyText, @AttachmentNames);
                            ";
                            ftsInsertCmd.Parameters.Add("@EntryId", DbType.String);
                            ftsInsertCmd.Parameters.Add("@Subject", DbType.String);
                            ftsInsertCmd.Parameters.Add("@SenderName", DbType.String);
                            ftsInsertCmd.Parameters.Add("@SenderEmail", DbType.String);
                            ftsInsertCmd.Parameters.Add("@ToAddresses", DbType.String);
                            ftsInsertCmd.Parameters.Add("@BodyText", DbType.String);
                            ftsInsertCmd.Parameters.Add("@AttachmentNames", DbType.String);
                        }

                        foreach (var item in emails)
                        {
                            pEntryId.Value = item.EntryId ?? "";
                            pStoreId.Value = item.StoreId ?? "";
                            pSubject.Value = item.Subject ?? "";
                            pSenderName.Value = item.SenderName ?? "";
                            pSenderEmail.Value = item.SenderEmail ?? "";
                            pToAddresses.Value = item.ToAddresses ?? "";
                            pCcAddresses.Value = item.CcAddresses ?? "";
                            pReceivedTime.Value = item.ReceivedTime.Ticks;
                            pHasAttachments.Value = item.HasAttachments ? 1 : 0;
                            pAttachmentNames.Value = item.AttachmentNames ?? "";
                            pAttachmentCount.Value = item.AttachmentCount;
                            pImportance.Value = item.Importance;
                            pSize.Value = item.Size;
                            pBodySnippet.Value = item.BodySnippet ?? "";
                            pBodyText.Value = item.BodyText ?? "";
                            pBodyHtml.Value = item.BodyHtml ?? "";
                            pFolderPath.Value = item.FolderPath ?? "";
                            pStoreName.Value = item.StoreName ?? "";
                            pIsRead.Value = item.IsRead ? 1 : 0;
                            pCategories.Value = item.Categories ?? "";
                            pLastModifiedTime.Value = item.LastModifiedTime.Ticks;

                            cmd.ExecuteNonQuery();

                            if (_fts5Supported && ftsDeleteCmd != null && ftsInsertCmd != null)
                            {
                                ftsDeleteCmd.Parameters["@EntryId"].Value = item.EntryId ?? "";
                                ftsDeleteCmd.ExecuteNonQuery();

                                ftsInsertCmd.Parameters["@EntryId"].Value = item.EntryId ?? "";
                                ftsInsertCmd.Parameters["@Subject"].Value = item.Subject ?? "";
                                ftsInsertCmd.Parameters["@SenderName"].Value = item.SenderName ?? "";
                                ftsInsertCmd.Parameters["@SenderEmail"].Value = item.SenderEmail ?? "";
                                ftsInsertCmd.Parameters["@ToAddresses"].Value = item.ToAddresses ?? "";
                                ftsInsertCmd.Parameters["@BodyText"].Value = item.BodyText ?? "";
                                ftsInsertCmd.Parameters["@AttachmentNames"].Value = item.AttachmentNames ?? "";
                                ftsInsertCmd.ExecuteNonQuery();
                            }
                        }

                        ftsDeleteCmd?.Dispose();
                        ftsInsertCmd?.Dispose();
                    }
                    transaction.Commit();
                }
            }
        }

        public List<EmailItemModel> Search(SearchQuery query, out int totalCount)
        {
            var results = new List<EmailItemModel>();
            totalCount = 0;

            lock (_lock)
            {
                using (var cmd = _connection.CreateCommand())
                {
                    var sqlBuilder = new StringBuilder();
                    var whereClauses = new List<string>();

                    bool useFts = _fts5Supported && !string.IsNullOrWhiteSpace(query.Keyword);

                    if (useFts)
                    {
                        // Clean query keyword for FTS
                        var ftsKeyword = FormatFtsQuery(query.Keyword);
                        if (!string.IsNullOrWhiteSpace(ftsKeyword))
                        {
                            sqlBuilder.Append(@"
                                FROM Emails e
                                INNER JOIN Emails_FTS f ON e.EntryId = f.EntryId
                            ");
                            whereClauses.Add("Emails_FTS MATCH @ftsQuery");
                            cmd.Parameters.AddWithValue("@ftsQuery", ftsKeyword);
                        }
                        else
                        {
                            sqlBuilder.Append(" FROM Emails e ");
                        }
                    }
                    else
                    {
                        sqlBuilder.Append(" FROM Emails e ");
                        if (!string.IsNullOrWhiteSpace(query.Keyword))
                        {
                            var kw = $"%{query.Keyword.Trim()}%";
                            whereClauses.Add("(e.Subject LIKE @kw OR e.SenderName LIKE @kw OR e.SenderEmail LIKE @kw OR e.ToAddresses LIKE @kw OR e.BodyText LIKE @kw OR e.AttachmentNames LIKE @kw)");
                            cmd.Parameters.AddWithValue("@kw", kw);
                        }
                    }

                    // Sender filter
                    if (!string.IsNullOrWhiteSpace(query.Sender))
                    {
                        var senderKw = $"%{query.Sender.Trim()}%";
                        whereClauses.Add("(e.SenderName LIKE @senderKw OR e.SenderEmail LIKE @senderKw)");
                        cmd.Parameters.AddWithValue("@senderKw", senderKw);
                    }

                    // Recipient filter
                    if (!string.IsNullOrWhiteSpace(query.Recipient))
                    {
                        var recipKw = $"%{query.Recipient.Trim()}%";
                        whereClauses.Add("(e.ToAddresses LIKE @recipKw OR e.CcAddresses LIKE @recipKw)");
                        cmd.Parameters.AddWithValue("@recipKw", recipKw);
                    }

                    // Subject filter
                    if (!string.IsNullOrWhiteSpace(query.Subject))
                    {
                        var subjKw = $"%{query.Subject.Trim()}%";
                        whereClauses.Add("e.Subject LIKE @subjKw");
                        cmd.Parameters.AddWithValue("@subjKw", subjKw);
                    }

                    // Exclude terms filter
                    if (!string.IsNullOrWhiteSpace(query.ExcludeTerms))
                    {
                        var terms = query.ExcludeTerms.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < terms.Length; i++)
                        {
                            var paramName = $"@exTerm_{i}";
                            whereClauses.Add($"(e.Subject NOT LIKE {paramName} AND e.BodyText NOT LIKE {paramName} AND e.SenderEmail NOT LIKE {paramName} AND e.SenderName NOT LIKE {paramName})");
                            cmd.Parameters.AddWithValue(paramName, $"%{terms[i]}%");
                        }
                    }

                    // Attachment filter
                    if (query.AttachmentFilter == 1) // Must have attachments
                    {
                        whereClauses.Add("e.HasAttachments = 1");
                    }
                    else if (query.AttachmentFilter == 2) // No attachments
                    {
                        whereClauses.Add("e.HasAttachments = 0");
                    }

                    // Attachment Extension Filter
                    if (!string.IsNullOrWhiteSpace(query.AttachmentExtension))
                    {
                        var ext = query.AttachmentExtension.Trim();
                        if (!ext.StartsWith(".")) ext = "." + ext;
                        whereClauses.Add("e.AttachmentNames LIKE @attExt");
                        cmd.Parameters.AddWithValue("@attExt", $"%{ext}%");
                    }

                    // Importance filter
                    if (query.ImportanceFilter >= 0)
                    {
                        whereClauses.Add("e.Importance = @importance");
                        cmd.Parameters.AddWithValue("@importance", query.ImportanceFilter);
                    }

                    // Date range
                    if (query.DateFrom.HasValue)
                    {
                        whereClauses.Add("e.ReceivedTime >= @dateFrom");
                        cmd.Parameters.AddWithValue("@dateFrom", query.DateFrom.Value.Date.Ticks);
                    }

                    if (query.DateTo.HasValue)
                    {
                        whereClauses.Add("e.ReceivedTime <= @dateTo");
                        cmd.Parameters.AddWithValue("@dateTo", query.DateTo.Value.Date.AddDays(1).AddTicks(-1).Ticks);
                    }

                    // Folder path
                    if (!string.IsNullOrWhiteSpace(query.FolderPath) && query.FolderPath != "All Folders")
                    {
                        whereClauses.Add("e.FolderPath = @folderPath");
                        cmd.Parameters.AddWithValue("@folderPath", query.FolderPath);
                    }

                    // Unread filter
                    if (query.UnreadOnly.HasValue && query.UnreadOnly.Value)
                    {
                        whereClauses.Add("e.IsRead = 0");
                    }

                    var whereSql = whereClauses.Count > 0 ? " WHERE " + string.Join(" AND ", whereClauses) : "";

                    // 1. Get total count
                    cmd.CommandText = "SELECT COUNT(*) " + sqlBuilder.ToString() + whereSql;
                    var countObj = cmd.ExecuteScalar();
                    totalCount = countObj != null ? Convert.ToInt32(countObj) : 0;

                    // 2. Fetch page results
                    var selectColumns = @"
                        SELECT e.EntryId, e.StoreId, e.Subject, e.SenderName, e.SenderEmail,
                               e.ToAddresses, e.CcAddresses, e.ReceivedTime, e.HasAttachments,
                               e.AttachmentNames, e.AttachmentCount, e.Importance, e.Size,
                               e.BodySnippet, e.BodyText, e.BodyHtml, e.FolderPath, e.StoreName,
                               e.IsRead, e.Categories, e.LastModifiedTime
                    ";

                    cmd.CommandText = selectColumns + sqlBuilder.ToString() + whereSql + " ORDER BY e.ReceivedTime DESC LIMIT @limit OFFSET @offset";
                    cmd.Parameters.AddWithValue("@limit", query.Limit > 0 ? query.Limit : 500);
                    cmd.Parameters.AddWithValue("@offset", query.Offset >= 0 ? query.Offset : 0);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(ReadEmailItem(reader));
                        }
                    }
                }
            }

            return results;
        }

        private string FormatFtsQuery(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            var terms = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var t in terms)
            {
                var clean = t.Replace("\"", "").Replace("'", "").Replace("*", "").Trim();
                if (clean.Length > 0)
                {
                    if (sb.Length > 0) sb.Append(" ");
                    sb.Append($"\"{clean}*\"");
                }
            }
            return sb.ToString();
        }

        public EmailItemModel GetEmailByEntryId(string entryId)
        {
            if (string.IsNullOrWhiteSpace(entryId)) return null;

            lock (_lock)
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT EntryId, StoreId, Subject, SenderName, SenderEmail,
                               ToAddresses, CcAddresses, ReceivedTime, HasAttachments,
                               AttachmentNames, AttachmentCount, Importance, Size,
                               BodySnippet, BodyText, BodyHtml, FolderPath, StoreName,
                               IsRead, Categories, LastModifiedTime
                        FROM Emails
                        WHERE EntryId = @entryId
                        LIMIT 1;
                    ";
                    cmd.Parameters.AddWithValue("@entryId", entryId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ReadEmailItem(reader);
                        }
                    }
                }
            }
            return null;
        }

        public bool DeleteEmail(string entryId)
        {
            if (string.IsNullOrWhiteSpace(entryId)) return false;

            lock (_lock)
            {
                using (var transaction = _connection.BeginTransaction())
                {
                    using (var cmd = _connection.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = "DELETE FROM Emails WHERE EntryId = @entryId;";
                        cmd.Parameters.AddWithValue("@entryId", entryId);
                        cmd.ExecuteNonQuery();

                        if (_fts5Supported)
                        {
                            using (var ftsCmd = _connection.CreateCommand())
                            {
                                ftsCmd.Transaction = transaction;
                                ftsCmd.CommandText = "DELETE FROM Emails_FTS WHERE EntryId = @entryId;";
                                ftsCmd.Parameters.AddWithValue("@entryId", entryId);
                                ftsCmd.ExecuteNonQuery();
                            }
                        }
                    }
                    transaction.Commit();
                }
            }
            return true;
        }

        public void ClearAll()
        {
            lock (_lock)
            {
                using (var transaction = _connection.BeginTransaction())
                {
                    using (var cmd = _connection.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = "DELETE FROM Emails;";
                        cmd.ExecuteNonQuery();

                        if (_fts5Supported)
                        {
                            try
                            {
                                using (var ftsCmd = _connection.CreateCommand())
                                {
                                    ftsCmd.Transaction = transaction;
                                    ftsCmd.CommandText = "DELETE FROM Emails_FTS;";
                                    ftsCmd.ExecuteNonQuery();
                                }
                            }
                            catch { }
                        }
                    }
                    transaction.Commit();
                }
            }
        }

        public int GetTotalEmailCount()
        {
            lock (_lock)
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) FROM Emails;";
                    var obj = cmd.ExecuteScalar();
                    return obj != null ? Convert.ToInt32(obj) : 0;
                }
            }
        }

        public DateTime? GetLastIndexedTimestamp()
        {
            lock (_lock)
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT MAX(LastModifiedTime) FROM Emails;";
                    var obj = cmd.ExecuteScalar();
                    if (obj != null && obj != DBNull.Value)
                    {
                        long ticks = Convert.ToInt64(obj);
                        if (ticks > 0) return new DateTime(ticks);
                    }
                }
            }
            return null;
        }

        public List<string> GetDistinctFolderPaths()
        {
            var list = new List<string>();
            lock (_lock)
            {
                using (var cmd = _connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT DISTINCT FolderPath FROM Emails WHERE FolderPath IS NOT NULL AND FolderPath != '' ORDER BY FolderPath ASC;";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return list;
        }

        public long GetDatabaseSizeInBytes()
        {
            try
            {
                if (File.Exists(_dbPath))
                {
                    var fi = new FileInfo(_dbPath);
                    return fi.Length;
                }
            }
            catch { }
            return 0;
        }

        private EmailItemModel ReadEmailItem(SQLiteDataReader reader)
        {
            return new EmailItemModel
            {
                EntryId = reader.IsDBNull(0) ? "" : reader.GetString(0),
                StoreId = reader.IsDBNull(1) ? "" : reader.GetString(1),
                Subject = reader.IsDBNull(2) ? "" : reader.GetString(2),
                SenderName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                SenderEmail = reader.IsDBNull(4) ? "" : reader.GetString(4),
                ToAddresses = reader.IsDBNull(5) ? "" : reader.GetString(5),
                CcAddresses = reader.IsDBNull(6) ? "" : reader.GetString(6),
                ReceivedTime = reader.IsDBNull(7) ? DateTime.MinValue : new DateTime(reader.GetInt64(7)),
                HasAttachments = !reader.IsDBNull(8) && reader.GetInt32(8) == 1,
                AttachmentNames = reader.IsDBNull(9) ? "" : reader.GetString(9),
                AttachmentCount = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                Importance = reader.IsDBNull(11) ? 1 : reader.GetInt32(11),
                Size = reader.IsDBNull(12) ? 0 : reader.GetInt64(12),
                BodySnippet = reader.IsDBNull(13) ? "" : reader.GetString(13),
                BodyText = reader.IsDBNull(14) ? "" : reader.GetString(14),
                BodyHtml = reader.IsDBNull(15) ? "" : reader.GetString(15),
                FolderPath = reader.IsDBNull(16) ? "" : reader.GetString(16),
                StoreName = reader.IsDBNull(17) ? "" : reader.GetString(17),
                IsRead = !reader.IsDBNull(18) && reader.GetInt32(18) == 1,
                Categories = reader.IsDBNull(19) ? "" : reader.GetString(19),
                LastModifiedTime = reader.IsDBNull(20) ? DateTime.MinValue : new DateTime(reader.GetInt64(20))
            };
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_connection != null)
                {
                    try
                    {
                        if (_connection.State == ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                    }
                    catch { }
                    _connection = null;
                }
            }
        }
    }
}
