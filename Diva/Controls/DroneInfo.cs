using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.EnergyConsumption;
using Diva.Mavlink;
using Diva.Properties;

namespace Diva.Controls
{
	public partial class DroneInfo : UserControl
	{
		private bool isActive = false;
        private bool isEnergyPanelVisible = false;
        public bool IsActive
		{
            get => isActive;
			private set
			{
				isActive = value;
                SetEnergyConsumptionInfoPanelVisibility(value && isEnergyPanelVisible);
                BackColor = isActive ? Color.FromArgb(67, 78, 84) : Color.FromArgb(128, 128, 128);
				Parent?.Invalidate(Bounds, true);
			}
		}
        public MavDrone Drone { get; private set; }
        public string droneName => TxtDroneName.Text;

		public DroneInfo(MavDrone m, string name)
		{
			InitializeComponent();
            TxtDroneName.Text = name;
            Margin = new Padding(0);
            var mav = (MavlinkInterface)m;
            mav.Status.GuidedMode.z = Planner.TAKEOFF_HEIGHT;
            mav.onCreate();
            Drone = m;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;

        public void UpdateTelemetryData(byte sysid, double battry_voltage, float satellite_count)
		{
			TxtSystemID.Text = sysid.ToString();
			TxtBatteryHealth.Text = battry_voltage.ToString("F2");
			TxtSatelliteCount.Text = satellite_count.ToString();
		}

		public void UpdateAssumeTime(double missionDistance)
		{
			// get the waypoint speed, default unit is mile/second
			// TxtAssumeTime.Text = (missionDistance / (GetParam("WPNAV_SPEED")*60/1000)).ToString("f1");
			TxtAssumeTime.Text = (missionDistance / 0.3).ToString("f1") + "m";
		}

		public void ResetAssumeTime()
		{
			TxtAssumeTime.Text = "0.0m";
		}

		public void LowVoltageWarning(bool isLowVoltage)
		{
			TxtBatteryHealth.ForeColor = isLowVoltage ? Color.Red : Color.White;
		}

        private double effectiveBatteryCapacity = 0.0;
        private int tokenSerialNumber = 0;

        public void UpdateEstimatedFlightEnergy(double en)
        {
            if (!IsActive || !isEnergyPanelVisible) return;
            if (en == double.NaN)
            {
                IconEnergyConsumption.Image = Resources.icon_error;
                LabelEstimatedEnergyConsumptionText.Text = Strings.StrEnergyConsumptionEstimationFailed;
            }
            else
            {
                IconEnergyConsumption.Image =
                    en < effectiveBatteryCapacity * 0.8 ? Resources.icon_power_full_32 :
                    en < effectiveBatteryCapacity ? Resources.icon_power_medium_32 :
                    Resources.icon_power_low_32;
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
            var planner = Planner.GetPlannerInstance();
            if (planner.MissionListItemCount == 0)
            {
                IconEnergyConsumption.Image = Resources.icon_error;
                LabelEstimatedEnergyConsumptionText.Text = Strings.StrNoMissionPointAvailable;
                return;
            }
            IconEnergyConsumption.Image = Resources.icon_loading;
            LabelEstimatedEnergyConsumptionText.Text = Strings.StrRecalculatingEstimatedEnergyConsumption;
            PowerModel.GetModel(Drone.Setting.PowerModel).CalculateEnergyConsumptionBackground
                (Drone, planner.GetCommandList(), planner.GetHomeLocationwp(),
                    CalculateEnergyConsumptionCallback, ++tokenSerialNumber);
        }

        public void NotifyMissionChanged() => TriggerEstimatedEnergyRecalculation(null, null);

        [Browsable(true)]
		public event EventHandler CloseButtonClicked;
		private void BtnClose_Click(object sender, EventArgs e) =>
            CloseButtonClicked?.Invoke(this, e) ;

		private void BtnPowerModel_Click(object sender, EventArgs e)
        {
            if (IsActive)
            {
                bool newVisible = !isEnergyPanelVisible;
                SetEnergyConsumptionInfoPanelVisibility(newVisible);
                isEnergyPanelVisible = newVisible;
            }
        }

        private void SetEnergyConsumptionInfoPanelVisibility(bool visible)
        {
            if (visible == isEnergyPanelVisible && IsActive) return;
            if (visible)
            {
                if (effectiveBatteryCapacity == 0.0)
                {
                    var setting = Drone.Setting;
                    double.TryParse(setting.BatteryCapacity, out double cap);
                    double.TryParse(setting.BatteryAvailability, out double avail);
                    if (cap <= 0 || avail <= 0 || avail > 100 ||
                        setting.PowerModel == PowerModel.PowerModelNone.ModelName)
                    {
                        MessageBox.Show(Strings.MsgInvalidPowerModelAndOrBatterySetting);
                        return;
                    }
                    effectiveBatteryCapacity = cap * (avail / 100.0);
                }
                Height = PanelEnergyConsumptionInfo.Bottom;
                NotifyMissionChanged();
            }
            else
            {
                Height = PanelEnergyConsumptionInfo.Top;
            }
        }
    }
}
