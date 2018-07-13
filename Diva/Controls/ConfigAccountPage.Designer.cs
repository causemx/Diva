namespace Diva.Controls
{
    partial class ConfigAccountPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LabelLoginAccount = new System.Windows.Forms.Label();
            this.LabelConfirmPassword = new System.Windows.Forms.Label();
            this.TBoxConfirmPassword = new System.Windows.Forms.TextBox();
            this.TBoxPassword = new System.Windows.Forms.TextBox();
            this.PanelExistingAccountControls = new System.Windows.Forms.Panel();
            this.LabelNewPassword = new System.Windows.Forms.Label();
            this.BtnChangePassword = new System.Windows.Forms.Button();
            this.BtnDeleteAccount = new System.Windows.Forms.Button();
            this.BtnCreateAccount = new System.Windows.Forms.Button();
            this.CBoxAccounts = new System.Windows.Forms.ComboBox();
            this.LabelPassword = new System.Windows.Forms.Label();
            this.PanelExistingAccountControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelLoginAccount
            // 
            this.LabelLoginAccount.AutoSize = true;
            this.LabelLoginAccount.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLoginAccount.Location = new System.Drawing.Point(60, 40);
            this.LabelLoginAccount.Name = "LabelLoginAccount";
            this.LabelLoginAccount.Size = new System.Drawing.Size(183, 20);
            this.LabelLoginAccount.TabIndex = 15;
            this.LabelLoginAccount.Text = "Current login account: ";
            // 
            // LabelConfirmPassword
            // 
            this.LabelConfirmPassword.AutoSize = true;
            this.LabelConfirmPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelConfirmPassword.Location = new System.Drawing.Point(600, 150);
            this.LabelConfirmPassword.Name = "LabelConfirmPassword";
            this.LabelConfirmPassword.Size = new System.Drawing.Size(153, 20);
            this.LabelConfirmPassword.TabIndex = 14;
            this.LabelConfirmPassword.Text = "Confirm Password:";
            // 
            // TBoxConfirmPassword
            // 
            this.TBoxConfirmPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxConfirmPassword.Location = new System.Drawing.Point(600, 180);
            this.TBoxConfirmPassword.Name = "TBoxConfirmPassword";
            this.TBoxConfirmPassword.Size = new System.Drawing.Size(140, 27);
            this.TBoxConfirmPassword.TabIndex = 13;
            this.TBoxConfirmPassword.UseSystemPasswordChar = true;
            this.TBoxConfirmPassword.TextChanged += new System.EventHandler(this.PasswordInputUpdate);
            // 
            // TBoxPassword
            // 
            this.TBoxPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxPassword.Location = new System.Drawing.Point(400, 180);
            this.TBoxPassword.Name = "TBoxPassword";
            this.TBoxPassword.Size = new System.Drawing.Size(140, 27);
            this.TBoxPassword.TabIndex = 12;
            this.TBoxPassword.UseSystemPasswordChar = true;
            this.TBoxPassword.TextChanged += new System.EventHandler(this.PasswordInputUpdate);
            // 
            // PanelExistingAccountControls
            // 
            this.PanelExistingAccountControls.Controls.Add(this.LabelNewPassword);
            this.PanelExistingAccountControls.Controls.Add(this.BtnChangePassword);
            this.PanelExistingAccountControls.Controls.Add(this.BtnDeleteAccount);
            this.PanelExistingAccountControls.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelExistingAccountControls.Location = new System.Drawing.Point(380, 70);
            this.PanelExistingAccountControls.Name = "PanelExistingAccountControls";
            this.PanelExistingAccountControls.Size = new System.Drawing.Size(400, 110);
            this.PanelExistingAccountControls.TabIndex = 10;
            this.PanelExistingAccountControls.Visible = false;
            // 
            // LabelNewPassword
            // 
            this.LabelNewPassword.AutoSize = true;
            this.LabelNewPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNewPassword.Location = new System.Drawing.Point(20, 80);
            this.LabelNewPassword.Name = "LabelNewPassword";
            this.LabelNewPassword.Size = new System.Drawing.Size(124, 20);
            this.LabelNewPassword.TabIndex = 4;
            this.LabelNewPassword.Text = "New Password:";
            // 
            // BtnChangePassword
            // 
            this.BtnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnChangePassword.Font = new System.Drawing.Font("Georgia", 10.2F);
            this.BtnChangePassword.Location = new System.Drawing.Point(160, 20);
            this.BtnChangePassword.Name = "BtnChangePassword";
            this.BtnChangePassword.Size = new System.Drawing.Size(200, 40);
            this.BtnChangePassword.TabIndex = 3;
            this.BtnChangePassword.Text = "Change Password";
            this.BtnChangePassword.UseVisualStyleBackColor = true;
            this.BtnChangePassword.Click += new System.EventHandler(this.BtnChangePassword_Click);
            // 
            // BtnDeleteAccount
            // 
            this.BtnDeleteAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteAccount.Font = new System.Drawing.Font("Georgia", 10.2F);
            this.BtnDeleteAccount.Location = new System.Drawing.Point(20, 20);
            this.BtnDeleteAccount.Name = "BtnDeleteAccount";
            this.BtnDeleteAccount.Size = new System.Drawing.Size(120, 40);
            this.BtnDeleteAccount.TabIndex = 2;
            this.BtnDeleteAccount.Text = "Delete";
            this.BtnDeleteAccount.UseVisualStyleBackColor = true;
            this.BtnDeleteAccount.Click += new System.EventHandler(this.BtnDeleteAccount_Click);
            // 
            // BtnCreateAccount
            // 
            this.BtnCreateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCreateAccount.Font = new System.Drawing.Font("Georgia", 10.2F);
            this.BtnCreateAccount.Location = new System.Drawing.Point(400, 90);
            this.BtnCreateAccount.Name = "BtnCreateAccount";
            this.BtnCreateAccount.Size = new System.Drawing.Size(120, 40);
            this.BtnCreateAccount.TabIndex = 9;
            this.BtnCreateAccount.Text = "Create";
            this.BtnCreateAccount.UseVisualStyleBackColor = true;
            this.BtnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // CBoxAccounts
            // 
            this.CBoxAccounts.BackColor = System.Drawing.SystemColors.Window;
            this.CBoxAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.CBoxAccounts.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoxAccounts.ForeColor = System.Drawing.SystemColors.WindowText;
            this.CBoxAccounts.FormattingEnabled = true;
            this.CBoxAccounts.Location = new System.Drawing.Point(60, 90);
            this.CBoxAccounts.Name = "CBoxAccounts";
            this.CBoxAccounts.Size = new System.Drawing.Size(240, 600);
            this.CBoxAccounts.TabIndex = 8;
            this.CBoxAccounts.SelectedIndexChanged += new System.EventHandler(this.CBoxAccounts_SelectedIndexChanged);
            this.CBoxAccounts.TextUpdate += new System.EventHandler(this.CBoxAccounts_TextUpdate);
            // 
            // LabelPassword
            // 
            this.LabelPassword.AutoSize = true;
            this.LabelPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPassword.Location = new System.Drawing.Point(400, 150);
            this.LabelPassword.Name = "LabelPassword";
            this.LabelPassword.Size = new System.Drawing.Size(81, 20);
            this.LabelPassword.TabIndex = 11;
            this.LabelPassword.Text = "Password";
            // 
            // ConfigAccountPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Controls.Add(this.LabelLoginAccount);
            this.Controls.Add(this.LabelConfirmPassword);
            this.Controls.Add(this.TBoxConfirmPassword);
            this.Controls.Add(this.TBoxPassword);
            this.Controls.Add(this.PanelExistingAccountControls);
            this.Controls.Add(this.BtnCreateAccount);
            this.Controls.Add(this.CBoxAccounts);
            this.Controls.Add(this.LabelPassword);
            this.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigAccountPage";
            this.Size = new System.Drawing.Size(960, 640);
            this.PanelExistingAccountControls.ResumeLayout(false);
            this.PanelExistingAccountControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelLoginAccount;
        private System.Windows.Forms.Label LabelConfirmPassword;
        private System.Windows.Forms.TextBox TBoxConfirmPassword;
        private System.Windows.Forms.TextBox TBoxPassword;
        private System.Windows.Forms.Panel PanelExistingAccountControls;
        private System.Windows.Forms.Label LabelNewPassword;
        private System.Windows.Forms.Button BtnChangePassword;
        private System.Windows.Forms.Button BtnDeleteAccount;
        private System.Windows.Forms.Button BtnCreateAccount;
        private System.Windows.Forms.ComboBox CBoxAccounts;
        private System.Windows.Forms.Label LabelPassword;
    }
}
