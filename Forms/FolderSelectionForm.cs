using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BettyMailZoom.Models;
using BettyMailZoom.Services;

namespace BettyMailZoom.Forms
{
    public partial class FolderSelectionForm : Form
    {
        private readonly OutlookService _outlookService;
        private readonly AppSettings _settings;
        private List<FolderInfo> _folders = new List<FolderInfo>();

        public FolderSelectionForm(OutlookService outlookService, AppSettings settings)
        {
            InitializeComponent();
            _outlookService = outlookService;
            _settings = settings;
        }

        private void FolderSelectionForm_Load(object sender, EventArgs e)
        {
            LoadOutlookFolders();
        }

        private void LoadOutlookFolders()
        {
            treeViewFolders.Nodes.Clear();
            Cursor = Cursors.WaitCursor;

            try
            {
                _folders = _outlookService.GetFolders();
                var savedSelection = new HashSet<string>(_settings.SelectedFolderPaths ?? new List<string>());

                // Group by StoreName
                var storeGroups = _folders.GroupBy(f => f.StoreName);

                foreach (var group in storeGroups)
                {
                    var storeNode = new TreeNode(group.Key)
                    {
                        Tag = "STORE",
                        Checked = true
                    };

                    foreach (var folder in group)
                    {
                        bool isChecked = savedSelection.Count > 0
                            ? savedSelection.Contains(folder.FolderPath)
                            : folder.IsSelected;

                        var folderNode = new TreeNode($"{folder.FolderName} ({folder.ItemCount:N0} items)")
                        {
                            Tag = folder,
                            Checked = isChecked
                        };
                        storeNode.Nodes.Add(folderNode);
                    }

                    treeViewFolders.Nodes.Add(storeNode);
                    storeNode.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Failed to load Outlook folders: {ex.Message}", "Outlook Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void treeViewFolders_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // If store node is checked/unchecked, cascade to children
            if (e.Node.Tag is string s && s == "STORE")
            {
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetAllCheckState(true);
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            SetAllCheckState(false);
        }

        private void SetAllCheckState(bool isChecked)
        {
            foreach (TreeNode storeNode in treeViewFolders.Nodes)
            {
                storeNode.Checked = isChecked;
                foreach (TreeNode folderNode in storeNode.Nodes)
                {
                    folderNode.Checked = isChecked;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var selectedPaths = new List<string>();

            foreach (TreeNode storeNode in treeViewFolders.Nodes)
            {
                foreach (TreeNode folderNode in storeNode.Nodes)
                {
                    if (folderNode.Checked && folderNode.Tag is FolderInfo fi)
                    {
                        selectedPaths.Add(fi.FolderPath);
                    }
                }
            }

            if (selectedPaths.Count == 0)
            {
                var confirm = MessageBox.Show(this, "No folders are selected. Are you sure you want to save with no folders selected?", "No Folders Selected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes)
                {
                    return;
                }
            }

            _settings.SelectedFolderPaths = selectedPaths;
            _settings.Save();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
