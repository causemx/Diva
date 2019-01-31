using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Diva.Utilities;
using static MAVLink;

namespace Diva.Mavlink
{
    public class MavDrone : MavCore<DroneStatus>
    {
        public DroneSetting Setting { get; }
        public string Name => Setting?.Name;
        public bool IsOpen => BaseStream?.IsOpen ?? false;
        public bool IsRotationStandby = true;

        public MavDrone(DroneSetting setting = null)
        {
            Setting = setting;
            RegisterMavMessageHandler(MAVLINK_MSG_ID.MISSION_CURRENT, MissionCurrentPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT, NavControllerOutputPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.HEARTBEAT, DroneHeartBeatPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GLOBAL_POSITION_INT, GPSPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GPS_RAW_INT, GPSRawPacketHandler);
        }

        public bool Connect()
        {
            BaseStream = MavStream.CreateStream(Setting);
            try
            {
                DateTime connecttime = DateTime.Now;
                Open();
                if (!IsOpen)
                {
                    log.Info("comport is closed. existing connect");
                    Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                Close();
                throw e;
            }
            return true;
        }

        public void Disconnect() => Close();

        #region Message packet handlers
        private void MissionCurrentPacketHandler(object holder, MAVLinkMessage packet)
        {
            var wpCur = GetMessage<mavlink_mission_current_t>(packet, ref holder);

            int wpno = wpCur.seq;
            int lastautowp = 0;

            if (Status.FlightMode == (int)MavUtlities.FlightMode.GUIDED && wpno != 0)
            {
                lastautowp = (int)wpno;
            }
        }

        private void NavControllerOutputPacketHandler(object holder, MAVLinkMessage packet)
        {
            var nav = GetMessage<mavlink_nav_controller_output_t>(packet, ref holder);
            Status.NAVBearing = nav.nav_bearing;
        }

        private void DroneHeartBeatPacketHandler(object holder, MAVLinkMessage packet)
        {
            var hb = GetMessage<mavlink_heartbeat_t>(packet, ref holder);
            Status.FlightMode = hb.custom_mode;
            Status.State = hb.system_status;
            if (hb.type != (byte)MAV_TYPE.GCS)
            {
                Status.IsArmed = hb.base_mode.HasFlag(MAV_MODE_FLAG.SAFETY_ARMED);
                Status.State = hb.system_status;
            }
        }

        private void GPSPacketHandler(object holder, MAVLinkMessage packet)
        {
            var loc = GetMessage<mavlink_global_position_int_t>(packet, ref holder);
            Status.Yaw = loc.hdg == UInt16.MaxValue ? float.NaN : loc.hdg / 100.0f;
        }

        private void GPSRawPacketHandler(object holder, MAVLinkMessage packet)
        {
            var gps = GetMessage<mavlink_gps_raw_int_t>(packet, ref holder);
            Status.GroundSpeed = gps.vel * 1.0e-2f;
            Status.GroundCourse = gps.cog * 1.0e-2f;
        }
        #endregion Message packet handlers

        #region Drone modes
        public bool DoArm(bool arm) =>
            SendCommandWaitAck(MAV_CMD.COMPONENT_ARM_DISARM, 1,
                arm ?
#if FORCE_ARM
                    2989.0f
#else
                    0
#endif
                : 21196.0f, 0, 0, 0, 0, 0, 10000);

        public bool TakeOff(float height) =>
            SendCommandWaitAck(MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, height);

        public void StartMission() =>
            SendCommandWaitAck(MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);

        public void ReturnToLaunch() =>
            SendCommandWaitAck(MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);

        public void SetMode(string targetmode)
        {
            if (MavUtlities.GetFlightModeByName(targetmode, out uint modeval))
            {
                bool verify(MAVLinkMessage p, ref bool more) =>
                    p.ToStructure<mavlink_command_ack_t>().command == (ushort)MAVLINK_MSG_ID.SET_MODE;
                bool accepted(MAVLinkMessage p) =>
                    p != null && MAV_RESULT.ACCEPTED ==
                        (MAV_RESULT)p.ToStructure<mavlink_command_ack_t>().result;
                var mode = new mavlink_set_mode_t
                {
                    target_system = SysId,
                    base_mode = (byte)MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
                    custom_mode = modeval
                };
                Console.WriteLine("mode switching");
                PortInUse = true;
                if (!accepted(SendPacketWaitReply(MAVLINK_MSG_ID.SET_MODE, mode,
                    MAVLINK_MSG_ID.COMMAND_ACK, verify)))
                {
                    var ack = SendPacketWaitReply(MAVLINK_MSG_ID.SET_MODE, mode,
                        MAVLINK_MSG_ID.COMMAND_ACK, verify);
                    Console.WriteLine("SetMode retry ack: " + (accepted(ack) ? "ok" : "failed"));
                }
                else
                    Console.WriteLine("SetMode ack: ok");
                PortInUse = false;
            }
            else
                Console.WriteLine("No Mode Changed");
        }
        #endregion Drone modes

        #region Waypoints, RallyPoints and Fencepoints
        public PointLatLngAlt GetRallyPoint(int no, ref int total)
        {
            PortInUse = true;

            var req = new mavlink_rally_fetch_point_t
            {
                idx = (byte)no,
                target_component = CompId,
                target_system = SysId
            };
            SendPacket(MAVLINK_MSG_ID.RALLY_FETCH_POINT, req);

            DateTime start = DateTime.Now;
            int retrys = 3;
            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("getRallyPoint Retry " + retrys + " - giv com " + PortInUse);
                        SendPacket(MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - getRallyPoint");
                }

                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.RALLY_POINT)
                    {
                        var fp = buffer.ToStructure<mavlink_rally_point_t>();
                        if (req.idx != fp.idx)
                        {
                            SendPacket(MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
                            continue;
                        }
                        total = fp.count;
                        PortInUse = false;
                        return new PointLatLngAlt
                                {
                                    Alt = fp.alt,
                                    Lat = fp.lat / 1.0e7,
                                    Lng = fp.lng / 1.0e7,
                                    Tag = fp.idx.ToString()
                                };
                    }
                }
            }
        }

