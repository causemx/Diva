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
            this.TxtAssumeTime = new System.Windows.Forms.Label();
            this.TxtBatteryHealth = new System.Windows.Forms.Label();
            this.TxtSatelliteCount = new System.Windows.Forms.Label();
            this.TxtSystemID = new System.Windows.Forms.Label();
            this.PanelEnergyConsumptionInfo = new System.Windows.Forms.Panel();
            this.IconEnergyConsumption = new System.Windows.Forms.PictureBox();
            this.LabelEstimatedEnergyConsumptionText = new System.Windows.Forms.Label();
            this.ChkBtnPowerModel = new System.Windows.Forms.CheckBox();
            this.ChkBtnTelemetryInfo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconFlightTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconSignalStrength)).BeginInit();
            this.PanelEnergyConsumptionInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconEnergyConsumption)).BeginInit();
            this.SuspendLayout();
            // 
            // PBDroneView
            // 
            this.PBDroneView.BackColor = System.Drawing.Color.Transparent;
            this.PBDroneView.Image = global::Diva.Properties.Resources.icon_drone_4axis;
            this.PBDroneView.Location = new System.Drawing.Point(8, 10);
            this.PBDroneView.Name = "PBDroneView";
            this.PBDroneView.Size = new System.Drawing.Size(58, 58);
            this.PBDroneView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PBDroneView.TabIndex = 0;
            this.PBDroneView.TabStop = false;
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
            // IconFlightTime
            // 
            this.IconFlightTime.Image = global::Diva.Properties.Resources.icon_flytime;
            this.IconFlightTime.Location = new System.Drawing.Point(75, 49);
            this.IconFlightTime.Name = "IconFlightTime";
            this.IconFlightTime.Size = new System.Drawing.Size(30, 22);
            this.IconFlightTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconFlightTime.TabIndex = 2;
            this.IconFlightTime.TabStop = false;
            // 
            // IconBattery
            // 
            this.IconBattery.Image = global::Diva.Properties.Resources.icon_battery_3;
            this.IconBattery.Location = new System.Drawing.Point(158, 49);
            this.IconBattery.Name = "IconBattery";
            this.IconBattery.Size = new System.Drawing.Size(30, 22);
            this.IconBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconBattery.TabIndex = 3;
            this.IconBattery.TabStop = false;
            // 
            // IconSignalStrength
            // 
            this.IconSignalStrength.Image = global::Diva.Properties.Resources.capacity_signal;
            this.IconSignalStrength.Location = new System.Drawing.Point(253, 49);
            this.IconSignalStrength.Name = "IconSignalStrength";
            this.IconSignalStrength.Size = new System.Drawing.Size(30, 22);
            this.IconSignalStrength.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconSignalStrength.TabIndex = 4;
            this.IconSignalStrength.TabStop = false;
            // 
            // TxtAssumeTime
            // 
            this.TxtAssumeTime.AutoSize = true;
            this.TxtAssumeTime.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtAssumeTime.ForeColor = System.Drawing.Color.White;
            this.TxtAssumeTime.Location = new System.Drawing.Point(111, 53);
            this.TxtAssumeTime.Name = "TxtAssumeTime";
            this.TxtAssumeTime.Size = new System.Drawing.Size(39, 15);
            this.TxtAssumeTime.TabIndex = 5;
            this.TxtAssumeTime.Text = "0.0m";
            // 
            // TxtBatteryHealth
            // 
            this.TxtBatteryHealth.AutoSize = true;
            this.TxtBatteryHealth.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBatteryHealth.ForeColor = System.Drawing.Color.White;
            this.TxtBatteryHealth.Location = new System.Drawing.Point(195, 53);
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
            // PanelEnergyConsumptionInfo
            // 
            this.PanelEnergyConsumptionInfo.Controls.Add(this.IconEnergyConsumption);
            this.PanelEnergyConsumptionInfo.Controls.Add(this.LabelEstimatedEnergyConsumptionText);
            this.PanelEnergyConsumptionInfo.Location = new System.Drawing.Point(0, 72);
            this.PanelEnergyConsumptionInfo.Name = "PanelEnergyConsumptionInfo";
            this.PanelEnergyConsumptionInfo.Size = new System.Drawing.Size(341, 28);
            this.PanelEnergyConsumptionInfo.TabIndex = 11;
            this.PanelEnergyConsumptionInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TriggerEstimatedEnergyRecalculation);
            // 
            // IconEnergyConsumption
            // 
            this.IconEnergyConsumption.Image = global::Diva.Properties.Resources.icon_loading;
            this.IconEnergyConsumption.InitialImage = global::Diva.Properties.Resources.icon_loading;
            this.IconEnergyConsumption.Location = new System.Drawing.Point(78, 3);
            this.IconEnergyConsumption.Name = "IconEnergyConsumption";
            this.IconEnergyConsumption.Size = new System.Drawing.Size(20, 20);
            this.IconEnergyConsumption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.IconEnergyConsumption.TabIndex = 12;
            this.IconEnergyConsumption.TabStop = false;
            this.IconEnergyConsumption.DoubleClick += new System.EventHandler(this.TriggerEstimatedEnergyRecalculation);
            // 
            // LabelEstimatedEnergyConsumptionText
            // 
            this.LabelEstimatedEnergyConsumptionText.AutoSize = true;
            this.LabelEstimatedEnergyConsumptionText.Font = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelEstimatedEnergyConsumptionText.ForeColor = System.Drawing.Color.White;
            this.LabelEstimatedEnergyConsumptionText.Location = new System.Drawing.Point(111, 5);
            this.LabelEstimatedEnergyConsumptionText.Name = "LabelEstimatedEnergyConsumptionText";
            this.LabelEstimatedEnergyConsumptionText.Size = new System.Drawing.Size(0, 15);
            this.LabelEstimatedEnergyConsumptionText.TabIndex = 13;
            this.LabelEstimatedEnergyConsumptionText.DoubleClick += new System.EventHandler(this.TriggerEstimatedEnergyRecalculation);
            // 
            // ChkBtnPowerModel
            // 
            this.ChkBtnPowerModel.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkBtnPowerModel.AutoCheck = false;
            this.ChkBtnPowerModel.AutoSize = true;
            this.ChkBtnPowerModel.FlatAppearance.BorderSize = 0;
            this.ChkBtnPowerModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkBtnPowerModel.Image = global::Diva.Properties.Resources.icon_lighting_24;
            this.ChkBtnPowerModel.Location = new System.Drawing.Point(270, 3);
            this.ChkBtnPowerModel.Name = "ChkBtnPowerModel";
            this.ChkBtnPowerModel.Size = new System.Drawing.Size(30, 30);
            this.ChkBtnPowerModel.TabIndex = 12;
            this.ChkBtnPowerModel.UseVisualStyleBackColor = true;
            this.ChkBtnPowerModel.Click += new System.EventHandler(this.BtnPowerModel_Click);
            // 
            // ChkBtnTelemetryInfo
            // 
            this.ChkBtnTelemetryInfo.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkBtnTelemetryInfo.AutoCheck = false;
            this.ChkBtnTelemetryInfo.AutoSize = true;
            this.ChkBtnTelemetryInfo.FlatAppearance.BorderSize = 0;
            this.ChkBtnTelemetryInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkBtnTelemetryInfo.Image = global::Diva.Properties.Resources.icon_zoomin1;
            this.ChkBtnTelemetryInfo.Location = new System.Drawing.Point(303, 3);
            this.ChkBtnTelemetryInfo.MaximumSize = new System.Drawing.Size(30, 30);
            this.ChkBtnTelemetryInfo.MinimumSize = new System.Drawing.Size(30, 30);
            this.ChkBtnTelemetryInfo.Name = "ChkBtnTelemetryInfo";
            this.ChkBtnTelemetryInfo.Size = new System.Drawing.Size(30, 30);
            this.ChkBtnTelemetryInfo.TabIndex = 13;
            this.ChkBtnTelemetryInfo.UseVisualStyleBackColor = true;
            this.ChkBtnTelemetryInfo.Click += new System.EventHandler(this.BtnTelemetryInfo_Click);
            // 
            // DroneInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.ChkBtnTelemetryInfo);
            this.Controls.Add(this.ChkBtnPowerModel);
            this.Controls.Add(this.PanelEnergyConsumptionInfo);
            this.Controls.Add(this.TxtSystemID);
            this.Controls.Add(this.TxtSatelliteCount);
            this.Controls.Add(this.TxtBatteryHealth);
            this.Controls.Add(this.TxtAssumeTime);
            this.Controls.Add(this.IconSignalStrength);
            this.Controls.Add(this.IconBattery);
            this.Controls.Add(this.IconFlightTime);
            this.Controls.Add(this.TxtDroneName);
            this.Controls.Add(this.PBDroneView);
            this.Name = "DroneInfo";
            this.Size = new System.Drawing.Size(344, 72);
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconFlightTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconSignalStrength)).EndInit();
            this.PanelEnergyConsumptionInfo.ResumeLayout(false);
            this.PanelEnergyConsumptionInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IconEnergyConsumption)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PBDroneView;
		private System.Windows.Forms.Label TxtDroneName;
		private System.Windows.Forms.PictureBox IconFlightTime;
		private System.Windows.Forms.PictureBox IconBattery;
		private System.Windows.Forms.PictureBox IconSignalStrength;
		private System.Windows.Forms.Label TxtAssumeTime;
		private System.Windows.Forms.Label TxtBatteryHealth;
		private System.Windows.Forms.Label TxtSatelliteCount;
		private System.Windows.Forms.Label TxtSystemID;
		private System.Windows.Forms.Panel PanelEnergyConsumptionInfo;
        private System.Windows.Forms.PictureBox IconEnergyConsumption;
        private System.Windows.Forms.Label LabelEstimatedEnergyConsumptionText;
        private System.Windows.Forms.CheckBox ChkBtnPowerModel;
        private System.Windows.Forms.CheckBox ChkBtnTelemetryInfo;
    }
}
