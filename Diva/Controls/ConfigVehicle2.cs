using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Diva.Utilities;
using System.Xml;

namespace Diva.Controls
{
	public partial class ConfigVehicle2 : UserControl
	{

		private static readonly string DIALOG_TITLE = "Parameters"; 

		public ConfigVehicle2()
		{
			InitializeComponent();
			string file = Settings.GetRunningDirectory() + "aircraft.xml";
			LoadXML(file);
		}

		public Dictionary<string, aircraftinfo> aircrafts = new Dictionary<string, aircraftinfo>();


		public struct aircraftinfo
		{
			public string name;
			public string port_name;
			public int port_number;
			public int baudrate;
			public string streaming;
		}

		

		private void BtnEdit_Click(object sender, EventArgs e)
		{
		
			switch (((Button)sender).Name)
			{
				case "BtnEdit1":
					ProgressInputDialog dialog1 = new ProgressInputDialog()
					{
						Text = "Parameters"
					};

					dialog1.DoConfirm_Click += delegate (object obj, EventArgs e2)
					{

						TxtPortName1.Text = dialog1.port_name;
						TxtPortNumber1.Text = dialog1.port;
						TxtBaudrate1.Text = dialog1.baudrate;

						aircraftinfo aircraft = new aircraftinfo()
						{
							name = "Copter1",
							port_name = TxtPortName1.Text,
							port_number = int.Parse(TxtPortNumber1.Text),
							baudrate = int.Parse(TxtBaudrate1.Text),
							streaming = TxtStreaming1.Text
						};

						aircrafts[aircraft.name] = aircraft;
						dialog1.Close();
					};

					dialog1.Show();
					break;

				case "BtnEdit2":
					ProgressInputDialog dialog2 = new ProgressInputDialog()
					{
						Text = "Parameters"
					};

					dialog2.DoConfirm_Click += delegate (object obj, EventArgs e2)
					{

						TxtPortName2.Text = dialog2.port_name;
						TxtPortNumber2.Text = dialog2.port;
						TxtBaudrate2.Text = dialog2.baudrate;

						aircraftinfo aircraft = new aircraftinfo()
						{
							name = "Copter2",
							port_name = TxtPortName1.Text,
							port_number = int.Parse(TxtPortNumber2.Text),
							baudrate = int.Parse(TxtBaudrate2.Text),
							streaming = TxtStreaming2.Text
						};

						aircrafts[aircraft.name] = aircraft;
						dialog2.Close();
					};

					dialog2.Show();
					break;

				case "BtnEdit3":
					ProgressInputDialog dialog3 = new ProgressInputDialog()
					{
						Text = "Parameters"
					};

					dialog3.DoConfirm_Click += delegate (object obj, EventArgs e2)
					{

						TxtPortName3.Text = dialog3.port_name;
						TxtPortNumber3.Text = dialog3.port;
						TxtBaudrate3.Text = dialog3.baudrate;

						aircraftinfo aircraft = new aircraftinfo()
						{
							name = "Copter3",
							port_name = TxtPortName3.Text,
							port_number = int.Parse(TxtPortNumber3.Text),
							baudrate = int.Parse(TxtBaudrate3.Text),
							streaming = TxtStreaming3.Text
						};

						aircrafts[aircraft.name] = aircraft;
						dialog3.Close();
					};

					dialog3.Show();

					break;
			}

			
		}


		private void BtnSave_Click(object sender, EventArgs e)
		{
			SaveXML();
		}

		private void BtnExit_Click(object sender, EventArgs e)
		{
			
		}


		public void FillTable()
		{
			TxtPortName1.Text = aircrafts["Copter1"].port_name;
			TxtPortNumber1.Text = (aircrafts["Copter1"].port_number).ToString();
			TxtBaudrate1.Text = aircrafts["Copter1"].baudrate.ToString();
			TxtStreaming1.Text = aircrafts["Copter1"].streaming;

			TxtPortName2.Text = aircrafts["Copter2"].port_name;
			TxtPortNumber2.Text = (aircrafts["Copter2"].port_number).ToString();
			TxtBaudrate2.Text = aircrafts["Copter2"].baudrate.ToString();
			TxtStreaming2.Text = aircrafts["Copter2"].streaming;
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
						xmlwriter.WriteElementString("port", (aircrafts[key].port_number).ToString());
						xmlwriter.WriteElementString("baudrate", (aircrafts[key].baudrate).ToString());
						xmlwriter.WriteElementString("vedio_addr", aircrafts[key].streaming);
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
												case "port_number":
													aircraft.port_number = int.Parse(xmlreader.ReadString());
													break;
												case "baudrate":
													aircraft.baudrate = int.Parse(xmlreader.ReadString());
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
						catch (Exception ee)
						{
							
						}
					}
				}
			}
			catch (Exception ex)
			{
				
			} 

		}

		
	}
}

