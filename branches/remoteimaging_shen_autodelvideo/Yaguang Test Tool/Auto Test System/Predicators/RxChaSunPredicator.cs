using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    using DataPickers;

    public class RxChaSunPredicator : BigThanPredicator
    {
        /// <summary>
        /// Initializes a new instance of the RxChaSunPredicator class.
        /// </summary>
        public RxChaSunPredicator()
        {
            this.Pick = ValuePicker.PickMin;
            this.StandardValue = float.Parse(Properties.Settings.Default.ThreshRxChaSun);
        }


    }
}
