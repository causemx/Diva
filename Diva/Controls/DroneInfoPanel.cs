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
		
		public DroneInfoPanel()
		{
			InitializeComponent();
		}

		public void Activate()
		{
			this.BackColor = Color.FromArgb(67,78,84);
			this.Enabled = true;
		}

		public void Deactivate()
		{
			this.BackColor = Color.FromArgb(128,128,128);
			this.Enabled = false;
		}


		public void UpdateTelemetryData(double battry_voltage, float satellite_count)
		{
			TxtAssumeTime.Text = "1hr";
			TxtBatteryHealth.Text = battry_voltage.ToString("F2");
			TxtSatelliteCount.Text = satellite_count.ToString();
		}
	}
}
