using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Test
{
    public struct MarkValuePair
    {
        public MarkValuePair(Instrument.Mark mark, float value)
        {
            this.mark = mark;
            this.value = value;
        }

        public Instrument.Mark mark;
        public float value;
    }
}
