using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class TelemetryDataPanel : UserControl
	{
		public TelemetryDataPanel()
		{
			InitializeComponent();
            this.UpdateLocale();
        }

        public void UpdateTelemetryData(double altitude, double verticalSpeed, double groundSpeed)
		{
			TxtVerticalSpeed.Text = verticalSpeed.ToString("F2");
			TxtGroundSpeed.Text = groundSpeed.ToString("F2");
			TxtAltitude.Text = altitude.ToString("F2");
		}

		public void UpdateStatusChecker(bool lowBattEnable, bool geoFenceEnable)
		{
			LBL_LowBatteryChecker.Text = lowBattEnable.ToString();
			LBL_GeoFenceChecker.Text = geoFenceEnable.ToString();
				
		}
	}
}
