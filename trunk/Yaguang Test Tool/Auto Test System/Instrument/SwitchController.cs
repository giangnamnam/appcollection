using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.IO;

namespace Yaguang.VJK3G.Instrument
{

    public enum PortIOMode : int { Input = 0, Output = 1 }
    public enum SwitchSetting
    {
        Start,
        ToolConfirm,
        TXChaSunZhuBo,
        RXChaSunZhuBo,
        RXGeLiDu,
        TXGeLiDu,
        TXPowerResist,
        RXPowerResist,
        SwitchSpeed
    }

    public class SwitchController
    {
        private int deviceNo = 0;
        public IntPtr Handle = AC6651.InvalidHandle;
        private static SwitchController instance = null;

        private SwitchSetting currentSwitchSetting;
        private IDictionary<SwitchSetting, int> _controlCodes;

        public IDictionary<SwitchSetting, int> ControlCodes
        {
            get
            {
                return this._controlCodes;
            }
            set
            {
                this._controlCodes = value;
            }
        }


        //         private readonly int ControlCodeStart = 0x0000;
        //         private readonly int ControlCodeToolConfirm = 0x0800;
        //         private readonly int ControlCodeTXChaSun = 0x02B1;
        //         private readonly int ControlCodeRXChaSun = 0x0D51;
        //         private readonly int ControlCodeRXGeLiDu = 0x0551;
        //         private readonly int ControlCodeTXGeLiDu = 0x0A91;
        //         private readonly int ControlCodeTXPowerResist = 0X022A;
        //         private readonly int ControlCodeRXPowerResist = 0X0D0A;
        //         private readonly int ControlCodeSwitchSpeed = 0x0D44;

        private readonly int PortNoForLowByte = 0;
        private readonly int PortNoForHiByte = 1;



        private void RaiseSwitchChangedEvent()
        {
            if (this.SwitchChanged != null)
            {
                this.SwitchChanged(this, null);
            }
        }

        private void SwitchTo(SwitchSetting switchSetting)
        {
            
            int code = this._controlCodes[switchSetting];
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



        public static SwitchController Open(int deviceNo)
        {
            if (instance == null)
            {
                IntPtr handle = AC6651.OpenDevice(deviceNo);
                if (handle == AC6651.InvalidHandle)
                {
                    throw Helper.NewCustomException("³õÊ¼»¯AC6651´íÎó");
                }

                instance = new SwitchController(deviceNo);
                instance.Handle = handle;
            }

            return instance;

        }

        public void Close()
        {
            AC6651.CloseDevice(this.Handle);
        }

        public void InitControlPort()
        {
            this.SetPortIOMode(PortNoForHiByte, PortIOMode.Output);
            this.SetPortIOMode(PortNoForLowByte, PortIOMode.Output);
            this.SetPortIOMode(2, PortIOMode.Input);
            this.WriteControlCode(0);
        }

        public void WriteControlCode(int controlCode)
        {
            int retcode = AC6651.DO(this.Handle, this.PortNoForLowByte, controlCode);
            if (retcode != AC6651.Succeed)
            {
                throw Helper.NewCustomException(@"StringAC6651DataOutError");
            }

            controlCode >>= 8;
            AC6651.DO(this.Handle, this.PortNoForHiByte, controlCode);
            if (retcode != AC6651.Succeed)
            {
                throw Helper.NewCustomException(@"StringAC6651DataOutError");
            }
        }

        public int ReadControlCode()
        {
            int Code = AC6651.DI(this.Handle, this.PortNoForHiByte);
            Code <<= 8;
            Code |= AC6651.DI(this.Handle, this.PortNoForLowByte);

            return Code;
        }

        public void InitPWM()
        {
            int RetCode = AC6651.SetTimerMode(this.Handle, 0, 3);
            if (RetCode != AC6651.Succeed)
            {
                throw Helper.NewCustomException("StringAC6651SetTimerModeError");
            }

            RetCode = AC6651.SetTimerMode(this.Handle, 1, 3);
            if (RetCode != AC6651.Succeed)
            {
                throw Helper.NewCustomException("StringAC6651SetTimerModeError");
            }
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

            this._controlCodes = new Dictionary<SwitchSetting, int>()
            {
                 {SwitchSetting.Start, int.Parse(Properties.Settings.Default.CtrlCodeZero, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.ToolConfirm, int.Parse(Properties.Settings.Default.CtrlCodeTool, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.TXChaSunZhuBo, int.Parse(Properties.Settings.Default.CtrlCodeTXChaSunZhuBo, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.RXChaSunZhuBo, int.Parse(Properties.Settings.Default.CtrlCodeRXChaSunZhuBo, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.RXGeLiDu, int.Parse(Properties.Settings.Default.CtrlCodeRXGeLiDu, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.TXGeLiDu, int.Parse(Properties.Settings.Default.CtrlCodeTXGeLiDu, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.TXPowerResist, int.Parse(Properties.Settings.Default.CtrlCodeTXNaiGongLu, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.RXPowerResist, int.Parse(Properties.Settings.Default.CtrlCodeRXNaiGongLu, System.Globalization.NumberStyles.HexNumber)},
                 {SwitchSetting.SwitchSpeed, int.Parse(Properties.Settings.Default.CtrlCodeSwitchSpeed, System.Globalization.NumberStyles.HexNumber)},
            };

        }

        public Yaguang.VJK3G.IO.AC6651 IO
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
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
            InitPWM();

            this.PWMPeriod = 1000;
            this.PWMLowTTL = 900;
        }

        public int TC1
        {
            get
            {
                int code = AC6651.DI(this.Handle, 2);
                code &= 0x01;
                return code;
            }
        }

        public int TC2
        {
            get
            {
                int code = AC6651.DI(this.Handle, 2);
                code &= 0x02;
                code >>= 1;
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


        public static SwitchController Default
        {
            get
            {
                return instance;
            }
        }
    }
}
