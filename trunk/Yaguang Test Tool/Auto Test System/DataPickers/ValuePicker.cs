using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G.DataPickers
{
    public delegate float PickData(IList<string> values);

    public static class ValuePicker
    {
        public static float PickMax(IList<string> values)
        {
            float max = float.Parse(values[0]);

            for (int i = 1; i < values.Count; i++)
            {
                float f = float.Parse(values[i]);
                if (f > max)
                {
                    max = f;
                }
            }

            return max;
        }



        public static float PickMin(IList<string> values)
        {
            float min = float.Parse(values[0]);

            for (int i = 1; i < values.Count; i++)
            {
                float f = float.Parse(values[i]);
                if (f < min)
                {
                    min = f;
                }
            }

            return min;
        }
    }
}
