using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.IO
{
    public interface IStringStream
    {
        string ReadString();

        void WriteString(string data);

        string Query(string queryString);
    }
}
