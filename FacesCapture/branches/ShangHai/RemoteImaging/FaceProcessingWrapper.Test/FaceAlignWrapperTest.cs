using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using OpenCvSharp;

namespace FaceProcessingWrapper.Test
{
    using FaceProcessingWrapper;

    [TestFixture]
    public class FaceAlignWrapperTest
    {
        private FaceAlignmentWrapper aligner;

        [SetUp]
        public void Setup()
        {
           aligner = new FaceAlignmentWrapper(@".\model.txt", @".\haarcascade_frontalface_alt2.xml", 68);
        }


        [Test]
        public void LibAlign()
        {
            var img = @"C:\faceLib\0001.jpg";
            var pt = img + ".pts";

            var ipl = IplImage.FromFile(img);
            var aligned = new IplImage(70, 70, BitDepth.U8, 1);
            var pts = ReadFeaturePoints(pt);

            aligner.LibFaceAlignment(ipl, aligned, pts);
            aligned.SaveImage("aligned.jpg");
        }

        private CvPoint[] ReadFeaturePoints(string pointFileName)
        {
            var points = new List<CvPoint>();

            using (var fileStream = System.IO.File.OpenText(pointFileName))
            {
                fileStream.ReadLine();
                fileStream.ReadLine();
                fileStream.ReadLine();

                for (int i = 0; i < 68; i++)
                {
                    var line = fileStream.ReadLine();
                    CvPoint point = ParseLine(line);
                    points.Add(point);
                }

            }

            return points.ToArray();

        }

        private CvPoint ParseLine(string line)
        {
            var splits = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(splits[0]);
            var y = int.Parse(splits[1]);
            return new CvPoint(x, y);
        }
    }
}
