using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    public class TestItemOnAV : TestItemWithSwitch
    {

        #region ITestItem Members

        #endregion

        public string ChannelID
        {
            get;
            set;
        }


        public override void ReadData()
        {
            NetworkAnalyzer.Default.ActiveChannel = this.ChannelID;

            Helper.CopyMarks(this.Values, NetworkAnalyzer.Default.Marks);
        }


        public TestItemOnAV(Instrument.SwitchSetting ss,
                            string channelID)
            : base(ss)
        {
            this.ChannelID = channelID;
            this.Values = new List<string>();
        }

        public override void DoSetup()
        {
            SwitchController.Default.CurrentSwitch = this.SwitchSetting;
            Helper.Sleep(Properties.Settings.Default.ItemSwitchHoldTime);
        }

    }
}
