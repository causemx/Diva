using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls.Dialog
{
    public partial class DroneinfoDetailDialog : Form
    {
        public string Title
        {
            set { titleLabel.Text = value; }
            get { return titleLabel.Text; }
        }

        public DroneinfoDetailDialog()
        {
            InitializeComponent();
        }

        public void AddRowToPanel(Tuple<string, string>[] rowElements)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rowElements.Length; i++)
                sb.Append(rowElements[i].Item1).Append("\t").Append(rowElements[i].Item2).Append("\n");
            contentLabel.Text = sb.ToString();
        }

        private void CloseButton_Click(object sender, EventArgs e) => this.Close();

    }
}
