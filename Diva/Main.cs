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

        public Main()
        {
            InitializeComponent();

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

        

    }
}
