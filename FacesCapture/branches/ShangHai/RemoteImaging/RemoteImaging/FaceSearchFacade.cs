using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.Imaging.Processors;
using Damany.PC.Domain;
using Damany.Cameras;
using Damany.PortraitCapturer.DAL;
using OpenCvSharp;

namespace RemoteImaging
{
    public class FaceSearchFacade
    {
        private readonly FaceComparer _faceComparer;
        private readonly MotionDetector _motionDetector;
        private readonly PortraitFinder _portraitFinder;
        private readonly IEnumerable<IFacePostFilter> _facePostFilters;
        private readonly IEventAggregator _eventAggregator;
        private AForge.Video.IVideoSource _jpegStream;
        private readonly ConcurrentQueue<List<Frame>> _motionFramesQueue = new ConcurrentQueue<List<Frame>>();
        private System.Threading.Tasks.Task _faceSearchTask;
        private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        private Damany.PC.Domain.CameraInfo _cameraInfo;
        private System.Threading.CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private Image _latestImage;
        private object _lastImageLock = new object();
        private Control _displayControl;


        public int MotionQueueSize { get; set; }

        public FaceSearchFacade(Damany.Imaging.Processors.MotionDetector motionDetector,
                                Damany.Imaging.Processors.PortraitFinder portraitFinder,
                                IEnumerable<Damany.Imaging.Common.IFacePostFilter> facePostFilters,
                                FaceComparer faceComparer,
                                IEventAggregator eventAggregator)
        {
            _motionDetector = motionDetector;
            _portraitFinder = portraitFinder;
            _facePostFilters = facePostFilters;
            _eventAggregator = eventAggregator;
            _faceComparer = faceComparer;

            MotionQueueSize = 10;

            Rectangle rectangle = GetRoi();

            _portraitFinder.ROI = rectangle;
        }

        public void SetupMonitorRegion()
        {
            var img = LastImage;
            if (img != null)
            {
                using (var form = new FormRoi())
                {
                    form.Image = img;
                    form.Roi = GetRoi();
                    if (form.ShowDialog(Application.OpenForms[0]) == DialogResult.OK)
                    {
                        _portraitFinder.ROI = form.Roi;
                        SaveRoi(form.Roi);
                    }
                }
            }
        }


        public void StartWith(CameraInfo cameraInfo, System.Windows.Forms.Control displayControl)
        {
            _displayControl = displayControl;

            if (_jpegStream == null)
            {
                switch (cameraInfo.Provider)
                {
                    case CameraProvider.LocalDirectory:
                        var dir = new Damany.Cameras.DirectoryFilesCamera(cameraInfo.Location.LocalPath, "*.jpg");
                        dir.Repeat = true;
                        dir.FrameIntervalMs = cameraInfo.Interval;

                        _jpegStream = dir;
                        break;

                    case CameraProvider.Sanyo:
                        var sanyo = new JPEGExtendStream(cameraInfo.Location.ToString());
                        sanyo.Login = cameraInfo.LoginUserName ?? "guest";
                        sanyo.Password = cameraInfo.LoginPassword ?? "guest";
                        sanyo.FrameInterval = cameraInfo.Interval;

                        sanyo.RequireCookie = cameraInfo.Provider == CameraProvider.Sanyo;
                        _portraitFinder.PostFilters = _facePostFilters;

                        _jpegStream = sanyo;
                        break;
                    case CameraProvider.AipStar:

                        var hwnd = displayControl != null ? displayControl.Handle : IntPtr.Zero;
                        var aip = CreateAipCamera(cameraInfo, hwnd);
                        aip.Camera.StreamId = 0;


                        Mediator.Instance.RegisterHandler(
                            "liveViewResize", new Action<object>(LiveViewResized));
                        _jpegStream = aip;

                        if (displayControl != null)
                        {
                            //aip.Camera.DisplayPos = displayControl.ClientRectangle;
                        }

                        var videoRecorder = CreateAipCamera(cameraInfo, IntPtr.Zero);
                        videoRecorder.Camera.PathFunc = d =>
                                                            {
                                                                var utc = d.ToUniversalTime();
                                                                var rp = string.Format(
                                                                    "{0:d2}\\NORMAL\\{1:d4}{2:d2}{3:d2}\\{4:d2}\\{5:d2}.m4v",
                                                                    cameraInfo.Id,
                                                                    utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute);

                                                                var ap = Path.Combine(Util.GetVideoOutputPath(), rp);
                                                                var directory = Path.GetDirectoryName(ap);
                                                                if (!Directory.Exists(directory))
                                                                {
                                                                    Directory.CreateDirectory(directory);
                                                                }

                                                                return ap;
                                                            };
                        videoRecorder.Camera.StreamId = 1;
                        videoRecorder.Start();
                        videoRecorder.Camera.StartRecord();

                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _cameraInfo = cameraInfo;

                _jpegStream.NewFrame += JpegStreamNewFrame;
                _jpegStream.Start();



                if (_faceSearchTask == null)
                {
                    _faceSearchTask =
                        Task.Factory.StartNew(
                            FaceSearchWorkerThread,
                            _tokenSource.Token,
                            TaskCreationOptions.LongRunning,
                            TaskScheduler.Default
                            );
                }



                _faceComparer.InitializeAsync();
                _faceComparer.Start();

                _faceComparer.Threshold = Properties.Settings.Default.RealTimeFaceCompareSensitivity;
                _faceComparer.Sensitivity = Properties.Settings.Default.LbpThreshold;

            }
        }

        public void SignalToStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.NewFrame -= JpegStreamNewFrame;
                _jpegStream.SignalToStop();

                _tokenSource.Cancel();
                _signal.Set();

                _faceComparer.SignalToStop();
            }
        }

