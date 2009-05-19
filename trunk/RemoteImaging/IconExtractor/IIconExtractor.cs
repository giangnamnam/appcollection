using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageProcess
{
    public interface IIconExtractor
    {
        //取样参数
        void SetFaceParas(int iMinFace, double dFaceChangeRatio);
        void SetExRatio(double topExRatio,
            double bottomExRatio,
            double leftExRatio,
            double rightExRatio);
        void SetDwSmpRatio(double dRatio);

        //基本截取函数
        void AddInImage(string strFileName);
        void SetOutputDir(string dir);
        string SelectBestImage();
    }
}
