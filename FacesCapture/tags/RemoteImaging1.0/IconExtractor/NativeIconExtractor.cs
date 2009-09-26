using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ImageProcess
{
    public static class NativeIconExtractor
    {

        const string dllName = "FaceSelDll.dll";



        /// Return Type: void
        ///x: int
        ///y: int
        ///width: int
        ///height: int
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetROI(int x, int y, int width, int height);


        /// Return Type: void
        ///strFileName: char*
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void AddInImage([InAttribute()]
                                              [MarshalAs(UnmanagedType.LPStr)] 
                                              string strFileName);


        /// Return Type: void
        ///dir: char*
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetOutputDir([InAttribute()]
                                                [MarshalAs(UnmanagedType.LPStr)] 
                                                string dir);


        /// Return Type: char*
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string SelectBestImage();



        /// Return Type: void
        ///iMinFace: int
        ///dFaceChangeRatio: double
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetFaceParas(int iMinFace, double dFaceChangeRatio);


        /// Return Type: void
        ///topExRatio: double
        ///bottomExRatio: double
        ///leftExRatio: double
        ///rightExRatio: double
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetExRatio(double topExRatio,
            double bottomExRatio,
            double leftExRatio,
            double rightExRatio);


        /// Return Type: void
        ///dRatio: double
        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetDwSmpRatio(double dRatio);


        [DllImport(dllName, CharSet = CharSet.Ansi)]
        public static extern void SetLightMode(int iMode);
    }
}
