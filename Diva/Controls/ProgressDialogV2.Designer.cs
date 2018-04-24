namespace FooApplication.Controls
{
	partial class ProgressDialogV2
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
			this.btn_cancel = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.lbl_progress_message = new System.Windows.Forms.Label();
			this.img_warning = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.img_warning)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_cancel
			// 
			this.btn_cancel.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_cancel.Location = new System.Drawing.Point(107, 184);
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.Size = new System.Drawing.Size(75, 23);
			this.btn_cancel.TabIndex = 0;
			this.btn_cancel.Text = "cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// progressBar
			// 
			this.progressBar.Location = new System.Drawing.Point(63, 155);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(165, 23);
			this.progressBar.TabIndex = 1;
			// 
			// lbl_progress_message
			// 
			this.lbl_progress_message.AutoSize = true;
			this.lbl_progress_message.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_progress_message.Location = new System.Drawing.Point(105, 127);
			this.lbl_progress_message.Name = "lbl_progress_message";
			this.lbl_progress_message.Size = new System.Drawing.Size(136, 15);
			this.lbl_progress_message.TabIndex = 2;
			this.lbl_progress_message.Text = "Do something please...";
			// 
			// img_warning
			// 
			this.img_warning.Image = global::FooApplication.Properties.Resources.if_warning_32;
			this.img_warning.Location = new System.Drawing.Point(63, 117);
			this.img_warning.Name = "img_warning";
			this.img_warning.Size = new System.Drawing.Size(32, 32);
			this.img_warning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.img_warning.TabIndex = 3;
			this.img_warning.TabStop = false;
			// 
			// ProgressDialogV2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.img_warning);
			this.Controls.Add(this.lbl_progress_message);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btn_cancel);
			this.Name = "ProgressDialogV2";
			this.Text = "ProgressDialogV2";
			((System.ComponentModel.ISupportInitialize)(this.img_warning)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_cancel;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label lbl_progress_message;
		private System.Windows.Forms.PictureBox img_warning;
	}
}