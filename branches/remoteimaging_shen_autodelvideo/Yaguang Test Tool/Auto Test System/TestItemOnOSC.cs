using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    public class TestItemOnOSC : TestItemBase
    {

        public TestItemOnOSC(SwitchSetting switchSetting)
        : base(switchSetting)
        {
            this.Values = new List<string>();
        }


        #region ITestItem Members

 
        public void ReadResult()
        {
            this.Values.Clear();

            string v1 = Oscillograph.Default.ReadMeasure("1");

            string v2 = Oscillograph.Default.ReadMeasure("2");

            this.Values.Add(v1);
            this.Values.Add(v2);

        }

        #endregion

        public override void DoTest()
        {
            this.ReadResult();
            
        }
    }
}
