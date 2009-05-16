using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IconExtractor
{
    public interface IIconExtractor
    {
        void AddInImage(string strFileName);
        void SetOutputDir(string dir);
        string SelectBestImage();
    }
}
