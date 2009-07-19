using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSZN.Net
{
    public class MAC
    {
        byte[] buffer = new byte[6];
        public MAC(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5)
        {
            buffer[0] = b0;
            buffer[1] = b1;
            buffer[2] = b2;
            buffer[3] = b3;
            buffer[4] = b4;
            buffer[5] = b5;
        }

        public override string ToString()
        {
            return string.Format("{0:d2}:{1:d2}:{2:d2}:{3:d2}:{4:d2}:{5:d2}",
                buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5]);
        }

        public byte[] GetBytes()
        {
            return (byte[])buffer.Clone();
        }
    }
}
