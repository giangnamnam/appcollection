using System;
using System.Collections.Generic;
using Yaguang.VJK3G.IO;
using Yaguang.VJK3G.Properties;
using System.Globalization;

namespace Yaguang.VJK3G.Instrument
{

    public enum PortIOMode : int { Input = 0, Output = 1 }
    public class SwitchController : ISwitchController
    {
        private int deviceNo = 0;
        public IntPtr Handle = AC6651.InvalidHandle;
        private static ISwitchController instance = null;

        private SwitchSetting currentSwitchSetting;

        public IDictionary<SwitchSetting, int> ControlCodes { get; set; }


        //         private readonly int ControlCodeStart = 0x0000;
        //         private readonly int ControlCodeToolConfirm = 0x0800;
        //         private readonly int ControlCodeTXChaSun = 0x02B1;
        //         private readonly int ControlCodeRXChaSun = 0x0D51;
        //         private readonly int ControlCodeRXGeLiDu = 0x0551;
        //         private readonly int ControlCodeTXGeLiDu = 0x0A91;
        //         private readonly int ControlCodeTXPowerResist = 0X022A;
        //         private readonly int ControlCodeRXPowerResist = 0X0D0A;
        //         private readonly int ControlCodeSwitchSpeed = 0x0D44;

        private readonly int PortNoForOutPut = 0;
        private readonly int PortNoForInput = 1;



        private void RaiseSwitchChangedEvent()
        {
            if (this.SwitchChanged != null)
            {
                this.SwitchChanged(this, null);
            }
        }

        private void SwitchTo(SwitchSetting switchSetting)
        {

            int code = this.ControlCodes[switchSetting];
            WriteControlCode(code);
            this.currentSwitchSetting = switchSetting;
            System.Diagnostics.Debug.WriteLine(switchSetting);
            RaiseSwitchChangedEvent();

        }


        public PortIOMode GetPortIOMode(int portNo)
        {
            this.CheckPortNo(portNo);

            int IOMode = AC6651.GetIOMode(this.Handle);

            IOMode = (IOMode >> portNo) & 0x1;

            return (PortIOMode)IOMode;
        }

        public void SetPortIOMode(int portNo, PortIOMode mode)
        {
            this.CheckPortNo(portNo);

            PortIOMode OrgPortIOMOde = this.GetPortIOMode(portNo);
            if (OrgPortIOMOde == mode)
            {
                return;
            }

            int IOMode = AC6651.GetIOMode(this.Handle);

            int NewIOMode = (int)mode;

            if (NewIOMode == 1)
            {
                Helper.SetBit(ref IOMode, portNo);
            }
            else if (NewIOMode == 0)
            {
                Helper.ClearBit(ref IOMode, portNo);
            }

            int RetCode = AC6651.SetIOMode(this.Handle, IOMode);
            if (RetCode != AC6651.Succeed)
            {
                throw Helper.NewCustomException("StringAC6651SetPortIOModeError");
            }

        }

        public int DeviceNo
        {
            get { return this.deviceNo; }
        }

        public static ISwitchController Open(int deviceNo)
        {
            if (instance == null)
            {
                if (!GUI.Program.Debug)
                {
                    IntPtr handle = AC6651.OpenDevice(deviceNo);
                    if (handle == AC6651.InvalidHandle)
                    {
                        throw Helper.NewCustomException("≥ı ºªØAC6651¥ÌŒÛ");
                    }

                    SwitchController c = new SwitchController(deviceNo);
                    c.Handle = handle;
                    instance = c;

                }
                else
                {
                    instance = new SwitchControllStub();
                }
            }

            return instance;

        }

        public void Close()
        {
            AC6651.CloseDevice(this.Handle);
        }

        public void InitControlPort()
        {
            this.SetPortIOMode(PortNoForInput, PortIOMode.Input);
            this.SetPortIOMode(PortNoForOutPut, PortIOMode.Output);
            this.WriteControlCode(0);
        }

