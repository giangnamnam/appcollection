using System;
using System.Runtime.InteropServices;

namespace Yaguang.VJK3G.IO
{
    /// <summary>
    /// Summary description for AC6651.
    /// </summary>
    ///
 

    public class AC6651
    {

        public static readonly IntPtr InvalidHandle = (IntPtr)(-1);

        public const long Succeed = 0;


        //device operation
        [DllImport("ac6651.dll", EntryPoint="AC6651_OpenDevice")]
        public static extern IntPtr OpenDevice(int deviceNum);

        [DllImport("ac6651.dll", EntryPoint="AC6651_CloseDevice")]
        public static extern int  CloseDevice(IntPtr handle);



        //IO
        [DllImport("ac6651.dll", EntryPoint="AC6651_DI")]
        public static extern int DI(IntPtr handle, int IONum);

        [DllImport("ac6651.dll", EntryPoint="AC6651_DO")]
        public static extern int DO(IntPtr handle, int IONum, int IOData);

        [DllImport("ac6651.dll", EntryPoint="AC6651_SetIOMode")]
        public static extern int SetIOMode(IntPtr handle, int IOMode);

        [DllImport("ac6651.dll", EntryPoint="AC6651_GetIOMode")]
        public static extern int GetIOMode(IntPtr handle);

        
        
        //timer
        [DllImport("ac6651.dll", EntryPoint="AC6651_SetTData")]
        public static extern int SetTimerData(IntPtr handle, int channel, int data);

        [DllImport("ac6651.dll", EntryPoint="AC6651_GetTData")]
        public static extern int GetTimerData(IntPtr handle, int channel);

        [DllImport("ac6651.dll", EntryPoint="AC6651_GetTST")]
        public static extern int GetTimerState(IntPtr handle);

        [DllImport("ac6651.dll", EntryPoint="AC6651_SetTMode")]
        public static extern int SetTimerMode(IntPtr handle, int channel, int mode);

    }
}

