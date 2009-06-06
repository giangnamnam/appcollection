using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    public class TestItemOnAV : TestItemBase
    {

        #region ITestItem Members

        #endregion

        public string ChannelID
        {
            get;
            set;
        }


        public override void DoTest()
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

    }
}
