using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LXCustomTools.Help
{
    public static class EnumDisplayInfo
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {

            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;

        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(
                   string deviceName, int modeNum, ref DEVMODE devMode);

        public static List<string> GetDisplaySizeList()
        {
            DEVMODE vDevMode = new DEVMODE();
            List<string> list = new List<string>();
            int i = 0;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                string dmpels = $"{vDevMode.dmPelsWidth} x {vDevMode.dmPelsHeight} ";
                if (!list.Contains(dmpels))
                {
                    list.Add(dmpels);
                }
                i++;
            }
            list.Sort((a, b) =>
            {
                // 解析分辨率字符串，提取宽和高
                var partsA = a.Split('x');
                var partsB = b.Split('x');

                int widthA = int.Parse(partsA[0]);
                int heightA = int.Parse(partsA[1]);

                int widthB = int.Parse(partsB[0]);
                int heightB = int.Parse(partsB[1]);

                // 按宽度优先排序，如果宽度相同则按高度排序
                if (widthA != widthB)
                    return widthB.CompareTo(widthA); // 从大到小排序
                return heightB.CompareTo(heightA);   // 宽度相同时，按高度排序
            });
            return list;
        }
    }
}
