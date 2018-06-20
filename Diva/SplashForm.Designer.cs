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
            this.cBoxDontNotify = new System.Windows.Forms.CheckBox();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnNewAccount = new System.Windows.Forms.Button();
            this.tBoxConfirm = new System.Windows.Forms.TextBox();
            this.tBoxPassword = new System.Windows.Forms.TextBox();
            this.tBoxUsername = new System.Windows.Forms.TextBox();
            this.lblNewAccountMessage = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.lblLoginMessage = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.tBoxLoginUser = new System.Windows.Forms.TextBox();
            this.lblLoginPassword = new System.Windows.Forms.Label();
            this.lblLoginUser = new System.Windows.Forms.Label();
            this.BorderPanel = new System.Windows.Forms.Panel();
            this.panelNewAccount.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.BorderPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblProgress.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(148, 320);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(320, 35);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Loading...";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelNewAccount
            // 
            this.panelNewAccount.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
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
            this.panelNewAccount.Font = new System.Drawing.Font("Georgia", 8F);
            this.panelNewAccount.ForeColor = System.Drawing.Color.White;
            this.panelNewAccount.Location = new System.Drawing.Point(48, 36);
            this.panelNewAccount.Name = "panelNewAccount";
            this.panelNewAccount.Size = new System.Drawing.Size(384, 288);
            this.panelNewAccount.TabIndex = 1;
            this.panelNewAccount.Visible = false;
            // 
            // cBoxDontNotify
            // 
            this.cBoxDontNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cBoxDontNotify.AutoSize = true;
            this.cBoxDontNotify.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.cBoxDontNotify.Font = new System.Drawing.Font("Georgia", 8F);
            this.cBoxDontNotify.ForeColor = System.Drawing.Color.White;
            this.cBoxDontNotify.Location = new System.Drawing.Point(88, 242);
            this.cBoxDontNotify.Name = "cBoxDontNotify";
            this.cBoxDontNotify.Size = new System.Drawing.Size(171, 21);
            this.cBoxDontNotify.TabIndex = 9;
            this.cBoxDontNotify.Text = "&Don\'t notify next time.";
            this.cBoxDontNotify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cBoxDontNotify.UseVisualStyleBackColor = false;
            // 
            // btnSkip
            // 
            this.btnSkip.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSkip.Font = new System.Drawing.Font("Georgia", 8F);
            this.btnSkip.ForeColor = System.Drawing.Color.White;
            this.btnSkip.Location = new System.Drawing.Point(265, 233);
            this.btnSkip.Margin = new System.Windows.Forms.Padding(0);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 38);
            this.btnSkip.TabIndex = 8;
            this.btnSkip.Text = "&Skip";
            this.btnSkip.UseVisualStyleBackColor = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnNewAccount
            // 
            this.btnNewAccount.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnNewAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewAccount.Font = new System.Drawing.Font("Georgia", 8F);
            this.btnNewAccount.ForeColor = System.Drawing.Color.White;
            this.btnNewAccount.Location = new System.Drawing.Point(265, 21);
            this.btnNewAccount.Margin = new System.Windows.Forms.Padding(0);
            this.btnNewAccount.Name = "btnNewAccount";
            this.btnNewAccount.Size = new System.Drawing.Size(75, 38);
            this.btnNewAccount.TabIndex = 7;
            this.btnNewAccount.Text = "&New";
            this.btnNewAccount.UseVisualStyleBackColor = false;
            this.btnNewAccount.Click += new System.EventHandler(this.btnNewAccount_Click);
            // 
            // tBoxConfirm
            // 
            this.tBoxConfirm.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxConfirm.Font = new System.Drawing.Font("Georgia", 8F);
            this.tBoxConfirm.ForeColor = System.Drawing.Color.White;
            this.tBoxConfirm.Location = new System.Drawing.Point(107, 117);
            this.tBoxConfirm.MaxLength = 32;
            this.tBoxConfirm.Name = "tBoxConfirm";
            this.tBoxConfirm.Size = new System.Drawing.Size(100, 23);
            this.tBoxConfirm.TabIndex = 6;
            this.tBoxConfirm.UseSystemPasswordChar = true;
            // 
            // tBoxPassword
            // 
            this.tBoxPassword.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxPassword.Font = new System.Drawing.Font("Georgia", 8F);
            this.tBoxPassword.ForeColor = System.Drawing.Color.White;
            this.tBoxPassword.Location = new System.Drawing.Point(107, 72);
            this.tBoxPassword.MaxLength = 32;
            this.tBoxPassword.Name = "tBoxPassword";
            this.tBoxPassword.Size = new System.Drawing.Size(100, 23);
            this.tBoxPassword.TabIndex = 4;
            this.tBoxPassword.UseSystemPasswordChar = true;
            // 
            // tBoxUsername
            // 
            this.tBoxUsername.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxUsername.Font = new System.Drawing.Font("Georgia", 8F);
            this.tBoxUsername.ForeColor = System.Drawing.Color.White;
            this.tBoxUsername.Location = new System.Drawing.Point(107, 27);
            this.tBoxUsername.MaxLength = 16;
            this.tBoxUsername.Name = "tBoxUsername";
            this.tBoxUsername.Size = new System.Drawing.Size(100, 23);
            this.tBoxUsername.TabIndex = 2;
            // 
            // lblNewAccountMessage
            // 
            this.lblNewAccountMessage.AutoSize = true;
            this.lblNewAccountMessage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblNewAccountMessage.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblNewAccountMessage.ForeColor = System.Drawing.Color.White;
            this.lblNewAccountMessage.Location = new System.Drawing.Point(226, 75);
            this.lblNewAccountMessage.MaximumSize = new System.Drawing.Size(150, 60);
            this.lblNewAccountMessage.Name = "lblNewAccountMessage";
            this.lblNewAccountMessage.Size = new System.Drawing.Size(0, 17);
            this.lblNewAccountMessage.TabIndex = 10;
            this.lblNewAccountMessage.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblConfirm
            // 
            this.lblConfirm.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblConfirm.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblConfirm.ForeColor = System.Drawing.Color.White;
            this.lblConfirm.Location = new System.Drawing.Point(0, 110);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(108, 40);
            this.lblConfirm.TabIndex = 5;
            this.lblConfirm.Text = "&Confirm Password:";
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPassword
            // 
            this.lblPassword.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblPassword.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(0, 70);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(108, 25);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "&Password:";
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblUsername.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(0, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(108, 25);
            this.lblUsername.TabIndex = 1;
            this.lblUsername.Text = "&Username: ";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.panelLogin.Controls.Add(this.lblLoginMessage);
            this.panelLogin.Controls.Add(this.btnExit);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.tBoxLoginPassword);
            this.panelLogin.Controls.Add(this.tBoxLoginUser);
            this.panelLogin.Controls.Add(this.lblLoginPassword);
            this.panelLogin.Controls.Add(this.lblLoginUser);
            this.panelLogin.Font = new System.Drawing.Font("Georgia", 8F);
            this.panelLogin.ForeColor = System.Drawing.Color.White;
            this.panelLogin.Location = new System.Drawing.Point(48, 36);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(384, 288);
            this.panelLogin.TabIndex = 10;
            this.panelLogin.Visible = false;
            this.panelLogin.VisibleChanged += new System.EventHandler(this.panelLogin_VisibleChanged);
            // 
            // lblLoginMessage
            // 
            this.lblLoginMessage.AutoSize = true;
            this.lblLoginMessage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblLoginMessage.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblLoginMessage.ForeColor = System.Drawing.Color.White;
            this.lblLoginMessage.Location = new System.Drawing.Point(226, 70);
            this.lblLoginMessage.MaximumSize = new System.Drawing.Size(150, 60);
            this.lblLoginMessage.Name = "lblLoginMessage";
            this.lblLoginMessage.Size = new System.Drawing.Size(0, 17);
            this.lblLoginMessage.TabIndex = 9;
            this.lblLoginMessage.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Georgia", 8F);
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(265, 233);
            this.btnExit.Margin = new System.Windows.Forms.Padding(0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 38);
            this.btnExit.TabIndex = 8;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Georgia", 8F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(265, 21);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(0);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 38);
            this.btnLogin.TabIndex = 7;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tBoxLoginPassword
            // 
            this.tBoxLoginPassword.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxLoginPassword.Font = new System.Drawing.Font("Georgia", 8F);
            this.tBoxLoginPassword.ForeColor = System.Drawing.Color.White;
            this.tBoxLoginPassword.Location = new System.Drawing.Point(107, 72);
            this.tBoxLoginPassword.MaxLength = 32;
            this.tBoxLoginPassword.Name = "tBoxLoginPassword";
            this.tBoxLoginPassword.Size = new System.Drawing.Size(100, 23);
            this.tBoxLoginPassword.TabIndex = 4;
            this.tBoxLoginPassword.UseSystemPasswordChar = true;
            // 
            // tBoxLoginUser
            // 
            this.tBoxLoginUser.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.tBoxLoginUser.Font = new System.Drawing.Font("Georgia", 8F);
            this.tBoxLoginUser.ForeColor = System.Drawing.Color.White;
            this.tBoxLoginUser.Location = new System.Drawing.Point(107, 27);
            this.tBoxLoginUser.MaxLength = 16;
            this.tBoxLoginUser.Name = "tBoxLoginUser";
            this.tBoxLoginUser.Size = new System.Drawing.Size(100, 23);
            this.tBoxLoginUser.TabIndex = 2;
            // 
            // lblLoginPassword
            // 
            this.lblLoginPassword.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblLoginPassword.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblLoginPassword.ForeColor = System.Drawing.Color.White;
            this.lblLoginPassword.Location = new System.Drawing.Point(0, 70);
            this.lblLoginPassword.Name = "lblLoginPassword";
            this.lblLoginPassword.Size = new System.Drawing.Size(108, 25);
            this.lblLoginPassword.TabIndex = 3;
            this.lblLoginPassword.Text = "&Password:";
            this.lblLoginPassword.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblLoginUser
            // 
            this.lblLoginUser.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblLoginUser.Font = new System.Drawing.Font("Georgia", 8F);
            this.lblLoginUser.ForeColor = System.Drawing.Color.White;
            this.lblLoginUser.Location = new System.Drawing.Point(0, 25);
            this.lblLoginUser.Name = "lblLoginUser";
            this.lblLoginUser.Size = new System.Drawing.Size(108, 25);
            this.lblLoginUser.TabIndex = 1;
            this.lblLoginUser.Text = "&Username: ";
            this.lblLoginUser.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // BorderPanel
            // 
            this.BorderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BorderPanel.Controls.Add(this.panelLogin);
            this.BorderPanel.Controls.Add(this.panelNewAccount);
            this.BorderPanel.Controls.Add(this.lblProgress);
            this.BorderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BorderPanel.Font = new System.Drawing.Font("Georgia", 8F);
            this.BorderPanel.Location = new System.Drawing.Point(0, 0);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Size = new System.Drawing.Size(480, 360);
            this.BorderPanel.TabIndex = 11;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(480, 360);
            this.ControlBox = false;
            this.Controls.Add(this.BorderPanel);
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
            this.BorderPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel BorderPanel;
    }
}