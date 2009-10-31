using System;
using System.Collections.Generic;
using System.IO;
using ImageProcessing;
using RemoteImaging.Core;
using JSZN.Component;
using MotionDetect;
using System.Drawing;
using OpenCvSharp;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;

namespace RemoteImaging.RealtimeDisplay
{
    public class Presenter : IImageScreenObserver
    {
        IImageScreen screen;
        ICamera camera;
        System.ComponentModel.BackgroundWorker worker;
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer videoFileCheckTimer = new System.Timers.Timer();

        Queue<Frame[]> framesQueue = new Queue<Frame[]>();
        Queue<Frame> motionFrames = new Queue<Frame>();
        Queue<Frame> rawFrames = new Queue<Frame>();

        object locker = new object();
        object rawFrameLocker = new object();
        object bgLocker = new object();
        object camLocker = new object();

        AutoResetEvent goSearch = new AutoResetEvent(false);
        AutoResetEvent goDetectMotion = new AutoResetEvent(false);

        Thread motionDetectThread = null;


        private IplImage _BackGround;
        public IplImage BackGround
        {
            get
            {
                return _BackGround;
            }
            set
            {
                _BackGround = value;
                value.IsEnabledDispose = false;
            }
        }

        public void UpdateBG()
        {
            byte[] imgData = null;
            lock (this.camLocker)
                imgData = this.camera.CaptureImageBytes();

            Image img = Image.FromStream(new MemoryStream(imgData));

            lock (this.bgLocker)
                this.BackGround = BitmapConverter.ToIplImage((Bitmap)img);

            img.Save("BG.jpg");
        }


        public void RemoteListener(LiveServer s)
        {
            if (this.ImageCaptured != null)
            {
                this.ImageCaptured -= s.ImageCaptured;
            }
        }



        /// <summary>
        /// Initializes a new instance of the Presenter class.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="uploadWatcher"></param>
        public Presenter(IImageScreen screen,
            ICamera camera)
        {
            this.screen = screen;
            this.camera = camera;

            motionDetectThread =
                Properties.Settings.Default.DetectMotion ?
                new Thread(this.DetectMotion) : new Thread(this.BypassDetectMotion);
            motionDetectThread.IsBackground = true;
            motionDetectThread.Name = "motion detect";


            this.screen.Observer = this;
            this.worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;

            this.timer.Interval = 1000 / int.Parse(Properties.Settings.Default.FPs);
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            videoFileCheckTimer.Interval = 1000 * 60;
            videoFileCheckTimer.Elapsed += new System.Timers.ElapsedEventHandler(videoFileCheckTimer_Elapsed);

            if (File.Exists("bg.jpg"))
                BackGround = OpenCvSharp.IplImage.FromFile(@"bg.jpg");
        }

        private static void DeleteVideoFileAt(DateTime time)
        {
            string m4vFile = FileSystemStorage.VideoFilePathNameAt(time, 2);
            if (File.Exists(m4vFile))
            {
                System.Diagnostics.Debug.WriteLine(m4vFile);
                File.Delete(m4vFile);
            }

            string idvFile = m4vFile.Replace(".m4v", ".idv");
            if (File.Exists(idvFile))
            {
                System.Diagnostics.Debug.WriteLine(idvFile);
                File.Delete(idvFile);
            }
        }


        void videoFileCheckTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime time = DateTime.Now.AddMinutes(-2);

            if (!FileSystemStorage.MotionImagesCapturedWhen(2, time))
                DeleteVideoFileAt(time);


        }

