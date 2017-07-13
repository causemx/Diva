using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooApplication.Utilities
{
	public static class Utility
	{
		public static double ConvertToDouble(object input)
		{
			if (input.GetType() == typeof(float))
			{
				return (float)input;
			}
			if (input.GetType() == typeof(double))
			{
				return (double)input;
			}
			if (input.GetType() == typeof(ulong))
			{
				return (ulong)input;
			}
			if (input.GetType() == typeof(long))
			{
				return (long)input;
			}
			if (input.GetType() == typeof(int))
			{
				return (int)input;
			}
			if (input.GetType() == typeof(uint))
			{
				return (uint)input;
			}
			if (input.GetType() == typeof(short))
			{
				return (short)input;
			}
			if (input.GetType() == typeof(ushort))
			{
				return (ushort)input;
			}
			if (input.GetType() == typeof(bool))
			{
				return (bool)input ? 1 : 0;
			}
			if (input.GetType() == typeof(string))
			{
				double ans = 0;
				if (double.TryParse((string)input, out ans))
				{
					return ans;
				}
			}
			if (input is Enum)
			{
				return Convert.ToInt32(input);
			}

			if (input == null)
				throw new Exception("Bad Type Null");
			else
				throw new Exception("Bad Type " + input.GetType().ToString());
		}
	}

	public static class MathHelper
	{
		public const double rad2deg = (180 / Math.PI);
		public const double deg2rad = (1.0 / rad2deg);

		public static double Degrees(double rad)
		{
			return rad * rad2deg;
		}

		public static double Radians(double deg)
		{
			return deg * deg2rad;
		}

		public static double map(double x, double in_min, double in_max, double out_min, double out_max)
		{
			return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
		}
	}
}
