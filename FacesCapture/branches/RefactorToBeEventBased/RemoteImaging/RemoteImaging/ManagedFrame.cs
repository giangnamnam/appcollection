using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcessing;

namespace RemoteImaging
{
    public class ManagedFrame
    {
        public byte CameraID;
        public OpenCvSharp.IplImage Ipl;
        public DateTime TimeStamp;
        public OpenCvSharp.CvRect MotionRect;

        public Frame Unmanaged
        {
            get
            {
                Frame f = new Frame();
                f.cameraID = CameraID;
                f.IplPtr = Ipl.CvPtr;
                f.timeStamp = TimeStamp.Ticks;

                return f;
            }
        }
    }
}
