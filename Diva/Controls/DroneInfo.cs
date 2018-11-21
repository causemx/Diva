using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Mavlink;

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
                BackColor = isActive ? Color.FromArgb(67, 78, 84) : Color.FromArgb(128, 128, 128);
				Parent?.Invalidate(Bounds, true);
			}
		}
        public MavDrone Drone { get; private set; }
        public string droneName => TxtDroneName.Text;

		public DroneInfo(MavDrone m, string name)
		{
			InitializeComponent();
            TxtDroneName.Text = name;
            Margin = new Padding(0);
            var mav = (MavlinkInterface)m;
            mav.Status.GuidedMode.z = Planner.TAKEOFF_HEIGHT;
            mav.onCreate();
            Drone = m;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateTelemetryData(byte sysid, double battry_voltage, float satellite_count)
		{
			TxtSystemID.Text = sysid.ToString();
			TxtBatteryHealth.Text = battry_voltage.ToString("F2");
			TxtSatelliteCount.Text = satellite_count.ToString();
		}

		public void UpdateAssumeTime(double missionDistance)
		{
			// get the waypoint speed, default unit is mile/second
			// TxtAssumeTime.Text = (missionDistance / (GetParam("WPNAV_SPEED")*60/1000)).ToString("f1");
			TxtAssumeTime.Text = (missionDistance / 0.3).ToString("f1") + "m";
		}

		public void ResetAssumeTime()
		{
			TxtAssumeTime.Text = "0.0m";
		}

		public void LowVoltageWarning(bool isLowVoltage)
		{
			TxtBatteryHealth.ForeColor = isLowVoltage ? Color.Red : Color.White;
		}

        [Browsable(true)]
		public event EventHandler CloseButtonClicked;
		private void BtnClose_Click(object sender, EventArgs e)
		{
			CloseButtonClicked?.Invoke(this, e) ;
		}

		private void BtnPowerModel_Click(object sender, EventArgs e)
		{
			DialogInputPowerModel dp = new DialogInputPowerModel();
			dp.ShowDialog();
			dp.DroneID = (Drone.Status.sysid).ToString();
			dp.DoClickEvent += (s, e1) =>
			{
				PictureBox pb = new PictureBox();
				Label lb = new Label();
				PanelConsumptionPower.Controls.Add(pb);
			};
		}
	}
}
