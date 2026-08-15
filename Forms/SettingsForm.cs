using System;
using System.IO;
using System.Windows.Forms;
using BettyMailZoom.Models;
using BettyMailZoom.Services;

namespace BettyMailZoom.Forms
{
    public partial class SettingsForm : Form
    {
        private readonly AppSettings _settings;
        private readonly SearchIndexDatabase _database;

        public SettingsForm(AppSettings settings, SearchIndexDatabase database)
        {
            InitializeComponent();
            _settings = settings;
            _database = database;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            chkAutoSyncStartup.Checked = _settings.AutoSyncOnStartup;
            
            switch (_settings.AutoSyncMinutes)
            {
                case 0: cmbAutoSync.SelectedIndex = 0; break;
                case 5: cmbAutoSync.SelectedIndex = 1; break;
                case 15: cmbAutoSync.SelectedIndex = 2; break;
                case 30: cmbAutoSync.SelectedIndex = 3; break;
                case 60: cmbAutoSync.SelectedIndex = 4; break;
                default: cmbAutoSync.SelectedIndex = 2; break;
            }

            numMaxResults.Value = Math.Max(50, Math.Min(10000, _settings.MaxResultsLimit));

            if (_settings.PreviewPanePosition == "Bottom")
                cmbPreviewPane.SelectedIndex = 1;
            else if (_settings.PreviewPanePosition == "Hidden")
                cmbPreviewPane.SelectedIndex = 2;
            else
                cmbPreviewPane.SelectedIndex = 0;

            chkIndexBody.Checked = _settings.IndexBodyContent;

            UpdateDatabaseStats();
        }

        private void UpdateDatabaseStats()
        {
            lblDbPath.Text = $"Location: {_database.DatabasePath}";
            
            long bytes = _database.GetDatabaseSizeInBytes();
            double mb = bytes / (1024.0 * 1024.0);
            lblDbSize.Text = $"Database Size: {mb:F2} MB ({bytes:N0} bytes)";
            
            int total = _database.GetTotalEmailCount();
            lblDbItems.Text = $"Total Indexed Emails: {total:N0}";

            var lastSync = _settings.LastSyncTime;
            lblLastSync.Text = lastSync.HasValue
                ? $"Last Synced Time: {lastSync.Value:yyyy-MM-dd HH:mm:ss}"
                : "Last Synced Time: Never (Initial index needed)";
        }

        private void btnPurgeDatabase_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show(this, "Are you sure you want to clear the entire local email search index database?\r\n\r\nYou will need to rebuild the index to search emails again.", "Confirm Clear Index", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                _database.ClearAll();
                _settings.LastSyncTime = null;
                _settings.Save();
                UpdateDatabaseStats();
                MessageBox.Show(this, "Local search database has been cleared.", "Index Cleared", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.AutoSyncOnStartup = chkAutoSyncStartup.Checked;

            switch (cmbAutoSync.SelectedIndex)
            {
                case 0: _settings.AutoSyncMinutes = 0; break;
                case 1: _settings.AutoSyncMinutes = 5; break;
                case 2: _settings.AutoSyncMinutes = 15; break;
                case 3: _settings.AutoSyncMinutes = 30; break;
                case 4: _settings.AutoSyncMinutes = 60; break;
                default: _settings.AutoSyncMinutes = 15; break;
            }

            _settings.MaxResultsLimit = (int)numMaxResults.Value;
            _settings.PreviewPanePosition = cmbPreviewPane.SelectedItem?.ToString() ?? "Right";
            _settings.IndexBodyContent = chkIndexBody.Checked;

            _settings.Save();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
