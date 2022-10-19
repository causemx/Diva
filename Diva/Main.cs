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
        public Page1 page1;
        public Page2 page2;
        PageManager MyView;

        public Main()
        {
            InitializeComponent();

            MyView = new PageManager(this);
            page1 = new Page1();
            page2 = new Page2();

            MyView.AddScreen(new PageManager.Screen("page1", page1, true));
            MyView.AddScreen(new PageManager.Screen("page2", page2, true));

            MyView.ShowScreen("page1");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyView.ShowScreen("page1");
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
    }
}
