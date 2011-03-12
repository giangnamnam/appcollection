using System;
using System.Collections.Concurrent;
using Damany.Imaging.PlugIns;
using MiscUtil;

namespace Damany.Imaging.Common
{
    public class EventAggregator : IEventAggregator
    {
        public event EventHandler<EventArgs<Portrait>> PortraitFound;
        public event EventHandler SwitchMotionDetector;
        public event EventHandler<EventArgs<bool>> IsBusyChanged;
        public event EventHandler<EventArgs<Tuple<int, int>>> FrameProcessed;

        public void InvokeFramProcessed(int milliSecondsUsed, int queueElementCount)
        {
            EventHandler<EventArgs<Tuple<int, int>>> handler = FrameProcessed;
            if (handler != null) handler(this, new EventArgs<Tuple<int, int>>( new Tuple<int, int>(milliSecondsUsed, queueElementCount)) );
        }

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


        public void PublishSwitchMotionDetectorEvent()
        {
            this.InvokeSwitchMotionDetectorEvent(EventArgs.Empty);
        }

        public void PublishIsBusyEvent(bool isBuy)
        {
            this.RaiseIsBusyChanged(isBuy);
        }

        public void PublishFrameProcessed(int milliSecondsUsed, int queueElementSize)
        {
            InvokeFramProcessed(milliSecondsUsed, queueElementSize);
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


    }
}