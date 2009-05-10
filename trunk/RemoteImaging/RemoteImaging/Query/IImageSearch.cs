using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    public interface IImageSearch
    {
        string[] SearchImages(ImageDirSys imageDir);
        void SelectedBestImageChanged(string selectedImageName);
                
    }
}
