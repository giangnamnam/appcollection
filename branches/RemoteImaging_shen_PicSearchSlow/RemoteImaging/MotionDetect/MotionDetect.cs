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

        [DllImport(dllName, EntryPoint = "PreProcessFrame")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool PreProcessFrame(Frame frame, ref Frame lastFrame);

        [DllImport(dllName)]
        public static extern void SetDrawRect(bool draw);

        [DllImport(dllName)]
        public static extern void SetAlarmArea(int leftX, int leftY, int rightX, int rightY, bool draw);

        [DllImport(dllName)]
        public static extern void SetRectThr(int fCount, int gCount);

    }
}
