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


        private static bool IsStaticFrame(Frame aFrame)
        {
            return aFrame.image != null
                    && (aFrame.searchRect.Width == 0 || aFrame.searchRect.Height == 0);
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

                    if (IsStaticFrame(frameToProcess))
                    {
                        Cv.Release(ref frameToProcess.image);
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
                    SaveFrame(f);

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

        private static string RootFolderForCamera(int cameraID)
        {
            return Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));
        }


        private static unsafe string FolderForFaces(int camID, DateTime dt)
        {
            string folderForFaces = ImageClassifier.BuildDestDirectory(RootFolderForCamera(camID),
                                    dt, Properties.Settings.Default.IconDirectoryName);

            if (!Directory.Exists(folderForFaces))
                Directory.CreateDirectory(folderForFaces);

            return folderForFaces;
        }


        private static unsafe string GetFaceFileName(string bigImagePath, int indexOfFace)
        {
            int idx = bigImagePath.IndexOf('.');
            string faceFileName = bigImagePath.Insert(idx, "-" + indexOfFace.ToString("d4"));
            return faceFileName;
        }

        private static unsafe string GetFacePath(Frame frame, DateTime dt, int j)
        {
            string folderFace = FolderForFaces(frame.cameraID, dt);

            string faceFileName = GetFaceFileName(frame.GetFileName(), j);

            string facePath = Path.Combine(folderFace, faceFileName);
            return facePath;
        }


        unsafe ImageDetail[] SaveImage(Target[] targets)
        {
            IList<ImageDetail> imgs = new List<ImageDetail>();

            foreach (Target t in targets)
            {
                Frame frame = t.BaseFrame;

                DateTime dt = DateTime.FromBinary(frame.timeStamp);

                for (int j = 0; j < t.FaceCount; ++j)
                {
                    IntPtr* f = ((IntPtr*)(t.FaceData)) + j;
                    IplImage aFace = new IplImage(*f);
                    aFace.IsEnabledDispose = false;

                    string facePath = GetFacePath(frame, dt, j);

                    aFace.SaveImage(facePath);

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
                        DateTime dt = DateTime.FromBinary(frames[i].timeStamp);

                        NativeIconExtractor.AddInFrame(frames[i]);
                    }

                    IntPtr target = IntPtr.Zero;

                    int count = NativeIconExtractor.SearchFaces(ref target);
                    if (count > 0)
                    {
                        Target* pTarget = (Target*)target;

                        IList<Target> targets = new List<Target>();

                        int upLimit = count;

                        if (frames.Length > 1 && Properties.Settings.Default.DetectMotion
                            && Properties.Settings.Default.removeDuplicatedFace)
                        {
                            upLimit = Math.Min(count, Properties.Settings.Default.MaxDupFaces);
                        }

                        for (int i = 0; i < upLimit; i++)
                        {
                            Target face = pTarget[i];

                            Frame frm = face.BaseFrame;

                            int idx = Array.FindIndex(frames, fm => fm.cameraID == frm.cameraID
                                                                && fm.image == frm.image
                                                                && fm.timeStamp == frm.timeStamp);

                            Debug.Assert(idx != -1);

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



        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.Path))
            {
                string bigPicPathName = ImageClassifier.BigImgPathFor(img);
                ImageDetail bigImageDetail = ImageDetail.FromPath(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }
}
