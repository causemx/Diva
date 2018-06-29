using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Controls
{
	public partial class MyListView : ListView
	{
		public MyListView()
		{
			InitializeComponent();
		}

		public string _focusFile;

		public string FocusFile
		{
			get { return _focusFile; }
			set { _focusFile = value; }
		}


	}
}
