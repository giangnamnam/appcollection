using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSZN.Component;
using ImageProcess;
using OpenCvSharp;

namespace RemoteImaging
{
    public class FrameCapturer
    {
        private System.Timers.Timer timer;
        public byte CameraID { get; set; }

        //Frames per second
        public int Frenquency { get; set; }

        public FrameCapturer(ICamera c, byte cameraID)
        {
            if (c == null) throw new ArgumentNullException("ICamera");

            this.Camera = c;
            this.CameraID = cameraID;
            this.Frenquency = 2;
        }

        public event EventHandler FrameCaptured;

        public ManagedFrame CurrentFrame { get; set; }

        public ICamera Camera { get; set; }

        public void StartCapture()
        {
            if (timer.Enabled) throw new System.InvalidOperationException("still running");

            timer = new System.Timers.Timer();
            timer.Interval = 1000 / this.Frenquency;

            timer.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            timer.Enabled = true;
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DoCapture();
        }


        private void DoCapture()
        {
            System.Drawing.Image img = Camera.CaptureImage();

            ManagedFrame mf = new ManagedFrame();
            mf.CameraID = CameraID;
            mf.Ipl = BitmapConverter.ToIplImage((System.Drawing.Bitmap) img);
            mf.Ipl.IsEnabledDispose = false;
            mf.TimeStamp = DateTime.Now;

            this.CurrentFrame = mf;

            this.OnFrameCaptured();
        }

        protected virtual void OnFrameCaptured()
        {
            if (FrameCaptured != null)
            {
                FrameCaptured(this, EventArgs.Empty);
            }
        }

    }
}
