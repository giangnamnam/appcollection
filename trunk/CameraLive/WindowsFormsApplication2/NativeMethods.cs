using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using OpenCvSharp;

namespace WindowsFormsApplication2
{

    [StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Frame
    {
        /// IplImage*
        public System.IntPtr image;

        public CvRect searchRect;

        /// int
        public int timeStamp;

    }


    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct Target
    {

        /// Frame*
        public System.IntPtr BaseFrame;

        /// int
        public int FaceCount;

        /// IplImage*
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
        public static extern bool PreProcessFrame(Frame frame, ref Frame lastFrame);


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
