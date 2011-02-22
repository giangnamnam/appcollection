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

        void PublishPortrait(Portrait portrait);
        void PublishFaceMatchEvent(PersonOfInterestDetectionResult matchResult);
        void PublishSwitchMotionDetectorEvent();
        void PublishIsBusyEvent(bool isBuy);
    }
}
