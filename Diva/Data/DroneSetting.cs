using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Diva.Controls;
using log4net;

namespace Diva
{
    public class DroneSetting
    {
        public string Name = "";
        public string PortName = "";
        public string PortNumber = "";
        public string Baudrate = "";
        public string StreamURI = "";
        public bool Enabled = true;

        public static void ExportXML(string file, List<DroneSetting> drones)
        {
            ILog log = LogManager.GetLogger(typeof(DroneSetting));
            using (var stream = System.IO.File.OpenWrite(file))
            using (XmlTextWriter xmlwriter = new XmlTextWriter(stream, Encoding.UTF8))
            {
                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.WriteStartDocument();
                xmlwriter.WriteStartElement("Aircraft");

                foreach (var d in drones)
                {
                    xmlwriter.WriteStartElement("Vehicle");
                    try
                    {
                        xmlwriter.WriteElementString("Name", d.Name);
                        xmlwriter.WriteElementString("PortName", d.PortName);
                        xmlwriter.WriteElementString("PortNumber", d.PortNumber);
                        xmlwriter.WriteElementString("Baudrate", d.Baudrate);
                        xmlwriter.WriteElementString("StreamURI", d.StreamURI);
                    }
                    catch (Exception e)
                    {
                        log.Debug(e.Message);
                    }
                    xmlwriter.WriteEndElement();
                }

                xmlwriter.WriteEndElement();
                xmlwriter.WriteEndDocument();
                xmlwriter.Close();
            }
        }

        public static List<DroneSetting> ImportXML(string file)
        {
            ILog log = LogManager.GetLogger(typeof(DroneSetting));
            List<DroneSetting> drones = new List<DroneSetting>();
            try
            {
                using (XmlTextReader xmlreader = new XmlTextReader(file))
                {
                    while (xmlreader.Read())
                    {
                        xmlreader.MoveToElement();
                        try
                        {
                            switch (xmlreader.Name)
                            {
                                case "Vehicle":
                                    DroneSetting d = new DroneSetting();
                                    bool more = true;

                                    while (more && xmlreader.Read())
                                    {
                                        xmlreader.MoveToElement();
                                        switch (xmlreader.Name)
                                        {
                                            case "Name":
                                                d.Name = xmlreader.ReadString();
                                                break;
                                            case "PortName":
                                                d.PortName = xmlreader.ReadString();
                                                break;
                                            case "PortNumber":
                                                d.PortNumber = xmlreader.ReadString();
                                                break;
                                            case "Baudrate":
                                                d.Baudrate = xmlreader.ReadString();
                                                break;
                                            case "StreamURI":
                                                d.StreamURI = xmlreader.ReadString();
                                                break;
                                            case "Vehicle":
                                                drones.Add(d);
                                                more = false;
                                                break;
                                        }
                                    }
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
            return drones;
        }

        public static string FileExtensionFilter => Properties.Strings.StrDroneSettingsFileExtensionFilter;
    }
}
