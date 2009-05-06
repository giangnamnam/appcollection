using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.RealtimeDisplay
{
    public interface IImageScreenObserver
    {
        void SelectedCameraIDChanged();

        void SelectedImageChanged();
    }
}
