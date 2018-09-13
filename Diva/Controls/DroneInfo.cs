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

		public bool IsActivate
		{
			get
			{
				return this.isActive;
			}
			set
			{
				this.isActive = value;
				if (isActive)
				{
					Activate();
				}
				else
				{
					Deactivate();
				}
				if (this.Parent != null)
				{
					Parent.Invalidate(this.Bounds, true);
				}
			}
		}
		public MavlinkInterface mav { get; }

        public string droneName => TxtDroneName.Text;

		public DroneInfo(MavlinkInterface m, string name)
		{
			InitializeComponent();
			this.mav = m;
            TxtDroneName.Text = name;
		}

		public void Activate()
		{
			mav.onCreate();
			mav.MAV.GuidedMode.z = Planner.TAKEOFF_HEIGHT;
			this.BackColor = Color.FromArgb(67, 78, 84);
		}

		public void Deactivate()
		{
			this.BackColor = Color.FromArgb(128, 128, 128);
		}


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

		public int GetParam(string paramname)
		{
			int _scale = 1;
			MAVLink.MAVLinkParamList paramlist = Planner.comPort.MAV.param;
			try
			{
				if (paramlist.ContainsKey(paramname))
				{
					int value = (int)((float)paramlist[paramname] / _scale);
					return value;
				}
				else
				{
					throw new Exception("can not retrive parameters");
				}
			}
			catch (Exception e)
			{
				Planner.log.Debug(e.ToString());
				return 0;
			}
			
		}

		public event EventHandler CloseButtonClicked;

		private void BtnClose_Click(object sender, EventArgs e)
		{
			CloseButtonClicked?.Invoke(this, e) ;
		}
	}
}
