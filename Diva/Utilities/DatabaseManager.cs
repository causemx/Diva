using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace Diva.Utilities
{
	class DatabaseManager
	{
		public static string dbFile
		{
			get { return Environment.CurrentDirectory + "\\FlightRecorder.sqlite"; }
		}

		public static string cnStr = "Data Source=" + dbFile;

		public static void InitialDatabase()
		{
			if (!File.Exists(dbFile))
			{
				CreateDatabase();
			}
		}

		public static void CreateDatabase()
		{

			using (var cnn = new SQLiteConnection(cnStr))
			{
				cnn.Open();
				cnn.Execute(
				@"create table FlightRecorder ( 
				ID integer primary key AUTOINCREMENT,
				UserName varchar(30) not null,
				StartTime datetime not null,
				EndTime datetime not null,
				TotalDistance double,
				HomeLatitude double,
				HomeLongitude double,
				HomeAltitude double)");
			}
		}

		public static void UpdateStartTime(long id, string starttime)
		{
			string sql = "UPDATE FlightRecorder SET StartTime = @StartTime WHERE ID = @ID;";
			using (var connection = new SQLiteConnection(cnStr))
			{
				var affectedRows = connection.Execute(sql, new { ID = id, StartTime = starttime });
			}
		}

		public static void UpdateEndTime(long id, string endtime)
		{
			string sql = "UPDATE FlightRecorder SET EndTime = @EndTime WHERE ID = @ID;";
			using (var connection = new SQLiteConnection(cnStr))
			{
				var affectedRows = connection.Execute(sql, new { ID = id, EndTime = endtime });
			}
		}

		public static void UpdateTotalDistance(long id, double distance)
		{
			string sql = "UPDATE FlightRecorder SET TotalDistance = @TotalDistance WHERE ID = @ID;";
			using (var connection = new SQLiteConnection(cnStr))
			{
				var affectedRows = connection.Execute(sql, new { ID = id, TotalDistance = distance });
			}
		}

		public static void UpdateHomeLocation(long id, double latitude, double longitude, double altitude)
		{
			string sql = "UPDATE FlightRecorder SET HomeLatitude = @HomeLatitude, HomeLongitude = @HomeLongitude WHERE ID = @ID;";
			using (var connection = new SQLiteConnection(cnStr))
			{
				var affectedRows = connection.Execute(sql, new { ID = id, HomeLatitude = latitude, HomeLongitude = longitude, HomeAltitude = altitude });
			}
		}

		public static string DateTimeSQLite(DateTime datetime)
		{
			string dateTimeFormat = "{0:yyyy-MM-dd hh:mm:ss}";
			return string.Format(dateTimeFormat, datetime);
		}

		public static void Dump(long id)
		{
			string sql = "SELECT * FROM FlightRecorder WHERE ID = @ID;";
			using (var connection = new SQLiteConnection(cnStr))
			{
				var detail = connection.QueryFirst(sql, new { ID = id });
				Planner.log.Info("ID:" + detail.ID + "\n"
					+ "username:" + detail.UserName + "\n"
					+ "starttime:" + detail.StartTime + "\n"
					+ "endtime:" + detail.EndTime + "\n"
					+ "totaldistance:" + detail.TotalDistance + "\n"
					+ "homelatitude:" + detail.HomeLatitude + "\n"
					+ "homelongitude" + detail.HomeLongitude + "\n");
			}
		}

		public static long InsertValue(FlightRecorder recorder)
		{
			if (!File.Exists(dbFile))
			{
				CreateDatabase();
			}

			using (var cn = new SQLiteConnection(cnStr))
			{
				cn.Open();
				recorder.Id = cn.Query<long>(
					@"INSERT INTO FlightRecorder
					( UserName, StartTime, EndTime, TotalDistance, HomeLatitude, HomeLongitude, HomeAltitude ) VALUES
					( @UserName, @StartTime, @EndTime, @TotalDistance, @HomeLatitude, @HomeLongitude, @HomeAltitude );
					SELECT last_insert_rowid()", recorder).First();
				return recorder.Id;
			}
		}

	}
}
