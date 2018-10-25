namespace Diva.Controls
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressDialogV2));
			this.btn_cancel = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.lbl_progress_message = new System.Windows.Forms.Label();
			this.img_warning = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.img_warning)).BeginInit();
			this.SuspendLayout();
			// 
			// btn_cancel
			// 
			resources.ApplyResources(this.btn_cancel, "btn_cancel");
			this.btn_cancel.Name = "btn_cancel";
			this.btn_cancel.UseVisualStyleBackColor = true;
			this.btn_cancel.Click += new System.EventHandler(this.BtnCancel_Click);
			// 
			// progressBar
			// 
			resources.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			// 
			// lbl_progress_message
			// 
			resources.ApplyResources(this.lbl_progress_message, "lbl_progress_message");
			this.lbl_progress_message.AutoEllipsis = true;
			this.lbl_progress_message.Name = "lbl_progress_message";
			// 
			// img_warning
			// 
			resources.ApplyResources(this.img_warning, "img_warning");
			this.img_warning.Name = "img_warning";
			this.img_warning.TabStop = false;
			// 
			// ProgressDialogV2
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.img_warning);
			this.Controls.Add(this.lbl_progress_message);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.btn_cancel);
			this.Name = "ProgressDialogV2";
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