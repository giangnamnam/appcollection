using System;
using System.Collections.Generic;
using System.Text;
using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    public class TxPowerTestItem : TestItemOnAV
    {
        public TxPowerTestItem(Instrument.SwitchSetting ss, string channelID)
            : base(ss, channelID)
        {

        }

        public override void ReadData()
        {
            NetworkAnalyzer.Default.ActiveChannel = this.ChannelID;

            this.Values.Clear();
            this.Values.Add(NetworkAnalyzer.Default.ReadMark("4"));
        }
    }
}
