using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Diva.Comms;
using Diva.Utilities;

namespace Diva.Mavlink
{
    public class MavDrone
    {
        public static readonly ILog log = Planner.log;
        public string Name => setting?.Name;

        private MavlinkInterface mav;
        private DroneSetting setting;
        public bool IsOpen => mav.BaseStream.IsOpen;
        public MavStatus Status => mav.Status;
        public static implicit operator MavlinkInterface(MavDrone drone) => drone.mav;

        public MavDrone(DroneSetting setting = null)
        {
            mav = new MavlinkInterface();
            this.setting = setting;
        }

        public bool Connect()
        {
            string portname = setting.PortName.ToLower();
            // Setup comport.basestream
            switch (portname)
            {
                case "udp":
                    mav.BaseStream = new UdpSerial(setting.PortNumber);
                    break;
                default:
                    mav.BaseStream = new SerialPort();
                    break;
            }

            try
            {
                log.Info("Set Portname");
                // set port, then options
                if (portname != "preset")
                    mav.BaseStream.PortName = portname;

                log.Info("Set Baudrate");
                string baud = setting.Baudrate;
                try
                {
                    if (baud != "" && baud != "0")
                        mav.BaseStream.BaudRate = int.Parse(baud);
                }
                catch (Exception exp)
                {
                    log.Error(exp);
                }

                // prevent serialreader from doing anything
                mav.giveComport = true;

                mav.giveComport = false;


                // reset connect time - for timeout functions
                DateTime connecttime = DateTime.Now;

                // do the connect
                mav.open();
                if (!mav.BaseStream.IsOpen)
                {
                    log.Info("comport is closed. existing connect");
                    try
                    {
                        mav.close();
                    }
                    catch
                    {
                    }
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
            catch (Exception ex)
            {
                log.Warn(ex);
                try
                {
                    mav.close();
                }
                catch (Exception ex2)
                {
                    log.Warn(ex2);
                }
                MessageBox.Show(Properties.Strings.MsgCannotEstablishConnection
                    .FormatWith(ex.Message));
                throw new Exception();
            }

            return true;
        }

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
