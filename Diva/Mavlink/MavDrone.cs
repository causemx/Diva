﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Diva.Events;
using Diva.Mission;
using Diva.Utilities;
using static MAVLink;
using Strings = Diva.Properties.Strings;

namespace Diva.Mavlink
{
    public class MavDrone : MavCore<DroneStatus>
    {
        public DroneSetting Setting { get; }
        public override string Name => Setting?.Name ?? base.Name;
        public bool IsOpen => BaseStream?.IsOpen ?? false;
        public bool IsRotationStandby = true;
        public EventHandler<MAV_STATE> StateChangedEvent;
        public EventHandler<ModeChangedEventArgs> FlightModeChanged;

        public MavDrone(DroneSetting setting = null)
        {
            Setting = setting;
            RegisterMavMessageHandler(MAVLINK_MSG_ID.MISSION_CURRENT, MissionCurrentPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT, NavControllerOutputPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.HEARTBEAT, DroneHeartBeatPacketHandler);
            //RegisterMavMessageHandler(MAVLINK_MSG_ID.GLOBAL_POSITION_INT, GPSPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GPS_RAW_INT, GPSRawPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.AUTOPILOT_VERSION, AutopilotVersionHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.ATTITUDE, AttitudePacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.VFR_HUD, VfrHUDPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.EKF_STATUS_REPORT, EKFStatusReportHandler);
        }

        protected override bool IsValidId(MAVLinkMessage message)
            => message.IsMainComponent();

        public bool Connect()
        {
            BaseStream = MavStream.CreateStream(Setting);
            try
            {
                if (ConfigData.GetBoolOption("GenerateTLog"))
                    LogFile = new System.IO.BufferedStream(System.IO.File.Open(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".tlog", System.IO.FileMode.OpenOrCreate));
                Open();
                Status.ArmedSince = null;
                if (!IsOpen)
                {
                    log.Info("port not opened. existing connect");
                    Disconnect();
                    return false;
                }
            }
            catch (Exception)
            {
                Disconnect();
                throw;
            }
            return true;
        }

        public void Disconnect()
        {
            Close();
            try { LogFile?.Close(); } catch { }
            LogFile = null;
        }

        private const int SetRequestStreamPeriod = 30;
        private const int HomePositionUpdatePeriod = 5;
        private int SetRequestStreamCounter = 30;
        private int HomePositionUpdateCounter = 0;

