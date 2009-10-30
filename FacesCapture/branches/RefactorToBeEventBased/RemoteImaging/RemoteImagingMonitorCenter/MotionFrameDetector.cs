using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MotionDetect;
using OpenCvSharp;
using ImageProcessing;

namespace RemoteImaging
{
    public class MotionFrameDetector : IFrameHandler
    {
        public event EventHandler<MotionFrameDetectedEventArgs> MotionFrameDetected;

        public void Initialize()
        {

        }


        private static bool IsStaticFrame(Frame aFrame)
        {
            return aFrame.IplPtr != null
                    && (aFrame.searchRect.Width == 0 || aFrame.searchRect.Height == 0);
        }


        protected virtual void OnMotionFrameDetected(MotionFrameDetectedEventArgs args)
        {
            if (this.MotionFrameDetected != null)
            {
                this.MotionFrameDetected(this, args);
            }
        }

        #region IFrameHandler Members

        public void HandleFrame(ManagedFrame f)
        {
            Frame uf = f.Unmanaged;
            Frame ufToBeProcessed = new Frame();

            bool groupCaptured = MotionDetecter.PreProcessFrame(uf, ref ufToBeProcessed);

            if (IsStaticFrame(ufToBeProcessed))
            {
                Cv.Release(ref ufToBeProcessed.IplPtr);
                return;
            }

            MotionFrameDetectedEventArgs args = new MotionFrameDetectedEventArgs();
            args.Frame = ufToBeProcessed.ToManaged();
            args.GroupCaptured = groupCaptured;

            OnMotionFrameDetected(args);
        }

        #endregion
    }
}
