using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
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
	public class KMLFileUtility
	{
		private List<Locationwp> cmds = null;
		internal string wpfilename;

		public KMLFileUtility()
		{
			this.cmds = new List<Locationwp>();
		}

		public List<Locationwp> ReadKMLMission()
		{
			cmds.Clear();

			using (OpenFileDialog fd = new OpenFileDialog())
			{
				fd.Filter = "Google Earth KML |*.kml;*.kmz";
				DialogResult result = fd.ShowDialog();
				try
				{
					string file = fd.FileName;
					try
					{
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

						parser.ElementAdded += ProcessKMLMission;
						parser.ParseString(kml, false);
					}
					catch (Exception e1)
					{
						MessageBox.Show("Bad kml error" + e1);
					}
				}
				catch( Exception e2)
				{
					MessageBox.Show("Can not find the file" + e2);
				}
				return cmds;
			}
		}

		private void ProcessKMLMission(object sender, ElementEventArgs e)
		{
			Element element = e.Element;
			Document doc = element as Document;
			Placemark pm = element as Placemark;

			if (doc != null)
			{
				foreach (var feat in doc.Features)
				{
					//Console.WriteLine("feat " + feat.GetType());
					//processKML((Element)feat);
				}
			}
			else if (pm != null)
			{
				if (pm.Geometry is SharpKml.Dom.Point)
				{
					var point = ((SharpKml.Dom.Point)pm.Geometry).Coordinate;
					SchemaData sdata = pm.ExtendedData.SchemaData.First();
					SimpleData[] datas = (sdata.SimpleData).ToArray();

					Locationwp temp = new Locationwp();
					if (datas[2].Text == "3") { temp.options = 1; }
					else if (datas[2].Text == "10") { temp.options = 8; }
					else { temp.options = 0; }

					temp.id = (ushort)Enum.Parse(typeof(MAVLink.MAV_CMD), datas[3].Text, false);
					if (temp.id == 99) { temp.id = 0; }

					temp.p1 = float.Parse(datas[4].Text, new CultureInfo("en-US"));
					temp.p2 = (float)(double.Parse(datas[5].Text, new CultureInfo("en-US")));
					temp.p3 = (float)(double.Parse(datas[6].Text, new CultureInfo("en-US")));
					temp.p4 = (float)(double.Parse(datas[7].Text, new CultureInfo("en-US")));
					temp.lat = point.Latitude;
					temp.lng = point.Longitude;
					temp.alt = (float)point.Altitude;

					cmds.Add(temp);
				}
			}
		}


		public void SaveKMLMission(List<Locationwp> _cmds, Locationwp home)
		{
			using (SaveFileDialog fd = new SaveFileDialog())
			{
				fd.Filter = "Google Earth KML |*.kml;*.kmz";
				fd.DefaultExt = ".kml";
				fd.FileName = wpfilename;
				DialogResult result = fd.ShowDialog();
				string file = fd.FileName;
				if (file != "")
				{
					try
					{
						// This is the root element of the file
						Kml kml = new Kml();

						Point point = new Point();
						Document document = new Document();
						Placemark placemark = new Placemark();

						point.Coordinate = new Vector(home.lng, home.lat, home.alt);
						placemark.Name = "Cool Statue";
						placemark.Geometry = point;

						
						ExtendedData extendedData = new ExtendedData();
						SchemaData schemaData = new SchemaData();
						schemaData.SchemaUrl = new Uri("http://192.168.0.1");
						schemaData.AddData(new SimpleData() { Name="p1", Text="00"});
						extendedData.AddSchemaData(schemaData);
						placemark.ExtendedData = extendedData;

						document.AddFeature(placemark);
						kml.Feature = document;

						Serializer serializer = new Serializer();
						serializer.Serialize(kml);
						Console.WriteLine(serializer.Xml);
							

					}
					catch (Exception e)
					{
						Planner.log.Error(e.ToString());
					}
				}
			}
		}

	}
}
