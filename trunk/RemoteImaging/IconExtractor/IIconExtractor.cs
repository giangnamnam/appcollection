using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcess
{
    public interface IIconExtractor
    {
        void AddInImage(string strFileName);
        void SetOutputDir(string dir);
        string SelectBestImage();
    }
}
