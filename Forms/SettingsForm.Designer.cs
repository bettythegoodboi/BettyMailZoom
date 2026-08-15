namespace BettyMailZoom.Forms
{
    partial class SettingsForm
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.chkAutoSyncStartup = new System.Windows.Forms.CheckBox();
            this.lblAutoSync = new System.Windows.Forms.Label();
            this.cmbAutoSync = new System.Windows.Forms.ComboBox();
            this.lblMaxResults = new System.Windows.Forms.Label();
            this.numMaxResults = new System.Windows.Forms.NumericUpDown();
            this.lblPreviewPane = new System.Windows.Forms.Label();
            this.cmbPreviewPane = new System.Windows.Forms.ComboBox();
            this.chkIndexBody = new System.Windows.Forms.CheckBox();
            this.grpDatabase = new System.Windows.Forms.GroupBox();
            this.lblDbPath = new System.Windows.Forms.Label();
            this.lblDbSize = new System.Windows.Forms.Label();
            this.lblDbItems = new System.Windows.Forms.Label();
            this.lblLastSync = new System.Windows.Forms.Label();
            this.btnPurgeDatabase = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResults)).BeginInit();
            this.grpDatabase.SuspendLayout();
            this.grpGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(12, 12);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(188, 20);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "BettyMailZoom Settings";
            // 
            // chkAutoSyncStartup
            // 
            this.chkAutoSyncStartup.AutoSize = true;
            this.chkAutoSyncStartup.Location = new System.Drawing.Point(16, 26);
            this.chkAutoSyncStartup.Name = "chkAutoSyncStartup";
            this.chkAutoSyncStartup.Size = new System.Drawing.Size(262, 19);
            this.chkAutoSyncStartup.TabIndex = 0;
            this.chkAutoSyncStartup.Text = "Automatically sync new emails when app starts";
            this.chkAutoSyncStartup.UseVisualStyleBackColor = true;
            // 
            // lblAutoSync
            // 
            this.lblAutoSync.AutoSize = true;
            this.lblAutoSync.Location = new System.Drawing.Point(13, 60);
            this.lblAutoSync.Name = "lblAutoSync";
            this.lblAutoSync.Size = new System.Drawing.Size(148, 15);
            this.lblAutoSync.TabIndex = 1;
            this.lblAutoSync.Text = "Background Sync Interval:";
            // 
            // cmbAutoSync
            // 
            this.cmbAutoSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAutoSync.FormattingEnabled = true;
            this.cmbAutoSync.Items.AddRange(new object[] {
            "Disabled (Manual only)",
            "Every 5 minutes",
            "Every 15 minutes",
            "Every 30 minutes",
            "Every 60 minutes"});
            this.cmbAutoSync.Location = new System.Drawing.Point(180, 57);
            this.cmbAutoSync.Name = "cmbAutoSync";
            this.cmbAutoSync.Size = new System.Drawing.Size(195, 23);
            this.cmbAutoSync.TabIndex = 2;
            // 
            // lblMaxResults
            // 
            this.lblMaxResults.AutoSize = true;
            this.lblMaxResults.Location = new System.Drawing.Point(13, 95);
            this.lblMaxResults.Name = "lblMaxResults";
            this.lblMaxResults.Size = new System.Drawing.Size(139, 15);
            this.lblMaxResults.TabIndex = 3;
            this.lblMaxResults.Text = "Max Search Results Limit:";
            // 
            // numMaxResults
            // 
            this.numMaxResults.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxResults.Location = new System.Drawing.Point(180, 93);
            this.numMaxResults.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxResults.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numMaxResults.Name = "numMaxResults";
            this.numMaxResults.Size = new System.Drawing.Size(100, 23);
            this.numMaxResults.TabIndex = 4;
            this.numMaxResults.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblPreviewPane
            // 
            this.lblPreviewPane.AutoSize = true;
            this.lblPreviewPane.Location = new System.Drawing.Point(13, 131);
            this.lblPreviewPane.Name = "lblPreviewPane";
            this.lblPreviewPane.Size = new System.Drawing.Size(130, 15);
            this.lblPreviewPane.TabIndex = 5;
            this.lblPreviewPane.Text = "Preview Pane Position:";
            // 
            // cmbPreviewPane
            // 
            this.cmbPreviewPane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPreviewPane.FormattingEnabled = true;
            this.cmbPreviewPane.Items.AddRange(new object[] {
            "Right",
            "Bottom",
            "Hidden"});
            this.cmbPreviewPane.Location = new System.Drawing.Point(180, 128);
            this.cmbPreviewPane.Name = "cmbPreviewPane";
            this.cmbPreviewPane.Size = new System.Drawing.Size(195, 23);
            this.cmbPreviewPane.TabIndex = 6;
            // 
            // chkIndexBody
            // 
            this.chkIndexBody.AutoSize = true;
            this.chkIndexBody.Location = new System.Drawing.Point(16, 166);
            this.chkIndexBody.Name = "chkIndexBody";
            this.chkIndexBody.Size = new System.Drawing.Size(262, 19);
            this.chkIndexBody.TabIndex = 7;
            this.chkIndexBody.Text = "Index full email body text for deep searches";
            this.chkIndexBody.UseVisualStyleBackColor = true;
            // 
            // grpDatabase
            // 
            this.grpDatabase.Controls.Add(this.btnPurgeDatabase);
            this.grpDatabase.Controls.Add(this.lblLastSync);
            this.grpDatabase.Controls.Add(this.lblDbItems);
            this.grpDatabase.Controls.Add(this.lblDbSize);
            this.grpDatabase.Controls.Add(this.lblDbPath);
            this.grpDatabase.Location = new System.Drawing.Point(16, 252);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(438, 150);
            this.grpDatabase.TabIndex = 2;
            this.grpDatabase.TabStop = false;
            this.grpDatabase.Text = "Local Search Database (SQLite)";
            // 
            // lblDbPath
            // 
            this.lblDbPath.AutoEllipsis = true;
            this.lblDbPath.Location = new System.Drawing.Point(13, 25);
            this.lblDbPath.Name = "lblDbPath";
            this.lblDbPath.Size = new System.Drawing.Size(410, 18);
            this.lblDbPath.TabIndex = 0;
            this.lblDbPath.Text = "Location: ...";
            // 
            // lblDbSize
            // 
            this.lblDbSize.AutoSize = true;
            this.lblDbSize.Location = new System.Drawing.Point(13, 49);
            this.lblDbSize.Name = "lblDbSize";
            this.lblDbSize.Size = new System.Drawing.Size(89, 15);
            this.lblDbSize.TabIndex = 1;
            this.lblDbSize.Text = "Database Size: ...";
            // 
            // lblDbItems
            // 
            this.lblDbItems.AutoSize = true;
            this.lblDbItems.Location = new System.Drawing.Point(13, 73);
            this.lblDbItems.Name = "lblDbItems";
            this.lblDbItems.Size = new System.Drawing.Size(126, 15);
            this.lblDbItems.TabIndex = 2;
            this.lblDbItems.Text = "Total Indexed Emails: ...";
            // 
            // lblLastSync
            // 
            this.lblLastSync.AutoSize = true;
            this.lblLastSync.Location = new System.Drawing.Point(13, 97);
            this.lblLastSync.Name = "lblLastSync";
            this.lblLastSync.Size = new System.Drawing.Size(107, 15);
            this.lblLastSync.TabIndex = 3;
            this.lblLastSync.Text = "Last Synced Time: ...";
            // 
            // btnPurgeDatabase
            // 
            this.btnPurgeDatabase.ForeColor = System.Drawing.Color.Maroon;
            this.btnPurgeDatabase.Location = new System.Drawing.Point(16, 117);
            this.btnPurgeDatabase.Name = "btnPurgeDatabase";
            this.btnPurgeDatabase.Size = new System.Drawing.Size(140, 25);
            this.btnPurgeDatabase.TabIndex = 4;
            this.btnPurgeDatabase.Text = "Clear Index Database";
            this.btnPurgeDatabase.UseVisualStyleBackColor = true;
            this.btnPurgeDatabase.Click += new System.EventHandler(this.btnPurgeDatabase_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(369, 412);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 28);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(278, 412);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(85, 28);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpGeneral
            // 
            this.grpGeneral.Controls.Add(this.chkAutoSyncStartup);
            this.grpGeneral.Controls.Add(this.lblAutoSync);
            this.grpGeneral.Controls.Add(this.cmbAutoSync);
            this.grpGeneral.Controls.Add(this.lblMaxResults);
            this.grpGeneral.Controls.Add(this.chkIndexBody);
            this.grpGeneral.Controls.Add(this.numMaxResults);
            this.grpGeneral.Controls.Add(this.cmbPreviewPane);
            this.grpGeneral.Controls.Add(this.lblPreviewPane);
            this.grpGeneral.Location = new System.Drawing.Point(16, 42);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(438, 201);
            this.grpGeneral.TabIndex = 1;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "Search & Sync Preferences";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 452);
            this.Controls.Add(this.grpGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpDatabase);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResults)).EndInit();
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.CheckBox chkAutoSyncStartup;
        private System.Windows.Forms.Label lblAutoSync;
        private System.Windows.Forms.ComboBox cmbAutoSync;
        private System.Windows.Forms.Label lblMaxResults;
        private System.Windows.Forms.NumericUpDown numMaxResults;
        private System.Windows.Forms.Label lblPreviewPane;
        private System.Windows.Forms.ComboBox cmbPreviewPane;
        private System.Windows.Forms.CheckBox chkIndexBody;
        private System.Windows.Forms.GroupBox grpDatabase;
        private System.Windows.Forms.Label lblDbPath;
        private System.Windows.Forms.Label lblDbSize;
        private System.Windows.Forms.Label lblDbItems;
        private System.Windows.Forms.Label lblLastSync;
        private System.Windows.Forms.Button btnPurgeDatabase;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox grpGeneral;
    }
}
