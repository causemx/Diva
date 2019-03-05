using System;
using static MAVLink;

namespace Diva.Mavlink
{
    public enum FlightMode
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
    }

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
        public static string GetName(this FlightMode mode)
            => Enum.GetName(typeof(FlightMode), mode);
        public static string GetName(this MAV_CMD mode)
            => Enum.GetName(typeof(MAV_CMD), mode);
        public static string GetName(this MAV_MISSION_RESULT mode)
            => Enum.GetName(typeof(MAV_MISSION_RESULT), mode);

        public static bool GetByName<T>(string name, out T mode) where T : struct, IConvertible
        {
            bool found = false;
            Type t = typeof(T);
            if (t.IsEnum)
            {
                if (!(found = Enum.TryParse(name, out mode)))
                    Console.WriteLine($"Failed to find Enum value {name} in type {t.Name}.");
            } else
            {
                Console.WriteLine($"GetByName: {t.Name} is not an Enum.");
                mode = default(T);
            }
            return found;
        }
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
            switch (frame)
            {
                case MAV_FRAME.GLOBAL:
                    return MAV_FRAME.GLOBAL_INT;
                case MAV_FRAME.GLOBAL_RELATIVE_ALT:
                    return MAV_FRAME.GLOBAL_RELATIVE_ALT_INT;
                case MAV_FRAME.GLOBAL_TERRAIN_ALT:
                    return MAV_FRAME.GLOBAL_TERRAIN_ALT_INT;
                default:
                    return frame;
            }
        }
        #endregion

        #region MavLink class support
        public static bool IsGCSPacket(this MAVLinkMessage mav)
            => (mav.sysid == 255 || mav.sysid == 253);
        #endregion
    }
}
