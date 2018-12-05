using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.EnergyConsumption;

namespace Diva.Controls
{
	public partial class DialogInputPowerModel : Form
	{
		private string modelFile = null;
		private string droneID;
		private double batteryCapacity;
		private int availablePercentage;

		public event EventHandler DoClick;

		public string DroneID { get => LBLDroneID.Text;
			set
			{
				droneID = value;
				LBLDroneID.Text = value;
			}
		}
		public double BatteryCapacity {
			get => double.Parse(TXTBatteryCapacity.Text);
			set
			{
				batteryCapacity = value;
				TXTBatteryCapacity.Text = batteryCapacity.ToString();
			}
		}
		public int AvailablePercentage {
			get => int.Parse(TXTAvailablePercentage.Text);
			set
			{
				availablePercentage = value;
				TXTAvailablePercentage.Text = AvailablePercentage.ToString();
			}
		}

		public double PredictedOutput { get; set; } = 0.0d;

		public DialogInputPowerModel()
		{
			InitializeComponent();
            modelFile = Planner.GetActiveDroneInfo()?.Drone.Setting.PowerModel;
            LBLModelPath.Text = modelFile ?? PowerModel.PowerModelNone.ModelName;
        }

		private void BTNConfirm_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(modelFile))
			{
				MessageBox.Show("Please Pick a model first");
				return;
			}
			DoClick?.Invoke(sender, e);
		}

		private void BTNCancel_Click(object sender, EventArgs e)
		{
			this.Dispose();
		}

		private void BTNBrowseModel_Click(object sender, EventArgs e)
		{
            var planner = Planner.GetPlannerInstance();
            var waypoints = planner.GetCommandList();
            var drone = Planner.GetActiveDroneInfo()?.Drone;
            var model = PowerModel.GetModel(drone?.Setting.PowerModel);
            if (waypoints.Count == 0 || model == PowerModel.PowerModelNone)
            {
                // error
                return;
            }
            PredictedOutput = model.CalculateEnergyConsumption(drone, waypoints, planner.GetHomeLocationwp());
        }
	}
}
