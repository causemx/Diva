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
			this.btn_help = new System.Windows.Forms.Button();
			this.btn_planner = new System.Windows.Forms.Button();
			this.btn_tuning = new System.Windows.Forms.Button();
			this.btn_geofence = new System.Windows.Forms.Button();
			this.configTuning1 = new Diva.Controls.ConfigTuning();
			this.side_panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// side_panel
			// 
			this.side_panel.Controls.Add(this.btn_help);
			this.side_panel.Controls.Add(this.btn_planner);
			this.side_panel.Controls.Add(this.btn_tuning);
			this.side_panel.Controls.Add(this.btn_geofence);
			this.side_panel.Dock = System.Windows.Forms.DockStyle.Left;
			this.side_panel.Location = new System.Drawing.Point(0, 0);
			this.side_panel.Name = "side_panel";
			this.side_panel.Size = new System.Drawing.Size(277, 603);
			this.side_panel.TabIndex = 0;
			// 
			// btn_help
			// 
			this.btn_help.Dock = System.Windows.Forms.DockStyle.Top;
			this.btn_help.Location = new System.Drawing.Point(0, 270);
			this.btn_help.Name = "btn_help";
			this.btn_help.Size = new System.Drawing.Size(277, 90);
			this.btn_help.TabIndex = 3;
			this.btn_help.Text = "help";
			this.btn_help.UseVisualStyleBackColor = true;
			// 
			// btn_planner
			// 
			this.btn_planner.Dock = System.Windows.Forms.DockStyle.Top;
			this.btn_planner.Location = new System.Drawing.Point(0, 180);
			this.btn_planner.Name = "btn_planner";
			this.btn_planner.Size = new System.Drawing.Size(277, 90);
			this.btn_planner.TabIndex = 2;
			this.btn_planner.Text = "planner";
			this.btn_planner.UseVisualStyleBackColor = true;
			// 
			// btn_tuning
			// 
			this.btn_tuning.Dock = System.Windows.Forms.DockStyle.Top;
			this.btn_tuning.Location = new System.Drawing.Point(0, 90);
			this.btn_tuning.Name = "btn_tuning";
			this.btn_tuning.Size = new System.Drawing.Size(277, 90);
			this.btn_tuning.TabIndex = 1;
			this.btn_tuning.Text = "tuning";
			this.btn_tuning.UseVisualStyleBackColor = true;
			// 
			// btn_geofence
			// 
			this.btn_geofence.Dock = System.Windows.Forms.DockStyle.Top;
			this.btn_geofence.Location = new System.Drawing.Point(0, 0);
			this.btn_geofence.Name = "btn_geofence";
			this.btn_geofence.Size = new System.Drawing.Size(277, 90);
			this.btn_geofence.TabIndex = 0;
			this.btn_geofence.Text = "geofence";
			this.btn_geofence.UseVisualStyleBackColor = true;
			this.btn_geofence.Click += new System.EventHandler(this.btn_geofence_Click);
			// 
			// configTuning1
			// 
			this.configTuning1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.configTuning1.Location = new System.Drawing.Point(277, 0);
			this.configTuning1.Name = "configTuning1";
			this.configTuning1.Size = new System.Drawing.Size(708, 603);
			this.configTuning1.TabIndex = 1;
			// 
			// Configure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(985, 603);
			this.Controls.Add(this.configTuning1);
			this.Controls.Add(this.side_panel);
			this.Name = "Configure";
			this.Text = "Configure";
			this.side_panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel side_panel;
		private System.Windows.Forms.Button btn_tuning;
		private System.Windows.Forms.Button btn_geofence;
		private System.Windows.Forms.Button btn_planner;
		private System.Windows.Forms.Button btn_help;
		private ConfigTuning configTuning1;
	}
}