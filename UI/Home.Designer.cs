namespace DisneyDown.UI
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.gbMediaInformation = new System.Windows.Forms.GroupBox();
            this.tlpMediaInformation = new System.Windows.Forms.TableLayoutPanel();
            this.gbDecryptionKey = new System.Windows.Forms.GroupBox();
            this.txtDecryptionKey = new System.Windows.Forms.TextBox();
            this.gbContentURL = new System.Windows.Forms.GroupBox();
            this.txtContentURL = new System.Windows.Forms.TextBox();
            this.gbDisneyPlusLogin = new System.Windows.Forms.GroupBox();
            this.tlpDisneyPlusLogin = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLoginCredentials = new System.Windows.Forms.TableLayoutPanel();
            this.gbPassword = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.gbUsername = new System.Windows.Forms.GroupBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnAssembleAndDecrypt = new System.Windows.Forms.Button();
            this.tlpMain.SuspendLayout();
            this.gbMediaInformation.SuspendLayout();
            this.tlpMediaInformation.SuspendLayout();
            this.gbDecryptionKey.SuspendLayout();
            this.gbContentURL.SuspendLayout();
            this.gbDisneyPlusLogin.SuspendLayout();
            this.tlpDisneyPlusLogin.SuspendLayout();
            this.tlpLoginCredentials.SuspendLayout();
            this.gbPassword.SuspendLayout();
            this.gbUsername.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Controls.Add(this.gbMediaInformation, 0, 0);
            this.tlpMain.Controls.Add(this.gbDisneyPlusLogin, 1, 0);
            this.tlpMain.Controls.Add(this.btnAssembleAndDecrypt, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMain.Size = new System.Drawing.Size(800, 450);
            this.tlpMain.TabIndex = 0;
            // 
            // gbMediaInformation
            // 
            this.gbMediaInformation.Controls.Add(this.tlpMediaInformation);
            this.gbMediaInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMediaInformation.Location = new System.Drawing.Point(3, 3);
            this.gbMediaInformation.Name = "gbMediaInformation";
            this.gbMediaInformation.Size = new System.Drawing.Size(394, 219);
            this.gbMediaInformation.TabIndex = 0;
            this.gbMediaInformation.TabStop = false;
            this.gbMediaInformation.Text = "Media Information";
            // 
            // tlpMediaInformation
            // 
            this.tlpMediaInformation.ColumnCount = 2;
            this.tlpMediaInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMediaInformation.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMediaInformation.Controls.Add(this.gbDecryptionKey, 0, 0);
            this.tlpMediaInformation.Controls.Add(this.gbContentURL, 0, 1);
            this.tlpMediaInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMediaInformation.Location = new System.Drawing.Point(3, 16);
            this.tlpMediaInformation.Name = "tlpMediaInformation";
            this.tlpMediaInformation.RowCount = 2;
            this.tlpMediaInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMediaInformation.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMediaInformation.Size = new System.Drawing.Size(388, 200);
            this.tlpMediaInformation.TabIndex = 0;
            // 
            // gbDecryptionKey
            // 
            this.tlpMediaInformation.SetColumnSpan(this.gbDecryptionKey, 2);
            this.gbDecryptionKey.Controls.Add(this.txtDecryptionKey);
            this.gbDecryptionKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDecryptionKey.Location = new System.Drawing.Point(3, 3);
            this.gbDecryptionKey.Name = "gbDecryptionKey";
            this.gbDecryptionKey.Size = new System.Drawing.Size(382, 94);
            this.gbDecryptionKey.TabIndex = 0;
            this.gbDecryptionKey.TabStop = false;
            this.gbDecryptionKey.Text = "Content Decryption Key";
            // 
            // txtDecryptionKey
            // 
            this.txtDecryptionKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDecryptionKey.Location = new System.Drawing.Point(3, 16);
            this.txtDecryptionKey.Multiline = true;
            this.txtDecryptionKey.Name = "txtDecryptionKey";
            this.txtDecryptionKey.Size = new System.Drawing.Size(376, 75);
            this.txtDecryptionKey.TabIndex = 0;
            // 
            // gbContentURL
            // 
            this.tlpMediaInformation.SetColumnSpan(this.gbContentURL, 2);
            this.gbContentURL.Controls.Add(this.txtContentURL);
            this.gbContentURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContentURL.Location = new System.Drawing.Point(3, 103);
            this.gbContentURL.Name = "gbContentURL";
            this.gbContentURL.Size = new System.Drawing.Size(382, 94);
            this.gbContentURL.TabIndex = 1;
            this.gbContentURL.TabStop = false;
            this.gbContentURL.Text = "Disney+ Content URL";
            // 
            // txtContentURL
            // 
            this.txtContentURL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContentURL.Location = new System.Drawing.Point(3, 16);
            this.txtContentURL.Multiline = true;
            this.txtContentURL.Name = "txtContentURL";
            this.txtContentURL.Size = new System.Drawing.Size(376, 75);
            this.txtContentURL.TabIndex = 1;
            // 
            // gbDisneyPlusLogin
            // 
            this.gbDisneyPlusLogin.Controls.Add(this.tlpDisneyPlusLogin);
            this.gbDisneyPlusLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDisneyPlusLogin.Location = new System.Drawing.Point(403, 3);
            this.gbDisneyPlusLogin.Name = "gbDisneyPlusLogin";
            this.gbDisneyPlusLogin.Size = new System.Drawing.Size(394, 219);
            this.gbDisneyPlusLogin.TabIndex = 1;
            this.gbDisneyPlusLogin.TabStop = false;
            this.gbDisneyPlusLogin.Text = "Login to Disney+";
            // 
            // tlpDisneyPlusLogin
            // 
            this.tlpDisneyPlusLogin.ColumnCount = 2;
            this.tlpDisneyPlusLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDisneyPlusLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDisneyPlusLogin.Controls.Add(this.tlpLoginCredentials, 0, 0);
            this.tlpDisneyPlusLogin.Controls.Add(this.btnLogin, 0, 1);
            this.tlpDisneyPlusLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDisneyPlusLogin.Location = new System.Drawing.Point(3, 16);
            this.tlpDisneyPlusLogin.Name = "tlpDisneyPlusLogin";
            this.tlpDisneyPlusLogin.RowCount = 2;
            this.tlpDisneyPlusLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDisneyPlusLogin.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDisneyPlusLogin.Size = new System.Drawing.Size(388, 200);
            this.tlpDisneyPlusLogin.TabIndex = 0;
            // 
            // tlpLoginCredentials
            // 
            this.tlpLoginCredentials.ColumnCount = 2;
            this.tlpDisneyPlusLogin.SetColumnSpan(this.tlpLoginCredentials, 2);
            this.tlpLoginCredentials.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCredentials.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCredentials.Controls.Add(this.gbPassword, 0, 1);
            this.tlpLoginCredentials.Controls.Add(this.gbUsername, 0, 0);
            this.tlpLoginCredentials.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLoginCredentials.Location = new System.Drawing.Point(3, 3);
            this.tlpLoginCredentials.Name = "tlpLoginCredentials";
            this.tlpLoginCredentials.RowCount = 2;
            this.tlpLoginCredentials.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCredentials.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpLoginCredentials.Size = new System.Drawing.Size(382, 165);
            this.tlpLoginCredentials.TabIndex = 0;
            // 
            // gbPassword
            // 
            this.tlpLoginCredentials.SetColumnSpan(this.gbPassword, 2);
            this.gbPassword.Controls.Add(this.txtPassword);
            this.gbPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPassword.Location = new System.Drawing.Point(3, 85);
            this.gbPassword.Name = "gbPassword";
            this.gbPassword.Size = new System.Drawing.Size(376, 77);
            this.gbPassword.TabIndex = 1;
            this.gbPassword.TabStop = false;
            this.gbPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPassword.Location = new System.Drawing.Point(3, 16);
            this.txtPassword.Multiline = true;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(370, 58);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // gbUsername
            // 
            this.tlpLoginCredentials.SetColumnSpan(this.gbUsername, 2);
            this.gbUsername.Controls.Add(this.txtUsername);
            this.gbUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUsername.Location = new System.Drawing.Point(3, 3);
            this.gbUsername.Name = "gbUsername";
            this.gbUsername.Size = new System.Drawing.Size(376, 76);
            this.gbUsername.TabIndex = 0;
            this.gbUsername.TabStop = false;
            this.gbUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUsername.Location = new System.Drawing.Point(3, 16);
            this.txtUsername.Multiline = true;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(370, 57);
            this.txtUsername.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.tlpDisneyPlusLogin.SetColumnSpan(this.btnLogin, 2);
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Location = new System.Drawing.Point(3, 174);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(382, 23);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // btnAssembleAndDecrypt
            // 
            this.btnAssembleAndDecrypt.Location = new System.Drawing.Point(3, 228);
            this.btnAssembleAndDecrypt.Name = "btnAssembleAndDecrypt";
            this.btnAssembleAndDecrypt.Size = new System.Drawing.Size(394, 23);
            this.btnAssembleAndDecrypt.TabIndex = 2;
            this.btnAssembleAndDecrypt.Text = "Assemble and Decrypt";
            this.btnAssembleAndDecrypt.UseVisualStyleBackColor = true;
            this.btnAssembleAndDecrypt.Click += new System.EventHandler(this.BtnAssembleAndDecrypt_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlpMain);
            this.Name = "Home";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Disney+ Downloader";
            this.tlpMain.ResumeLayout(false);
            this.gbMediaInformation.ResumeLayout(false);
            this.tlpMediaInformation.ResumeLayout(false);
            this.gbDecryptionKey.ResumeLayout(false);
            this.gbDecryptionKey.PerformLayout();
            this.gbContentURL.ResumeLayout(false);
            this.gbContentURL.PerformLayout();
            this.gbDisneyPlusLogin.ResumeLayout(false);
            this.tlpDisneyPlusLogin.ResumeLayout(false);
            this.tlpLoginCredentials.ResumeLayout(false);
            this.gbPassword.ResumeLayout(false);
            this.gbPassword.PerformLayout();
            this.gbUsername.ResumeLayout(false);
            this.gbUsername.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox gbMediaInformation;
        private System.Windows.Forms.TableLayoutPanel tlpMediaInformation;
        private System.Windows.Forms.GroupBox gbDecryptionKey;
        private System.Windows.Forms.TextBox txtDecryptionKey;
        private System.Windows.Forms.GroupBox gbContentURL;
        private System.Windows.Forms.TextBox txtContentURL;
        private System.Windows.Forms.GroupBox gbDisneyPlusLogin;
        private System.Windows.Forms.TableLayoutPanel tlpDisneyPlusLogin;
        private System.Windows.Forms.TableLayoutPanel tlpLoginCredentials;
        private System.Windows.Forms.GroupBox gbUsername;
        private System.Windows.Forms.GroupBox gbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnAssembleAndDecrypt;
    }
}

