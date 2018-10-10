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
			this.BUT_Cancel = new System.Windows.Forms.Button();
			this.LBL_Message = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// BUT_Cancel
			// 
			this.BUT_Cancel.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BUT_Cancel.Location = new System.Drawing.Point(78, 100);
			this.BUT_Cancel.Name = "BUT_Cancel";
			this.BUT_Cancel.Size = new System.Drawing.Size(75, 23);
			this.BUT_Cancel.TabIndex = 1;
			this.BUT_Cancel.Text = "Cancel";
			this.BUT_Cancel.UseVisualStyleBackColor = true;
			this.BUT_Cancel.Click += new System.EventHandler(this.BUT_Cancel_Click);
			// 
			// LBL_Message
			// 
			this.LBL_Message.AutoSize = true;
			this.LBL_Message.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LBL_Message.Location = new System.Drawing.Point(42, 62);
			this.LBL_Message.Name = "LBL_Message";
			this.LBL_Message.Size = new System.Drawing.Size(0, 15);
			this.LBL_Message.TabIndex = 2;
			// 
			// InformDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(246, 166);
			this.Controls.Add(this.LBL_Message);
			this.Controls.Add(this.BUT_Cancel);
			this.Name = "InformDialog";
			this.Text = "InformDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button BUT_Cancel;
		private System.Windows.Forms.Label LBL_Message;
	}
}