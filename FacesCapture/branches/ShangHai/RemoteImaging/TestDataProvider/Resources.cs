using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace TestDataProvider
{
    public static class Data
    {
        public static IplImage GetFrame()
        {
            return IplImage.FromBitmap(Properties.Resources.frame).Clone();
        }

        public static IplImage ImageWithOneFace
        {
            get { return IplImage.FromBitmap(Properties.Resources.luobinFace).Clone(); }
        }

        public static IplImage FaceOfDengDong
        {
            get { return IplImage.FromBitmap(Properties.Resources.dengDongFace).Clone(); }
        }

        public static IplImage Face1OfXue
        {
            get { return IplImage.FromBitmap(Properties.Resources.xueXiaoLiFace1).Clone(); }
        }

        public static IplImage Face2OfXue
        {
            get { return IplImage.FromBitmap(Properties.Resources.xueXiaoLiFace2).Clone(); }
        }

        public static IplImage FaceOfShen
        {
            get { return IplImage.FromBitmap(Properties.Resources.FaceOfShen).Clone(); }
        }

        public static IplImage GetPortrait()
        {
            return IplImage.FromBitmap(Properties.Resources.portrait).Clone();
        }

    }
}
