using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Instrument
{
    public struct Frequency
    {
        public float value;
        public FrequencyUnit unit;

        public Frequency(float value, FrequencyUnit unit)
        {
            this.value = value;
            this.unit = unit;
        }

        public FrequencyUnit Unit
        {
            get
            {
                return this.unit;
            }
            set
            {
                this.unit = value;
            }
        }

        public float Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        public override string ToString()
        {
            return value.ToString() + this.unit.ToString();
        }
    }
}
