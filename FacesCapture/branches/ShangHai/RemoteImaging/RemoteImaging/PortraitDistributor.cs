using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging
{
    class PortraitDistributor : IMyStartable
    {
        public NServiceBus.IBus Bus { get; set; }
        public Damany.Imaging.Common.IEventAggregator EventAggregator { get; set; }

        public void Start()
        {
            Damany.Imaging.Common.Mediator.Instance.RegisterHandler<Damany.Imaging.Common.Portrait>(HandlePortrait);
            EventAggregator.PortraitFound += new EventHandler<MiscUtil.EventArgs<Damany.Imaging.Common.Portrait>>(EventAggregator_PortraitFound);
        }

        void EventAggregator_PortraitFound(object sender, MiscUtil.EventArgs<Damany.Imaging.Common.Portrait> e)
        {
            var msg = new Damany.RemoteImaging.Net.Messages.Portrait();
            msg.FaceImage = e.Value.GetIpl().ToBitmap();
            msg.CaptureTime = e.Value.CapturedAt;

            if (Bus != null)
            {
                try
                {
                    Bus.Publish(msg);
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public void HandlePortrait(Damany.Imaging.Common.Portrait portrait)
        {

        }
    }
}