        protected override void DoBackgroundWork()
        {
            ++SetRequestStreamCounter;
            ++HomePositionUpdateCounter;

            if (!BaseStream.IsOpen) return;
            // re-request streams
            try
            {
                if (HomePositionUpdateCounter >= HomePositionUpdatePeriod)
                {
                    HomePositionUpdateCounter = 0;
                    if (!Status.IsArmed || Status.State != MAV_STATE.ACTIVE) return;
                    var planner = Planner.GetPlannerInstance();
                    if (!planner.FullControl || !this.IsShip())
                    {
                        var loc = planner.HomeLocation;
                        if (loc != GMap.NET.PointLatLng.Empty)
                            SetHome(loc.Lat, loc.Lng);
                    }
                    return;
                }
                if (SetRequestStreamCounter >= SetRequestStreamPeriod)
                {
                    // diva actually handles position/raw sensors/raw controllers only
                    SetDataStreamFrequency(MAV_DATA_STREAM.ALL, 2);
                    /*SetDataStreamFrequency(MAV_DATA_STREAM.EXTENDED_STATUS, 2);
                    SetDataStreamFrequency(MAV_DATA_STREAM.POSITION, 2);
                    SetDataStreamFrequency(MAV_DATA_STREAM.EXTRA1, 4);
                    SetDataStreamFrequency(MAV_DATA_STREAM.EXTRA2, 4);
                    SetDataStreamFrequency(MAV_DATA_STREAM.EXTRA3, 2);
                    SetDataStreamFrequency(MAV_DATA_STREAM.RAW_SENSORS, 2);
                    SetDataStreamFrequency(MAV_DATA_STREAM.RAW_CONTROLLER, 2);
                    SetDataStreamFrequency(MAV_DATA_STREAM.RC_CHANNELS, 2);*/

                    // REQUEST_DATA_STREAM is replaced by SET_MESSAGE_INTERVAL
                    /*SetMessageInterval(MAVLINK_MSG_ID.MISSION_CURRENT, 500000);
                    SetMessageInterval(MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT, 500000);
                    SetMessageInterval(MAVLINK_MSG_ID.GPS_RAW_INT, 500000);
                    SetMessageInterval(MAVLINK_MSG_ID.ATTITUDE, 500000);*/
                    SetRequestStreamCounter = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("MavDrone background loop exception: " + e);
            }
        }

        #region Message packet handlers
        private void MissionCurrentPacketHandler(object holder, MAVLinkMessage packet)
        {
            var wpCur = GetMessage<mavlink_mission_current_t>(packet, ref holder);

            int wpno = wpCur.seq;
            int lastautowp = 0;

            if (this.IsMode("GUIDED") && wpno != 0)
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
            if (hb.type != (byte)MAV_TYPE.GCS)
            {
                uint mode = hb.custom_mode;
                bool armed = hb.base_mode.HasFlag(MAV_MODE_FLAG.SAFETY_ARMED);
                bool modechange = Status.FlightMode != mode || armed != Status.IsArmed;
                if (modechange)
                {
                    ModeChangedEventArgs me = new ModeChangedEventArgs(Status.FlightMode, mode);
                    Status.FlightMode = mode;
                    Status.IsArmed = armed;
                    FlightModeChanged?.Invoke(this, me);
                }
                var state = (MAV_STATE)hb.system_status;
                if (Status.State != state)
                {
                    Status.State = state;
                    StateChangedEvent?.Invoke(this, state);
                }
            }
        }

        /*private void GPSPacketHandler(object holder, MAVLinkMessage packet)
        {
            var loc = GetMessage<mavlink_global_position_int_t>(packet, ref holder);
            Status.Yaw = loc.hdg == UInt16.MaxValue ? float.NaN : loc.hdg / 100.0f;
        }*/

        private void GPSRawPacketHandler(object holder, MAVLinkMessage packet)
        {
            var gps = GetMessage<mavlink_gps_raw_int_t>(packet, ref holder);
            Status.GroundSpeed = gps.vel * 1.0e-2f;
            Status.GroundCourse = gps.cog * 1.0e-2f;
        }

        private void AttitudePacketHandler(object holder, MAVLinkMessage packet)
        {
            var att = GetMessage<mavlink_attitude_t>(packet, ref holder);
            Status.Roll = (float)(att.roll * 180 / Math.PI);
            Status.Yaw = (float)(att.yaw * 180 / Math.PI);
            Status.Pitch = (float)(att.pitch * 180 / Math.PI);
        }

        private void AutopilotVersionHandler(object holder, MAVLinkMessage packet)
        {
            var ver = GetMessage<mavlink_autopilot_version_t>(packet, ref holder);
            Status.Capabilities = ver.capabilities;
        }

        private void VfrHUDPacketHandler(object holder, MAVLinkMessage packet)
        {
            var vh = GetMessage<mavlink_vfr_hud_t>(packet, ref holder);
            float airspeed = vh.airspeed;
            Status.AirSpeed = airspeed;
        }

        private void EKFStatusReportHandler(object holder, MAVLinkMessage packet)
        {
            var status = GetMessage<mavlink_ekf_status_report_t>(packet, ref holder);
            Status.ekfvelv = status.velocity_variance;
            Status.ekfposhor = status.pos_horiz_variance;
            Status.ekfposvert = status.pos_vert_variance;
            Status.ekfcompv = status.compass_variance;
        }
        #endregion Message packet handlers

        #region Drone modes
        public bool DoArm(bool arm, bool force = false) =>
            SendCommandWaitAck(MAV_CMD.COMPONENT_ARM_DISARM, arm ? 1 : 0,
                force ? (arm ? 2989.0f : 21196.0f) : 0, 0, 0, 0, 0, 0, 10000);

        public bool TakeOff(float height) =>
            SendCommandWaitAck(MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, height);

        public bool StartMission() =>
            SendCommandWaitAck(MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);

        public bool ReturnToLaunch() =>
            SendCommandWaitAck(MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);

        public void SetMode(string targetModeName, bool waitReply = true)
        {
            uint targetMode = Status.FlightModeType[targetModeName];
            if (targetMode != Status.FlightMode)
            {
                var modePkt = new mavlink_set_mode_t
                {
                    target_system = SysId,
                    base_mode = (byte)MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
                    custom_mode = targetMode
                };
                Console.WriteLine("mode switching");
                if (waitReply)
                {
                    bool verify(MAVLinkMessage p, ref bool more) =>
                        p.ToStructure<mavlink_command_ack_t>().command == (ushort)MAVLINK_MSG_ID.SET_MODE;
                    bool accepted(MAVLinkMessage p) =>
                        p != null && MAV_RESULT.ACCEPTED ==
                            (MAV_RESULT)p.ToStructure<mavlink_command_ack_t>().result;
                    if (!accepted(SendPacketWaitReply(MAVLINK_MSG_ID.SET_MODE, modePkt,
                        MAVLINK_MSG_ID.COMMAND_ACK, verify)))
                    {
                        var ack = SendPacketWaitReply(MAVLINK_MSG_ID.SET_MODE, modePkt,
                            MAVLINK_MSG_ID.COMMAND_ACK, verify);
                        Console.WriteLine("SetMode retry ack: " + (accepted(ack) ? "ok" : "failed"));
                    }
                    else
                        Console.WriteLine("SetMode ack: ok");
                }
                else
                {
                    SendPacket(MAVLINK_MSG_ID.SET_MODE, modePkt);
                    System.Threading.Thread.Sleep(10);
                    SendPacket(MAVLINK_MSG_ID.SET_MODE, modePkt);
                }
            }
            else
                Console.WriteLine("No Mode Changed");
        }
        #endregion Drone modes

        public void SetHome(PointLatLngAlt home, MAV_FRAME frame = MAV_FRAME.GLOBAL_RELATIVE_ALT_INT, bool useCurrent = false) =>
             SetHome(home.Lat, home.Lng, home.Alt, frame, useCurrent);

        public void SetHome(double lat, double lng, double alt = 0, MAV_FRAME frame = MAV_FRAME.GLOBAL_RELATIVE_ALT_INT, bool useCurrent = false)
        {
            log.Info($"Send SetHome with CommandInt cmd lat {lat} lng {lng} alt {alt}");
            SendPacket(MAVLINK_MSG_ID.COMMAND_INT, new mavlink_command_int_t
            {
                target_system = SysId,
                target_component = CompId,
                frame = (byte)frame,
                command = (ushort)MAV_CMD.DO_SET_HOME,
                current = 0,
                autocontinue = 0,
                param1 = useCurrent ? 1 : 0,
                param2 = 0,
                param3 = 0,
                param4 = 0,
                x = (int)(lat * 1e7),
                y = (int)(lng * 1e7),
                z = (float)(alt / 100)
            });
        }

        #region Waypoints, RallyPoints and Fencepoints
        public PointLatLngAlt GetRallyPoint(int no, ref int total)
        {
            int retries = 3;
            var req = new mavlink_rally_fetch_point_t
            {
                idx = (byte)no,
                target_component = CompId,
                target_system = SysId
            };
            MAVLinkMessage reply;
            while (true)
            {
                reply = SendPacketWaitReply(MAVLINK_MSG_ID.RALLY_FETCH_POINT,
                    req, MAVLINK_MSG_ID.RALLY_POINT, null, 700);
                if (reply != null)
                {
                    var fp = reply.ToStructure<mavlink_rally_point_t>();
                    if (req.idx != fp.idx)
                        continue;
                    total = fp.count;
                    return new PointLatLngAlt
                    {
                        Alt = fp.alt,
                        Lat = fp.lat / 1.0e7,
                        Lng = fp.lng / 1.0e7,
                        Tag = fp.idx.ToString()
                    };
                }
                if (retries == 0)
                    throw new TimeoutException("Timeout on read - getRallyPoint");
                log.Info("getRallyPoint Retry " + retries);
                retries--;
            }
        }

        public PointLatLngAlt GetFencePoint(int no, ref int total)
        {
            var req = new mavlink_fence_fetch_point_t
            {
                idx = (byte)no,
                target_system = SysId,
                target_component = CompId
            };
            MAVLinkMessage reply = SendPacketWaitReply(MAVLINK_MSG_ID.FENCE_FETCH_POINT, req,
                    MAVLINK_MSG_ID.FENCE_POINT, null, 700, 3);
            if (reply == null)
                throw new TimeoutException("Timeout on read - getFencePoint");

            var fp = reply.ToStructure<mavlink_fence_point_t>();
            total = fp.count;
            return new PointLatLngAlt
            {
                Lat = fp.lat,
                Lng = fp.lng,
                Tag = fp.idx.ToString()
            };
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

        public int GetWPCount()
        {
            var req = new mavlink_mission_request_list_t
            {
                target_component = CompId,
                target_system = SysId
            };
            MAVLinkMessage reply =
                SendPacketWaitReply(MAVLINK_MSG_ID.MISSION_REQUEST_LIST, req,
                    MAVLINK_MSG_ID.MISSION_COUNT, null, 700, 6);
            if (reply == null)
                throw new TimeoutException("Timeout on read - GetWPCount");

            var wpc = reply.ToStructure<mavlink_mission_count_t>();
            log.Info("wpcount: " + wpc.count);

            // should be ushort, but apm has limited wp count < byte
            return wpc.count;
        }

        public WayPoint GetWP(int index)
        {
            int retries = 5;
            WayPoint loc = new WayPoint();
            object req;
            MAVLINK_MSG_ID msgid, repid;
            if (Status.MissionIntSupport)
            {
                msgid = MAVLINK_MSG_ID.MISSION_REQUEST_INT;
                repid = MAVLINK_MSG_ID.MISSION_ITEM_INT;
                req = new mavlink_mission_request_int_t
                {
                    target_system = SysId,
                    target_component = CompId,
                    seq = (ushort)index
                };
            }
            else
            {
                msgid = MAVLINK_MSG_ID.MISSION_REQUEST;
                repid = MAVLINK_MSG_ID.MISSION_ITEM;
                req = new mavlink_mission_request_t
                {
                    target_system = SysId,
                    target_component = CompId,
                    seq = (ushort)index
                };
            }

            while (true)
            {
                var reply = SendPacketWaitReply(msgid, req, repid, null, 3500);
                if (reply != null)
                {
                    if (reply.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM)
                    {
                        var wp = reply.ToStructure<mavlink_mission_item_t>();
                        if (index != wp.seq)
                            continue;
                        loc = wp;
                    }
                    else //if (reply.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM_INT)
                    {
                        var wp = reply.ToStructure<mavlink_mission_item_int_t>();
                        if (index != wp.seq)
                            continue;
                        loc = wp;
                        if (loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL ||
                                loc.Id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
                            loc.Latitude = wp.x;
                    }
                    log.InfoFormat($"GetWP {loc.Id} {loc.Param1} {loc.Altitude} {loc.Latitude} {loc.Longitude} opt {loc.Option}");
                    break;
                }
                if (--retries < 0)
                    throw new TimeoutException("Timeout on read - GetWP");
            }
            return loc;
        }

        public int GetRequestedWPNo()
        {
            // Question: shouldn't we check for MISSION_REQUEST_INT, too?
            var pkt = WaitPacket(MAVLINK_MSG_ID.MISSION_REQUEST, null, 5000);
            if (pkt != null)
            {
                var ans = pkt.ToStructure<mavlink_mission_request_t>();
                log.InfoFormat("GetRequestedWPNo seq {0} ts {1} tc {2}", ans.seq, ans.target_system, ans.target_component);
                return pkt.seq;
            }
            throw new TimeoutException("Timeout on read - GetRequestedWPNo");
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

        public MAV_MISSION_RESULT SetWP(WayPoint loc, int index, byte current = 0)
        {
            //byte contMode = (byte)((Status.Firmware == Firmwares.ArduPlane) ? 2 : 1);
            bool useint = Status.MissionIntSupport;
            var req = useint ?
                (object)loc.ToMissionItemInt(this, current) : loc.ToMissionItem(this, current);
            var msgid = useint ?
                MAVLINK_MSG_ID.MISSION_ITEM_INT : MAVLINK_MSG_ID.MISSION_ITEM;

            int retries = 10;
            var result = MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
            MAVLinkMessage reply;
            do
            {
                reply = SendPacketWaitReplies(msgid, req,
                new[]
                {
                    MAVLINK_MSG_ID.MISSION_ACK,
                    MAVLINK_MSG_ID.MISSION_REQUEST_INT,
                    MAVLINK_MSG_ID.MISSION_REQUEST
                },
                new ReplyPacketFilter[]
                {
                    (MAVLinkMessage p, ref bool more) =>
                    {
                        result = (MAV_MISSION_RESULT)p.ToStructure<mavlink_mission_ack_t>().type;
                        log.Info($"SetWP {index} ACK 47: {p.msgid} ans " +
                            Enum.Parse(typeof(MAV_MISSION_RESULT), result.ToString()));
                        return true;
                    },
                    (MAVLinkMessage p, ref bool more) =>
                    {
                        var m = p.ToStructure<mavlink_mission_request_int_t>();
                        bool seqOk = m.seq == (index + 1);
                        if (seqOk) log.Info($"SetWPi: doing {index} req {m.seq} REQ 40: {p.msgid}");
                        return seqOk;
                    },
                    (MAVLinkMessage p, ref bool more) =>
                    {
                        var m = p.ToStructure<mavlink_mission_request_t>();
                        bool seqOk = m.seq == (index + 1);
                        if (seqOk) log.Info($"SetWP: doing {index} req {m.seq} REQ 40: {p.msgid}");
                        return seqOk;
                    }
                }, 400);
            } while (reply == null && retries-- > 0);
            log.Info($"SetWP: reply=" + reply);

            if (reply == null)
                throw new TimeoutException("Timeout on read - SetWP");
            return result;
        }

        public MAV_MISSION_RESULT SetWPs(List<WayPoint> wps, WayPoint home, Action<int> reportCB)
        {
            if (Status.APName != MAV_AUTOPILOT.PX4) wps.Insert(0, home);

            int totalWPs = wps.Count;
            try
            {
                SetWPTotal(totalWPs);
            }
            catch (TimeoutException)
            {
                MessageBox.Show(Strings.MsgSaveWPTimeout);
            }

            MAV_MISSION_RESULT result = MAV_MISSION_RESULT.MAV_MISSION_INVALID;
            bool retry = false;
            for (int i = 0; i < totalWPs; i++)
            {
                var wp = wps[i];
                reportCB?.Invoke(i);

                // try send the wp
                try
                {
                    result = SetWP(wp, i);
                } catch (TimeoutException)
                {
                    if (retry)
                    {
                        MessageBox.Show(Strings.MsgSaveWPTimeout);
                        log.Error("Timeout after retry to set waypoint " +
                            (i == 0 ? "Home" : (Status.APName != MAV_AUTOPILOT.PX4 ? i - 1 : i).ToString()));
                        result = MAV_MISSION_RESULT.MAV_MISSION_ERROR;
                    } else
                    {
                        retry = true;
                        --i;
                        continue;
                    }
                }
                retry = false;

                // we timed out while uploading wps/ command wasnt replaced/ command wasnt added
                if (result == MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                {
                    if (i == 0)
                        SetWPTotal(totalWPs);
                    else
                        // resend for partial upload
                        SetWPPartialUpdate(i, totalWPs);
                    // reupload this point.
                    result = SetWP(wp, i);
                }

                switch (result)
                {
                    case MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE:
                        log.Error("Upload failed, please reduce the number of wp's");
                        throw new InsufficientMemoryException(
                            Strings.MsgMissionRejectedTooManyWaypoints, new Exception("SetWPs"));
                    case MAV_MISSION_RESULT.MAV_MISSION_INVALID:
                        log.Error("Upload failed, mission was rejected byt the Mav,\n " +
                            $"item had a bad option wp# {i} {result}");
                        throw new NotSupportedException(
                            Strings.MsgMissionRejectedBadWP.FormatWith(i, result),
                            new Exception("SetWPs"));
                    case MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE:
                        i = GetRequestedWPNo() - 1;
                        continue;
                    case MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED:
                        continue;
                    default:
                        log.Error($"Upload wps failed {((MAV_CMD)wp.Id).GetName()} {result.GetName()}");
                        throw new InvalidOperationException(
                            Strings.MsgMissionRejectedGeneral.FormatWith(
                                ((MAV_CMD)wp.Id).GetName(), result.GetName()),
                            new Exception("SetWPs"));
                }
            }

            SetWayPointAck();
            reportCB?.Invoke(-1);

            // set radius, is these required?
            //SetParam("WP_RADIUS", 30 / 1);
            //SetParam("WPNAV_RADIUS", 30 / 1 * 100.0);
            //try { SetAnyParam(new[] { "LOITER_RAD", "WP_LOITER_RAD" }, 45); } catch { }

            return result;
        }

        public List<WayPoint> GetWPs(Action<int> progCB, Func<bool> isCanceled)
        {
            int totalWPs = GetWPCount();
            List<WayPoint> wps = new List<WayPoint>(totalWPs);

            progCB?.Invoke(totalWPs);
            for (int i = 0; i < totalWPs; i++)
            {
                try { if (isCanceled()) return null; } catch { }
                log.Info("Getting WP" + i + ": " + (DateTime.Now.Second + DateTime.Now.Millisecond / 1000.0));
                wps.Add(GetWP(i));
                progCB?.Invoke(i);
            }
            SetWayPointAck();

            return wps;
        }

        public void SetWPPartialUpdate(int startwp, int endwp)
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

        public void SetWPTotal(int totalWPs)
        {
            var req = new mavlink_mission_count_t
            {
                target_system = SysId,
                target_component = CompId, // MSG_NAMES.MISSION_COUNT
                count = (ushort)totalWPs
            };
            MAVLinkMessage reply = SendPacketWaitReply(MAVLINK_MSG_ID.MISSION_COUNT, req,
                    MAVLINK_MSG_ID.MISSION_REQUEST,
                    (MAVLinkMessage p, ref bool more) =>
                    {
                        var mreq = p.ToStructure<mavlink_mission_request_t>();
                        log.Info("receive mission request feedback");
                        if (mreq.seq == 0)
                        {
                            if (Status.Params["WP_TOTAL"] != null)
                                Status.Params["WP_TOTAL"].Value = totalWPs - 1;
                            if (Status.Params["CMD_TOTAL"] != null)
                                Status.Params["CMD_TOTAL"].Value = totalWPs - 1;
                            if (Status.Params["MIS_TOTAL"] != null)
                                Status.Params["MIS_TOTAL"].Value = totalWPs - 1;
                            return true;
                        }
                        return false;
                    }, 700, 3);
            if (reply == null)
                throw new TimeoutException("Timeout on read - SetWPTotal");
        }

        private bool SetPositionTargetGlobalInt(MAV_FRAME frame, WayPoint pos)
        {
            // for mavlink SET_POSITION_TARGET messages
            const ushort MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE = ((1 << 0) | (1 << 1) | (1 << 2));
            const ushort MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE = ((0 << 0) | (0 << 1) | (1 << 2));

            var target = new mavlink_set_position_target_global_int_t
            {
                target_system = SysId,
                target_component = CompId,
                alt = (float)pos.Altitude,
                lat_int = (int)(pos.Latitude * 1e7),
                lon_int = (int)(pos.Longitude * 1e7),
                coordinate_frame = (byte)frame,
                vx = 0f,
                vy = 0f,
                vz = 0f,
                yaw = 0f,
                yaw_rate = 0f,
                type_mask = ushort.MaxValue
            };

            if (pos.Latitude != 0 && pos.Longitude != 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE;
            if (pos.Latitude == 0 && pos.Longitude == 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE;

            if (pos.Latitude != 0)
                Status.GuidedMode.x = (float)pos.Longitude;
            if (pos.Longitude != 0)
                Status.GuidedMode.y = (float)pos.Longitude;
            Status.GuidedMode.z = (float)pos.Altitude;

            SendPacket(MAVLINK_MSG_ID.SET_POSITION_TARGET_GLOBAL_INT, target);
            return true;
            //MAVLinkMessage msg = SendPacketWaitReply(MAVLINK_MSG_ID.SET_POSITION_TARGET_GLOBAL_INT, target, MAVLINK_MSG_ID.POSITION_TARGET_GLOBAL_INT);
            //MAVLinkMessage msg = WaitPacket(MAVLINK_MSG_ID.POSITION_TARGET_GLOBAL_INT, null, 1000);
#if !DEBUG
            var result = msg.ToStructure<mavlink_position_target_global_int_t>();
            bool altEquals() { return (result.type_mask & MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE) != 0 || target.alt == result.alt; }
            bool coordEquals(double tolerance)
            {
                return (result.type_mask & MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE) != 0 ||
                        Math.Abs(1 - (float)result.lat_int / target.lat_int) < tolerance &&
                        Math.Abs(1 - (float)result.lon_int / target.lon_int) < tolerance;
            }
            bool ae = altEquals(), ce = coordEquals(1e-4);
            return target.type_mask == result.type_mask &&
                // frame type changed results in altitude not comparable
                (target.coordinate_frame != result.coordinate_frame || ae) && ce;
#endif
        }

        public bool SetGuidedModeWP(WayPoint dest, bool setmode = true)
        {
            if (dest.Altitude == 0 || dest.Latitude == 0 || dest.Longitude == 0)
                return false;

            try
            {
                dest.Id = (ushort)MAV_CMD.WAYPOINT;
                // Must be Guided mode.
                if (setmode)
                    SetMode("GUIDED", false);
                log.InfoFormat($"SetGuidedModeWP {SysId}:{CompId}" +
                    $" lat {dest.Latitude} lng {dest.Longitude} alt {dest.Altitude}");
                if (Status.Firmware == Firmwares.ArduPlane)
                {
                    MAV_MISSION_RESULT ans = SetWP(dest, 0, 2);
                    if (ans != MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        throw new Exception("Guided Mode Failed");
                    return true;
                }
                else
                    return SetPositionTargetGlobalInt(MAV_FRAME.GLOBAL_RELATIVE_ALT_INT, dest);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return false;
        }
        #endregion Waypoints, RallyPoints and Fencepoints
    }
}
