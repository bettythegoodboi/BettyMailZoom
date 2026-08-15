using System;
using System.Collections.Generic;

namespace BettyMailZoom.Models
{
    /// <summary>
    /// Represents an email item indexed locally.
    /// </summary>
    public class EmailItemModel
    {
        public string EntryId { get; set; }
        public string StoreId { get; set; }
        public string Subject { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string ToAddresses { get; set; }
        public string CcAddresses { get; set; }
        public DateTime ReceivedTime { get; set; }
        public bool HasAttachments { get; set; }
        public string AttachmentNames { get; set; }
        public int AttachmentCount { get; set; }
        public int Importance { get; set; } // 0 = Low, 1 = Normal, 2 = High
        public long Size { get; set; } // in bytes
        public string BodySnippet { get; set; }
        public string BodyText { get; set; }
        public string BodyHtml { get; set; }
        public string FolderPath { get; set; }
        public string StoreName { get; set; }
        public bool IsRead { get; set; }
        public string Categories { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public string ImportanceDisplayName
        {
            get
            {
                switch (Importance)
                {
                    case 2: return "🔴 High";
                    case 0: return "🔵 Low";
                    default: return "Normal";
                }
            }
        }

        public string SizeFormatted
        {
            get
            {
                if (Size < 1024) return $"{Size} B";
                if (Size < 1024 * 1024) return $"{Size / 1024.0:F1} KB";
                return $"{Size / (1024.0 * 1024.0):F2} MB";
            }
        }

        public string DisplaySender => string.IsNullOrWhiteSpace(SenderName) ? SenderEmail : $"{SenderName} <{SenderEmail}>";

        public List<string> GetAttachmentList()
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(AttachmentNames)) return list;
            var parts = AttachmentNames.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries);
            list.AddRange(parts);
            return list;
        }
    }
}
