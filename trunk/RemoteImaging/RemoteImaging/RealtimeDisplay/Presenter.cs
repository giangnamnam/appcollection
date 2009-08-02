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

            motionDetectThread = new Thread(this.DetectMotion);
            motionDetectThread.IsBackground = true;
            motionDetectThread.Name = "motion detect";


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

                Frame frameToRelease = new Frame();

                bool groupCaptured = MotionDetect.MotionDetect.PreProcessFrame(f, ref frameToRelease);

                if (frameToRelease.searchRect.Width == 0 || frameToRelease.searchRect.Height == 0)
                {
                    if (frameToRelease.image != IntPtr.Zero)
                    {
                        Cv.Release(ref frameToRelease.image);
                    }
                }
                else
                {
                    frameSeq.Enqueue(frameToRelease);
                }

                if (groupCaptured)
                {
                    Frame[] frames = frameSeq.ToArray();
                    frameSeq.Clear();

                    if (frames.Length <= 0) continue;

                    lock (locker)
                    {
                        framesQueue.Enqueue(frames);
                        go.Set();
                    }
                }

                Thread.Sleep(250);
            }


        }

        public void Start()
        {
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

                string root = Path.Combine(Properties.Settings.Default.OutputPath,
                    frame->cameraID.ToString("d2"));

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
