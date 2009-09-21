using System;
using System.Collections.Generic;
using System.Text;
using Yaguang.VJK3G.IO;

namespace Yaguang.VJK3G.Instrument
{
    public class SwitchControllStub : ISwitchController
    {
        public SwitchControllStub()
        {

        }
        public IDictionary<SwitchSetting, int> ControlCodes
        {
            get
            {
                return new Dictionary<SwitchSetting, int>();

            }
            set
            {

            }
        }
        public PortIOMode GetPortIOMode(int portNo)
        {
            return PortIOMode.Input;
        }
        public void SetPortIOMode(int portNo, PortIOMode mode)
        {

        }
        public int DeviceNo
        {
            get
            {
                return 0;
            }
        }
        public void Close()
        {

        }
        public void InitControlPort()
        {

        }
        public void WriteControlCode(int controlCode)
        {

        }
        public int ReadControlCode()
        {
            return 0;
        }
        public void InitPWM()
        {

        }
        public SwitchSetting CurrentSwitch
        {
            get
            {
                return SwitchSetting.Start;

            }
            set
            {
                System.Diagnostics.Debug.WriteLine(string.Format("WriteControlCode: {0}", value));
            }
        }
        public void Initialize()
        {

        }
        public int TC1
        {
            get
            {
                return 0;
            }
        }
        public int TC2
        {
            get
            {
                return 1;
            }
        }
        public int PWMPeriod
        {
            set
            {

            }
        }
        public int PWMLowTTL
        {
            set
            {

            }
        }
        public event EventHandler SwitchChanged;
    }
}
