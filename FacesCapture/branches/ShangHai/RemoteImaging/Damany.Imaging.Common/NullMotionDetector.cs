using System;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public class NullMotionDetector : IMotionDetector
    {
        private Guid _oldGuid = Guid.Empty;
        private CvRect _oldCvRect = new CvRect(0, 0, 0, 0);


        public bool Detect(Frame frame, ref MotionDetectionResult result)
        {
            result.FrameGuid = _oldGuid;
            result.MotionRect = _oldCvRect;

            var ipl = frame.GetImage();
            _oldCvRect = new CvRect(0, 0, ipl.Width, ipl.Height);

            _oldGuid = frame.Guid;

            return true;
        }



    }
}