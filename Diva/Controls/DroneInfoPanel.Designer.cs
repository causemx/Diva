namespace Diva.Controls
{
	partial class DroneInfoPanel
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.TxtDroneName = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.TxtAssumeTime = new System.Windows.Forms.Label();
			this.TxtBatteryHealth = new System.Windows.Forms.Label();
			this.TxtSatelliteCount = new System.Windows.Forms.Label();
			this.TxtSystemID = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.White;
			this.pictureBox1.Image = global::Diva.Properties.Resources.if_Psyduck_3151565;
			this.pictureBox1.Location = new System.Drawing.Point(5, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// TxtDroneName
			// 
			this.TxtDroneName.AutoSize = true;
			this.TxtDroneName.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtDroneName.ForeColor = System.Drawing.Color.White;
			this.TxtDroneName.Location = new System.Drawing.Point(75, 6);
			this.TxtDroneName.Name = "TxtDroneName";
			this.TxtDroneName.Size = new System.Drawing.Size(45, 15);
			this.TxtDroneName.TabIndex = 1;
			this.TxtDroneName.Text = "APM-1";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::Diva.Properties.Resources.icon_flytime;
			this.pictureBox2.Location = new System.Drawing.Point(75, 49);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(30, 22);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 2;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.Image = global::Diva.Properties.Resources.icon_battery_100;
			this.pictureBox3.Location = new System.Drawing.Point(164, 49);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(30, 22);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox3.TabIndex = 3;
			this.pictureBox3.TabStop = false;
			// 
			// pictureBox4
			// 
			this.pictureBox4.Image = global::Diva.Properties.Resources.icon_signal_100;
			this.pictureBox4.Location = new System.Drawing.Point(253, 49);
			this.pictureBox4.Name = "pictureBox4";
			this.pictureBox4.Size = new System.Drawing.Size(30, 22);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox4.TabIndex = 4;
			this.pictureBox4.TabStop = false;
			// 
			// TxtAssumeTime
			// 
			this.TxtAssumeTime.AutoSize = true;
			this.TxtAssumeTime.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtAssumeTime.ForeColor = System.Drawing.Color.White;
			this.TxtAssumeTime.Location = new System.Drawing.Point(112, 53);
			this.TxtAssumeTime.Name = "TxtAssumeTime";
			this.TxtAssumeTime.Size = new System.Drawing.Size(40, 15);
			this.TxtAssumeTime.TabIndex = 5;
			this.TxtAssumeTime.Text = "00 hr";
			// 
			// TxtBatteryHealth
			// 
			this.TxtBatteryHealth.AutoSize = true;
			this.TxtBatteryHealth.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtBatteryHealth.ForeColor = System.Drawing.Color.White;
			this.TxtBatteryHealth.Location = new System.Drawing.Point(201, 53);
			this.TxtBatteryHealth.Name = "TxtBatteryHealth";
			this.TxtBatteryHealth.Size = new System.Drawing.Size(44, 15);
			this.TxtBatteryHealth.TabIndex = 6;
			this.TxtBatteryHealth.Text = "00 vol";
			// 
			// TxtSatelliteCount
			// 
			this.TxtSatelliteCount.AutoSize = true;
			this.TxtSatelliteCount.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtSatelliteCount.ForeColor = System.Drawing.Color.White;
			this.TxtSatelliteCount.Location = new System.Drawing.Point(290, 53);
			this.TxtSatelliteCount.Name = "TxtSatelliteCount";
			this.TxtSatelliteCount.Size = new System.Drawing.Size(45, 15);
			this.TxtSatelliteCount.TabIndex = 7;
			this.TxtSatelliteCount.Text = "00 cut";
			// 
			// TxtSystemID
			// 
			this.TxtSystemID.AutoSize = true;
			this.TxtSystemID.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtSystemID.ForeColor = System.Drawing.Color.White;
			this.TxtSystemID.Location = new System.Drawing.Point(75, 26);
			this.TxtSystemID.Name = "TxtSystemID";
			this.TxtSystemID.Size = new System.Drawing.Size(23, 15);
			this.TxtSystemID.TabIndex = 8;
			this.TxtSystemID.Text = "00";
			// 
			// button1
			// 
			this.button1.AutoSize = true;
			this.button1.FlatAppearance.BorderSize = 0;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Image = global::Diva.Properties.Resources.icon_zoomin;
			this.button1.Location = new System.Drawing.Point(305, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(30, 30);
			this.button1.TabIndex = 9;
			this.button1.UseVisualStyleBackColor = true;
			// 
			// DroneInfoPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.TxtSystemID);
			this.Controls.Add(this.TxtSatelliteCount);
			this.Controls.Add(this.TxtBatteryHealth);
			this.Controls.Add(this.TxtAssumeTime);
			this.Controls.Add(this.pictureBox4);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.TxtDroneName);
			this.Controls.Add(this.pictureBox1);
			this.Name = "DroneInfoPanel";
			this.Size = new System.Drawing.Size(341, 78);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label TxtDroneName;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label TxtAssumeTime;
		private System.Windows.Forms.Label TxtBatteryHealth;
		private System.Windows.Forms.Label TxtSatelliteCount;
		private System.Windows.Forms.Label TxtSystemID;
		private System.Windows.Forms.Button button1;
	}
}
