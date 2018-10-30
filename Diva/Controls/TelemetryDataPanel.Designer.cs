namespace Diva.Controls
{
	partial class TelemetryDataPanel
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
			this.GBTitle = new Diva.Controls.MyGroupBox();
			this.GBVerticalSpeed = new Diva.Controls.MyGroupBox();
			this.TxtVerticalSpeed = new System.Windows.Forms.Label();
			this.GBGroundSpeed = new Diva.Controls.MyGroupBox();
			this.TxtGroundSpeed = new System.Windows.Forms.Label();
			this.GBGeoFence = new Diva.Controls.MyGroupBox();
			this.LBL_GeoFenceChecker = new System.Windows.Forms.Label();
			this.GBAltitude = new Diva.Controls.MyGroupBox();
			this.TxtAltitude = new System.Windows.Forms.Label();
			this.GBBattery = new Diva.Controls.MyGroupBox();
			this.LBL_LowBatteryChecker = new System.Windows.Forms.Label();
			this.GBTitle.SuspendLayout();
			this.GBVerticalSpeed.SuspendLayout();
			this.GBGroundSpeed.SuspendLayout();
			this.GBGeoFence.SuspendLayout();
			this.GBAltitude.SuspendLayout();
			this.GBBattery.SuspendLayout();
			this.SuspendLayout();
			// 
			// GBTitle
			// 
			this.GBTitle.BackColor = System.Drawing.Color.Transparent;
			this.GBTitle.Controls.Add(this.GBVerticalSpeed);
			this.GBTitle.Controls.Add(this.GBGroundSpeed);
			this.GBTitle.Controls.Add(this.GBGeoFence);
			this.GBTitle.Controls.Add(this.GBAltitude);
			this.GBTitle.Controls.Add(this.GBBattery);
			this.GBTitle.Location = new System.Drawing.Point(3, 3);
			this.GBTitle.Name = "GBTitle";
			this.GBTitle.Radious = 1;
			this.GBTitle.Size = new System.Drawing.Size(335, 235);
			this.GBTitle.TabIndex = 0;
			this.GBTitle.TabStop = false;
			this.GBTitle.Text = "Telemetry Data : APM-01";
			this.GBTitle.TitleBackColor = System.Drawing.Color.Gray;
			this.GBTitle.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBTitle.TitleForeColor = System.Drawing.Color.White;
			this.GBTitle.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBTitle.TitlePadding = 3;
			// 
			// GBVerticalSpeed
			// 
			this.GBVerticalSpeed.BackColor = System.Drawing.Color.Transparent;
			this.GBVerticalSpeed.Controls.Add(this.TxtVerticalSpeed);
			this.GBVerticalSpeed.Location = new System.Drawing.Point(169, 128);
			this.GBVerticalSpeed.Name = "GBVerticalSpeed";
			this.GBVerticalSpeed.Radious = 1;
			this.GBVerticalSpeed.Size = new System.Drawing.Size(163, 100);
			this.GBVerticalSpeed.TabIndex = 2;
			this.GBVerticalSpeed.TabStop = false;
			this.GBVerticalSpeed.Text = "Vertical Speed";
			this.GBVerticalSpeed.TitleBackColor = System.Drawing.Color.Gray;
			this.GBVerticalSpeed.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBVerticalSpeed.TitleForeColor = System.Drawing.Color.White;
			this.GBVerticalSpeed.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBVerticalSpeed.TitlePadding = 3;
			// 
			// TxtVerticalSpeed
			// 
			this.TxtVerticalSpeed.AutoSize = true;
			this.TxtVerticalSpeed.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtVerticalSpeed.ForeColor = System.Drawing.Color.White;
			this.TxtVerticalSpeed.Location = new System.Drawing.Point(69, 49);
			this.TxtVerticalSpeed.Name = "TxtVerticalSpeed";
			this.TxtVerticalSpeed.Size = new System.Drawing.Size(42, 18);
			this.TxtVerticalSpeed.TabIndex = 6;
			this.TxtVerticalSpeed.Text = "0.00";
			// 
			// GBGroundSpeed
			// 
			this.GBGroundSpeed.BackColor = System.Drawing.Color.Transparent;
			this.GBGroundSpeed.Controls.Add(this.TxtGroundSpeed);
			this.GBGroundSpeed.Location = new System.Drawing.Point(3, 128);
			this.GBGroundSpeed.Name = "GBGroundSpeed";
			this.GBGroundSpeed.Radious = 1;
			this.GBGroundSpeed.Size = new System.Drawing.Size(163, 100);
			this.GBGroundSpeed.TabIndex = 1;
			this.GBGroundSpeed.TabStop = false;
			this.GBGroundSpeed.Text = "Ground Speed";
			this.GBGroundSpeed.TitleBackColor = System.Drawing.Color.Gray;
			this.GBGroundSpeed.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBGroundSpeed.TitleForeColor = System.Drawing.Color.White;
			this.GBGroundSpeed.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBGroundSpeed.TitlePadding = 3;
			// 
			// TxtGroundSpeed
			// 
			this.TxtGroundSpeed.AutoSize = true;
			this.TxtGroundSpeed.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtGroundSpeed.ForeColor = System.Drawing.Color.White;
			this.TxtGroundSpeed.Location = new System.Drawing.Point(67, 49);
			this.TxtGroundSpeed.Name = "TxtGroundSpeed";
			this.TxtGroundSpeed.Size = new System.Drawing.Size(42, 18);
			this.TxtGroundSpeed.TabIndex = 5;
			this.TxtGroundSpeed.Text = "0.00";
			// 
			// GBGeoFence
			// 
			this.GBGeoFence.BackColor = System.Drawing.Color.Transparent;
			this.GBGeoFence.Controls.Add(this.LBL_GeoFenceChecker);
			this.GBGeoFence.Location = new System.Drawing.Point(223, 24);
			this.GBGeoFence.Name = "GBGeoFence";
			this.GBGeoFence.Radious = 1;
			this.GBGeoFence.Size = new System.Drawing.Size(109, 100);
			this.GBGeoFence.TabIndex = 2;
			this.GBGeoFence.TabStop = false;
			this.GBGeoFence.Text = "GeoFence";
			this.GBGeoFence.TitleBackColor = System.Drawing.Color.Gray;
			this.GBGeoFence.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBGeoFence.TitleForeColor = System.Drawing.Color.White;
			this.GBGeoFence.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBGeoFence.TitlePadding = 3;
			// 
			// LBL_GeoFenceChecker
			// 
			this.LBL_GeoFenceChecker.AutoSize = true;
			this.LBL_GeoFenceChecker.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LBL_GeoFenceChecker.ForeColor = System.Drawing.Color.White;
			this.LBL_GeoFenceChecker.Location = new System.Drawing.Point(35, 52);
			this.LBL_GeoFenceChecker.Name = "LBL_GeoFenceChecker";
			this.LBL_GeoFenceChecker.Size = new System.Drawing.Size(39, 18);
			this.LBL_GeoFenceChecker.TabIndex = 4;
			this.LBL_GeoFenceChecker.Text = "N/A";
			// 
			// GBAltitude
			// 
			this.GBAltitude.BackColor = System.Drawing.Color.Transparent;
			this.GBAltitude.Controls.Add(this.TxtAltitude);
			this.GBAltitude.Location = new System.Drawing.Point(113, 24);
			this.GBAltitude.Name = "GBAltitude";
			this.GBAltitude.Radious = 1;
			this.GBAltitude.Size = new System.Drawing.Size(109, 100);
			this.GBAltitude.TabIndex = 1;
			this.GBAltitude.TabStop = false;
			this.GBAltitude.Text = "Altitude";
			this.GBAltitude.TitleBackColor = System.Drawing.Color.Gray;
			this.GBAltitude.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBAltitude.TitleForeColor = System.Drawing.Color.White;
			this.GBAltitude.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBAltitude.TitlePadding = 3;
			// 
			// TxtAltitude
			// 
			this.TxtAltitude.AutoSize = true;
			this.TxtAltitude.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtAltitude.ForeColor = System.Drawing.Color.White;
			this.TxtAltitude.Location = new System.Drawing.Point(36, 52);
			this.TxtAltitude.Name = "TxtAltitude";
			this.TxtAltitude.Size = new System.Drawing.Size(36, 18);
			this.TxtAltitude.TabIndex = 3;
			this.TxtAltitude.Text = "100";
			// 
			// GBBattery
			// 
			this.GBBattery.BackColor = System.Drawing.Color.Transparent;
			this.GBBattery.Controls.Add(this.LBL_LowBatteryChecker);
			this.GBBattery.Location = new System.Drawing.Point(3, 24);
			this.GBBattery.Name = "GBBattery";
			this.GBBattery.Radious = 1;
			this.GBBattery.Size = new System.Drawing.Size(109, 100);
			this.GBBattery.TabIndex = 0;
			this.GBBattery.TabStop = false;
			this.GBBattery.Text = " Battery";
			this.GBBattery.TitleBackColor = System.Drawing.Color.Gray;
			this.GBBattery.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.GBBattery.TitleForeColor = System.Drawing.Color.White;
			this.GBBattery.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.GBBattery.TitlePadding = 3;
			// 
			// LBL_LowBatteryChecker
			// 
			this.LBL_LowBatteryChecker.AutoSize = true;
			this.LBL_LowBatteryChecker.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LBL_LowBatteryChecker.ForeColor = System.Drawing.Color.White;
			this.LBL_LowBatteryChecker.Location = new System.Drawing.Point(31, 52);
			this.LBL_LowBatteryChecker.Name = "LBL_LowBatteryChecker";
			this.LBL_LowBatteryChecker.Size = new System.Drawing.Size(39, 18);
			this.LBL_LowBatteryChecker.TabIndex = 1;
			this.LBL_LowBatteryChecker.Text = "N/A";
			// 
			// TelemetryDataPanel
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.GBTitle);
			this.Name = "TelemetryDataPanel";
			this.Size = new System.Drawing.Size(341, 242);
			this.GBTitle.ResumeLayout(false);
			this.GBVerticalSpeed.ResumeLayout(false);
			this.GBVerticalSpeed.PerformLayout();
			this.GBGroundSpeed.ResumeLayout(false);
			this.GBGroundSpeed.PerformLayout();
			this.GBGeoFence.ResumeLayout(false);
			this.GBGeoFence.PerformLayout();
			this.GBAltitude.ResumeLayout(false);
			this.GBAltitude.PerformLayout();
			this.GBBattery.ResumeLayout(false);
			this.GBBattery.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private MyGroupBox GBTitle;
		private MyGroupBox GBGeoFence;
		private MyGroupBox GBAltitude;
		private MyGroupBox GBBattery;
		private MyGroupBox GBGroundSpeed;
		private MyGroupBox GBVerticalSpeed;
		private System.Windows.Forms.Label TxtAltitude;
		private System.Windows.Forms.Label LBL_LowBatteryChecker;
		private System.Windows.Forms.Label LBL_GeoFenceChecker;
		private System.Windows.Forms.Label TxtVerticalSpeed;
		private System.Windows.Forms.Label TxtGroundSpeed;
	}
}
