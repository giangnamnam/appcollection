using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.RealtimeDisplay
{
    public interface IImageScreen
    {
        int SelectedCameraID
        {
            get;
            set;
        }

        ImageUploadEventArgs SelectedImage
        {
            get;
            set;
        }

        ImageUploadEventArgs BigImage
        {
            get;
            set;
        }

        IImageScreenObserver Observer
        {
            get;
            set;
        }
    
        void ShowImages(ImageDetail[] images);
    }
}
