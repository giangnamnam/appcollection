using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FaceRecoWrapperTest
    {
        [Test]
        public void Test()
        {
            //
            // TODO: Add test logic here
            //
            var comparer = FaceRecoWrapper.FromModel(@"model.txt",
                                                     @"haarcascade_frontalface_alt2.xml");


            var fpXue1 = new FaceSpecification();
            var sucXue1 = comparer.CalcFeature(TestDataProvider.Data.GetImage1(), fpXue1);

            var fp1Clone = new FaceSpecification();
            var bmp = TestDataProvider.Data.Face1OfXue.ToBitmap();
            var bmpClone = OpenCvSharp.IplImage.FromBitmap(bmp);
            var suc2 = comparer.CalcFeature( TestDataProvider.Data.GetImage2() , fp1Clone);
            var similar = comparer.CmpFace(fpXue1, fp1Clone);
            Assert.IsTrue(similar == 100.0);

            //var fpXue2 = new FaceSpecification();
            //var sucXue2 = comparer.CalcFeature(TestDataProvider.Data.Face2OfXue, fpXue2);
            //var simXue1Xue2 = comparer.CmpFace(fpXue1, fpXue2);

            //var fpDeng = new FaceSpecification();
            //var sucDeng = comparer.CalcFeature(TestDataProvider.Data.FaceOfDengDong, fpDeng);
            ////var simXue1Deng = comparer.CmpFace(fp1, fpDeng);

            //var fpGirl = new FaceSpecification();
            //var sucGirl = comparer.CalcFeature(TestDataProvider.Data.GetPortrait(), fpGirl);

            //var simXue1Girl = comparer.CmpFace(fpXue1, fpGirl);
            //var simXue2Girl = comparer.CmpFace(fpXue2, fpGirl);


            //var fpShen = new FaceSpecification();
            //var sucShen = comparer.CalcFeature(TestDataProvider.Data.FaceOfShen, fpShen);

            //var simXue1Shen = comparer.CmpFace(fpXue1, fpShen);
            //Assert.IsTrue(simXue1Shen == 0);


        }
    }
}
