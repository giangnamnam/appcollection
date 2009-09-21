using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yaguang.VJK3G
{
    public class DataReadEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the DataReadEventArgs class.
        /// </summary>
        public DataReadEventArgs(IList<string> data)
        {
            Data = data;
        }

        public IList<string> Data { get; private set; }
    }
}
