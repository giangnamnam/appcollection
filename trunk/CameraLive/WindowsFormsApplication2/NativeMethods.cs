using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct CvRect
    {
        public int x;
        public int y;
        public int width;
        public int height;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Frame
    {
        public int dataLen;

        public System.IntPtr data;

        /// IplImage*
        public System.IntPtr image;

        /// cvRect*
        public System.IntPtr searchRect;

        /// int
        public int timeStamp;

        public System.IntPtr fileName;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct ByteArray
    {

        /// byte*
        public System.IntPtr bytes;

        /// int
        public int length;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Target
    {

        /// Frame*
        public System.IntPtr BaseFrame;

        /// int
        public int FaceCount;

        /// ByteArray*
        public System.IntPtr FaceData;
    }

    public partial class NativeMethods
    {
        const string PreProcessDLLName = "PreProcess.dll";
        const string FaceDetectDll = "FaceSelDllD.dll";
        /// Return Type: boolean
        ///frame: Frame*
        [System.Runtime.InteropServices.DllImportAttribute(PreProcessDLLName, EntryPoint = "PreProcessFrame")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)]
        public static extern bool PreProcessFrame(ref Frame frame);


        /// Return Type: int
        ///frames: Frame**
        [System.Runtime.InteropServices.DllImportAttribute(PreProcessDLLName, EntryPoint = "GetGroupedFrames")]
        public static extern int GetGroupedFrames(ref System.IntPtr frames);



        /// Return Type: void
        ///frames: Frame*
        [System.Runtime.InteropServices.DllImportAttribute(FaceDetectDll, EntryPoint = "ReleaseMem")]
        public static extern void ReleaseMem();


        /// Return Type: void
        ///frame: Frame*
        [System.Runtime.InteropServices.DllImportAttribute(FaceDetectDll, EntryPoint = "AddInFrame")]
        public static extern void AddInFrame(ref Frame frame);


        /// Return Type: int
        ///targets: Target**
        [System.Runtime.InteropServices.DllImportAttribute(FaceDetectDll, EntryPoint = "SearchFaces")]
        public static extern int SearchFaces(ref System.IntPtr targets);

    }

}
