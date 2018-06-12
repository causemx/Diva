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
			this.DTxtPortName = new System.Windows.Forms.TextBox();
			this.but_confirm = new System.Windows.Forms.Button();
			this.but_cancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.DTxtPortNumber = new System.Windows.Forms.TextBox();
			this.DCbBaudrate = new System.Windows.Forms.ComboBox();
			this.DTxtStreaming = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(59, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Port";
			// 
			// DTxtPortName
			// 
			this.DTxtPortName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DTxtPortName.Location = new System.Drawing.Point(106, 18);
			this.DTxtPortName.Name = "DTxtPortName";
			this.DTxtPortName.Size = new System.Drawing.Size(338, 22);
			this.DTxtPortName.TabIndex = 1;
			// 
			// but_confirm
			// 
			this.but_confirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.but_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.but_confirm.FlatAppearance.BorderSize = 0;
			this.but_confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.but_confirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_confirm.Location = new System.Drawing.Point(282, 156);
			this.but_confirm.Name = "but_confirm";
			this.but_confirm.Size = new System.Drawing.Size(75, 23);
			this.but_confirm.TabIndex = 2;
			this.but_confirm.Text = "Confirm";
			this.but_confirm.UseVisualStyleBackColor = false;
			this.but_confirm.Click += new System.EventHandler(this.but_confirm_click);
			// 
			// but_cancel
			// 
			this.but_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.but_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.but_cancel.FlatAppearance.BorderSize = 0;
			this.but_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.but_cancel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.but_cancel.Location = new System.Drawing.Point(369, 156);
			this.but_cancel.Name = "but_cancel";
			this.but_cancel.Size = new System.Drawing.Size(75, 23);
			this.but_cancel.TabIndex = 3;
			this.but_cancel.Text = "Cancel";
			this.but_cancel.UseVisualStyleBackColor = false;
			this.but_cancel.Click += new System.EventHandler(this.but_cancel_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(33, 86);
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
			this.label3.Location = new System.Drawing.Point(13, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 14);
			this.label3.TabIndex = 6;
			this.label3.Text = "Port number";
			// 
			// DTxtPortNumber
			// 
			this.DTxtPortNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DTxtPortNumber.Location = new System.Drawing.Point(106, 51);
			this.DTxtPortNumber.Name = "DTxtPortNumber";
			this.DTxtPortNumber.Size = new System.Drawing.Size(338, 22);
			this.DTxtPortNumber.TabIndex = 7;
			// 
			// DCbBaudrate
			// 
			this.DCbBaudrate.FormattingEnabled = true;
			this.DCbBaudrate.Items.AddRange(new object[] {
            "9600",
            "57600",
            "115200"});
			this.DCbBaudrate.Location = new System.Drawing.Point(105, 84);
			this.DCbBaudrate.Name = "DCbBaudrate";
			this.DCbBaudrate.Size = new System.Drawing.Size(339, 20);
			this.DCbBaudrate.TabIndex = 8;
			// 
			// DTxtStreaming
			// 
			this.DTxtStreaming.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DTxtStreaming.Location = new System.Drawing.Point(106, 115);
			this.DTxtStreaming.Name = "DTxtStreaming";
			this.DTxtStreaming.Size = new System.Drawing.Size(338, 22);
			this.DTxtStreaming.TabIndex = 11;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(27, 118);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(62, 14);
			this.label5.TabIndex = 12;
			this.label5.Text = "Streaming";
			// 
			// ProgressInputDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
			this.ClientSize = new System.Drawing.Size(473, 191);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.DTxtStreaming);
			this.Controls.Add(this.DCbBaudrate);
			this.Controls.Add(this.DTxtPortNumber);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.but_cancel);
			this.Controls.Add(this.but_confirm);
			this.Controls.Add(this.DTxtPortName);
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
		private System.Windows.Forms.TextBox DTxtPortName;
		private System.Windows.Forms.Button but_confirm;
		private System.Windows.Forms.Button but_cancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox DTxtPortNumber;
		private System.Windows.Forms.ComboBox DCbBaudrate;
		private System.Windows.Forms.TextBox DTxtStreaming;
		private System.Windows.Forms.Label label5;
	}
}