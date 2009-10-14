using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    public class MotionFrameDetectedEventArgs : EventArgs
    {
        public ManagedFrame Frame { get; set; }
        public bool GroupCaptured { get; set; }
    }
}
