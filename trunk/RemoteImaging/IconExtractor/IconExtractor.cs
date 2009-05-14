using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IconExtractor
{
    public static class IconExtractor
    {

        const string dllName = "Common_Grabber.dll";

        /// Return Type: void
        ///strFileName: char*
        [DllImport(dllName, CharSet=CharSet.Ansi)]
        public static extern  void AddInImage([InAttribute()]
                                              [MarshalAs(UnmanagedType.LPStr)] 
                                              string strFileName);

        
        /// Return Type: void
        ///dir: char*
        [DllImport(dllName, CharSet=CharSet.Ansi)]
        public static extern  void SetOutputDir([InAttribute()]
                                                [MarshalAs(UnmanagedType.LPStr)] 
                                                string dir);

        
        /// Return Type: char*
        [DllImport(dllName, CharSet=CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        private static extern string SelectBestImage();
    }
}
