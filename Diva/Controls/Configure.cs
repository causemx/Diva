using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Diva.Controls
{
	public partial class Configure : Form
	{
        private Dictionary<Button, Panel> pages;
        private MyGMap gmap;
        private bool disableSelectionUpate;
        private string loginAccount = AccountManager.GetLoginAccount();

		public Configure(MyGMap map)
        {
            InitializeComponent();

            gmap = map;
            pages = new Dictionary<Button, Panel>()
            {
                { BtnMap, PanelMapControls },
                { BtnAccount, PanelAccountConfig }
            };
            UpdateTabPages(BtnAbout);

            InitMapPage();
            MapConfigDirty = false;
            InitAccountPage();
        }

        private void UpdateTabPages(Button b)
        {
            foreach (var p in pages)
                p.Value.Visible = b == p.Key;
        }

        private void MenuButton_Click(object sender, EventArgs e)
		{
            var btn = sender as Button;

            if (btn == null || btn.Top == IndicatorPanel.Top)
                return;

			IndicatorPanel.Height = btn.Height;
			IndicatorPanel.Top = btn.Top;
            UpdateTabPages(btn);

            /*switch (((Button)sender).Name)
			{
				case "BtnVehicle":
					break;
				case "BtnGeoFence":
					break;
				case "BtnTuning":
					break;
                case "BtnMap":
                    break;
                case "BtnAccount":
                    break;
                case "BtnAbout":
					break;
			}*/
        }

        public static void SetEnabled(Control c, bool enabled)
        {
            c.Enabled = enabled;
            if (c is Button)
            {
                c.BackColor = enabled ? SystemColors.InactiveCaptionText : Color.Gray;
            } else if (c is Label)
            {
                c.Enabled = true;
                c.ForeColor = enabled ? Color.White : Color.Gray;
            }
        }

        #region Account Configuration
        private void InitAccountPage()
        {
            if (loginAccount == "")
            {
                if (ConfigData.GetOption(ConfigData.OptionName.SkipNoAccountAlert) == "true")
                {
                    BtnAccount.Visible = false;
                    BtnAbout.Top = BtnAccount.Top;
                }
                else
                    LabelLoginAccount.Text += "(Anonymous)";
            }
            else
                LabelLoginAccount.Text += loginAccount;

            LoadAccount();
        }

        private void LoadAccount()
        {
            string acc = CBoxAccounts.Text;
            disableSelectionUpate = true;
            if (CBoxAccounts.Items.Count > 0)
                CBoxAccounts.Items.Clear();
            else
                acc = loginAccount;
            var accs = AccountManager.GetAccounts();
            foreach (string s in accs)
                CBoxAccounts.Items.Add(s);
            disableSelectionUpate = false;
            CBoxAccounts.Text = acc;
            UpdateAccountPanelControls();
        }

        private void UpdateAccountPanelControls()
        {
            bool accoutExist = CBoxAccounts.FindStringExact(CBoxAccounts.Text) >= 0;
            bool passwordConfirmed = TBoxPassword.Text != "" &&
                TBoxPassword.Text == TBoxConfirmPassword.Text &&
                AccountManager.IsValidPassword(TBoxPassword.Text);
            PanelExistingAccountControls.Visible = accoutExist;
            if (accoutExist)
            {
                SetEnabled(BtnDeleteAccount, CBoxAccounts.Text != AccountManager.GetLoginAccount());
                SetEnabled(BtnChangePassword, passwordConfirmed);
            } else
                SetEnabled(BtnCreateAccount, passwordConfirmed);
        }

        private void PasswordInputUpdate(object o, EventArgs e) => UpdateAccountPanelControls();

        private void CBoxAccounts_TextUpdate(object sender, EventArgs e)
        {
            if (disableSelectionUpate) return;

            int i = CBoxAccounts.SelectedIndex;
            int n = CBoxAccounts.FindStringExact(CBoxAccounts.Text);
            if (i != n)
                CBoxAccounts.SelectedItem = CBoxAccounts.Text;
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }

        private void CBoxAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }

        private void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Create account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.CreateAccount(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Really delete account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.DeleteAccount(CBoxAccounts.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            LoadAccount();
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Chage password for account '{CBoxAccounts.Text}'?", "Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            AccountManager.ChangePassword(CBoxAccounts.Text, TBoxPassword.Text);
            TBoxPassword.Text = TBoxConfirmPassword.Text = "";
            UpdateAccountPanelControls();
        }
        #endregion

        #region Map Configuration
        private bool mapConfigDirty;
        private bool MapConfigDirty { get => mapConfigDirty;
            set
            {
                mapConfigDirty = value;
                SetEnabled(BtnMapConfigApply, value);
                SetEnabled(BtnMapConfigReset, value);
            }
        }
        private void InitMapPage()
        {
            TBoxMapCacheLocation.Text = gmap.CacheLocation;

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
            (ConfigData.GetOption(ConfigData.OptionName.UseImageMap) == "true" ?
                RBtnIndoorMap : RBtnGlobalMap).Checked = true;
        }

        private void MapControl_RadioCheckedChanged(object sender, EventArgs e)
        {
            void updateControls(Control container, bool enabled)
            {
                foreach (Control c in container.Controls)
                    SetEnabled(c, enabled);
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
            ConfigData.SetOption(ConfigData.OptionName.ImageMapSource, TBoxIndoorMapLocation.Text);
            bool imagemap = RBtnIndoorMap.Checked;
            ConfigData.SetOption(ConfigData.OptionName.UseImageMap, imagemap.ToString());
            if (imagemap || gmap.MapProvider != GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance)
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
        #endregion
    }
}
