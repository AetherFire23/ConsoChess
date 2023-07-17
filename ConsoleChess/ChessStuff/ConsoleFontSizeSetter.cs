using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChess.ChessStuff
{
    public static class ConsoleFontSizeSetter
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CONSOLE_FONT_INFO_EX
        {
            public int cbSize;
            public uint nFont;
            public Coord dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Coord
        {
            public short X;
            public short Y;
        }

        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;

        public static void SetConsoleFontSize(int size)
        {
            IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
            CONSOLE_FONT_INFO_EX consoleFont = new CONSOLE_FONT_INFO_EX();
            consoleFont.cbSize = Marshal.SizeOf(consoleFont);
            consoleFont.FaceName = "Consolas"; // Set the desired font name
            consoleFont.dwFontSize = new Coord { X = 0, Y = (short)size };
            consoleFont.FontFamily = TMPF_TRUETYPE;

            SetCurrentConsoleFontEx(hnd, false, ref consoleFont);
        }
    }
}
