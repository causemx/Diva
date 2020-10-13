using System;
using System.Linq;
using System.Collections.Generic;
using static MAVLink;

namespace Diva.Mavlink
{
    public sealed class FlightMode
    {
        private readonly Dictionary<string, uint> modes = new Dictionary<string, uint>();
        public string PauseMode { get; private set; }

        public static readonly FlightMode CopterMode;
        public static readonly FlightMode PlaneMode;

        public static FlightMode GetFlightMode(Firmwares firmwares)
        {
            FlightMode mode = CopterMode;
            switch (firmwares)
            {
                case Firmwares.ArduPlane:
                    mode = PlaneMode;
                    break;
            }
            return mode;
        }

        static FlightMode()
        {
            CopterMode = new FlightMode { PauseMode = "BRAKE" };
            foreach (var m in Enum.GetValues(typeof(COPTER_MODE)))
                CopterMode.modes.Add(m.ToString(), (uint)m);

            PlaneMode = new FlightMode { PauseMode = "LOITER" };
            foreach (var m in Enum.GetValues(typeof(PLANE_MODE)))
                PlaneMode.modes.Add(m.ToString(), (uint)m);
        }

        private FlightMode() { }

        public uint this[string name] => modes[name];
        public string this[uint value] => modes.FirstOrDefault(p => p.Value == value).Key;

        public string[] Values => modes.Keys.ToArray();
    }

    public enum COPTER_MODE : uint
    {
        STABILIZE = 0,
        ACRO = 1,
        ALT_HOLD = 2,
        AUTO = 3,
        GUIDED = 4,
        LOITER = 5,
        RTL = 6,
        CIRCLE = 7,
        POSITION = 8,
        LAND = 9,
        OF_LOITER = 10,
        DRIFT = 11,
        SPORT = 13,
        FLIP = 14,
        AUTOTUNE = 15,
        POSHOLD = 16,
        BRAKE = 17,
        THROW = 18,
        AVOID_ADSB = 19,
        GUIDED_NOGPS = 20,
        SMART_RTL = 21,
        FLOWHOLD = 22,
        FOLLOW = 23,
        ZIGZAG = 24,
        SYSTEMID = 25,
        AUTOROTATE = 26,
    }

    public enum PLANE_MODE : uint
    {
        MANUAL = 0,
        CIRCLE = 1,
        STABILIZE = 2,
        TRAINING = 3,
        ACRO = 4,
        FLY_BY_WIRE_A = 5,
        FLY_BY_WIRE_B = 6,
        CRUISE = 7,
        AUTOTUNE = 8,
        AUTO = 10,
        RTL = 11,
        LOITER = 12,
        TAKEOFF = 13,
        AVOID_ADSB = 14,
        GUIDED = 15,
        INITIALIZING = 16,
        QSTABILIZE = 17,
        QHOVER = 18,
        QLOITER = 19,
        QLAND = 20,
        QRTL = 21,
        QAUTOTUNE = 22,
        QACRO = 23,
    };

    public enum Firmwares
    {
        ArduPlane,
        ArduCopter2,
        ArduRover,
        ArduSub,
        Ateryx,
        ArduTracker,
        Gymbal,
        PX4
    }

    public static class MavUtlities
    {
        #region Bit flag test
        public static bool IsSetIn(this MAVLink.MAV_MODE_FLAG flag, byte value) => (value & (byte)flag) == (byte)flag;
        public static bool HasFlag(this byte value, MAVLink.MAV_MODE_FLAG flag) => (value & (byte)flag) == (byte)flag;
        #endregion

        #region Enums name/value conversion
        public static string GetName(this MAV_CMD mode)
            => Enum.GetName(typeof(MAV_CMD), mode);
        public static string GetName(this MAV_MISSION_RESULT mode)
            => Enum.GetName(typeof(MAV_MISSION_RESULT), mode);

        public static bool IsMode(this MavDrone drone, string name)
            => drone.Status.FlightMode == drone.Status.FlightModeType[name];
        #endregion flight mode

        #region Altitude mode translate
        public static MAV_FRAME ToFloatFrame(this MAV_FRAME frame)
        {
            switch (frame)
            {
                case MAV_FRAME.GLOBAL_INT:
                    return MAV_FRAME.GLOBAL;
                case MAV_FRAME.GLOBAL_RELATIVE_ALT_INT:
                    return MAV_FRAME.GLOBAL_RELATIVE_ALT;
                case MAV_FRAME.GLOBAL_TERRAIN_ALT_INT:
                    return MAV_FRAME.GLOBAL_TERRAIN_ALT;
                default:
                    return frame;
            }
        }
        public static MAV_FRAME ToIntFrame(this MAV_FRAME frame)
        {
            /*switch (frame)
            {
                case MAV_FRAME.GLOBAL:
                    return MAV_FRAME.GLOBAL_INT;
                case MAV_FRAME.GLOBAL_RELATIVE_ALT:
                    return MAV_FRAME.GLOBAL_RELATIVE_ALT_INT;
                case MAV_FRAME.GLOBAL_TERRAIN_ALT:
                    return MAV_FRAME.GLOBAL_TERRAIN_ALT_INT;
                default:
                    return frame;
            }*/
            // somehow ardupilot has no int type frame support (actually for any enum > 4)
            return frame.ToFloatFrame();
        }
        #endregion

        #region MavLink class support
        public static bool IsGCSPacket(this MAVLinkMessage mav)
            => (mav.sysid == 255 || mav.sysid == 253);

        public static bool IsRadioPacket(this MAVLinkMessage mav)
            => (mav.sysid == 51 && mav.compid == 68)    //3dr radio
            || (MAV_COMPONENT)mav.compid == MAV_COMPONENT.MAV_COMP_ID_UDP_BRIDGE;   // wifi chip

        public static bool IsMainComponent(this MAVLinkMessage mav)
            => (MAV_COMPONENT)mav.compid == MAV_COMPONENT.MAV_COMP_ID_AUTOPILOT1;
        #endregion
    }
}
