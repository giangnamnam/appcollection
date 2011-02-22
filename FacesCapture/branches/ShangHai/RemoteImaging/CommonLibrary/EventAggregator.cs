using System;
using System.Collections.Concurrent;
using Damany.Imaging.PlugIns;
using MiscUtil;

namespace Damany.Imaging.Common
{
    public class EventAggregator : IEventAggregator
    {
        public event EventHandler<EventArgs<Portrait>> PortraitFound;
        public event EventHandler<EventArgs<PersonOfInterestDetectionResult>> FaceMatchFound;
        public event EventHandler SwitchMotionDetector;
        public event EventHandler<EventArgs<bool>> IsBusyChanged;

        public void RaiseIsBusyChanged(bool isBusy)
        {
            var e = new MiscUtil.EventArgs<bool>(isBusy);
            EventHandler<EventArgs<bool>> handler = IsBusyChanged;
            if (handler != null) handler(this, e);
        }

        public void InvokeSwitchMotionDetectorEvent(EventArgs e)
        {
            EventHandler handler = SwitchMotionDetector;
            if (handler != null) handler(this, e);
        }


        public void PublishFaceMatchEvent(PersonOfInterestDetectionResult matchResult)
        {
            var e = new EventArgs<PersonOfInterestDetectionResult>(matchResult);
            this.InvokeFaceMatchFound(e);
        }

        public void PublishSwitchMotionDetectorEvent()
        {
            this.InvokeSwitchMotionDetectorEvent(EventArgs.Empty);
        }

        public void PublishIsBusyEvent(bool isBuy)
        {
            this.RaiseIsBusyChanged(isBuy);
        }

        public void PublishPortrait(Portrait portrait)
        {
            var e = new EventArgs<Portrait>(portrait);
            this.InvokePortraitFound(e);
        }

        private void InvokePortraitFound(EventArgs<Portrait> e)
        {
            EventHandler<EventArgs<Portrait>> handler = PortraitFound;
            if (handler != null) handler(this, e);
        }

        private void InvokeFaceMatchFound(EventArgs<PersonOfInterestDetectionResult> e)
        {
            EventHandler<EventArgs<PersonOfInterestDetectionResult>> handler = FaceMatchFound;
            if (handler != null) handler(this, e);
        }


    }
}