using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Diva.Utilities;

namespace Diva.Mavlink
{
    public class MavDrone
    {
        public static readonly ILog log = Planner.log;
        public string Name => setting?.Name;

        private MavlinkInterface mav;
        private DroneSetting setting;
        public MavStatus Status => mav.Status;
        public bool IsOpen => mav.BaseStream?.IsOpen ?? false;
        public bool giveComport { get => mav.giveComport; set => mav.giveComport = value; }

        public MavDrone(DroneSetting setting = null)
        {
            mav = new MavlinkInterface();
            this.setting = setting;
        }

        public bool Connect()
        {
            mav.BaseStream = MavBaseStream.CreateStream(setting);

            try
            {
                // reset connect time - for timeout functions
                DateTime connecttime = DateTime.Now;

                // do the connect
                mav.Open();
                if (!IsOpen)
                {
                    log.Info("comport is closed. existing connect");
                    mav.Close();
                    return false;
                }

                // get all the params
                foreach (var mavstate in mav.MAVlist)
                {
                    mav.sysidcurrent = mavstate.sysid;
                    mav.compidcurrent = mavstate.compid;
                    // TODO: comPort.getParamList();
                }

                // set to first seen
                mav.sysidcurrent = mav.MAVlist.First().sysid;
                mav.compidcurrent = mav.MAVlist.First().compid;
            }
            catch (Exception e)
            {
                mav.Close();
                throw e;
            }
            return true;
        }

        public void Disconnect() => mav.Close();

        #region Intermediate functions before replacing MavlinkInterface
        public KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>>
            SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID type,
                Func<MAVLink.MAVLinkMessage, bool> function, bool exclusive = false)
            => mav.SubscribeToPacketType(type, function, exclusive);
                public void UnSubscribeToPacketType
                (KeyValuePair<MAVLink.MAVLINK_MSG_ID, Func<MAVLink.MAVLinkMessage, bool>> item)
            => mav.UnSubscribeToPacketType(item);

        public void sendPacket(object indata, int sysid, int compid)
            => mav.sendPacket(indata, sysid, compid);
        public bool doCommand(MAVLink.MAV_CMD actionid, float p1, float p2, float p3, float p4, float p5,
                float p6, float p7, bool requireack = true)
            => mav.doCommand(actionid, p1, p2, p3, p4, p5, p6, p7, requireack);

        public bool setParam(string[] paramnames, double value) => mav.setParam(paramnames, value);
        public bool setParam(string paramname, double value, bool force = false)
            => mav.setParam(paramname, value, force);
        public void GetParam(string name) => mav.GetParam(name);
        public void getParamList() => mav.getParamList();
        public bool doARM(bool armit) => mav.doARM(armit);
        public void setMode(string modein) => mav.setMode(modein);
        public void setMode(byte sysid, byte compid, MAVLink.mavlink_set_mode_t mode,
                MAVLink.MAV_MODE_FLAG base_mode = 0)
            => mav.setMode(sysid, compid, mode, base_mode);
        public void setGuidedModeWP(Locationwp gotohere, bool setguidedmode = true)
            => mav.setGuidedModeWP(gotohere, setguidedmode);

        public MAVLink.MAV_MISSION_RESULT setWP(Locationwp loc, ushort index, MAVLink.MAV_FRAME frame,
                byte current = 0, byte autocontinue = 1, bool use_int = false)
            => mav.setWP(loc, index, frame, current, autocontinue, use_int);
        public void setWPTotal(ushort wp_total) => mav.setWPTotal(wp_total);
        public void setWPPartialUpdate(ushort startwp, ushort endwp) => mav.setWPPartialUpdate(startwp, endwp);
        public void setWPACK() => mav.setWPACK();
        public int getRequestedWPNo() => mav.getRequestedWPNo();
        public int getWPCount() => mav.getWPCount();
        public Locationwp getWP(ushort index) => mav.getWP(index);

        public PointLatLngAlt getRallyPoint(int no, ref int total) => mav.getRallyPoint(no, ref total);
        public bool setFencePoint(byte index, PointLatLngAlt plla, byte fencepointcount)
            => mav.setFencePoint(index, plla, fencepointcount);

#endregion

