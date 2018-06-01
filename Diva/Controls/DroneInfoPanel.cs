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
	public partial class DroneInfoPanel : UserControl, IActivate, IDeactivate
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

		private string droneName = "";
		public string DroneName
		{
			get
			{
				return droneName;
			}
			set
			{
				this.droneName = value;
				TxtDroneName.Text = droneName;
			}
		}

		public DroneInfoPanel()
		{
			InitializeComponent();
		}

		public void Activate()
		{
			this.BackColor = Color.FromArgb(67,78,84);
			PBDroneView.Image = Properties.Resources.if_Psyduck_3151565;
		}

		public void Deactivate()
		{
			this.BackColor = Color.FromArgb(128,128,128);
			PBDroneView.Image = Properties.Resources.if_Psyduck_3186864;
		}


		public void UpdateTelemetryData(byte sysid, double battry_voltage, float satellite_count)
		{
			TxtSystemID.Text = sysid.ToString();
			TxtAssumeTime.Text = "1hr";
			TxtBatteryHealth.Text = battry_voltage.ToString("F2");
			TxtSatelliteCount.Text = satellite_count.ToString();
		}
	}
}
