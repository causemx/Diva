namespace FooApplication.Controls
{
	partial class Configuration
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.SidePanel = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.controlTuning1 = new FooApplication.Controls.ControlTuning();
			this.controlVersion1 = new FooApplication.Controls.ControlVersion();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.Silver;
			this.panel1.Controls.Add(this.SidePanel);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.button1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(200, 562);
			this.panel1.TabIndex = 0;
			// 
			// SidePanel
			// 
			this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.SidePanel.Location = new System.Drawing.Point(5, 5);
			this.SidePanel.Name = "SidePanel";
			this.SidePanel.Size = new System.Drawing.Size(10, 50);
			this.SidePanel.TabIndex = 2;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.Color.Silver;
			this.button2.FlatAppearance.BorderSize = 0;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.button2.Image = global::FooApplication.Properties.Resources.if_pear_2003196;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(4, 61);
			this.button2.Name = "button2";
			this.button2.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.button2.Size = new System.Drawing.Size(191, 50);
			this.button2.TabIndex = 3;
			this.button2.Text = " About";
			this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Silver;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.button1.Image = global::FooApplication.Properties.Resources.if_apple_2003193;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(4, 5);
			this.button1.Name = "button1";
			this.button1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.button1.Size = new System.Drawing.Size(191, 50);
			this.button1.TabIndex = 2;
			this.button1.Text = " Tuning";
			this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.Brown;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(200, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(743, 10);
			this.panel2.TabIndex = 1;
			// 
			// controlTuning1
			// 
			this.controlTuning1.Location = new System.Drawing.Point(200, 5);
			this.controlTuning1.Name = "controlTuning1";
			this.controlTuning1.Size = new System.Drawing.Size(743, 557);
			this.controlTuning1.TabIndex = 2;
			// 
			// controlVersion1
			// 
			this.controlVersion1.Location = new System.Drawing.Point(200, 5);
			this.controlVersion1.Name = "controlVersion1";
			this.controlVersion1.Size = new System.Drawing.Size(743, 557);
			this.controlVersion1.TabIndex = 3;
			// 
			// Configuration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(943, 562);
			this.Controls.Add(this.controlVersion1);
			this.Controls.Add(this.controlTuning1);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "Configuration";
			this.Text = "Configuration";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Panel SidePanel;
		private ControlTuning controlTuning1;
		private ControlVersion controlVersion1;
	}
}