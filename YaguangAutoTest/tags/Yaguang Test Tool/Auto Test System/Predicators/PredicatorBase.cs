using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    public abstract class PredicatorBase : IPredicator
    {
        public abstract bool Predicate(IList<string> values);
        public float StandardValue { get; set; }
        public DataPickers.PickData Pick { get; set; }
    }
}
