using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    using Instrument;

    public abstract class TestItemBase : Yaguang.VJK3G.Test.ITestItem
    {

        public TestItemBase(Instrument.SwitchSetting switchSetting)
        {
            this.SwitchSetting = switchSetting;

            this.SetupCommandsAV = new List<string>();
            this.SetupCommandsOSC = new List<string>();
        }

        #region ITestItem Members



        public SwitchSetting SwitchSetting
        {
            get;
            set;
        }

        public override void Setup()
        {
            SwitchController.Default.CurrentSwitch = this.SwitchSetting;

            this.ExecCommands(NetworkAnalyzer.Default, this.SetupCommandsAV);
            this.ExecCommands(Oscillograph.Default, this.SetupCommandsOSC);

            Helper.Sleep(Properties.Settings.Default.ItemSwitchHoldTime);
        }

        public override void AfterTest()
        {
            base.AfterTest();

            if (Properties.Settings.Default.ShutDownSwitchAfterTestItem)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
                Helper.Sleep(Properties.Settings.Default.SwitchOffMillisecond);
            }
        }

        #endregion


        public IList<string> SetupCommandsAV
        {
            get;
            set;
        }

        public IList<string> SetupCommandsOSC
        {
            get;
            set;
        }

        public void ExecCommands(GPIBDeviceBase dev, IList<string> commands)
        {
            foreach (var s in commands)
            {
                dev.ExecuteCommand(s);
            }
        }

    }
}
