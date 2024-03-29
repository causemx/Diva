﻿using GMap.NET;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using static MAVLink;

namespace Diva.Mavlink
{
	public class MavStatus
	{
        public string VersionString { get; set; } = "";
        public string SoftwareVersions { get; set; } = "";
        public string SerialString { get; set; } = "";
        public string FrameString { get; set; } = "";

        private double batteryVoltage;
        public double BatteryVoltage
        {
            get { return batteryVoltage; }
            set
            {
                if (batteryVoltage == 0) batteryVoltage = value;
                batteryVoltage = value * 0.2f + batteryVoltage * 0.8f;
            }
        }

        public double BatteryLowVoltage { get; set; }
        private double _current;
        private DateTime _lastcurrent = DateTime.MinValue;
        public double battery_usedmah { get; set; }

        public double current
        {
            get => _current;
            set
            {
                if (_lastcurrent == DateTime.MinValue) _lastcurrent = PacketTime;
                // break; case for no sensor
                if (value == -0.01f)
                {
                    _current = 0;
                    return;
                }

                battery_usedmah += value * 1000.0 * (PacketTime - _lastcurrent).TotalHours;
                _current = value;
                _lastcurrent = PacketTime;
            }
        } //current may to be below zero - recuperation in arduplane
        public int rxrssi { get; set; }
        public double Latitude { get; set; }
		public double Longitude { get; set; }
        public virtual GMap.NET.PointLatLng Location => new GMap.NET.PointLatLng(Latitude, Longitude);
        public virtual float Altitude { get; set; }
        public double AbsoluteAltitude { get; set; }

        public Firmwares Firmware = Firmwares.ArduCopter2;

		public int SatteliteCount { get; set; }
        public float gpshdop { get; set; }

        public bool MavLinkV2 = false;

		public MAVLinkParamList Params { get; set; } = new MAVLinkParamList();

        public Dictionary<string, MAV_PARAM_TYPE> ParamTypes = new Dictionary<string, MAV_PARAM_TYPE>();

        #region packets
        internal int recvPacketCount = 0;
        private ConcurrentDictionary<uint, MAVLinkMessage> Packets = new ConcurrentDictionary<uint, MAVLinkMessage>();
        public DateTime LastPacket { get; set; } = DateTime.MinValue;
        public Dictionary<uint, double> PacketsPerSecond { get; } = new Dictionary<uint, double>();
        public Dictionary<uint, DateTime> PacketsPerSecondBuild { get; } = new Dictionary<uint, DateTime>();
        public DateTime PacketTime { get; set; }
        public float PacketsLost = 0;
        public float PacketsNotLost = 0;
        public DateTime PacketLostTimer = DateTime.MinValue;
        public float SyncLost = 0;
        readonly object packetLock = new object();

        public MAVLinkMessage GetPacket(MAVLINK_MSG_ID id) => GetPacket((uint)id);

        public MAVLinkMessage GetPacket(uint id)
		{
           return Packets.GetOrAdd(id, (s) => null);
            /*
			lock (packetLock)
			{
				if (Packets.ContainsKey(id))
				{
					return Packets[id];
				}
			}*/
		}

		public void AddPacket(MAVLinkMessage msg)
		{
            Packets.AddOrUpdate(
                msg.msgid,
                (s) => new MAVLinkMessage(),
                (i, m) => Packets[i] = m);
            /*
			lock (packetLock)
			{
				Packets[msg.msgid] = msg;
			}*/
		}

        public void ClearPacket(MAVLINK_MSG_ID id) => ClearPacket((uint)id);

        public void ClearPacket(uint mavlinkid)
		{
            Packets.AddOrUpdate(
                mavlinkid,
                (s) => null,
                (i, m) => Packets[mavlinkid] = null);

            /*
			lock (packetLock)
			{
				if (Packets.ContainsKey(mavlinkid))
				{
					Packets[mavlinkid] = null;
				}
			}*/
        }
        #endregion packets

        public MAV_TYPE APType { get; set; } = 0;
        public MAV_AUTOPILOT APName { get; set; } = 0;

        public Int64 TimeOffset_ns { get; set; }

        public UInt32 SensorPresent { get; set; }
        public UInt32 SensorEnabled { get; set; }
        public UInt32 SensorHealth { get; set; }

        public UInt32 SensorError => SensorEnabled & SensorPresent & ~SensorHealth;
    }

    public class DroneStatus : MavStatus
    {
        public float GroundSpeed { get; set; }

        DateTime lastAltitude = DateTime.MinValue;
        private volatile float altitude = 0;
        float oldAltitude = 0;
        public override float Altitude
        {
            get { return altitude; }
            set
            {
                // check update rate, and ensure time hasnt gone backwards                
                altitude = value;
                if ((PacketTime - lastAltitude).TotalSeconds >= 0.2 && oldAltitude != Altitude || lastAltitude > PacketTime)
                {
                    VerticalSpeed = (Altitude - oldAltitude) / (float)(PacketTime - lastAltitude).TotalSeconds;
                    if (float.IsInfinity(verticalSpeed))
                        verticalSpeed = 0;
                    lastAltitude = PacketTime;
                    oldAltitude = Altitude;
                }
            }
        }

        float verticalSpeed = 0;
        public float VerticalSpeed
        {
            get => verticalSpeed;
            set
            {
                verticalSpeed = verticalSpeed * 0.4f + value * 0.6f;
                if (float.IsNaN(verticalSpeed)) verticalSpeed = 0;
            }
        }

        float airspeed = 0;
        public float AirSpeed
        {
            get => airspeed*1; // multiplierspeed:1
            set => airspeed = value;
        }

        public uint FlightMode { get; set; }
        public FlightMode FlightModeType { get; set; } = Diva.Mavlink.FlightMode.CopterMode;

        public float NAVBearing { get; set; } = float.NaN;

        public float Roll { get; set; } = float.NaN;

        private float yaw = float.NaN;
        public float Yaw
        {
            get { return yaw; }
            set { yaw = value < 0 ? value + 360 : value; }
        }

        public float Pitch { get; set; } = float.NaN;

        private float groundCourse = float.NaN;
        public float GroundCourse
        {
            get { return groundCourse; }
            set { groundCourse = value < 0 ? value + 360 : value; }
        }

        public float ekfvelv { get; set; }

        public float ekfcompv { get; set; }

        public float ekfposhor { get; set; }

        public float ekfposvert { get; set; }

        public MAV_STATE State { get; set; }

        public DateTime? ArmedSince { get; set; }

        private bool isArmed = false;
        public bool IsArmed
        {
            get => isArmed;
            set
            {
                if (value)
                {
                    if (!isArmed)
                        ArmedSince = DateTime.Now;
                }
                else
                    ArmedSince = null;
                isArmed = value;
            }
        }

        public ulong Capabilities { get; set; } = (ulong)MAV_PROTOCOL_CAPABILITY.MISSION_FLOAT;

        [Obsolete]
        public bool MissionIntSupport { get => (Capabilities & (ulong)MAV_PROTOCOL_CAPABILITY.MISSION_INT) != 0; }

        public List<Mission.WayPoint> Mission { get; set; } = new List<Mission.WayPoint>();

        [Obsolete]
        public mavlink_mission_item_t GuidedMode = new mavlink_mission_item_t();
    }
}
