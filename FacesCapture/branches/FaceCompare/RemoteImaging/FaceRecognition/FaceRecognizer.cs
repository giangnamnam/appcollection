using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FaceRecognition
{
    public static class FaceRecognizer
    {
        const string DllName = "FaceRecognize.dll";

        [DllImport(DllName, EntryPoint = "InitData")]
        public static extern void InitData(int sampleCount, int imgLen, int eigenNum);


        [DllImport(DllName, EntryPoint = "FaceRecognition")]
        public static extern void FaceRecognition(
            ref float currentFace, 
            int sampleCount, 
            ref RecognizeResult similarity, 
            int imgLen, 
            int eigenNum);
    }
}
