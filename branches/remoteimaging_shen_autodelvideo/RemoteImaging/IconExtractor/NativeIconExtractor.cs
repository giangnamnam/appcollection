using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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
        public static extern void SetROI(int x, int y, int width, int height);



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
        public static extern void SetFaceParas(int iMinFace, double dFaceChangeRatio);


        /// Return Type: void
        ///topExRatio: double
        ///bottomExRatio: double
        ///leftExRatio: double
        ///rightExRatio: double
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetExRatio(double topExRatio,
            double bottomExRatio,
            double leftExRatio,
            double rightExRatio);


        /// Return Type: void
        ///dRatio: double
        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetDwSmpRatio(double dRatio);


        [DllImport(FaceSearchDll, CharSet = CharSet.Ansi)]
        public static extern void SetLightMode(int iMode);


        [DllImport(FaceSearchDll, EntryPoint = "ReleaseMem")]
        public static extern void ReleaseMem();


        /// Return Type: void
        ///frame: Frame*
        [DllImport(FaceSearchDll, EntryPoint = "AddInFrame")]
        public static extern void AddInFrame(Frame frame);


        /// Return Type: int
        ///targets: Target**
        [DllImport(FaceSearchDll, EntryPoint = "SearchFaces")]
        public static extern int SearchFaces(ref System.IntPtr targets);

    }
}
