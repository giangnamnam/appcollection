using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    public class RxGeLiPredicator : PredicatorBase
    {
        public RxGeLiPredicator()
        {
            this.Pick = DataPickers.ValuePicker.PickMin;
            this.StandardValue = float.Parse(Properties.Settings.Default.ThreshRxGeLiDu);
        }

        public override bool Predicate(IList<string> values)
        {
            float v = this.Pick(values);
            bool pass = Math.Abs(v) >= Math.Abs(this.StandardValue);
            string msg = string.Format("passed: {0} = abs({1})>=abs({2})", pass, v, StandardValue);
            System.Diagnostics.Debug.WriteLine(msg);
            return pass;
        }
    }
}
