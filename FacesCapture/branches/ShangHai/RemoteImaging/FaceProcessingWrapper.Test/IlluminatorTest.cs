using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;

namespace FaceProcessingWrapper.Test
{
    [TestFixture]
    public class IlluminatorTest
    {
        private IlluminationWrapper _wrapper;

        [SetUp]
        public void Setup()
        {
            var ipl = OpenCvSharp.IplImage.FromFile("ref.jpg");
            _wrapper = new IlluminationWrapper(ipl);
        }

        [Test]
        public void Test()
        {
            var toProcess = OpenCvSharp.IplImage.FromFile("inimg.jpg");
            var processed = (OpenCvSharp.IplImage) toProcess.Clone();

            _wrapper.Process(toProcess, processed);
        }
    }
}
