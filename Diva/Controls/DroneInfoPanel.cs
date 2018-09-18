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

namespace Diva.Controls
{
    public partial class DroneInfoPanel : UserControl
    {
        private DroneInfo activeDrone = null;
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
                        DroneInfoTip.SetToolTip(activeDrone, Properties.Strings.strActivateDrone);
                    }
                    activeDrone = value;
                    if (activeDrone != null)
                    {
                        activeDrone.Activate();
                        DroneInfoTip.SetToolTip(activeDrone, Properties.Strings.strActivateDrone);
                    }
                    TelemetryData.Visible = false;
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
            dinfo.CloseButtonClicked += (s, e) =>
            {
                var d = s as DroneInfo;
                if (d != null)
                {
                    if (d != ActiveDroneInfo)
                    {
                        ActiveDroneInfo = d;
                    }
                    else
                    {
                        TelemetryData.Visible = !TelemetryData.Visible;
                    }
                }
            };
            ThePanel.Controls.Remove(TelemetryData);
            ThePanel.Controls.Add(dinfo);
            ThePanel.Controls.Add(TelemetryData);
            if (setActive)
                ActiveDroneInfo = dinfo;
            return dinfo;
        }

        public void UpdateDisplayInfo()
        {
            if (ActiveDroneInfo != null)
            {
                MavStatus status = ActiveDroneInfo.Drone.Status;
                ActiveDroneInfo.UpdateTelemetryData(status.sysid, status.battery_voltage, status.satcount);
                TelemetryData.UpdateTelemetryData(status.alt, status.verticalspeed, status.groundspeed);
                string getText(string name) =>
                    TelemetryData.Controls.Find(name, true)[0].Text;
                DroneInfoTip.SetToolTip(ActiveDroneInfo, $@"{getText("GBAltitude")}: {getText("TxtAltitude")}
{getText("GBGroundSpeed")}: {getText("TxtGroundSpeed")}
{getText("GBVerticalSpeed")}: {getText("TxtVerticalSpeed")}");
            }
        }

        public void UpdateAssumeTime(double missionDistance) =>
            ActiveDroneInfo?.UpdateAssumeTime(missionDistance);
    }
}
