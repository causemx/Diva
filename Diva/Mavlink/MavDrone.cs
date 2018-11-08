using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using log4net;
using Diva.Utilities;

namespace Diva.Mavlink
{
    public class MavDrone : MavCore
    {
        public string Name => setting?.Name;

        private MavCore mav => this as MavCore;
        private DroneSetting setting;
        public bool IsOpen => BaseStream?.IsOpen ?? false;
        public byte SysId => (byte)sysidcurrent;
        public byte CompId => (byte)compidcurrent;

        public MavDrone(DroneSetting setting = null)
        {
            this.setting = setting;
            RegisterMavMessageHandler(MAVLINK_MSG_ID.MISSION_CURRENT, MissionCurrentPacketHandler);

            // NAV_CONTROLLER_OUTPUT is for fixed wing
            //RegisterMavMessageHandler(MAVLINK_MSG_ID.NAV_CONTROLLER_OUTPUT, NavControllerOutputPacketHandler);
        }

        public bool Connect()
        {
            BaseStream = MavBaseStream.CreateStream(setting);

            try
            {
                // reset connect time - for timeout functions
                DateTime connecttime = DateTime.Now;

                // do the connect
                Open();
                if (!IsOpen)
                {
                    log.Info("comport is closed. existing connect");
                    Close();
                    return false;
                }

                // set to first seen
                sysidcurrent = MAVlist.First().sysid;
                compidcurrent = MAVlist.First().compid;
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
        private void MissionCurrentPacketHandler(MAVLinkMessage packet)
        {
            var wpCur = packet.ToStructure<mavlink_mission_current_t>();

            int wpno = 0;
            int lastautowp = 0;

            int oldwp = (int)wpno;

            wpno = wpCur.seq;

            if (Status.mode == 4 && wpno != 0)
            {
                lastautowp = (int)wpno;
            }
        }

        /*private void NavControllerOutputPacketHandler(MAVLinkMessage packet)
        {
            var nav = packet.ToStructure<mavlink_nav_controller_output_t>();

            float nav_roll = nav.nav_roll;
            float nav_pitch = nav.nav_pitch;
            short nav_bearing = nav.nav_bearing;
            short target_bearing = nav.target_bearing;
            ushort wp_dist = nav.wp_dist;
            float alt_error = nav.alt_error;
            float aspd_error = nav.aspd_error / 100.0f;
            float xtrack_error = nav.xtrack_error;

            Status.nav_bearing = nav_bearing;
        }*/
        #endregion Message packet handlers

        #region Drone modes
        public bool DoArm(bool arm) => arm ?
            DoCommand(MAV_CMD.COMPONENT_ARM_DISARM, 1,
#if FORCE_ARM
                                                                    2989.0f
#else
                                                                    0
#endif
                                                                            , 0, 0, 0, 0, 0) :
            DoCommand(MAV_CMD.COMPONENT_ARM_DISARM, 0, 21196.0f, 0, 0, 0, 0, 0);

        public bool TakeOff(float height) => DoCommand(MAV_CMD.TAKEOFF, 0, 0, 0, 0, 0, 0, height);

        public void StartMission() => DoCommand(MAV_CMD.MISSION_START, 0, 0, 0, 0, 0, 0, 0);

        public void ReturnToLaunch() => DoCommand(MAV_CMD.RETURN_TO_LAUNCH, 0, 0, 0, 0, 0, 0, 0);

        public void SetMode(string targetmode)
        {
            uint modeval;
            if (MavUtlities.GetFlightModeByName(targetmode, out modeval))
            {
                var mode = new mavlink_set_mode_t()
                {
                    target_system = SysId,
                    base_mode = (byte)MAV_MODE_FLAG.CUSTOM_MODE_ENABLED,
                    custom_mode = modeval
                };
                Console.WriteLine("mode switching");
                SendPacket(MAVLINK_MSG_ID.SET_MODE, mode);
                Thread.Sleep(10);
                SendPacket(MAVLINK_MSG_ID.SET_MODE, mode);
            }
            else
                Console.WriteLine("No Mode Changed");
        }
        #endregion Drone modes

        #region Waypoints, RallyPoints and Fencepoints
        public PointLatLngAlt GetRallyPoint(int no, ref int total)
        {
            MAVLinkMessage buffer;

            PortInUse = true;

            PointLatLngAlt plla = new PointLatLngAlt();
            mavlink_rally_fetch_point_t req = new mavlink_rally_fetch_point_t()
            {
                idx = (byte)no,
                target_component = CompId,
                target_system = SysId
            };

            // request point
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

                buffer = ReadPacket();
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

                        plla.Lat = fp.lat / 1.0e7;
                        plla.Lng = fp.lng / 1.0e7;
                        plla.Tag = fp.idx.ToString();
                        plla.Alt = fp.alt;

                        total = fp.count;

                        PortInUse = false;

                        return plla;
                    }
                }
            }
        }

