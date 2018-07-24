using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Utilities;

namespace Diva.Controls
{
	public partial class ConfigGeoFencePage : UserControl
	{
		public ConfigGeoFencePage()
		{
			InitializeComponent();
		}

		public void Activate()
		{
			myCheckBox1.setup(1, 0, "FENCE_ENABLE", Planner.comPort.MAV.param);

			myComboBox1.setup(
				ParameterMetaDataRepository.GetParameterOptionsInt("FENCE_TYPE",
					"ArduCopter2"), "FENCE_TYPE", Planner.comPort.MAV.param);


			myComboBox2.setup(
				ParameterMetaDataRepository.GetParameterOptionsInt("FENCE_ACTION",
					"ArduCopter2"), "FENCE_ACTION", Planner.comPort.MAV.param);


			// 3

			// myNumericUpDown1.setup(10, 1000, (float)CurrentState.fromDistDisplayUnit(1), 1, "FENCE_ALT_MAX",
			//	MainV2.comPort.MAV.param);
			myNumericUpDown1.setup(10, 1000, (float)(1), 1, "FENCE_ALT_MAX",
				Planner.comPort.MAV.param);

			myNumericUpDown2.setup(30, 65536, (float)(1), 1, "FENCE_RADIUS",
				Planner.comPort.MAV.param);

			myNumericUpDown3.setup(1, 500, (float)(100), 1, "RTL_ALT",
				Planner.comPort.MAV.param);
		}

	}


}
