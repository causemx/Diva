namespace Diva.Controls
{
	partial class DroneInfo
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
            this.PBDroneView = new System.Windows.Forms.PictureBox();
            this.TxtDroneName = new System.Windows.Forms.Label();
            this.IconFlightTime = new System.Windows.Forms.PictureBox();
            this.IconBattery = new System.Windows.Forms.PictureBox();
            this.IconSignalStrength = new System.Windows.Forms.PictureBox();
            this.TxtEstimatedTime = new System.Windows.Forms.Label();
            this.TxtBatteryHealth = new System.Windows.Forms.Label();
            this.TxtSatelliteCount = new System.Windows.Forms.Label();
            this.TxtSystemID = new System.Windows.Forms.Label();
            this.BtnExpand = new System.Windows.Forms.Button();
            this.iconRemoteControl = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.armStatusLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconFlightTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconSignalStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRemoteControl)).BeginInit();
            this.SuspendLayout();
            // 
            // PBDroneView
            // 
            this.PBDroneView.BackColor = System.Drawing.Color.Transparent;
            this.PBDroneView.Image = global::Diva.Properties.Resources.VTOL;
            this.PBDroneView.Location = new System.Drawing.Point(10, 5);
            this.PBDroneView.Name = "PBDroneView";
            this.PBDroneView.Size = new System.Drawing.Size(50, 50);
            this.PBDroneView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PBDroneView.TabIndex = 0;
            this.PBDroneView.TabStop = false;
            // 
            // TxtDroneName
            // 
            this.TxtDroneName.AutoSize = true;
            this.TxtDroneName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.TxtDroneName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TxtDroneName.Location = new System.Drawing.Point(66, 22);
            this.TxtDroneName.Name = "TxtDroneName";
            this.TxtDroneName.Size = new System.Drawing.Size(49, 15);
            this.TxtDroneName.TabIndex = 1;
            this.TxtDroneName.Text = "APM-1";
            // 
            // IconFlightTime
            // 
            this.IconFlightTime.Image = global::Diva.Properties.Resources.icon_burger_24;
            this.IconFlightTime.Location = new System.Drawing.Point(147, 14);
            this.IconFlightTime.Name = "IconFlightTime";
            this.IconFlightTime.Size = new System.Drawing.Size(24, 24);
            this.IconFlightTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconFlightTime.TabIndex = 2;
            this.IconFlightTime.TabStop = false;
            // 
            // IconBattery
            // 
            this.IconBattery.Image = global::Diva.Properties.Resources.icon_burger_24;
            this.IconBattery.Location = new System.Drawing.Point(211, 14);
            this.IconBattery.Name = "IconBattery";
            this.IconBattery.Size = new System.Drawing.Size(24, 24);
            this.IconBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconBattery.TabIndex = 3;
            this.IconBattery.TabStop = false;
            // 
            // IconSignalStrength
            // 
            this.IconSignalStrength.Image = global::Diva.Properties.Resources.icon_burger_24;
            this.IconSignalStrength.Location = new System.Drawing.Point(281, 14);
            this.IconSignalStrength.Name = "IconSignalStrength";
            this.IconSignalStrength.Size = new System.Drawing.Size(24, 24);
            this.IconSignalStrength.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconSignalStrength.TabIndex = 4;
            this.IconSignalStrength.TabStop = false;
            // 
            // TxtEstimatedTime
            // 
            this.TxtEstimatedTime.AutoSize = true;
            this.TxtEstimatedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtEstimatedTime.ForeColor = System.Drawing.Color.White;
            this.TxtEstimatedTime.Location = new System.Drawing.Point(141, 40);
            this.TxtEstimatedTime.Name = "TxtEstimatedTime";
            this.TxtEstimatedTime.Size = new System.Drawing.Size(35, 15);
            this.TxtEstimatedTime.TabIndex = 5;
            this.TxtEstimatedTime.Text = "0.0m";
            // 
            // TxtBatteryHealth
            // 
            this.TxtBatteryHealth.AutoSize = true;
            this.TxtBatteryHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtBatteryHealth.ForeColor = System.Drawing.Color.White;
            this.TxtBatteryHealth.Location = new System.Drawing.Point(205, 40);
            this.TxtBatteryHealth.Name = "TxtBatteryHealth";
            this.TxtBatteryHealth.Size = new System.Drawing.Size(39, 15);
            this.TxtBatteryHealth.TabIndex = 6;
            this.TxtBatteryHealth.Text = "00 vol";
            this.TxtBatteryHealth.Click += new System.EventHandler(this.DetailValue_Click);
            // 
            // TxtSatelliteCount
            // 
            this.TxtSatelliteCount.AutoSize = true;
            this.TxtSatelliteCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtSatelliteCount.ForeColor = System.Drawing.Color.White;
            this.TxtSatelliteCount.Location = new System.Drawing.Point(273, 40);
            this.TxtSatelliteCount.Name = "TxtSatelliteCount";
            this.TxtSatelliteCount.Size = new System.Drawing.Size(40, 15);
            this.TxtSatelliteCount.TabIndex = 7;
            this.TxtSatelliteCount.Text = "00 cut";
            // 
            // TxtSystemID
            // 
            this.TxtSystemID.AutoSize = true;
            this.TxtSystemID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtSystemID.ForeColor = System.Drawing.Color.White;
            this.TxtSystemID.Location = new System.Drawing.Point(78, 12);
            this.TxtSystemID.Name = "TxtSystemID";
            this.TxtSystemID.Size = new System.Drawing.Size(21, 15);
            this.TxtSystemID.TabIndex = 8;
            this.TxtSystemID.Text = "00";
            this.TxtSystemID.Visible = false;
            // 
            // BtnExpand
            // 
            this.BtnExpand.AutoSize = true;
            this.BtnExpand.FlatAppearance.BorderSize = 0;
            this.BtnExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExpand.Image = global::Diva.Properties.Resources.icon_btn_expand;
            this.BtnExpand.Location = new System.Drawing.Point(562, 17);
            this.BtnExpand.Name = "BtnExpand";
            this.BtnExpand.Size = new System.Drawing.Size(36, 30);
            this.BtnExpand.TabIndex = 9;
            this.BtnExpand.UseVisualStyleBackColor = true;
            this.BtnExpand.Click += new System.EventHandler(this.BtnExpand_Click);
            // 
            // iconRemoteControl
            // 
            this.iconRemoteControl.Image = global::Diva.Properties.Resources.icon_burger_24;
            this.iconRemoteControl.Location = new System.Drawing.Point(349, 14);
            this.iconRemoteControl.Name = "iconRemoteControl";
            this.iconRemoteControl.Size = new System.Drawing.Size(24, 24);
            this.iconRemoteControl.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconRemoteControl.TabIndex = 10;
            this.iconRemoteControl.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(342, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "00 cut";
            // 
            // armStatusLabel
            // 
            this.armStatusLabel.AutoSize = true;
            this.armStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.armStatusLabel.ForeColor = System.Drawing.Color.White;
            this.armStatusLabel.Location = new System.Drawing.Point(456, 23);
            this.armStatusLabel.Name = "armStatusLabel";
            this.armStatusLabel.Size = new System.Drawing.Size(39, 15);
            this.armStatusLabel.TabIndex = 12;
            this.armStatusLabel.Text = "status";
            // 
            // DroneInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.Controls.Add(this.armStatusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iconRemoteControl);
            this.Controls.Add(this.BtnExpand);
            this.Controls.Add(this.TxtSystemID);
            this.Controls.Add(this.TxtSatelliteCount);
            this.Controls.Add(this.TxtBatteryHealth);
            this.Controls.Add(this.TxtEstimatedTime);
            this.Controls.Add(this.IconSignalStrength);
            this.Controls.Add(this.IconBattery);
            this.Controls.Add(this.IconFlightTime);
            this.Controls.Add(this.TxtDroneName);
            this.Controls.Add(this.PBDroneView);
            this.Name = "DroneInfo";
            this.Size = new System.Drawing.Size(601, 60);
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconFlightTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconSignalStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRemoteControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PBDroneView;
		private System.Windows.Forms.Label TxtDroneName;
		private System.Windows.Forms.PictureBox IconFlightTime;
		private System.Windows.Forms.PictureBox IconBattery;
		private System.Windows.Forms.PictureBox IconSignalStrength;
		private System.Windows.Forms.Label TxtEstimatedTime;
		private System.Windows.Forms.Label TxtBatteryHealth;
		private System.Windows.Forms.Label TxtSatelliteCount;
		private System.Windows.Forms.Label TxtSystemID;
		private System.Windows.Forms.Button BtnExpand;
        private System.Windows.Forms.PictureBox iconRemoteControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label armStatusLabel;
    }
}
