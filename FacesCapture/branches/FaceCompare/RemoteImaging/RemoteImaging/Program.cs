using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace RemoteImaging
{
    using RealtimeDisplay;

    static class Program
    {
        public static string directory;
        public static int ImageSampleCount = 2230;
        public static int ImageLen = 400;
        public static int EigenNum = 40;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG

            if (!Util.VerifyKey())
            {
                RegisterForm form = new RegisterForm();
                DialogResult res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    Application.Restart();
                }
                
                return;
            }
#endif

            if (argv.Length > 0)
            {
                directory = argv[0];
            }

            Application.Run(new MainForm());

        }

        static void watcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            DateTime time = args.Images[0].CaptureTime;
            string msg = string.Format("camID={0} count={1} time={2}", args.CameraID, args.Images.Length, time);
            System.Diagnostics.Debug.WriteLine(msg);
        }
    }
}
