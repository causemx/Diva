namespace Diva.Controls
{
	partial class ProgressDialog
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
			this.LBL_Message = new System.Windows.Forms.Label();
			this.MyProgressBar = new System.Windows.Forms.ProgressBar();
			this.Icon = new System.Windows.Forms.PictureBox();
			this.But_Cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
			this.SuspendLayout();
			// 
			// LBL_Message
			// 
			this.LBL_Message.AutoSize = true;
			this.LBL_Message.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LBL_Message.ForeColor = System.Drawing.Color.Black;
			this.LBL_Message.Location = new System.Drawing.Point(73, 18);
			this.LBL_Message.Name = "LBL_Message";
			this.LBL_Message.Size = new System.Drawing.Size(187, 19);
			this.LBL_Message.TabIndex = 0;
			this.LBL_Message.Text = "wait for work complete...";
			// 
			// MyProgressBar
			// 
			this.MyProgressBar.Location = new System.Drawing.Point(16, 50);
			this.MyProgressBar.Name = "MyProgressBar";
			this.MyProgressBar.Size = new System.Drawing.Size(356, 23);
			this.MyProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.MyProgressBar.TabIndex = 1;
			// 
			// Icon
			// 
			this.Icon.Image = global::Diva.Properties.Resources.if_warning_32;
			this.Icon.Location = new System.Drawing.Point(16, 5);
			this.Icon.Name = "Icon";
			this.Icon.Size = new System.Drawing.Size(51, 39);
			this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.Icon.TabIndex = 5;
			this.Icon.TabStop = false;
			// 
			// But_Cancel
			// 
			this.But_Cancel.Font = new System.Drawing.Font("Noto Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.But_Cancel.Location = new System.Drawing.Point(153, 82);
			this.But_Cancel.Name = "But_Cancel";
			this.But_Cancel.Size = new System.Drawing.Size(75, 23);
			this.But_Cancel.TabIndex = 6;
			this.But_Cancel.Text = "Cancel";
			this.But_Cancel.UseVisualStyleBackColor = true;
			this.But_Cancel.Click += new System.EventHandler(this.But_Cancel_Click);
			// 
			// ProgressDialog
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(384, 117);
			this.Controls.Add(this.But_Cancel);
			this.Controls.Add(this.Icon);
			this.Controls.Add(this.MyProgressBar);
			this.Controls.Add(this.LBL_Message);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ProgressDialog";
			this.Text = "ProgressDialog";
			((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LBL_Message;
		private System.Windows.Forms.ProgressBar MyProgressBar;
		private System.Windows.Forms.PictureBox Icon;
		private System.Windows.Forms.Button But_Cancel;
	}
}