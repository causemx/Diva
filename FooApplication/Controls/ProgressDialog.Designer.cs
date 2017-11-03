namespace FooApplication.Controls
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
			this.lbl_message = new System.Windows.Forms.Label();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.but_nagtive = new System.Windows.Forms.Button();
			this.but_positive = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// lbl_message
			// 
			this.lbl_message.AutoSize = true;
			this.lbl_message.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_message.ForeColor = System.Drawing.Color.White;
			this.lbl_message.Location = new System.Drawing.Point(73, 18);
			this.lbl_message.Name = "lbl_message";
			this.lbl_message.Size = new System.Drawing.Size(187, 19);
			this.lbl_message.TabIndex = 0;
			this.lbl_message.Text = "wait for work complete...";
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(16, 50);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(356, 23);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 1;
			// 
			// but_nagtive
			// 
			this.but_nagtive.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_nagtive.Location = new System.Drawing.Point(297, 82);
			this.but_nagtive.Name = "but_nagtive";
			this.but_nagtive.Size = new System.Drawing.Size(75, 31);
			this.but_nagtive.TabIndex = 3;
			this.but_nagtive.Text = "cancel";
			this.but_nagtive.UseVisualStyleBackColor = true;
			this.but_nagtive.Click += new System.EventHandler(this.but_nagtive_Click);
			// 
			// but_positive
			// 
			this.but_positive.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_positive.Location = new System.Drawing.Point(216, 82);
			this.but_positive.Name = "but_positive";
			this.but_positive.Size = new System.Drawing.Size(75, 31);
			this.but_positive.TabIndex = 4;
			this.but_positive.Text = "ignore";
			this.but_positive.UseVisualStyleBackColor = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::FooApplication.Properties.Resources.if_warning_32;
			this.pictureBox1.Location = new System.Drawing.Point(16, 5);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(51, 39);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// ProgressDialog
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(384, 123);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.but_positive);
			this.Controls.Add(this.but_nagtive);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.lbl_message);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ProgressDialog";
			this.Text = "ProgressDialog";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lbl_message;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Button but_nagtive;
		private System.Windows.Forms.Button but_positive;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}