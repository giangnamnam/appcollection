using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;

namespace Damany.Imaging.Processors
{

    public class MotionDetector
    {
        private readonly IEventAggregator _eventAggregator;
        private IMotionDetector _nullMotionDetector = new NullMotionDetector();
        private IMotionDetector _motionDetector = new FaceProcessingWrapper.MotionDetector();

        private IMotionDetector _currentMotionDetector;

        private object _detectorLock = new object();

        OpenCvSharp.CvSize lastImageSize;
        readonly FrameManager _manager = new FrameManager();


        public IMotionDetector CurrentMotionDetector
        {
            get
            {
                lock (_detectorLock)
                {
                    return _currentMotionDetector;
                }
            }
            set
            {
                lock (_detectorLock)
                {
                    _currentMotionDetector = value;
                }
            }

        }


        public event Action<IList<Frame>> MotionFrameCaptured;

        public MotionDetector(IEventAggregator eventAggregator, int workMode)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            _eventAggregator = eventAggregator;

            _eventAggregator.SwitchMotionDetector += delegate { this.SwitchMotionDetector(); };
            _currentMotionDetector = workMode == 1 ? _nullMotionDetector : _motionDetector;
        }

        public void SwitchMotionDetector()
        {
            if (CurrentMotionDetector == _nullMotionDetector)
            {
                CurrentMotionDetector = _motionDetector;
            }
            else
            {
                CurrentMotionDetector = _nullMotionDetector;
            }
        }

        public bool ProcessFrame(Frame frame)
        {
            try
            {
                frame.GetImage();
            }
            catch (System.ArgumentException ex)
            {
                return false;
            }

            this._manager.AddNewFrame(frame);

            var oldFrameMotionResult = new MotionDetectionResult();
            bool groupCaptured =
                ProcessNewFrame(frame, ref oldFrameMotionResult);

            ProcessOldFrame(oldFrameMotionResult);

            return groupCaptured;

        }

        public List<Frame> GetMotionFrames()
        {
            return _manager.RetrieveMotionFrames();
        }


        private bool ProcessNewFrame(Frame frame, ref MotionDetectionResult detectionResult)
        {
            var result = CurrentMotionDetector.Detect(frame, ref detectionResult);

            return result;
        }


        private static bool IsStaticFrame(OpenCvSharp.CvRect rect)
        {
            return rect.Width == 0 || rect.Height == 0;
        }


        private void ProcessOldFrame(MotionDetectionResult result)
        {
            if (IsStaticFrame(result.MotionRect))
            {
                this._manager.DisposeFrame(result.FrameGuid);
            }
            else
            {
                this._manager.MoveToMotionFrames(result);
            }
        }

        private void NotifyListener()
        {
            var frames = this._manager.RetrieveMotionFrames();
            if (frames.Count == 0) return;

            if (this.MotionFrameCaptured != null)
            {
                this.MotionFrameCaptured(frames);
            }
        }

        private bool ImageResolutionChanged(Frame currentFrame)
        {
            return currentFrame.GetImage().Size != lastImageSize;
        }

    }
}
