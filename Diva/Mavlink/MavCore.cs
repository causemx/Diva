﻿using Diva.Controls;
using Diva.Properties;
using Diva.Utilities;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Timers.Timer;
using static MAVLink;
using Diva.Controls.Dialogs;

namespace Diva.Mavlink
{
    public class MavCore<StatusType> where StatusType : MavStatus
    {
        #region Constants
        public const double CONNECT_TIMEOUT_SECONDS = 30;
        public const int GET_PARAM_TIMEOUT = 1000;
        private const int GROUNDCONTROLSTATION_SYSTEM_ID = 255;
        #endregion Constants

        private MavStream baseStream;
        public MavStream BaseStream
        {
            get => baseStream;
            set
            {
                baseStream?.Dispose();
                baseStream = value;
            }
        }

        public StatusType Status = Activator.CreateInstance<StatusType>();

        public byte SysId { get; private set; }
        public byte CompId { get; private set; }
        public virtual string Name { get => baseStream?.StreamDescription ?? ""; }

        public BufferedStream LogFile { get; set; }
        public BufferedStream RawLogFile { get; set; }

        protected static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly object writeLock = new object();
        private int pacCount = 0;
        private byte mavlinkVersion = 0;
        private bool GPSLocationMode = false;
        private DialogProgress frmProgressReporter;

        #region Core
        public MavCore()
        {
            InitializeMavLinkMessageHandler();
        }

        public void Close()
        {
            //bgReaderLoop.Cancel();
            //bgReaderLoop = null;
            lock (mavs) mavs.Remove(this);
            try { BaseStream.Close(); } catch { }
        }

        public void Open()
        {
            frmProgressReporter = new DialogProgress("Connect", 
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning)
            {
                StartPosition = FormStartPosition.CenterScreen,
                Text = Strings.TextFormProgressConnection.FormatWith(
                        (this as MavDrone).Name ?? ""),
            };

            frmProgressReporter.DoWork += OpenBg;
            frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressSync);
            frmProgressReporter.RunBackgroundOperationAsync();
            frmProgressReporter.Dispose();
        }

        public void OpenBg(object PRsender, ProgressWorkerEventArgs progressWorkerEventArgs, object param = null)
        {
            bool gotHearbeat = false;
            frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressMavlinkConnecting);

            try
            {
                BaseStream.ReadBufferSize = 16 * 1024;

                lock (writeLock) // so we dont have random traffic
                {
                    log.Info("Open port with " + BaseStream.StreamDescription);

                    progressWorkerEventArgs.CancelRequestChanged += (o, e) =>
                            ((ProgressWorkerEventArgs)o).CancelAcknowledged = true;

                    BaseStream.Open();
                }

                lock (mavs) mavs.Add(this);
                //if (bgReaderLoop == null)
                //    bgReaderLoop = BackgroundLoop.Start(SerialReader);

                MAVLinkMessage buffer = MAVLinkMessage.Invalid;
                MAVLinkMessage buffer1 = MAVLinkMessage.Invalid;

                DateTime start = DateTime.Now;
                DateTime deadline = start.AddSeconds(CONNECT_TIMEOUT_SECONDS);

                // CountDown timer for connecting.
                var countDown = new Timer { Interval = 1000, AutoReset = false };
                countDown.Elapsed += (sender, e) =>
                {
                    int secondsRemaining = (deadline - e.SignalTime).Seconds;
                    frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgFormProgressTrying.FormatWith(secondsRemaining));
                    if (secondsRemaining > 0) countDown.Start();
                };
                countDown.Start();

                do
                {
                    if (progressWorkerEventArgs.CancelRequested)
                    {
                        progressWorkerEventArgs.CancelAcknowledged = true;
                        countDown.Stop();
                        if (BaseStream.IsOpen)
                            BaseStream.Close();
                        return;
                    }
                    log.Info(DateTime.Now.Millisecond + " Start connect loop ");
                    if (DateTime.Now > deadline)
                    {
                        countDown.Stop();
                        if (BaseStream.IsOpen)
                            BaseStream.Close();
                        progressWorkerEventArgs.ErrorMessage = Strings.MsgNoHeartBeat;
                        throw new Exception("Can not establish a connection\n");
                    }
                    Thread.Sleep(1);

                    frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgWaitHeartBeat);
                    // since we're having only 1 sysid/compid in this link, skip id checks
                    if (buffer.Length == 0)
                        buffer = GetHeartBeat(true);
                    // skip 2 hbs match test
                }
                while (buffer.Length <= 0);
                countDown.Stop();

                byte[] temp = ASCIIEncoding.ASCII.GetBytes("Diva GCS " + Assembly.GetEntryAssembly().GetName().Version + "\0");
                Array.Resize(ref temp, 50);

                SendPacket(MAVLINK_MSG_ID.STATUSTEXT,
                    new mavlink_statustext_t { severity = (byte)MAV_SEVERITY.INFO, text = temp });

                GetHeartBeat();

                frmProgressReporter.UpdateProgressAndStatus(-1, Strings.MsgAskFlightControllerVersion);
                GetVersion();

                gotHearbeat = true;

                frmProgressReporter.UpdateProgressAndStatus(0,
                    Strings.MsgGettingParams.FormatWith(new object[] { SysId, CompId }));
                // TODO: If wanna be quicker, mark parameter reader.
#if DEBUG
                GetParamListBG();
