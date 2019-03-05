using SharpKml.Base;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Diva.Utilities
{
	public class KMLFileUtility
	{
		public static List<WayPoint> ReadKMLMission()
		{
            using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Filter = "Google Earth KML |*.kml";
				DialogResult result = fd.ShowDialog();
				if (result == DialogResult.OK)
				{
                    return ReadKMLMissionFile(fd.FileName);
				}
				else
				{
                    Planner.log.Error("no waypoint file specified");
				}
			}
            return null;
		}

        public static List<WayPoint> ReadKMLMissionFile(string file)
        {
            List<WayPoint> cmds = new List<WayPoint>();
            string kml = "";
            string tempdir = "";

            var sr = new StreamReader(File.OpenRead(file));
            kml = sr.ReadToEnd();
            sr.Close();

            // cleanup after out
            if (tempdir != "")
                Directory.Delete(tempdir, true);

            kml = kml.Replace("<Snippet/>", "");

            var parser = new Parser();

            parser.ElementAdded += (o, a) => ProcessKMLReadMission(cmds, a);
            parser.ParseString(kml, false);

            return cmds;
        }

        private static void ProcessKMLReadMission(object sender, ElementEventArgs e)
		{
            List<WayPoint> cmds = sender as List<WayPoint>;
            Element element = e.Element;

            if (element is Document doc)
            {
                foreach (var feat in doc.Features)
                {
                    //Console.WriteLine("feat " + feat.GetType());
                    //processKML((Element)feat);
                }
            }
            else if (element is Placemark pm)
            {
                if (pm.Geometry is Point)
                {
                    var point = ((Point)pm.Geometry).Coordinate;
                    SchemaData sdata = pm.ExtendedData.SchemaData.First();
                    SimpleData[] datas = (sdata.SimpleData).ToArray();

                    WayPoint temp = new WayPoint();
                    if (datas[2].Text == "3") { temp.Option = 1; }
                    else if (datas[2].Text == "10") { temp.Option = 8; }
                    else { temp.Option = 0; }

                    temp.Id = (ushort)Enum.Parse(typeof(MAVLink.MAV_CMD), datas[3].Text, false);
                    if (temp.Id == 99) { temp.Id = 0; }

                    temp.Param1 = float.Parse(datas[4].Text, new CultureInfo("en-US"));
                    temp.Param2 = float.Parse(datas[5].Text, new CultureInfo("en-US"));
                    temp.Param3 = float.Parse(datas[6].Text, new CultureInfo("en-US"));
                    temp.Param4 = float.Parse(datas[7].Text, new CultureInfo("en-US"));
                    temp.Latitude = point.Latitude;
                    temp.Longitude = point.Longitude;
                    temp.Altitude = (float)point.Altitude;

                    cmds.Add(temp);
                }
            }
        }

		public static void SaveKMLMission(List<WayPoint> _cmds, WayPoint home)
		{
			using (SaveFileDialog fd = new SaveFileDialog())
			{
				int wpCount = 0;
				fd.Filter = "Google Earth KML |*.kml";
				fd.DefaultExt = ".kml";
				DialogResult result = fd.ShowDialog();

				if (result == DialogResult.OK)
				{
					string file = fd.FileName;
					if (file != "")
					{
						try
						{
							FileStream fs = File.Create(file);

							// This is the root element of the file
							Kml kml = new Kml();
							Document document = new Document();

							document.AddFeature(ProcessKMLSaveMission(home, true, wpCount++));

							foreach (WayPoint wp in _cmds)
							{
								document.AddFeature(ProcessKMLSaveMission(wp, false, wpCount++));
							}

							kml.Feature = document;

							Serializer serializer = new Serializer();
							serializer.Serialize(kml, fs);
							fs.Close();

						}
						catch (Exception e)
						{
							Planner.log.Error(e.ToString());
						}
					}

					Thread.Sleep(1000);
					MessageBox.Show(Properties.Strings.MsgBoxSaveMission);
					
				}
			}
		}

		private static Placemark ProcessKMLSaveMission(WayPoint wp, bool isHome, int wpCount)
		{

			Point point = new Point();
			Placemark placemark = new Placemark();

			point.Coordinate = new Vector(wp.Latitude, wp.Longitude, wp.Altitude);
			placemark.Name = isHome? "home" : "waypoint";
			placemark.Geometry = point;


			ExtendedData extendedData = new ExtendedData();
            SchemaData schemaData = new SchemaData { SchemaUrl = new Uri("https://127.0.0.1") };
            schemaData.AddData(new SimpleData() { Name = "p1", Text = wpCount.ToString() });
			schemaData.AddData(new SimpleData() { Name = "p2", Text = isHome? "1" : "0" });
			schemaData.AddData(new SimpleData() { Name = "p3", Text = isHome ? "0" : "3" });
			schemaData.AddData(new SimpleData() { Name = "p4", Text = (wp.Id).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p5", Text = (wp.Param1).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p6", Text = (wp.Param2).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p7", Text = (wp.Param3).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p8", Text = (wp.Param4).ToString() });
			schemaData.AddData(new SimpleData() { Name = "wp_speed", Text = "6m/s" });
			extendedData.AddSchemaData(schemaData);
			placemark.ExtendedData = extendedData;

			return placemark;
		}
	}
}
