using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Diva.Utilities.ResourceHelper;


namespace Diva.Controls
{
	public partial class TelemetryDataPanel : UserControl
	{
		public TelemetryDataPanel()
		{
			InitializeComponent();
            this.UpdateLocale();
        }

        public void UpdateTelemetryData(double altitude, double verticalSpeed, double groundSpeed, double airspeed)
		{
			labelVerticalspeed.Text = verticalSpeed.ToString("F2") + "m";
			labelGroundspeed.Text = groundSpeed.ToString("F2") + "m/s";
			labelAltitude.Text = altitude.ToString("F2") + "m/s";
			labelAirspeed.Text = airspeed.ToString("F2") + "m/s";
		}

		public void UpdateStatusChecker(bool lowBattEnable, bool geoFenceEnable)
		{
			// LBL_LowBatteryChecker.Text = lowBattEnable.ToString();
			labelGeofenceEnable.Text = geoFenceEnable.ToString();
				
		}
	}
}
