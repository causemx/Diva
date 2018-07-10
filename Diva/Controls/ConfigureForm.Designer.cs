namespace Diva.Controls
{
	partial class ConfigureForm
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
            this.configMapPage = new Diva.Controls.ConfigMapPage();
            this.configAccountPage = new Diva.Controls.ConfigAccountPage();
            this.SidePanel.SuspendLayout();
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
            // configMapPage
            // 
            this.configMapPage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.configMapPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configMapPage.ForeColor = System.Drawing.Color.White;
            this.configMapPage.Location = new System.Drawing.Point(333, 0);
            this.configMapPage.Name = "configMapPage";
            this.configMapPage.Size = new System.Drawing.Size(980, 754);
            this.configMapPage.TabIndex = 17;
            // 
            // configAccountPage
            // 
            this.configAccountPage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.configAccountPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configAccountPage.Font = new System.Drawing.Font("Georgia", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configAccountPage.ForeColor = System.Drawing.Color.White;
            this.configAccountPage.Location = new System.Drawing.Point(333, 0);
            this.configAccountPage.Margin = new System.Windows.Forms.Padding(4);
            this.configAccountPage.Name = "configAccountPage";
            this.configAccountPage.Size = new System.Drawing.Size(980, 754);
            this.configAccountPage.TabIndex = 18;
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(1313, 754);
            this.Controls.Add(this.configAccountPage);
            this.Controls.Add(this.configMapPage);
            this.Controls.Add(this.SidePanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfigureForm";
            this.Text = "Configure";
            this.SidePanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button BtnMap;
        private ConfigMapPage configMapPage;
        private ConfigAccountPage configAccountPage;
    }
}