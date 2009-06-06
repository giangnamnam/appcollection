using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    public abstract class BigThanPredicator : PredicatorBase
    {
        public override bool Predicate(IList<string> values)
        {
            float v = this.Pick(values);
            bool pass = v >= StandardValue;
            string msg = string.Format("passed: {0}={1}>={2}", pass, v, StandardValue);
            System.Diagnostics.Debug.WriteLine(msg);
            return pass;
        }
    }
}
