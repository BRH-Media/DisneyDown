
namespace DisneyDown.Common.KeySystem.Manager.UI
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DisneyDown.Common.Util.Kit.Components.Styling.BoolColour boolColour2 = new DisneyDown.Common.Util.Kit.Components.Styling.BoolColour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.itmConnectionSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.itmData = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataReportKey = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataKeyLookup = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataKeyLookupServer = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataKeyLookupClient = new System.Windows.Forms.ToolStripMenuItem();
            this.itmDataWhitelist = new System.Windows.Forms.ToolStripMenuItem();
            this.itmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.itmHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.lblViewing = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblViewingValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvMain = new DisneyDown.Common.Util.Kit.Components.FlatDataGridView();
            this.lblUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUserValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmConnection,
            this.itmData,
            this.itmHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 0;
            // 
            // itmFile
            // 
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmConnection
            // 
            this.itmConnection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmConnectionSetup});
            this.itmConnection.Name = "itmConnection";
            this.itmConnection.Size = new System.Drawing.Size(81, 20);
            this.itmConnection.Text = "Connection";
            // 
            // itmConnectionSetup
            // 
            this.itmConnectionSetup.Name = "itmConnectionSetup";
            this.itmConnectionSetup.Size = new System.Drawing.Size(104, 22);
            this.itmConnectionSetup.Text = "Setup";
            // 
            // itmData
            // 
            this.itmData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDataReportKey,
            this.itmDataKeyLookup,
            this.itmDataWhitelist});
            this.itmData.Name = "itmData";
            this.itmData.Size = new System.Drawing.Size(43, 20);
            this.itmData.Text = "Data";
            // 
            // itmDataReportKey
            // 
            this.itmDataReportKey.Name = "itmDataReportKey";
            this.itmDataReportKey.Size = new System.Drawing.Size(180, 22);
            this.itmDataReportKey.Text = "Report Key";
            // 
            // itmDataKeyLookup
            // 
            this.itmDataKeyLookup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmDataKeyLookupServer,
            this.itmDataKeyLookupClient});
            this.itmDataKeyLookup.Name = "itmDataKeyLookup";
            this.itmDataKeyLookup.Size = new System.Drawing.Size(180, 22);
            this.itmDataKeyLookup.Text = "Key Lookup";
            // 
            // itmDataKeyLookupServer
            // 
            this.itmDataKeyLookupServer.Name = "itmDataKeyLookupServer";
            this.itmDataKeyLookupServer.Size = new System.Drawing.Size(132, 22);
            this.itmDataKeyLookupServer.Text = "Server-side";
            // 
            // itmDataKeyLookupClient
            // 
            this.itmDataKeyLookupClient.Name = "itmDataKeyLookupClient";
            this.itmDataKeyLookupClient.Size = new System.Drawing.Size(132, 22);
            this.itmDataKeyLookupClient.Text = "Client-side";
            // 
            // itmDataWhitelist
            // 
            this.itmDataWhitelist.Name = "itmDataWhitelist";
            this.itmDataWhitelist.Size = new System.Drawing.Size(180, 22);
            this.itmDataWhitelist.Text = "Whitelist";
            this.itmDataWhitelist.Click += new System.EventHandler(this.ItmDataWhitelist_Click);
            // 
            // itmHelp
            // 
            this.itmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmHelpAbout});
            this.itmHelp.Name = "itmHelp";
            this.itmHelp.Size = new System.Drawing.Size(44, 20);
            this.itmHelp.Text = "Help";
            // 
            // itmHelpAbout
            // 
            this.itmHelpAbout.Name = "itmHelpAbout";
            this.itmHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.itmHelpAbout.Text = "About";
            this.itmHelpAbout.Click += new System.EventHandler(this.ItmHelpAbout_Click);
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblViewing,
            this.lblViewingValue,
            this.lblUser,
            this.lblUserValue});
            this.statusMain.Location = new System.Drawing.Point(0, 428);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(800, 22);
            this.statusMain.TabIndex = 1;
            // 
            // lblViewing
            // 
            this.lblViewing.Name = "lblViewing";
            this.lblViewing.Size = new System.Drawing.Size(52, 17);
            this.lblViewing.Text = "Viewing:";
            // 
            // lblViewingValue
            // 
            this.lblViewingValue.Name = "lblViewingValue";
            this.lblViewingValue.Size = new System.Drawing.Size(24, 17);
            this.lblViewingValue.Text = "0/0";
            // 
            // dgvMain
            // 
            this.dgvMain.AllowUserToAddRows = false;
            this.dgvMain.AllowUserToDeleteRows = false;
            this.dgvMain.AllowUserToOrderColumns = true;
            this.dgvMain.AllowUserToResizeRows = false;
            this.dgvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            boolColour2.BoolColouringEnabled = false;
            boolColour2.ColouringMode = DisneyDown.Common.Util.Kit.Components.Styling.BoolColourMode.BackColour;
            boolColour2.FalseColour = System.Drawing.Color.DarkRed;
            boolColour2.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour2.RelevantColumns")));
            boolColour2.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvMain.BoolColouringScheme = boolColour2;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMain.CellContentClickMessage = false;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMain.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.dgvMain.IsContentTable = false;
            this.dgvMain.Location = new System.Drawing.Point(0, 24);
            this.dgvMain.MultiSelect = false;
            this.dgvMain.Name = "dgvMain";
            this.dgvMain.RowHeadersVisible = false;
            this.dgvMain.RowsEmptyText = "No Data Found";
            this.dgvMain.RowsEmptyTextForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(134)))), ((int)(((byte)(134)))));
            this.dgvMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMain.Size = new System.Drawing.Size(800, 404);
            this.dgvMain.TabIndex = 2;
            // 
            // lblUser
            // 
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(33, 17);
            this.lblUser.Text = "User:";
            // 
            // lblUserValue
            // 
            this.lblUserValue.ForeColor = System.Drawing.Color.DarkRed;
            this.lblUserValue.Name = "lblUserValue";
            this.lblUserValue.Size = new System.Drawing.Size(95, 17);
            this.lblUserValue.Text = "Unauthenticated";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "Home";
            this.Text = "Manager";
            this.Load += new System.EventHandler(this.Home_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.ToolStripStatusLabel lblViewing;
        private System.Windows.Forms.ToolStripStatusLabel lblViewingValue;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmConnection;
        private System.Windows.Forms.ToolStripMenuItem itmData;
        private System.Windows.Forms.ToolStripMenuItem itmHelp;
        private System.Windows.Forms.ToolStripMenuItem itmHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem itmConnectionSetup;
        private Util.Kit.Components.FlatDataGridView dgvMain;
        private System.Windows.Forms.ToolStripMenuItem itmDataReportKey;
        private System.Windows.Forms.ToolStripMenuItem itmDataKeyLookup;
        private System.Windows.Forms.ToolStripMenuItem itmDataKeyLookupServer;
        private System.Windows.Forms.ToolStripMenuItem itmDataKeyLookupClient;
        private System.Windows.Forms.ToolStripMenuItem itmDataWhitelist;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;
        private System.Windows.Forms.ToolStripStatusLabel lblUserValue;
    }
}

