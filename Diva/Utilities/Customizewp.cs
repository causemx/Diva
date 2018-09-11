using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Diva.Utilities
{
	public class Customizewp
	{
		public ushort Id { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public float Alt { get; set; }

		public Customizewp(ushort id, double lat, double lng, float alt)
		{
			this.Id = id;
			this.Lat = lat;
			this.Lng = lng;
			this.Alt = alt;
		}

		public static PointLatLngAlt ConvertToPoint(Customizewp cp)
		{
			return new PointLatLngAlt(cp.Lat, cp.Lng);
		}

		public static Dictionary<string, List<Customizewp>> ImportOverlayXML(string file)
		{
			Dictionary<string, List<Customizewp>> cpDict = new Dictionary<string, List<Customizewp>>();
			try
			{
				XElement root = XElement.Load(file);
				IEnumerable<XElement> polygons = root.Elements("Polygon");

				for (int i = 0; i < polygons.Count(); i++)
				{
					XElement polygon = polygons.ElementAt(i);
					List<Customizewp> cpList = new List<Customizewp>();
					string _key = polygon.Attribute("Type").Value;
					IEnumerable<XElement> points = polygon.Elements("Point");
					foreach (XElement point in points)
					{

						cpList.Add(new Customizewp(
							(ushort)Convert.ToInt16(point.Element("Id").Value), 
							Double.Parse(point.Element("Lat").Value), 
							Double.Parse(point.Element("Lng").Value), 
							(float)Double.Parse(point.Element("Alt").Value)));
					}
					cpDict.Add(_key, cpList);
				}

				return cpDict;
			}
			catch (Exception e)
			{
				Planner.log.Debug(e.ToString());
				return null;
			}
		}
	}
		
}
