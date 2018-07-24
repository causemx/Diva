
namespace GMap.NET
{
   using System;
   using System.IO;
   using System.Drawing;

   /// <summary>
   /// image abstraction proxy
   /// </summary>
   public abstract class PureImageProxy
   {
      abstract public PureImage FromStream(Stream stream);
      abstract public bool Save(Stream stream, PureImage image);

      public PureImage FromArray(byte[] data)
      {
         MemoryStream m = new MemoryStream(data, 0, data.Length, false, true);
         var pi = FromStream(m);
         if(pi != null)
         {
            m.Position = 0;
            pi.Data = m;
         }
         else
         {
            m.Dispose();
         }
         m = null;

         return pi;
      }

        public PureImage GenerateDebugTile(string text, int x, int y, int z,
    Font font = null, Color? fg = null, Color? bg = null)
        {
            font = font ?? SystemFonts.DefaultFont;
            font = new Font(font.FontFamily, 24);
            var fb = new SolidBrush(fg ?? SystemColors.WindowText);
            PureImage ret = null;
            using (Image image = new Bitmap(256, 256))
            using (var g = Graphics.FromImage(image))
            {
                g.FillRectangle(SystemBrushes.ActiveBorder, 0, 0, 256, 256);
                g.FillRectangle(new SolidBrush(bg ?? SystemColors.WindowFrame), 4, 4, 252, 252);
                g.DrawString(text, font, fb, 12, 12);
                var size = g.MeasureString(text, font);
                g.DrawString($"Tile ({x}, {y})\nZoom Level={z}",
                    new Font(font.FontFamily, 12), fb, 12, 36 + size.Height);
                ret = FromImage(image);
            }
            return ret;
        }

        public PureImage FromImage(Image image)
        {
            PureImage ret = null;
            // chw: GMapImageProxy.FromStream does not set Data field
            //      which causes nullptr exception later
            //using (var ms = new MemoryStream())
            var ms = new MemoryStream();
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Position = 0;
                ret = FromStream(ms);
                if (ret.Data == null)
                    ret.Data = ms;
                else
                    ms.Dispose();
            }
            return ret;
        }
    }

    /// <summary>
    /// image abstraction
    /// </summary>
    public abstract class PureImage : IDisposable
   {
      public MemoryStream Data;

      internal bool IsParent;
      internal long Ix;
      internal long Xoff;
      internal long Yoff;

      #region IDisposable Members

      public abstract void Dispose();

      #endregion
   }
}
