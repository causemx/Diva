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
			this.BtnLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.BtnVehicle = new System.Windows.Forms.Button();
			this.BtnGeoFence = new System.Windows.Forms.Button();
			this.BtnMap = new System.Windows.Forms.Button();
			this.BtnAccount = new System.Windows.Forms.Button();
			this.BtnAbout = new System.Windows.Forms.Button();
			this.IndicatorPanel = new System.Windows.Forms.Panel();
			this.VehicleSettingsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.VehicleConfigPanel = new System.Windows.Forms.Panel();
			this.VConfBtnsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.BtnVConfReset = new System.Windows.Forms.Button();
			this.BtnVConfApply = new System.Windows.Forms.Button();
			this.BtnVConfExport = new System.Windows.Forms.Button();
			this.BtnVConfImport = new System.Windows.Forms.Button();
			this.AboutBoxPanel = new System.Windows.Forms.Panel();
			this.CreditsTablePanel = new System.Windows.Forms.TableLayoutPanel();
			this.LabelProject = new System.Windows.Forms.Label();
			this.LabelLicense = new System.Windows.Forms.Label();
			this.LabelAboutSoftware = new System.Windows.Forms.Label();
			this.TBoxLicenseTerms = new System.Windows.Forms.TextBox();
			this.configMapPage = new Diva.Controls.ConfigMapPage();
			this.configGeoFencePage = new Diva.Controls.ConfigGeoFencePage();
			this.configAccountPage = new Diva.Controls.ConfigAccountPage();
			this.SidePanel.SuspendLayout();
			this.BtnLayoutPanel.SuspendLayout();
			this.VehicleConfigPanel.SuspendLayout();
			this.VConfBtnsPanel.SuspendLayout();
			this.AboutBoxPanel.SuspendLayout();
			this.CreditsTablePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// SidePanel
			// 
			this.SidePanel.BackColor = System.Drawing.Color.Black;
			this.SidePanel.Controls.Add(this.BtnLayoutPanel);
			this.SidePanel.Controls.Add(this.IndicatorPanel);
			resources.ApplyResources(this.SidePanel, "SidePanel");
			this.SidePanel.Name = "SidePanel";
			// 
			// BtnLayoutPanel
			// 
			this.BtnLayoutPanel.Controls.Add(this.BtnVehicle);
			this.BtnLayoutPanel.Controls.Add(this.BtnGeoFence);
			this.BtnLayoutPanel.Controls.Add(this.BtnMap);
			this.BtnLayoutPanel.Controls.Add(this.BtnAccount);
			this.BtnLayoutPanel.Controls.Add(this.BtnAbout);
			resources.ApplyResources(this.BtnLayoutPanel, "BtnLayoutPanel");
			this.BtnLayoutPanel.Name = "BtnLayoutPanel";
			// 
			// BtnVehicle
			// 
			this.BtnVehicle.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnVehicle, "BtnVehicle");
			this.BtnVehicle.ForeColor = System.Drawing.Color.White;
			this.BtnVehicle.Image = global::Diva.Properties.Resources.icon_drone;
			this.BtnVehicle.Name = "BtnVehicle";
			this.BtnVehicle.UseVisualStyleBackColor = true;
			this.BtnVehicle.Click += new System.EventHandler(this.MenuButton_Click);
			// 
			// BtnGeoFence
			// 
			this.BtnGeoFence.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnGeoFence, "BtnGeoFence");
			this.BtnGeoFence.ForeColor = System.Drawing.Color.White;
			this.BtnGeoFence.Image = global::Diva.Properties.Resources.icon_fence;
			this.BtnGeoFence.Name = "BtnGeoFence";
			this.BtnGeoFence.UseVisualStyleBackColor = true;
			this.BtnGeoFence.Click += new System.EventHandler(this.MenuButton_Click);
			// 
			// BtnMap
			// 
			this.BtnMap.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnMap, "BtnMap");
			this.BtnMap.ForeColor = System.Drawing.Color.White;
			this.BtnMap.Image = global::Diva.Properties.Resources.icon_map;
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
			// BtnAbout
			// 
			this.BtnAbout.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnAbout, "BtnAbout");
			this.BtnAbout.ForeColor = System.Drawing.Color.White;
			this.BtnAbout.Image = global::Diva.Properties.Resources.icon_information;
			this.BtnAbout.Name = "BtnAbout";
			this.BtnAbout.UseVisualStyleBackColor = true;
			this.BtnAbout.Click += new System.EventHandler(this.MenuButton_Click);
			// 
			// IndicatorPanel
			// 
			this.IndicatorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
			resources.ApplyResources(this.IndicatorPanel, "IndicatorPanel");
			this.IndicatorPanel.Name = "IndicatorPanel";
			// 
			// VehicleSettingsPanel
			// 
			resources.ApplyResources(this.VehicleSettingsPanel, "VehicleSettingsPanel");
			this.VehicleSettingsPanel.Name = "VehicleSettingsPanel";
			// 
			// VehicleConfigPanel
			// 
			this.VehicleConfigPanel.Controls.Add(this.VehicleSettingsPanel);
			this.VehicleConfigPanel.Controls.Add(this.VConfBtnsPanel);
			resources.ApplyResources(this.VehicleConfigPanel, "VehicleConfigPanel");
			this.VehicleConfigPanel.Name = "VehicleConfigPanel";
			// 
			// VConfBtnsPanel
			// 
			this.VConfBtnsPanel.Controls.Add(this.BtnVConfReset);
			this.VConfBtnsPanel.Controls.Add(this.BtnVConfApply);
			this.VConfBtnsPanel.Controls.Add(this.BtnVConfExport);
			this.VConfBtnsPanel.Controls.Add(this.BtnVConfImport);
			resources.ApplyResources(this.VConfBtnsPanel, "VConfBtnsPanel");
			this.VConfBtnsPanel.Name = "VConfBtnsPanel";
			// 
			// BtnVConfReset
			// 
			this.BtnVConfReset.BackColor = System.Drawing.Color.Black;
			this.BtnVConfReset.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnVConfReset, "BtnVConfReset");
			this.BtnVConfReset.ForeColor = System.Drawing.Color.White;
			this.BtnVConfReset.Name = "BtnVConfReset";
			this.BtnVConfReset.UseVisualStyleBackColor = false;
			this.BtnVConfReset.Click += new System.EventHandler(this.BtnVConfReset_Click);
			// 
			// BtnVConfApply
			// 
			this.BtnVConfApply.BackColor = System.Drawing.Color.Black;
			this.BtnVConfApply.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnVConfApply, "BtnVConfApply");
			this.BtnVConfApply.ForeColor = System.Drawing.Color.White;
			this.BtnVConfApply.Name = "BtnVConfApply";
			this.BtnVConfApply.UseVisualStyleBackColor = false;
			this.BtnVConfApply.Click += new System.EventHandler(this.BtnVConfApply_Click);
			// 
			// BtnVConfExport
			// 
			this.BtnVConfExport.BackColor = System.Drawing.Color.Black;
			this.BtnVConfExport.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnVConfExport, "BtnVConfExport");
			this.BtnVConfExport.ForeColor = System.Drawing.Color.White;
			this.BtnVConfExport.Name = "BtnVConfExport";
			this.BtnVConfExport.UseVisualStyleBackColor = false;
			this.BtnVConfExport.Click += new System.EventHandler(this.BtnVConfExport_Click);
			// 
			// BtnVConfImport
			// 
			this.BtnVConfImport.BackColor = System.Drawing.Color.Black;
			this.BtnVConfImport.FlatAppearance.BorderSize = 0;
			resources.ApplyResources(this.BtnVConfImport, "BtnVConfImport");
			this.BtnVConfImport.ForeColor = System.Drawing.Color.White;
			this.BtnVConfImport.Name = "BtnVConfImport";
			this.BtnVConfImport.UseVisualStyleBackColor = false;
			this.BtnVConfImport.Click += new System.EventHandler(this.BtnVConfImport_Click);
			// 
			// AboutBoxPanel
			// 
			resources.ApplyResources(this.AboutBoxPanel, "AboutBoxPanel");
			this.AboutBoxPanel.BackColor = System.Drawing.Color.Black;
			this.AboutBoxPanel.Controls.Add(this.CreditsTablePanel);
			this.AboutBoxPanel.Controls.Add(this.LabelAboutSoftware);
			this.AboutBoxPanel.ForeColor = System.Drawing.Color.White;
			this.AboutBoxPanel.Name = "AboutBoxPanel";
			// 
			// CreditsTablePanel
			// 
			resources.ApplyResources(this.CreditsTablePanel, "CreditsTablePanel");
			this.CreditsTablePanel.Controls.Add(this.LabelProject, 0, 0);
			this.CreditsTablePanel.Controls.Add(this.LabelLicense, 1, 0);
			this.CreditsTablePanel.Name = "CreditsTablePanel";
			// 
			// LabelProject
			// 
			resources.ApplyResources(this.LabelProject, "LabelProject");
			this.LabelProject.Name = "LabelProject";
			// 
			// LabelLicense
			// 
			resources.ApplyResources(this.LabelLicense, "LabelLicense");
			this.LabelLicense.Name = "LabelLicense";
			// 
			// LabelAboutSoftware
			// 
			resources.ApplyResources(this.LabelAboutSoftware, "LabelAboutSoftware");
			this.LabelAboutSoftware.Name = "LabelAboutSoftware";
			// 
			// TBoxLicenseTerms
			// 
			resources.ApplyResources(this.TBoxLicenseTerms, "TBoxLicenseTerms");
			this.TBoxLicenseTerms.Name = "TBoxLicenseTerms";
			this.TBoxLicenseTerms.ReadOnly = true;
			// 
			// configMapPage
			// 
			this.configMapPage.BackColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.configMapPage, "configMapPage");
			this.configMapPage.ForeColor = System.Drawing.Color.White;
			this.configMapPage.Name = "configMapPage";
			// 
			// configGeoFencePage
			// 
			this.configGeoFencePage.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			resources.ApplyResources(this.configGeoFencePage, "configGeoFencePage");
			this.configGeoFencePage.Name = "configGeoFencePage";
			// 
			// configAccountPage
			// 
			this.configAccountPage.BackColor = System.Drawing.Color.Black;
			resources.ApplyResources(this.configAccountPage, "configAccountPage");
			this.configAccountPage.ForeColor = System.Drawing.Color.White;
			this.configAccountPage.Name = "configAccountPage";
			// 
			// ConfigureForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.Controls.Add(this.TBoxLicenseTerms);
			this.Controls.Add(this.VehicleConfigPanel);
			this.Controls.Add(this.configGeoFencePage);
			this.Controls.Add(this.configMapPage);
			this.Controls.Add(this.configAccountPage);
			this.Controls.Add(this.AboutBoxPanel);
			this.Controls.Add(this.SidePanel);
			this.Name = "ConfigureForm";
			this.Load += new System.EventHandler(this.ConfigureForm_Load);
			this.SidePanel.ResumeLayout(false);
			this.BtnLayoutPanel.ResumeLayout(false);
			this.VehicleConfigPanel.ResumeLayout(false);
			this.VConfBtnsPanel.ResumeLayout(false);
			this.AboutBoxPanel.ResumeLayout(false);
			this.AboutBoxPanel.PerformLayout();
			this.CreditsTablePanel.ResumeLayout(false);
			this.CreditsTablePanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel SidePanel;
		private System.Windows.Forms.Button BtnGeoFence;
		private System.Windows.Forms.Button BtnAbout;
		private System.Windows.Forms.Button BtnVehicle;
		private System.Windows.Forms.Panel IndicatorPanel;
        private System.Windows.Forms.Button BtnMap;
        private ConfigMapPage configMapPage;
        private System.Windows.Forms.FlowLayoutPanel BtnLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel VehicleSettingsPanel;
        private System.Windows.Forms.Panel VehicleConfigPanel;
        private System.Windows.Forms.FlowLayoutPanel VConfBtnsPanel;
        private System.Windows.Forms.Button BtnVConfReset;
        private System.Windows.Forms.Button BtnVConfApply;
        private System.Windows.Forms.Button BtnVConfExport;
        private System.Windows.Forms.Button BtnVConfImport;
        private System.Windows.Forms.Panel AboutBoxPanel;
        private System.Windows.Forms.TableLayoutPanel CreditsTablePanel;
        private System.Windows.Forms.TextBox TBoxLicenseTerms;
        private System.Windows.Forms.Label LabelAboutSoftware;
        private System.Windows.Forms.Label LabelProject;
        private System.Windows.Forms.Label LabelLicense;
		private ConfigGeoFencePage configGeoFencePage;
		private ConfigAccountPage configAccountPage;
		private System.Windows.Forms.Button BtnAccount;
	}
}