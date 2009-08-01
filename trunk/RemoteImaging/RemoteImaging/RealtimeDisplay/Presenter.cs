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

namespace RemoteImaging.RealtimeDisplay
{
    public class Presenter : IImageScreenObserver
    {
        IImageScreen screen;
        ICamera camera;
        System.ComponentModel.BackgroundWorker worker;

        Queue<Frame[]> framesQueue = new Queue<Frame[]>();
        Queue<Frame> frameSeq = new Queue<Frame>();

        object locker = new object();
        AutoResetEvent go = new AutoResetEvent(false);

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

            this.screen.Observer = this;
            this.worker = new System.ComponentModel.BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.DoWork += worker_DoWork;
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


        private void DetectMotion()
        {
            while (true)
            {
                byte[] image = camera.CaptureImageBytes();

                MemoryStream memStream = new MemoryStream(image);
                Bitmap bmp = null;
                try
                {
                    bmp = (Bitmap)Image.FromStream(memStream);
                }
                catch (System.ArgumentException)
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

                Frame lastFrame = new Frame();

                bool groupCaptured = MotionDetect.MotionDetect.PreProcessFrame(f, ref lastFrame);

                if (lastFrame.searchRect.Width == 0 || lastFrame.searchRect.Height == 0)
                {
                    if (lastFrame.image != IntPtr.Zero)
                    {
                        Cv.Release(ref lastFrame.image);
                    }
                }
                else
                {
                    frameSeq.Enqueue(lastFrame);
                }

                if (groupCaptured)
                {
                    Frame[] frames = frameSeq.ToArray();
                    framesQueue.Clear();

                    if (frames.Length <= 0) return;

                    lock (locker)
                    {
                        framesQueue.Enqueue(frames);
                        go.Set();
                    }
                }

                Thread.Sleep(200);
            }


        }

        public void Start()
        {
            motionDetectThread = new Thread(this.DetectMotion);
            motionDetectThread.IsBackground = true;
            motionDetectThread.Start();

            this.worker.RunWorkerAsync();

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

        private ImageDetail[] BuildIconImages(string destFolder, ImageDetail[] bigImgs, string iconFilesString)
        {
            string[] iconFiles = iconFilesString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            IDictionary<int, IList<string>> iconGroup = new Dictionary<int, IList<string>>();

            //分组
            for (int i = 0; i < iconFiles.Length; i += 2)
            {
                int idxOfBigPic = int.Parse(iconFiles[i + 1]);
                if (!iconGroup.ContainsKey(idxOfBigPic))
                {
                    iconGroup.Add(idxOfBigPic, new List<string>());
                }

                iconGroup[idxOfBigPic].Add(iconFiles[i]);
            }

            IList<ImageDetail> returnImgs = new List<ImageDetail>();
            foreach (var iconSubGroup in iconGroup)
            {
                for (int i = 0; i < iconSubGroup.Value.Count; i++)
                {
                    int idx = iconSubGroup.Key;
                    string bigPicPath = bigImgs[idx].Path;
                    string bigpicExtension = Path.GetExtension(bigPicPath);
                    string bigPicNameWithoutExtention = Path.GetFileNameWithoutExtension(bigPicPath);
                    string iconFileName = string.Format("{0}-{1:d4}{2}", bigPicNameWithoutExtention, i, bigpicExtension);
                    string iconFilePath = Path.Combine(destFolder, iconFileName);

                    //rename file
                    string origIconFilePath = Path.Combine(destFolder, iconSubGroup.Value[i]);
                    File.Move(origIconFilePath, iconFilePath);
                    returnImgs.Add(ImageDetail.FromPath(iconFilePath));
                }
            }

            ImageDetail[] returnImgsArray = new ImageDetail[returnImgs.Count];
            returnImgs.CopyTo(returnImgsArray, 0);

            return returnImgsArray;
        }


        unsafe ImageDetail[] SaveImage(Target[] targets)
        {
            IList<ImageDetail> imgs = new List<ImageDetail>();

            foreach (Target t in targets)
            {
                Frame* frame = (Frame*)t.BaseFrame;
                IplImage ipl = new IplImage(frame->image);
                ipl.IsEnabledDispose = false;

                string path = frame->GetFileName();
                DateTime dt = DateTime.FromBinary(frame->timeStamp);

                string root = Path.Combine(Properties.Settings.Default.OutputPath, frame->cameraID.ToString("d2"));

                string folder = ImageClassifier.BuildDestDirectory(root, dt, Properties.Settings.Default.BigImageDirectoryName);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                path = Path.Combine(folder, path);
                ipl.SaveImage(path);

                for (int j = 0; j < t.FaceCount; ++j)
                {
                    IntPtr* f = ((IntPtr*)(t.FaceData)) + j;
                    IplImage iplFace = new IplImage(*f);
                    iplFace.IsEnabledDispose = false;

                    string folderFace = ImageClassifier.BuildDestDirectory(root, dt, Properties.Settings.Default.IconDirectoryName);
                    string bigImgFileName = frame->GetFileName();
                    int idx = bigImgFileName.IndexOf('.');
                    bigImgFileName.Insert(idx, "-" + j.ToString("d4"));

                    string facePath = Path.Combine(folderFace, bigImgFileName);

                    iplFace.SaveImage(facePath);

                    imgs.Add(ImageDetail.FromPath(facePath));

                }
            }

            return (ImageDetail[])imgs;

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
                        Frame frame = frames[i];
                        NativeIconExtractor.AddInFrame(ref frame);
                        IplImage ipl = new IplImage(frame.image);
                        ipl.IsEnabledDispose = false;
                    }

                    IntPtr target = IntPtr.Zero;

                    int count = NativeIconExtractor.SearchFaces(ref target);
                    if (count <= 0) continue;

                    Target* pTarget = (Target*)target;

                    IList<Target> targets = new List<Target>();

                    for (int i = 0; i < count; i++)
                    {
                        Target face = pTarget[i];
                        targets.Add(face);
                    }


                    ImageDetail[] imgs = this.SaveImage((Target[])targets);

                    this.screen.ShowImages(imgs);

                    NativeIconExtractor.ReleaseMem();

                    Array.ForEach(frames, f => Cv.Release(ref f.image));
                }
                else
                {
                    go.WaitOne();
                }
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
