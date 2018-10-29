namespace Diva.Controls
{
	partial class InformDialog
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformDialog));
			this.BUT_Cancel = new System.Windows.Forms.Button();
			this.LBL_Message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BUT_Cancel
			// 
			resources.ApplyResources(this.BUT_Cancel, "BUT_Cancel");
			this.BUT_Cancel.Name = "BUT_Cancel";
			this.BUT_Cancel.UseVisualStyleBackColor = true;
			this.BUT_Cancel.Click += new System.EventHandler(this.BUT_Cancel_Click);
			// 
			// LBL_Message
			// 
			resources.ApplyResources(this.LBL_Message, "LBL_Message");
			this.LBL_Message.Name = "LBL_Message";
			// 
			// InformDialog
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LBL_Message);
			this.Controls.Add(this.BUT_Cancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "InformDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BUT_Cancel;
		private System.Windows.Forms.Label LBL_Message;
	}
}