#endif

                if (frmProgressReporter.doWorkArgs.CancelAcknowledged)
                {
                    if (BaseStream.IsOpen)
                        BaseStream.Close();
                    return;
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (!gotHearbeat)
                        BaseStream.Close();
                    else
                        progressWorkerEventArgs.ErrorMessage = Strings.MsgFailedReadingParametersPleaseCheckConnection;
                }
                catch { }
                if (string.IsNullOrEmpty(progressWorkerEventArgs.ErrorMessage))
                    progressWorkerEventArgs.ErrorMessage = Strings.MsgConnectionFailed;
                log.Error(e);
                throw;
            }
            //frmProgressReporter.Close();
            frmProgressReporter.UpdateProgressAndStatus(100, Strings.MsgFormProgressDone);
            log.Info($"Done open {SysId} {CompId}");
            Status.PacketsLost = 0;
            Status.SyncLost = 0;
        }

        protected virtual bool IsValidId(MAVLinkMessage message) => false;

        public MAVLinkMessage ReadPacket()
        {
            byte[] buffer = new byte[MAVLINK_MAX_PACKET_LEN + 25];
            int count = 0;
            int length = 0;
            int readcount = 0;
            MAVLinkMessage message = null;
            string plainTextLine = "";
            string plaintxtline = "";
            DateTime start = DateTime.Now;

            while (BaseStream.IsOpen)
            {
                try
                {
                    if (readcount > 300)
                    {
                        break;
                    }
                    readcount++;

                    // time updated for internal reference
                    Status.PacketTime = DateTime.Now;
                    // 1200 ms between chars - the gps detection requires this.
                    DateTime to = DateTime.Now.AddMilliseconds(1200);

                    while (BaseStream.IsOpen && BaseStream.BytesAvailable <= 0)
                    {
                        if (DateTime.Now > to)
                        {
                            log.InfoFormat("MAVLINK: 1 wait time out btr {0} len {1}", BaseStream.BytesAvailable,
                                length);
                            throw new TimeoutException("Timeout");
                        }
                        Thread.Sleep(1);
                    }
                    if (BaseStream.IsOpen)
                    {
                        BaseStream.Read(buffer, count, 1);
                        if (RawLogFile?.CanWrite == true)
                            RawLogFile.WriteByte(buffer[count]);
                    }
                }
                catch (Exception e)
                {
                    log.Info("MAVLink readpacket read error: " + e.ToString());
                    break;
                }

                // check if looks like a mavlink packet and check for exclusions and write to console
                if (buffer[0] != 0xfe && buffer[0] != 'U' && buffer[0] != 0xfd)
                {
                    if ((buffer[0] >= 0x20 && buffer[0] <= 127) ||
                        buffer[0] == '\n' || buffer[0] == '\r')
                    {
                        // check for line termination
                        if (buffer[0] == '\r' || buffer[0] == '\n')
                        {
                            // check new line is valid
                            if (plainTextLine.Length > 3)
                                plaintxtline = plainTextLine;

                            log.Info(plaintxtline);
                            // reset for next line
                            plainTextLine = "";
                        }
                        plainTextLine += (char)buffer[0];
                    }
                    count = 0;
                    buffer[1] = 0;
                    continue;
                }
                readcount = 0;

                if (buffer[0] == 0xfe || buffer[0] == 0xfd || buffer[0] == 'U')
                {
                    var mavlinkv2 = buffer[0] == MAVLINK_STX;

                    int headerlength = mavlinkv2 ? MAVLINK_CORE_HEADER_LEN : MAVLINK_CORE_HEADER_MAVLINK1_LEN;
                    int headerlengthstx = headerlength + 1;

                    // if we have the header, and no other chars, get the length and packet identifiers
                    if (count == 0)
                    {
                        DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

                        while (BaseStream.IsOpen && BaseStream.BytesAvailable < headerlength)
                        {
                            if (DateTime.Now > to)
                            {
                                log.InfoFormat("MAVLINK: 2 wait time out btr {0} len {1}", BaseStream.BytesAvailable,
                                    length);
                                throw new TimeoutException("Timeout");
                            }
                            Thread.Sleep(1);
                        }
                        int read = BaseStream.Read(buffer, 1, headerlength);
                        count = read;
                        if (RawLogFile?.CanWrite == true)
                            RawLogFile.Write(buffer, 1, read);
                    }

                    // packet length
                    if (buffer[0] == MAVLINK_STX)
                    {
                        length = buffer[1] + headerlengthstx + MAVLINK_NUM_CHECKSUM_BYTES; // data + header + checksum - magic - length
                        if ((buffer[2] & MAVLINK_IFLAG_SIGNED) > 0)
                        {
                            length += MAVLINK_SIGNATURE_BLOCK_LEN;
                        }
                    }
                    else
                    {
                        length = buffer[1] + headerlengthstx + MAVLINK_NUM_CHECKSUM_BYTES; // data + header + checksum - U - length    
                    }

                    if (count >= headerlength)
                    {
                        try
                        {
                            DateTime to = DateTime.Now.AddMilliseconds(BaseStream.ReadTimeout);

                            while (BaseStream.IsOpen && BaseStream.BytesAvailable < (length - (headerlengthstx)))
                            {
                                if (DateTime.Now > to)
                                {
                                    log.InfoFormat("MAVLINK: 3 wait time out btr {0} len {1}",
                                        BaseStream.BytesAvailable, length);
                                    break;
                                }
                                Thread.Sleep(1);
                            }
                            if (BaseStream.IsOpen)
                            {
                                int read = BaseStream.Read(buffer, headerlengthstx, length - headerlengthstx);
                                if (read != (length - headerlengthstx))
                                    log.InfoFormat("MAVLINK: bad read hdrlen {0}, expecting {1}, read {2}, receiving {3}",
                                        headerlengthstx, length, count, read);
                                if (RawLogFile?.CanWrite == true)
                                {
                                    // write only what we read, temp is the whole packet, so 6-end
                                    RawLogFile.Write(buffer, headerlengthstx, read);
                                }
                            }
                            count = length;
                        }
                        catch
                        {
                            break;
                        }
                        break;
                    }
                }

                count++;
                if (count == 299)
                    break;
            }

            // resize the packet to the correct length
            Array.Resize(ref buffer, count);

            if (buffer.Length == 0)
                return MAVLinkMessage.Invalid;

            if (message == null)
                message = new MAVLinkMessage(buffer);

            uint msgid = message.msgid;

            message_info msginfo = MAVLINK_MESSAGE_INFOS.GetMessageInfo(msgid);

            // calc crc
            var sigsize = (message.sig != null) ? MAVLINK_SIGNATURE_BLOCK_LEN : 0;
            ushort crc = MavlinkCRC.crc_calculate(buffer, message.Length - sigsize - MAVLINK_NUM_CHECKSUM_BYTES);

            // calc extra bit of crc for mavlink 1.0/2.0
            if (message.header == 0xfe || message.header == 0xfd)
            {
                crc = MavlinkCRC.crc_accumulate(msginfo.crc, crc);
            }

            // check message length size vs table (mavlink1 explicit size check | mavlink2 oversize check, no undersize because of 0 trimming)
            if ((!message.ismavlink2 && message.payloadlength != msginfo.minlength)/* || (message.ismavlink2 && message.payloadlength > msginfo.length)*/)
            {
                if (msginfo.length == 0) // pass for unknown packets
                {
                    // log.InfoFormat("unknown packet type {0}", message.msgid);
                }
                else
                {
                    log.InfoFormat("Mavlinkv{2} Bad Packet Length {0} pkno {1} msgid {3}", buffer.Length, message.msgid, message.ismavlink2 ? 2 : 1, message.msgid);
                    return MAVLinkMessage.Invalid;
                }
            }

            // check crc
            if ((message.crc16 >> 8) != (crc >> 8) ||
                (message.crc16 & 0xff) != (crc & 0xff))
            {
                if (buffer.Length > 5 && msginfo.name != null)
                    log.InfoFormat("Mavlink Bad Packet (crc fail) len {0} crc {1} vs {4} pkno {2} {3}", buffer.Length,
                        crc, message.msgid, msginfo.name.ToString(),
                        message.crc16);
                return MAVLinkMessage.Invalid;
            }

            if (message.IsGCSPacket())
                return message;

            if (message.IsRadioPacket())
                return MAVLinkMessage.Invalid;

            byte sysid = message.sysid;
            byte compid = message.compid;
            byte packetSeqNo = message.seq;

            if (SysId == 0 && IsValidId(message))
            {
                log.Info($"MavCore opened with (sysid, compid)=({sysid},{compid})");
                SysId = sysid;
                CompId = compid;
                Status.MavLinkV2 = message.buffer[0] == MAVLINK_STX ? true : false;
            }
            else if (sysid != SysId)
            {
                log.Info($"MavCore ({SysId},{CompId}) received foreign packet ({sysid},{compid}), dropped");
                return MAVLinkMessage.Invalid;
            }
            else if (compid != CompId)
            {
                // handle multi component here
                log.Info($"MavCore ({SysId},{CompId}) received subcomponent packet ({sysid},{compid}), dropped");
                return MAVLinkMessage.Invalid;
            }

            // update packet loss statistics
            if (Status.PacketLostTimer.AddSeconds(5) < DateTime.Now)
            {
                Status.PacketLostTimer = DateTime.Now;
                Status.PacketsLost = Status.PacketsLost * 0.8f;
                Status.PacketsNotLost = Status.PacketsNotLost * 0.8f;
            }

            try
            {
                if ((message.header == 'U' || message.header == 0xfe || message.header == 0xfd) && buffer.Length >= message.payloadlength)
                {
                    // check if we lost pacakets based on seqno
                    int expectedSeqNo = ((Status.recvPacketCount + 1) % 0x100);

                    // the second part is to work around a 3dr radio bug sending dup seqno's
                    if (packetSeqNo != expectedSeqNo && packetSeqNo != Status.recvPacketCount)
                    {
                        Status.SyncLost++; // actualy sync loss's
                        int numLost = 0;
                        if (packetSeqNo < ((Status.recvPacketCount + 1)))
                        // recvpacketcount = 255 then   10 < 256 = true if was % 0x100 this would fail
                        {
                            numLost = 0x100 - expectedSeqNo + packetSeqNo;
                        }
                        else
                        {
                            numLost = packetSeqNo - expectedSeqNo;
                        }
                        Status.PacketsLost += numLost;
                    }
                    Status.PacketsNotLost++;
                    //Console.WriteLine("{0} {1}", sysid, packetSeqNo);
                    Status.recvPacketCount = packetSeqNo;

                    // packet stats per mav
                    if (!Status.PacketsPerSecond.ContainsKey(msgid) || double.IsInfinity(Status.PacketsPerSecond[msgid]))
                        Status.PacketsPerSecond[msgid] = 0;
                    if (!Status.PacketsPerSecondBuild.ContainsKey(msgid))
                        Status.PacketsPerSecondBuild[msgid] = DateTime.Now;

                    Status.PacketsPerSecond[msgid] = (((1000 /
                        ((DateTime.Now - Status.PacketsPerSecondBuild[msgid]).TotalMilliseconds) +
                                                        Status.PacketsPerSecond[msgid]) / 2));

                    Status.PacketsPerSecondBuild[msgid] = DateTime.Now;

                    // store packet history
                    Status.AddPacket(message);

                    LogPacket(buffer, msgid == 0);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            // update last valid packet receive time
            Status.LastPacket = DateTime.Now;

            return message;
        }

        private void LogPacket(byte[] packet, bool flush = false)
        {
            try
            {
                if (LogFile?.CanWrite == true)
                {
                    lock (LogFile)
                    {
                        byte[] timestamp =
                            BitConverter.GetBytes(
                                (UInt64)((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds * 1000));
                        Array.Reverse(timestamp);
                        LogFile.Write(timestamp, 0, timestamp.Length);
                        LogFile.Write(packet, 0, packet.Length);
                        if (flush)
                            LogFile.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private byte[] PreparePacket(MAVLINK_MSG_ID msgid, object indata)
        {
            int messageType = (byte)msgid;
            byte[] data = null;
            try
            {
                data = MavlinkUtil.StructureToByteArray(indata);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                int len = Marshal.SizeOf(indata);
                byte[] arr = new byte[len];
                IntPtr ptr = Marshal.AllocHGlobal(len);
                Marshal.StructureToPtr(indata, ptr, true);
                Marshal.Copy(ptr, arr, 0, len);
                Marshal.FreeHGlobal(ptr);
                data = arr;
            }
            var info = MAVLINK_MESSAGE_INFOS.SingleOrDefault(p => p.msgid == messageType);
            if (data.Length != info.minlength) Array.Resize(ref data, (int)info.minlength);
            byte[] packet = new byte[data.Length + 6 + 2];
            packet[0] = MAVLINK_STX_MAVLINK1;
            packet[1] = (byte)data.Length;
            // TODO: Overflow exception here. fix it!
            packet[2] = (byte)(++pacCount & 255);
            packet[3] = GROUNDCONTROLSTATION_SYSTEM_ID;
            packet[4] = (byte)MAV_COMPONENT.MAV_COMP_ID_MISSIONPLANNER;
            packet[5] = (byte)messageType;
            Array.Copy(data, 0, packet, 6, data.Length);

            ushort checksum = MavlinkCRC.crc_calculate(packet, packet[1] + 6);
            checksum = MavlinkCRC.crc_accumulate(MAVLINK_MESSAGE_INFOS.GetMessageInfo((uint)messageType).crc, checksum);
            packet[packet.Length - 2] = (byte)(checksum & 0xFF);
            packet[packet.Length - 1] = (byte)(checksum >> 8);

            LogPacket(packet);
            return packet;
        }

        public void SendPacket(MAVLINK_MSG_ID msgid, object indata)
        {
            byte[] pktdata = PreparePacket(msgid, indata);
            lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
        }

        public delegate bool WaitPacketFilter(MAVLinkMessage packet);
        public delegate bool ReplyPacketFilter(MAVLinkMessage packet, ref bool more);

        public MAVLinkMessage WaitPacket(MAVLINK_MSG_ID msgid,
            WaitPacketFilter filter = null, int timeoutms = Timeout.Infinite, bool gcs = false)
        {
            MAVLinkMessage reply = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                void eh(object _, MAVLinkMessage p)
                {
                    if (filter == null || filter(p))
                    {
                        reply = p;
                        ev.Set();
                    }
                }
                var pkt = Status.GetPacket(msgid);
                var lasttime = pkt?.rxtime ?? DateTime.Now;
                RegisterMavMessageHandler(msgid, eh, gcs);
                ev.WaitOne(timeoutms);
                UnregisterMavMessageHandler(msgid, eh, gcs);
                // last minute ride
                if (reply == null)
                {
                    pkt = Status.GetPacket(msgid);
                    if (pkt != null && pkt.rxtime > lasttime && (filter == null || filter(pkt)))
                        reply = pkt;
                }
            }
            return reply;
        }

        public MAVLinkMessage WaitPackets(MAVLINK_MSG_ID[] msgids,
            WaitPacketFilter[] filters, bool[] gcs = null, int timeoutms = Timeout.Infinite)
        {
            MAVLinkMessage reply = null;
            using (AutoResetEvent ev = new AutoResetEvent(false))
            {
                DateTime lasttime = DateTime.MinValue;
                var ehs = new EventHandler<MAVLinkMessage>[msgids.Length];
                for (var i = msgids.Length; i > 0;)
                {
                    int ival = --i;
                    ehs[ival] = (object _, MAVLinkMessage p) =>
                    {
                        if (filters[ival] == null || filters[ival](p))
                        {
                            reply = p;
                            ev.Set();
                        }
                    };
                    RegisterMavMessageHandler(msgids[ival], ehs[ival],
                        gcs != null && gcs.Length > ival && gcs[ival]);
                    var ipkt = Status.GetPacket(msgids[ival]);
                    if (ipkt != null && ipkt.rxtime > lasttime)
                        lasttime = ipkt.rxtime;
                }
                if (lasttime == DateTime.MinValue)
                    lasttime = DateTime.Now;
                ev.WaitOne(timeoutms);
                for (var i = msgids.Length; --i >= 0;)
                {
                    UnregisterMavMessageHandler(msgids[i], ehs[i],
                        gcs != null && gcs.Length > i && gcs[i]);
                    if (reply == null)
                    {
                        // last minute ride
                        var ipkt = Status.GetPacket(msgids[i]);
                        if (ipkt != null && ipkt.rxtime > lasttime &&
                                (filters[i] == null || filters[i](ipkt)))
                            reply = ipkt;
                    }
                }
            }
            return reply;
        }

        public MAVLinkMessage SendPacketWaitReply(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID replyid, ReplyPacketFilter filter = null, int timeoutms = 500, int retries = 1)
                => SendPacketWaitReply(msgid, indata, replyid, filter, false, timeoutms, retries);

        public MAVLinkMessage SendPacketWaitReply(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID replyid, ReplyPacketFilter filter, bool gcs, int timeoutms = 500, int retries = 1)
        {
            MAVLinkMessage reply = null;
            long dueticks = 0;
            using (ManualResetEvent ev = new ManualResetEvent(false))
            {
                void eh(object _, MAVLinkMessage p)
                {
                    bool more = false;
                    if (filter == null || filter(p, ref more))
                    {
                        if (more)
                        {
                            Volatile.Write(ref dueticks,
                                DateTime.Now.AddMilliseconds(timeoutms).Ticks);
                        }
                        else
                        {
                            reply = p;
                            ev.Set();
                        }
                    }
                }
                byte[] pktdata = PreparePacket(msgid, indata);
                var pkt = Status.GetPacket(replyid);
                var lasttime = pkt?.rxtime ?? DateTime.Now;
                RegisterMavMessageHandler(replyid, eh, gcs);
                bool notdone;
                do
                {
                    Volatile.Write(ref dueticks,
                        DateTime.Now.AddMilliseconds(timeoutms).Ticks);
                    lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
                    while ((notdone = !ev.WaitOne(new TimeSpan(dueticks - DateTime.Now.Ticks)))
                        && DateTime.Now.Ticks < Volatile.Read(ref dueticks)) ;
                    if (notdone)
                        log.Debug("SendPacketWaitReply timeouted: " +
                            DateTime.Now.ToString("HH:mm:ss.fff") + ", due " +
                            new DateTime(Volatile.Read(ref dueticks)).ToString("HH:mm:ss.fff"));
                } while (notdone && --retries > 0);
                UnregisterMavMessageHandler(replyid, eh, gcs);
                // last minute ride
                if (reply == null)
                {
                    pkt = Status.GetPacket(replyid);
                    bool more = false;
                    if (pkt != null && pkt.rxtime > lasttime && filter(pkt, ref more))
                        reply = pkt;
                }
            }
            return reply;
        }

        public MAVLinkMessage SendPacketWaitReplies(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID[] rids, ReplyPacketFilter[] filters = null, int timeoutms = 500, int retries = 1)
                => SendPacketWaitReplies(msgid, indata, rids, filters, null, timeoutms, retries);

        public MAVLinkMessage SendPacketWaitReplies(MAVLINK_MSG_ID msgid, object indata,
            MAVLINK_MSG_ID[] rids, ReplyPacketFilter[] filters, bool[] gcs, int timeoutms = 500, int retries = 1)
        {
            if (rids == null || filters == null || filters.Length == 0
                || rids.Length != filters.Length)
            {
                SendPacket(msgid, indata);
                return null;
            }
            MAVLinkMessage reply = null;
            long dueticks = 0;
            using (ManualResetEvent ev = new ManualResetEvent(false))
            {
                byte[] pktdata = PreparePacket(msgid, indata);
                var ehs = new EventHandler<MAVLinkMessage>[rids.Length];
                for (var i = rids.Length; i > 0;)
                {
                    int ival = --i;
                    ehs[ival] = (object _, MAVLinkMessage p) =>
                    {
                        bool more = false;
                        if (filters[ival] == null || filters[ival](p, ref more))
                        {
                            if (more)
                            {
                                Volatile.Write(ref dueticks,
                                        DateTime.Now.AddMilliseconds(timeoutms).Ticks);
                            }
                            else
                            {
                                reply = p;
                                ev.Set();
                            }
                        }
                    };
                    RegisterMavMessageHandler(rids[ival], ehs[ival],
                        gcs != null && gcs.Length > ival && gcs[ival]);
                }
                bool notdone;
                do
                {
                    Volatile.Write(ref dueticks,
                        DateTime.Now.AddMilliseconds(timeoutms).Ticks);
                    lock (writeLock) BaseStream.Write(pktdata, 0, pktdata.Length);
                    while ((notdone = !ev.WaitOne(new TimeSpan(dueticks - DateTime.Now.Ticks)))
                        && DateTime.Now.Ticks < Volatile.Read(ref dueticks)) ;
                    if (notdone)
                        log.Debug("SendPacketWaitReplies timeouted: " +
                            DateTime.Now.ToString("HH:mm:ss.fff") + ", due " +
                            new DateTime(Volatile.Read(ref dueticks)).ToString("HH:mm:ss.fff"));
                } while (notdone && --retries > 0);
                for (var i = rids.Length; --i >= 0;)
                    UnregisterMavMessageHandler(rids[i], ehs[i],
                        gcs != null && gcs.Length > i && gcs[i]);
            }
            return reply;
        }
        #endregion Core

        #region Background Loop
        private static readonly List<MavCore<StatusType>> mavs = new List<MavCore<StatusType>>();
        static MavCore() { BackgroundLoop.Start(MavCoreBackgroundLoop); }

        protected virtual void DoBackgroundWork() { }

        private static void MavCoreBackgroundLoop(CancellationToken token)
        {
            DateTime lastHeartBeatSent = DateTime.Now;
            DateTime nextUpdateTime = DateTime.Now.AddSeconds(1);
            DateTime lastUpdatedTime = DateTime.MinValue;

            while (!token.IsCancellationRequested)
            {
                try
                {
                    var loopStart = DateTime.Now;

                    lock (mavs) foreach (var mav in mavs)
                        {
                            if (!mav.BaseStream.IsOpen) continue;
                            try
                            {
                                while (mav.BaseStream.BytesAvailable > 5)
                                {
                                    MAVLinkMessage packet = mav.ReadPacket();
                                    if (packet == MAVLinkMessage.Invalid || packet.Length < 5) break;
                                    mav.HandleMavLinkMessage(packet);
                                }
                            }
                            catch { }
                        }

                    var now = DateTime.Now;

                    if (lastHeartBeatSent.Second != now.Second)
                    {
                        lastHeartBeatSent = now;
                        lock (mavs) foreach (var mav in mavs)
                            {
                                if (!mav.BaseStream.IsOpen) continue;
                                try
                                {
                                    mav.SendPacket(MAVLINK_MSG_ID.HEARTBEAT, new mavlink_heartbeat_t
                                    {
                                        type = (byte)MAV_TYPE.GCS,
                                        autopilot = (byte)MAV_AUTOPILOT.INVALID,
                                        mavlink_version = 3 // MAVLINK_VERSION
                                    });
                                }
                                catch { }
                            }
                    }

                    if (now > nextUpdateTime)
                    {
                        nextUpdateTime = now.AddSeconds(1);
                        lock (mavs) foreach (var mav in mavs) mav.DoBackgroundWork();
                    }

                    now = DateTime.Now;
                    int msElapsed = (now - loopStart).Milliseconds;
                    if (msElapsed < 30)
                        Thread.Sleep(30 - msElapsed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MavCoreBackgroundLoop got exception: " + ex);
                }
            }
            Console.WriteLine("MavCoreBackgroundLoop done");
        }
        #endregion Background Loop

        #region Retrieve packets from flight control.
        public class MessageHolder { public object Message = null; };

        public const int MAX_MAVLINK_MSGID = 256;
        private readonly EventHandler<MAVLinkMessage>[] msgMap = new EventHandler<MAVLinkMessage>[MAX_MAVLINK_MSGID];
        private readonly EventHandler<MAVLinkMessage>[] gcsMsgMap = new EventHandler<MAVLinkMessage>[MAX_MAVLINK_MSGID];
        protected void RegisterMavMessageHandler(MAVLINK_MSG_ID msgid, EventHandler<MAVLinkMessage> handler, bool gcs = false)
            => (gcs ? gcsMsgMap : msgMap)[(uint)msgid] += handler;
        protected void UnregisterMavMessageHandler(MAVLINK_MSG_ID msgid, EventHandler<MAVLinkMessage> handler, bool gcs = false)
            => (gcs ? gcsMsgMap : msgMap)[(uint)msgid] -= handler;
        protected static T GetMessage<T>(MAVLinkMessage packet, ref object h)
        {
            var mh = h as MessageHolder;
            return (T)(mh.Message ?? (mh.Message = packet.ToStructure<T>()));
        }

        protected void HandleMavLinkMessage(MAVLinkMessage message)
        {
            if (message?.msgid < MAX_MAVLINK_MSGID)
                (message.IsGCSPacket() ? gcsMsgMap : msgMap)[message.msgid]?.
                    Invoke(new MessageHolder(), message);
        }

        protected void InitializeMavLinkMessageHandler()
        {
            RegisterMavMessageHandler(MAVLINK_MSG_ID.HEARTBEAT, HeartBeatPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.TIMESYNC, TimeSyncPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.PARAM_VALUE, ParamValuePacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GLOBAL_POSITION_INT, GPSPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.GPS_RAW_INT, GPSRawPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.SYS_STATUS, SysStatusPacketHandler);
            RegisterMavMessageHandler(MAVLINK_MSG_ID.STATUSTEXT, StatusTextPacketHandler);
        }

        private void HeartBeatPacketHandler(object holder, MAVLinkMessage packet) =>
            GetMessage<mavlink_heartbeat_t>(packet, ref holder);

        private void TimeSyncPacketHandler(object holder, MAVLinkMessage packet)
        {
            var tsync = GetMessage<mavlink_timesync_t>(packet, ref holder);
            Int64 now_ns =
                (Int64)((DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds *
                         1000000);
            // tc1 - client
            // ts1 - server

            // system does not know the time
            if (tsync.tc1 == 0)
            {
                tsync.tc1 = now_ns;
                SendPacket(MAVLINK_MSG_ID.TIMESYNC, tsync);
            } // system knows the time 
            else if (tsync.tc1 > 0)
            {
                Int64 offset_ns = (tsync.ts1 + now_ns - tsync.tc1 * 2) / 2;
                Int64 dt = Status.TimeOffset_ns - offset_ns;

                if (Math.Abs(dt) > 10000000) // 10 millisecond skew
                {
                    Status.TimeOffset_ns = offset_ns; // hard-set it.
                }
                else
                {
                    var offset_avg_alpha = 0.6;
                    var avg = (offset_avg_alpha * offset_ns) +
                              (1.0 - offset_avg_alpha) * Status.TimeOffset_ns;
                    Status.TimeOffset_ns = (long)avg;
                }
            }
        }

        private void ParamValuePacketHandler(object holder, MAVLinkMessage packet)
        {
            var value = GetMessage<mavlink_param_value_t>(packet, ref holder);
            string st = ASCIIEncoding.ASCII.GetString(value.param_id).Split('\0')[0];

            Status.ParamTypes[st] = (MAV_PARAM_TYPE)value.param_type;
            if (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA && packet.compid != (byte)MAV_COMPONENT.MAV_COMP_ID_UDP_BRIDGE)
            {
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
                    MAV_PARAM_TYPE.REAL32, (MAV_PARAM_TYPE)value.param_type);
            }
            else
            {
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                Status.Params[st] = new MAVLinkParam(st, BitConverter.GetBytes(value.param_value),
                    (MAV_PARAM_TYPE)value.param_type, (MAV_PARAM_TYPE)value.param_type);
            }
            Status.ParamTypes[st] = (MAV_PARAM_TYPE)value.param_type;
            Status.Params.TotalReported = value.param_count;
            //Console.WriteLine($"ParamValuePacketHandler: idx={value.param_index}, name={st}");
        }

        private void GPSPacketHandler(object holder, MAVLinkMessage packet)
        {
            var loc = GetMessage<mavlink_global_position_int_t>(packet, ref holder);

            // some flight control reserves 0 alt and 0 long
            if (loc.lat == 0 && loc.lon == 0)
            {
                GPSLocationMode = false;
            }
            else
            {
                GPSLocationMode = true;
                Status.Latitude = loc.lat * 1.0e-7;
                Status.Longitude = loc.lon * 1.0e-7;
                Status.AbsoluteAltitude = loc.alt * 1.0e-3;
            }
            Status.Altitude = loc.relative_alt * 1.0e-3f;
        }

        private void GPSRawPacketHandler(object holder, MAVLinkMessage packet)
        {
            var gps = GetMessage<mavlink_gps_raw_int_t>(packet, ref holder);

            if (!GPSLocationMode)
            {
                Status.Latitude = gps.lat * 1.0e-7;
                Status.Longitude = gps.lon * 1.0e-7;
                Status.AbsoluteAltitude = gps.alt * 1.0e-3;
            }

            Status.SatteliteCount = gps.satellites_visible;
        }

        private void SysStatusPacketHandler(object holder, MAVLinkMessage packet)
        {
            var sysstatus = GetMessage<mavlink_sys_status_t>(packet, ref holder);

            float load = (float)sysstatus.load / 10.0f;

            float battery_voltage = (float)sysstatus.voltage_battery / 1000.0f;

            // byte battery_remaining = sysstatus.battery_remaining;
            float current = (float)sysstatus.current_battery / 100.0f;

            ushort packetdropremote = sysstatus.drop_rate_comm;

            Status.BatteryVoltage = sysstatus.voltage_battery == ushort.MaxValue ? 0 : battery_voltage;
            Status.SensorEnabled = sysstatus.onboard_control_sensors_enabled;
            Status.SensorHealth = sysstatus.onboard_control_sensors_health;
            Status.SensorPresent = sysstatus.onboard_control_sensors_present;
        }

        private void StatusTextPacketHandler(object holder, MAVLinkMessage packet)
        {
            var m = GetMessage<mavlink_statustext_t>(packet, ref holder);
            string s = ASCIIEncoding.ASCII.GetString(m.text).Split('\0')[0];
            FloatMessage.NewMessage(Name, m.severity, s);
        }
        #endregion Retrieve sensor data from flight control

        #region Parameters
        private mavlink_param_value_t VerifyParam(MAVLINK_MSG_ID msgid, object outp, string name, int timeout, int retries = 3)
        {
            mavlink_param_value_t pv = new mavlink_param_value_t();
            if (SendPacketWaitReply(MAVLINK_MSG_ID.PARAM_SET, outp,
                MAVLINK_MSG_ID.PARAM_VALUE, (MAVLinkMessage p, ref bool _) =>
                {
                    pv = p.ToStructure<mavlink_param_value_t>();
                    string st = ASCIIEncoding.ASCII.GetString(pv.param_id).Split('\0')[0];
                    if (st != name)
                        log.Error($"Wrong Param Replied {pv.param_index} - " +
                            $"{ASCIIEncoding.ASCII.GetString(pv.param_id)} - {pv.param_value}" +
                            $"Expected: '{name}' Received: '{st}'");
                    return st == name;
                }, timeout, retries) == null)
                throw new TimeoutException("Timeout on waiting PARAM_VALUE for " + name);
            return pv;
        }

        public float GetParam(string name = "")
        {
            if (name == "") return 0;

            log.Info("GetParam name: '" + name + "' " + SysId + ":" + CompId);

            mavlink_param_request_read_t req = new mavlink_param_request_read_t
            {
                target_component = CompId,
                target_system = SysId,
                param_id = ASCIIEncoding.ASCII.GetBytes(name),
                param_index = -1
            };
            Array.Resize(ref req.param_id, 16);

            mavlink_param_value_t pv = VerifyParam(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req, name, 1000);
            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
            Status.Params[name] = new MAVLinkParam(name, BitConverter.GetBytes(pv.param_value),
                Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA ?
                   MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                (MAV_PARAM_TYPE)pv.param_type);
            Status.ParamTypes[name] = (MAV_PARAM_TYPE)pv.param_type;
            log.Info($"{DateTime.Now.Millisecond} got param {pv.param_index} of {pv.param_count} name: {name}");
            return pv.param_value;
        }

        public bool SetParam(string name, double value)
        {
            if (!Status.Params.ContainsKey(name))
            {
                log.Warn("Trying to set Param that doesnt exist " + name + "=" + value);
                return false;
            }

            if (Status.Params[name].Value == value)
            {
                log.Warn("setParam " + name + " not modified as same");
                return true;
            }

            var req = new mavlink_param_set_t
            {
                target_system = SysId,
                target_component = CompId,
                // param type is set here, however it is always sent over the air as a float 100int = 100f.
                param_type = (byte)Status.ParamTypes[name],
                param_value = new MAVLinkParam(name, value,
                    (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                        MAV_PARAM_TYPE.REAL32 : Status.ParamTypes[name]).float_value,
                param_id = ASCIIEncoding.ASCII.GetBytes(name)
            };
            Array.Resize(ref req.param_id, 16);

            log.Info($"setParam '{name}'='{value}' sysid {SysId} compid {CompId}");
            mavlink_param_value_t pv = VerifyParam(MAVLINK_MSG_ID.PARAM_SET, req, name, 700);
            var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
            Status.Params[name] = new MAVLinkParam(name, BitConverter.GetBytes(pv.param_value),
                (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                    MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                (MAV_PARAM_TYPE)pv.param_type);
            log.Info($"SetParam got ack {name} : {Status.Params[name]}");
            return true;
        }

        public bool SetAnyParam(string[] names, double value) => names.Any(n => SetParam(n, value));

        private Dictionary<string, double> GetParamListBG()
        {
            int paramTotal = 0;
            List<int> indices = new List<int>();
            MAVLinkParamList paramList = new MAVLinkParamList();
            int targetIndex = -1;
            bool ParamValueHandler(MAVLinkMessage m, ref bool more)
            {
                if (frmProgressReporter.doWorkArgs.CancelRequested) return false;

                mavlink_param_value_t pv = m.ToStructure<mavlink_param_value_t>();
                more = true;
                if (indices.Contains(pv.param_index)) return true;

                paramList.TotalReported = paramTotal = pv.param_count;
                string pid = ASCIIEncoding.ASCII.GetString(pv.param_id).Split('\0')[0];
                var offset = Marshal.OffsetOf(typeof(mavlink_param_value_t), "param_value");
                paramList[pid] = new MAVLinkParam(pid, BitConverter.GetBytes(pv.param_value),
                    (Status.APName == MAV_AUTOPILOT.ARDUPILOTMEGA) ?
                        MAV_PARAM_TYPE.REAL32 : (MAV_PARAM_TYPE)pv.param_type,
                    (MAV_PARAM_TYPE)pv.param_type);
                Status.ParamTypes[pid] = (MAV_PARAM_TYPE)pv.param_type;
                if (pv.param_index != 65535)
                    indices.Add(pv.param_index);

                frmProgressReporter.UpdateProgressAndStatus(
                    (indices.Count * 100) / paramTotal, Strings.GotParam + pid);
                if (pv.param_index == (paramTotal - 1))
                    more = false;
                return true;
            }

            int retries = 6;
            var lreq = new mavlink_param_request_list_t { target_system = SysId, target_component = CompId };
            do
            {
                SendPacketWaitReplies(MAVLINK_MSG_ID.PARAM_REQUEST_LIST, lreq,
                    new MAVLINK_MSG_ID[] { MAVLINK_MSG_ID.PARAM_VALUE, MAVLINK_MSG_ID.STATUSTEXT },
                    new ReplyPacketFilter[]
                    {
                        ParamValueHandler,
                        (MAVLinkMessage m, ref bool more) =>
                        {
                            mavlink_statustext_t pv = m.ToStructure<mavlink_statustext_t>();
                            string st = ASCIIEncoding.ASCII.GetString(pv.text).Split('\0')[0];
                            switch (st)
                            {
                                case string c when c.Contains("copter"):
                                case string r when r.Contains("rover"):
                                case string p when p.Contains("plane"):
                                    Status.VersionString = st;
                                    break;
                                case string n when n.Contains("nuttx"):
                                    Status.SoftwareVersions = st;
                                    break;
                                case string x when x.Contains("px4v2"):
                                    Status.SerialString = st;
                                    break;
                                case string f when f.Contains("frame"):
                                    Status.FrameString = st;
                                    break;
                            }
                            return more = true;
                        }
                    });
                if (paramTotal > 0 && indices.Count == paramTotal)
                {
                    Status.Params.Clear();
                    Status.Params.TotalReported = paramTotal;
                    Status.Params.AddRange(paramList);
                    return Status.Params;
                }
                log.Debug("GetParamListBG: Params receive timeouted, " +
                    (retries > 0 ? "retry" : "try 1-by-1 mode"));
                if (frmProgressReporter.doWorkArgs.CancelRequested)
                {
                    frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
                    frmProgressReporter.doWorkArgs.ErrorMessage = Strings.MsgFormProgressUserCanceled;
                    return Status.Params;
                }
            } while (retries-- > 0 && (indices.Count < paramTotal * 0.95 || paramTotal == 0));

            if (indices.Count != paramTotal || paramTotal == 0)
            {
                if (paramTotal > 0)
                {
                    log.Debug("Not all params read, start one by one mode.");
                    mavlink_param_request_read_t req = new mavlink_param_request_read_t
                    {
                        target_system = SysId,
                        target_component = CompId,
                        param_id = new byte[16]
                    };
                    req.param_id[0] = 0;
                    for (short i = 0; i < paramTotal; i++)
                    {
                        if (frmProgressReporter.doWorkArgs.CancelRequested) break;
                        if (indices.Contains(i)) continue;
                        targetIndex = req.param_index = i;
                        SendPacketWaitReply(MAVLINK_MSG_ID.PARAM_REQUEST_READ, req,
                            MAVLINK_MSG_ID.PARAM_VALUE, ParamValueHandler,
                            GET_PARAM_TIMEOUT);
                        if (!indices.Contains(i)) break;
                    }
                }

                if (indices.Count != paramTotal || paramTotal == 0)
                {
                    if (frmProgressReporter.doWorkArgs.CancelRequested)
                    {
                        frmProgressReporter.doWorkArgs.CancelAcknowledged = true;
                        frmProgressReporter.doWorkArgs.ErrorMessage = Strings.MsgFormProgressUserCanceled;
                        return Status.Params;
                    }
                    else
                        throw new Exception($"Missing Params: got {indices.Count}/{paramTotal}");
                }
            }

            Status.Params.Clear();
            Status.Params.TotalReported = paramTotal;
            Status.Params.AddRange(paramList);
            return Status.Params;
        }
        #endregion Parameters

        private void SetupMavConnect(MAVLinkMessage message, mavlink_heartbeat_t hb)
        {
            /*if (SysId != message.sysid || CompId != message.compid)
            {
                log.Info($"MavCore ({SysId},{CompId}) received foreign packet ({message.sysid},{message.compid}), dropped");
            }*/
            mavlinkVersion = hb.mavlink_version;
            MAV_TYPE type = (MAV_TYPE)hb.type;
            MAV_AUTOPILOT name = (MAV_AUTOPILOT)hb.autopilot;
            if (Status.APType != type || Status.APName != name)
                SetAPType(type, name);
            Status.recvPacketCount = message.seq;
        }

        public MAVLinkMessage GetHeartBeat(bool setup = false)
        {
            MAVLinkMessage buffer = WaitPacket(MAVLINK_MSG_ID.HEARTBEAT, null, 2200);
            if (buffer != null)
            {
                mavlink_heartbeat_t hb = buffer.ToStructure<mavlink_heartbeat_t>();
                if ((MAV_TYPE)hb.type == MAV_TYPE.GCS)
                    buffer = null;
                else if (setup)
                    SetupMavConnect(buffer, hb);
            }

            return buffer ?? MAVLinkMessage.Invalid;
        }

        public void SetAPType(MAV_TYPE type, MAV_AUTOPILOT name)
        {
            Status.APType = type;
            Status.APName = name;
            switch (name)
            {
                case MAV_AUTOPILOT.ARDUPILOTMEGA:
                    switch (type)
                    {
                        case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.ArduPlane;
                            break;
                        case MAV_TYPE.QUADROTOR:
                        case MAV_TYPE.TRICOPTER:
                        case MAV_TYPE.HEXAROTOR:
                        case MAV_TYPE.OCTOROTOR:
                        case MAV_TYPE.HELICOPTER:
                            Status.Firmware = Firmwares.ArduCopter2;
                            break;
                        case MAV_TYPE.GROUND_ROVER:
                            Status.Firmware = Firmwares.ArduRover;
                            break;
                        case MAV_TYPE.SUBMARINE:
                            Status.Firmware = Firmwares.ArduSub;
                            break;
                        case MAV_TYPE.ANTENNA_TRACKER:
                            Status.Firmware = Firmwares.ArduTracker;
                            break;
                        default:
                            log.Error(Status.APType + " not registered as valid type");
                            break;
                    }
                    break;
                case MAV_AUTOPILOT.UDB:
                    switch (type)
                    {
                        case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.ArduPlane;
                            break;
                    }
                    break;
                case MAV_AUTOPILOT.GENERIC:
                    switch (type)
                    {
                        case MAV_TYPE.FIXED_WING:
                            Status.Firmware = Firmwares.Ateryx;
                            break;
                    }
                    break;
                case MAV_AUTOPILOT.PX4:
                    Status.Firmware = Firmwares.PX4;
                    break;
                default:
                    switch (type)
                    {
                        case MAV_TYPE.GIMBAL: // storm32 - name 83
                            Status.Firmware = Firmwares.Gymbal;
                            break;
                    }
                    break;
            }
            if (Status is DroneStatus status)
                status.FlightModeType = FlightMode.GetFlightMode(Status.Firmware);
        }

        public bool GetVersion()
        {
            return SendPacketWaitReply(MAVLINK_MSG_ID.AUTOPILOT_VERSION_REQUEST,
                    new mavlink_autopilot_version_request_t
                    {
                        target_component = CompId,
                        target_system = SysId
                    },
                    MAVLINK_MSG_ID.AUTOPILOT_VERSION, null, 1000, 3) != null;
        }

        public void SendCommand(MAV_CMD cmd, float p1, float p2, float p3, float p4, float p5, float p6, float p7)
        {
            log.Info($"SendCommand cmd {cmd.ToString()} {p1} {p2} {p3} {p4} {p5} {p6} {p7}");
            SendPacket(MAVLINK_MSG_ID.COMMAND_LONG, new mavlink_command_long_t
            {
                target_system = SysId,
                target_component = CompId,
                command = (ushort)cmd,
                param1 = p1,
                param2 = p2,
                param3 = p3,
                param4 = p4,
                param5 = p5,
                param6 = p6,
                param7 = p7
            });
        }

        public bool SendCommandWaitAck(MAV_CMD cmd, float p1, float p2, float p3, float p4, float p5, float p6, float p7, int timeoutms = 2000)
        {
            log.Info($"SendCommandWaitAck cmd {cmd.ToString()} {p1} {p2} {p3} {p4} {p5} {p6} {p7}");
            MAVLinkMessage ack = SendPacketWaitReply(MAVLINK_MSG_ID.COMMAND_LONG,
                    new mavlink_command_long_t
                    {
                        target_system = SysId,
                        target_component = CompId,
                        command = (ushort)cmd,
                        param1 = p1,
                        param2 = p2,
                        param3 = p3,
                        param4 = p4,
                        param5 = p5,
                        param6 = p6,
                        param7 = p7
                    }, MAVLINK_MSG_ID.COMMAND_ACK,
                    (MAVLinkMessage p, ref bool _) =>
                        (MAV_CMD)p.ToStructure<mavlink_command_ack_t>().command == cmd,
                    1000, 3);
            if (ack == null)
                throw new TimeoutException($"Timeout on waiting ack from command {cmd}");
            return (MAV_RESULT)ack.ToStructure<mavlink_command_ack_t>().result == MAV_RESULT.ACCEPTED;
        }

        public void SetDataStreamFrequency(MAV_DATA_STREAM id, byte hzrate)
        {
            mavlink_request_data_stream_t req = new mavlink_request_data_stream_t
            {
                target_component = CompId,
                target_system = SysId,
                req_message_rate = hzrate,
                start_stop = 1, //start
                req_stream_id = (byte)id
            };

            // send each one twice.
            SendPacket(MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req);
            SendPacket(MAVLINK_MSG_ID.REQUEST_DATA_STREAM, req);
        }

        public bool SetMessageInterval(MAVLINK_MSG_ID id, int intervalUs)
        {
            return SendCommandWaitAck(MAV_CMD.SET_MESSAGE_INTERVAL,
                (ushort)id, intervalUs, 0, 0, 0, 0, 0);
        }
    }
}