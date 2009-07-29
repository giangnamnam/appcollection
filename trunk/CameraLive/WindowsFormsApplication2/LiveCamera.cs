using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Net;
using System.IO;
using System.Web;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;
using OpenCvSharp;


namespace WindowsFormsApplication2
{
    public partial class LiveCamera : Form
    {
        public LiveCamera()
        {
            InitializeComponent();
            Thread t = new Thread(this.SearchFace);
            t.IsBackground = true;
            t.Start();


        }

        public string bagName
        {
            get;
            set;
        }

        private void MethodA()
        {
            Console.WriteLine("methoda is called");
        }

        private void MethodB()
        {
            Console.WriteLine("methodb is called");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config =
                ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);

            ConfigurationSectionGroupCollection groups =
                config.SectionGroups;

            //             ConfigurationSectionGroup userSettings = groups["userSettings"];


            foreach (ConfigurationSectionGroup group
                in groups)
            {
                if (!group.IsDeclared)
                {
                    continue;
                }

                foreach (ConfigurationSection sec in group.Sections)
                {
                    ClientSettingsSection clientSec
                        = sec as ClientSettingsSection;
                    Console.WriteLine("Group {0}", clientSec);

                    foreach (SettingElement setting in clientSec.Settings)
                    {
                        Console.WriteLine("Name: {0}, Value: {1}",
                            setting.Name, setting.Value.ValueXml.InnerText);

                    }

                    SettingElement newElement =
                        new SettingElement();
                    newElement.Name = "benny";
                    newElement.SerializeAs = SettingsSerializeAs.String;

                    clientSec.Settings.Add(newElement);

                }
            }

            config.Save();


        }

