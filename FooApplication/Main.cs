using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using GMap.NET.WindowsForms;
using GMap.NET;
using FooApplication.Mavlink;

namespace FooApplication
{
	public partial class Main : Form
	{

		public static readonly ILog log = LogManager.GetLogger(typeof(Main));

		public static MavlinkInterface comPort
		{
			get
			{
				return _comPort;
			}
			set
			{
				if (_comPort == value)
					return;
				_comPort = value;
			}
		}

		public static MavlinkInterface _comPort = new MavlinkInterface();

		public Main()
		{
			InitializeComponent();
		}

		private void gMapControl1_Load(object sender, EventArgs e)
		{
			gmapControl.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			gmapControl.SetPositionByKeywords("Paris, France");
			GMapOverlay overlay1 = new GMapOverlay("overlay1");
			gmapControl.Overlays.Add(overlay1);
		}

		private void btn_connect_Click(object sender, EventArgs e)
		{

		}
	}

}
