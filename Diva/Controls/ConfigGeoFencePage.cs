using System.ComponentModel;
using System.Windows.Forms;
using Diva.Controls.Components;
using Diva.Utilities;

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
			CheckBox_Enable.setup(1, 0, "FENCE_ENABLE", mav.Status.param);

			ComboBox_Fence_Type.setup(
				ParameterMetaDataRepository.GetParameterOptionsInt("FENCE_TYPE",
					"ArduCopter2"), "FENCE_TYPE", mav.Status.param);


			ComboBox_Fence_Action.setup(
				ParameterMetaDataRepository.GetParameterOptionsInt("FENCE_ACTION",
					"ArduCopter2"), "FENCE_ACTION", mav.Status.param);


			// 3

			// myNumericUpDown1.setup(10, 1000, (float)CurrentState.fromDistDisplayUnit(1), 1, "FENCE_ALT_MAX",
			//	MainV2.comPort.MAV.param);
			NumericUpDown_Fence_Radius.setup(10, 1000, (float)(1), 1, "FENCE_RADIUS",
				mav.Status.param);

			NumericUpDown_Fence_Altitude.setup(30, 65536, (float)(1), 1, "FENCE_ALT_MAX",
				mav.Status.param);

			NumericUpDown_Fence_RTL_Altitude.setup(1, 500, (float)(100), 1, "RTL_ALT",
				mav.Status.param);


			// arducopter
			ComboBox_Batt_Action.setup(
				ParameterMetaDataRepository.GetParameterOptionsInt("FS_BATT_ENABLE",
					mav.Status.firmware.ToString()), "FS_BATT_ENABLE", mav.Status.param);

			// low battery
			if (mav.Status.param.ContainsKey("LOW_VOLT"))
            {
				NumericUpDown_LowBatt_Value.setup(6, 99, 1, 0.1f, "LOW_VOLT", mav.Status.param);
            }
            else
            {
				NumericUpDown_LowBatt_Value.setup(6, 99, 1, 0.1f, "FS_BATT_VOLTAGE", mav.Status.param);
            }

			NumericUpDown_LowBatt_Value.RaisingValueChanged += (s, e) => {
				mav.Status.low_voltage = (double)((MyNumericUpDown)s).Value; };

		}
			

	}


}
