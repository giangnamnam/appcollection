using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.IO
{
    public class AVStub : IStringStream
    {
        public string ReadString()
        {
            System.Threading.Thread.Sleep(20);
            Random rand = new Random();
            float value = (float)(100 * rand.NextDouble());

            System.Diagnostics.Debug.WriteLine(string.Format("In AVStub: {0}", value));

            //7.189189E+01,  0.000000E+00,   1.800000000E+09
            return string.Format("{0:E}, 0.000000E+00,   1.800000000E+09", value);

        }
        public void WriteString(string data)
        {

        }
        public string Query(string queryString)
        {
            return string.Empty;

        }
    }
}
