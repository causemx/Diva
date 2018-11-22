namespace Diva.Controls
{
	partial class PowerModelInfo
	{
		/// <summary> 
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清除任何使用中的資源。
		/// </summary>
		/// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 元件設計工具產生的程式碼

		/// <summary> 
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.LBLConsumption = new System.Windows.Forms.TextBox();
			this.LBLDescription = new System.Windows.Forms.Label();
			this.PBHint = new System.Windows.Forms.PictureBox();
			this.BTNClose = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.PBHint)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Noto Sans", 11.25F);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(10, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Power consumption:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Noto Sans", 9F);
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(271, 44);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "mAh";
			// 
			// LBLConsumption
			// 
			this.LBLConsumption.Location = new System.Drawing.Point(164, 41);
			this.LBLConsumption.Name = "LBLConsumption";
			this.LBLConsumption.Size = new System.Drawing.Size(100, 22);
			this.LBLConsumption.TabIndex = 2;
			// 
			// LBLDescription
			// 
			this.LBLDescription.AutoSize = true;
			this.LBLDescription.Font = new System.Drawing.Font("Noto Sans", 11.25F);
			this.LBLDescription.ForeColor = System.Drawing.Color.White;
			this.LBLDescription.Location = new System.Drawing.Point(91, 76);
			this.LBLDescription.Name = "LBLDescription";
			this.LBLDescription.Size = new System.Drawing.Size(175, 20);
			this.LBLDescription.TabIndex = 4;
			this.LBLDescription.Text = "Full Power(about 100%)";
			// 
			// PBHint
			// 
			this.PBHint.Image = global::Diva.Properties.Resources.icon_warn;
			this.PBHint.Location = new System.Drawing.Point(53, 70);
			this.PBHint.Name = "PBHint";
			this.PBHint.Size = new System.Drawing.Size(32, 32);
			this.PBHint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PBHint.TabIndex = 3;
			this.PBHint.TabStop = false;
			// 
			// BTNClose
			// 
			this.BTNClose.AutoSize = true;
			this.BTNClose.FlatAppearance.BorderSize = 0;
			this.BTNClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BTNClose.Font = new System.Drawing.Font("Noto Sans", 9F);
			this.BTNClose.ForeColor = System.Drawing.Color.White;
			this.BTNClose.Image = global::Diva.Properties.Resources.icon_close_24;
			this.BTNClose.Location = new System.Drawing.Point(305, 2);
			this.BTNClose.Name = "BTNClose";
			this.BTNClose.Size = new System.Drawing.Size(32, 32);
			this.BTNClose.TabIndex = 5;
			this.BTNClose.UseVisualStyleBackColor = true;
			this.BTNClose.Click += new System.EventHandler(this.BTNClose_Click);
			// 
			// PowerModelInfo
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.BTNClose);
			this.Controls.Add(this.LBLDescription);
			this.Controls.Add(this.PBHint);
			this.Controls.Add(this.LBLConsumption);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "PowerModelInfo";
			this.Size = new System.Drawing.Size(340, 119);
			((System.ComponentModel.ISupportInitialize)(this.PBHint)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox LBLConsumption;
		private System.Windows.Forms.Label LBLDescription;
		private System.Windows.Forms.PictureBox PBHint;
		private System.Windows.Forms.Button BTNClose;
	}
}
