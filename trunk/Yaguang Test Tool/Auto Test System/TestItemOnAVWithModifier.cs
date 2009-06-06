using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Test
{
    using Instrument;

    public class TestItemOnAVWithModifier : Yaguang.VJK3G.Test.TestItemOnAV
    {
        public TestItemOnAVWithModifier(SwitchSetting ss, string chID)
            : base(ss, chID)
        {

        }

        public Func<IList<string>, IList<string>> Modifier
        {
            get;
            set;
        }

        public override void AfterTest()
        {
            base.AfterTest();
            Modifier(Values);
        }

    }
}
