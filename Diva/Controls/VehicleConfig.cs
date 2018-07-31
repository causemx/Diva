using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diva.Controls
{
    public partial class ConfigureForm : Form
    {
        private List<DroneSetting> drones;
        public List<DroneSetting> EditingDroneList => drones;
        private bool Dirty
        {
            set
            {
                SetEnabled(BtnVConfApply, value);
                SetEnabled(BtnVConfReset, value);
            }
        }

        private void InitVehicleSettings(List<DroneSetting> droneSettings = null)
        {
            VehicleSettingsPanel.Controls.Clear();
            drones = droneSettings ?? ConfigData.GetTypeList<DroneSetting>();
            DroneSettingInput.SetDefaultHandlers(DroneAdded, DroneModified, DroneRemoved);
            foreach (var d in drones)
                VehicleSettingsPanel.Controls.Add(DroneSettingInput.FromSetting(d));
            VehicleSettingsPanel.Controls.Add(new DroneSettingInput());
            Dirty = false;
            Disposed += delegate { DroneSettingInput.SetDefaultHandlers(null, null, null); };
        }

        private void DroneAdded(object sender, EventArgs e)
        {
            VehicleSettingsPanel.Controls.Add(new DroneSettingInput());
            Dirty = true;
        }

        private void DroneModified(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void DroneRemoved(object sender, EventArgs e)
        {
            VehicleSettingsPanel.Controls.Remove(sender as Control);
            Dirty = true;
        }

        private void BtnVConfImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = DroneSetting.FileExtensionFilter;
                fd.DefaultExt = "*.xml";
                if (fd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    InitVehicleSettings(DroneSetting.ImportXML(fd.FileName));
                    Dirty = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(Properties.Strings.MsgCantOpenFile.FormatWith(fd.FileName));
                }
            }
        }

        private void BtnVConfExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog fd = new SaveFileDialog())
            {
                fd.Filter = DroneSetting.FileExtensionFilter;
                fd.DefaultExt = "*.xml";
                if (fd.ShowDialog() != DialogResult.OK) return;
                try { DroneSetting.ExportXML(fd.FileName, drones); }
                catch (Exception)
                {
                    MessageBox.Show(Properties.Strings.MsgCantOpenFile.FormatWith(fd.FileName));
                }
            }
        }

        private void BtnVConfApply_Click(object sender, EventArgs e)
        {
            ConfigData.UpdateList(EditingDroneList);
            Dirty = false;
        }

        private void BtnVConfReset_Click(object sender, EventArgs e)
        {
            InitVehicleSettings();
            Dirty = false;
        }
    }
}