using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Damany.Imaging.Extensions;
using OpenCvSharp;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class FaceSearcherTest
    {
        [Test]
        public void Test()
        {
            var faceSearcher = new FaceSearchWrapper.FaceSearch();
            var frame = TestDataProvider.Data.ImageWithOneFace;
            var rect = new OpenCvSharp.CvRect(0, 0, frame.Width, frame.Height);
            var faces = faceSearcher.SearchFace(frame, rect);
            Assert.AreEqual(faces.Length, 1);
        }
    }
}
