using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.Utilities
{
	public class PointLatLngAlt
	{
		public static readonly PointLatLngAlt Zero = new PointLatLngAlt();
		public double Lat = 0;
		public double Lng = 0;
		public double Alt = 0;
		public string Tag = "";
		public string Tag2 = "";
		public Color Color = Color.White;

        public PointLatLngAlt() { }

		public PointLatLngAlt(double lat, double lng)
		{
			Lat = lat;
			Lng = lng;
		}

		public PointLatLngAlt(double lat, double lng, double alt)
		{
			Lat = lat;
			Lng = lng;
			Alt = alt;
		}

		public PointLatLngAlt(double lat, double lng, double alt, string tag)
		{
			Lat = lat;
			Lng = lng;
			Alt = alt;
			Tag = tag;
		}

        public PointLatLngAlt(double lat, double lng, double alt, string tag, Color color)
        {
            Lat = lat;
            Lng = lng;
            Alt = alt;
            Tag = tag;
            Color = color;
        }

        public PointLatLngAlt(PointLatLng pll)
		{
			Lat = pll.Lat;
			Lng = pll.Lng;
		}

		public PointLatLngAlt(PointLatLngAlt plla)
		{
			Lat = plla.Lat;
			Lng = plla.Lng;
			Alt = plla.Alt;
			Color = plla.Color;
			Tag = plla.Tag;
            Tag2 = plla.Tag2;
		}

		public static implicit operator PointLatLngAlt(PointLatLng a)
            => new PointLatLngAlt(a);

		public static implicit operator PointLatLng(PointLatLngAlt a)
            => new PointLatLng(a.Lat, a.Lng);

		public static implicit operator double[] (PointLatLngAlt a)
		    => new double[] { a.Lng, a.Lat, a.Alt };

		public static implicit operator PointLatLngAlt(double[] a)
            => new PointLatLngAlt(a[0], a[1], a.Length > 2 ? a[2] : 0);

        public PointLatLngAlt OffsetAngleDistance(double bearing, double distance)
		{
			// '''extrapolate latitude/longitude given a heading and distance 
			//   thanks to http://www.movable-type.co.uk/scripts/latlong.html
			//  '''
			// from math import sin, asin, cos, atan2, radians, degrees
			double radius_of_earth = 6378100.0;//# in meters

			double lat1 = MathHelper.deg2rad * (this.Lat);
			double lon1 = MathHelper.deg2rad * (this.Lng);
			double brng = MathHelper.deg2rad * (bearing);
			double dr = distance / radius_of_earth;

			double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(dr) +
						Math.Cos(lat1) * Math.Sin(dr) * Math.Cos(brng));
			double lon2 = lon1 + Math.Atan2(Math.Sin(brng) * Math.Sin(dr) * Math.Cos(lat1),
								Math.Cos(dr) - Math.Sin(lat1) * Math.Sin(lat2));

			double latout = MathHelper.rad2deg * (lat2);
			double lngout = MathHelper.rad2deg * (lon2);

			return new PointLatLngAlt(latout, lngout, this.Alt, this.Tag);
		}

		public PointLatLngAlt OffsetNorthEast(double north, double east)
		{
			double bearing = Math.Atan2(east, north) * MathHelper.rad2deg;
			double distance = Math.Sqrt(Math.Pow(east, 2) + Math.Pow(north, 2));
			return OffsetAngleDistance(bearing, distance);
		}

		public double GetBearing(PointLatLngAlt p2)
		{
			var latitude1 = MathHelper.deg2rad * (this.Lat);
			var latitude2 = MathHelper.deg2rad * (p2.Lat);
			var longitudeDifference = MathHelper.deg2rad * (p2.Lng - this.Lng);

			var y = Math.Sin(longitudeDifference) * Math.Cos(latitude2);
			var x = Math.Cos(latitude1) * Math.Sin(latitude2) - Math.Sin(latitude1) * Math.Cos(latitude2) * Math.Cos(longitudeDifference);

			return (MathHelper.rad2deg * (Math.Atan2(y, x)) + 360) % 360;
		}

		public double GetDistance(PointLatLngAlt p2)
		{
			double d = Lat * 0.017453292519943295;
			double num2 = Lng * 0.017453292519943295;
			double num3 = p2.Lat * 0.017453292519943295;
			double num4 = p2.Lng * 0.017453292519943295;
			double num5 = num4 - num2;
			double num6 = num3 - d;
			double num7 = Math.Pow(Math.Sin(num6 / 2.0), 2.0) + ((Math.Cos(d) * Math.Cos(num3)) * Math.Pow(Math.Sin(num5 / 2.0), 2.0));
			double num8 = 2.0 * Math.Atan2(Math.Sqrt(num7), Math.Sqrt(1.0 - num7));
			return (6371 * num8) * 1000.0; // M
		}
	}
}
