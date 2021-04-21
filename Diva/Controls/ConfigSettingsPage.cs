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

            rbDroneCurrent.CheckedChanged += AltitudeModeChanged;
            rbMinimumAltitude.CheckedChanged += AltitudeModeChanged;
            rbDefaultAltitude.CheckedChanged += AltitudeModeChanged;
            rbAlwaysPrompt.CheckedChanged += AltitudeModeChanged;
            numWPAltitude.ValueChanged += NumWPAltitude_ValueChanged;
            cbCreateTLog.CheckedChanged += CbCreateTLog_CheckedChanged;
        }

        private void NumWPAltitude_ValueChanged(object sender, EventArgs e)
        {
            if (sender is NumericUpDown num)
                ConfigData.SetOption("GuidedWPAltitude", num.Value.ToString());
        }

        private void CbCreateTLog_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is CheckBox cb)
                ConfigData.SetBoolOption("GenerateTLog", cb.Checked);
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
    }
}
