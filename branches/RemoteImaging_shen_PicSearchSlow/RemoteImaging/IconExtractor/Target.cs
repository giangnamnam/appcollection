using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Target
    {

        /// Frame
        public Frame BaseFrame;

        /// int
        public int FaceCount;

        /// IplImage*
        public System.IntPtr FaceData;
    }
}
