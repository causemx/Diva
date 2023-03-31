using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Xml.Linq;
using System.IO;

namespace Diva.Mavlink
{
    public static class MavParamRepositoryV2
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static XDocument xdoc = null;

        public static string FromRepository(string nodeKey, string metaKey, string vechileType)
		{
            string res = string.Empty;
            if (xdoc == null)
            {
                try
                { 
                    xdoc = XDocument.Parse(Properties.Resources.ParameterMetaData); 
                }
                catch (IOException)
                {
                    xdoc = XDocument.Parse(Properties.Resources.ParameterMetaDataBackup);
                }
                catch (Exception _e)
                {
                    log.Error(_e);
                }
            }
            if (xdoc != null)
            {
                try
                {
                    var elts = xdoc.Element("Params").Elements(vechileType);
                    foreach (var el in elts)
                    {
                        if (el != null && el.HasElements)
                        {
                            var node = el.Element(nodeKey);
                            if (node != null && node.HasElements)
                            {
                                var val = node.Element(metaKey);
                                if (val != null)
                                {
                                    res = val.Value;
                                    break;
                                }
                            }
                        }
                    }
                } catch { }
            }
            return res;
		}

		public static List<KeyValuePair<int, string>> GetOptionsInt(string nodeKey, string vechileType)
		{
			string[] values = FromRepository(nodeKey, "Values", vechileType)
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var valuePairs = new List<KeyValuePair<int, string>>();
			foreach (string val in values)
			{
				try
				{
					string[] valParts = val.Split(new[] { ':' });
					valuePairs.Add(new KeyValuePair<int, string>(int.Parse(valParts[0].Trim()),
						valParts[valParts.Length > 1 ? 1 : 0].Trim()));
				}
				catch
				{
					Console.WriteLine("Bad entry in param meta data: " + nodeKey);
				}
			}
			return valuePairs;
		}
	}
}
