using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Diva.Controls;
using log4net;

namespace Diva
{
    public struct DroneSetting
    {
        public string Name;
        public string PortName;
        public string PortNumber;
        public string Baudrate;
        public string StreamURI;

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
                        xmlwriter.WriteElementString("name", d.Name);
                        xmlwriter.WriteElementString("port_name", d.PortName);
                        xmlwriter.WriteElementString("port_number", d.PortNumber);
                        xmlwriter.WriteElementString("baudrate", d.Baudrate);
                        xmlwriter.WriteElementString("streaming", d.StreamURI);
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
                                            case "name":
                                                d.Name = xmlreader.ReadString();
                                                break;
                                            case "port_name":
                                                d.PortName = xmlreader.ReadString();
                                                break;
                                            case "port_number":
                                                d.PortNumber = xmlreader.ReadString();
                                                break;
                                            case "baudrate":
                                                d.Baudrate = xmlreader.ReadString();
                                                break;
                                            case "streaming":
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
