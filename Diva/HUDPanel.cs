using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Diva
{
    public class HUDPanel
    {
        public const float PitchPixelRatio = 2f;
        public const float PitchClampLimit = 60f;
        public const float PitchScaleLimit = 32f;
        public const int OuterDiameter = 132;
        public const int InnerDiameter = 120;
        public const int InnerRadius = 60;
        public const int HUDCenterX = 81;
        public const int HUDCenterY = 76;
        public const int HUDInnerLeft = 21;
        public const int HUDInnerRight = 141;
        public const int HUDInnerTop = 16;
        public const int HUDInnerButtom = 76;
        public const int HUDOuterLeft = 15;
        public const int HUDOuterRight = 147;
        public const int HUDOuterTop = 10;
        public const int HUDOuterButtom = 142;
        public const int CompassCenterX = 243;
        public const int CompassCenterY = 76;
        public const int CompassInnerLeft = 183;
        public const int CompassInnerRight = 303;
        public const int CompassInnerTop = 16;
        public const int CompassInnerButtom = 136;
        public const int CompassOuterLeft = 177;
        public const int CompassOuterRight = 309;
        public const int CompassOuterTop = 10;
        public const int CompassOuterButtom = 142;
        public static readonly Bitmap HUDSource = Properties.Resources.hud;
        public static readonly Bitmap CompassSource = Properties.Resources.compass;
        public static readonly Point[] RollIndicator =
            new Point[] { new Point(60, 6), new Point(55, 20), new Point(65, 20) };
        public static readonly Point[] PitchIndicator = new Point[] {
                new Point(15, 60), new Point(30, 60),
                new Point(90, 60), new Point(105, 60),
                new Point(40, 70), new Point(60, 60),
                new Point(80, 70), new Point(60, 60),
            };
        public static int PitchScaleLength(bool large = true, bool half = true)
        {
            int l = large ? 20 : 12;
            return half ? l / 2 : l;
        }
        public static readonly Point[] YawIndicator = new Point[] {
            new Point(0, -30), new Point(20, 25),
            new Point(0, 10), new Point(-20, 25) };

        public Color BorderColor;
        public Color GroundLineColor;
        public Color IndicatorColor;
        public Color ScaleLineColor;

        public Font TextFont;

        public Func<Point?> GetReferencePoint;
        public Func<(float Roll, float Yaw, float Pitch)> GetAttitude;

        public void Draw(Graphics g)
        {
            var pt = GetReferencePoint();
            if (pt == null) return;

            int refx = pt.Value.X, refy = pt.Value.Y;
            var (roll, yaw, pitch) = GetAttitude();

            using (var bgbrush = new SolidBrush(BorderColor))
            {
                g.FillEllipse(bgbrush, new Rectangle
                {
                    X = refx + HUDOuterLeft,
                    Y = refy + HUDOuterTop,
                    Width = OuterDiameter,
                    Height = OuterDiameter
                });
                g.FillEllipse(bgbrush, new Rectangle
                {
                    X = refx + CompassOuterLeft,
                    Y = refy + CompassOuterTop,
                    Width = OuterDiameter,
                    Height = OuterDiameter
                });
            }

            using (var hud = new Bitmap(InnerDiameter, InnerDiameter))
            {
                using (var hg = Graphics.FromImage(hud))
                {
                    hg.TranslateTransform(InnerRadius, InnerRadius);
                    // roll
                    hg.RotateTransform(-roll);

                    // ground & sky
                    float goffset = float.IsNaN(pitch) ? 0 : pitch * PitchPixelRatio;
                    if (goffset > PitchClampLimit) goffset = PitchClampLimit;
                    else if (goffset < -PitchClampLimit) goffset = -PitchClampLimit;
                    hg.DrawImage(HUDSource,
                        new Rectangle(-InnerRadius, -InnerRadius,
                            InnerDiameter, InnerDiameter),
                        0, InnerDiameter / 2 - goffset,
                        InnerDiameter, InnerDiameter, GraphicsUnit.Pixel);

                    using (var pen = new Pen(ScaleLineColor, 2))
                    {
                        // pitch scale line
                        if (!float.IsNaN(pitch))
                        {
                            int pitchscale = -90;
                            float soff;
                            while ((soff = (pitchscale - pitch) * PitchPixelRatio) < -PitchScaleLimit)
                                pitchscale += 5;
                            while ((soff = (pitchscale - pitch) * PitchPixelRatio) < PitchScaleLimit)
                            {
                                bool mark = pitchscale % 10 == 0;
                                int len = PitchScaleLength(mark);
                                hg.DrawLine(pen, -len, -soff, len, -soff);
                                if (mark)
                                {
                                    using (var brush = new SolidBrush(ScaleLineColor))
                                    {
                                        var str = pitchscale.ToString();
                                        var tsize = hg.MeasureString(str, TextFont);
                                        hg.DrawString(str, TextFont, brush,
                                            -len * 1.5f - tsize.Width, -soff - tsize.Height / 2);
                                        hg.DrawString(str, TextFont, brush,
                                            len * 1.5f, -soff - tsize.Height / 2);
                                    }
                                }
                                pitchscale += 5;
                            }
                            using (var gpen = new Pen(GroundLineColor, 2))
                            {
                                int len = PitchScaleLength(false) * 3;
                                float off = pitch * PitchPixelRatio;
                                hg.DrawLine(gpen, -len, off, len, off);
                            }
                        }

                        // roll scale line
                        if (!float.IsNaN(roll))
                        {
                            double R = InnerRadius * 0.9;
                            double interval = Math.PI * 10 / 180; // 10 degree
                            for (int i = -6; i <= 6; i++)
                            {
                                bool mark = i % 3 == 0;
                                double r = InnerRadius * (mark ? 0.7 : 0.8);
                                double deg = i * interval;
                                float x(double rr) => (float)(rr * Math.Sin(deg) - 0.5);
                                float y(double rr) => (float)(-rr * Math.Cos(deg));
                                hg.DrawLine(pen, x(r), y(r), x(R), y(R));
                            }
                        }
                    }

                    hg.ResetTransform();
                    // pitch indicator
                    if (!float.IsNaN(pitch))
                        using (var pen = new Pen(IndicatorColor, 2))
                        {
                            hg.DrawLine(pen, PitchIndicator[0], PitchIndicator[1]);
                            hg.DrawLine(pen, PitchIndicator[2], PitchIndicator[3]);
                            hg.DrawLine(pen, PitchIndicator[4], PitchIndicator[5]);
                            hg.DrawLine(pen, PitchIndicator[6], PitchIndicator[7]);
                        }
                    // roll indicator
                    if (!float.IsNaN(roll))
                        using (var brush = new SolidBrush(IndicatorColor))
                            hg.FillPolygon(brush, RollIndicator);
                }
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(refx + HUDInnerLeft,
                        refy + HUDInnerTop, InnerDiameter, InnerDiameter);
                    g.SetClip(path);
                    g.DrawImage(hud, refx + HUDInnerLeft, refy + HUDInnerTop);
                }
                g.ResetClip();
            }

            using (var compass = new Bitmap(InnerDiameter, InnerDiameter))
            {
                compass.MakeTransparent();
                if (!float.IsNaN(yaw))
                    using (var cg = Graphics.FromImage(compass))
                    {
                        cg.TranslateTransform(InnerRadius, InnerRadius);
                        cg.RotateTransform(yaw);
                        using (var brush = new SolidBrush(IndicatorColor))
                            cg.FillPolygon(brush, YawIndicator);
                    }
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(refx + CompassInnerLeft,
                        refy + CompassInnerTop, InnerDiameter, InnerDiameter);
                    g.SetClip(path);
                    g.DrawImage(CompassSource, refx + CompassInnerLeft, refy + CompassInnerTop);
                    g.DrawImage(compass, refx + CompassInnerLeft, refy + CompassInnerTop);

                    if (!float.IsNaN(yaw))
                    {
                        var str = ((int)yaw).ToString();
                        using (var font = new Font(TextFont.FontFamily, TextFont.Size * 2))
                        {
                            var size = g.MeasureString(str, font);
                            using (var brush = new SolidBrush(ScaleLineColor))
                                g.DrawString(str, font, brush,
                                    refx + CompassCenterX - size.Width / 2,
                                    refy + CompassCenterY - size.Height / 2);
                        };
                    }
                }
                g.ResetClip();
            }
        }
    }
}
