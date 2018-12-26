using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;
using ResStrings = Diva.Properties.Strings;

namespace Diva.Controls
{
    public partial class ConfigMapPage : UserControl
    {
        private MyGMap gmap;
        private bool mapConfigDirty = false;
        private bool mapConfigDirtyUpdate = false;
        private bool MapConfigDirty
        {
            get => mapConfigDirty;
            set
            {
                mapConfigDirty = value;
                if (mapConfigDirtyUpdate)
                {
                    ConfigureForm.SetEnabled(BtnMapConfigApply, value);
                    ConfigureForm.SetEnabled(BtnMapConfigReset, value);
                }
            }
        }

        public ConfigMapPage()
        {
            InitializeComponent();
            this.UpdateLocale();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                Control c = Parent;
                Form f;
                while (c != null && !(c is Form)) c = c.Parent;
                for (f = c as Form; f != null; f = f.Owner)
                    if (f is Planner)
                    {
                        gmap = (f as Planner).GMapControl;
                        break;
                    }
                InitMapPage();
                MapConfigDirty = false;
            }
        }

        private void InitMapPage()
        {
            mapConfigDirtyUpdate = false;
            TBoxMapCacheLocation.Text = gmap == null ?
                ConfigData.GetOption(ConfigData.OptionName.MapCacheLocation) :
                gmap.CacheLocation;

            double lng = Planner.DEFAULT_LONGITUDE, lat = Planner.DEFAULT_LATITUDE, zoom = Planner.DEFAULT_ZOOM;

            string loc = ConfigData.GetOption(ConfigData.OptionName.MapInitialLocation);
            if (loc != "")
            {
                string[] locs = loc.Split('|');
                if (locs.Length > 1)
                {
                    double.TryParse(locs[0], out lat);
                    double.TryParse(locs[1], out lng);
                    if (locs.Length > 2)
                        double.TryParse(locs[2], out zoom);
                }
            }
            TBoxIPLatitude.Text = lat.ToString();
            TBoxIPLongitude.Text = lng.ToString();
            TBoxInitialZoom.Text = zoom.ToString();

            string proxy = ConfigData.GetOption(ConfigData.OptionName.MapProxy);
            string[] prox = proxy.Split(':');
            if (prox.Length == 2)
            {
                RBtnProxyCustom.Checked = true;
                TBoxProxyHost.Text = prox[0];
                TBoxProxyPort.Text = prox[1];
            }
            else
            {
                RBtnProxySystem.Checked = true;
                TBoxProxyHost.Text = TBoxProxyPort.Text = "";
            }

            TBoxIndoorMapLocation.Text = ConfigData.GetOption(ConfigData.OptionName.ImageMapSource);
            (ConfigData.GetBoolOption(ConfigData.OptionName.UseImageMap)
                ? RBtnIndoorMap : RBtnGlobalMap).Checked = true;
            mapConfigDirtyUpdate = true;
        }

        private void MapControl_RadioCheckedChanged(object sender, EventArgs e)
        {
            void updateControls(Control container, bool enabled)
            {
                foreach (Control c in container.Controls)
                    ConfigureForm.SetEnabled(c, enabled);
            }
            if (!(sender as RadioButton).Checked) return;
            updateControls(PanelGlobalMapControls, RBtnGlobalMap.Checked);
            updateControls(PanelIndoorMapControls, RBtnIndoorMap.Checked);
            MapConfigDirty = true;
        }

        private void MapConfigChanged(object sender, EventArgs e)
            => MapConfigDirty = true;

        private void BtnMapConfigReset_Click(object sender, EventArgs e)
        {
            InitMapPage();
            MapConfigDirty = false;
        }

        private void BtnMapConfigApply_Click(object sender, EventArgs e)
        {
            MapConfigDirty = false;
            ConfigData.SetOption(ConfigData.OptionName.MapCacheLocation, TBoxMapCacheLocation.Text);
            double lat = Planner.DEFAULT_LATITUDE, lng = Planner.DEFAULT_LONGITUDE, zoom = Planner.DEFAULT_ZOOM;
            double.TryParse(TBoxIPLatitude.Text, out lat);
            double.TryParse(TBoxIPLongitude.Text, out lng);
            double.TryParse(TBoxInitialZoom.Text, out zoom);
            ConfigData.SetOption(ConfigData.OptionName.MapInitialLocation,
                $"{lat},{lng},{zoom}");
            int.TryParse(TBoxProxyPort.Text, out int port);
            ConfigData.SetOption(ConfigData.OptionName.MapProxy,
                RBtnProxySystem.Checked ? "System" : $"{TBoxProxyHost.Text}:{port}");
            ConfigData.SetOption(ConfigData.OptionName.ImageMapSource, TBoxIndoorMapLocation.Text);
            bool imagemap = RBtnIndoorMap.Checked;
            ConfigData.SetOption(ConfigData.OptionName.UseImageMap, imagemap.ToString());
            if (gmap != null && (imagemap || gmap.MapProvider != gmap.GlobalMapProvider))
                gmap.ResetMapProvider();
        }

        private void BtnBrowseMapLocation_Click(object sender, EventArgs e)
        {
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.EnsurePathExists = true;
                dlg.IsFolderPicker = true;
                dlg.InitialDirectory = TBoxMapCacheLocation.Text;
                if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
                    TBoxMapCacheLocation.Text = dlg.FileName;
            }
        }

        private void BtnBrowseIndoorMap_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.CheckFileExists = true;
                var result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                    TBoxIndoorMapLocation.Text = dlg.FileName;
            }
        }

        private void ProxySetting_RadioCheckedChanged(object sender, EventArgs e)
        {
            ConfigureForm.SetEnabled(TBoxProxyHost, RBtnProxyCustom.Checked);
            ConfigureForm.SetEnabled(TBoxProxyPort, RBtnProxyCustom.Checked);
            MapConfigDirty = true;
        }
    }
}
