using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Diva.Utilities
{
    class FloatMessage
    {
        private const int BasicDisplayTime = 10;
        private const int SeverityExtendedTime = 16;
        private static readonly List<FloatMessage> floatMessages = new List<FloatMessage>();
        private readonly static Brush SrcBrushDflt = Brushes.Violet;
        public static Font SrcFont => SystemFonts.MessageBoxFont;
        public readonly static Brush TimeBrush = Brushes.DarkViolet;
        public static Font TimeFont => SystemFonts.MessageBoxFont;
        public readonly static Brush[] MsgBrushes =
        {
            Brushes.Magenta,
            Brushes.Red,
            Brushes.Orange,
            Brushes.Yellow,
            Brushes.GreenYellow,
            Brushes.Cyan,
            Brushes.Gray,
            Brushes.DimGray
        };
        public readonly static bool[] MsgBlink = { true, true, true, false, false, false, false, false };
        public readonly static Brush BlinkBrush = Brushes.Black;
        public static Brush BgBrush { get; } = new SolidBrush(Color.FromArgb(127, Color.Black));
        public static Font MsgFont => SystemFonts.MessageBoxFont;
        public static event EventHandler NewMessageNotice;

        public static void NewMessage(int severity, string message, int timeout = 0)
            => NewMessage("", SrcBrushDflt, severity, message, timeout);

        public static void NewMessage(string src, int severity, string message, int timeout = 0)
            => NewMessage(src, SrcBrushDflt, severity, message, timeout);

        public static void NewMessage(string src, Brush srcbrush, int severity, string message, int timeout = 0)
        {
            FloatMessage newMsg = new FloatMessage(src, srcbrush, severity, message, timeout);
            lock (floatMessages)
                floatMessages.Add(newMsg);
            NewMessageNotice?.Invoke(newMsg, null);
        }

        public static FloatMessage[] GetMessages(int msgLevel = 6) // Info, no debug
        {
            lock (floatMessages)
            {
                floatMessages.RemoveAll(m => m.Due < DateTime.Now);
                return floatMessages.Where(m => m.Severity <= msgLevel).ToArray();
            }
        }

        public readonly Brush SrcBrush;
        public readonly string Source;
        public readonly int Severity;
        public readonly string Message;
        public readonly string TimeStr;
        public readonly DateTime Due;

        private FloatMessage(string src, Brush srcbrush, int severity, string message, int timeout)
        {
            Source = src;
            SrcBrush = srcbrush;
            Severity = severity;
            Message = message;
            TimeStr = DateTime.Now.ToLocalTime().ToString("HH:mm:ss");
            Due = DateTime.Now.AddSeconds(timeout > 0 ? timeout :
                BasicDisplayTime + (SeverityExtendedTime / (1 + severity)));
        }
    }
}
