using System;

namespace BettyMailZoom.Models
{
    /// <summary>
    /// Encapsulates all search criteria and filter options.
    /// </summary>
    public class SearchQuery
    {
        public string Keyword { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string ExcludeTerms { get; set; }

        // Attachment filter: 0 = Any, 1 = Has Attachment, 2 = No Attachment
        public int AttachmentFilter { get; set; } = 0;
        public string AttachmentExtension { get; set; } // e.g. ".pdf", ".xlsx"

        // Importance filter: -1 = All, 0 = Low, 1 = Normal, 2 = High
        public int ImportanceFilter { get; set; } = -1;

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public string FolderPath { get; set; }
        public bool? UnreadOnly { get; set; }

        public int Limit { get; set; } = 500;
        public int Offset { get; set; } = 0;

        public bool IsEmpty =>
            string.IsNullOrWhiteSpace(Keyword) &&
            string.IsNullOrWhiteSpace(Sender) &&
            string.IsNullOrWhiteSpace(Recipient) &&
            string.IsNullOrWhiteSpace(Subject) &&
            string.IsNullOrWhiteSpace(ExcludeTerms) &&
            AttachmentFilter == 0 &&
            string.IsNullOrWhiteSpace(AttachmentExtension) &&
            ImportanceFilter == -1 &&
            !DateFrom.HasValue &&
            !DateTo.HasValue &&
            string.IsNullOrWhiteSpace(FolderPath) &&
            (!UnreadOnly.HasValue || !UnreadOnly.Value);
    }
}
