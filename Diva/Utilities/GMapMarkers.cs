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
	public class GMapRectMarker : GMapLineMarker
    {
		public GMapMarker InnerMarker;

		public int wprad = 0;

        public GMapRectMarker(PointLatLng p) : base(p, Brushes.White) { }

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
	public class GMapEllipseMarker : GMapLineMarker
    {
        public GMapEllipseMarker(PointLatLng p) : base(p, Brushes.Black) { }

		public override void OnRender(Graphics g)
		{
			base.OnRender(g);
			g.DrawEllipse(Pen, new RectangleF(new PointF(-125.0F, -125.0F),	new SizeF(250.0F, 250.0F)));

		}
	}

    [Serializable]
    public class GMapPlusMarker : GMapMarker
    {
        private static readonly Bitmap icong = new Bitmap(Resources.icon_mid_plus_32);

        public GMapPlusMarker(PointLatLng p)
            : base(p)
        {
            // used for hitarea
            Size = icong.Size;
            Offset = new Point(-Size.Width / 2, -Size.Height / 2);
        }

        public override void OnRender(Graphics g)
        {
            var temp = g.Transform;
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

            if (IsMouseOver)
            {
                g.TranslateTransform(icong.Width / 2, icong.Height / -4);
                g.RotateTransform(45);
            }

            g.DrawImageUnscaled(icong, 0, 0);// icong.Width / -2, icong.Height / -2);

            g.Transform = temp;
        }
    }

    [Serializable]
	public class GMapDroneMarker : GMapMarker
	{
		public Bitmap Icon;
        public readonly MavDrone Drone;

		float Yaw => Drone.Status.Yaw;
		float CoG => Drone.Status.GroundCourse;
		float Bearing => Drone.Status.NAVBearing;

		public float warn = -1;
		public float danger = -1;

        public readonly bool IsShip;

        public GMapDroneMarker(MavDrone drone)
			: base(drone.Status.Location)
		{
            Drone = drone;
            IsShip = drone == BaseLocation.AsDrone || drone.IsShip();
            Icon = IsShip ? Resources.boat_side : Resources.VTOL;
            Size = Icon.Size;
            //Icon.SetResolution(100, 100);
        }

        public override void OnRender(Graphics g)
		{
            Position = Drone.Status.Location;
            Matrix temp = g.Transform;
			g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

			const int length = 500;

            if (!IsShip)
            {
                g.DrawLine(new Pen(Color.Green, 2), 0.0f, 0.0f, (float)Math.Cos((CoG - 90) * MathHelper.deg2rad) * length,
                    (float)Math.Sin((CoG - 90) * MathHelper.deg2rad) * length);
                using (Pen dashpen = new Pen(Color.Orange, 2) { DashStyle = DashStyle.Dash })
                    g.DrawLine(dashpen, 0.0f, 0.0f,
                        (float)Math.Cos((Bearing - 90) * MathHelper.deg2rad) * length,
                        (float)Math.Sin((Bearing - 90) * MathHelper.deg2rad) * length);
            }

            // anti NaN
            try
            {
                if (!IsShip)
                {
                    g.DrawLine(new Pen(Color.Red, 2), 0.0f, 0.0f, (float)Math.Cos((Yaw - 90) * MathHelper.deg2rad) * length,
                        (float)Math.Sin((Yaw - 90) * MathHelper.deg2rad) * length);
                    g.RotateTransform(Yaw);
                }
            }
            catch
			{
			}
            g.DrawImageUnscaled(Icon, -Size.Width / 2 + 1, -Size.Height / 2);

			// Show name on the drone icon.
            string name = Drone.Name;
            // shift text for ship name for clarity
            if (IsShip) name = Environment.NewLine + Environment.NewLine + name;
            Font font = new Font(FontFamily.GenericMonospace, SystemFonts.DefaultFont.Size, FontStyle.Bold);
            SizeF textSize = g.MeasureString(name, font);
            if (textSize.Width > Icon.Width + 8)
            {
                do
                {
                    name = name.Substring(0, name.Length - 1);
                } while ((textSize = g.MeasureString(name, font)).Width > Size.Width);
                name += "...";
            }
            using (GraphicsPath p = new GraphicsPath())
            {
                p.AddString(name, FontFamily.GenericMonospace, (int)FontStyle.Bold,
                    g.DpiY * font.Size / 72,
                    new PointF(-textSize.Width / 2, -textSize.Height / 2),
                    new StringFormat());
                g.DrawPath(new Pen(Brushes.Aqua, 2), p);
                g.FillPath(Brushes.Blue, p);
            }

            g.Transform = temp;

			{
				double width =
	   (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
		   Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
				double m2pixelwidth = Overlay.Control.Width / width;

				GPoint loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * warn * 2)), LocalPosition.Y);

				if (m2pixelwidth > 0.001 && warn > 0)
					g.DrawArc(Pens.Orange, new Rectangle(
						LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						LocalPosition.Y - Offset.Y - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);

				loc = new GPoint((int)(LocalPosition.X - (m2pixelwidth * danger * 2)), LocalPosition.Y);

				if (m2pixelwidth > 0.001 && danger > 0)
					g.DrawArc(Pens.Red, new Rectangle(
						LocalPosition.X - Offset.X - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						LocalPosition.Y - Offset.Y - (int)(Math.Abs(loc.X - LocalPosition.X) / 2),
						(int)Math.Abs(loc.X - LocalPosition.X), (int)Math.Abs(loc.X - LocalPosition.X)), 0, 360);
			}
		}
	}


    [Serializable]
	public class GMapTaggedMarker : GMarkerGoogle
	{
		public bool Selected = false;
        readonly int PosOffsetX;
        readonly int PosOffsetY = 3;
		static Dictionary<string, Bitmap> fontBitmaps = new Dictionary<string, Bitmap>();
		static Font font = SystemFonts.DefaultFont;

		public GMapTaggedMarker(PointLatLng p, string tag)
			: base(p, new Bitmap(Resources.point_blue))
		{
            Tag = tag;
            SizeF txtSize;
			if (fontBitmaps.ContainsKey(tag))
            {
                using (Graphics g = Graphics.FromImage(fontBitmaps[tag]))
                {
                    txtSize = g.MeasureString(tag, font);
                }
            }
            else
			{
				Bitmap temp = new Bitmap(100, 40, PixelFormat.Format32bppArgb);
				
				using (Graphics g = Graphics.FromImage(temp))
				{
                    txtSize = g.MeasureString(tag, font);
					g.DrawString(tag, font, Brushes.Black, new PointF(0, 0));
				}
				fontBitmaps[tag] = temp;
			}
            PosOffsetX = txtSize.Width > 15 ? 6 : 10;
		}

		public override void OnRender(Graphics g)
		{
			if (Selected)
			{
				g.FillEllipse(Brushes.Red, new Rectangle(LocalPosition, Size));
				g.DrawArc(Pens.Red, new Rectangle(LocalPosition, Size), 0, 360);
			}

			base.OnRender(g);

			var midw = LocalPosition.X + PosOffsetX;
			var midh = LocalPosition.Y + PosOffsetY;

			if (Overlay.Control.Zoom > 16 || IsMouseOver)
				g.DrawImageUnscaled(fontBitmaps[(string)Tag], midw, midh);
		}
	}

	[Serializable]
	public class GMapMarkerRallyPt : GMapMarker
	{
        public static Bitmap Localcache2 => Resources.icon_live;
        static readonly Size SizeSt = Localcache2.Size;

        public new PointLatLngAlt Position;
		public int Alt
        {
            get => (int)Position.Alt;
            set => Position.Alt = value;
        }

        public GMapMarkerRallyPt(PointLatLngAlt p) : base(p)
		{
			Size = SizeSt;
			Offset = new Point(-10, -40);
		}

		static readonly Point[] Arrow = new Point[]
		{
            new Point(-7, 7),
            new Point(0, -22),
            new Point(7, 7),
            new Point(0, 2)
        };

		public override void OnRender(Graphics g)
		{
			g.DrawImageUnscaled(Localcache2, LocalPosition.X, LocalPosition.Y);
		}
	}
}
