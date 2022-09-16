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
using Diva.Properties;
using Diva.Utilities;

namespace Diva.Controls
{
	public partial class DroneInfoPanel : UserControl
	{
		private DroneInfo activeDrone;
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
            // TelemetryData.Height = 0; // disable display
		}

		public DroneInfo AddDrone(MavDrone drone, bool setActive = true)
		{
			var dinfo = new DroneInfo(drone, drone.Name);

            dinfo.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    ActiveDroneInfo = s as DroneInfo;
                else if (e.Button == MouseButtons.Right)
                    Mission.DroneMission.GetMission((s as DroneInfo).Drone).ChangeRouteColor();
            };
			dinfo.DoubleClick += (s, e) =>
			{
                if (e is MouseEventArgs m && m.Button == MouseButtons.Right
                        || Planner.MIRDCMode)
                    return;
				ThePanel.Controls.Remove(s as Control);
				if (s == ActiveDroneInfo)
                {
					ActiveDroneInfo = null;
                    TelemetryData.Visible = false;
                }
				drone.Disconnect();
				DroneClosed?.Invoke(s, e);
			};
			dinfo.ToggleTelemetryInfo += (s, e) =>
			{
				if (s is DroneInfo d)
				{
					ActiveDroneInfo = d;
					bool expanded = TelemetryData.Visible = !TelemetryData.Visible;
                    foreach (var c in ThePanel.Controls)
                        (c as DroneInfo)?.SetExpanded(expanded);
                }
			};

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
            foreach (var i in ThePanel.Controls.Cast<Control>().Where(c => c is DroneInfo))
            {
                try
                {
                    var di = i as DroneInfo;
					bool arm_status = false;

                    if (di == ActiveDroneInfo)
                    {
                        var status = di.Drone.Status;
						arm_status = status.IsArmed;
                        TelemetryData.UpdateTelemetryData(status.Altitude,
                            status.VerticalSpeed, status.GroundSpeed, status.AirSpeed);
                        string getText(string name) =>
                            TelemetryData.Controls.Find(name, true)[0].Text;
                        DroneInfoTip.SetToolTip(di, $@"{"Altitude"}: {getText("labelAltitude")}
{"GroundSpeed"}: {getText("labelGroundspeed")}
{"VerticalSpeed"}: {getText("labelVerticalspeed")}
({status.Latitude}, {status.Longitude})");
                    }
					di.IsArming(arm_status);
                    di.UpdateTelemetryData(DroneInfoTip);
                    MAVLink.MAVLinkParamList paramList = ActiveDroneInfo.Drone.Status.Params;
                    if (paramList["FENCE_ENABLE"] != null)
                        TelemetryData.UpdateStatusChecker(true, paramList["FENCE_ENABLE"].Value == 1 ? true : false);
                }
                catch(Exception e) { Planner.log.Error(e.ToString()); }
            }
		}

        public void NotifyMissionChanged() => ActiveDroneInfo?.NotifyMissionChanged();

        public bool HUDShown => TelemetryData.Visible;
    }
}
