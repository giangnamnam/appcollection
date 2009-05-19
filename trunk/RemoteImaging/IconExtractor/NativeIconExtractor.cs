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
    }
}
