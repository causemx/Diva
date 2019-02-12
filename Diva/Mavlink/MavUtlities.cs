using System;
using static MAVLink;

namespace Diva.Mavlink
{
    public enum AltitudeMode
    {
        Relative = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT,
        Absolute = MAVLink.MAV_FRAME.GLOBAL,
        Terrain = MAVLink.MAV_FRAME.GLOBAL_TERRAIN_ALT
    }

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

        #region Flight mode name/value conversion
        public static string GetName(this FlightMode mode) => Enum.GetName(typeof(FlightMode), mode);

        public static bool GetFlightModeByName(string name, out FlightMode mode)
        {
            bool found;
            if (!(found = Enum.TryParse(name, out mode)))
                Console.WriteLine("Failed to find flight Mode: " + name);
            return found;
        }
        #endregion flight mode
    }
}
