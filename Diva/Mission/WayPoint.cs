using Diva.Mavlink;
using Diva.Utilities;
using PointLatLng = GMap.NET.PointLatLng;
using MAV_FRAME = MAVLink.MAV_FRAME;

namespace Diva.Mission
{
	public struct WayPoint
	{
		public ushort SeqNo;
		public object Tag;

		public ushort Id;               // command id
		public byte Option;
		public float Param1;            // param 1
		public float Param2;            // param 2
		public float Param3;            // param 3
		public float Param4;            // param 4
		public double Latitude;         // Lattitude * 10**7
		public double Longitude;        // Longitude * 10**7
		public float Altitude;          // Altitude in centimeters (meters * 100)
		public MAV_FRAME Frame;

        [System.Obsolete]
        public MAVLink.mavlink_mission_item_t ToMissionItem<T>(MavCore<T> mav, byte current) where T : MavStatus
            => new MAVLink.mavlink_mission_item_t
            {
                target_system = mav?.SysId ?? 0,
                target_component = mav?.CompId ?? 0,
                autocontinue = 1,
                current = current,
                command = Id,
                param1 = Param1,
                param2 = Param2,
                param3 = Param3,
                param4 = Param4,
                x = (float)Latitude,
                y = (float)Longitude,
                z = Altitude,
                seq = SeqNo,
                frame = (byte)Frame.ToFloatFrame()
            };

        public MAVLink.mavlink_mission_item_int_t ToMissionItemInt<T>(MavCore<T> mav, byte current) where T : MavStatus
        {
            bool camControl = Id == (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONTROL ||
                Id == (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONFIGURE;
            return new MAVLink.mavlink_mission_item_int_t
            {
                target_system = mav?.SysId ?? 0,
                target_component = mav?.CompId ?? 0,
                autocontinue = 1,
                current = current,
                command = Id,
                param1 = Param1,
                param2 = Param2,
                param3 = Param3,
                param4 = Param4,
                x = (int)System.Math.Round(camControl ? Latitude : Latitude * 1.0e7),
                y = (int)System.Math.Round(camControl ? Longitude : Longitude * 1.0e7),
                z = Altitude,
                seq = SeqNo,
                frame = (byte)Frame.ToIntFrame()
            };
        }

        [System.Obsolete]
        public static implicit operator WayPoint(MAVLink.mavlink_mission_item_t input)
            => new WayPoint
			{
				Id = input.command,
				Param1 = input.param1,
				Param2 = input.param2,
				Param3 = input.param3,
				Param4 = input.param4,
				Latitude = input.x,
				Longitude = input.y,
				Altitude = input.z,
				SeqNo = input.seq,
				Frame = (MAV_FRAME)input.frame
			};

		public static implicit operator WayPoint(MAVLink.mavlink_mission_item_int_t input)
        {
            bool camControl = false;
            switch (input.command)
            {
                case (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONTROL:
                case (ushort)MAVLink.MAV_CMD.DO_DIGICAM_CONFIGURE:
                    camControl = true;
                    break;
            }
            return new WayPoint
            {
                Id = input.command,
                Param1 = input.param1,
                Param2 = input.param2,
                Param3 = input.param3,
                Param4 = input.param4,
                Latitude = camControl ? input.x : input.x / 1.0e7,
                Longitude = camControl ? input.y : input.y / 1.0e7,
                Altitude = input.z,
                SeqNo = input.seq,
                Frame = (MAV_FRAME)input.frame
            };
        }

        public static implicit operator WayPoint(MissionFile.MissionItem input)
            => new WayPoint()
			{
				Id = input.command,
				Param1 = input.param1,
				Param2 = input.param2,
				Param3 = input.param3,
				Param4 = input.param4,
				Latitude = input.coordinate[0],
				Longitude = input.coordinate[1],
				Altitude = (float)input.coordinate[2],
				SeqNo = input.id,
				Frame = (MAV_FRAME)input.frame
			};

		public static implicit operator MissionFile.MissionItem(WayPoint input)
		    => new MissionFile.MissionItem
			{
				command = input.Id,
				param1 = input.Param1,
				param2 = input.Param2,
				param3 = input.Param3,
				param4 = input.Param4,
				coordinate = new double[] { input.Latitude, input.Longitude, input.Altitude },
				id = input.SeqNo,
				frame = (byte)input.Frame
			};

        public PointLatLng ToPointLatLng() => new PointLatLng(Latitude, Longitude);

        public PointLatLngAlt ToPointLatLngAlt()
            => new PointLatLngAlt(Latitude, Longitude, Altitude, Tag?.ToString());
    }
}
