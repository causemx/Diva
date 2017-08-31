using FooApplication.Properties;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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



	[Serializable]
	public class GMapMarkerRect : GMapMarker
	{
		public Pen Pen = new Pen(Brushes.White, 2);

		public Color Color
		{
			get { return Pen.Color; }
			set
			{
				if (!initcolor.HasValue) initcolor = value;
				Pen.Color = value;
			}
		}

		Color? initcolor = null;

		public GMapMarker InnerMarker;

		public int wprad = 0;

		public void ResetColor()
		{
			if (initcolor.HasValue)
				Color = initcolor.Value;
			else
				Color = Color.White;
		}

		public GMapMarkerRect(PointLatLng p)
			: base(p)
		{
			Pen.DashStyle = DashStyle.Dash;

			// do not forget set Size of the marker
			// if so, you shall have no event on it ;}
			Size = new System.Drawing.Size(50, 50);
			Offset = new System.Drawing.Point(-Size.Width / 2, -Size.Height / 2 - 20);
		}

		public override void OnRender(Graphics g)
		{
			base.OnRender(g);

			if (wprad == 0 || Overlay.Control == null)
				return;

			// if we have drawn it, then keep that color
			if (!initcolor.HasValue)
				Color = Color.White;

			// undo autochange in mouse over
			//if (Pen.Color == Color.Blue)
			//  Pen.Color = Color.White;

			double width =
				(Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
					Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
			double height =
				(Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
					Overlay.Control.FromLocalToLatLng(Overlay.Control.Height, 0)) * 1000.0);
			double m2pixelwidth = Overlay.Control.Width / width;
			double m2pixelheight = Overlay.Control.Height / height;

			GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * wprad * 2)), LocalPosition.Y);
			// MainMap.FromLatLngToLocal(wpradposition);

			if (m2pixelheight > 0.5 && !double.IsInfinity(m2pixelheight))
				g.DrawArc(Pen,
					new System.Drawing.Rectangle(
						LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2,
						(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);
		}
	}


	[Serializable]
	public class GMapMarkerWP : GMarkerGoogle
	{
		string wpno = "";
		public bool selected = false;
		SizeF txtsize = SizeF.Empty;
		static Dictionary<string, Bitmap> fontBitmaps = new Dictionary<string, Bitmap>();
		static Font font;

		public GMapMarkerWP(PointLatLng p, string wpno)
			: base(p, GMarkerGoogleType.green)
		{
			this.wpno = wpno;
			if (font == null)
				font = SystemFonts.DefaultFont;

			if (!fontBitmaps.ContainsKey(wpno))
			{
				Bitmap temp = new Bitmap(100, 40, PixelFormat.Format32bppArgb);
				using (Graphics g = Graphics.FromImage(temp))
				{
					txtsize = g.MeasureString(wpno, font);

					g.DrawString(wpno, font, Brushes.Black, new PointF(0, 0));
				}
				fontBitmaps[wpno] = temp;
			}
		}

		public override void OnRender(Graphics g)
		{
			if (selected)
			{
				g.FillEllipse(Brushes.Red, new Rectangle(this.LocalPosition, this.Size));
				g.DrawArc(Pens.Red, new Rectangle(this.LocalPosition, this.Size), 0, 360);
			}

			base.OnRender(g);

			var midw = LocalPosition.X + 10;
			var midh = LocalPosition.Y + 3;

			if (txtsize.Width > 15)
				midw -= 4;

			if (Overlay.Control.Zoom > 16 || IsMouseOver)
				g.DrawImageUnscaled(fontBitmaps[wpno], midw, midh);
		}
	}

	[Serializable]
	public class GMapMarkerQuad : GMapMarker
	{
		private readonly Bitmap icon = Resources.if_plane_48;

		float heading = 0;
		float cog = -1;
		float target = -1;
		private int sysid = -1;

		public float warn = -1;
		public float danger = -1;

		public GMapMarkerQuad(PointLatLng p, float heading, float cog, float target, int sysid)
			: base(p)
		{
			this.heading = heading;
			this.cog = cog;
			this.target = target;
			this.sysid = sysid;
			Size = icon.Size;
		}

		public override void OnRender(Graphics g)
		{
			Matrix temp = g.Transform;
			g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

			int length = 500;
			// anti NaN
			try
			{
				g.DrawLine(new Pen(Color.Red, 2), 0.0f, 0.0f, (float)Math.Cos((heading - 90) * MathHelper.deg2rad) * length,
					(float)Math.Sin((heading - 90) * MathHelper.deg2rad) * length);
			}
			catch
			{
			}
			//g.DrawLine(new Pen(Color.Green, 2), 0.0f, 0.0f, (float)Math.Cos((nav_bearing - 90) * MathHelper.deg2rad) * length, (float)Math.Sin((nav_bearing - 90) * MathHelper.deg2rad) * length);
			//g.DrawLine(new Pen(Color.Black, 2), 0.0f, 0.0f, (float)Math.Cos((cog - 90) * MathHelper.deg2rad) * length,
			//	(float)Math.Sin((cog - 90) * MathHelper.deg2rad) * length);
			Pen dashpen = new Pen(Color.Orange, 2);
			dashpen.DashStyle = DashStyle.Dash;

			g.DrawLine(dashpen, 0.0f, 0.0f, (float)Math.Cos((target - 90) * MathHelper.deg2rad) * length,
				(float)Math.Sin((target - 90) * MathHelper.deg2rad) * length);
			// anti NaN
			try
			{
				g.RotateTransform(heading);
			}
			catch
			{
			}

			g.DrawImageUnscaled(icon, icon.Width / -2 + 2, icon.Height / -2);

			g.DrawString(sysid.ToString(), new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold), Brushes.Red, -8,
				-8);

			g.Transform = temp;

			{
				double width =
	   (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
		   Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
				double m2pixelwidth = Overlay.Control.Width / width;

				GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * warn * 2)), LocalPosition.Y);

				if (m2pixelwidth > 0.001 && warn > 0)
					g.DrawArc(Pens.Orange,
						new System.Drawing.Rectangle(
							LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
							LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2,
							(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);

				loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * danger * 2)), LocalPosition.Y);

				if (m2pixelwidth > 0.001 && danger > 0)
					g.DrawArc(Pens.Red,
						new System.Drawing.Rectangle(
							LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
							LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2,
							(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);

			}
		}
	}

	[Serializable]
	public class GMapMarkerRallyPt : GMapMarker
	{
		public float? Bearing;

		static readonly System.Drawing.Size SizeSt = new System.Drawing.Size(Resources.if_3_15636567.Width,
			Resources.if_3_15636567.Height);

		//static Bitmap localcache1 = Resources.shadow50;
		static Bitmap localcache2 = Resources.if_3_15636567;

		public int Alt { get; set; }

		public GMapMarkerRallyPt(PointLatLng p)
			: base(p)
		{
			Size = SizeSt;
			Offset = new Point(-10, -40);
		}

		public GMapMarkerRallyPt(MAVLink.mavlink_rally_point_t mark)
			: base(new PointLatLng(mark.lat / 1e7, mark.lng / 1e7))
		{
			Size = SizeSt;
			Offset = new Point(-10, -40);
			Alt = mark.alt;
			Alt = (int)mark.alt;
			ToolTipMode = MarkerTooltipMode.OnMouseOver;
			ToolTipText = "Rally Point" + "\nAlt: " + mark.alt;
		}

		static readonly Point[] Arrow = new Point[]
		{new Point(-7, 7), new Point(0, -22), new Point(7, 7), new Point(0, 2)};

		public override void OnRender(Graphics g)
		{
#if !PocketPC
			g.DrawImageUnscaled(localcache2, LocalPosition.X, LocalPosition.Y);

#else
	//    DrawImageUnscaled(g, Resources.shadow50, LocalPosition.X, LocalPosition.Y);
			DrawImageUnscaled(g, Resources.marker, LocalPosition.X, LocalPosition.Y);
#endif
		}
	}
}
