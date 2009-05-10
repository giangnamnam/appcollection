using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.RealtimeDisplay
{
    public interface IIconExtractor
    {
        string ExtractIcons(string bigImage, string destFolder);
        void Initialize();
    }
}
