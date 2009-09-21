using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    public class TxChaSunPredicator : BigThanPredicator
    {
        /// <summary>
        /// Initializes a new instance of the TxChaSunPredicatorDescendant class.
        /// </summary>
        public TxChaSunPredicator()
        {
            this.Pick = DataPickers.ValuePicker.PickMin;
            this.StandardValue = float.Parse(Properties.Settings.Default.ThreshTxChaSun);
        }

    }
}
