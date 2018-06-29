namespace Diva.Controls
{
	partial class Configure
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
            this.SidePanel = new System.Windows.Forms.Panel();
            this.BtnMap = new System.Windows.Forms.Button();
            this.BtnAccount = new System.Windows.Forms.Button();
            this.IndicatorPanel = new System.Windows.Forms.Panel();
            this.BtnVehicle = new System.Windows.Forms.Button();
            this.BtnAbout = new System.Windows.Forms.Button();
            this.BtnTuning = new System.Windows.Forms.Button();
            this.BtnGeoFence = new System.Windows.Forms.Button();
            this.PanelAccountConfig = new System.Windows.Forms.Panel();
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
            this.PanelMapControls = new System.Windows.Forms.Panel();
            this.BtnMapConfigReset = new System.Windows.Forms.Button();
            this.BtnMapConfigApply = new System.Windows.Forms.Button();
            this.PanelIndoorMapControls = new System.Windows.Forms.Panel();
            this.LabelImageMapLocation = new System.Windows.Forms.Label();
            this.TBoxIndoorMapLocation = new System.Windows.Forms.TextBox();
            this.BtnBrowseIndoorMap = new System.Windows.Forms.Button();
            this.RBtnIndoorMap = new System.Windows.Forms.RadioButton();
            this.PanelGlobalMapControls = new System.Windows.Forms.Panel();
            this.LabelInitialPosition = new System.Windows.Forms.Label();
            this.TBoxInitialZoom = new System.Windows.Forms.TextBox();
            this.LabelInitialZoom = new System.Windows.Forms.Label();
            this.LabelIPLongtitude = new System.Windows.Forms.Label();
            this.TBoxIPLongitude = new System.Windows.Forms.TextBox();
            this.LabelIPLatitude = new System.Windows.Forms.Label();
            this.TBoxIPLatitude = new System.Windows.Forms.TextBox();
            this.LabelMapCacheLocation = new System.Windows.Forms.Label();
            this.TBoxMapCacheLocation = new System.Windows.Forms.TextBox();
            this.BtnBrowseMapLocation = new System.Windows.Forms.Button();
            this.RBtnGlobalMap = new System.Windows.Forms.RadioButton();
            this.SidePanel.SuspendLayout();
            this.PanelAccountConfig.SuspendLayout();
            this.PanelExistingAccountControls.SuspendLayout();
            this.PanelMapControls.SuspendLayout();
            this.PanelIndoorMapControls.SuspendLayout();
            this.PanelGlobalMapControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.SidePanel.Controls.Add(this.BtnMap);
            this.SidePanel.Controls.Add(this.BtnAccount);
            this.SidePanel.Controls.Add(this.IndicatorPanel);
            this.SidePanel.Controls.Add(this.BtnVehicle);
            this.SidePanel.Controls.Add(this.BtnAbout);
            this.SidePanel.Controls.Add(this.BtnTuning);
            this.SidePanel.Controls.Add(this.BtnGeoFence);
            this.SidePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SidePanel.Location = new System.Drawing.Point(0, 0);
            this.SidePanel.Margin = new System.Windows.Forms.Padding(4);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(333, 754);
            this.SidePanel.TabIndex = 0;
            // 
            // BtnMap
            // 
            this.BtnMap.FlatAppearance.BorderSize = 0;
            this.BtnMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMap.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMap.ForeColor = System.Drawing.Color.White;
            this.BtnMap.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnMap.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnMap.Location = new System.Drawing.Point(16, 338);
            this.BtnMap.Margin = new System.Windows.Forms.Padding(4);
            this.BtnMap.Name = "BtnMap";
            this.BtnMap.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnMap.Size = new System.Drawing.Size(317, 100);
            this.BtnMap.TabIndex = 7;
            this.BtnMap.Text = "Map";
            this.BtnMap.UseVisualStyleBackColor = true;
            this.BtnMap.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAccount
            // 
            this.BtnAccount.FlatAppearance.BorderSize = 0;
            this.BtnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAccount.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAccount.ForeColor = System.Drawing.Color.White;
            this.BtnAccount.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAccount.Location = new System.Drawing.Point(17, 438);
            this.BtnAccount.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAccount.Name = "BtnAccount";
            this.BtnAccount.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnAccount.Size = new System.Drawing.Size(317, 100);
            this.BtnAccount.TabIndex = 6;
            this.BtnAccount.Text = "Account";
            this.BtnAccount.UseVisualStyleBackColor = true;
            this.BtnAccount.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // IndicatorPanel
            // 
            this.IndicatorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            this.IndicatorPanel.Location = new System.Drawing.Point(0, 38);
            this.IndicatorPanel.Margin = new System.Windows.Forms.Padding(4);
            this.IndicatorPanel.Name = "IndicatorPanel";
            this.IndicatorPanel.Size = new System.Drawing.Size(16, 100);
            this.IndicatorPanel.TabIndex = 5;
            // 
            // BtnVehicle
            // 
            this.BtnVehicle.FlatAppearance.BorderSize = 0;
            this.BtnVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVehicle.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVehicle.ForeColor = System.Drawing.Color.White;
            this.BtnVehicle.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnVehicle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVehicle.Location = new System.Drawing.Point(16, 38);
            this.BtnVehicle.Margin = new System.Windows.Forms.Padding(4);
            this.BtnVehicle.Name = "BtnVehicle";
            this.BtnVehicle.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnVehicle.Size = new System.Drawing.Size(317, 100);
            this.BtnVehicle.TabIndex = 4;
            this.BtnVehicle.Text = "Vehicle";
            this.BtnVehicle.UseVisualStyleBackColor = true;
            this.BtnVehicle.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAbout
            // 
            this.BtnAbout.FlatAppearance.BorderSize = 0;
            this.BtnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAbout.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAbout.ForeColor = System.Drawing.Color.White;
            this.BtnAbout.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnAbout.Location = new System.Drawing.Point(16, 538);
            this.BtnAbout.Margin = new System.Windows.Forms.Padding(4);
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnAbout.Size = new System.Drawing.Size(317, 100);
            this.BtnAbout.TabIndex = 3;
            this.BtnAbout.Text = "About";
            this.BtnAbout.UseVisualStyleBackColor = true;
            this.BtnAbout.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnTuning
            // 
            this.BtnTuning.FlatAppearance.BorderSize = 0;
            this.BtnTuning.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnTuning.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnTuning.ForeColor = System.Drawing.Color.White;
            this.BtnTuning.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnTuning.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnTuning.Location = new System.Drawing.Point(16, 238);
            this.BtnTuning.Margin = new System.Windows.Forms.Padding(4);
            this.BtnTuning.Name = "BtnTuning";
            this.BtnTuning.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnTuning.Size = new System.Drawing.Size(317, 100);
            this.BtnTuning.TabIndex = 1;
            this.BtnTuning.Text = "Tuning";
            this.BtnTuning.UseVisualStyleBackColor = true;
            this.BtnTuning.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnGeoFence
            // 
            this.BtnGeoFence.FlatAppearance.BorderSize = 0;
            this.BtnGeoFence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGeoFence.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGeoFence.ForeColor = System.Drawing.Color.White;
            this.BtnGeoFence.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnGeoFence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGeoFence.Location = new System.Drawing.Point(16, 138);
            this.BtnGeoFence.Margin = new System.Windows.Forms.Padding(4);
            this.BtnGeoFence.Name = "BtnGeoFence";
            this.BtnGeoFence.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.BtnGeoFence.Size = new System.Drawing.Size(317, 100);
            this.BtnGeoFence.TabIndex = 0;
            this.BtnGeoFence.Text = "Geofence";
            this.BtnGeoFence.UseVisualStyleBackColor = true;
            this.BtnGeoFence.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // PanelAccountConfig
            // 
            this.PanelAccountConfig.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.PanelAccountConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelAccountConfig.Controls.Add(this.LabelLoginAccount);
            this.PanelAccountConfig.Controls.Add(this.LabelConfirmPassword);
            this.PanelAccountConfig.Controls.Add(this.TBoxConfirmPassword);
            this.PanelAccountConfig.Controls.Add(this.TBoxPassword);
            this.PanelAccountConfig.Controls.Add(this.PanelExistingAccountControls);
            this.PanelAccountConfig.Controls.Add(this.BtnCreateAccount);
            this.PanelAccountConfig.Controls.Add(this.CBoxAccounts);
            this.PanelAccountConfig.Controls.Add(this.LabelPassword);
            this.PanelAccountConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelAccountConfig.ForeColor = System.Drawing.Color.White;
            this.PanelAccountConfig.Location = new System.Drawing.Point(333, 0);
            this.PanelAccountConfig.Name = "PanelAccountConfig";
            this.PanelAccountConfig.Size = new System.Drawing.Size(980, 754);
            this.PanelAccountConfig.TabIndex = 1;
            this.PanelAccountConfig.Visible = false;
            // 
            // LabelLoginAccount
            // 
            this.LabelLoginAccount.AutoSize = true;
            this.LabelLoginAccount.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLoginAccount.Location = new System.Drawing.Point(60, 40);
            this.LabelLoginAccount.Name = "LabelLoginAccount";
            this.LabelLoginAccount.Size = new System.Drawing.Size(206, 23);
            this.LabelLoginAccount.TabIndex = 7;
            this.LabelLoginAccount.Text = "Current login account: ";
            // 
            // LabelConfirmPassword
            // 
            this.LabelConfirmPassword.AutoSize = true;
            this.LabelConfirmPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelConfirmPassword.Location = new System.Drawing.Point(600, 150);
            this.LabelConfirmPassword.Name = "LabelConfirmPassword";
            this.LabelConfirmPassword.Size = new System.Drawing.Size(153, 20);
            this.LabelConfirmPassword.TabIndex = 6;
            this.LabelConfirmPassword.Text = "Confirm Password:";
            // 
            // TBoxConfirmPassword
            // 
            this.TBoxConfirmPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxConfirmPassword.Location = new System.Drawing.Point(600, 180);
            this.TBoxConfirmPassword.Name = "TBoxConfirmPassword";
            this.TBoxConfirmPassword.Size = new System.Drawing.Size(140, 27);
            this.TBoxConfirmPassword.TabIndex = 5;
            this.TBoxConfirmPassword.UseSystemPasswordChar = true;
            this.TBoxConfirmPassword.TextChanged += new System.EventHandler(this.PasswordInputUpdate);
            // 
            // TBoxPassword
            // 
            this.TBoxPassword.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxPassword.Location = new System.Drawing.Point(400, 180);
            this.TBoxPassword.Name = "TBoxPassword";
            this.TBoxPassword.Size = new System.Drawing.Size(140, 27);
            this.TBoxPassword.TabIndex = 4;
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
            this.PanelExistingAccountControls.TabIndex = 2;
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
            this.BtnChangePassword.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.BtnDeleteAccount.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.BtnCreateAccount.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCreateAccount.Location = new System.Drawing.Point(400, 90);
            this.BtnCreateAccount.Name = "BtnCreateAccount";
            this.BtnCreateAccount.Size = new System.Drawing.Size(120, 40);
            this.BtnCreateAccount.TabIndex = 1;
            this.BtnCreateAccount.Text = "Create";
            this.BtnCreateAccount.UseVisualStyleBackColor = true;
            this.BtnCreateAccount.Click += new System.EventHandler(this.BtnCreateAccount_Click);
            // 
            // CBoxAccounts
            // 
            this.CBoxAccounts.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.CBoxAccounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.CBoxAccounts.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBoxAccounts.ForeColor = System.Drawing.Color.White;
            this.CBoxAccounts.FormattingEnabled = true;
            this.CBoxAccounts.Location = new System.Drawing.Point(60, 90);
            this.CBoxAccounts.Name = "CBoxAccounts";
            this.CBoxAccounts.Size = new System.Drawing.Size(240, 600);
            this.CBoxAccounts.TabIndex = 0;
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
            this.LabelPassword.TabIndex = 3;
            this.LabelPassword.Text = "Password";
            // 
            // PanelMapControls
            // 
            this.PanelMapControls.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.PanelMapControls.Controls.Add(this.BtnMapConfigReset);
            this.PanelMapControls.Controls.Add(this.BtnMapConfigApply);
            this.PanelMapControls.Controls.Add(this.PanelIndoorMapControls);
            this.PanelMapControls.Controls.Add(this.RBtnIndoorMap);
            this.PanelMapControls.Controls.Add(this.PanelGlobalMapControls);
            this.PanelMapControls.Controls.Add(this.RBtnGlobalMap);
            this.PanelMapControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMapControls.ForeColor = System.Drawing.Color.White;
            this.PanelMapControls.Location = new System.Drawing.Point(333, 0);
            this.PanelMapControls.Name = "PanelMapControls";
            this.PanelMapControls.Size = new System.Drawing.Size(980, 754);
            this.PanelMapControls.TabIndex = 8;
            // 
            // BtnMapConfigReset
            // 
            this.BtnMapConfigReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMapConfigReset.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMapConfigReset.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigReset.Location = new System.Drawing.Point(690, 420);
            this.BtnMapConfigReset.Name = "BtnMapConfigReset";
            this.BtnMapConfigReset.Size = new System.Drawing.Size(120, 40);
            this.BtnMapConfigReset.TabIndex = 16;
            this.BtnMapConfigReset.Text = "Reset";
            this.BtnMapConfigReset.UseVisualStyleBackColor = true;
            this.BtnMapConfigReset.Click += new System.EventHandler(this.BtnMapConfigReset_Click);
            // 
            // BtnMapConfigApply
            // 
            this.BtnMapConfigApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnMapConfigApply.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMapConfigApply.ForeColor = System.Drawing.Color.White;
            this.BtnMapConfigApply.Location = new System.Drawing.Point(540, 420);
            this.BtnMapConfigApply.Name = "BtnMapConfigApply";
            this.BtnMapConfigApply.Size = new System.Drawing.Size(120, 40);
            this.BtnMapConfigApply.TabIndex = 15;
            this.BtnMapConfigApply.Text = "Apply";
            this.BtnMapConfigApply.UseVisualStyleBackColor = true;
            this.BtnMapConfigApply.Click += new System.EventHandler(this.BtnMapConfigApply_Click);
            // 
            // PanelIndoorMapControls
            // 
            this.PanelIndoorMapControls.Controls.Add(this.LabelImageMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.TBoxIndoorMapLocation);
            this.PanelIndoorMapControls.Controls.Add(this.BtnBrowseIndoorMap);
            this.PanelIndoorMapControls.Location = new System.Drawing.Point(60, 320);
            this.PanelIndoorMapControls.Name = "PanelIndoorMapControls";
            this.PanelIndoorMapControls.Size = new System.Drawing.Size(800, 60);
            this.PanelIndoorMapControls.TabIndex = 14;
            // 
            // LabelImageMapLocation
            // 
            this.LabelImageMapLocation.AutoSize = true;
            this.LabelImageMapLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelImageMapLocation.ForeColor = System.Drawing.Color.White;
            this.LabelImageMapLocation.Location = new System.Drawing.Point(0, 0);
            this.LabelImageMapLocation.Name = "LabelImageMapLocation";
            this.LabelImageMapLocation.Size = new System.Drawing.Size(204, 23);
            this.LabelImageMapLocation.TabIndex = 8;
            this.LabelImageMapLocation.Text = "Indoor map image file:";
            // 
            // TBoxIndoorMapLocation
            // 
            this.TBoxIndoorMapLocation.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIndoorMapLocation.Location = new System.Drawing.Point(0, 30);
            this.TBoxIndoorMapLocation.Name = "TBoxIndoorMapLocation";
            this.TBoxIndoorMapLocation.Size = new System.Drawing.Size(640, 27);
            this.TBoxIndoorMapLocation.TabIndex = 9;
            this.TBoxIndoorMapLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseIndoorMap
            // 
            this.BtnBrowseIndoorMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowseIndoorMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowseIndoorMap.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseIndoorMap.Location = new System.Drawing.Point(660, 20);
            this.BtnBrowseIndoorMap.Name = "BtnBrowseIndoorMap";
            this.BtnBrowseIndoorMap.Size = new System.Drawing.Size(120, 40);
            this.BtnBrowseIndoorMap.TabIndex = 10;
            this.BtnBrowseIndoorMap.Text = "Browse";
            this.BtnBrowseIndoorMap.UseVisualStyleBackColor = true;
            this.BtnBrowseIndoorMap.Click += new System.EventHandler(this.BtnBrowseIndoorMap_Click);
            // 
            // RBtnIndoorMap
            // 
            this.RBtnIndoorMap.AutoSize = true;
            this.RBtnIndoorMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBtnIndoorMap.ForeColor = System.Drawing.Color.White;
            this.RBtnIndoorMap.Location = new System.Drawing.Point(40, 280);
            this.RBtnIndoorMap.Name = "RBtnIndoorMap";
            this.RBtnIndoorMap.Size = new System.Drawing.Size(132, 27);
            this.RBtnIndoorMap.TabIndex = 13;
            this.RBtnIndoorMap.Text = "Indoor Map";
            this.RBtnIndoorMap.UseVisualStyleBackColor = true;
            this.RBtnIndoorMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // PanelGlobalMapControls
            // 
            this.PanelGlobalMapControls.Controls.Add(this.LabelInitialPosition);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxInitialZoom);
            this.PanelGlobalMapControls.Controls.Add(this.LabelInitialZoom);
            this.PanelGlobalMapControls.Controls.Add(this.LabelIPLongtitude);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxIPLongitude);
            this.PanelGlobalMapControls.Controls.Add(this.LabelIPLatitude);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxIPLatitude);
            this.PanelGlobalMapControls.Controls.Add(this.LabelMapCacheLocation);
            this.PanelGlobalMapControls.Controls.Add(this.TBoxMapCacheLocation);
            this.PanelGlobalMapControls.Controls.Add(this.BtnBrowseMapLocation);
            this.PanelGlobalMapControls.ForeColor = System.Drawing.Color.Transparent;
            this.PanelGlobalMapControls.Location = new System.Drawing.Point(60, 80);
            this.PanelGlobalMapControls.Name = "PanelGlobalMapControls";
            this.PanelGlobalMapControls.Size = new System.Drawing.Size(800, 160);
            this.PanelGlobalMapControls.TabIndex = 12;
            // 
            // LabelInitialPosition
            // 
            this.LabelInitialPosition.AutoSize = true;
            this.LabelInitialPosition.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInitialPosition.ForeColor = System.Drawing.Color.White;
            this.LabelInitialPosition.Location = new System.Drawing.Point(0, 90);
            this.LabelInitialPosition.Name = "LabelInitialPosition";
            this.LabelInitialPosition.Size = new System.Drawing.Size(179, 23);
            this.LabelInitialPosition.TabIndex = 11;
            this.LabelInitialPosition.Text = "Initial map position";
            // 
            // TBoxInitialZoom
            // 
            this.TBoxInitialZoom.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxInitialZoom.Location = new System.Drawing.Point(320, 130);
            this.TBoxInitialZoom.Name = "TBoxInitialZoom";
            this.TBoxInitialZoom.Size = new System.Drawing.Size(80, 27);
            this.TBoxInitialZoom.TabIndex = 17;
            this.TBoxInitialZoom.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelInitialZoom
            // 
            this.LabelInitialZoom.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelInitialZoom.ForeColor = System.Drawing.Color.White;
            this.LabelInitialZoom.Location = new System.Drawing.Point(100, 130);
            this.LabelInitialZoom.Name = "LabelInitialZoom";
            this.LabelInitialZoom.Size = new System.Drawing.Size(200, 23);
            this.LabelInitialZoom.TabIndex = 16;
            this.LabelInitialZoom.Text = "Initial zoom level:";
            this.LabelInitialZoom.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelIPLongtitude
            // 
            this.LabelIPLongtitude.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPLongtitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLongtitude.Location = new System.Drawing.Point(470, 90);
            this.LabelIPLongtitude.Name = "LabelIPLongtitude";
            this.LabelIPLongtitude.Size = new System.Drawing.Size(150, 23);
            this.LabelIPLongtitude.TabIndex = 15;
            this.LabelIPLongtitude.Text = "Longitude:";
            this.LabelIPLongtitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBoxIPLongitude
            // 
            this.TBoxIPLongitude.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIPLongitude.Location = new System.Drawing.Point(630, 90);
            this.TBoxIPLongitude.Name = "TBoxIPLongitude";
            this.TBoxIPLongitude.Size = new System.Drawing.Size(150, 27);
            this.TBoxIPLongitude.TabIndex = 14;
            this.TBoxIPLongitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelIPLatitude
            // 
            this.LabelIPLatitude.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIPLatitude.ForeColor = System.Drawing.Color.White;
            this.LabelIPLatitude.Location = new System.Drawing.Point(160, 90);
            this.LabelIPLatitude.Name = "LabelIPLatitude";
            this.LabelIPLatitude.Size = new System.Drawing.Size(150, 23);
            this.LabelIPLatitude.TabIndex = 13;
            this.LabelIPLatitude.Text = "Latitude:";
            this.LabelIPLatitude.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBoxIPLatitude
            // 
            this.TBoxIPLatitude.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxIPLatitude.Location = new System.Drawing.Point(320, 90);
            this.TBoxIPLatitude.Name = "TBoxIPLatitude";
            this.TBoxIPLatitude.Size = new System.Drawing.Size(150, 27);
            this.TBoxIPLatitude.TabIndex = 12;
            this.TBoxIPLatitude.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // LabelMapCacheLocation
            // 
            this.LabelMapCacheLocation.AutoSize = true;
            this.LabelMapCacheLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMapCacheLocation.ForeColor = System.Drawing.Color.White;
            this.LabelMapCacheLocation.Location = new System.Drawing.Point(0, 0);
            this.LabelMapCacheLocation.Name = "LabelMapCacheLocation";
            this.LabelMapCacheLocation.Size = new System.Drawing.Size(628, 23);
            this.LabelMapCacheLocation.TabIndex = 8;
            this.LabelMapCacheLocation.Text = "Offline map cache file: (will be created under \'TileDBv5\\en\' subdirectory)";
            // 
            // TBoxMapCacheLocation
            // 
            this.TBoxMapCacheLocation.Font = new System.Drawing.Font("Georgia", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBoxMapCacheLocation.Location = new System.Drawing.Point(0, 40);
            this.TBoxMapCacheLocation.Name = "TBoxMapCacheLocation";
            this.TBoxMapCacheLocation.Size = new System.Drawing.Size(640, 27);
            this.TBoxMapCacheLocation.TabIndex = 9;
            this.TBoxMapCacheLocation.TextChanged += new System.EventHandler(this.MapConfigChanged);
            // 
            // BtnBrowseMapLocation
            // 
            this.BtnBrowseMapLocation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowseMapLocation.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrowseMapLocation.ForeColor = System.Drawing.Color.White;
            this.BtnBrowseMapLocation.Location = new System.Drawing.Point(660, 30);
            this.BtnBrowseMapLocation.Name = "BtnBrowseMapLocation";
            this.BtnBrowseMapLocation.Size = new System.Drawing.Size(120, 40);
            this.BtnBrowseMapLocation.TabIndex = 10;
            this.BtnBrowseMapLocation.Text = "Browse";
            this.BtnBrowseMapLocation.UseVisualStyleBackColor = true;
            this.BtnBrowseMapLocation.Click += new System.EventHandler(this.BtnBrowseMapLocation_Click);
            // 
            // RBtnGlobalMap
            // 
            this.RBtnGlobalMap.AutoSize = true;
            this.RBtnGlobalMap.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RBtnGlobalMap.ForeColor = System.Drawing.Color.White;
            this.RBtnGlobalMap.Location = new System.Drawing.Point(40, 40);
            this.RBtnGlobalMap.Name = "RBtnGlobalMap";
            this.RBtnGlobalMap.Size = new System.Drawing.Size(130, 27);
            this.RBtnGlobalMap.TabIndex = 11;
            this.RBtnGlobalMap.Text = "Global Map";
            this.RBtnGlobalMap.UseVisualStyleBackColor = true;
            this.RBtnGlobalMap.CheckedChanged += new System.EventHandler(this.MapControl_RadioCheckedChanged);
            // 
            // Configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1313, 754);
            this.Controls.Add(this.PanelMapControls);
            this.Controls.Add(this.PanelAccountConfig);
            this.Controls.Add(this.SidePanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Configure";
            this.Text = "Configure";
            this.SidePanel.ResumeLayout(false);
            this.PanelAccountConfig.ResumeLayout(false);
            this.PanelAccountConfig.PerformLayout();
            this.PanelExistingAccountControls.ResumeLayout(false);
            this.PanelExistingAccountControls.PerformLayout();
            this.PanelMapControls.ResumeLayout(false);
            this.PanelMapControls.PerformLayout();
            this.PanelIndoorMapControls.ResumeLayout(false);
            this.PanelIndoorMapControls.PerformLayout();
            this.PanelGlobalMapControls.ResumeLayout(false);
            this.PanelGlobalMapControls.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel SidePanel;
		private System.Windows.Forms.Button BtnTuning;
		private System.Windows.Forms.Button BtnGeoFence;
		private System.Windows.Forms.Button BtnAbout;
		private System.Windows.Forms.Button BtnVehicle;
		private System.Windows.Forms.Panel IndicatorPanel;
        private System.Windows.Forms.Button BtnAccount;
        private System.Windows.Forms.Panel PanelAccountConfig;
        private System.Windows.Forms.ComboBox CBoxAccounts;
        private System.Windows.Forms.Button BtnCreateAccount;
        private System.Windows.Forms.Panel PanelExistingAccountControls;
        private System.Windows.Forms.Button BtnDeleteAccount;
        private System.Windows.Forms.Button BtnChangePassword;
        private System.Windows.Forms.Label LabelPassword;
        private System.Windows.Forms.Label LabelNewPassword;
        private System.Windows.Forms.TextBox TBoxPassword;
        private System.Windows.Forms.TextBox TBoxConfirmPassword;
        private System.Windows.Forms.Label LabelConfirmPassword;
        private System.Windows.Forms.Label LabelLoginAccount;
        private System.Windows.Forms.Button BtnMap;
        private System.Windows.Forms.Panel PanelMapControls;
        private System.Windows.Forms.Label LabelMapCacheLocation;
        private System.Windows.Forms.TextBox TBoxMapCacheLocation;
        private System.Windows.Forms.Button BtnBrowseMapLocation;
        private System.Windows.Forms.RadioButton RBtnGlobalMap;
        private System.Windows.Forms.Panel PanelGlobalMapControls;
        private System.Windows.Forms.Label LabelIPLatitude;
        private System.Windows.Forms.Label LabelInitialPosition;
        private System.Windows.Forms.TextBox TBoxIPLatitude;
        private System.Windows.Forms.Label LabelIPLongtitude;
        private System.Windows.Forms.TextBox TBoxIPLongitude;
        private System.Windows.Forms.Label LabelInitialZoom;
        private System.Windows.Forms.TextBox TBoxInitialZoom;
        private System.Windows.Forms.RadioButton RBtnIndoorMap;
        private System.Windows.Forms.Panel PanelIndoorMapControls;
        private System.Windows.Forms.Label LabelImageMapLocation;
        private System.Windows.Forms.TextBox TBoxIndoorMapLocation;
        private System.Windows.Forms.Button BtnBrowseIndoorMap;
        private System.Windows.Forms.Button BtnMapConfigReset;
        private System.Windows.Forms.Button BtnMapConfigApply;
    }
}