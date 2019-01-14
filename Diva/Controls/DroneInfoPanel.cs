﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Mavlink;
using Diva.Properties;
using Diva.Utilities;

namespace Diva.Controls
{
	public partial class DroneInfoPanel : UserControl
	{
		private DroneInfo activeDrone = null;
        [Browsable(true)]
        public event EventHandler ActiveDroneChanged;
		public DroneInfo ActiveDroneInfo
		{
			get => activeDrone;
			set
			{
				if (activeDrone != value &&
                    ThePanel.Controls.IndexOf(value) >= 0)
				{
					if (activeDrone != null)
					{
						activeDrone.Deactivate();
						DroneInfoTip.SetToolTip(activeDrone, Strings.StrActivateDrone);
					}
					activeDrone = value;
					if (activeDrone != null)
					{
						activeDrone.Activate();
						DroneInfoTip.SetToolTip(activeDrone, Strings.StrActivateDrone);
					}
					TelemetryData.Visible = false;
                    ActiveDroneChanged?.Invoke(value, null);
				}
			}
		}
		[Browsable(true)]
		public event EventHandler DroneClosed;

		public DroneInfoPanel()
		{
			InitializeComponent();
			TelemetryData.Visible = false;
		}

		public NotificationManager.INotification battNotification;

		public DroneInfo AddDrone(MavDrone drone, bool setActive = true)
		{
			var dinfo = new DroneInfo(drone, drone.Name);

			dinfo.Click += (s, e) => ActiveDroneInfo = s as DroneInfo;
			dinfo.DoubleClick += (s, e) =>
			{
				ThePanel.Controls.Remove(s as Control);
				if (s == ActiveDroneInfo)
                {
					ActiveDroneInfo = null;
                    TelemetryData.Visible = false;
                }
				drone.Disconnect();
				DroneClosed?.Invoke(s, e);
			};
			dinfo.ToggleTelemetryInfoTriggered += (s, e) =>
			{
				var d = s as DroneInfo;
				if (d != null)
				{
					ActiveDroneInfo = d;
					TelemetryData.Visible = !TelemetryData.Visible;
                }
			};

			battNotification = new NotificationManager.BatteryNotification(dinfo);

			ThePanel.Controls.Remove(TelemetryData);
			ThePanel.Controls.Add(dinfo);
			ThePanel.Controls.Add(TelemetryData);
			if (setActive) ActiveDroneInfo = dinfo;

			return dinfo;
		}

		public void Clear()
		{
			ActiveDroneInfo = null;
			TelemetryData.Visible = false;
			ThePanel.Controls.OfType<DroneInfo>().ToList().ForEach(d => d.Drone.Disconnect());
			ThePanel.Controls.Clear();
			ThePanel.Controls.Add(TelemetryData);
		}

		public void UpdateDisplayInfo()
		{
			if (ActiveDroneInfo != null) try
			{
				MavStatus status = ActiveDroneInfo.Drone.Status;
				ActiveDroneInfo.UpdateTelemetryData(activeDrone.Drone.SysId,
                    status.battery_voltage, status.satcount);
				TelemetryData.UpdateTelemetryData(status.alt,
                    status.verticalspeed, status.groundspeed);
				string getText(string name) =>
					TelemetryData.Controls.Find(name, true)[0].Text;
				DroneInfoTip.SetToolTip(ActiveDroneInfo, $@"{getText("GBAltitude")}: {getText("TxtAltitude")}
{getText("GBGroundSpeed")}: {getText("TxtGroundSpeed")}
{getText("GBVerticalSpeed")}: {getText("TxtVerticalSpeed")}");

				battNotification.Notify();

				MAVLink.MAVLinkParamList paramList = ActiveDroneInfo.Drone.Status.param;
                if (paramList["FENCE_ENABLE"] != null)
    				TelemetryData.UpdateStatusChecker(true, paramList["FENCE_ENABLE"].Value == 1 ? true : false);
			}
            catch { }
		}

		public void UpdateAssumeTime(double missionDistance) =>
            ActiveDroneInfo?.UpdateAssumeTime(missionDistance);

		public void ResetAssumeTime() =>
			ActiveDroneInfo?.ResetAssumeTime();

        public void NotifyMissionChanged() => ActiveDroneInfo?.NotifyMissionChanged();

    }
}
