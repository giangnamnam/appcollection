using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;


namespace Damany.Imaging.Common
{
    public interface IEventAggregator
    {

        event EventHandler<MiscUtil.EventArgs<Portrait>> PortraitFound;
        event EventHandler SwitchMotionDetector;
        event EventHandler<MiscUtil.EventArgs<bool>> IsBusyChanged;
        event EventHandler<MiscUtil.EventArgs<Tuple<int, int>>> FrameProcessed;

        void PublishPortrait(Portrait portrait);
        void PublishSwitchMotionDetectorEvent();
        void PublishIsBusyEvent(bool isBuy);
        void PublishFrameProcessed(int milliSecondsUsed, int queueElementCount);
    }
}
