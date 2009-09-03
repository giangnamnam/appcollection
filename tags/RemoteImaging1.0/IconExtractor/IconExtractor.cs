using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcess
{
    public class IconExtractor : IIconExtractor
    {

        public static IconExtractor Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new IconExtractor();
                }
                return instance;
            }
        }


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

        #region IIconExtractor Members

        public void SetFaceParas(int iMinFace, double dFaceChangeRatio)
        {
            NativeIconExtractor.SetFaceParas(iMinFace, dFaceChangeRatio);
        }

        public void SetExRatio(double topExRatio,
            double bottomExRatio,
            double leftExRatio,
            double rightExRatio)
        {
            NativeIconExtractor.SetExRatio(topExRatio, bottomExRatio, leftExRatio, rightExRatio);
        }

        public void SetDwSmpRatio(double dRatio)
        {
            NativeIconExtractor.SetDwSmpRatio(dRatio);
        }

        private IconExtractor() { }

        private static IconExtractor instance;

        #endregion

        #region IIconExtractor Members


        public void SetROI(int x, int y, int width, int height)
        {
            NativeIconExtractor.SetROI(x, y, width, height);
        }

        #endregion

        #region IIconExtractor Members


        public void SetLightMode(int mode)
        {
            NativeIconExtractor.SetLightMode(mode);
        }

        #endregion
    }
}
