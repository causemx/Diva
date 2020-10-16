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
        private bool IsLowVoltage => Drone.Status.BatteryVoltage != 0 && Drone.Status.BatteryLowVoltage > Drone.Status.BatteryVoltage;

        public DroneInfo(MavDrone m, string name)
        {
            InitializeComponent();
            TxtDroneName.Text = name;
            TxtSystemID.Text = "";
            Margin = new Padding(0);
            m.Status.GuidedMode.z = (float)DefaultValues.TakeoffHeight;
            Drone = m;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateTelemetryData()
        {
            if (Drone.Status.BatteryVoltage == 0)
            {
                TxtBatteryHealth.Text = "N/A";
                TxtBatteryHealth.ForeColor = Color.White;
                IconBattery.Image = Properties.Resources.icon_battery_normal;
            }
            else
            {
                TxtBatteryHealth.Text = Drone.Status.BatteryVoltage.ToString("F2") + 'V';
                if (IsLowVoltage)
                {
                    TxtBatteryHealth.ForeColor = Color.White;
                    IconBattery.Image = Properties.Resources.icon_battery_normal;
                }
                else
                {
                    TxtBatteryHealth.ForeColor = Color.Red;
                    IconBattery.Image = Properties.Resources.icon_battery_nopower;
                }
            }
            TxtSatelliteCount.Text = Drone.Status.SatteliteCount.ToString();
            if (Drone.Status.ArmedSince != null)
            {
                var elapsed = DateTime.Now - Drone.Status.ArmedSince.Value;
                if (elapsed < new TimeSpan(0, 1, 0))
                    TxtEstimatedTime.Text = elapsed.Seconds + "s";
                else if (elapsed < new TimeSpan(1, 0, 0))
                    TxtEstimatedTime.Text = elapsed.Minutes + "m" + elapsed.Seconds + "s";
                else
                {
                    int m = (int)elapsed.TotalMinutes;
                    TxtEstimatedTime.Text = (m / 60) + "h" + (m % 60) + "m";
                }
            }
            else
                TxtEstimatedTime.Text = "-";
        }

        /*public void UpdateEstimatedTime(double missionDistance)
        {
            // get the waypoint speed, default unit is mile/second
            // TxtAssumeTime.Text = (missionDistance / (GetParam("WPNAV_SPEED")*60/1000)).ToString("f1");
            TxtEstimatedTime.Text = (missionDistance / 0.72).ToString("f1") + "m";
        }

        public void ResetEstimatedTime()
        {
            TxtEstimatedTime.Text = "0.0m";
        }*/

        /*public void LowVoltageWarning(bool isLowVoltage)
        {
            TxtBatteryHealth.ForeColor = isLowVoltage ? Color.Red : Color.White;
        }*/

        private readonly Color EC_ColorNormal = Color.White;
        private readonly Color EC_ColorWarning = Color.Orange;
        private readonly Color EC_ColorError = Color.Red;

        public void NotifyMissionChanged() {}

        [Browsable(true)]
		public event EventHandler ToggleTelemetryInfo;
		private void BtnExpand_Click(object sender, EventArgs e)
		{
			ToggleTelemetryInfo?.Invoke(this, e);
		}

        public void SetExpanded(bool expanded) {
            BtnExpand.Image = expanded ? Properties.Resources.icon_btn_collapse
                : Properties.Resources.icon_btn_expand;
        }
	}
}
