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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureForm));
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
            this.BtnLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SidePanel.SuspendLayout();
            this.BtnLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.SidePanel.Controls.Add(this.BtnLayoutPanel);
            this.SidePanel.Controls.Add(this.IndicatorPanel);
            resources.ApplyResources(this.SidePanel, "SidePanel");
            this.SidePanel.Name = "SidePanel";
            // 
            // BtnMap
            // 
            this.BtnMap.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnMap, "BtnMap");
            this.BtnMap.ForeColor = System.Drawing.Color.White;
            this.BtnMap.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnMap.Name = "BtnMap";
            this.BtnMap.UseVisualStyleBackColor = true;
            this.BtnMap.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAccount
            // 
            this.BtnAccount.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnAccount, "BtnAccount");
            this.BtnAccount.ForeColor = System.Drawing.Color.White;
            this.BtnAccount.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnAccount.Name = "BtnAccount";
            this.BtnAccount.UseVisualStyleBackColor = true;
            this.BtnAccount.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // IndicatorPanel
            // 
            this.IndicatorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
            resources.ApplyResources(this.IndicatorPanel, "IndicatorPanel");
            this.IndicatorPanel.Name = "IndicatorPanel";
            // 
            // BtnVehicle
            // 
            this.BtnVehicle.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnVehicle, "BtnVehicle");
            this.BtnVehicle.ForeColor = System.Drawing.Color.White;
            this.BtnVehicle.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnVehicle.Name = "BtnVehicle";
            this.BtnVehicle.UseVisualStyleBackColor = true;
            this.BtnVehicle.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnAbout
            // 
            this.BtnAbout.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnAbout, "BtnAbout");
            this.BtnAbout.ForeColor = System.Drawing.Color.White;
            this.BtnAbout.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnAbout.Name = "BtnAbout";
            this.BtnAbout.UseVisualStyleBackColor = true;
            this.BtnAbout.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnTuning
            // 
            this.BtnTuning.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnTuning, "BtnTuning");
            this.BtnTuning.ForeColor = System.Drawing.Color.White;
            this.BtnTuning.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnTuning.Name = "BtnTuning";
            this.BtnTuning.UseVisualStyleBackColor = true;
            this.BtnTuning.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // BtnGeoFence
            // 
            this.BtnGeoFence.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.BtnGeoFence, "BtnGeoFence");
            this.BtnGeoFence.ForeColor = System.Drawing.Color.White;
            this.BtnGeoFence.Image = global::Diva.Properties.Resources.icon_emoticon_48;
            this.BtnGeoFence.Name = "BtnGeoFence";
            this.BtnGeoFence.UseVisualStyleBackColor = true;
            this.BtnGeoFence.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // configMapPage
            // 
            this.configMapPage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            resources.ApplyResources(this.configMapPage, "configMapPage");
            this.configMapPage.ForeColor = System.Drawing.Color.White;
            this.configMapPage.Name = "configMapPage";
            // 
            // configAccountPage
            // 
            this.configAccountPage.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            resources.ApplyResources(this.configAccountPage, "configAccountPage");
            this.configAccountPage.ForeColor = System.Drawing.Color.White;
            this.configAccountPage.Name = "configAccountPage";
            // 
            // BtnLayoutPanel
            // 
            this.BtnLayoutPanel.Controls.Add(this.BtnVehicle);
            this.BtnLayoutPanel.Controls.Add(this.BtnTuning);
            this.BtnLayoutPanel.Controls.Add(this.BtnGeoFence);
            this.BtnLayoutPanel.Controls.Add(this.BtnMap);
            this.BtnLayoutPanel.Controls.Add(this.BtnAccount);
            this.BtnLayoutPanel.Controls.Add(this.BtnAbout);
            resources.ApplyResources(this.BtnLayoutPanel, "BtnLayoutPanel");
            this.BtnLayoutPanel.Name = "BtnLayoutPanel";
            // 
            // ConfigureForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.configAccountPage);
            this.Controls.Add(this.configMapPage);
            this.Controls.Add(this.SidePanel);
            this.Name = "ConfigureForm";
            this.SidePanel.ResumeLayout(false);
            this.BtnLayoutPanel.ResumeLayout(false);
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
        private System.Windows.Forms.FlowLayoutPanel BtnLayoutPanel;
    }
}