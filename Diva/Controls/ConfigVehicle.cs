using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Diva.Utilities;

namespace Diva.Controls
{
	public partial class ConfigVehicle : UserControl
	{
		
		public Dictionary<string, aircraftinfo> aircrafts = new Dictionary<string, aircraftinfo>();
		

		public struct aircraftinfo
		{
			public string name;
			public string port_name;
			public int port;
			public int baudrate;
			public string vedio_addr;
		}

		public ConfigVehicle()
		{
			InitializeComponent();
			string file = Settings.GetRunningDirectory() + "aircraft.xml";
			LoadXML(file);
			FillTable();

		}

		private void btnEdit1_Click(object sender, EventArgs e)
		{
			ProgressInputDialog dialog = new ProgressInputDialog()
			{
				Text = "Parameters"
			};

			dialog.DoConfirm_Click += delegate (object obj, EventArgs e2)
			{

				TxtPortName1.Text = dialog.port_name;
				TxtPortNumber1.Text = dialog.port;
				TxtBaudrate1.Text = dialog.baudrate;

				aircraftinfo aircraft = new aircraftinfo()
				{
					name = "Copter1",
					port_name = TxtPortName1.Text,
					port = int.Parse(TxtPortNumber1.Text),
					baudrate = int.Parse(TxtBaudrate1.Text),
					vedio_addr = TxtVedioAddr1.Text
				};

				aircrafts[aircraft.name] = aircraft;

				SaveXML();
				dialog.Close();
			};

			dialog.Show();

		}


		public void FillTable()
		{
			TxtPortName1.Text = aircrafts["Copter1"].port_name;
			TxtPortNumber1.Text = (aircrafts["Copter1"].port).ToString();
			TxtBaudrate1.Text = aircrafts["Copter1"].baudrate.ToString();
			TxtVedioAddr1.Text = aircrafts["Copter1"].vedio_addr;

			TxtPortName2.Text = aircrafts["Copter2"].port_name;
			TxtPortNumber2.Text = (aircrafts["Copter2"].port).ToString();
			TxtBaudrate2.Text = aircrafts["Copter2"].baudrate.ToString();
			TxtVedioAddr2.Text = aircrafts["Copter2"].vedio_addr;
		}

		public void SaveXML()
		{
			string filename = Settings.GetRunningDirectory() + "aircraft.xml";
			using (XmlTextWriter xmlwriter = new XmlTextWriter(filename, Encoding.UTF8))
			{
				xmlwriter.Formatting = Formatting.Indented;

				xmlwriter.WriteStartDocument();

				xmlwriter.WriteStartElement("Aircraft");

				
				foreach (string key in aircrafts.Keys)
				{
					xmlwriter.WriteStartElement("Vehicle");
					try
					{
						xmlwriter.WriteElementString("name", aircrafts[key].name);
						xmlwriter.WriteElementString("port_name", aircrafts[key].port_name);
						xmlwriter.WriteElementString("port", (aircrafts[key].port).ToString());
						xmlwriter.WriteElementString("baudrate", (aircrafts[key].baudrate).ToString());
						xmlwriter.WriteElementString("vedio_addr", aircrafts[key].vedio_addr);
					}
					catch
					{
					}
					xmlwriter.WriteEndElement();
				}

				xmlwriter.WriteEndElement();

				xmlwriter.WriteEndDocument();
				xmlwriter.Close();
			}
		}


		public void LoadXML(string filename)
		{
			try
			{
				using (XmlTextReader xmlreader = new XmlTextReader(filename))
				{
					while (xmlreader.Read())
					{
						xmlreader.MoveToElement();
						try
						{
							switch (xmlreader.Name)
							{
								case "Vehicle":
									{
										aircraftinfo aircraft = new aircraftinfo();

										while (xmlreader.Read())
										{
											bool dobreak = false;
											xmlreader.MoveToElement();
											switch (xmlreader.Name)
											{
												case "name":
													aircraft.name = xmlreader.ReadString();
													break;
												case "port_name":
													aircraft.port_name = xmlreader.ReadString();
													break;
												case "port":
													aircraft.port = int.Parse(xmlreader.ReadString());
													break;
												case "baudrate":
													aircraft.baudrate = int.Parse(xmlreader.ReadString());
													break;
												case "vedio_addr":
													aircraft.vedio_addr = xmlreader.ReadString();
													break;
												case "Vehicle":
													aircrafts[aircraft.name] = aircraft;
													dobreak = true;
													break;
											}
											if (dobreak)
												break;
										}
										string temp = xmlreader.ReadString();
									}
									break;
								case "xml":
									break;
								default:
									break;
							}
						}
						catch (Exception ee) {
							Console.WriteLine(ee.Message);
						} // silent fail on bad entry
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Bad Aircraft File: " + ex.ToString());
			} // bad config file

		}
	}
}
