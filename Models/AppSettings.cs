using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BettyMailZoom.Models
{
    public class AppSettings
    {
        public List<string> SelectedFolderPaths { get; set; } = new List<string>();
        public DateTime? LastSyncTime { get; set; }
        public int AutoSyncMinutes { get; set; } = 15; // 0 = disabled
        public bool AutoSyncOnStartup { get; set; } = true;
        public int MaxResultsLimit { get; set; } = 1000;
        public string PreviewPanePosition { get; set; } = "Right"; // "Right", "Bottom", "Hidden"
        public int SplitterDistance { get; set; } = 550;
        public bool IndexBodyContent { get; set; } = true;
        public int MaxBodyIndexLength { get; set; } = 30000; // max chars of email body to index for speed

        private static string SettingsFilePath
        {
            get
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BettyMailZoom");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                return Path.Combine(folder, "settings.json");
            }
        }

        public static AppSettings Load()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    var json = File.ReadAllText(SettingsFilePath);
                    var settings = JsonConvert.DeserializeObject<AppSettings>(json);
                    if (settings != null) return settings;
                }
            }
            catch
            {
                // fallback to default
            }
            return new AppSettings();
        }

        public void Save()
        {
            try
            {
                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsFilePath, json);
            }
            catch
            {
                // ignore
            }
        }
    }
}
