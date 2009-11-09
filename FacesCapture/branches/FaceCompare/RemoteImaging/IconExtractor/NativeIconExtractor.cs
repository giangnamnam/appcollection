using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace ImageProcess
{
    public static class NativeIconExtractor
    {

#if DEBUG
        const string FaceSearchDll = "FaceSelDllD.dll";
#else
        const string FaceSearchDll = "FaceSelDll.dll";
#endif

        /// Return Type: void
        ///x: int
        ///y: int
        ///width: int
        ///height: int
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetROI(int x, int y, int width, int height, int idx);



        /// Return Type: void
        ///dir: char*
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetOutputDir([InAttribute()]
                                                [MarshalAs(UnmanagedType.LPStr)] 
                                                string dir);


        /// Return Type: void
        ///iMinFace: int
        ///dFaceChangeRatio: double
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetFaceParas(int iMinFace, double dFaceChangeRatio, int idx);


        /// Return Type: void
        ///topExRatio: double
        ///bottomExRatio: double
        ///leftExRatio: double
        ///rightExRatio: double
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetExRatio(double topExRatio,
            double bottomExRatio,
            double leftExRatio,
            double rightExRatio,
            int idx);


        /// Return Type: void
        ///dRatio: double
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetDwSmpRatio(double dRatio, int idx);


        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetLightMode(int iMode, int idx);


        [DllImport(FaceSearchDll, EntryPoint = "ReleaseMem")]
        public static extern void ReleaseMem(int idx);


        /// Return Type: void
        ///frame: Frame*
        [DllImport(FaceSearchDll, EntryPoint = "AddInFrame")]
        public static extern void AddInFrame(Frame frame, int idx);


        /// Return Type: int
        ///targets: Target**
        [DllImport(FaceSearchDll, EntryPoint = "SearchFaces")]
        public static extern int SearchFaces(ref System.IntPtr targets, int idx);


        [DllImport(FaceSearchDll, EntryPoint = "FaceImagePreprocess")]
        public static extern void NormalizeFace(
            System.IntPtr faceIplPtr,
            ref System.IntPtr normalizedFace,
            CvRect roi,
            int idx);


        public static float[] ResizeIplTo(IplImage Face, int width, int height, BitDepth bitDepth, int channel)
        {
            IplImage smallerFace = new IplImage(new OpenCvSharp.CvSize(width, height), bitDepth, channel);

            Face.Resize(smallerFace, Interpolation.Linear);

            unsafe
            {
                byte* smallFaceData = smallerFace.ImageDataPtr;
                float[] currentFace = new float[width * height * 8 * channel];
                for (int i = 0; i < smallerFace.Height; i++)
                {
                    for (int j = 0; j < smallerFace.Width; j++)
                    {
                        currentFace[i * smallerFace.WidthStep + j] = 
                            (float)smallFaceData[i * smallerFace.WidthStep + j];
                    }
                }

                smallerFace.Dispose();

                return currentFace;
            }
        }

    }
}
