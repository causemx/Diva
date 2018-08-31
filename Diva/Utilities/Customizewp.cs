using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Diva.Utilities
{
	public class Customizewp
	{
		public ushort Id { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
		public float Alt { get; set; }
		public object Tag { get; set; }

		public static void ImportOverlayXML(string file)
		{
			Dictionary<string, List<Customizewp>> items = new Dictionary<string, List<Customizewp>>();
			try
			{
				using (XmlTextReader reader = new XmlTextReader(file))
				{

					while (reader.Read())
					{
						reader.MoveToElement();
						switch (reader.Name)
						{
							case "layout":

								while (reader.Read())
								{
									reader.MoveToElement();

									Customizewp item = new Customizewp();
									string _name = "";

									switch (reader.Name)
									{
										case "name":
											Planner.log.Debug("name " + reader.ReadString());
											break;
										case "id":
											Planner.log.Debug("id " + reader.ReadString());
											break;
										case "lat":
											Planner.log.Debug("lat " + reader.ReadString());
											break;
										case "lng":
											Planner.log.Debug("lng " + reader.ReadString());
											break;
										case "alt":
											Planner.log.Debug("alt " + reader.ReadString());
											break;
										case "polygon":
											break;
									}
								}


								break;
							default:
								break;
						}
					}
				}
			}
			catch (Exception e)
			{
			}
		}
	}
		
}
