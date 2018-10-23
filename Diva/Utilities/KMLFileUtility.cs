﻿using SharpKml.Base;
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

						parser.ElementAdded += ProcessKMLReadMission;
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

		private void ProcessKMLReadMission(object sender, ElementEventArgs e)
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
				wpCount = 0;
				fd.Filter = "Google Earth KML |*.kml";
				fd.DefaultExt = ".kml";
				fd.FileName = wpfilename;
				DialogResult result = fd.ShowDialog();
				string file = fd.FileName;
				
				if (file != "")
				{
					try
					{
						FileStream fs = File.Create(file);

						// This is the root element of the file
						Kml kml = new Kml();
						Document document = new Document();

						document.AddFeature(ProcessKMLSaveMission(home, true));
					
						foreach (Locationwp wp in _cmds)
						{
							document.AddFeature(ProcessKMLSaveMission(wp, false));
						}

						kml.Feature = document;

						Serializer serializer = new Serializer();
						serializer.Serialize(kml, fs);

					}
					catch (Exception e)
					{
						Planner.log.Error(e.ToString());
					}
				}
			}
		}

		private int wpCount;

		private Placemark ProcessKMLSaveMission(Locationwp wp, bool isHome)
		{

			Point point = new Point();
			Placemark placemark = new Placemark();

			point.Coordinate = new Vector(wp.lat, wp.lng, wp.alt);
			placemark.Name = isHome? "home" : "waypoint";
			placemark.Geometry = point;


			ExtendedData extendedData = new ExtendedData();
			SchemaData schemaData = new SchemaData();
			schemaData.SchemaUrl = new Uri("https://127.0.0.1");
			schemaData.AddData(new SimpleData() { Name = "p1", Text = wpCount.ToString() });
			schemaData.AddData(new SimpleData() { Name = "p2", Text = isHome? "1" : "0" });
			schemaData.AddData(new SimpleData() { Name = "p3", Text = isHome ? "0" : "3" });
			schemaData.AddData(new SimpleData() { Name = "p4", Text = (wp.id).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p5", Text = (wp.p1).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p6", Text = (wp.p2).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p7", Text = (wp.p3).ToString() });
			schemaData.AddData(new SimpleData() { Name = "p8", Text = (wp.p4).ToString() });
			schemaData.AddData(new SimpleData() { Name = "wp_speed", Text = "6m/s" });
			extendedData.AddSchemaData(schemaData);
			placemark.ExtendedData = extendedData;

			wpCount = wpCount + 1;

			return placemark;

		}
	}
}
