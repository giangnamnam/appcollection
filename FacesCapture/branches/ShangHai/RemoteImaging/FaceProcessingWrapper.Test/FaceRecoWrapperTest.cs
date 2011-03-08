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

            var face1 = TestDataProvider.Data.Face1OfXue;
            var face2 = TestDataProvider.Data.Face1OfXue;

            var fp1 = new FaceSpecification();
            var suc1 = comparer.CalcFeature(face2, fp1);

            var fp2 = new FaceSpecification();
            var suc2 = comparer.CalcFeature(face2, fp2);

            var similar = comparer.CmpFace(fp1, fp2);

            Assert.IsTrue(similar == 100.0);
        }
    }
}
