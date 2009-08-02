using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace ImageProcess
{
    [StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Frame
    {
        public byte cameraID;
        /// IplImage*
        public System.IntPtr image;

        public CvRect searchRect;

        /// int
        public long timeStamp;
    }
}
