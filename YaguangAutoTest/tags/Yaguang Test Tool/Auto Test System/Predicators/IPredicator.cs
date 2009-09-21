using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{

    public interface IPredicator
    {
        bool Predicate(IList<string> values);
    }
}
