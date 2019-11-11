using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Diva.EnergyConsumption;
using Diva.Mavlink;
using Diva.Properties;
using static Diva.Utilities.ResourceHelper;

namespace Diva.Controls
{
	public partial class DroneInfo : UserControl
	{
		private bool isActive = false;
        public bool IsActive
		{
            get => isActive;
			private set
			{
				isActive = value;
                BackColor = isActive ? Color.FromArgb(37, 54, 98) : Color.FromArgb(128, 128, 128);
				Parent?.Invalidate(Bounds, true);
			}
		}
        public MavDrone Drone { get; private set; }

		public DroneInfo(MavDrone m, string name)
		{
			InitializeComponent();
            TxtDroneName.Text = name;
            Margin = new Padding(0);
            m.Status.GuidedMode.z = Planner.TAKEOFF_HEIGHT;
            Drone = m;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateTelemetryData()
		{
			TxtSystemID.Text = Drone.SysId.ToString();
			TxtBatteryHealth.Text = Drone.Status.BatteryVoltage.ToString("F2");
			TxtSatelliteCount.Text = Drone.Status.SatteliteCount.ToString();
		}

		public void UpdateAssumeTime(double missionDistance)
		{
			// get the waypoint speed, default unit is mile/second
			// TxtAssumeTime.Text = (missionDistance / (GetParam("WPNAV_SPEED")*60/1000)).ToString("f1");
			TxtAssumeTime.Text = (missionDistance / 0.72).ToString("f1") + "m";
		}

		public void ResetAssumeTime()
		{
			TxtAssumeTime.Text = "0.0m";
		}

		public void LowVoltageWarning(bool isLowVoltage)
		{
			TxtBatteryHealth.ForeColor = isLowVoltage ? Color.Red : Color.White;
		}

        private readonly Color EC_ColorNormal = Color.White;
        private readonly Color EC_ColorWarning = Color.Orange;
        private readonly Color EC_ColorError = Color.Red;

        private double effectiveBatteryCapacity = 0.0;
        private int tokenSerialNumber = 0;

        public void UpdateEstimatedFlightEnergy(double en)
        {
            if (!IsActive || !ChkBtnPowerModel.Checked) return;
            if (en == double.NaN)
            {
                IconEnergyConsumption.Image = Resources.icon_error;
                LabelEstimatedEnergyConsumptionText.Text = Strings.StrEnergyConsumptionEstimationFailed;
                LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorError;
            }
            else
            {
                if (en < effectiveBatteryCapacity * 0.8)
                {
                    IconEnergyConsumption.Image = Resources.icon_power_full_32;
                    LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorNormal;
                }
                else if (en < effectiveBatteryCapacity)
                {
                    IconEnergyConsumption.Image = Resources.icon_power_medium_32;
                    LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorWarning;
                }
                else
                {
                    IconEnergyConsumption.Image = Resources.icon_power_low_32;
                    LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorError;
                }
                LabelEstimatedEnergyConsumptionText.Text = Strings.StrEstimatedEnergy.FormatWith(en);
            }
        }

        private void CalculateEnergyConsumptionCallback(double value, object token)
        {
            if ((int)token != tokenSerialNumber) return;
            if (InvokeRequired)
                Invoke((Action) delegate { UpdateEstimatedFlightEnergy(value); });
            else
                UpdateEstimatedFlightEnergy(value);
        }

        private void TriggerEstimatedEnergyRecalculation(object sender, EventArgs e)
        {
            if (!IsActive || !ChkBtnPowerModel.Checked) return;
            var planner = Planner.GetPlannerInstance();
            if (planner.MissionListItemCount == 0)
            {
                IconEnergyConsumption.Image = Resources.icon_error;
                LabelEstimatedEnergyConsumptionText.Text = Strings.StrNoMissionPointAvailable;
                LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorError;
                return;
            }
            IconEnergyConsumption.Image = Resources.icon_loading;
            LabelEstimatedEnergyConsumptionText.ForeColor = EC_ColorWarning;
            LabelEstimatedEnergyConsumptionText.Text = Strings.StrRecalculating;
            PowerModelManager.GetModel(Drone.Setting.PowerModel).
                CalculateEnergyConsumptionBackground(Drone,
                    planner.GetCommandList(), planner.GetHomeWP(),
                    CalculateEnergyConsumptionCallback, ++tokenSerialNumber);
        }

        public void NotifyMissionChanged() => TriggerEstimatedEnergyRecalculation(null, null);

        [Browsable(true)]
		public event EventHandler CloseButtonClicked;
		private void BtnClose_Click(object sender, EventArgs e)
		{
			CloseButtonClicked?.Invoke(this, e) ;
		}
	}
}
