using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    using Instrument;

    public abstract class TestItemWithSwitch : Yaguang.VJK3G.Test.TestItemBase
    {

        public TestItemWithSwitch(Instrument.SwitchSetting switchSetting)
        {
            this.SwitchSetting = switchSetting;
        }

        #region ITestItem Members

        public SwitchSetting SwitchSetting
        {
            get;
            set;
        }



        #endregion




    }
}
