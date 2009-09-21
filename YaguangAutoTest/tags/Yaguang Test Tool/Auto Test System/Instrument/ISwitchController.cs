using System;
using System.Collections.Generic;
using System.Text;
using Yaguang.VJK3G.IO;

namespace Yaguang.VJK3G.Instrument
{
    public interface ISwitchController
    {
        IDictionary<SwitchSetting, int> ControlCodes { get; set; }
        PortIOMode GetPortIOMode(int portNo);
        void SetPortIOMode(int portNo, PortIOMode mode);
        int DeviceNo { get; }
        void Close();
        void InitControlPort();
        void WriteControlCode(int controlCode);
        int ReadControlCode();
        SwitchSetting CurrentSwitch { get; set; }
        void Initialize();
        int TC1 { get; }
        int TC2 { get; }
        int PWMPeriod { set; }
        int PWMLowTTL { set; }
        event EventHandler SwitchChanged;
    }
}
