using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NServiceBus;

namespace Damany.RemoteImaging.Net.Messages
{
    [Serializable]
    [Express]
    [TimeToBeReceived("00:00:30")]
    public class Portrait : IMessage
    {
        public System.Drawing.Image FaceImage { get; set; }
        public DateTime CaptureTime { get; set; }
    }
}
