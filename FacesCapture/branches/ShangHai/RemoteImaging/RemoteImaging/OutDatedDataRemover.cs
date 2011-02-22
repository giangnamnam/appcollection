using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using DevExpress.Xpo;
using MiscUtil.Threading;

namespace RemoteImaging
{
    public class OutDatedDataRemover : IMyStartable
    {
        public OutDatedDataRemover(string outputDirectory)
        {
            if (String.IsNullOrEmpty(outputDirectory))
                throw new ArgumentException("outputDirectory is null or empty.", "outputDirectory");
            if (!Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException(outputDirectory + " not found.");

            _outputDirectory = outputDirectory;

            Interval = 10 * 60 * 1000;
            ReservedDiskSpaceInGb = 1;

#if DEBUG
            Interval = 10 * 1000;
#endif

        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DeleteData();

            _timer.Enabled = true;
        }

        private void DeleteData()
        {
            while (ShouldDoCleaning())
            {
                using (var uow = new UnitOfWork())
                {
                    var videoQuery = new XPQuery<Damany.PortraitCapturer.DAL.DTO.Video>(uow);
                    var oldestVideo = (from v in videoQuery
                                       orderby v.CaptureTime ascending
                                       select v).FirstOrDefault();

                    var portraitQuery = new XPQuery<Damany.PortraitCapturer.DAL.DTO.Portrait>(uow);
                    var oldestPortrait = (from p in portraitQuery
                                          orderby p.CaptureTime ascending
                                          select p).FirstOrDefault();

                    var oldest = DateTime.MaxValue;
                    if (oldestVideo != null)
                    {
                        if (oldestVideo.CaptureTime <= oldest)
                        {
                            oldest = oldestVideo.CaptureTime;
                        }
                    }

                    if (oldestPortrait != null)
                    {
                        if (oldestPortrait.CaptureTime <= oldest)
                        {
                            oldest = oldestPortrait.CaptureTime;
                        }
                    }

                    if (oldest != DateTime.MaxValue)
                    {
                        var range = new Damany.Util.DateTimeRange(oldest.Date, oldest.Date.AddDays(1));
                        DeleteOutdatedPortrait(range, uow);
                        DeleteOutdatedVideos(range, uow);
                    }

                    uow.CommitChanges();
                }
            }
        }

        private static void DeleteOutdatedPortrait(Damany.Util.DateTimeRange range, UnitOfWork uow)
        {
            var cs = string.Format("CaptureTime >= '{0}' And CaptureTime < '{1}'", range.From, range.To);
            var c = DevExpress.Data.Filtering.CriteriaOperator.Parse(cs);
            var portraits = new XPCollection<Damany.PortraitCapturer.DAL.DTO.Portrait>(uow, c);
            uow.Delete(portraits);
            uow.Save(portraits);
        }

        private static void DeleteOutdatedVideos(Damany.Util.DateTimeRange range, UnitOfWork uow)
        {
            var cs = string.Format("CaptureTime >= '{0}' And CaptureTime < '{1}'", range.From, range.To);
            var c = DevExpress.Data.Filtering.CriteriaOperator.Parse(cs);
            var videos = new XPCollection<Damany.PortraitCapturer.DAL.DTO.Video>(uow, c);
            uow.Delete(videos);
        }

        private bool ShouldDoCleaning()
        {
            var freeSpace = GetFreeDiskSpaceInGb(_outputDirectory);
            return freeSpace <= ReservedDiskSpaceInGb;
        }

        public long GetFreeDiskSpaceInGb(string drive)
        {
            var driveInfo = new DriveInfo(drive);
            long freeSpace = driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024);

            return freeSpace;
        }



        public double Interval { get; set; }
        private float _reservedDiskSpaceInGb;

        public float ReservedDiskSpaceInGb
        {
            get
            {
                return _reservedDiskSpaceInGb;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("value should > 0");
                }

                _reservedDiskSpaceInGb = value;
            }
        }


        private readonly FileSystemStorage _videoRepository;
        private readonly string _outputDirectory;

        private System.Timers.Timer _timer;
        public void Start()
        {
            if (_timer == null)
            {
                _timer = new System.Timers.Timer();
                _timer.Interval = Interval;
                _timer.AutoReset = false;
                _timer.Elapsed += timer_Elapsed;
                _timer.Enabled = true;
            }
        }
    }
}