        public PointLatLngAlt GetFencePoint(int no, ref int total)
        {
            MAVLinkMessage buffer;

            PortInUse = true;

            PointLatLngAlt plla = new PointLatLngAlt();
            mavlink_fence_fetch_point_t req = new mavlink_fence_fetch_point_t()
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

                buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.FENCE_POINT)
                    {
                        PortInUse = false;

                        mavlink_fence_point_t fp = buffer.ToStructure<mavlink_fence_point_t>();

                        plla.Lat = fp.lat;
                        plla.Lng = fp.lng;
                        plla.Tag = fp.idx.ToString();

                        total = fp.count;

                        return plla;
                    }
                }
            }
        }

        public bool SetFencePoint(byte index, PointLatLngAlt plla, byte fencepointcount)
        {
            var fp = new mavlink_fence_point_t()
            {
                idx = index,
                count = fencepointcount,
                lat = (float)plla.Lat,
                lng = (float)plla.Lng,
                target_component = CompId,
                target_system = SysId
            };

            int retry = 3;

            PointLatLngAlt newfp;

            while (retry > 0)
            {
                SendPacket(MAVLINK_MSG_ID.FENCE_POINT, fp);
                int counttemp = 0;
                newfp = GetFencePoint(fp.idx, ref counttemp);

                if (newfp.GetDistance(plla) < 5)
                    return true;
                retry--;
            }

            throw new Exception("Could not verify GeoFence Point");
        }

        public ushort GetWPCount()
        {
            PortInUse = true;
            MAVLinkMessage buffer;
            mavlink_mission_request_list_t req = new mavlink_mission_request_list_t();

            req.target_system = Status.sysid;
            req.target_component = Status.compid;

            // request list
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
                    //return (byte)int.Parse(param["WP_TOTAL"].ToString());
                    throw new TimeoutException("Timeout on read - getWPCount");
                }

                buffer = ReadPacket();
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

        public Locationwp GetWP(ushort index)
        {
            while (PortInUse)
            {
                System.Threading.Thread.Sleep(100);
            }

            // bool use_int = (MAV.cs.capabilities & MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;
            bool use_int = true;

            object req;

            if (use_int)
            {
                mavlink_mission_request_int_t reqi = new mavlink_mission_request_int_t();

                reqi.target_system = Status.sysid;
                reqi.target_component = Status.compid;

                reqi.seq = index;

                // request
                SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_INT, reqi);

                req = reqi;
            }
            else
            {
                mavlink_mission_request_t reqf = new mavlink_mission_request_t();

                reqf.target_system = Status.sysid;
                reqf.target_component = Status.compid;

                reqf.seq = index;

                // request
                SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST, reqf);

                req = reqf;
            }

            PortInUse = true;
            Locationwp loc = new Locationwp();

            DateTime start = DateTime.Now;
            int retrys = 5;

            while (true)
            {
                if (!(start.AddMilliseconds(3500) > DateTime.Now)) // apm times out after 5000ms
                {
                    if (retrys > 0)
                    {
                        log.Info("getWP Retry " + retrys);
                        if (use_int)
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_INT, req);
                        else
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - getWP");
                }
                //Console.WriteLine("getwp read " + DateTime.Now.Millisecond);
                MAVLinkMessage buffer = ReadPacket();
                //Console.WriteLine("getwp readend " + DateTime.Now.Millisecond);
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM)
                    {
                        //Console.WriteLine("getwp ans " + DateTime.Now.Millisecond);

                        var wp = buffer.ToStructure<mavlink_mission_item_t>();

                        // received a packet, but not what we requested
                        if (index != wp.seq)
                        {
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST, req);
                            continue;
                        }

                        loc.options = (byte)(wp.frame);
                        loc.id = (ushort)(wp.command);
                        loc.p1 = (wp.param1);
                        loc.p2 = (wp.param2);
                        loc.p3 = (wp.param3);
                        loc.p4 = (wp.param4);

                        loc.alt = ((wp.z));
                        loc.lat = ((wp.x));
                        loc.lng = ((wp.y));

                        log.InfoFormat("getWP {0} {1} {2} {3} {4} opt {5}", loc.id, loc.p1, loc.alt, loc.lat, loc.lng,
                            loc.options);

                        break;
                    }
                    else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ITEM_INT)
                    {
                        //Console.WriteLine("getwp ans " + DateTime.Now.Millisecond);

                        var wp = buffer.ToStructure<mavlink_mission_item_int_t>();

                        // received a packet, but not what we requested
                        if (index != wp.seq)
                        {
                            SendPacket(MAVLINK_MSG_ID.MISSION_REQUEST_INT, req);
                            continue;
                        }

                        loc.options = (byte)(wp.frame);
                        loc.id = (ushort)(wp.command);
                        loc.p1 = (wp.param1);
                        loc.p2 = (wp.param2);
                        loc.p3 = (wp.param3);
                        loc.p4 = (wp.param4);

                        loc.alt = ((wp.z));
                        loc.lat = ((wp.x / 1.0e7));
                        loc.lng = ((wp.y / 1.0e7));

                        if (loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
                        {
                            loc.lat = wp.x;
                        }

                        log.InfoFormat("getWPint {0} {1} {2} {3} {4} opt {5}", loc.id, loc.p1, loc.alt, loc.lat, loc.lng,
                            loc.options);

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
                    throw new TimeoutException("Timeout on read - getRequestedWPNo");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_request_t>();

                        log.InfoFormat("getRequestedWPNo seq {0} ts {1} tc {2}", ans.seq, ans.target_system, ans.target_component);

                        PortInUse = false;

                        return ans.seq;
                    }
                }
            }
        }

        private void SetPositionTargetGlobalInt(bool pos, bool vel, bool acc, bool yaw, MAV_FRAME frame, double lat, double lng, double alt, double vx, double vy, double vz, double yawangle, double yawrate)
        {
            // for mavlink SET_POSITION_TARGET messages
            const ushort MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE = ((1 << 0) | (1 << 1) | (1 << 2));
            const ushort MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE = ((0 << 0) | (0 << 1) | (1 << 2));
            const ushort MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE = ((1 << 3) | (1 << 4) | (1 << 5));
            const ushort MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE = ((1 << 6) | (1 << 7) | (1 << 8));
            const ushort MAVLINK_SET_POS_TYPE_MASK_YAW_IGNORE = ((1 << 10) | (1 << 11));

            mavlink_set_position_target_global_int_t target = new mavlink_set_position_target_global_int_t()
            {
                target_system = Status.sysid,
                target_component = Status.compid,
                alt = (float)alt,
                lat_int = (int)(lat * 1e7),
                lon_int = (int)(lng * 1e7),
                coordinate_frame = (byte)frame,
                vx = (float)vx,
                vy = (float)vy,
                vz = (float)vz,
                yaw = (float)yawangle,
                yaw_rate = (float)yawrate
            };

            target.type_mask = ushort.MaxValue;

            if (pos && lat != 0 && lng != 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE;
            if (pos && lat == 0 && lng == 0)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ALT_IGNORE;
            if (vel)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE;
            if (acc)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE;
            if (yaw)
                target.type_mask -= MAVLINK_SET_POS_TYPE_MASK_YAW_IGNORE;

            if (pos)
            {
                if (lat != 0)
                    MAVlist[Status.sysid, Status.compid].GuidedMode.x = (float)lat;
                if (lng != 0)
                    MAVlist[Status.sysid, Status.compid].GuidedMode.y = (float)lng;
                MAVlist[Status.sysid, Status.compid].GuidedMode.z = (float)alt;
            }

            bool pos_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_POS_IGNORE) > 0;
            bool vel_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_VEL_IGNORE) > 0;
            bool acc_ignore = (target.type_mask & MAVLINK_SET_POS_TYPE_MASK_ACC_IGNORE) > 0;

            SendPacket(MAVLINK_MSG_ID.SET_POSITION_TARGET_GLOBAL_INT, target);
        }

        public void SetWayPointAck()
        {
            mavlink_mission_ack_t req = new mavlink_mission_ack_t()
            {
                target_component = CompId,
                target_system = SysId,
                type = 0
            };

            SendPacket(MAVLINK_MSG_ID.MISSION_ACK, req);
        }

        public MAV_MISSION_RESULT SetWP(Locationwp loc, ushort index, MAV_FRAME frame, bool use_int = false)
        {
            byte contmode = (byte)((MAVlist[Status.sysid, Status.compid].firmware == MavUtlities.Firmwares.ArduPlane) ? 2 : 1);

            if (use_int)
            {
                bool camcon = loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONTROL || loc.id == (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE;
                double x = loc.lat, y = loc.lng;
                if (loc.id != (ushort)MAV_CMD.DO_DIGICAM_CONTROL && loc.id != (ushort)MAV_CMD.DO_DIGICAM_CONFIGURE)
                {
                    x *= 1.0e7;
                    y *= 1.0e7;
                }

                mavlink_mission_item_int_t req = new mavlink_mission_item_int_t()
                {
                    target_component = Status.compid,
                    target_system = Status.sysid,
                    command = loc.id,
                    current = 0,
                    autocontinue = contmode,
                    frame = (byte)frame,
                    x = (int)x,
                    y = (int)y,
                    z = loc.alt,
                    param1 = loc.p1,
                    param2 = loc.p2,
                    param3 = loc.p3,
                    param4 = loc.p4,
                    seq = index
                };

                return SetWP(req);
            }
            else
            {
                mavlink_mission_item_t req = new mavlink_mission_item_t()
                {
                    target_component = Status.compid,
                    target_system = Status.sysid,
                    command = loc.id,
                    current = 0,
                    autocontinue = contmode,
                    frame = (byte)frame,
                    x = (float)loc.lat,
                    y = (float)loc.lng,
                    z = loc.alt,
                    param1 = loc.p1,
                    param2 = loc.p2,
                    param3 = loc.p3,
                    param4 = loc.p4,
                    seq = index
                };

                return SetWP(req);
            }
        }

        private MAV_MISSION_RESULT SetWP(mavlink_mission_item_t req)
        {
            PortInUse = true;

            ushort index = req.seq;

            // request
            SendPacket(MAVLINK_MSG_ID.MISSION_ITEM, req);


            DateTime start = DateTime.Now;
            int retrys = 10;

            while (true)
            {
                if (!(start.AddMilliseconds(400) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("setWP Retry " + retrys);
                        SendPacket(MAVLINK_MSG_ID.MISSION_ITEM, req);

                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - setWP");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ACK)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_ack_t>();
                        log.Info("set wp " + index + " ACK 47 : " + buffer.msgid + " ans " +
                                 Enum.Parse(typeof(MAV_MISSION_RESULT), ans.type.ToString()));


                        if (req.current == 2)
                        {
                            // MAVlist[req.target_system, req.target_component].GuidedMode = req;
                        }
                        else if (req.current == 3)
                        {
                        }
                        else
                        {
                            // MAVlist[req.target_system, req.target_component].wps[req.seq] = req;
                        }

                        //if (ans.target_system == req.target_system && ans.target_component == req.target_component)
                        {
                            PortInUse = false;
                            return (MAV_MISSION_RESULT)ans.type;
                        }
                    }
                    else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_request_t>();
                        if (ans.seq == (index + 1))
                        {
                            log.Info("set wp doing " + index + " req " + ans.seq + " REQ 40 : " + buffer.msgid);
                            PortInUse = false;

                            if (req.current == 2)
                            {
                                // MAVlist[req.target_system, req.target_component].GuidedMode = req;
                            }
                            else if (req.current == 3)
                            {
                            }
                            else
                            {
                                // MAVlist[req.target_system, req.target_component].wps[req.seq] = req;
                            }

                            //if (ans.target_system == req.target_system && ans.target_component == req.target_component)
                            {
                                PortInUse = false;
                                return MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
                            }
                        }
                        else
                        {
                            start = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        // Console.WriteLine(DateTime.Now + " PC setwp " + buffer.msgid);
                    }
                }
            }

            // return MAV_MISSION_RESULT.MAV_MISSION_INVALID;
        }

        private MAV_MISSION_RESULT SetWP(mavlink_mission_item_int_t req)
        {
            PortInUse = true;

            ushort index = req.seq;

            // request
            SendPacket(MAVLINK_MSG_ID.MISSION_ITEM_INT, req);

            DateTime start = DateTime.Now;
            int retrys = 10;

            while (true)
            {
                if (!(start.AddMilliseconds(400) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("setWP Retry " + retrys);
                        SendPacket(MAVLINK_MSG_ID.MISSION_ITEM_INT, req);

                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - setWP");
                }
                MAVLinkMessage buffer = ReadPacket();
                if (buffer.Length > 5)
                {
                    if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_ACK)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_ack_t>();
                        log.Info("set wp " + index + " ACK 47 : " + buffer.msgid + " ans " +
                                 Enum.Parse(typeof(MAV_MISSION_RESULT), ans.type.ToString()));
                        PortInUse = false;

                        if (req.current == 2)
                        {
                            // MAVlist[req.target_system, req.target_component].GuidedMode = (Locationwp)req;
                        }
                        else if (req.current == 3)
                        {
                        }
                        else
                        {
                            // MAVlist[req.target_system, req.target_component].wps[req.seq] = (Locationwp)req;
                        }

                        //if (ans.target_system == req.target_system && ans.target_component == req.target_component)
                        {
                            PortInUse = false;
                            return (MAV_MISSION_RESULT)ans.type;
                        }
                    }
                    else if (buffer.msgid == (byte)MAVLINK_MSG_ID.MISSION_REQUEST)
                    {
                        var ans = buffer.ToStructure<mavlink_mission_request_t>();
                        if (ans.seq == (index + 1))
                        {
                            log.Info("set wp doing " + index + " req " + ans.seq + " REQ 40 : " + buffer.msgid);
                            PortInUse = false;

                            if (req.current == 2)
                            {
                                // MAVlist[req.target_system, req.target_component].GuidedMode = (Locationwp)req;
                            }
                            else if (req.current == 3)
                            {

                            }
                            else
                            {
                                // MAVlist[req.target_system, req.target_component].wps[req.seq] = (Locationwp)req;
                            }

                            //if (ans.target_system == req.target_system && ans.target_component == req.target_component)
                            {
                                PortInUse = false;
                                return MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED;
                            }
                        }
                        else
                        {

                            // resend point now
                            start = DateTime.MinValue;
                        }
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now + " PC setwp " + buffer.msgid);
                    }
                }
            }

            // return MAV_MISSION_RESULT.MAV_MISSION_INVALID;
        }

        public void SetWPPartialUpdate(ushort startwp, ushort endwp)
        {
            mavlink_mission_write_partial_list_t req = new mavlink_mission_write_partial_list_t();

            req.target_system = Status.sysid;
            req.target_component = Status.compid;

            req.start_index = (short)startwp;
            req.end_index = (short)endwp;

            SendPacket(MAVLINK_MSG_ID.MISSION_WRITE_PARTIAL_LIST, req);
        }

        public void SetWPTotal(ushort wp_total)
        {
            PortInUse = true;
            mavlink_mission_count_t req = new mavlink_mission_count_t();

            req.target_system = Status.sysid;
            req.target_component = Status.compid; // MSG_NAMES.MISSION_COUNT

            req.count = wp_total;

            SendPacket(MAVLINK_MSG_ID.MISSION_COUNT, req);

            DateTime start = DateTime.Now;
            int retrys = 3;

            while (true)
            {
                if (!(start.AddMilliseconds(700) > DateTime.Now))
                {
                    if (retrys > 0)
                    {
                        log.Info("setWPTotal Retry " + retrys);
                        SendPacket(MAVLINK_MSG_ID.MISSION_COUNT, req);
                        start = DateTime.Now;
                        retrys--;
                        continue;
                    }
                    PortInUse = false;
                    throw new TimeoutException("Timeout on read - setWPTotal");
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
                            if (Status.param["WP_TOTAL"] != null)
                                Status.param["WP_TOTAL"].Value = wp_total - 1;
                            if (Status.param["CMD_TOTAL"] != null)
                                Status.param["CMD_TOTAL"].Value = wp_total - 1;
                            if (Status.param["MIS_TOTAL"] != null)
                                Status.param["MIS_TOTAL"].Value = wp_total - 1;

                            Status.wps.Clear();

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

        public void SetGuidedModeWP(Locationwp gotohere)
        {
            if (gotohere.alt == 0 || gotohere.lat == 0 || gotohere.lng == 0)
                return;

            PortInUse = true;

            try
            {
                gotohere.id = (ushort)MAV_CMD.WAYPOINT;

                // Must be Guided mode.s
                // fix for followme change
                (this as MavDrone)?.SetMode("GUIDED");

                log.InfoFormat("setGuidedModeWP {0}:{1} lat {2} lng {3} alt {4}", Status.sysid, Status.compid, gotohere.lat, gotohere.lng, gotohere.alt);

                if (MAVlist[Status.sysid, Status.compid].firmware == MavUtlities.Firmwares.ArduPlane)
                {
                    MAV_MISSION_RESULT ans = SetWP(gotohere, 0, MAV_FRAME.GLOBAL_RELATIVE_ALT);

                    if (ans != MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        throw new Exception("Guided Mode Failed");
                }
                else
                {
                    SetPositionTargetGlobalInt(
                        true, false, false, false, MAV_FRAME.GLOBAL_RELATIVE_ALT_INT,
                        gotohere.lat, gotohere.lng, gotohere.alt, 0, 0, 0, 0, 0);
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
