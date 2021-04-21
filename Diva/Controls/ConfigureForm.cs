using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class ConfigureForm : Form
	{
        private Dictionary<Button, Control> pages;
        public static string initPage = "About";
        public static string InitPage
        {
            get => initPage;
            set
            {
                if (!string.IsNullOrEmpty(value))
                    initPage = value.ToLower();
            }
        }

		public ConfigureForm()
        {
            InitializeComponent();
            InitVehicleSettings();
            InitAboutBox();

            pages = new Dictionary<Button, Control>()
            {
                { BtnVehicle, VehicleConfigPanel },
                { BtnGeoFence, configGeoFencePage },
                { BtnMap, configMapPage },
                { BtnSettings, configSettingsPage },
                { BtnAbout, AboutBoxPanel }
            };

            if (Planner.MIRDCMode)
            {
                List<Control> mirdcPageKeys = new List<Control> { BtnVehicle, BtnSettings, BtnAbout };
                int rtop = pages.Keys.First().Top;
                var keys = pages.Keys.ToList();
                keys.ForEach(k =>
                {
                    if (mirdcPageKeys.Contains(k))
                    {
                        k.Top = rtop;
                        rtop += k.Height;
                    }
                    else
                    {
                        k.Visible = false;
                        pages[k].Visible = false;
                        pages.Remove(k);
                    }
                });
            }
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            Button initButton = BtnAbout;
            var m = pages.Keys.Where(b => b.Name.ToLower().Contains(InitPage));
            if (m.Count() == 1)
                initButton = m.First();
            IndicatorPanel.Top = 0;
            MenuButton_Click(initButton, null);
        }

        private void UpdateTabPages(Button b)
        {
            foreach (var p in pages)
                p.Value.Visible = b == p.Key;
        }

        private void MenuButton_Click(object sender, EventArgs e)
		{
            if (!(sender is Button btn) || btn.Top == IndicatorPanel.Top)
                return;

            IndicatorPanel.Height = btn.Height;
			IndicatorPanel.Top = btn.Top;
            InitPage = btn.Name;
            UpdateTabPages(btn);
        }

        public static void SetEnabled(Control c, bool enabled, bool apply = true)
        {
            if (apply)
                c.Enabled = enabled;
            else
                enabled &= c.Enabled;
            if (c is Button)
            {
                c.BackColor = enabled ? Color.Black : Color.Gray;
            } else if (c is Label)
            {
                c.Enabled = true;
                c.ForeColor = enabled ? Color.White : Color.Gray;
            }
        }
    }
}
