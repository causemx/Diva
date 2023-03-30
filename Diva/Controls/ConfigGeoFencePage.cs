using System.ComponentModel;
using System.Windows.Forms;
using Diva.Controls.Components;
using Diva.Mavlink;

namespace Diva.Controls
{
	public partial class ConfigGeoFencePage : UserControl
	{
        private Mavlink.MavDrone mav = Planner.GetActiveDrone();

        public ConfigGeoFencePage()
		{
			InitializeComponent();
			if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
			{
				RenderToCtl();
			}
		}

		public void RenderToCtl()
		{
			CheckBox_Enable.setup(1, 0, "FENCE_ENABLE", mav.Status.Params);

			ComboBox_Fence_Type.setup(
				MavParamRepositoryV2.GetOptionsInt("FENCE_TYPE",
					"ArduCopter2"), "FENCE_TYPE", mav.Status.Params);


			ComboBox_Fence_Action.setup(
				MavParamRepositoryV2.GetOptionsInt("FENCE_ACTION",
					"ArduCopter2"), "FENCE_ACTION", mav.Status.Params);


			// 3

			// myNumericUpDown1.setup(10, 1000, (float)CurrentState.fromDistDisplayUnit(1), 1, "FENCE_ALT_MAX",
			//	MainV2.comPort.MAV.param);
			NumericUpDown_Fence_Radius.setup(10, 1000, (float)(1), 1, "FENCE_RADIUS",
				mav.Status.Params);

			NumericUpDown_Fence_Altitude.setup(30, 65536, (float)(1), 1, "FENCE_ALT_MAX",
				mav.Status.Params);

			NumericUpDown_Fence_RTL_Altitude.setup(1, 500, (float)(100), 1, "RTL_ALT",
				mav.Status.Params);


			// arducopter
			ComboBox_Batt_Action.setup(
				MavParamRepositoryV2.GetOptionsInt("FS_BATT_ENABLE",
					mav.Status.Firmware.ToString()), "FS_BATT_ENABLE", mav.Status.Params);

			// low battery
			if (mav.Status.Params.ContainsKey("LOW_VOLT"))
            {
				NumericUpDown_LowBatt_Value.setup(6, 99, 1, 0.1f, "LOW_VOLT", mav.Status.Params);
            }
            else
            {
				NumericUpDown_LowBatt_Value.setup(6, 99, 1, 0.1f, "FS_BATT_VOLTAGE", mav.Status.Params);
            }

			NumericUpDown_LowBatt_Value.RaisingValueChanged += (s, e) => {
				mav.Status.BatteryLowVoltage = (double)((MyNumericUpDown)s).Value; };

		}
			

	}


}
