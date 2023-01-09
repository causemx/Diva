using Diva.Controls.Components;
using Diva.Controls.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva
{
    public partial class Main : Form
    {
        public Planner planner;
        public Page2 page2;
        PageManager MyView;
        private static Main MyMain = null;
        internal static Main GetInstance() => MyMain;

        public bool ConnectState 
        {
            get { return connectButton.Checked; }
            set { connectButton.Checked = value; }
        }


    public Main()
        {
            InitializeComponent();

            MyMain = this;

            MyView = new PageManager(thePanel);
            planner = new Planner();
            page2 = new Page2();

            MyView.AddScreen(new PageManager.Screen("planner", planner, true));
            MyView.AddScreen(new PageManager.Screen("page2", page2, true));

            MyView.ShowScreen("planner");
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            MyView.ShowScreen("planner");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MyView.ShowScreen("page2");
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            dropdownMenuOperation.Show(iconButton1, iconButton1.Width, 0);
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            dropdownMenuPlanning.Show(iconButton2, iconButton2.Width, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            planner.Planner_FormClosing();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            planner.Planner_FormClosed();
        }

        private void MenuButtons_Click(object sender, EventArgs e)
        {
            var btn = (MyButton)sender;
            switch (btn.Name)
            {
                case "connectButton":
                    planner.BUT_Connect_Click(sender, e);
                    break;
                case "configButton":
                    planner.BUT_Configure_Click(sender, e);
                    break;
            }
        }

        private void Buttons_Click(object sender, EventArgs e)
        {
            var btn = (IconButton)sender;
            switch (btn.Name)
            {
                case "BtnStreaming":
                    planner.BUT_Live_Click(sender, e);
                    break;
            }
        }

        private void MenuItems_Click(object sender, EventArgs e)
        {
            var btn = (ToolStripMenuItem)sender;
            switch (btn.Name)
            {
                case "throttleMenuItem":
                    planner.BUT_Arm_Click(sender, e);
                    break;
                case "landMenuItem":
                    planner.BUT_Land_Click(sender, e);
                    break;
                case "autoMenuItem":
                    planner.BUT_Auto_Click(sender, e);
                    break;
                case "rtlMenuItem":
                    planner.BUT_RTL_Click(sender, e);
                    break;
                case "writeMissionMenuItem":
                    planner.BUT_write_Click(sender, e);
                    break;
                case "readMissionMenuItem":
                    planner.BUT_read_Click(sender, e);
                    break;
                case "importMissionMenuItem":
                    // planner.BUT_RTL_Click(sender, e);
                    break;
                case "exportMissionMenuItem":
                    // planner.BUT_RTL_Click(sender, e);
                    break;
            }
        }
    }
}