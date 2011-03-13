using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FaceProcessingWrapper;

namespace RemoteImaging
{
    public static class MiscHelper
    {
        public static FaceRecoWrapper CreateFaceComparer()
        {
            var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model.txt");
            var classifierPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_alt2.xml");
            var comparer = FaceRecoWrapper.FromModel(modelPath, classifierPath);

            return comparer;
        }
    }
}
