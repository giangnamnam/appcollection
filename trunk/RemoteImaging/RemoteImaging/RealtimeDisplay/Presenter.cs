using System;
using System.Collections.Generic;
using System.IO;
using ImageProcess;
using RemoteImaging.Core;
using JSZN.Component;
using MotionDetect;
using System.Drawing;
using OpenCvSharp;
using System.Threading;
using System.Diagnostics;

namespace RemoteImaging.RealtimeDisplay
{
    public class Presenter : IImageScreenObserver
    {
        IImageScreen screen;
        ICamera camera;
        System.ComponentModel.BackgroundWorker worker;
        System.Timers.Timer timer = new System.Timers.Timer();

        Queue<Frame[]> framesQueue = new Queue<Frame[]>();
        Queue<Frame> motionFrames = new Queue<Frame>();
        Queue<Frame> rawFrames = new Queue<Frame>();

        object locker = new object();
        object rawFrameLocker = new object();

        AutoResetEvent goSearch = new AutoResetEvent(false);
        AutoResetEvent goDetectMotion = new AutoResetEvent(false);

        Thread motionDetectThread = null;


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


        private void QueryRawFrame()
        {
            byte[] image = camera.CaptureImageBytes();

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

            Frame f = new Frame();
            f.timeStamp = DateTime.Now.Ticks;
            f.image = IntPtr.Zero;
            f.cameraID = 2;

            IplImage ipl = BitmapConverter.ToIplImage(bmp);
            ipl.IsEnabledDispose = false;
            f.image = ipl.CvPtr;

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


        private void DetectMotion()
        {
            while (true)
            {
                Frame newFrame = GetNewFrame();
                if (newFrame.image != IntPtr.Zero)
                {
                    Frame frameToProcess = new Frame();



                    bool groupCaptured = MotionDetect.MotionDetect.PreProcessFrame(newFrame, ref frameToProcess);


                    Debug.WriteLine(DateTime.FromBinary(frameToProcess.timeStamp));

                    if (frameToProcess.searchRect.Width == 0 || frameToProcess.searchRect.Height == 0)
                    {
                        if (frameToProcess.image != IntPtr.Zero)
                        {
                            Cv.Release(ref frameToProcess.image);
                        }
                    }
                    else
                    {
                        SaveFrame(frameToProcess);
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

                if (f.image != IntPtr.Zero)
                {
                    IplImage ipl = new IplImage(f.image);
                    ipl.IsEnabledDispose = false;
                    f.searchRect.Width = ipl.Width;
                    f.searchRect.Height = ipl.Height;

                    motionFrames.Enqueue(f);

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


            if (!motionDetectThread.IsAlive)
            {
                motionDetectThread.Start();
            }

            if (!this.worker.IsBusy)
            {
                this.worker.RunWorkerAsync();
            }
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


        private static void SaveFrame(Frame frame)
        {
            IplImage ipl = new IplImage(frame.image);
            ipl.IsEnabledDispose = false;

            string path = frame.GetFileName();
            DateTime dt = DateTime.FromBinary(frame.timeStamp);

            string root = Path.Combine(Properties.Settings.Default.OutputPath,
                      frame.cameraID.ToString("d2"));

            string folder = ImageClassifier.BuildDestDirectory(root, dt, Properties.Settings.Default.BigImageDirectoryName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            path = Path.Combine(folder, path);
            ipl.SaveImage(path);
        }

        unsafe ImageDetail[] SaveImage(Target[] targets)
        {
            IList<ImageDetail> imgs = new List<ImageDetail>();

            foreach (Target t in targets)
            {
                Frame* frame = (Frame*)t.BaseFrame;

                //SaveFrame(*frame);

                string root = Path.Combine(Properties.Settings.Default.OutputPath,
                                            frame->cameraID.ToString("D2"));

                DateTime dt = DateTime.FromBinary(frame->timeStamp);


                for (int j = 0; j < t.FaceCount; ++j)
                {
                    IntPtr* f = ((IntPtr*)(t.FaceData)) + j;
                    IplImage iplFace = new IplImage(*f);
                    iplFace.IsEnabledDispose = false;


                    string folderFace = ImageClassifier.BuildDestDirectory(root, dt, Properties.Settings.Default.IconDirectoryName);

                    if (!Directory.Exists(folderFace))
                    {
                        Directory.CreateDirectory(folderFace);
                    }

                    string bigImgFileName = frame->GetFileName();
                    int idx = bigImgFileName.IndexOf('.');
                    string faceFileName = bigImgFileName.Insert(idx, "-" + j.ToString("d4"));

                    string facePath = Path.Combine(folderFace, faceFileName);

                    iplFace.SaveImage(facePath);

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
                        NativeIconExtractor.AddInFrame(ref frames[i]);
                    }


                    IntPtr target = IntPtr.Zero;

                    int count = NativeIconExtractor.SearchFaces(ref target);
                    if (count > 0)
                    {
                        Target* pTarget = (Target*)target;

                        IList<Target> targets = new List<Target>();

                        for (int i = 0; i < count; i++)
                        {
                            Target face = pTarget[i];
                            targets.Add(face);
                        }

                        Target[] tgArr = new Target[targets.Count];

                        targets.CopyTo(tgArr, 0);


                        ImageDetail[] imgs = this.SaveImage(tgArr);
                        this.screen.ShowImages(imgs);

                    }


                    NativeIconExtractor.ReleaseMem();

                    Array.ForEach(frames, f => Cv.Release(ref f.image));
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

        private static string BuildFolderPath(ImageDetail img)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(img.Name);
            int idx = nameWithoutExtension.LastIndexOf('-');
            nameWithoutExtension = nameWithoutExtension.Remove(idx);

            string bigPicName = nameWithoutExtension + Path.GetExtension(img.Name);
            string bigPicFolder = Directory.GetParent(img.ContainedBy).ToString();
            bigPicFolder = Path.Combine(bigPicFolder, Properties.Settings.Default.BigImageDirectoryName);
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }
        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.Path))
            {
                string bigPicPathName = BuildFolderPath(img);
                ImageDetail bigImageDetail = ImageDetail.FromPath(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }
}
