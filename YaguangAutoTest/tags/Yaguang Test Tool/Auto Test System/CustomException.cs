using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G
{
    class CustomException : Exception
    {
        public CustomException() : base() {}
        public CustomException(string msg) : base(msg) {}
        public CustomException(string message, Exception innerException)
              : base(message, innerException) {}
    }
}
