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
    public partial class DroneInfoPanel : UserControl
    {
        private DroneInfo activeDrone = null;
        public DroneInfo ActiveDrone
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
        public event EventHandler DroneClosed;

        public DroneInfoPanel()
        {
            InitializeComponent();
            TelemetryData.Visible = false;
        }

        private DroneInfo newDroneInfo(string name)
        {
            var drone = new DroneInfo(new Mavlink.MavlinkInterface(), name);
            drone.Click += (s, e) => ActiveDrone = s as DroneInfo;
            drone.DoubleClick += (s, e) =>
            {
                ThePanel.Controls.Remove(s as Control);
                if (s == ActiveDrone)
                    ActiveDrone = null;
                DroneClosed?.Invoke(s, e);
            };
            drone.CloseButtonClicked += (s, e) =>
            {
                var d = s as DroneInfo;
                if (d != null)
                {
                    if (d != ActiveDrone)
                    {
                        ActiveDrone = d;
                    } else
                    {
                        TelemetryData.Visible = !TelemetryData.Visible;
                    }
                }
            };
            return drone;
        }

        public void AddDrone(DroneSetting drone, bool setActive = true)
        {
            var dinfo = newDroneInfo(drone.Name);
            ThePanel.Controls.Remove(TelemetryData);
            ThePanel.Controls.Add(dinfo);
            ThePanel.Controls.Add(TelemetryData);
            if (setActive)
                ActiveDrone = dinfo;
        }

        public void UpdateDroneInfo(byte sysid, double battry_voltage, float satellite_count) =>
            ActiveDrone?.UpdateTelemetryData(sysid, battry_voltage, satellite_count);

        public void UpdateTelemetryData(double altitude, double verticalSpeed, double groundSpeed)
        {
            TelemetryData.UpdateTelemetryData(altitude, verticalSpeed, groundSpeed);
            if (ActiveDrone != null)
            {
                string getText(string name) =>
                    TelemetryData.Controls.Find(name, true)[0].Text;
                DroneInfoTip.SetToolTip(ActiveDrone, $@"{getText("GBAltitude")}: {getText("TxtAltitude")}
{getText("GBGroundSpeed")}: {getText("TxtGroundSpeed")}
{getText("GBVerticalSpeed")}: {getText("TxtVerticalSpeed")}");
            }
        }

        public void UpdateAssumeTime(double missionDistance) =>
            ActiveDrone?.UpdateAssumeTime(missionDistance);
    }
}
