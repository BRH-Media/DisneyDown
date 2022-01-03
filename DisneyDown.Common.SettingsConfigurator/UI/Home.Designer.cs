namespace DisneyDown.Common.SettingsConfigurator.UI
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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmApplySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.itmRevertSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lstSettingsSections = new System.Windows.Forms.ListBox();
            this.prgSettingsGrid = new System.Windows.Forms.PropertyGrid();
            this.menuMain.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmApplySettings,
            this.itmRevertSettings});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(556, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // itmApplySettings
            // 
            this.itmApplySettings.Name = "itmApplySettings";
            this.itmApplySettings.Size = new System.Drawing.Size(95, 20);
            this.itmApplySettings.Text = "Apply Settings";
            this.itmApplySettings.Click += new System.EventHandler(this.ItmApplySettings_Click);
            // 
            // itmRevertSettings
            // 
            this.itmRevertSettings.Name = "itmRevertSettings";
            this.itmRevertSettings.Size = new System.Drawing.Size(97, 20);
            this.itmRevertSettings.Text = "Revert Settings";
            this.itmRevertSettings.Click += new System.EventHandler(this.ItmRevertSettings_Click);
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.lstSettingsSections, 0, 0);
            this.tlpMain.Controls.Add(this.prgSettingsGrid, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(556, 426);
            this.tlpMain.TabIndex = 1;
            // 
            // lstSettingsSections
            // 
            this.lstSettingsSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSettingsSections.FormattingEnabled = true;
            this.lstSettingsSections.Items.AddRange(new object[] {
            "Segment Filtering",
            "Bandwidth Definitions",
            "Subtitles Language Priority",
            "Audio Language Priority",
            "Disney+ API",
            "Key System"});
            this.lstSettingsSections.Location = new System.Drawing.Point(3, 3);
            this.lstSettingsSections.Name = "lstSettingsSections";
            this.lstSettingsSections.Size = new System.Drawing.Size(140, 420);
            this.lstSettingsSections.TabIndex = 0;
            this.lstSettingsSections.SelectedIndexChanged += new System.EventHandler(this.LstSettingsSections_SelectedIndexChanged);
            // 
            // prgSettingsGrid
            // 
            this.prgSettingsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgSettingsGrid.Location = new System.Drawing.Point(149, 3);
            this.prgSettingsGrid.Name = "prgSettingsGrid";
            this.prgSettingsGrid.Size = new System.Drawing.Size(404, 420);
            this.prgSettingsGrid.TabIndex = 1;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 450);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "Home";
            this.Text = "Settings Configurator";
            this.Load += new System.EventHandler(this.Home_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmApplySettings;
        private System.Windows.Forms.ToolStripMenuItem itmRevertSettings;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.ListBox lstSettingsSections;
        private System.Windows.Forms.PropertyGrid prgSettingsGrid;
    }
}