        public void WaitForStop()
        {
            if (_jpegStream != null)
            {
                _jpegStream.SignalToStop();
                _jpegStream.WaitForStop();
            }

            if (_faceSearchTask != null)
            {
                _signal.Set();
                _tokenSource.Cancel();
                try
                {
                    _faceSearchTask.Wait();
                }
                catch (AggregateException)
                {
                }

            }

            foreach (List<Frame> frames in _motionFramesQueue)
            {
                frames.ForEach(f =>
                                   {
                                       f.Dispose();
                                       f = null;
                                   });
            }

            if (_faceComparer != null)
            {
                _faceComparer.WaitForStop();
            }
        }

        public Image LastImage
        {
            get
            {
                lock (_lastImageLock)
                {
                    if (_latestImage != null)
                    {
                        return (Image)_latestImage.Clone();
                    }

                    return _latestImage;
                }
            }
            set
            {
                lock (_lastImageLock)
                {
                    if (value != null)
                    {
                        _latestImage = (Image)value.Clone();
                    }
                }
            }
        }


        private bool _resized;

        void JpegStreamNewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (!_resized)
            {
                if (_displayControl != null)
                {
                    _displayControl.Invalidate();
                }
                this.LiveViewResized(null);
                _resized = true;
            }

            LastImage = (Image)eventArgs.Frame.Clone();

            if (_motionFramesQueue.Count > MotionQueueSize)
            {
                return;
            }

            var bmp = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

            OpenCvSharp.IplImage ipl = null;

