namespace Diva
{
    partial class SplashForm
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
            this.lblProgress = new System.Windows.Forms.Label();
            this.panelNewAccount = new System.Windows.Forms.Panel();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.tBoxUsername = new System.Windows.Forms.TextBox();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.tBoxConfirm = new System.Windows.Forms.TextBox();
            this.btnNewAccount = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.cBoxDontNotify = new System.Windows.Forms.CheckBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.tBoxLoginUser = new System.Windows.Forms.TextBox();
            this.lblLoginPassword = new System.Windows.Forms.Label();
            this.lblLoginUser = new System.Windows.Forms.Label();
            this.lblLoginMessage = new System.Windows.Forms.Label();
            this.lblNewAccountMessage = new System.Windows.Forms.Label();
            this.panelNewAccount.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(148, 336);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(320, 15);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Loading...";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelNewAccount
            // 
            this.panelNewAccount.Controls.Add(this.cBoxDontNotify);
            this.panelNewAccount.Controls.Add(this.btnSkip);
            this.panelNewAccount.Controls.Add(this.btnNewAccount);
            this.panelNewAccount.Controls.Add(this.tBoxConfirm);
            this.panelNewAccount.Controls.Add(this.tBoxPassword);
            this.panelNewAccount.Controls.Add(this.tBoxUsername);
            this.panelNewAccount.Controls.Add(this.lblNewAccountMessage);
            this.panelNewAccount.Controls.Add(this.lblConfirm);
            this.panelNewAccount.Controls.Add(this.lblPassword);
            this.panelNewAccount.Controls.Add(this.lblUsername);
            this.panelNewAccount.Location = new System.Drawing.Point(48, 36);
            this.panelNewAccount.Name = "panelNewAccount";
            this.panelNewAccount.Size = new System.Drawing.Size(384, 288);
            this.panelNewAccount.TabIndex = 1;
            this.panelNewAccount.Visible = false;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(30, 30);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(71, 15);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "&Username: ";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(30, 75);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 15);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "&Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.ForeColor = System.Drawing.Color.White;
            this.lblConfirm.Location = new System.Drawing.Point(30, 115);
            this.lblConfirm.MaximumSize = new System.Drawing.Size(75, 0);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(64, 30);
            this.lblConfirm.TabIndex = 5;
            this.lblConfirm.Text = "&Confirm Password:";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // tBoxUsername
            // 
            this.tBoxUsername.Location = new System.Drawing.Point(107, 27);
            this.tBoxUsername.MaxLength = 16;
            this.tBoxUsername.Name = "tBoxUsername";
            this.tBoxUsername.Size = new System.Drawing.Size(100, 25);
            this.tBoxUsername.TabIndex = 2;
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.Location = new System.Drawing.Point(107, 72);
            this.tBoxPassword.MaxLength = 32;
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.Size = new System.Drawing.Size(100, 25);
            this.tBoxPassword.TabIndex = 4;
            this.tBoxPassword.UseSystemPasswordChar = true;
            // 
            // tBoxConfirm
            // 
            this.tBoxConfirm.Location = new System.Drawing.Point(107, 117);
            this.tBoxConfirm.MaxLength = 32;
            this.tBoxConfirm.Name = "tBoxConfirm";
            this.tBoxConfirm.Size = new System.Drawing.Size(100, 25);
            this.tBoxConfirm.TabIndex = 6;
            this.tBoxConfirm.UseSystemPasswordChar = true;
            // 
            // btnNewAccount
            // 
            this.btnNewAccount.Location = new System.Drawing.Point(265, 26);
            this.btnNewAccount.Name = "btnNewAccount";
            this.btnNewAccount.Size = new System.Drawing.Size(75, 28);
            this.btnNewAccount.TabIndex = 7;
            this.btnNewAccount.Text = "&New";
            this.btnNewAccount.UseVisualStyleBackColor = true;
            this.btnNewAccount.Click += new System.EventHandler(this.btnNewAccount_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(265, 238);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 28);
            this.btnSkip.TabIndex = 8;
            this.btnSkip.Text = "&Skip";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // cBoxDontNotify
            // 
            this.cBoxDontNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxDontNotify.AutoSize = true;
            this.cBoxDontNotify.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.cBoxDontNotify.Location = new System.Drawing.Point(100, 244);
            this.cBoxDontNotify.Name = "cBoxDontNotify";
            this.cBoxDontNotify.Size = new System.Drawing.Size(159, 19);
            this.cBoxDontNotify.TabIndex = 9;
            this.cBoxDontNotify.Text = "&Don\'t notify next time.";
            this.cBoxDontNotify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cBoxDontNotify.UseVisualStyleBackColor = true;
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.lblLoginMessage);
            this.panelLogin.Controls.Add(this.btnExit);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.tBoxLoginPassword);
            this.panelLogin.Controls.Add(this.tBoxLoginUser);
            this.panelLogin.Controls.Add(this.lblLoginPassword);
            this.panelLogin.Controls.Add(this.lblLoginUser);
            this.panelLogin.Location = new System.Drawing.Point(48, 36);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(384, 288);
            this.panelLogin.TabIndex = 10;
            this.panelLogin.Visible = false;
            this.panelLogin.VisibleChanged += new System.EventHandler(this.panelLogin_VisibleChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(265, 238);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(265, 26);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 28);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tBoxLoginPassword
            // 
            this.tBoxLoginPassword.Location = new System.Drawing.Point(107, 72);
            this.tBoxLoginPassword.MaxLength = 32;
            this.tBoxLoginPassword.Name = "tBoxLoginPassword";
            this.tBoxLoginPassword.Size = new System.Drawing.Size(100, 25);
            this.tBoxLoginPassword.TabIndex = 4;
            this.tBoxLoginPassword.UseSystemPasswordChar = true;
            // 
            // tBoxLoginUser
            // 
            this.tBoxLoginUser.Location = new System.Drawing.Point(107, 27);
            this.tBoxLoginUser.MaxLength = 16;
            this.tBoxLoginUser.Name = "tBoxLoginUser";
            this.tBoxLoginUser.Size = new System.Drawing.Size(100, 25);
            this.tBoxLoginUser.TabIndex = 2;
            // 
            // lblLoginPassword
            // 
            this.lblLoginPassword.AutoSize = true;
            this.lblLoginPassword.ForeColor = System.Drawing.Color.White;
            this.lblLoginPassword.Location = new System.Drawing.Point(30, 75);
            this.lblLoginPassword.Name = "lblLoginPassword";
            this.lblLoginPassword.Size = new System.Drawing.Size(64, 15);
            this.lblLoginPassword.TabIndex = 3;
            this.lblLoginPassword.Text = "&Password:";
            this.lblLoginPassword.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblLoginUser
            // 
            this.lblLoginUser.AutoSize = true;
            this.lblLoginUser.ForeColor = System.Drawing.Color.White;
            this.lblLoginUser.Location = new System.Drawing.Point(30, 30);
            this.lblLoginUser.Name = "lblLoginUser";
            this.lblLoginUser.Size = new System.Drawing.Size(71, 15);
            this.lblLoginUser.TabIndex = 1;
            this.lblLoginUser.Text = "&Username: ";
            this.lblLoginUser.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblLoginMessage
            // 
            this.lblLoginMessage.AutoSize = true;
            this.lblLoginMessage.ForeColor = System.Drawing.Color.White;
            this.lblLoginMessage.Location = new System.Drawing.Point(226, 75);
            this.lblLoginMessage.MaximumSize = new System.Drawing.Size(150, 60);
            this.lblLoginMessage.Name = "lblLoginMessage";
            this.lblLoginMessage.Size = new System.Drawing.Size(0, 15);
            this.lblLoginMessage.TabIndex = 9;
            this.lblLoginMessage.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblNewAccountMessage
            // 
            this.lblNewAccountMessage.AutoSize = true;
            this.lblNewAccountMessage.ForeColor = System.Drawing.Color.White;
            this.lblNewAccountMessage.Location = new System.Drawing.Point(226, 75);
            this.lblNewAccountMessage.MaximumSize = new System.Drawing.Size(150, 60);
            this.lblNewAccountMessage.Name = "lblNewAccountMessage";
            this.lblNewAccountMessage.Size = new System.Drawing.Size(0, 15);
            this.lblNewAccountMessage.TabIndex = 10;
            this.lblNewAccountMessage.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(176)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.ControlBox = false;
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelNewAccount);
            this.Controls.Add(this.lblProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SplashForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SplashForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SplashForm_FormClosed);
            this.Load += new System.EventHandler(this.SplashForm_Load);
            this.panelNewAccount.ResumeLayout(false);
            this.panelNewAccount.PerformLayout();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Panel panelNewAccount;
        private System.Windows.Forms.TextBox tBoxConfirm;
        private System.Windows.Forms.TextBox tBoxPassword;
        private System.Windows.Forms.TextBox tBoxUsername;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnNewAccount;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.CheckBox cBoxDontNotify;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox tBoxLoginPassword;
        private System.Windows.Forms.TextBox tBoxLoginUser;
        private System.Windows.Forms.Label lblLoginPassword;
        private System.Windows.Forms.Label lblLoginUser;
        private System.Windows.Forms.Label lblLoginMessage;
        private System.Windows.Forms.Label lblNewAccountMessage;
    }
}