using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedMouseSettings.Model
{
    static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(uint uiAction, uint uiParam, ref IntPtr pvParam, uint fWinIni);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);
    }

    public static class WinApiConstants
    {
        public const uint SPI_SETMOUSESPEED = 0x0071;
        public const uint SPI_GETMOUSESPEED = 0x0070;
    }
}
