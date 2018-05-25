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
			this.side_panel = new System.Windows.Forms.Panel();
			this.BtnHelp = new System.Windows.Forms.Button();
			this.BtnPlanner = new System.Windows.Forms.Button();
			this.BtnTuning = new System.Windows.Forms.Button();
			this.BtnGeoFence = new System.Windows.Forms.Button();
			this.configGeoFence1 = new Diva.Controls.ConfigGeoFence();
			this.configTuning1 = new Diva.Controls.ConfigTuning();
			this.side_panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// side_panel
			// 
			this.side_panel.Controls.Add(this.BtnHelp);
			this.side_panel.Controls.Add(this.BtnPlanner);
			this.side_panel.Controls.Add(this.BtnTuning);
			this.side_panel.Controls.Add(this.BtnGeoFence);
			this.side_panel.Dock = System.Windows.Forms.DockStyle.Left;
			this.side_panel.Location = new System.Drawing.Point(0, 0);
			this.side_panel.Name = "side_panel";
			this.side_panel.Size = new System.Drawing.Size(222, 603);
			this.side_panel.TabIndex = 0;
			// 
			// BtnHelp
			// 
			this.BtnHelp.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnHelp.Location = new System.Drawing.Point(0, 270);
			this.BtnHelp.Name = "BtnHelp";
			this.BtnHelp.Size = new System.Drawing.Size(222, 90);
			this.BtnHelp.TabIndex = 3;
			this.BtnHelp.Text = "help";
			this.BtnHelp.UseVisualStyleBackColor = true;
			// 
			// BtnPlanner
			// 
			this.BtnPlanner.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnPlanner.Location = new System.Drawing.Point(0, 180);
			this.BtnPlanner.Name = "BtnPlanner";
			this.BtnPlanner.Size = new System.Drawing.Size(222, 90);
			this.BtnPlanner.TabIndex = 2;
			this.BtnPlanner.Text = "planner";
			this.BtnPlanner.UseVisualStyleBackColor = true;
			// 
			// BtnTuning
			// 
			this.BtnTuning.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnTuning.Location = new System.Drawing.Point(0, 90);
			this.BtnTuning.Name = "BtnTuning";
			this.BtnTuning.Size = new System.Drawing.Size(222, 90);
			this.BtnTuning.TabIndex = 1;
			this.BtnTuning.Text = "tuning";
			this.BtnTuning.UseVisualStyleBackColor = true;
			this.BtnTuning.Click += new System.EventHandler(this.BtnTuning_Click);
			// 
			// BtnGeoFence
			// 
			this.BtnGeoFence.Dock = System.Windows.Forms.DockStyle.Top;
			this.BtnGeoFence.Location = new System.Drawing.Point(0, 0);
			this.BtnGeoFence.Name = "BtnGeoFence";
			this.BtnGeoFence.Size = new System.Drawing.Size(222, 90);
			this.BtnGeoFence.TabIndex = 0;
			this.BtnGeoFence.Text = "geofence";
			this.BtnGeoFence.UseVisualStyleBackColor = true;
			this.BtnGeoFence.Click += new System.EventHandler(this.BtnGeoFence_Click);
			// 
			// configGeoFence1
			// 
			this.configGeoFence1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.configGeoFence1.Location = new System.Drawing.Point(222, 0);
			this.configGeoFence1.Name = "configGeoFence1";
			this.configGeoFence1.Size = new System.Drawing.Size(763, 603);
			this.configGeoFence1.TabIndex = 1;
			// 
			// configTuning1
			// 
			this.configTuning1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.configTuning1.Location = new System.Drawing.Point(222, 0);
			this.configTuning1.Name = "configTuning1";
			this.configTuning1.Size = new System.Drawing.Size(763, 603);
			this.configTuning1.TabIndex = 2;
		
			// 
			// Configure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(985, 603);
			this.Controls.Add(this.configTuning1);
			this.Controls.Add(this.configGeoFence1);
			this.Controls.Add(this.side_panel);
			this.Name = "Configure";
			this.Text = "Configure";
			this.side_panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel side_panel;
		private System.Windows.Forms.Button BtnTuning;
		private System.Windows.Forms.Button BtnGeoFence;
		private System.Windows.Forms.Button BtnPlanner;
		private System.Windows.Forms.Button BtnHelp;
		private ConfigGeoFence configGeoFence1;
		private ConfigTuning configTuning1;
	}
}