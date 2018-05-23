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
	public partial class DroneInfoPanel : UserControl
	{
		public DroneInfoPanel()
		{
			InitializeComponent();
		}

		public void DoEnable(bool enable)
		{
			this.Enabled = enable;
		}

		public void UpdateTelemetryData(double battry_voltage, float satellite_count)
		{
			TxtAssumeTime.Text = "1hr";
			TxtBatteryHealth.Text = battry_voltage.ToString("F2");
			TxtSatelliteCount.Text = satellite_count.ToString();
		}
	}
}