        /*public void SaveWPs(DataGridView dgvWayPoints, Locationwp home)
        {
            try
            {

                if (!IsOpen)
                {
                    throw new Exception("Please connect first!");
                    // MessageBox.Show(ResStrings.MsgConnectFirst);
                    //return;
                }

                mav.giveComport = true;
                int a = 0;

                // log
                log.Info("wps values " + Status.wps.Values.Count);
                log.Info("cmd rows " + (dgvWayPoints.Rows.Count + 1)); // + home

                // check for changes / future mod to send just changed wp's
                if (Status.wps.Values.Count == (dgvWayPoints.Rows.Count + 1))
                {
                    Hashtable wpstoupload = new Hashtable();

                    a = -1;
                    foreach (var item in ActiveDrone.Status.wps.Values)
                    {
                        // skip home
                        if (a == -1)
                        {
                            a++;
                            continue;
                        }

                        MAVLink.mavlink_mission_item_t temp = DataViewtoLocationwp(a);

                        if (temp.command == item.command &&
                            temp.x == item.x &&
                            temp.y == item.y &&
                            temp.z == item.z &&
                            temp.param1 == item.param1 &&
                            temp.param2 == item.param2 &&
                            temp.param3 == item.param3 &&
                            temp.param4 == item.param4
                            )
                        {
                            log.Info("wp match " + (a + 1));
                        }
                        else
                        {
                            log.Info("wp no match" + (a + 1));
                            wpstoupload[a] = "";
                        }

                        a++;
                    }
                }

                uint capabilities = (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_FLOAT;
                bool use_int = (capabilities & (uint)MAVLink.MAV_PROTOCOL_CAPABILITY.MISSION_INT) > 0;

                // set wp total
                // ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set total wps ");

                ushort totalwpcountforupload = (ushort)(dgvWayPoints.Rows.Count + 1);

                if (Status.apname == MAVLink.MAV_AUTOPILOT.PX4)
                {
                    totalwpcountforupload--;
                }

                try
                {
                    ActiveDrone.setWPTotal(totalwpcountforupload);
                }
                catch (TimeoutException)
                {
                    MessageBox.Show(Properties.Strings.MsgSaveWPTimeout);
                }
                // + home

                // set home location - overwritten/ignored depending on firmware.
                // ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(0, "Set home");

                // upload from wp0
                a = 0;

                if (Status.apname != MAVLink.MAV_AUTOPILOT.PX4)
                {
                    try
                    {
                        var homeans = ActiveDrone.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                MessageBox.Show(Properties.Strings.MsgSaveWPRejected.FormatWith(1));
                                return;
                            }
                        }
                        a++;
                    }
                    catch (TimeoutException)
                    {
                        use_int = false;
                        // added here to prevent timeout errors
                        ActiveDrone.setWPTotal(totalwpcountforupload);
                        var homeans = ActiveDrone.setWP(home, (ushort)a, MAVLink.MAV_FRAME.GLOBAL, 0, 1, use_int);
                        if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                        {
                            if (homeans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                            {
                                MessageBox.Show(Properties.Strings.MsgSaveWPRejected.FormatWith(2));
                                return;
                            }
                        }
                        a++;
                    }
                }
                else
                {
                    use_int = false;
                }

                // define the default frame.
                MAVLink.MAV_FRAME frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;

                // get the command list from the datagrid
                //var commandlist = GetCommandList();
                var commandlist = from dgvWa

                // process commandlist to the mav
                for (a = 1; a <= commandlist.Count; a++)
                {
                    var temp = commandlist[a - 1];

                    // ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(a * 100 / Commands.Rows.Count,
                    //	"Setting WP " + a);

                    // make sure we are using the correct frame for these commands
                    if (temp.id < (ushort)MAVLink.MAV_CMD.LAST || temp.id == (ushort)MAVLink.MAV_CMD.DO_SET_HOME)
                    {
                        var mode = altmode.Relative;

                        if (mode == altmode.Terrain)
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT;
                        }
                        else if (mode == altmode.Absolute)
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL;
                        }
                        else
                        {
                            frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT;
                        }
                    }

                    // handle current wp upload number
                    int uploadwpno = a;
                    if (ActiveDrone.Status.apname == MAVLink.MAV_AUTOPILOT.PX4)
                        uploadwpno--;

                    // try send the wp
                    MAVLink.MAV_MISSION_RESULT ans = ActiveDrone.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);

                    // we timed out while uploading wps/ command wasnt replaced/ command wasnt added
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ERROR)
                    {
                        // resend for partial upload
                        ActiveDrone.setWPPartialUpdate((ushort)(uploadwpno), totalwpcountforupload);
                        // reupload this point.
                        ans = ActiveDrone.setWP(temp, (ushort)(uploadwpno), frame, 0, 1, use_int);
                    }

                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_NO_SPACE)
                    {
                        MessageBox.Show(ResStrings.MsgMissionRejectedTooManyWaypoints);
                        log.Error("Upload failed, please reduce the number of wp's");
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID)
                    {

                        MessageBox.Show(ResStrings.MsgMissionRejectedBadWP.FormatWith(a, ans));
                        log.Error("Upload failed, mission was rejected byt the Mav,\n " +
                            "item had a bad option wp# " + a + " " +
                            ans);
                        return;
                    }
                    if (ans == MAVLink.MAV_MISSION_RESULT.MAV_MISSION_INVALID_SEQUENCE)
                    {
                        // invalid sequence can only occur if we failed to see a response from the apm when we sent the request.
                        // or there is io lag and we send 2 mission_items and get 2 responces, one valid, one a ack of the second send

                        // the ans is received via mission_ack, so we dont know for certain what our current request is for. as we may have lost the mission_request

                        // get requested wp no - 1;
                        a = ActiveDrone.getRequestedWPNo() - 1;

                        continue;
                    }
                    if (ans != MAVLink.MAV_MISSION_RESULT.MAV_MISSION_ACCEPTED)
                    {

                        MessageBox.Show(ResStrings.MsgMissionRejectedGeneral.FormatWith(
                            Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()),
                            Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString())));
                        Console.WriteLine("Upload wps failed " + Enum.Parse(typeof(MAVLink.MAV_CMD), temp.id.ToString()) +
                                         " " + Enum.Parse(typeof(MAVLink.MAV_MISSION_RESULT), ans.ToString()));
                        return;
                    }
                }

                ActiveDrone.setWPACK();

                // ((ProgressReporterDialogue)sender).UpdateProgressAndStatus(95, "Setting params");

                // m
                ActiveDrone.setParam("WP_RADIUS", float.Parse("30") / 1);

                // cm's
                ActiveDrone.setParam("WPNAV_RADIUS", float.Parse("30") / 1 * 100.0);

                // Remind the user after uploading the mission into firmware.
                MessageBox.Show(ResStrings.MsgMissionAcceptWP.FormatWith(a));
            }
            catch (Exception ex)
            {
                log.Error(ex);
                ActiveDrone.giveComport = false;
                throw;
            }

            ActiveDrone.giveComport = false;
        }*/

    }
}
