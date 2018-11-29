using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.EnergyConsumption;

namespace Diva.Controls
{
    public partial class NewPowerModelForm : Form
    {
        public NewPowerModelForm()
        {
            InitializeComponent();
            BtnDataCollectionTab.Checked = true;
        }

        public string NewPowerModelName { get; private set; }

        private void TabRadio_CheckChanged(object sender, EventArgs e)
        {
            PanelCollection.Visible = BtnDataCollectionTab.Checked;
            PanelAnalysis.Visible = BtnDataAnalysisTab.Checked;
        }

        private void BtnActions_Click(object sender, EventArgs e)
        {
            if (sender == BtnActionOk)
            {
                if (BtnDataAnalysisTab.Checked)
                {
                    string name = TBoxModelName.Text;
                    if (!PowerModel.IsValidModelName(name))
                    {
                        MessageBox.Show(Properties.Strings.MsgInvalidPowerModelName.FormatWith(name));
                        return;
                    }
                    bool ok = false;
                    try
                    {
                        ok = new System.IO.FileInfo(TBoxLogFileLocation.Text).Exists;
                    } catch { }
                    if (!ok)
                    {
                        MessageBox.Show(Properties.Strings.MsgPowerLogFileNotFound);
                        return;
                    }
                    // generate power model here
                    PowerModelTools.Trainer.Start(TBoxLogFileLocation.Text, name);
                    //PowerModelTools.Predictor.RunAsTask(TBoxLogFileLocation.Text + '|' + name, null);
                    PowerModel.RefreshPowerModelsList();
                    if (PowerModel.GetModel(name) != PowerModel.PowerModelNone)
                    {
                        NewPowerModelName = name;
                        DialogResult = DialogResult.Yes;
                    } else
                    {
                        DialogResult = DialogResult.Abort;
                    }
                } else
                {
                    // generate training mission here
                    DialogResult = DialogResult.OK;
                }
            } else
            {
                DialogResult = DialogResult.Cancel;
            }
            Close();
        }

        private void BtnLogFileBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = Properties.Strings.StrPowerLogFileFilter;
                dlg.InitialDirectory = TBoxLogFileLocation.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                    TBoxLogFileLocation.Text = dlg.FileName;
            }
        }
    }
}
