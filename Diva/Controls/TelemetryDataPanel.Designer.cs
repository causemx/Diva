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
			this.myGroupBox6 = new Diva.Controls.MyGroupBox();
			this.myGroupBox5 = new Diva.Controls.MyGroupBox();
			this.myGroupBox4 = new Diva.Controls.MyGroupBox();
			this.myGroupBox3 = new Diva.Controls.MyGroupBox();
			this.myGroupBox2 = new Diva.Controls.MyGroupBox();
			this.TxtBatteryVoltage = new System.Windows.Forms.Label();
			this.TxtBatteryPercentage = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtAltitude = new System.Windows.Forms.Label();
			this.TxtGeoFenceEnable = new System.Windows.Forms.Label();
			this.TxtGroundSpeed = new System.Windows.Forms.Label();
			this.TxtVerticalSpeed = new System.Windows.Forms.Label();
			this.GBTitle.SuspendLayout();
			this.myGroupBox6.SuspendLayout();
			this.myGroupBox5.SuspendLayout();
			this.myGroupBox4.SuspendLayout();
			this.myGroupBox3.SuspendLayout();
			this.myGroupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// GBTitle
			// 
			this.GBTitle.BackColor = System.Drawing.Color.Transparent;
			this.GBTitle.Controls.Add(this.myGroupBox6);
			this.GBTitle.Controls.Add(this.myGroupBox5);
			this.GBTitle.Controls.Add(this.myGroupBox4);
			this.GBTitle.Controls.Add(this.myGroupBox3);
			this.GBTitle.Controls.Add(this.myGroupBox2);
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
			// myGroupBox6
			// 
			this.myGroupBox6.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox6.Controls.Add(this.TxtVerticalSpeed);
			this.myGroupBox6.Location = new System.Drawing.Point(169, 128);
			this.myGroupBox6.Name = "myGroupBox6";
			this.myGroupBox6.Radious = 1;
			this.myGroupBox6.Size = new System.Drawing.Size(163, 100);
			this.myGroupBox6.TabIndex = 2;
			this.myGroupBox6.TabStop = false;
			this.myGroupBox6.Text = "Vertical Speed";
			this.myGroupBox6.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox6.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.myGroupBox6.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox6.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox6.TitlePadding = 3;
			// 
			// myGroupBox5
			// 
			this.myGroupBox5.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox5.Controls.Add(this.TxtGroundSpeed);
			this.myGroupBox5.Location = new System.Drawing.Point(3, 128);
			this.myGroupBox5.Name = "myGroupBox5";
			this.myGroupBox5.Radious = 1;
			this.myGroupBox5.Size = new System.Drawing.Size(163, 100);
			this.myGroupBox5.TabIndex = 1;
			this.myGroupBox5.TabStop = false;
			this.myGroupBox5.Text = "Ground Speed";
			this.myGroupBox5.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox5.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.myGroupBox5.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox5.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox5.TitlePadding = 3;
			// 
			// myGroupBox4
			// 
			this.myGroupBox4.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox4.Controls.Add(this.TxtGeoFenceEnable);
			this.myGroupBox4.Location = new System.Drawing.Point(223, 24);
			this.myGroupBox4.Name = "myGroupBox4";
			this.myGroupBox4.Radious = 1;
			this.myGroupBox4.Size = new System.Drawing.Size(109, 100);
			this.myGroupBox4.TabIndex = 2;
			this.myGroupBox4.TabStop = false;
			this.myGroupBox4.Text = "GeoFence";
			this.myGroupBox4.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox4.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.myGroupBox4.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox4.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox4.TitlePadding = 3;
			// 
			// myGroupBox3
			// 
			this.myGroupBox3.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox3.Controls.Add(this.TxtAltitude);
			this.myGroupBox3.Controls.Add(this.label3);
			this.myGroupBox3.Location = new System.Drawing.Point(113, 24);
			this.myGroupBox3.Name = "myGroupBox3";
			this.myGroupBox3.Radious = 1;
			this.myGroupBox3.Size = new System.Drawing.Size(109, 100);
			this.myGroupBox3.TabIndex = 1;
			this.myGroupBox3.TabStop = false;
			this.myGroupBox3.Text = "Altitude";
			this.myGroupBox3.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox3.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.myGroupBox3.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox3.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox3.TitlePadding = 3;
			// 
			// myGroupBox2
			// 
			this.myGroupBox2.BackColor = System.Drawing.Color.Transparent;
			this.myGroupBox2.Controls.Add(this.TxtBatteryPercentage);
			this.myGroupBox2.Controls.Add(this.TxtBatteryVoltage);
			this.myGroupBox2.Location = new System.Drawing.Point(3, 24);
			this.myGroupBox2.Name = "myGroupBox2";
			this.myGroupBox2.Radious = 1;
			this.myGroupBox2.Size = new System.Drawing.Size(109, 100);
			this.myGroupBox2.TabIndex = 0;
			this.myGroupBox2.TabStop = false;
			this.myGroupBox2.Text = " Battery";
			this.myGroupBox2.TitleBackColor = System.Drawing.Color.Gray;
			this.myGroupBox2.TitleFont = new System.Drawing.Font("Georgia", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.myGroupBox2.TitleForeColor = System.Drawing.Color.White;
			this.myGroupBox2.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
			this.myGroupBox2.TitlePadding = 3;
			// 
			// TxtBatteryVoltage
			// 
			this.TxtBatteryVoltage.AutoSize = true;
			this.TxtBatteryVoltage.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtBatteryVoltage.ForeColor = System.Drawing.Color.White;
			this.TxtBatteryVoltage.Location = new System.Drawing.Point(32, 36);
			this.TxtBatteryVoltage.Name = "TxtBatteryVoltage";
			this.TxtBatteryVoltage.Size = new System.Drawing.Size(53, 18);
			this.TxtBatteryVoltage.TabIndex = 0;
			this.TxtBatteryVoltage.Text = "12.5 V";
			// 
			// TxtBatteryPercentage
			// 
			this.TxtBatteryPercentage.AutoSize = true;
			this.TxtBatteryPercentage.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtBatteryPercentage.ForeColor = System.Drawing.Color.White;
			this.TxtBatteryPercentage.Location = new System.Drawing.Point(32, 62);
			this.TxtBatteryPercentage.Name = "TxtBatteryPercentage";
			this.TxtBatteryPercentage.Size = new System.Drawing.Size(49, 18);
			this.TxtBatteryPercentage.TabIndex = 1;
			this.TxtBatteryPercentage.Text = "100%";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.White;
			this.label3.Location = new System.Drawing.Point(36, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "AGL";
			// 
			// TxtAltitude
			// 
			this.TxtAltitude.AutoSize = true;
			this.TxtAltitude.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtAltitude.ForeColor = System.Drawing.Color.White;
			this.TxtAltitude.Location = new System.Drawing.Point(36, 62);
			this.TxtAltitude.Name = "TxtAltitude";
			this.TxtAltitude.Size = new System.Drawing.Size(36, 18);
			this.TxtAltitude.TabIndex = 3;
			this.TxtAltitude.Text = "100";
			// 
			// TxtGeoFenceEnable
			// 
			this.TxtGeoFenceEnable.AutoSize = true;
			this.TxtGeoFenceEnable.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtGeoFenceEnable.ForeColor = System.Drawing.Color.White;
			this.TxtGeoFenceEnable.Location = new System.Drawing.Point(35, 52);
			this.TxtGeoFenceEnable.Name = "TxtGeoFenceEnable";
			this.TxtGeoFenceEnable.Size = new System.Drawing.Size(39, 18);
			this.TxtGeoFenceEnable.TabIndex = 4;
			this.TxtGeoFenceEnable.Text = "N/A";
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
			// TelemetryDataPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.Controls.Add(this.GBTitle);
			this.Name = "TelemetryDataPanel";
			this.Size = new System.Drawing.Size(341, 242);
			this.GBTitle.ResumeLayout(false);
			this.myGroupBox6.ResumeLayout(false);
			this.myGroupBox6.PerformLayout();
			this.myGroupBox5.ResumeLayout(false);
			this.myGroupBox5.PerformLayout();
			this.myGroupBox4.ResumeLayout(false);
			this.myGroupBox4.PerformLayout();
			this.myGroupBox3.ResumeLayout(false);
			this.myGroupBox3.PerformLayout();
			this.myGroupBox2.ResumeLayout(false);
			this.myGroupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private MyGroupBox GBTitle;
		private MyGroupBox myGroupBox4;
		private MyGroupBox myGroupBox3;
		private MyGroupBox myGroupBox2;
		private MyGroupBox myGroupBox5;
		private MyGroupBox myGroupBox6;
		private System.Windows.Forms.Label TxtAltitude;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label TxtBatteryPercentage;
		private System.Windows.Forms.Label TxtBatteryVoltage;
		private System.Windows.Forms.Label TxtGeoFenceEnable;
		private System.Windows.Forms.Label TxtVerticalSpeed;
		private System.Windows.Forms.Label TxtGroundSpeed;
	}
}
