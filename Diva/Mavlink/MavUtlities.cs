using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Mavlink
{
    public static class MavUtlities
    {
        #region Bit flag test
        public static bool IsSetIn(this MAVLink.MAV_MODE_FLAG flag, byte value) => (value & (byte)flag) == (byte)flag;
        public static bool HasFlag(this byte value, MAVLink.MAV_MODE_FLAG flag) => (value & (byte)flag) == (byte)flag;
        #endregion

        #region flight mode
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

        public static bool GetFlightModeByName(string name, out uint mode)
        {
            FlightMode fmode;
            bool found;
            if (found = Enum.TryParse(name, out fmode))
            {
                mode = (uint)fmode;
            } else
            {
                mode = 0;
                Console.WriteLine("Failed to find Mode");
            }
            return found;
        }

        public static string GetFlightModeName(uint mode)
        {
            Type flightType = typeof(FlightMode);
            string name = null;
            try { name = Enum.GetName(flightType, mode); } catch { }
            return name;
        }
        #endregion flight mode

        #region firmware types
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
        #endregion firmware types
    }
}
