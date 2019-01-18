using Diva.Properties;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Diva.Mavlink;

namespace Diva.Utilities
{
    public class GMapLineMarker : GMapMarker
    {
        public Pen Pen;
        public Color Color
        {
            get => Pen.Color;
            set
            {
                Pen.Color = value;
                if (InitialColor == null)
                    InitialColor = value;
            }
        }
        Color? InitialColor;

        public GMapLineMarker(PointLatLng p, Brush b) : base(p)
        {
            Pen = new Pen(b, 2) { DashStyle = DashStyle.Dash };
            Size = new Size(50, 50);
            Offset = new Point(-Size.Width / 2, -Size.Height / 2 - 20);
        }

        public void ResetColor() { Color = InitialColor ?? Color.White;  }
    }

	[Serializable]
	public class GMapMarkerRect : GMapLineMarker
    {
		public GMapMarker InnerMarker;

		public int wprad = 0;

        public GMapMarkerRect(PointLatLng p) : base(p, Brushes.White) { }

		public override void OnRender(Graphics g)
		{
			base.OnRender(g);

			if (wprad == 0 || Overlay.Control == null)
				return;

			double width =
				(Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
					Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
			double height =
				(Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
					Overlay.Control.FromLocalToLatLng(Overlay.Control.Height, 0)) * 1000.0);
			double m2pixelwidth = Overlay.Control.Width / width;
			double m2pixelheight = Overlay.Control.Height / height;

			GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * wprad * 2)), LocalPosition.Y);

			if (m2pixelheight > 0.5 && !double.IsInfinity(m2pixelheight))
				g.DrawArc(Pen,
					new Rectangle(
						LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						LocalPosition.Y - Offset.Y - (int)Math.Abs(loc.X - LocalPosition.X) / 2,
						(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);
		}
	}

	[Serializable]
	public class GMapMarkerEllipse : GMapLineMarker
    {
        public GMapMarkerEllipse(PointLatLng p) : base(p, Brushes.Black) { }

		public override void OnRender(Graphics g)
		{
			base.OnRender(g);
			g.DrawEllipse(Pen, new RectangleF(new PointF(-125.0F, -125.0F),	new SizeF(250.0F, 250.0F)));

		}
	}

	[Serializable]
	public class GMapDroneMarker : GMapMarker
	{
		private readonly Bitmap icon = Resources.icon_drone_4axis;
        public readonly MavDrone Drone;

		float Yaw => Drone.Status.Yaw;
		float CoG => Drone.Status.GroundCourse;
		float Bearing => Drone.Status.NAVBearing;
		int SysId => Drone.SysId;

		public float warn = -1;
		public float danger = -1;

		public GMapDroneMarker(MavDrone drone)
			: base(new PointLatLng(drone.Status.Latitude, drone.Status.Longitude))
		{
            Drone = drone;
			Size = icon.Size;
			icon.SetResolution(100, 100);
		}

		public override void OnRender(Graphics g)
		{
            Position = new PointLatLng(Drone.Status.Latitude, Drone.Status.Longitude);
            Matrix temp = g.Transform;
			g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

			int length = 500;
			// anti NaN
			try
			{
				g.DrawLine(new Pen(Color.Red, 2), 0.0f, 0.0f, (float)Math.Cos((Yaw - 90) * MathHelper.deg2rad) * length,
					(float)Math.Sin((Yaw - 90) * MathHelper.deg2rad) * length);
			}
			catch
			{
			}
			g.DrawLine(new Pen(Color.Green, 2), 0.0f, 0.0f, (float)Math.Cos((CoG - 90) * MathHelper.deg2rad) * length,
				(float)Math.Sin((CoG - 90) * MathHelper.deg2rad) * length);
            Pen dashpen = new Pen(Color.Orange, 2)
            {
                DashStyle = DashStyle.Dash
            };

            g.DrawLine(dashpen, 0.0f, 0.0f, (float)Math.Cos((Bearing - 90) * MathHelper.deg2rad) * length,
				(float)Math.Sin((Bearing - 90) * MathHelper.deg2rad) * length);
			// anti NaN
			try
			{
				g.RotateTransform(Yaw);
			}
			catch
			{
			}

			g.DrawImageUnscaled(icon, icon.Width / -2 + 2, icon.Height / -2);


			// Show SYSID on the drone icon.
			g.RotateTransform(180);
			g.DrawString(SysId.ToString(), new Font(FontFamily.GenericMonospace, 15, FontStyle.Bold), Brushes.Blue, -8, -8);

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

    #region not done yet
    [Serializable]
	public class GMapMarkerWP : GMarkerGoogle
	{
		string wpno = "";
		public bool selected = false;
		SizeF txtsize = SizeF.Empty;
		static Dictionary<string, Bitmap> fontBitmaps = new Dictionary<string, Bitmap>();
		static Font font;

		public GMapMarkerWP(PointLatLng p, string wpno)
			: base(p, new Bitmap(Resources.point_blue))
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
	public class GMapMarkerRallyPt : GMapMarker
	{
		public float? Bearing;

		// TODO(causemx): add location icon here.
		static readonly Size SizeSt = new Size(Resources.icon_live.Width,
			Resources.icon_live.Height);

		static Bitmap localcache2 = Resources.icon_live;

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

	[Serializable]
	public class GMapCustomizedPolygon : GMapPolygon
	{
		public string zoneName = "zone";

		public GMapCustomizedPolygon(List<PointLatLng> points, string name, string zonename) : base(points, name)
		{
			this.zoneName = zonename;
		}

		public override void OnRender(Graphics g)
		{
			base.OnRender(g);

			PointF p = GetCentroid(this.LocalPoints);

			Font font = new Font(FontFamily.GenericSansSerif, 24, FontStyle.Bold);
			// Measure the size of the text. 
			// You might want to add some extra space around your text. 
			// MeasureString is quite tricky...
			SizeF textSize = g.MeasureString(this.zoneName, font);

			// Get LocalPoint (your LatLng coordinate in pixel)
			Point localPosition = new Point(0, 0);

			// Move the localPosition by the half size of the text.
			// PointF textPosition = new PointF((float)(localPosition.X - textSize.Width / 2f), (float)(localPosition.Y - textSize.Height / 2f));



			// Draw Background
			g.FillRectangle(new SolidBrush(Color.Transparent), new RectangleF(p, textSize));
			g.DrawString(this.zoneName, font, new SolidBrush(Color.Red), p);
		}

		public PointF GetCentroid(List<GPoint> poly)
		{
			float accumulatedArea = 0.0f;
			float centerX = 0.0f;
			float centerY = 0.0f;

			for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
			{
				float temp = poly[i].X * poly[j].Y - poly[j].X * poly[i].Y;
				accumulatedArea += temp;
				centerX += (poly[i].X + poly[j].X) * temp;
				centerY += (poly[i].Y + poly[j].Y) * temp;
			}

			if (Math.Abs(accumulatedArea) < 1E-7f)
				return PointF.Empty;  // Avoid division by zero

			accumulatedArea *= 3f;
			return new PointF(centerX / accumulatedArea, centerY / accumulatedArea);

		}
	}
    #endregion
}
