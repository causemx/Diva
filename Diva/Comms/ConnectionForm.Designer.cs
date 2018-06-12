namespace Diva.Comms
{
	partial class ConnectionForm
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
			this.BtnSave = new System.Windows.Forms.Button();
			this.BtnExit = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// BtnSave
			// 
			this.BtnSave.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnSave.FlatAppearance.BorderSize = 0;
			this.BtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnSave.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnSave.ForeColor = System.Drawing.Color.White;
			this.BtnSave.Location = new System.Drawing.Point(312, 619);
			this.BtnSave.Name = "BtnSave";
			this.BtnSave.Size = new System.Drawing.Size(75, 31);
			this.BtnSave.TabIndex = 0;
			this.BtnSave.Text = "Save";
			this.BtnSave.UseVisualStyleBackColor = false;
			this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// BtnExit
			// 
			this.BtnExit.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.BtnExit.FlatAppearance.BorderSize = 0;
			this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BtnExit.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnExit.ForeColor = System.Drawing.Color.White;
			this.BtnExit.Location = new System.Drawing.Point(393, 619);
			this.BtnExit.Name = "BtnExit";
			this.BtnExit.Size = new System.Drawing.Size(75, 31);
			this.BtnExit.TabIndex = 1;
			this.BtnExit.Text = "Exit";
			this.BtnExit.UseVisualStyleBackColor = false;
			this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
			// 
			// ConnectionForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(492, 662);
			this.Controls.Add(this.BtnExit);
			this.Controls.Add(this.BtnSave);
			this.Name = "ConnectionForm";
			this.Text = "Connection";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button BtnSave;
		private System.Windows.Forms.Button BtnExit;
	}
}