        public PointLatLngAlt GetFencePoint(int no, ref int total)
        {
            PortInUse = true;
            var req = new mavlink_fence_fetch_point_t
            {
                idx = (byte)no,
                target_system = SysId,
                target_component = CompId
            };
            SendPacket(MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);

            DateTime start = DateTime.Now;
            int retrys = 3;

            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("getFencePoint Retry " + retrys + " - giv com " + PortInUse);
                        SendPacket(MAVLINK_MSG_ID.FENCE_FETCH_POINT, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - getFencePoint");
                }

                var buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.FENCE_POINT)
                    {
                        PortInUse = false;
                        mavlink_fence_point_t fp = buffer.ToStructure<mavlink_fence_point_t>();
                        total = fp.count;
                        return new PointLatLngAlt
                                {
                                    Lat = fp.lat,
                                    Lng = fp.lng,
                                    Tag = fp.idx.ToString()
                                };
                    }
                }
            }
        }

        public bool SetFencePoint(byte index, PointLatLngAlt plla, byte fencepointcount)
        {
            var fp = new mavlink_fence_point_t
            {
                idx = index,
                count = fencepointcount,
                lat = (float)plla.Lat,
                lng = (float)plla.Lng,
                target_component = CompId,
                target_system = SysId
            };

            int retry = 3;
            while (retry > 0)
            {
                SendPacket(MAVLINK_MSG_ID.FENCE_POINT, fp);
                int counttemp = 0;
                var newfp = GetFencePoint(fp.idx, ref counttemp);

                if (newfp.GetDistance(plla) < 5)
                    return true;
                retry--;
            }

            throw new Exception("Could not verify GeoFence Point");
        }

        public ushort GetWPCount()
        {
            PortInUse = true;
            var req = new mavlink_mission_request_list_t
            {
                target_component = CompId,
                target_system = SysId
            };
            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_LIST, req);

            DateTime start = DateTime.Now;
            int retrys = 6;
            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("getWPCount Retry " + retrys + " - giv com " + PortInUse);
                        SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_LIST, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - getWPCount");
                }

                var buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_COUNT)
                    {
                        var count = buffer.ToStructure<mavlink_mission_count_t>();
                        log.Info("wpcount: " + count.count);
                        PortInUse = false;
                        return count.count; // should be ushort, but apm has limited wp count < byte
                    }
                }
            }
        }

        public WayPoint GetWP(ushort index)
        {
            //while (PortInUse) Thread.Sleep(100);

            bool use_int = true;
            object req;
            var msgid = use_int ? MAVLINK_MSG_ID.MISSION_REQUEST_INT : MAVLINK_MSG_ID.MISSION_REQUEST;
            if (use_int)
            {
                req = new mavlink_mission_request_int_t
                {
                    target_system = SysId,
                    target_component = CompId,
                    seq = index
                };
            }
            else
            {
                req = new mavlink_mission_request_t
                {
                    target_system = SysId,
                    target_component = CompId,
                    seq = index
                };
            }
            PortInUse = true;
            SendPacket(msgid, req);
            WayPoint loc = new WayPoint();

            DateTime start = DateTime.Now;
            int retrys = 5;
            while (true)
            {
                if (!(start.AddMilliseconds(3500) > DateTime.Now)) // apm times out after 5000ms
                {
                    if (retrys > 0)
                    {
                        log.Info("getWP Retry " + retrys);
                        SendPacket(msgid, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - getWP");
                }
                var buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM)
                    {
                        var wp = buffer.ToStructure<mavlink_mission_item_t>();
                        if (index != wp.seq)
                        {
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST, req);
                            continue;
                        }

                        loc = wp;
                        log.InfoFormat("getWP {0} {1} {2} {3} {4} opt {5}", loc.Id, loc.Param1, loc.Altitude, loc.Latitude, loc.Longitude,
                            loc.Option);
                        break;
                    }
                    else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM_INT)
                    {
                        var wp = buffer.ToStructure<mavlink_mission_item_int_t>();
                        if (index != wp.seq)
                        {
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_INT, req);
                            continue;
                        }

                        loc = wp;
                        if (loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
                        {
                            loc.Latitude = wp.x;
                        }

                        log.InfoFormat("getWPint {0} {1} {2} {3} {4} opt {5}", loc.Id, loc.Param1, loc.Altitude, loc.Latitude, loc.Longitude,
                            loc.Option);

                        break;
                    }
                    else
                    {
                        //log.Info(DateTime.Now + " PC getwp " + buffer.msgid);
                    }
                }
            }
            PortInUse = false;
            return loc;
        }

        public int GetRequestedWPNo()
        {
            PortInUse = true;
            DateTime start = DateTime.Now;

            while (true)
            {
                if (!(start.AddMilliseconds(5000) > DateTime.Now))
                {
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - GetRequestedWPNo");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5 && buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                {
                    var ans = buffer.ToStructure<mavlink_mission_request_t>();
                    log.InfoFormat("GetRequestedWPNo seq {0} ts {1} tc {2}", ans.seq, ans.target_system, ans.target_component);
                    PortInUse = false;
                    return ans.seq;
                }
            }
        }

        private void SetPositionTargetGlobalInt(MAV_FRAME frame, double lat, double lng, double alt)
        {
            // for mavlink SET_POSITION_TARGET messages
            const ushort MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE = ((1 << 0) | (1 << 1) | (1 << 2));
            const ushort MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE = ((0 << 0) | (0 << 1) | (1 << 2));

            var target = new mavlink_set_position_target_global_int_t
            {
                target_system = SysId,
                target_component = CompId,
                alt = (float)alt,
                lat_int = (int)(lat * 1e7),
                lon_int = (int)(lng * 1e7),
                coordinate_frame = (byte)frame,
                vx = 0f,
                vy = 0f,
                vz = 0f,
                yaw = 0f,
                yaw_rate = 0f,
                type_mask = ushort.MaxValue
            };

            if (lat != 0 && lng != 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE;
            if (lat == 0 && lng == 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE;

            if (lat != 0)
                Status.GuidedMode.x = (float)lat;
            if (lng != 0)
                Status.GuidedMode.y = (float)lng;
            Status.GuidedMode.z = (float)alt;

            SendPacket(MAVLINK_MSG_ID.SET_POSITION_TARGET_GLOBAL_INT, target);
        }

        public void SetWayPointAck()
        {
            SendPacket(MAVLINK_MSG_ID.MISSION_ACK,
                new mavlink_mission_ack_t
                {
                    target_component = CompId,
                    target_system = SysId,
                    type = 0
                });
        }

        public MAV_MISSION_RESULT SetWP(WayPoint loc, ushort index, MAV_FRAME frame, bool use_int = false)
        {
            byte contmode = (byte)((Status.firmware == MavUtlities.Firmwares.ArduPlane) ? 2 : 1);
            object req;
            if (use_int)
            {
                bool camcon = loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE;
                double x = loc.Latitude, y = loc.Longitude;
                if (loc.Id != (ushort)MAV_CMD.DO_DIGICAM_CONTROL && loc.Id != (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
                {
                    x *= 1.0e7;
                    y *= 1.0e7;
                }

                req = new mavlink_mission_item_int_t
                {
                    target_component = CompId,
                    target_system = SysId,
                    command = loc.Id,
                    current = 0,
                    autocontinue = contmode,
                    frame = (byte)frame,
                    x = (int)x,
                    y = (int)y,
                    z = loc.Altitude,
                    param1 = loc.Param1,
                    param2 = loc.Param2,
                    param3 = loc.Param3,
                    param4 = loc.Param4,
                    seq = index
                };
            }
            else
            {
                req = new mavlink_mission_item_t
                {
                    target_component = CompId,
                    target_system = SysId,
                    command = loc.Id,
                    current = 0,
                    autocontinue = contmode,
                    frame = (byte)frame,
                    x = (float)loc.Latitude,
                    y = (float)loc.Longitude,
                    z = loc.Altitude,
                    param1 = loc.Param1,
                    param2 = loc.Param2,
                    param3 = loc.Param3,
                    param4 = loc.Param4,
                    seq = index
                };
            }

            var msgid = use_int ? MAVLINK_MSG_ID.MISSION_ITEM_INT : MAVLINK_MSG_ID.MISSION_ITEM;
            PortInUse = true;
            SendPacket(msgid, req);

            DateTime start = DateTime.Now;
            int retrys = 10;
            while (true)
            {
                if (!(start.AddMilliseconds(400) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("SetWP Retry " + retrys);
                        SendPacket(msgid, req);

                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - SetWP");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ACK)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_ack_t>();
                        log.Info("SetWP " + index + " ACK 47 : " + buffer.msgid + " ans " +
                                 Enum.Parse(typeof(MAV_MISSION_RESULT), ans.type.ToString()));
                        PortInUse = false;
                        return (MAV_MISSION_RESULT)ans.type;
                    }
                    else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_request_t>();
                        if (ans.seq == (index + 1))
                        {
                            log.Info("set wp doing " + index + " req " + ans.seq + " REQ 40 : " + buffer.msgid);
                            PortInUse = false;
                            return MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
                        }
                        else
                        {
                            start = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now + " PC SetWP " + buffer.msgid);
                    }
                }
            }
        }

        public void SetWPPartialUpdate(ushort startwp, ushort endwp)
        {
            SendPacket(MAVLINK_MSG_ID.MISSION_WRITE_PARTIAL_LIST,
                new mavlink_mission_write_partial_list_t
                {
                    target_system = SysId,
                    target_component = CompId,
                    start_index = (short)startwp,
                    end_index = (short)endwp
                });
        }

        public void SetWPTotal(ushort wp_total)
        {
            PortInUse = true;
            var req = new mavlink_mission_count_t
            {
                target_system = SysId,
                target_component = CompId, // MSG_NAMES.MISSION_COUNT
                count = wp_total
            };
            SendPacket(MAVLINK_MSG_ID.MISSION_COUNT, req);

            DateTime start = DateTime.Now;
            int retrys = 3;

            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("SetWPTotal Retry " + retrys);
                        SendPacket(MAVLINK_MSG_ID.MISSION_COUNT, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - SetWPTotal");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 9)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                    {
                        var request = buffer.ToStructure<mavlink_mission_request_t>();

                        log.Info("receive mission request feedback");

                        if (request.seq == 0)
                        {
                            if (Status.Params["WP_TOTAL"] != null)
                                Status.Params["WP_TOTAL"].Value = wp_total - 1;
                            if (Status.Params["CMD_TOTAL"] != null)
                                Status.Params["CMD_TOTAL"].Value = wp_total - 1;
                            if (Status.Params["MIS_TOTAL"] != null)
                                Status.Params["MIS_TOTAL"].Value = wp_total - 1;

                            PortInUse = false;
                            return;
                        }
                    }
                    else
                    {
                        log.Info(DateTime.Now + " PC getwp " + buffer.msgid);
                    }
                }
            }
        }

        public void SetGuidedModeWP(WayPoint gotohere)
        {
            if (gotohere.Altitude == 0 || gotohere.Latitude == 0 || gotohere.Longitude == 0)
                return;

            PortInUse = true;

            try
            {
                gotohere.Id = (ushort)MAV_CMD.WAYPOINT;
                // Must be Guided mode.s
                // fix for followme change
                SetMode("GUIDED");
                log.InfoFormat("setGuidedModeWP {0}:{1} lat {2} lng {3} alt {4}",
                    SysId, CompId, gotohere.Latitude, gotohere.Longitude, gotohere.Altitude);
                if (Status.firmware == MavUtlities.Firmwares.ArduPlane)
                {
                    MAV_MISSION_RESULT ans = SetWP(gotohere, 0, MAV_FRAME.GLOBAL_RELATIVE_ALT);
                    if (ans != MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        throw new Exception("Guided Mode Failed");
                }
                else
                {
                    SetPositionTargetGlobalInt(MAV_FRAME.GLOBAL_RELATIVE_ALT_INT,
                        gotohere.Latitude, gotohere.Longitude, gotohere.Altitude);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            PortInUse = false;
        }
        #endregion Waypoints, RallyPoints and Fencepoints
    }
}
