using System;

namespace BettyMailZoom.Models
{
    /// <summary>
    /// Progress information during index rebuild or refresh.
    /// </summary>
    public class IndexProgress
    {
        public int TotalDiscovered { get; set; }
        public int ProcessedCount { get; set; }
        public int IndexedCount { get; set; }
        public int SkippedCount { get; set; }
        public int ErrorCount { get; set; }
        public string CurrentFolder { get; set; }
        public string CurrentAccount { get; set; }
        public string StatusMessage { get; set; }
        public double ItemsPerSecond { get; set; }
        public bool IsFinished { get; set; }
        public bool IsCancelled { get; set; }
        public Exception Error { get; set; }

        public int PercentComplete
        {
            get
            {
                if (TotalDiscovered <= 0) return 0;
                var pct = (int)((ProcessedCount * 100.0) / TotalDiscovered);
                return Math.Min(100, Math.Max(0, pct));
            }
        }
    }
}