            try
            {
                ipl = OpenCvSharp.IplImage.FromBitmap(bmp);
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                }
            }


            var frame = new Frame(ipl);
            var grouped = _motionDetector.ProcessFrame(frame);

            if (grouped)
            {
                var motionFrames = _motionDetector.GetMotionFrames();
                if (motionFrames != null)
                {
                    foreach (var motionFrame in motionFrames)
                    {
                        motionFrame.DeviceId = _cameraInfo.Id;
                    }

                    SaveMotionFrames(motionFrames);

                    _motionFramesQueue.Enqueue(motionFrames);
                    _signal.Set();
                }

            }
        }

        private static Rectangle GetRoi()
        {
            var rectangle = new Rectangle();
            rectangle.X = Properties.Settings.Default.SrchRegionLeft;
            rectangle.Y = Properties.Settings.Default.SrchRegionTop;
            rectangle.Width = Properties.Settings.Default.SrchRegionWidth;
            rectangle.Height = Properties.Settings.Default.SrchRegionHeight;
            return rectangle;
        }

        private static void SaveRoi(Rectangle rectangle)
        {
            Properties.Settings.Default.SrchRegionLeft = rectangle.X;
            Properties.Settings.Default.SrchRegionTop = rectangle.Y;
            Properties.Settings.Default.SrchRegionWidth = rectangle.Width;
            Properties.Settings.Default.SrchRegionHeight = rectangle.Height;

            Properties.Settings.Default.Save();
        }


        private static string GetImagePath(DateTime dateTime)
        {
            var relativePath = string.Format("{0}\\{1}\\{2}\\{3}\\{4}.jpg",
                                                                 dateTime.Year,
                                                                 dateTime.Month,
                                                                 dateTime.Day,
                                                                 dateTime.Hour,
                                                                 Guid.NewGuid());

            var absPath = Path.Combine(Properties.Settings.Default.OutputPath, relativePath);

            return absPath;
        }

        private List<Damany.PortraitCapturer.DAL.DTO.CapturedImageObject> SaveMotionFrames(List<Frame> motionFrames)
        {
            List<Damany.PortraitCapturer.DAL.DTO.CapturedImageObject> savedObjects = new List<Damany.PortraitCapturer.DAL.DTO.CapturedImageObject>();
            using (var uow = new DevExpress.Xpo.UnitOfWork())
            {
                foreach (var motionFrame in motionFrames)
                {
                    var f = new Damany.PortraitCapturer.DAL.DTO.Frame(uow);
                    var path = SaveImage(motionFrame.GetImage(), motionFrame.CapturedAt);
                    f.CaptureTime = motionFrame.CapturedAt;
                    f.ImagePath = path;
                    f.ImageSourceId = motionFrame.DeviceId;
                    f.Save();
                    savedObjects.Add(f);
                }

                uow.CommitChanges();
            }

            for (int i = 0; i < motionFrames.Count; i++)
            {
                motionFrames[i].Oid = savedObjects[i].Oid;
            }

            return savedObjects;
        }

        void FaceSearchWorkerThread()
        {
            var watcher = new System.Diagnostics.Stopwatch();
            while (true)
            {
                if (_tokenSource.Token.IsCancellationRequested) break;

                List<Frame> frames = null;
                if (_motionFramesQueue.TryDequeue(out frames))
                {
                    if (_tokenSource.Token.IsCancellationRequested) break;

                    foreach (var frame in frames)
                    {
                        watcher.Restart();
                        var portraits = _portraitFinder.ProcessFrame(frame, _tokenSource.Token);

                        foreach (var portrait in portraits)
                        {
                            var p = portrait;
                            p.Frame.Oid = frame.Oid;
                        }

                        SavePortraits(portraits);
                        frame.Dispose();

                        if (_eventAggregator != null)
                        {
                            portraits.ForEach(p => _eventAggregator.PublishPortrait(p));
                        }

                        if (_faceComparer != null)
                        {
                            _faceComparer.ProcessPortraits(portraits);
                        }

                        portraits.ForEach(p => p.Dispose());
                        var ms = watcher.ElapsedMilliseconds;
                        if (_eventAggregator != null) _eventAggregator.PublishFrameProcessed((int)ms, _motionFramesQueue.Count);
                    }

                }
                else
                {
                    _signal.WaitOne();
                }
            }
        }

        private void SavePortraits(List<Portrait> portraits)
        {
            using (var uow = new DevExpress.Xpo.UnitOfWork())
            {
                foreach (var portrait in portraits)
                {
                    var path = SaveImage(portrait.GetIpl(), portrait.CapturedAt);
                    var p = new Damany.PortraitCapturer.DAL.DTO.Portrait(uow);
                    p.ImagePath = path;
                    p.FaceBounds = portrait.FaceBounds;
                    p.CaptureTime = portrait.CapturedAt;
                    p.ImageSourceId = portrait.DeviceId;

                    var frame = uow.GetObjectByKey(typeof(Damany.PortraitCapturer.DAL.DTO.Frame), portrait.Frame.Oid);
                    p.Frame = (Damany.PortraitCapturer.DAL.DTO.Frame)frame;
                }

                uow.CommitChanges();
            }
        }

        private static string SaveImage(IplImage image, DateTime captureTime)
        {
            var path = GetImagePath(captureTime);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            image.SaveImage(path);

            return path;
        }

        private void LiveViewResized(object state)
        {
            FoKoShVideoSourceAdapter cam = _jpegStream as FoKoShVideoSourceAdapter;
            if (cam != null && _displayControl != null)
            {
                var w = cam.Camera.ImageWidth;
                var h = cam.Camera.ImageHeight;
                Size size = new Size(w, h);
                float num = Math.Min((float)(((float)_displayControl.ClientRectangle.Width) / ((float)size.Width)), (float)(((float)_displayControl.ClientRectangle.Height) / ((float)size.Height)));
                var rectangle = new Rectangle();
                rectangle.Width = (int)(size.Width * num);
                rectangle.Height = (int)(size.Height * num);
                rectangle.X = (_displayControl.ClientRectangle.Width - rectangle.Width) / 2;
                rectangle.Y = (_displayControl.ClientRectangle.Height - rectangle.Height) / 2;

                cam.Camera.DisplayPos = rectangle;
            }
        }
        private static FoKoShVideoSourceAdapter CreateAipCamera(CameraInfo cameraInfo, IntPtr hwnd)
        {
            var c = new FoKoShVideoSourceAdapter(hwnd);
            c.Camera.Ip = cameraInfo.Location.Host;
            c.Camera.Port = 6002;
            c.Camera.UserName = cameraInfo.LoginUserName ?? "system";
            c.Camera.Password = cameraInfo.LoginPassword ?? "system";
            c.FrameInterval = cameraInfo.Interval;

            return c;
        }
    }
}
