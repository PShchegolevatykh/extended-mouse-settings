using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtendedMouseSettings.Model
{
    public interface IMouseSettingsService
    {
        void GetSensitivity(Action<MouseSettings, Exception> callback);
        void SetSensitivity(Action<MouseSettings, Exception> callback, int newValue);
        void SaveSensitivity(Action<MouseSettings, Exception> callback, int newValue);
    }
}
