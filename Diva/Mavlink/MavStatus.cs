using Diva.Utilities;
using GMap.NET;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Mavlink
{
	public class MavStatus : MAVLink, IDisposable
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MavlinkInterface parent;

		public MavStatus(MavlinkInterface mavLinkInterface, byte sysid, byte compid)
		{
			this.parent = mavLinkInterface;
			this.sysid = sysid;
			this.compid = compid;
			this.packetspersecond = new Dictionary<uint, double>();
			this.packetspersecondbuild = new Dictionary<uint, DateTime>();
			this.lastvalidpacket = DateTime.MinValue;
			sendlinkid = (byte)(new Random().Next(256));
			signing = false;
			this.param = new MAVLinkParamList();
			this.packets = new Dictionary<uint, MAVLinkMessage>();
			this.aptype = 0;
			this.apname = 0;
			this.recvpacketcount = 0;
			this.VersionString = "";
			this.SoftwareVersions = "";
			this.SerialString = "";
			this.FrameString = "";
		

			camerapoints.Clear();

			// GMapMarkerOverlapCount.Clear();

			this.packetslost = 0f;
			this.packetsnotlost = 0f;
			this.packetlosttimer = DateTime.MinValue;
		}

		public float packetslost = 0;
		public float packetsnotlost = 0;
		public DateTime packetlosttimer = DateTime.MinValue;
		public float synclost = 0;

		// for flight recorder
		public PointLatLng HomeLocation { get; set; }
		public string SystemStartTime { get; set; }
		public string SystemStopTime { get; set; }
		
		// all
		public string VersionString { get; set; }
		// px4+ only
		public string SoftwareVersions { get; set; }
		// px4+ only
		public string SerialString { get; set; }
		// AC frame type
		public string FrameString { get; set; }

		public string Guid { get; set; }

		public double current_lat { get; set; }

		public double altasl { get; set; }

		public double current_lng { get; set; }

		public float groundspeed { get; set; }

		public uint mode{ get; set; }

		// Copter parameter

		public float nav_bearing { get; set; }

		public Planner.Firmwares firmware = Planner.Firmwares.ArduCopter2;

		public float alt
		{
			get { return _alt; }
			set
			{
				// check update rate, and ensure time hasnt gone backwards                
				_alt = value;

				if ((datetime - lastalt).TotalSeconds >= 0.2 && oldalt != alt || lastalt > datetime)
				{
					climbrate = (alt - oldalt) / (float)(datetime - lastalt).TotalSeconds;
					verticalspeed = (alt - oldalt) / (float)(datetime - lastalt).TotalSeconds;
					if (float.IsInfinity(_verticalspeed))
						_verticalspeed = 0;
					lastalt = datetime;
					oldalt = alt;
				}
			}
		}

		float _verticalspeed;
		public float verticalspeed
		{
			get
			{
				if (float.IsNaN(_verticalspeed)) _verticalspeed = 0;
				return _verticalspeed;
			}
			set { _verticalspeed = _verticalspeed * 0.4f + value * 0.6f; }
		}

		float _climbrate;
		public float climbrate
		{
			get { return _climbrate; }
			set { _climbrate = value; }
		}

		public float satcount { get; set; }

		public DateTime datetime { get; set; }

		DateTime lastalt = DateTime.MinValue;

		private float _alt = 0;
		float oldalt = 0;

		public double battery_voltage
		{
			get { return _battery_voltage; }
			set
			{
				if (_battery_voltage == 0) _battery_voltage = value;
				_battery_voltage = value * 0.2f + _battery_voltage * 0.8f;
			}
		}

		internal double _battery_voltage;

		public float yaw
		{
			get { return _yaw; }
			set
			{
				if (value < 0)
				{
					_yaw = value + 360;
				}
				else
				{
					_yaw = value;
				}
			}
		}

		private float _yaw = 0;

		public float groundcourse
		{
			get { return _groundcourse; }
			set
			{
				if (value < 0)
				{
					_groundcourse = value + 360;
				}
				else
				{
					_groundcourse = value;
				}
			}
		}

		private float _groundcourse = 0;

		/// <summary>
		/// the static global state of the currently connected MAV
		/// </summary>
		// public CurrentState cs = new CurrentState();

		private byte _sysid;
		/// <summary>
		/// mavlink remote sysid
		/// </summary>
		public byte sysid
		{
			get { return _sysid; }
			set { _sysid = value; }
		}

		/// <summary>
		/// mavlink remove compid
		/// </summary>
		public byte compid { get; set; }

		public byte linkid { get; set; }

		public byte sendlinkid { get; internal set; }

		public byte sys_status { get; set; }

		public UInt64 timestamp { get; set; }


		public bool armed { get; set; }

		public bool landed { get; set; }
		
		public bool actived { get; set; }

		public bool failsafe { get; set; }

		internal byte[] signingKey;

		/// <summary>
		/// are we signing outgoing packets, and checking incomming packet signatures
		/// </summary>
		public bool signing { get; set; }

		/// <summary>
		/// ignore the incomming signature
		/// </summary>
		public bool signingignore { get; set; }

		/// <summary>
		/// mavlink 2 enable
		/// </summary>
		public bool mavlinkv2 = false;

		/// <summary>
		/// storage for whole paramater list
		/// </summary>
		public MAVLinkParamList param { get; set; }

		public Dictionary<string, MAV_PARAM_TYPE> param_types = new Dictionary<string, MAV_PARAM_TYPE>();

		/// <summary>
		/// storage of a previous packet recevied of a specific type
		/// </summary>
		Dictionary<uint, MAVLinkMessage> packets { get; set; }

		object packetslock = new object();

		public MAVLinkMessage getPacket(uint mavlinkid)
		{
			//log.InfoFormat("getPacket {0}", (MAVLINK_MSG_ID)mavlinkid);
			lock (packetslock)
			{
				if (packets.ContainsKey(mavlinkid))
				{
					return packets[mavlinkid];
				}
			}

			return null;
		}

		public void addPacket(MAVLinkMessage msg)
		{
			lock (packetslock)
			{
				packets[msg.msgid] = msg;
			}
		}

		public void clearPacket(uint mavlinkid)
		{
			lock (packetslock)
			{
				if (packets.ContainsKey(mavlinkid))
				{
					packets[mavlinkid] = null;
				}
			}
		}


		/// <summary>
		/// time seen of last mavlink packet
		/// </summary>
		public DateTime lastvalidpacket { get; set; }


		/// <summary>
		/// used to calc packets per second on any single message type - used for stream rate comparaison
		/// </summary>
		public Dictionary<uint, double> packetspersecond { get; set; }

		/// <summary>
		/// time last seen a packet of a type
		/// </summary>
		public Dictionary<uint, DateTime> packetspersecondbuild { get; set; }

		/// <summary>
		/// mavlink ap type
		/// </summary>
		public MAV_TYPE aptype { get; set; }

		public MAV_AUTOPILOT apname { get; set; }


		/// <summary>
		/// used as a snapshot of what is loaded on the ap atm. - derived from the stream
		/// </summary>
		public ConcurrentDictionary<int, mavlink_mission_item_t> wps = new ConcurrentDictionary<int, mavlink_mission_item_t>();

		public ConcurrentDictionary<int, mavlink_rally_point_t> rallypoints = new ConcurrentDictionary<int, mavlink_rally_point_t>();

		public ConcurrentDictionary<int, mavlink_fence_point_t> fencepoints = new ConcurrentDictionary<int, mavlink_fence_point_t>();

		public List<mavlink_camera_feedback_t> camerapoints = new List<mavlink_camera_feedback_t>();

		// public GMapMarkerOverlapCount GMapMarkerOverlapCount = new GMapMarkerOverlapCount(PointLatLng.Empty);

		/// <summary>
		/// Store the guided mode wp location
		/// </summary>
		public mavlink_mission_item_t GuidedMode = new mavlink_mission_item_t();

		internal int recvpacketcount = 0;
		public Int64 time_offset_ns { get; set; }

		public override string ToString()
		{
			return sysid.ToString();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		private PointLatLngAlt _trackerloc;

		public PointLatLngAlt TrackerLocation
		{
			get
			{
				if (_trackerloc.Lng != 0) return _trackerloc;
				return HomeLocation;
			}
			set { _trackerloc = value; }
		}
	}
}
