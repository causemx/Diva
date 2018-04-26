namespace Diva.Controls
{
	partial class ProgressInputDialog
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
			this.label1 = new System.Windows.Forms.Label();
			this.txt_target = new System.Windows.Forms.TextBox();
			this.but_confirm = new System.Windows.Forms.Button();
			this.but_cancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_port = new System.Windows.Forms.TextBox();
			this.cb_baud = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(23, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Target";
			// 
			// txt_target
			// 
			this.txt_target.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_target.Location = new System.Drawing.Point(83, 18);
			this.txt_target.Name = "txt_target";
			this.txt_target.Size = new System.Drawing.Size(338, 22);
			this.txt_target.TabIndex = 1;
			// 
			// but_confirm
			// 
			this.but_confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.but_confirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_confirm.Location = new System.Drawing.Point(257, 125);
			this.but_confirm.Name = "but_confirm";
			this.but_confirm.Size = new System.Drawing.Size(75, 23);
			this.but_confirm.TabIndex = 2;
			this.but_confirm.Text = "Confirm";
			this.but_confirm.UseVisualStyleBackColor = true;
			this.but_confirm.Click += new System.EventHandler(this.but_confirm_click);
			// 
			// but_cancel
			// 
			this.but_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.but_cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_cancel.Location = new System.Drawing.Point(343, 125);
			this.but_cancel.Name = "but_cancel";
			this.but_cancel.Size = new System.Drawing.Size(75, 23);
			this.but_cancel.TabIndex = 3;
			this.but_cancel.Text = "Cancel";
			this.but_cancel.UseVisualStyleBackColor = true;
			this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(19, 87);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 14);
			this.label2.TabIndex = 4;
			this.label2.Text = "Baudrate";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(28, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Port";
			// 
			// txt_port
			// 
			this.txt_port.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_port.Location = new System.Drawing.Point(82, 51);
			this.txt_port.Name = "txt_port";
			this.txt_port.Size = new System.Drawing.Size(338, 22);
			this.txt_port.TabIndex = 7;
			// 
			// cb_baud
			// 
			this.cb_baud.FormattingEnabled = true;
			this.cb_baud.Items.AddRange(new object[] {
            "9600",
            "57600",
            "115200"});
			this.cb_baud.Location = new System.Drawing.Point(82, 85);
			this.cb_baud.Name = "cb_baud";
			this.cb_baud.Size = new System.Drawing.Size(339, 20);
			this.cb_baud.TabIndex = 8;
			// 
			// ProgressInputDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(449, 160);
			this.Controls.Add(this.cb_baud);
			this.Controls.Add(this.txt_port);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.but_cancel);
			this.Controls.Add(this.but_confirm);
			this.Controls.Add(this.txt_target);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "ProgressInputDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ProgressInputDialog";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_target;
		private System.Windows.Forms.Button but_confirm;
		private System.Windows.Forms.Button but_cancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_port;
		private System.Windows.Forms.ComboBox cb_baud;
	}
}