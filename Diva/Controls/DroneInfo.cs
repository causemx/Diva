using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Diva.Mavlink;
using Diva.Utilities;

namespace Diva.Controls
{
    public partial class DroneInfo : UserControl
    {
        private bool isActive = false;
        public bool IsActive
        {
            get => isActive;
            private set
            {
                isActive = value;
                BackColor = isActive ? Color.FromArgb(37, 54, 98) : Color.FromArgb(128, 128, 128);
                Parent?.Invalidate(Bounds, true);
            }
        }
        public MavDrone Drone { get; private set; }

        public DroneInfo(MavDrone m, string name)
        {
            InitializeComponent();
            TxtDroneName.Text = name;
            Margin = new Padding(0);
            m.Status.GuidedMode.z = (float)DefaultValues.TakeoffHeight;
            Drone = m;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateTelemetryData()
        {
            TxtSystemID.Text = Drone.SysId.ToString();
            TxtBatteryHealth.Text = Drone.Status.BatteryVoltage.ToString("F2");
            TxtSatelliteCount.Text = Drone.Status.SatteliteCount.ToString();
        }

        public void UpdateEstimatedTime(double missionDistance)
        {
            // get the waypoint speed, default unit is mile/second
            // TxtAssumeTime.Text = (missionDistance / (GetParam("WPNAV_SPEED")*60/1000)).ToString("f1");
            TxtEstimatedTime.Text = (missionDistance / 0.72).ToString("f1") + "m";
        }

        public void ResetEstimatedTime()
        {
            TxtEstimatedTime.Text = "0.0m";
        }

        public void LowVoltageWarning(bool isLowVoltage)
        {
            TxtBatteryHealth.ForeColor = isLowVoltage ? Color.Red : Color.White;
        }

        private readonly Color EC_ColorNormal = Color.White;
        private readonly Color EC_ColorWarning = Color.Orange;
        private readonly Color EC_ColorError = Color.Red;

        private double effectiveBatteryCapacity = 0.0;
        private int tokenSerialNumber = 0;

        public void NotifyMissionChanged() {}

        [Browsable(true)]
		public event EventHandler ToggleTelemetryInfo;
		private void BtnExpand_Click(object sender, EventArgs e)
		{
			ToggleTelemetryInfo?.Invoke(this, e) ;
		}
	}
}
