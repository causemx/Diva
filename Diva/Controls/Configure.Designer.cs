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
			this.SidePanel = new System.Windows.Forms.Panel();
			this.BtnVehicle = new System.Windows.Forms.Button();
			this.BtnAbout = new System.Windows.Forms.Button();
			this.BtnTuning = new System.Windows.Forms.Button();
			this.BtnGeoFence = new System.Windows.Forms.Button();
			this.configVehicle21 = new Diva.Controls.ConfigVehicle2();
			this.side_panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// side_panel
			// 
			this.side_panel.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.side_panel.Controls.Add(this.SidePanel);
			this.side_panel.Controls.Add(this.BtnVehicle);
			this.side_panel.Controls.Add(this.BtnAbout);
			this.side_panel.Controls.Add(this.BtnTuning);
			this.side_panel.Controls.Add(this.BtnGeoFence);
			this.side_panel.Dock = System.Windows.Forms.DockStyle.Left;
			this.side_panel.Location = new System.Drawing.Point(0, 0);
			this.side_panel.Name = "side_panel";
			this.side_panel.Size = new System.Drawing.Size(250, 603);
			this.side_panel.TabIndex = 0;
			// 
			// SidePanel
			// 
			this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(8)))), ((int)(((byte)(55)))));
			this.SidePanel.Location = new System.Drawing.Point(0, 30);
			this.SidePanel.Name = "SidePanel";
			this.SidePanel.Size = new System.Drawing.Size(12, 80);
			this.SidePanel.TabIndex = 5;
			// 
			// BtnVehicle
			// 
			this.BtnVehicle.FlatAppearance.BorderSize = 0;
			this.BtnVehicle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnVehicle.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnVehicle.ForeColor = System.Drawing.Color.White;
			this.BtnVehicle.Image = global::Diva.Properties.Resources.icon_emoticon_48;
			this.BtnVehicle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnVehicle.Location = new System.Drawing.Point(12, 30);
			this.BtnVehicle.Name = "BtnVehicle";
			this.BtnVehicle.Padding = new System.Windows.Forms.Padding(5);
			this.BtnVehicle.Size = new System.Drawing.Size(238, 80);
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
			this.BtnAbout.Location = new System.Drawing.Point(12, 270);
			this.BtnAbout.Name = "BtnAbout";
			this.BtnAbout.Padding = new System.Windows.Forms.Padding(5);
			this.BtnAbout.Size = new System.Drawing.Size(238, 80);
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
			this.BtnTuning.Location = new System.Drawing.Point(12, 190);
			this.BtnTuning.Name = "BtnTuning";
			this.BtnTuning.Padding = new System.Windows.Forms.Padding(5);
			this.BtnTuning.Size = new System.Drawing.Size(238, 80);
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
			this.BtnGeoFence.Location = new System.Drawing.Point(12, 110);
			this.BtnGeoFence.Name = "BtnGeoFence";
			this.BtnGeoFence.Padding = new System.Windows.Forms.Padding(5);
			this.BtnGeoFence.Size = new System.Drawing.Size(238, 80);
			this.BtnGeoFence.TabIndex = 0;
			this.BtnGeoFence.Text = "Geofence";
			this.BtnGeoFence.UseVisualStyleBackColor = true;
			this.BtnGeoFence.Click += new System.EventHandler(this.MenuButton_Click);
			// 
			// configVehicle21
			// 
			this.configVehicle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.configVehicle21.Dock = System.Windows.Forms.DockStyle.Fill;
			this.configVehicle21.Location = new System.Drawing.Point(250, 0);
			this.configVehicle21.Name = "configVehicle21";
			this.configVehicle21.Size = new System.Drawing.Size(735, 603);
			this.configVehicle21.TabIndex = 1;
			// 
			// Configure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.ClientSize = new System.Drawing.Size(985, 603);
			this.Controls.Add(this.configVehicle21);
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
		private System.Windows.Forms.Button BtnAbout;
		private System.Windows.Forms.Button BtnVehicle;
		private System.Windows.Forms.Panel SidePanel;
		private ConfigVehicle2 configVehicle21;
	}
}