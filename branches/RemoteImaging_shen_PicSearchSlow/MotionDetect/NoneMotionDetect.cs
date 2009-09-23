using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageProcess;
using OpenCvSharp;

namespace MotionDetect
{
    public static class NoneMotionDetect
    {
        public static bool PreProcessFrame(Frame frame, ref Frame lastFrame)
        {
            OpenCvSharp.IplImage ipl = new OpenCvSharp.IplImage(frame.image);
            ipl.IsEnabledDispose = false;


            lastFrame = frame;
            lastFrame.searchRect = new CvRect(0, 0, ipl.Width, ipl.Height);

            return true;
        }
    }
}