        public void StartServer(object serverPort)
        {
            TcpListener server = new TcpListener((int)serverPort);
            server.Start();

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                System.Diagnostics.Debug.WriteLine("accept connection:" + client.Client.RemoteEndPoint);

                LiveServer ls = new LiveServer(client, this);

                this.ImageCaptured += ls.ImageCaptured;
            }
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.QueryRawFrame();
        }


        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            this.SearchFace();

        }

        void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            ImageDetail[] iconImgs = e.Result as ImageDetail[];

            if (iconImgs.Length > 0)
            {
                if (screen.SelectedCamera.ID == -1
                || screen.SelectedCamera.ID == iconImgs[0].FromCamera)
                {
                    screen.ShowImages(iconImgs);
                }
            }


            if (framesQueue.Count > 0)
            {
                screen.ShowProgress = true;
                worker.RunWorkerAsync(framesQueue.Dequeue());
            }
            else
            {
                screen.ShowProgress = false;
            }
        }


        public event EventHandler<ImageCapturedEventArgs> ImageCaptured;


        private void QueryRawFrame()
        {
            byte[] image;

            lock (this.camLocker)
            {
                image = camera.CaptureImageBytes();
            }


            MemoryStream memStream = new MemoryStream(image);
            Bitmap bmp = null;
            try
            {
                bmp = (Bitmap)Image.FromStream(memStream);
            }
            catch (System.ArgumentException)//图片格式出错
            {
                return;
            }


            if (ImageCaptured != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs() { ImageCaptured = bmp };
                ImageCaptured(this, args);
            }



            Frame f = new Frame();
            f.timeStamp = DateTime.Now.Ticks;
            f.IplPtr = IntPtr.Zero;
            f.cameraID = 2;

            IplImage ipl = BitmapConverter.ToIplImage(bmp);
            ipl.IsEnabledDispose = false;
            f.IplPtr = ipl.CvPtr;


            lock (this.rawFrameLocker) rawFrames.Enqueue(f);

            goDetectMotion.Set();
        }


        private Frame GetNewFrame()
        {
            Frame newFrame = new Frame();

            lock (this.rawFrameLocker)
            {
                if (rawFrames.Count > 0)
                {
                    newFrame = rawFrames.Dequeue();
                }
            }
            return newFrame;
        }


        private static bool IsStaticFrame(Frame aFrame)
        {
            return aFrame.IplPtr != null
                    && (aFrame.searchRect.Width == 0 || aFrame.searchRect.Height == 0);
        }

        private void DetectMotion()
        {
            while (true)
            {
                Frame newFrame = GetNewFrame();
                if (newFrame.IplPtr != IntPtr.Zero)
                {
                    Frame frameToProcess = new Frame();

                    bool groupCaptured = MotionDetect.MotionDetecter.PreProcessFrame(newFrame, ref frameToProcess);

                    if (IsStaticFrame(frameToProcess))
                    {
                        Cv.Release(ref frameToProcess.IplPtr);
                    }
                    else
                    {
                        FileSystemStorage.SaveFrame(frameToProcess);
                        motionFrames.Enqueue(frameToProcess);
                    }

                    if (groupCaptured)
                    {
                        Frame[] frames = motionFrames.ToArray();
                        motionFrames.Clear();

                        if (frames.Length <= 0) continue;

                        lock (locker) framesQueue.Enqueue(frames);

                        goSearch.Set();

                    }

                }
                else
                    goDetectMotion.WaitOne();

            }
        }

        void BypassDetectMotion()
        {
            while (true)
            {
                Frame f = this.GetNewFrame();

                if (f.IplPtr != IntPtr.Zero)
                {
                    IplImage ipl = new IplImage(f.IplPtr);
                    ipl.IsEnabledDispose = false;
                    f.searchRect.Width = ipl.Width;
                    f.searchRect.Height = ipl.Height;

                    motionFrames.Enqueue(f);
                    FileSystemStorage.SaveFrame(f);

                    if (motionFrames.Count == 6)
                    {
                        Frame[] frames = motionFrames.ToArray();
                        motionFrames.Clear();
                        lock (locker) framesQueue.Enqueue(frames);
                        goSearch.Set();
                    }
                }
                else
                    goDetectMotion.WaitOne();

            }

        }

        public void Start()
        {

            this.timer.Enabled = false;
            this.timer.Enabled = true;

            videoFileCheckTimer.Enabled = true;


            if (!motionDetectThread.IsAlive)
            {
                motionDetectThread.Start();
            }

            if (!this.worker.IsBusy)
            {
                this.worker.RunWorkerAsync();
            }

            ThreadPool.QueueUserWorkItem(this.StartServer, 20000);
        }

        private string PrepareDestFolder(ImageDetail imgToProcess)
        {
            string parentOfBigPicFolder = Directory.GetParent(imgToProcess.ContainedBy).FullName;
            string destFolder = Path.Combine(parentOfBigPicFolder, Properties.Settings.Default.IconDirectoryName);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            return destFolder;
        }


        unsafe ImageDetail[] SaveImage(ManagedTarget[] targets)
        {
            IList<ImageDetail> imgs = new List<ImageDetail>();

            foreach (ManagedTarget t in targets)
            {
                Frame frame = t.BaseFrame;

                for (int j = 0; j < t.Faces.Length; ++j)
                {
                    string facePath = FileSystemStorage.PathForFaceImage(frame, j);
                    t.Faces[j].Img.SaveImage(facePath);
                    imgs.Add(ImageDetail.FromPath(facePath));
                }

            }

            ImageDetail[] details = new ImageDetail[imgs.Count];
            imgs.CopyTo(details, 0);

            return details;

        }


        unsafe void SearchFace()
        {
            while (true)
            {
                Frame[] frames = null;
                lock (locker)
                {
                    if (framesQueue.Count > 0)
                    {
                        frames = framesQueue.Dequeue();
                    }
                }

                if (frames != null)
                {
                    for (int i = 0; i < frames.Length; ++i)
                    {
                        NativeIconExtractor.AddInFrame(frames[i]);
                    }

                    IntPtr target = IntPtr.Zero;

                    int count = NativeIconExtractor.SearchFaces(ref target);
                    if (count > 0)
                    {
                        Target* pTarget = (Target*)target;

                        IList<ManagedTarget> targets = new List<ManagedTarget>();

                        int upLimit = count;

                        if (frames.Length > 1 && Properties.Settings.Default.DetectMotion
                            && Properties.Settings.Default.removeDuplicatedFace)
                        {
                            upLimit = Math.Min(count, Properties.Settings.Default.MaxDupFaces);
                        }

                        for (int i = 0; i < upLimit; i++)
                        {
                            Target faceWithFrame = pTarget[i];

                            IList<Face> facesList = new List<Face>();

                            for (int j = 0; j < faceWithFrame.FaceCount; ++j)
                            {

                                IntPtr* faceImgData = ((IntPtr*)(faceWithFrame.FaceData)) + j;
                                IplImage aFaceImg = new IplImage(*faceImgData);
                                aFaceImg.IsEnabledDispose = false;

                                CvRect* pFaceBounds = (CvRect*)faceWithFrame.CvRects;
                                CvRect faceRect = pFaceBounds[j];


                                Face aFace = new Face() { Bounds = faceRect, Img = aFaceImg };

                                if (Properties.Settings.Default.RecheckFace
                                    && BackGround != null)
                                {
                                    lock (this.bgLocker)
                                        if (BackGroundComparer.IsFace(aFace.Img.CvPtr, BackGround.CvPtr, faceRect))
                                            facesList.Add(aFace);
                                }
                                else
                                    facesList.Add(aFace);

                            }

                            Face[] faceArray = new Face[facesList.Count];
                            facesList.CopyTo(faceArray, 0);

                            ManagedTarget t = new ManagedTarget()
                            {
                                BaseFrame = faceWithFrame.BaseFrame,
                                Faces = faceArray
                            };

                            targets.Add(t);
                        }

                        ManagedTarget[] tgArr = new ManagedTarget[targets.Count];

                        targets.CopyTo(tgArr, 0);

                        ImageDetail[] imgs = this.SaveImage(tgArr);
                        this.screen.ShowImages(imgs);

                    }


                    NativeIconExtractor.ReleaseMem();

                    Array.ForEach(frames, f => Cv.Release(ref f.IplPtr));
                }
                else
                    goSearch.WaitOne();
            }
        }


        #region IImageScreenObserver Members

        public void SelectedCameraChanged()
        {
            throw new NotImplementedException();
        }



        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.Path))
            {
                string bigPicPathName = FileSystemStorage.BigImgPathForFace(img);
                ImageDetail bigImageDetail = ImageDetail.FromPath(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }
}
