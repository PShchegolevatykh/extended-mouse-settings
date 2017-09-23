using System;
using Microsoft.Win32;

namespace ExtendedMouseSettings.Model
{
    public class MouseSettingsService : IMouseSettingsService
    {
        public void GetSensitivity(Action<MouseSettings, Exception> callback)
        {
            callback(new MouseSettings { Sensitivity = GetCurrentSensitivity() }, null);
        }

        private static int GetCurrentSensitivity()
        {
            var pvParam = (IntPtr) 0;
            NativeMethods.SystemParametersInfo(WinApiConstants.SPI_GETMOUSESPEED, 0, ref pvParam, 0);
            return pvParam.ToInt32();
        }

        public void SetSensitivity(Action<MouseSettings, Exception> callback, int newValue)
        {
            var pvParam = (IntPtr) newValue;
            NativeMethods.SystemParametersInfo(WinApiConstants.SPI_SETMOUSESPEED, 0, pvParam, 0);
            callback(new MouseSettings {Sensitivity = GetCurrentSensitivity()}, null);
        }

        public void SaveSensitivity(Action<MouseSettings, Exception> callback, int val)
        {
            SetForCurrentUser(val);
            SetForAllUsers(val); // would work on login screen
            callback(new MouseSettings {Sensitivity = GetCurrentSensitivity()}, null);
        }

        private void SetForAllUsers(int val)
        {
            var defaultSubKey = Registry.Users.OpenSubKey(".DEFAULT", true); // requires admin access rights
            var controlPanelKey = defaultSubKey?.OpenSubKey("Control Panel", true);
            controlPanelKey = controlPanelKey?.OpenSubKey("Mouse", true);
            controlPanelKey?.SetValue("MouseSensitivity", val.ToString());
        }

        private static void SetForCurrentUser(int val)
        {
            var currentKey = Registry.CurrentUser.OpenSubKey("Control Panel", true);
            currentKey = currentKey?.OpenSubKey("Mouse", true);
            currentKey?.SetValue("MouseSensitivity", val.ToString());
        }
    }
}