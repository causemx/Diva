using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using System.Xml.Linq;

namespace Diva.Utilities
{
    public static class ParameterRepository
    {
        private static readonly ILog log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static XDocument xdoc = null;

        public static string FromRepository(string nodeKey, string metaKey, string vechileType)
		{
            string res = string.Empty;
            if (xdoc == null)
            {
                try { xdoc = XDocument.Parse(Properties.Resources.ParameterMetaData); }
                catch (Exception e) { log.Error(e); }
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
                }
                catch
                {
                }
            }
            return res;
		}

		public static List<KeyValuePair<int, string>> GetOptionsInt(string nodeKey, string vechileType)
		{
			string availableValuesRaw = FromRepository(nodeKey, "Values", vechileType);
			string[] availableValues = availableValuesRaw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (availableValues.Any())
			{
				var splitValues = new List<KeyValuePair<int, string>>();
				// Add the values to the ddl
				foreach (string val in availableValues)
				{
					try
					{
						string[] valParts = val.Split(new[] { ':' });
						splitValues.Add(new KeyValuePair<int, string>(int.Parse(valParts[0].Trim()),
							(valParts.Length > 1) ? valParts[1].Trim() : valParts[0].Trim()));
					}
					catch
					{
						Console.WriteLine("Bad entry in param meta data: " + nodeKey);
					}
				}
				;

				return splitValues;
			}

			return new List<KeyValuePair<int, string>>();
		}
	}
}
