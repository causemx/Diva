using Diva.Controls;
using Diva.Mavlink;
using Diva.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Diva.Comms
{
	public partial class ConnectionForm : Form
	{
		

		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		public static readonly string DIALOG_TITLE = "Parameters";
		public static readonly string AIRCRAFT_FILE_NAME = "aircraft.xml";

		public List<DroneInput> droneInputs = new List<DroneInput>();
		public static Dictionary<string, aircraftinfo> aircrafts = new Dictionary<string, aircraftinfo>();
		public event EventHandler ButtonSaveClick;


		private int drone_index = 0;


		public ConnectionForm()
		{
			InitializeComponent();

			InitializeComponent();

			string file = Settings.GetRunningDirectory() + AIRCRAFT_FILE_NAME;

			LoadXML(AIRCRAFT_FILE_NAME);

			foreach (string key in aircrafts.Keys)
			{
				DroneInput droneInput = new DroneInput();
				droneInput.Name = aircrafts[key].name;
				droneInput.PortName = aircrafts[key].port_name;
				droneInput.PortNumber = aircrafts[key].port_number;
				droneInput.Baudrate = aircrafts[key].baudrate;
				droneInput.Streaming = aircrafts[key].streaming;

				droneInput.Location = new Point(10, drone_index * 200);
				droneInput.ButtonRemoveClick += new EventHandler(ButtonRemove_Click);

				droneInputs.Add(droneInput);
				this.Controls.Add(droneInput);

				drone_index++;
			}
		}


		protected void ButtonRemove_Click(object sender, EventArgs e)
		{
			Console.WriteLine("remove click");
		}

		public struct aircraftinfo
		{
			public string name;
			public string port_name;
			public string port_number;
			public string baudrate;
			public string streaming;
		}

		private void BtnSave_Click(object sender, EventArgs e)
		{
			foreach (DroneInput _input in droneInputs)
			{
				aircraftinfo aircraft = new aircraftinfo()
				{
					name = _input.Name,
					port_name = _input.PortName,
					port_number = _input.PortNumber,
					baudrate = _input.Baudrate,
					streaming = _input.Streaming,
				};

				

				aircrafts[aircraft.name] = aircraft;
			}

			SaveXML(AIRCRAFT_FILE_NAME);

			if (ButtonSaveClick != null)
				ButtonSaveClick(this, e);
			
		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			Dispose();
		}

		public void SaveXML(string file)
		{
			string filename = Settings.GetRunningDirectory() + file;
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
						xmlwriter.WriteElementString("port_number", (aircrafts[key].port_number).ToString());
						xmlwriter.WriteElementString("baudrate", (aircrafts[key].baudrate).ToString());
						xmlwriter.WriteElementString("streaming", aircrafts[key].streaming);
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


		public void LoadXML(string file)
		{
			string filename = Settings.GetRunningDirectory() + file;
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
												case "port_number":
													aircraft.port_number = xmlreader.ReadString();
													break;
												case "baudrate":
													aircraft.baudrate = xmlreader.ReadString();
													break;
												case "streaming":
													aircraft.streaming = xmlreader.ReadString();
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
						catch (Exception e1)
						{
							log.Debug(e1.ToString());
						}
					}
				}
			}
			catch (Exception e2)
			{
				log.Debug(e2.ToString());
			}
		}
	}
}
