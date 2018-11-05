using Diva.Mavlink;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diva.Utilities
{
	class FileUtility : IFileUtility
	{
		internal string wpfilename;
		private MavlinkInterface activeDrone;
		private List<Locationwp> cmds;
		private Locationwp home;

		public FileUtility(MavlinkInterface _activeDrone, List<Locationwp> _cmds, Locationwp _home)
		{
			this.activeDrone = _activeDrone;
			this.cmds = _cmds;
			this.home = _home;
		}

		public void Read()
		{
			throw new NotImplementedException();
		}
				
		public void Write()
		{
			using (SaveFileDialog fd = new SaveFileDialog())
			{
				fd.Filter = "Mission|*.waypoints;*.txt|Mission JSON|*.mission";
				fd.DefaultExt = ".waypoints";
				fd.FileName = wpfilename;
				DialogResult result = fd.ShowDialog();
				string file = fd.FileName;
				string file2 = Path.GetDirectoryName(fd.FileName) + "/param.txt";

				try
				{
					StreamWriter sw = new StreamWriter(file2);
					sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED"]);
					sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED_UP"]);
					sw.WriteLine(activeDrone.Status.param["WPNAV_SPEED_DN"]);
					sw.WriteLine(activeDrone.Status.param["LAND_SPEED"]);
					sw.WriteLine(activeDrone.Status.param["LAND_SPEED_HIGH"]);
					sw.WriteLine(activeDrone.Status.param["RTL_SPEED"]);
					sw.WriteLine(activeDrone.Status.param["RTL_ALT"]);
					sw.WriteLine(activeDrone.Status.param["WPNAV_ACCEL"]);
					sw.WriteLine(activeDrone.Status.param["WPNAV_ACCEL_Z"]);
					sw.Close();
					sw.Dispose();
				}
				catch (Exception e1)
				{
				}

				if (file != "")
				{
					try
					{
						StreamWriter sw = new StreamWriter(file);
						sw.WriteLine("QGC WPL 110");
						try
						{
							sw.WriteLine("0\t1\t0\t16\t0\t0\t0\t0\t" +
											(home.lat).ToString("0.000000", new CultureInfo("en-US")) +
											"\t" +
											(home.lng).ToString("0.000000", new CultureInfo("en-US")) +
											"\t" +
											(home.alt).ToString("0.000000", new CultureInfo("en-US")) +
											"\t1");
						}
						catch (Exception ex)
						{
							Planner.log.Error(ex);
							sw.WriteLine("0\t1\t0\t0\t0\t0\t0\t0\t0\t0\t0\t1");
						}

						int count = 0;

						foreach (Locationwp wp in cmds)
						{
							sw.Write((count + 1)); // seq
							sw.Write("\t" + 0); // current
							sw.Write("\t" + "Relative"); //frame 
							sw.Write("\t" + wp.id);
							sw.Write("\t" + wp.p1);
							sw.Write("\t" + wp.p2);
							sw.Write("\t" + wp.p3);
							sw.Write("\t" + wp.p4);
							sw.Write("\t" + wp.lat);
							sw.Write("\t" + wp.lng);
							sw.Write("\t" + wp.alt);
							sw.Write("\t" + 1);
							sw.WriteLine("");

							count = count + 1;
						}

						sw.Close();
						sw.Dispose();
						MessageBox.Show("Mission Saved");
					}
					catch (Exception ex)
					{
						Planner.log.Error(ex);
					}
				}
			}
		}


	}
}
