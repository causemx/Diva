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
            this.IconTime = new System.Windows.Forms.PictureBox();
            this.IconBattery = new System.Windows.Forms.PictureBox();
            this.IconGPS = new System.Windows.Forms.PictureBox();
            this.TxtEstimatedTime = new System.Windows.Forms.Label();
            this.TxtBatteryHealth = new System.Windows.Forms.Label();
            this.TxtSatelliteCount = new System.Windows.Forms.Label();
            this.TxtSystemID = new System.Windows.Forms.Label();
            this.BtnExpand = new System.Windows.Forms.Button();
            this.iconRC = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelIsArm = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconGPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRC)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.TxtDroneName.Location = new System.Drawing.Point(77, 21);
            this.TxtDroneName.Name = "TxtDroneName";
            this.TxtDroneName.Size = new System.Drawing.Size(49, 15);
            this.TxtDroneName.TabIndex = 1;
            this.TxtDroneName.Text = "APM-1";
            // 
            // IconTime
            // 
            this.IconTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconTime.Image = global::Diva.Properties.Resources.icon_telegram_2_24;
            this.IconTime.Location = new System.Drawing.Point(16, 5);
            this.IconTime.Name = "IconTime";
            this.IconTime.Size = new System.Drawing.Size(24, 24);
            this.IconTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconTime.TabIndex = 2;
            this.IconTime.TabStop = false;
            this.IconTime.MouseHover += new System.EventHandler(this.Pb_MouseHover);
            // 
            // IconBattery
            // 
            this.IconBattery.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconBattery.Image = global::Diva.Properties.Resources.icon_battery_normal;
            this.IconBattery.Location = new System.Drawing.Point(71, 5);
            this.IconBattery.Name = "IconBattery";
            this.IconBattery.Size = new System.Drawing.Size(28, 24);
            this.IconBattery.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconBattery.TabIndex = 3;
            this.IconBattery.TabStop = false;
            this.IconBattery.MouseHover += new System.EventHandler(this.Pb_MouseHover);
            // 
            // IconGPS
            // 
            this.IconGPS.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.IconGPS.Image = global::Diva.Properties.Resources.icon_gps_24;
            this.IconGPS.Location = new System.Drawing.Point(130, 5);
            this.IconGPS.Name = "IconGPS";
            this.IconGPS.Size = new System.Drawing.Size(24, 24);
            this.IconGPS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.IconGPS.TabIndex = 4;
            this.IconGPS.TabStop = false;
            this.IconGPS.MouseHover += new System.EventHandler(this.Pb_MouseHover);
            // 
            // TxtEstimatedTime
            // 
            this.TxtEstimatedTime.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtEstimatedTime.AutoSize = true;
            this.TxtEstimatedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtEstimatedTime.ForeColor = System.Drawing.Color.White;
            this.TxtEstimatedTime.Location = new System.Drawing.Point(11, 37);
            this.TxtEstimatedTime.Name = "TxtEstimatedTime";
            this.TxtEstimatedTime.Size = new System.Drawing.Size(35, 15);
            this.TxtEstimatedTime.TabIndex = 5;
            this.TxtEstimatedTime.Text = "0.0m";
            // 
            // TxtBatteryHealth
            // 
            this.TxtBatteryHealth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtBatteryHealth.AutoSize = true;
            this.TxtBatteryHealth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtBatteryHealth.ForeColor = System.Drawing.Color.White;
            this.TxtBatteryHealth.Location = new System.Drawing.Point(66, 37);
            this.TxtBatteryHealth.Name = "TxtBatteryHealth";
            this.TxtBatteryHealth.Size = new System.Drawing.Size(39, 15);
            this.TxtBatteryHealth.TabIndex = 6;
            this.TxtBatteryHealth.Text = "00 vol";
            this.TxtBatteryHealth.Click += new System.EventHandler(this.DetailValue_Click);
            // 
            // TxtSatelliteCount
            // 
            this.TxtSatelliteCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtSatelliteCount.AutoSize = true;
            this.TxtSatelliteCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.TxtSatelliteCount.ForeColor = System.Drawing.Color.White;
            this.TxtSatelliteCount.Location = new System.Drawing.Point(122, 37);
            this.TxtSatelliteCount.Name = "TxtSatelliteCount";
            this.TxtSatelliteCount.Size = new System.Drawing.Size(40, 15);
            this.TxtSatelliteCount.TabIndex = 7;
            this.TxtSatelliteCount.Text = "00 cut";
            // 
            // TxtSystemID
            // 
            this.TxtSystemID.AutoSize = true;
            this.TxtSystemID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSystemID.ForeColor = System.Drawing.Color.White;
            this.TxtSystemID.Location = new System.Drawing.Point(92, 6);
            this.TxtSystemID.Name = "TxtSystemID";
            this.TxtSystemID.Size = new System.Drawing.Size(19, 13);
            this.TxtSystemID.TabIndex = 8;
            this.TxtSystemID.Text = "00";
            // 
            // BtnExpand
            // 
            this.BtnExpand.AutoSize = true;
            this.BtnExpand.FlatAppearance.BorderSize = 0;
            this.BtnExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnExpand.Image = global::Diva.Properties.Resources.icon_btn_expand;
            this.BtnExpand.Location = new System.Drawing.Point(377, 16);
            this.BtnExpand.Name = "BtnExpand";
            this.BtnExpand.Size = new System.Drawing.Size(36, 30);
            this.BtnExpand.TabIndex = 9;
            this.BtnExpand.UseVisualStyleBackColor = true;
            this.BtnExpand.Click += new System.EventHandler(this.BtnExpand_Click);
            // 
            // iconRC
            // 
            this.iconRC.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.iconRC.Image = global::Diva.Properties.Resources.icon_rc_24;
            this.iconRC.Location = new System.Drawing.Point(188, 5);
            this.iconRC.Name = "iconRC";
            this.iconRC.Size = new System.Drawing.Size(24, 24);
            this.iconRC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.iconRC.TabIndex = 10;
            this.iconRC.TabStop = false;
            this.iconRC.MouseHover += new System.EventHandler(this.Pb_MouseHover);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(180, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "00 cut";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtEstimatedTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.iconRC, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.IconBattery, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtBatteryHealth, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.IconGPS, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtSatelliteCount, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.IconTime, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(141, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(230, 55);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // labelIsArm
            // 
            this.labelIsArm.AutoSize = true;
            this.labelIsArm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelIsArm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(255)))), ((int)(((byte)(88)))));
            this.labelIsArm.Location = new System.Drawing.Point(72, 38);
            this.labelIsArm.Name = "labelIsArm";
            this.labelIsArm.Size = new System.Drawing.Size(58, 15);
            this.labelIsArm.TabIndex = 13;
            this.labelIsArm.Text = "DisARM";
            // 
            // DroneInfo
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(54)))), ((int)(((byte)(98)))));
            this.Controls.Add(this.labelIsArm);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BtnExpand);
            this.Controls.Add(this.TxtSystemID);
            this.Controls.Add(this.TxtDroneName);
            this.Controls.Add(this.PBDroneView);
            this.Name = "DroneInfo";
            this.Size = new System.Drawing.Size(426, 60);
            ((System.ComponentModel.ISupportInitialize)(this.PBDroneView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconBattery)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IconGPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconRC)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox PBDroneView;
		private System.Windows.Forms.Label TxtDroneName;
		private System.Windows.Forms.PictureBox IconTime;
		private System.Windows.Forms.PictureBox IconBattery;
		private System.Windows.Forms.PictureBox IconGPS;
		private System.Windows.Forms.Label TxtEstimatedTime;
		private System.Windows.Forms.Label TxtBatteryHealth;
		private System.Windows.Forms.Label TxtSatelliteCount;
		private System.Windows.Forms.Label TxtSystemID;
		private System.Windows.Forms.Button BtnExpand;
        private System.Windows.Forms.PictureBox iconRC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelIsArm;
    }
}
