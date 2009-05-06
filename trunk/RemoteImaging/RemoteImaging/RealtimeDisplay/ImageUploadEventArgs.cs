using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.RealtimeDisplay
{
    public class ImageUploadEventArgs
    {
        public int CameraID { get; set; }

        public ImageDetail[] Images { get; set; }
    }
}
