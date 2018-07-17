using Diva.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;


namespace Diva
{
	class FlightRecorder
	{
		public long Id { get; set; }
		public string UserName { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
		public double TotalDistance { get; set; }
		public double HomeLatitude { get; set; }	
		public double HomeLongitude { get; set; }
		public double HomeAltitude { get; set; }

	}
}
