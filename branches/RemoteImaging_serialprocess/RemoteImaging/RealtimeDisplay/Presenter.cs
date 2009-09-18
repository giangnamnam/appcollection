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

            motionDetectThread = new Thread(this.DetectMotion);
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

            screen.LiveImage = bmp;

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

            DateTime lastTime = DateTime.Now.AddMinutes(-3);
            CvVideoWriter videoWriter = null;
            int count = 0;

            while (true)
            {
                Frame newFrame = GetNewFrame();
                if (newFrame.image != IntPtr.Zero)
                {
                    DateTime dtFrame = DateTime.FromBinary(newFrame.timeStamp);

                    IplImage ipl = new IplImage(newFrame.image);
                    ipl.IsEnabledDispose = false;

                    //new minute start, create new vide file
                    if (dtFrame.Minute != lastTime.Minute)
                    {
                        if (videoWriter != null)
                        {
                            Cv.ReleaseVideoWriter(videoWriter);
                            videoWriter = null;
                        }


                        videoWriter = Cv.CreateVideoWriter(
                            @"d:\" + dtFrame.Minute + ".avi",
                            "XVID",
                            10,
                            ipl.Size
                            );

                        System.Diagnostics.Debug.WriteLine(@"d:\" + dtFrame.Minute + ".avi");
                    }

                    videoWriter.WriteFrame(ipl);

                    lastTime = dtFrame;

                    Frame frameToProcess = new Frame();

                    bool groupCaptured = false;

                    count++;

                    if (count % 5 == 0)
                    {
                        if (Properties.Settings.Default.DetectMotion)
                            groupCaptured =
                                MotionDetect.MotionDetect.PreProcessFrame(newFrame, ref frameToProcess);
                        else
                            groupCaptured = NoneMotionDetect.PreProcessFrame(newFrame, ref frameToProcess);

                        count = 0;
                    }
                    else
                    {
                        Cv.Release(ref newFrame.image);
                    }

                    if (IsStaticFrame(frameToProcess))
                    {
                        Cv.Release(ref frameToProcess.image);
                    }
                    else
                    {
                        SaveFrame(frameToProcess);
                        NativeIconExtractor.AddInFrame(frameToProcess);
                        motionFrames.Enqueue(frameToProcess);
                    }

                    if (groupCaptured)
                    {
                        Target[] tgts = SearchFaces();

                        //remove duplicated faces
                        int upLimit = tgts.Length;

                        if (Properties.Settings.Default.DetectMotion
                            && Properties.Settings.Default.removeDuplicatedFace)
                        {
                            upLimit = Math.Min(upLimit, Properties.Settings.Default.MaxDupFaces);
                        }

                        Target[] removed = new Target[upLimit];
                        Array.ConstrainedCopy(tgts, 0, removed, 0, upLimit);

                        ImageDetail[] details = this.SaveImage(removed);
                        this.screen.ShowImages(details);

                        Frame[] frames = motionFrames.ToArray();
                        motionFrames.Clear();

                        //release memory
                        Array.ForEach(frames, f => Cv.Release(ref f.image));
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
                //this.worker.RunWorkerAsync();
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

        private unsafe Target[] SearchFaces()
        {
            IntPtr retTarget = IntPtr.Zero;

            int count = NativeIconExtractor.SearchFaces(ref retTarget);

            if (count <= 0) return new Target[0];

            Target* pTarget = (Target*)retTarget;

            IList<Target> targets = new List<Target>();

            for (int i = 0; i < count; i++)
            {
                Target face = pTarget[i];
                targets.Add(face);
            }

            Target[] tgArr = new Target[targets.Count];
            targets.CopyTo(tgArr, 0);

            return tgArr;
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
