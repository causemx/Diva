using Diva.Utilities;
using GMap.NET;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using static MAVLink;

namespace Diva.Mavlink
{
	public class MavStatus
	{
		static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public string VersionString { get; set; } = "";
        public string SoftwareVersions { get; set; } = "";
        public string SerialString { get; set; } = "";
        public string FrameString { get; set; } = "";

        private double batvolt;
        public double BatteryVoltage
        {
            get { return batvolt; }
            set
            {
                if (batvolt == 0) batvolt = value;
                batvolt = value * 0.2f + batvolt * 0.8f;
            }
        }

        public double BatteryLowVoltage { get; set; }

        public double Latitude { get; set; }
		public double Longitude { get; set; }
        public virtual float Altitude { get; set; }
        public double AbsoluteAltitude { get; set; }

        public MavUtlities.Firmwares firmware = MavUtlities.Firmwares.ArduCopter2;

		public int SatteliteCount { get; set; }

		public bool MavLinkV2 = false;

		public MAVLinkParamList Params { get; set; } = new MAVLinkParamList();

        public Dictionary<string, MAV_PARAM_TYPE> ParamTypes = new Dictionary<string, MAV_PARAM_TYPE>();

        #region packets
        internal int recvpacketcount = 0;
        private Dictionary<uint, MAVLinkMessage> Packets = new Dictionary<uint, MAVLinkMessage>();
        private Dictionary<uint, Action<MAVLinkMessage>> Listeners = new Dictionary<uint, Action<MAVLinkMessage>>();
        public DateTime LastPacket { get; set; } = DateTime.MinValue;
        public Dictionary<uint, double> PacketsPerSecond { get; } = new Dictionary<uint, double>();
        public Dictionary<uint, DateTime> PacketsPerSecondBuild { get; } = new Dictionary<uint, DateTime>();
        public DateTime PacketTime { get; set; }
        public float PacketsLost = 0;
        public float PacketsNotLost = 0;
        public DateTime PacketLostTimer = DateTime.MinValue;
        public float SyncLost = 0;

        object packetslock = new object();

        public MAVLinkMessage GetPacket(MAVLINK_MSG_ID id) => GetPacket((uint)id);

        public MAVLinkMessage GetPacket(uint id)
		{
			lock (packetslock)
			{
				if (Packets.ContainsKey(id))
				{
					return Packets[id];
				}
			}
			return null;
		}

		public void AddPacket(MAVLinkMessage msg)
		{
			lock (packetslock)
			{
				Packets[msg.msgid] = msg;
			}
		}

        public void ClearPacket(MAVLINK_MSG_ID id) => ClearPacket((uint)id);

        public void ClearPacket(uint mavlinkid)
		{
			lock (packetslock)
			{
				if (Packets.ContainsKey(mavlinkid))
				{
					Packets[mavlinkid] = null;
				}
			}
		}
        #endregion packets

        public MAV_TYPE APType { get; set; } = 0;
        public MAV_AUTOPILOT APName { get; set; } = 0;

		public mavlink_mission_item_t GuidedMode = new mavlink_mission_item_t();

		public Int64 TimeOffset_ns { get; set; }
	}

    public class DroneStatus : MavStatus
    {
        public float GroundSpeed { get; set; }

        DateTime lastalt = DateTime.MinValue;
        private volatile float altitude = 0;
        float oldalt = 0;
        public override float Altitude
        {
            get { return altitude; }
            set
            {
                // check update rate, and ensure time hasnt gone backwards                
                altitude = value;
                if ((PacketTime - lastalt).TotalSeconds >= 0.2 && oldalt != Altitude || lastalt > PacketTime)
                {
                    VerticalSpeed = (Altitude - oldalt) / (float)(PacketTime - lastalt).TotalSeconds;
                    if (float.IsInfinity(verticalspeed))
                        verticalspeed = 0;
                    lastalt = PacketTime;
                    oldalt = Altitude;
                }
            }
        }

        float verticalspeed = 0;
        public float VerticalSpeed
        {
            get => verticalspeed;
            set
            {
                verticalspeed = verticalspeed * 0.4f + value * 0.6f;
                if (float.IsNaN(verticalspeed)) verticalspeed = 0;
            }
        }

        public uint FlightMode { get; set; }

        public float NAVBearing { get; set; } = float.NaN;

        private float yaw = float.NaN;
        public float Yaw
        {
            get { return yaw; }
            set
            {
                if (value < 0)
                {
                    yaw = value + 360;
                }
                else
                {
                    yaw = value;
                }
            }
        }

        private float groundcourse = float.NaN;
        public float GroundCourse
        {
            get { return groundcourse; }
            set { groundcourse = value < 0 ? value + 360 : value; }
        }

        public byte State { get; set; }

        public bool IsArmed { get; set; }
    }
}
