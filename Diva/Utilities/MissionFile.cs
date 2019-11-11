using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Utilities
{
	public class MissionFile
	{
		public static void test()
		{
			var file = File.ReadAllText(@"C:\Users\michael\Desktop\logs\FileFormat.mission");

			var output = JsonConvert.DeserializeObject<Format>(file);

			var fileout = JsonConvert.SerializeObject(output);
		}

		public static Format ReadFile(string filename)
		{
			var file = File.ReadAllText(filename);

			var output = JsonConvert.DeserializeObject<Format>(file);

			return output;
		}

		public static void WriteFile(string filename, Format format)
		{
			var fileout = JsonConvert.SerializeObject(format, Formatting.Indented);

			File.WriteAllText(filename, fileout);
		}

		public class Format
		{
			public int MAV_AUTOPILOT;
			public List<object> complexItems = new List<object>();
			public string groundStation;
			public List<MissionItem> items = new List<MissionItem>();
			public MissionItem plannedHomePosition;
			public string version = "1.0";
		}

		public class MissionItem
		{
			public bool autoContinue = true;
			public UInt16 command;
			public double[] coordinate = new double[3];
			public byte frame;
			public UInt16 id;
			public Single param1;
			public Single param2;
			public Single param3;
			public Single param4;
			public string type;
		}
	}
}
