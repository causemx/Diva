using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class ConfigureForm : Form
	{
        private Dictionary<Button, Control> pages;
        public string InitPage = "";

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
                { BtnAbout, AboutBoxPanel }
            };
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            Button initButton = BtnAbout;
            if (InitPage != null && InitPage != "")
            {
                var s = InitPage.ToLower();
                var m = pages.Keys.Where(b => b.Name.ToLower().Contains(s));
                if (m.Count() == 1)
                    initButton = m.First();
            }
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
            var btn = sender as Button;

            if (btn == null || btn.Top == IndicatorPanel.Top)
                return;

			IndicatorPanel.Height = btn.Height;
			IndicatorPanel.Top = btn.Top;
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
