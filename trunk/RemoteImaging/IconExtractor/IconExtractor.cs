using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IconExtractor
{
    public class IconExtractor : IIconExtractor
    {
      
        #region IIconExtractor Members

        public void AddInImage(string strFileName)
        {
            NativeIconExtractor.AddInImage(strFileName);
        }

        public void SetOutputDir(string dir)
        {
            NativeIconExtractor.SetOutputDir(dir);
        }

        public string SelectBestImage()
        {
            return NativeIconExtractor.SelectBestImage();
        }

        #endregion
    }
}
