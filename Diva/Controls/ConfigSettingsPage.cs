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
    public partial class ConfigSettingsPage : UserControl
    {
        public ConfigSettingsPage()
        {
            InitializeComponent();

            int altitudeMode = ConfigData.GetIntOption("GuidedWPAltMode");
            if (altitudeMode == 3)
                rbAlwaysPrompt.Checked = true;
            else if (altitudeMode == 2)
                rbDefaultAltitude.Checked = true;
            else if (altitudeMode == 1)
                rbMinimumAltitude.Checked = true;
            else
                rbDroneCurrent.Checked = true;

            numWPAltitude.Value = float.TryParse(ConfigData.GetOption("GuidedWPAltitude"),
                out float altitudeValue) ? (decimal)altitudeValue : 50;
            cbCreateTLog.Checked = ConfigData.GetBoolOption("GenerateTLog");
            int maxEntries = ConfigData.GetIntOption("MaxRouteEntries");
            numRouteEntriesMax.Value = maxEntries < numRouteEntriesMax.Minimum ? 200 : maxEntries;
            cbDisplayDroneRoute.Checked = ConfigData.GetBoolOption("DisplayDroneRoute");

            rbDroneCurrent.CheckedChanged += AltitudeModeChanged;
            rbMinimumAltitude.CheckedChanged += AltitudeModeChanged;
            rbDefaultAltitude.CheckedChanged += AltitudeModeChanged;
            rbAlwaysPrompt.CheckedChanged += AltitudeModeChanged;
            numWPAltitude.ValueChanged += (o, e) =>
                ConfigData.SetOption("GuidedWPAltitude", numWPAltitude.Value.ToString());
            cbCreateTLog.CheckedChanged += (o,e) =>
                ConfigData.SetBoolOption("GenerateTLog", cbCreateTLog.Checked);
            numRouteEntriesMax.ValueChanged += (o, e) =>
                ConfigData.SetIntOption("MaxRouteEntries", (int)numRouteEntriesMax.Value);
            cbDisplayDroneRoute.CheckedChanged += SetRouteDisplayEnabled;
        }

        private void AltitudeModeChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton rb) || !rb.Checked)
                return;

            int mode = 0;
            if (sender == rbMinimumAltitude)
                mode = 1;
            else if (sender == rbDefaultAltitude)
                mode = 2;
            else if (sender == rbAlwaysPrompt)
                mode = 3;

            ConfigData.SetIntOption("GuidedWPAltMode", mode);
        }

        private void SetRouteDisplayEnabled(object sender, EventArgs e)
        {
            bool enabled = cbDisplayDroneRoute.Checked;
            ConfigData.SetBoolOption("DisplayDroneRoute", enabled);
            Planner.GetPlannerInstance().GMapControl.Overlays.
                First(o => o.Id == "Routes").IsVisibile = enabled;
        }
    }
}
