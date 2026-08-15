namespace BettyMailZoom.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTopHeader = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnFolders = new System.Windows.Forms.Button();
            this.btnRebuildIndex = new System.Windows.Forms.Button();
            this.btnRefreshIndex = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSearchSection = new System.Windows.Forms.Panel();
            this.btnToggleFilters = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchKeyword = new System.Windows.Forms.TextBox();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.chkUnreadOnly = new System.Windows.Forms.CheckBox();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.cmbFolder = new System.Windows.Forms.ComboBox();
            this.lblFolder = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.cmbDateRange = new System.Windows.Forms.ComboBox();
            this.lblDateRange = new System.Windows.Forms.Label();
            this.cmbImportance = new System.Windows.Forms.ComboBox();
            this.lblImportance = new System.Windows.Forms.Label();
            this.txtAttachmentExt = new System.Windows.Forms.TextBox();
            this.lblAttachmentExt = new System.Windows.Forms.Label();
            this.cmbAttachment = new System.Windows.Forms.ComboBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.txtExclude = new System.Windows.Forms.TextBox();
            this.lblExclude = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtRecipient = new System.Windows.Forms.TextBox();
            this.lblRecipient = new System.Windows.Forms.Label();
            this.txtSender = new System.Windows.Forms.TextBox();
            this.lblSender = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.colImportance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttachment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuResults = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemOpenOutlook = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemCopySubject = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopySender = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopyBody = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopyAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.webBrowserPreview = new System.Windows.Forms.WebBrowser();
            this.txtPlainTextPreview = new System.Windows.Forms.TextBox();
            this.flowAttachments = new System.Windows.Forms.FlowLayoutPanel();
            this.panelPreviewToolbar = new System.Windows.Forms.Panel();
            this.btnToggleHtmlText = new System.Windows.Forms.Button();
            this.btnDeleteEmail = new System.Windows.Forms.Button();
            this.btnCopyBody = new System.Windows.Forms.Button();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.btnOpenInOutlook = new System.Windows.Forms.Button();
            this.panelPreviewHeader = new System.Windows.Forms.Panel();
            this.lblPreviewFolder = new System.Windows.Forms.Label();
            this.lblPreviewDate = new System.Windows.Forms.Label();
            this.lblPreviewRecipients = new System.Windows.Forms.Label();
            this.lblPreviewSender = new System.Windows.Forms.Label();
            this.lblPreviewSubject = new System.Windows.Forms.Label();
            this.statusStripBottom = new System.Windows.Forms.StatusStrip();
            this.statusLblResults = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblIndexProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusBtnCancelIndex = new System.Windows.Forms.ToolStripDropDownButton();
            this.statusLblIndexState = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerAutoSync = new System.Windows.Forms.Timer(this.components);
            this.panelTopHeader.SuspendLayout();
            this.panelSearchSection.SuspendLayout();
            this.panelFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.contextMenuResults.SuspendLayout();
            this.panelPreview.SuspendLayout();
            this.panelPreviewToolbar.SuspendLayout();
            this.panelPreviewHeader.SuspendLayout();
            this.statusStripBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTopHeader
            // 
            this.panelTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.panelTopHeader.Controls.Add(this.btnSettings);
            this.panelTopHeader.Controls.Add(this.btnFolders);
            this.panelTopHeader.Controls.Add(this.btnRebuildIndex);
            this.panelTopHeader.Controls.Add(this.btnRefreshIndex);
            this.panelTopHeader.Controls.Add(this.lblTitle);
            this.panelTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopHeader.Location = new System.Drawing.Point(0, 0);
            this.panelTopHeader.Name = "panelTopHeader";
            this.panelTopHeader.Size = new System.Drawing.Size(1184, 46);
            this.panelTopHeader.TabIndex = 0;
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.BackColor = System.Drawing.Color.White;
            this.btnSettings.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSettings.Location = new System.Drawing.Point(1082, 8);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(90, 30);
            this.btnSettings.TabIndex = 4;
            this.btnSettings.Text = "⚙ Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnFolders
            // 
            this.btnFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolders.BackColor = System.Drawing.Color.White;
            this.btnFolders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnFolders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFolders.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnFolders.Location = new System.Drawing.Point(986, 8);
            this.btnFolders.Name = "btnFolders";
            this.btnFolders.Size = new System.Drawing.Size(90, 30);
            this.btnFolders.TabIndex = 3;
            this.btnFolders.Text = "📁 Folders";
            this.btnFolders.UseVisualStyleBackColor = false;
            this.btnFolders.Click += new System.EventHandler(this.btnFolders_Click);
            // 
            // btnRebuildIndex
            // 
            this.btnRebuildIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRebuildIndex.BackColor = System.Drawing.Color.White;
            this.btnRebuildIndex.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnRebuildIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRebuildIndex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRebuildIndex.Location = new System.Drawing.Point(860, 8);
            this.btnRebuildIndex.Name = "btnRebuildIndex";
            this.btnRebuildIndex.Size = new System.Drawing.Size(120, 30);
            this.btnRebuildIndex.TabIndex = 2;
            this.btnRebuildIndex.Text = "🔄 Rebuild Index";
            this.btnRebuildIndex.UseVisualStyleBackColor = false;
            this.btnRebuildIndex.Click += new System.EventHandler(this.btnRebuildIndex_Click);
            // 
            // btnRefreshIndex
            // 
            this.btnRefreshIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnRefreshIndex.FlatAppearance.BorderSize = 0;
            this.btnRefreshIndex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshIndex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefreshIndex.ForeColor = System.Drawing.Color.White;
            this.btnRefreshIndex.Location = new System.Drawing.Point(734, 8);
            this.btnRefreshIndex.Name = "btnRefreshIndex";
            this.btnRefreshIndex.Size = new System.Drawing.Size(120, 30);
            this.btnRefreshIndex.TabIndex = 1;
            this.btnRefreshIndex.Text = "⚡ Refresh Index";
            this.btnRefreshIndex.UseVisualStyleBackColor = false;
            this.btnRefreshIndex.Click += new System.EventHandler(this.btnRefreshIndex_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "⚡ BettyMailZoom - Fast Outlook Search";
            // 
            // panelSearchSection
            // 
            this.panelSearchSection.BackColor = System.Drawing.Color.White;
            this.panelSearchSection.Controls.Add(this.btnToggleFilters);
            this.panelSearchSection.Controls.Add(this.btnClearSearch);
            this.panelSearchSection.Controls.Add(this.btnSearch);
            this.panelSearchSection.Controls.Add(this.txtSearchKeyword);
            this.panelSearchSection.Controls.Add(this.lblSearchIcon);
            this.panelSearchSection.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearchSection.Location = new System.Drawing.Point(0, 46);
            this.panelSearchSection.Name = "panelSearchSection";
            this.panelSearchSection.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelSearchSection.Size = new System.Drawing.Size(1184, 50);
            this.panelSearchSection.TabIndex = 1;
            // 
            // btnToggleFilters
            // 
            this.btnToggleFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.btnToggleFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(220)))));
            this.btnToggleFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleFilters.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnToggleFilters.Location = new System.Drawing.Point(1030, 9);
            this.btnToggleFilters.Name = "btnToggleFilters";
            this.btnToggleFilters.Size = new System.Drawing.Size(142, 32);
            this.btnToggleFilters.TabIndex = 4;
            this.btnToggleFilters.Text = "🔍 Filters (Hide ▲)";
            this.btnToggleFilters.UseVisualStyleBackColor = false;
            this.btnToggleFilters.Click += new System.EventHandler(this.btnToggleFilters_Click);
            // 
            // btnClearSearch
            // 
            this.btnClearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearSearch.BackColor = System.Drawing.Color.White;
            this.btnClearSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(215)))), ((int)(((byte)(220)))));
            this.btnClearSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClearSearch.Location = new System.Drawing.Point(948, 9);
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Size = new System.Drawing.Size(76, 32);
            this.btnClearSearch.TabIndex = 3;
            this.btnClearSearch.Text = "Clear";
            this.btnClearSearch.UseVisualStyleBackColor = false;
            this.btnClearSearch.Click += new System.EventHandler(this.btnClearSearch_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(842, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchKeyword
            // 
            this.txtSearchKeyword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchKeyword.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearchKeyword.Location = new System.Drawing.Point(40, 11);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(796, 27);
            this.txtSearchKeyword.TabIndex = 1;
            this.txtSearchKeyword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchKeyword_KeyDown);
            // 
            // lblSearchIcon
            // 
            this.lblSearchIcon.AutoSize = true;
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearchIcon.Location = new System.Drawing.Point(12, 14);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(26, 21);
            this.lblSearchIcon.TabIndex = 0;
            this.lblSearchIcon.Text = "🔎";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(253)))));
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.chkUnreadOnly);
            this.panelFilters.Controls.Add(this.btnClearFilters);
            this.panelFilters.Controls.Add(this.cmbFolder);
            this.panelFilters.Controls.Add(this.lblFolder);
            this.panelFilters.Controls.Add(this.dtpDateTo);
            this.panelFilters.Controls.Add(this.lblDateTo);
            this.panelFilters.Controls.Add(this.dtpDateFrom);
            this.panelFilters.Controls.Add(this.lblDateFrom);
            this.panelFilters.Controls.Add(this.cmbDateRange);
            this.panelFilters.Controls.Add(this.lblDateRange);
            this.panelFilters.Controls.Add(this.cmbImportance);
            this.panelFilters.Controls.Add(this.lblImportance);
            this.panelFilters.Controls.Add(this.txtAttachmentExt);
            this.panelFilters.Controls.Add(this.lblAttachmentExt);
            this.panelFilters.Controls.Add(this.cmbAttachment);
            this.panelFilters.Controls.Add(this.lblAttachment);
            this.panelFilters.Controls.Add(this.txtExclude);
            this.panelFilters.Controls.Add(this.lblExclude);
            this.panelFilters.Controls.Add(this.txtSubject);
            this.panelFilters.Controls.Add(this.lblSubject);
            this.panelFilters.Controls.Add(this.txtRecipient);
            this.panelFilters.Controls.Add(this.lblRecipient);
            this.panelFilters.Controls.Add(this.txtSender);
            this.panelFilters.Controls.Add(this.lblSender);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 96);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Size = new System.Drawing.Size(1184, 88);
            this.panelFilters.TabIndex = 2;
            // 
            // chkUnreadOnly
            // 
            this.chkUnreadOnly.AutoSize = true;
            this.chkUnreadOnly.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.chkUnreadOnly.Location = new System.Drawing.Point(920, 50);
            this.chkUnreadOnly.Name = "chkUnreadOnly";
            this.chkUnreadOnly.Size = new System.Drawing.Size(91, 19);
            this.chkUnreadOnly.TabIndex = 23;
            this.chkUnreadOnly.Text = "Unread only";
            this.chkUnreadOnly.UseVisualStyleBackColor = true;
            this.chkUnreadOnly.CheckedChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearFilters.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnClearFilters.Location = new System.Drawing.Point(1055, 46);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(115, 26);
            this.btnClearFilters.TabIndex = 22;
            this.btnClearFilters.Text = "Reset Filters";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // cmbFolder
            // 
            this.cmbFolder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cmbFolder.FormattingEnabled = true;
            this.cmbFolder.Location = new System.Drawing.Point(744, 48);
            this.cmbFolder.Name = "cmbFolder";
            this.cmbFolder.Size = new System.Drawing.Size(160, 21);
            this.cmbFolder.TabIndex = 21;
            this.cmbFolder.SelectedIndexChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblFolder.Location = new System.Drawing.Point(698, 51);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(43, 15);
            this.lblFolder.TabIndex = 20;
            this.lblFolder.Text = "Folder:";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "yyyy-MM-dd";
            this.dtpDateTo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(595, 48);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(95, 23);
            this.dtpDateTo.TabIndex = 19;
            this.dtpDateTo.ValueChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDateTo.Location = new System.Drawing.Point(570, 51);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(22, 15);
            this.lblDateTo.TabIndex = 18;
            this.lblDateTo.Text = "To:";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpDateFrom.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(468, 48);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(95, 23);
            this.dtpDateFrom.TabIndex = 17;
            this.dtpDateFrom.ValueChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDateFrom.Location = new System.Drawing.Point(428, 51);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(38, 15);
            this.lblDateFrom.TabIndex = 16;
            this.lblDateFrom.Text = "From:";
            // 
            // cmbDateRange
            // 
            this.cmbDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDateRange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cmbDateRange.FormattingEnabled = true;
            this.cmbDateRange.Items.AddRange(new object[] {
            "Anytime",
            "Today",
            "Past 7 Days",
            "Past 30 Days",
            "Past 6 Months",
            "This Year",
            "Custom Range"});
            this.cmbDateRange.Location = new System.Drawing.Point(315, 48);
            this.cmbDateRange.Name = "cmbDateRange";
            this.cmbDateRange.Size = new System.Drawing.Size(105, 21);
            this.cmbDateRange.TabIndex = 15;
            this.cmbDateRange.SelectedIndexChanged += new System.EventHandler(this.cmbDateRange_SelectedIndexChanged);
            // 
            // lblDateRange
            // 
            this.lblDateRange.AutoSize = true;
            this.lblDateRange.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDateRange.Location = new System.Drawing.Point(276, 51);
            this.lblDateRange.Name = "lblDateRange";
            this.lblDateRange.Size = new System.Drawing.Size(34, 15);
            this.lblDateRange.TabIndex = 14;
            this.lblDateRange.Text = "Date:";
            // 
            // cmbImportance
            // 
            this.cmbImportance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImportance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cmbImportance.FormattingEnabled = true;
            this.cmbImportance.Items.AddRange(new object[] {
            "All",
            "🔴 High",
            "Normal",
            "🔵 Low"});
            this.cmbImportance.Location = new System.Drawing.Point(82, 48);
            this.cmbImportance.Name = "cmbImportance";
            this.cmbImportance.Size = new System.Drawing.Size(180, 21);
            this.cmbImportance.TabIndex = 13;
            this.cmbImportance.SelectedIndexChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblImportance
            // 
            this.lblImportance.AutoSize = true;
            this.lblImportance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblImportance.Location = new System.Drawing.Point(12, 51);
            this.lblImportance.Name = "lblImportance";
            this.lblImportance.Size = new System.Drawing.Size(71, 15);
            this.lblImportance.TabIndex = 12;
            this.lblImportance.Text = "Importance:";
            // 
            // txtAttachmentExt
            // 
            this.txtAttachmentExt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtAttachmentExt.Location = new System.Drawing.Point(1075, 12);
            this.txtAttachmentExt.Name = "txtAttachmentExt";
            this.txtAttachmentExt.Size = new System.Drawing.Size(95, 23);
            this.txtAttachmentExt.TabIndex = 11;
            this.txtAttachmentExt.TextChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblAttachmentExt
            // 
            this.lblAttachmentExt.AutoSize = true;
            this.lblAttachmentExt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAttachmentExt.Location = new System.Drawing.Point(1045, 15);
            this.lblAttachmentExt.Name = "lblAttachmentExt";
            this.lblAttachmentExt.Size = new System.Drawing.Size(26, 15);
            this.lblAttachmentExt.TabIndex = 10;
            this.lblAttachmentExt.Text = "Ext:";
            // 
            // cmbAttachment
            // 
            this.cmbAttachment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttachment.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.cmbAttachment.FormattingEnabled = true;
            this.cmbAttachment.Items.AddRange(new object[] {
            "All",
            "📎 Has Attachment",
            "No Attachment"});
            this.cmbAttachment.Location = new System.Drawing.Point(920, 12);
            this.cmbAttachment.Name = "cmbAttachment";
            this.cmbAttachment.Size = new System.Drawing.Size(120, 21);
            this.cmbAttachment.TabIndex = 9;
            this.cmbAttachment.SelectedIndexChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblAttachment.Location = new System.Drawing.Point(870, 15);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(46, 15);
            this.lblAttachment.TabIndex = 8;
            this.lblAttachment.Text = "Attach:";
            // 
            // txtExclude
            // 
            this.txtExclude.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtExclude.Location = new System.Drawing.Point(710, 12);
            this.txtExclude.Name = "txtExclude";
            this.txtExclude.Size = new System.Drawing.Size(150, 23);
            this.txtExclude.TabIndex = 7;
            this.txtExclude.TextChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblExclude
            // 
            this.lblExclude.AutoSize = true;
            this.lblExclude.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblExclude.Location = new System.Drawing.Point(656, 15);
            this.lblExclude.Name = "lblExclude";
            this.lblExclude.Size = new System.Drawing.Size(51, 15);
            this.lblExclude.TabIndex = 6;
            this.lblExclude.Text = "Exclude:";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtSubject.Location = new System.Drawing.Point(495, 12);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(150, 23);
            this.txtSubject.TabIndex = 5;
            this.txtSubject.TextChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSubject.Location = new System.Drawing.Point(445, 15);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(49, 15);
            this.lblSubject.TabIndex = 4;
            this.lblSubject.Text = "Subject:";
            // 
            // txtRecipient
            // 
            this.txtRecipient.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtRecipient.Location = new System.Drawing.Point(280, 12);
            this.txtRecipient.Name = "txtRecipient";
            this.txtRecipient.Size = new System.Drawing.Size(150, 23);
            this.txtRecipient.TabIndex = 3;
            this.txtRecipient.TextChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblRecipient
            // 
            this.lblRecipient.AutoSize = true;
            this.lblRecipient.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblRecipient.Location = new System.Drawing.Point(255, 15);
            this.lblRecipient.Name = "lblRecipient";
            this.lblRecipient.Size = new System.Drawing.Size(23, 15);
            this.lblRecipient.TabIndex = 2;
            this.lblRecipient.Text = "To:";
            // 
            // txtSender
            // 
            this.txtSender.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.txtSender.Location = new System.Drawing.Point(82, 12);
            this.txtSender.Name = "txtSender";
            this.txtSender.Size = new System.Drawing.Size(160, 23);
            this.txtSender.TabIndex = 1;
            this.txtSender.TextChanged += new System.EventHandler(this.FilterControl_Changed);
            // 
            // lblSender
            // 
            this.lblSender.AutoSize = true;
            this.lblSender.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSender.Location = new System.Drawing.Point(12, 15);
            this.lblSender.Name = "lblSender";
            this.lblSender.Size = new System.Drawing.Size(68, 15);
            this.lblSender.TabIndex = 0;
            this.lblSender.Text = "From / Sdr:";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 184);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.dgvResults);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panelPreview);
            this.splitContainerMain.Size = new System.Drawing.Size(1184, 495);
            this.splitContainerMain.SplitterDistance = 640;
            this.splitContainerMain.SplitterWidth = 5;
            this.splitContainerMain.TabIndex = 3;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.Color.White;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResults.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(244)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(64)))), ((int)(((byte)(67)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImportance,
            this.colAttachment,
            this.colSender,
            this.colSubject,
            this.colDate,
            this.colFolder,
            this.colSize});
            this.dgvResults.ContextMenuStrip = this.contextMenuResults;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(233)))), ((int)(((byte)(237)))));
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowTemplate.Height = 30;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.Size = new System.Drawing.Size(640, 495);
            this.dgvResults.TabIndex = 0;
            this.dgvResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellDoubleClick);
            this.dgvResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResults_CellFormatting);
            this.dgvResults.SelectionChanged += new System.EventHandler(this.dgvResults_SelectionChanged);
            this.dgvResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvResults_KeyDown);
            // 
            // colImportance
            // 
            this.colImportance.HeaderText = "!";
            this.colImportance.Name = "colImportance";
            this.colImportance.ReadOnly = true;
            this.colImportance.Width = 30;
            // 
            // colAttachment
            // 
            this.colAttachment.HeaderText = "📎";
            this.colAttachment.Name = "colAttachment";
            this.colAttachment.ReadOnly = true;
            this.colAttachment.Width = 35;
            // 
            // colSender
            // 
            this.colSender.HeaderText = "From";
            this.colSender.Name = "colSender";
            this.colSender.ReadOnly = true;
            this.colSender.Width = 140;
            // 
            // colSubject
            // 
            this.colSubject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSubject.HeaderText = "Subject";
            this.colSubject.Name = "colSubject";
            this.colSubject.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.HeaderText = "Date";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            this.colDate.Width = 125;
            // 
            // colFolder
            // 
            this.colFolder.HeaderText = "Folder";
            this.colFolder.Name = "colFolder";
            this.colFolder.ReadOnly = true;
            this.colFolder.Width = 100;
            // 
            // colSize
            // 
            this.colSize.HeaderText = "Size";
            this.colSize.Name = "colSize";
            this.colSize.ReadOnly = true;
            this.colSize.Width = 65;
            // 
            // contextMenuResults
            // 
            this.contextMenuResults.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOpenOutlook,
            this.toolStripSeparator1,
            this.menuItemCopySubject,
            this.menuItemCopySender,
            this.menuItemCopyBody,
            this.menuItemCopyAll,
            this.toolStripSeparator2,
            this.menuItemDelete});
            this.contextMenuResults.Name = "contextMenuResults";
            this.contextMenuResults.Size = new System.Drawing.Size(206, 148);
            // 
            // menuItemOpenOutlook
            // 
            this.menuItemOpenOutlook.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuItemOpenOutlook.Name = "menuItemOpenOutlook";
            this.menuItemOpenOutlook.ShortcutKeyDisplayString = "Enter";
            this.menuItemOpenOutlook.Size = new System.Drawing.Size(205, 22);
            this.menuItemOpenOutlook.Text = "📧 Open in Outlook";
            this.menuItemOpenOutlook.Click += new System.EventHandler(this.btnOpenInOutlook_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // menuItemCopySubject
            // 
            this.menuItemCopySubject.Name = "menuItemCopySubject";
            this.menuItemCopySubject.Size = new System.Drawing.Size(205, 22);
            this.menuItemCopySubject.Text = "📋 Copy Subject";
            this.menuItemCopySubject.Click += new System.EventHandler(this.menuItemCopySubject_Click);
            // 
            // menuItemCopySender
            // 
            this.menuItemCopySender.Name = "menuItemCopySender";
            this.menuItemCopySender.Size = new System.Drawing.Size(205, 22);
            this.menuItemCopySender.Text = "📋 Copy Sender Email";
            this.menuItemCopySender.Click += new System.EventHandler(this.menuItemCopySender_Click);
            // 
            // menuItemCopyBody
            // 
            this.menuItemCopyBody.Name = "menuItemCopyBody";
            this.menuItemCopyBody.Size = new System.Drawing.Size(205, 22);
            this.menuItemCopyBody.Text = "📋 Copy Email Body";
            this.menuItemCopyBody.Click += new System.EventHandler(this.btnCopyBody_Click);
            // 
            // menuItemCopyAll
            // 
            this.menuItemCopyAll.Name = "menuItemCopyAll";
            this.menuItemCopyAll.ShortcutKeyDisplayString = "Ctrl+C";
            this.menuItemCopyAll.Size = new System.Drawing.Size(205, 22);
            this.menuItemCopyAll.Text = "📋 Copy All Info";
            this.menuItemCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(202, 6);
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.ForeColor = System.Drawing.Color.Maroon;
            this.menuItemDelete.Name = "menuItemDelete";
            this.menuItemDelete.ShortcutKeyDisplayString = "Del";
            this.menuItemDelete.Size = new System.Drawing.Size(205, 22);
            this.menuItemDelete.Text = "🗑 Delete Email";
            this.menuItemDelete.Click += new System.EventHandler(this.btnDeleteEmail_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.BackColor = System.Drawing.Color.White;
            this.panelPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPreview.Controls.Add(this.webBrowserPreview);
            this.panelPreview.Controls.Add(this.txtPlainTextPreview);
            this.panelPreview.Controls.Add(this.flowAttachments);
            this.panelPreview.Controls.Add(this.panelPreviewToolbar);
            this.panelPreview.Controls.Add(this.panelPreviewHeader);
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(0, 0);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(539, 495);
            this.panelPreview.TabIndex = 0;
            // 
            // webBrowserPreview
            // 
            this.webBrowserPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserPreview.Location = new System.Drawing.Point(0, 166);
            this.webBrowserPreview.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserPreview.Name = "webBrowserPreview";
            this.webBrowserPreview.Size = new System.Drawing.Size(537, 327);
            this.webBrowserPreview.TabIndex = 3;
            // 
            // txtPlainTextPreview
            // 
            this.txtPlainTextPreview.BackColor = System.Drawing.Color.White;
            this.txtPlainTextPreview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlainTextPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlainTextPreview.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPlainTextPreview.Location = new System.Drawing.Point(0, 166);
            this.txtPlainTextPreview.Multiline = true;
            this.txtPlainTextPreview.Name = "txtPlainTextPreview";
            this.txtPlainTextPreview.ReadOnly = true;
            this.txtPlainTextPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlainTextPreview.Size = new System.Drawing.Size(537, 327);
            this.txtPlainTextPreview.TabIndex = 4;
            this.txtPlainTextPreview.Visible = false;
            // 
            // flowAttachments
            // 
            this.flowAttachments.AutoSize = true;
            this.flowAttachments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.flowAttachments.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowAttachments.Location = new System.Drawing.Point(0, 166);
            this.flowAttachments.Name = "flowAttachments";
            this.flowAttachments.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.flowAttachments.Size = new System.Drawing.Size(537, 8);
            this.flowAttachments.TabIndex = 2;
            // 
            // panelPreviewToolbar
            // 
            this.panelPreviewToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelPreviewToolbar.Controls.Add(this.btnToggleHtmlText);
            this.panelPreviewToolbar.Controls.Add(this.btnDeleteEmail);
            this.panelPreviewToolbar.Controls.Add(this.btnCopyBody);
            this.panelPreviewToolbar.Controls.Add(this.btnCopyAll);
            this.panelPreviewToolbar.Controls.Add(this.btnOpenInOutlook);
            this.panelPreviewToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPreviewToolbar.Location = new System.Drawing.Point(0, 126);
            this.panelPreviewToolbar.Name = "panelPreviewToolbar";
            this.panelPreviewToolbar.Size = new System.Drawing.Size(537, 40);
            this.panelPreviewToolbar.TabIndex = 1;
            // 
            // btnToggleHtmlText
            // 
            this.btnToggleHtmlText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleHtmlText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnToggleHtmlText.Location = new System.Drawing.Point(448, 6);
            this.btnToggleHtmlText.Name = "btnToggleHtmlText";
            this.btnToggleHtmlText.Size = new System.Drawing.Size(82, 28);
            this.btnToggleHtmlText.TabIndex = 4;
            this.btnToggleHtmlText.Text = "Plain Text";
            this.btnToggleHtmlText.UseVisualStyleBackColor = true;
            this.btnToggleHtmlText.Click += new System.EventHandler(this.btnToggleHtmlText_Click);
            // 
            // btnDeleteEmail
            // 
            this.btnDeleteEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDeleteEmail.ForeColor = System.Drawing.Color.Maroon;
            this.btnDeleteEmail.Location = new System.Drawing.Point(324, 6);
            this.btnDeleteEmail.Name = "btnDeleteEmail";
            this.btnDeleteEmail.Size = new System.Drawing.Size(78, 28);
            this.btnDeleteEmail.TabIndex = 3;
            this.btnDeleteEmail.Text = "🗑 Delete";
            this.btnDeleteEmail.UseVisualStyleBackColor = true;
            this.btnDeleteEmail.Click += new System.EventHandler(this.btnDeleteEmail_Click);
            // 
            // btnCopyBody
            // 
            this.btnCopyBody.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCopyBody.Location = new System.Drawing.Point(232, 6);
            this.btnCopyBody.Name = "btnCopyBody";
            this.btnCopyBody.Size = new System.Drawing.Size(86, 28);
            this.btnCopyBody.TabIndex = 2;
            this.btnCopyBody.Text = "📋 Copy Body";
            this.btnCopyBody.UseVisualStyleBackColor = true;
            this.btnCopyBody.Click += new System.EventHandler(this.btnCopyBody_Click);
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnCopyAll.Location = new System.Drawing.Point(145, 6);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(81, 28);
            this.btnCopyAll.TabIndex = 1;
            this.btnCopyAll.Text = "📋 Copy All";
            this.btnCopyAll.UseVisualStyleBackColor = true;
            this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // btnOpenInOutlook
            // 
            this.btnOpenInOutlook.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnOpenInOutlook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenInOutlook.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnOpenInOutlook.ForeColor = System.Drawing.Color.White;
            this.btnOpenInOutlook.Location = new System.Drawing.Point(8, 6);
            this.btnOpenInOutlook.Name = "btnOpenInOutlook";
            this.btnOpenInOutlook.Size = new System.Drawing.Size(131, 28);
            this.btnOpenInOutlook.TabIndex = 0;
            this.btnOpenInOutlook.Text = "📧 Open in Outlook";
            this.btnOpenInOutlook.UseVisualStyleBackColor = false;
            this.btnOpenInOutlook.Click += new System.EventHandler(this.btnOpenInOutlook_Click);
            // 
            // panelPreviewHeader
            // 
            this.panelPreviewHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.panelPreviewHeader.Controls.Add(this.lblPreviewFolder);
            this.panelPreviewHeader.Controls.Add(this.lblPreviewDate);
            this.panelPreviewHeader.Controls.Add(this.lblPreviewRecipients);
            this.panelPreviewHeader.Controls.Add(this.lblPreviewSender);
            this.panelPreviewHeader.Controls.Add(this.lblPreviewSubject);
            this.panelPreviewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPreviewHeader.Location = new System.Drawing.Point(0, 0);
            this.panelPreviewHeader.Name = "panelPreviewHeader";
            this.panelPreviewHeader.Padding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.panelPreviewHeader.Size = new System.Drawing.Size(537, 126);
            this.panelPreviewHeader.TabIndex = 0;
            // 
            // lblPreviewFolder
            // 
            this.lblPreviewFolder.AutoEllipsis = true;
            this.lblPreviewFolder.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPreviewFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(105)))), ((int)(((byte)(115)))));
            this.lblPreviewFolder.Location = new System.Drawing.Point(12, 98);
            this.lblPreviewFolder.Name = "lblPreviewFolder";
            this.lblPreviewFolder.Size = new System.Drawing.Size(515, 18);
            this.lblPreviewFolder.TabIndex = 4;
            this.lblPreviewFolder.Text = "📁 Folder: Inbox";
            // 
            // lblPreviewDate
            // 
            this.lblPreviewDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewDate.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPreviewDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblPreviewDate.Location = new System.Drawing.Point(340, 36);
            this.lblPreviewDate.Name = "lblPreviewDate";
            this.lblPreviewDate.Size = new System.Drawing.Size(187, 18);
            this.lblPreviewDate.TabIndex = 3;
            this.lblPreviewDate.Text = "Date: ...";
            this.lblPreviewDate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPreviewRecipients
            // 
            this.lblPreviewRecipients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewRecipients.AutoEllipsis = true;
            this.lblPreviewRecipients.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblPreviewRecipients.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPreviewRecipients.Location = new System.Drawing.Point(12, 58);
            this.lblPreviewRecipients.Name = "lblPreviewRecipients";
            this.lblPreviewRecipients.Size = new System.Drawing.Size(515, 36);
            this.lblPreviewRecipients.TabIndex = 2;
            this.lblPreviewRecipients.Text = "To: ...";
            // 
            // lblPreviewSender
            // 
            this.lblPreviewSender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewSender.AutoEllipsis = true;
            this.lblPreviewSender.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblPreviewSender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblPreviewSender.Location = new System.Drawing.Point(12, 35);
            this.lblPreviewSender.Name = "lblPreviewSender";
            this.lblPreviewSender.Size = new System.Drawing.Size(322, 20);
            this.lblPreviewSender.TabIndex = 1;
            this.lblPreviewSender.Text = "From: ...";
            // 
            // lblPreviewSubject
            // 
            this.lblPreviewSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewSubject.AutoEllipsis = true;
            this.lblPreviewSubject.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblPreviewSubject.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.lblPreviewSubject.Location = new System.Drawing.Point(12, 8);
            this.lblPreviewSubject.Name = "lblPreviewSubject";
            this.lblPreviewSubject.Size = new System.Drawing.Size(515, 24);
            this.lblPreviewSubject.TabIndex = 0;
            this.lblPreviewSubject.Text = "(Select an email to view preview)";
            // 
            // statusStripBottom
            // 
            this.statusStripBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(243)))), ((int)(((byte)(246)))));
            this.statusStripBottom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLblResults,
            this.statusLblSpring,
            this.statusLblIndexProgress,
            this.statusProgressBar,
            this.statusBtnCancelIndex,
            this.statusLblIndexState});
            this.statusStripBottom.Location = new System.Drawing.Point(0, 679);
            this.statusStripBottom.Name = "statusStripBottom";
            this.statusStripBottom.Size = new System.Drawing.Size(1184, 22);
            this.statusStripBottom.TabIndex = 4;
            // 
            // statusLblResults
            // 
            this.statusLblResults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLblResults.Name = "statusLblResults";
            this.statusLblResults.Size = new System.Drawing.Size(112, 17);
            this.statusLblResults.Text = "0 emails found (0ms)";
            // 
            // statusLblSpring
            // 
            this.statusLblSpring.Name = "statusLblSpring";
            this.statusLblSpring.Size = new System.Drawing.Size(786, 17);
            this.statusLblSpring.Spring = true;
            // 
            // statusLblIndexProgress
            // 
            this.statusLblIndexProgress.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.statusLblIndexProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.statusLblIndexProgress.Name = "statusLblIndexProgress";
            this.statusLblIndexProgress.Size = new System.Drawing.Size(0, 17);
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(120, 16);
            this.statusProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.statusProgressBar.Visible = false;
            // 
            // statusBtnCancelIndex
            // 
            this.statusBtnCancelIndex.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusBtnCancelIndex.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.statusBtnCancelIndex.ForeColor = System.Drawing.Color.Maroon;
            this.statusBtnCancelIndex.Name = "statusBtnCancelIndex";
            this.statusBtnCancelIndex.ShowDropDownArrow = false;
            this.statusBtnCancelIndex.Size = new System.Drawing.Size(49, 20);
            this.statusBtnCancelIndex.Text = "Cancel";
            this.statusBtnCancelIndex.Visible = false;
            this.statusBtnCancelIndex.Click += new System.EventHandler(this.statusBtnCancelIndex_Click);
            // 
            // statusLblIndexState
            // 
            this.statusLblIndexState.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusLblIndexState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.statusLblIndexState.Name = "statusLblIndexState";
            this.statusLblIndexState.Size = new System.Drawing.Size(91, 17);
            this.statusLblIndexState.Text = "🟢 Index Ready";
            // 
            // timerAutoSync
            // 
            this.timerAutoSync.Tick += new System.EventHandler(this.timerAutoSync_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 701);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.statusStripBottom);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelSearchSection);
            this.Controls.Add(this.panelTopHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BettyMailZoom - Local Outlook Fast Email Search Helper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelTopHeader.ResumeLayout(false);
            this.panelTopHeader.PerformLayout();
            this.panelSearchSection.ResumeLayout(false);
            this.panelSearchSection.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.contextMenuResults.ResumeLayout(false);
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            this.panelPreviewToolbar.ResumeLayout(false);
            this.panelPreviewHeader.ResumeLayout(false);
            this.statusStripBottom.ResumeLayout(false);
            this.statusStripBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelTopHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnRefreshIndex;
        private System.Windows.Forms.Button btnRebuildIndex;
        private System.Windows.Forms.Button btnFolders;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel panelSearchSection;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.TextBox txtSearchKeyword;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Button btnToggleFilters;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.Label lblSender;
        private System.Windows.Forms.TextBox txtSender;
        private System.Windows.Forms.Label lblRecipient;
        private System.Windows.Forms.TextBox txtRecipient;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.TextBox txtExclude;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.ComboBox cmbAttachment;
        private System.Windows.Forms.Label lblAttachmentExt;
        private System.Windows.Forms.TextBox txtAttachmentExt;
        private System.Windows.Forms.Label lblImportance;
        private System.Windows.Forms.ComboBox cmbImportance;
        private System.Windows.Forms.Label lblDateRange;
        private System.Windows.Forms.ComboBox cmbDateRange;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.ComboBox cmbFolder;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.CheckBox chkUnreadOnly;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Panel panelPreviewHeader;
        private System.Windows.Forms.Label lblPreviewSubject;
        private System.Windows.Forms.Label lblPreviewSender;
        private System.Windows.Forms.Label lblPreviewRecipients;
        private System.Windows.Forms.Label lblPreviewDate;
        private System.Windows.Forms.Label lblPreviewFolder;
        private System.Windows.Forms.Panel panelPreviewToolbar;
        private System.Windows.Forms.Button btnOpenInOutlook;
        private System.Windows.Forms.Button btnCopyAll;
        private System.Windows.Forms.Button btnCopyBody;
        private System.Windows.Forms.Button btnDeleteEmail;
        private System.Windows.Forms.Button btnToggleHtmlText;
        private System.Windows.Forms.FlowLayoutPanel flowAttachments;
        private System.Windows.Forms.WebBrowser webBrowserPreview;
        private System.Windows.Forms.TextBox txtPlainTextPreview;
        private System.Windows.Forms.StatusStrip statusStripBottom;
        private System.Windows.Forms.ToolStripStatusLabel statusLblResults;
        private System.Windows.Forms.ToolStripStatusLabel statusLblSpring;
        private System.Windows.Forms.ToolStripStatusLabel statusLblIndexProgress;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.ToolStripDropDownButton statusBtnCancelIndex;
        private System.Windows.Forms.ToolStripStatusLabel statusLblIndexState;
        private System.Windows.Forms.Timer timerAutoSync;
        private System.Windows.Forms.ContextMenuStrip contextMenuResults;
        private System.Windows.Forms.ToolStripMenuItem menuItemOpenOutlook;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopySubject;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopySender;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopyBody;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopyAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImportance;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSize;
    }
}
