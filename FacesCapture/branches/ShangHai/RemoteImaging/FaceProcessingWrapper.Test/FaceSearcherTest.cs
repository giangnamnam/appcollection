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


            int count = 0;
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            foreach (var file in System.IO.Directory.EnumerateFiles(@"F:\测试图片\Lb", "*.jpg"))
            {
                var img = IplImage.FromFile(file);
                var rect = new CvRect(0, 0, img.Width, img.Height);
                var faces = faceSearcher.SearchFace(img, rect);
                count++;
            }

            var msPerPic = timer.ElapsedMilliseconds/count;
            System.Diagnostics.Debug.WriteLine("millisecond per picture: " + msPerPic);

            
        }
    }
}
