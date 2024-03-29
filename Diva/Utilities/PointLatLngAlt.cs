﻿using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using GMap.NET;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
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
            => MathHelper.BearingOf(Lat, Lng, p2.Lat, p2.Lng);

		public double GetDistance(PointLatLngAlt p2)
            => MathHelper.DistanceBetween(Lat, Lng, p2.Lat, p2.Lng);

		public double[] ToUTM() => ToUTM(GetUTMZone());

		public double[] ToUTM(int utmzone)
		{
			ICoordinateTransformation trans = TryGetTransform(utmzone, Lat);

			double[] pll = { Lng, Lat };

			// get leader utm coords
			double[] utmxy = trans.MathTransform.Transform(pll);

			return utmxy;
		}

		public static List<double[]> ToUTM(int utmzone, List<PointLatLngAlt> list)
		{
			ICoordinateTransformation trans = TryGetTransform(utmzone, list[0].Lat);

			List<double[]> data = new List<double[]>();

			list.ForEach(x => { data.Add((double[])x); });

			return trans.MathTransform.TransformList(data).ToList();
		}

		public int GetUTMZone()
		{
			int zone = (int)((Lng - -186.0) / 6.0);
			if (Lat < 0)
				zone *= -1;

			return zone;
		}

		static CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
		static IGeographicCoordinateSystem wgs84 = GeographicCoordinateSystem.WGS84;
		private static Dictionary<int, ICoordinateTransformation> coordtrans = new Dictionary<int, ICoordinateTransformation>();

		static ICoordinateTransformation TryGetTransform(int utmzone, double lat)
		{
			if (lat < 0 && utmzone > 0)
				utmzone *= -1;

			lock (coordtrans)
				if (coordtrans.ContainsKey(utmzone))
					return coordtrans[utmzone];

			IProjectedCoordinateSystem utm = ProjectedCoordinateSystem.WGS84_UTM(Math.Abs(utmzone), lat < 0 ? false : true);
			ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(wgs84, utm);

			lock (coordtrans)
				coordtrans[utmzone] = trans;

			lock (coordtrans)
				return coordtrans[utmzone];
		}
	}
}