        public void WriteControlCode(int controlCode)
        {
            int retcode = AC6651.DO(this.Handle, this.PortNoForOutPut, controlCode);
            if (retcode != AC6651.Succeed)
            {
                throw Helper.NewCustomException(@"StringAC6651DataOutError");
            }
        }

        public int ReadControlCode()
        {
            int Code = AC6651.DI(this.Handle, this.PortNoForInput);

            return Code;
        }

        private void CheckPortNo(int portNo)
        {
            if (portNo < 0 || portNo > 3)
            {
                throw Helper.NewCustomException("StringPortNoOutOfRang");
            }
        }

        private SwitchController(int deviceNo)
        {
            this.deviceNo = deviceNo;

            this.ControlCodes = new Dictionary<SwitchSetting, int>()
            {
                 {SwitchSetting.Start, int.Parse(Settings.Default.CtrlCodeZero, NumberStyles.HexNumber)},
                 {SwitchSetting.ToolConfirm, int.Parse(Settings.Default.CtrlCodeTool, NumberStyles.HexNumber)},
                 {SwitchSetting.TXChaSunZhuBo, int.Parse(Settings.Default.CtrlCodeTXChaSunZhuBo, NumberStyles.HexNumber)},
                 {SwitchSetting.RXChaSunZhuBo, int.Parse(Settings.Default.CtrlCodeRXChaSunZhuBo, NumberStyles.HexNumber)},
                 {SwitchSetting.RXGeLiDu, int.Parse(Settings.Default.CtrlCodeRXGeLiDu, NumberStyles.HexNumber)},
                 {SwitchSetting.TXGeLiDu, int.Parse(Settings.Default.CtrlCodeTXGeLiDu, NumberStyles.HexNumber)},
                 {SwitchSetting.TXPowerResist, int.Parse(Settings.Default.CtrlCodeTXNaiGongLu, NumberStyles.HexNumber)},
                 {SwitchSetting.RXPowerResist, int.Parse(Settings.Default.CtrlCodeRXNaiGongLu, NumberStyles.HexNumber)},
                 {SwitchSetting.SysPowerCalib, int.Parse(Settings.Default.CtrlCodeSystemPower, NumberStyles.HexNumber)},
                 {SwitchSetting.PreTxPower, int.Parse(Settings.Default.CtrlCodeTxPreTxPower, NumberStyles.HexNumber)},
                 {SwitchSetting.PostTxPower, int.Parse(Settings.Default.CtrlCodeTxPostTxPower, NumberStyles.HexNumber)},
            };

        }


        public SwitchSetting CurrentSwitch
        {
            get
            {
                return this.currentSwitchSetting;
            }
            set
            {
                this.SwitchTo(value);
                this.currentSwitchSetting = value;
            }
        }

        public void Initialize()
        {
            InitControlPort();
            WriteControlCode(0);
            //InitPWM();

            //this.PWMPeriod = 1000;
            //this.PWMLowTTL = 900;
        }

        public int TC1
        {
            get
            {
                int code = AC6651.DI(this.Handle, PortNoForInput);
                code &= 0x40;
                code >>= 2;
                return code;
            }
        }

        public int TC2
        {
            get
            {
                int code = AC6651.DI(this.Handle, PortNoForInput);
                code &= 0x80;
                code >>= 3;
                return code;
            }
        }

        public int PWMPeriod
        {
            set
            {
                int RetCode = AC6651.SetTimerData(this.Handle, 1, value);
                if (RetCode != AC6651.Succeed)
                {
                    throw Helper.NewCustomException("StringAC6651SetTimerDataError");
                }
            }
        }

        public int PWMLowTTL
        {
            set
            {
                int RetCode = AC6651.SetTimerData(this.Handle, 0, value);
                if (RetCode != AC6651.Succeed)
                {
                    throw Helper.NewCustomException("StringAC6651SetTimerDataError");
                }
            }
        }

        public event EventHandler SwitchChanged;

        public static ISwitchController Default
        {
            get
            {
                return instance;
            }
        }
    }
}
