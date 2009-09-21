using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.Predicators
{
    public class TxPowerPredicator : BigThanPredicator
    {
        public TxPowerPredicator()
        {
            this.Pick = DataPickers.ValuePicker.PickMin;
            this.StandardValue = float.Parse(Properties.Settings.Default.ThreshPowerResist);
        }
    }
}
