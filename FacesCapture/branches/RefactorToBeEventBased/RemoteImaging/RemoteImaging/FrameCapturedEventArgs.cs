using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class FrameCapturedEventArgs : EventArgs
    {
        public ManagedFrame Frame { get; set; }
    }
}
