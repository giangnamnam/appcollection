using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using ImageProcess;

namespace MotionDetect
{
    public static class MotionDetect
    {
        const string dllName = "PreProcess.dll";

        [System.Runtime.InteropServices.DllImportAttribute(dllName, EntryPoint = "PreProcessFrame")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PreProcessFrame(Frame frame, ref Frame lastFrame);

    }
}
