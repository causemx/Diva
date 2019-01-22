namespace Diva.Utilities
{
	public struct WayPoint
	{
		private ushort SeqNo;
		private byte Frame;
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

		/*public static implicit operator MAVLink.mavlink_mission_item_t(WayPoint input)
            => new MAVLink.mavlink_mission_item_t
            {
                command = input.Id,
                param1 = input.Param1,
                param2 = input.Param2,
                param3 = input.Param3,
                param4 = input.Param4,
                x = (float)input.Latitude,
                y = (float)input.Longitude,
                z = (float)input.Altitude,
                seq = input.SeqNo,
                frame = input.Frame
            };

		public static implicit operator MAVLink.mavlink_mission_item_int_t(WayPoint input)
			=> new MAVLink.mavlink_mission_item_int_t
            {
                command = input.Id,
                param1 = input.Param1,
                param2 = input.Param2,
                param3 = input.Param3,
                param4 = input.Param4,
                x = (int)(input.Latitude * 1.0e7),
                y = (int)(input.Longitude * 1.0e7),
                z = input.Altitude,
                seq = input.SeqNo,
                frame = input.Frame
            };*/

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
				Frame = input.frame
			};

		public static implicit operator WayPoint(MAVLink.mavlink_mission_item_int_t input)
		    => new WayPoint
			{
				Id = input.command,
				Param1 = input.param1,
				Param2 = input.param2,
				Param3 = input.param3,
				Param4 = input.param4,
				Latitude = input.x / 1.0e7,
				Longitude = input.y / 1.0e7,
				Altitude = input.z,
				SeqNo = input.seq,
				Frame = input.frame
			};

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
				Frame = input.frame
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
				frame = input.Frame
			};
	}
}
