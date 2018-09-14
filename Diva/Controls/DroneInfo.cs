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
	public partial class DroneInfo : UserControl, IActivate, IDeactivate
	{

		private bool isActive = false;

		public bool IsActive
		{
			get
			{
				return isActive;
			}
			private set
			{
				isActive = value;
                BackColor = isActive ? Color.FromArgb(67, 78, 84) : Color.FromArgb(128, 128, 128);
				if (Parent != null)
				{
					Parent.Invalidate(Bounds, true);
				}
			}
		}
		public MavlinkInterface mav { get; }

        public string droneName => TxtDroneName.Text;

		public DroneInfo(MavlinkInterface m, string name)
		{
			InitializeComponent();
			mav = m;
            mav.onCreate();
            mav.Status.GuidedMode.z = Planner.TAKEOFF_HEIGHT;
            TxtDroneName.Text = name;
            Margin = new Padding(0);
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
			TxtAssumeTime.Text = (missionDistance / 0.3).ToString("f1");
			Planner.log.Info("distance double: " + missionDistance);
		} 

		public event EventHandler CloseButtonClicked;

		private void BtnClose_Click(object sender, EventArgs e)
		{
			CloseButtonClicked?.Invoke(this, e) ;
		}
	}
}
