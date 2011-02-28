using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Damany.Imaging.Common
{
    public interface IEventAggregator
    {
        event EventHandler<MiscUtil.EventArgs<Portrait>> PortraitFound;
        event EventHandler<MiscUtil.EventArgs<PersonOfInterestDetectionResult>> FaceMatchFound;
        event EventHandler SwitchMotionDetector;
        event EventHandler<MiscUtil.EventArgs<bool>> IsBusyChanged;
        event EventHandler<MiscUtil.EventArgs<int>> FrameProcessed;

        void PublishPortrait(Portrait portrait);
        void PublishFaceMatchEvent(PersonOfInterestDetectionResult matchResult);
        void PublishSwitchMotionDetectorEvent();
        void PublishIsBusyEvent(bool isBuy);
        void PublishFrameProcessed(int milliSecondsUsed);
    }
}