        private void Form1_MouseClick(object sender)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cameraIP.Text = sanyoNetCamera1.IPAddress;
            UpdateFrameRate();

        }


        void AskForOpinion()
        {
            Action d = AskForOpinionInternal;
            this.Invoke(d);
        }

        void AskForOpinionInternal()
        {
            MessageBox.Show(this, "hello");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            AskForOpinion();

            MessageBox.Show("hello 2");

        }

        private void button2_Click(object sender, EventArgs e)
        {


            Process p = Process.Start("mspaint.exe");

            System.Threading.Thread.Sleep(5000);

            p.Kill();



        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Hello There");
        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {

        }

        private void Play_Click(object sender, EventArgs e)
        {


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            return;

            using (LinearGradientBrush lb1 =
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(this.ClientRectangle.Width, this.ClientRectangle.Height),
                    Color.LightSteelBlue,
                    Color.CornflowerBlue))
            {
                using (LinearGradientBrush lb2 =
                    new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(this.ClientRectangle.Width, this.ClientRectangle.Height),
                    Color.LightSteelBlue, Color.CornflowerBlue
                    ))
                {
                    ControlPaint.DrawCaptionButton(e.Graphics,
                        this.ClientRectangle,
                        CaptionButton.Close,
                        ButtonState.Normal);
                }

            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //this.axCamImgCtrl1.ImageType =
            //this.axCamImgCtrl1.PlayImgStart(0, 0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
        }



        private DateTime lastImageTime = DateTime.Now;
        CookieContainer container = null;

        private void button2_Click_1(object sender, EventArgs e)
        {
            sanyoNetCamera1.Connect();

            //sanyoNetCamera1.Init();

            this.trackBar1.Enabled = true;
            this.timer1.Enabled = true;


        }

        int i = 0;


        unsafe private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] image = sanyoNetCamera1.GetImage();

            MemoryStream memStream = new MemoryStream(image);
            Bitmap bmp = (Bitmap)Image.FromStream(memStream);


            if (this.pictureBox1.Image != null)
            {
                //this.pictureBox1.Image.Dispose();
            }

            if (this.live.Checked)
            {
                this.pictureBox1.Image = bmp;
            }

            //lastImageTime = time;

            Frame f = new Frame();
            f.timeStamp = 0;
            f.searchRect = IntPtr.Zero;

            System.Diagnostics.Debug.WriteLine("before convert");

            try
            {
                IplImage ipl = BitmapConverter.ToIplImage(bmp);
                ipl.IsEnabledDispose = false;
                f.image = ipl.CvPtr;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex.Message);
                this.timer1.Enabled = false;
            }


            System.Diagnostics.Debug.WriteLine("after convert");

            bool groupCaptured = NativeMethods.PreProcessFrame(ref f);
            if (groupCaptured)
            {
                IntPtr frames = IntPtr.Zero;
                int count = NativeMethods.GetGroupedFrames(ref frames);
                Console.WriteLine(count);
                if (count <= 0) return;
                Frame* pFrame = (Frame*)frames;

                Debug.WriteLine("frame: " + frames.ToString());

                FrameArray frameGroup = new FrameArray();
                frameGroup.count = count;
                frameGroup.frames = pFrame;

                lock (locker)
                {
                    //System.Diagnostics.Debug.WriteLine(string.Format("enqueue: {0:x}", f.image));
                    frameQueue.Enqueue(frameGroup);
                    go.Set();
                }

            }


        }

        unsafe void SearchFace()
        {
            while (true)
            {
                FrameArray frames = null;
                lock (locker)
                {
                    if (frameQueue.Count > 0)
                    {
                        frames = frameQueue.Dequeue();
                    }
                }

                if (frames != null)
                {
                    for (int i = 0; i < frames.count; i++)
                    {
                        Frame f = frames.frames[i];

                        System.Diagnostics.Debug.WriteLine(string.Format("dequeue: {0:x}", f.image));

                        IplImage ipl = new IplImage(f.image);
                        ipl.IsEnabledDispose = false;

                        System.Diagnostics.Debug.WriteLine(ipl);

                        NativeMethods.AddInFrame(ref f);


                        CvRect* rect = (CvRect*)f.searchRect;
                        ipl.DrawRect(rect->x, rect->y, rect->x + rect->width, rect->y + rect->height, CvColor.Yellow, 3);

                        try
                        {
                            System.Diagnostics.Debug.WriteLine("before");
                            pictureFiltered.Image = ipl.ToBitmap();
                            System.Diagnostics.Debug.WriteLine("after");
                        }
                        catch (System.Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }


                        //this.timer1.Enabled = false;

                    }

                    IntPtr target = IntPtr.Zero;

                    int count = NativeMethods.SearchFaces(ref target);
                    Target* pTarget = (Target*)target;

                    for (int i = 0; i < count; i++)
                    {
                        Target face = pTarget[i];
                        for (int j = 0; j < face.FaceCount; ++j)
                        {
                            IntPtr* f = ((IntPtr*)(face.FaceData)) + j;
                            IplImage ipl = new IplImage(*f);
                            ipl.IsEnabledDispose = false;

                            Bitmap faceBmp = ipl.ToBitmap();
                            pictureFace.Image = faceBmp;

                        }
                    }


                }
                else
                    go.WaitOne();
            }
        }




        private void AXConnect_Click(object sender, EventArgs e)
        {
            //this.axCamImgCtrl2.ImageFileURL = @"liveimg.cgi";
            //this.axCamImgCtrl2.ImageType = @"JPEG";
            //this.axCamImgCtrl2.CameraModel = 1;
            //this.axCamImgCtrl2.CtlLocation = @"http://192.168.0.2";
            //this.axCamImgCtrl2.uid = "admin";
            //this.axCamImgCtrl2.pwd = "admin";
            //this.axCamImgCtrl2.RecordingFolderPath
            //    = @"d:\ImageOutput\02";
            //this.axCamImgCtrl2.RecordingFormat = 0;
            //this.axCamImgCtrl2.UniIP = this.axCamImgCtrl2.CtlLocation;
            //this.axCamImgCtrl2.UnicastPort = 3939;
            //this.axCamImgCtrl2.ComType = 0;

            //this.axCamImgCtrl2.CamImgCtrlStart();
            //this.axCamImgCtrl2.CamImgRecStart();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            uint len = 1024 * 1024 * 10;
            byte[] buff = new byte[len];
            //IntPtr buff = Marshal.AllocHGlobal((int)len);
            //object o = new object();
            uint actualLen = 0;
            //this.axCamImgCtrl1.BMPImageGet(buff, (uint)len, ref actualLen);
            byte be;
            uint maxLen = uint.MaxValue;

            byte[] bBuff = new byte[actualLen];

            //         Marshal.Copy(buff, bBuff, 0, bBuff.Length);
            //Marshal.FreeHGlobal(buff);


            //this.axCamImgCtrl2.ImageGet(buff, len, out actualLen);
            //File.WriteAllBytes("abc.jpg", buff);

        }

        private void UpdateFrameRate()
        {
            framesPerSec.Text = string.Format("{0} 帧/秒", trackBar1.Value);
            this.timer1.Interval = 1000 / trackBar1.Value;

        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            UpdateFrameRate();
            this.timer1.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void cameraIP_TextChanged(object sender, EventArgs e)
        {
            this.sanyoNetCamera1.IPAddress = this.cameraIP.Text;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(@"D:\pictures in hall");
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file);
                if (ext != ".jpg") continue;

                Bitmap img1 = (Bitmap)Bitmap.FromFile(file);

                IplImage ipl = BitmapConverter.ToIplImage(img1);

                IplImage ipl1 = new IplImage(ipl.CvPtr);

                Bitmap bmp = ipl1.ToBitmap();
                this.pictureFace.Image = bmp;

                ipl1.SaveImage(@"d:\iplimg.jpg");

                return;


                //                 byte[] data = File.ReadAllBytes(file);
                //                 Frame f = new Frame();
                //                 f.data = IntPtr.Zero;// Marshal.AllocCoTaskMem(data.Length);
                //                 //Marshal.Copy(data, 0, f.data, data.Length);
                //                 f.dataLength = 0;// data.Length;
                //                 f.image = IntPtr.Zero;
                //                 f.timeStamp = 0;
                //                 f.searchRect = IntPtr.Zero;
                //                 f.fileName = Marshal.StringToCoTaskMemAnsi(file);
                // 
                //                 bool group = NativeMethods.PreProcessFrame(ref f);


            }
        }

        object locker = new object();
        Queue<FrameArray> frameQueue = new Queue<FrameArray>();
        AutoResetEvent go = new AutoResetEvent(false);
    }

    unsafe class FrameArray
    {
        public int count;
        public Frame* frames;
    }



    public delegate void GetAString();
}
