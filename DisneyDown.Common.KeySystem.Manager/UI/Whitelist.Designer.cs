
namespace DisneyDown.Common.KeySystem.Manager.UI
{
    partial class Whitelist
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
            DisneyDown.Common.Util.Kit.Components.Styling.BoolColour boolColour1 = new DisneyDown.Common.Util.Kit.Components.Styling.BoolColour();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Whitelist));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.itmFile = new System.Windows.Forms.ToolStripMenuItem();
            this.itmFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.itmSystemViewBindings = new System.Windows.Forms.ToolStripMenuItem();
            this.lblViewing = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblViewingValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgvMain = new DisneyDown.Common.Util.Kit.Components.FlatDataGridView();
            this.statusMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // statusMain
            // 
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblViewing,
            this.lblViewingValue});
            this.statusMain.Location = new System.Drawing.Point(0, 428);
            this.statusMain.Name = "statusMain";
            this.statusMain.Size = new System.Drawing.Size(800, 22);
            this.statusMain.TabIndex = 0;
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFile,
            this.itmSystem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(800, 24);
            this.menuMain.TabIndex = 1;
            // 
            // itmFile
            // 
            this.itmFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmFileExport});
            this.itmFile.Name = "itmFile";
            this.itmFile.Size = new System.Drawing.Size(37, 20);
            this.itmFile.Text = "File";
            // 
            // itmFileExport
            // 
            this.itmFileExport.Name = "itmFileExport";
            this.itmFileExport.Size = new System.Drawing.Size(180, 22);
            this.itmFileExport.Text = "Export";
            // 
            // itmSystem
            // 
            this.itmSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itmSystemViewBindings});
            this.itmSystem.Name = "itmSystem";
            this.itmSystem.Size = new System.Drawing.Size(57, 20);
            this.itmSystem.Text = "System";
            // 
            // itmSystemViewBindings
            // 
            this.itmSystemViewBindings.Name = "itmSystemViewBindings";
            this.itmSystemViewBindings.Size = new System.Drawing.Size(180, 22);
            this.itmSystemViewBindings.Text = "View Bindings";
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
            boolColour1.BoolColouringEnabled = false;
            boolColour1.ColouringMode = DisneyDown.Common.Util.Kit.Components.Styling.BoolColourMode.BackColour;
            boolColour1.FalseColour = System.Drawing.Color.DarkRed;
            boolColour1.RelevantColumns = ((System.Collections.Generic.List<string>)(resources.GetObject("boolColour1.RelevantColumns")));
            boolColour1.TrueColour = System.Drawing.Color.DarkGreen;
            this.dgvMain.BoolColouringScheme = boolColour1;
            this.dgvMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvMain.CellContentClickMessage = false;
            this.dgvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMain.DefaultCellStyle = dataGridViewCellStyle1;
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
            // Whitelist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvMain);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "Whitelist";
            this.Text = "Whitelist";
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem itmFile;
        private System.Windows.Forms.ToolStripMenuItem itmFileExport;
        private System.Windows.Forms.ToolStripMenuItem itmSystem;
        private System.Windows.Forms.ToolStripMenuItem itmSystemViewBindings;
        private System.Windows.Forms.ToolStripStatusLabel lblViewing;
        private System.Windows.Forms.ToolStripStatusLabel lblViewingValue;
        private Util.Kit.Components.FlatDataGridView dgvMain;
    }
}