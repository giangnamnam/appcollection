using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common;
using Damany.Util;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using RemoteControlService;
using RemoteImaging.LicensePlate;
using Video = RemoteImaging.Core.Video;
using Damany.Util.Extensions;
using Portrait = Damany.PortraitCapturer.DAL.DTO.Portrait;

namespace RemoteImaging.Query
{
    using LicensePlate;

    public class VideoQueryPresenter : IVideoQueryPresenter
    {
        private readonly IVideoQueryScreen _screen;
        private readonly LicensePlateRepository _licensePlateRepository;
        private readonly ConfigurationManager _manager;
        private Damany.PC.Domain.CameraInfo _selectedCamera;
        private DateTimeRange _range;
        private SearchScope _scope;
        private DateTimeRange _currentRange;
        private XPCollection<Damany.PortraitCapturer.DAL.DTO.Portrait> _portraits;

        public VideoQueryPresenter(IVideoQueryScreen screen,
                                   ConfigurationManager manager)
        {
            _screen = screen;
            _manager = manager;
        }

        public void Start()
        {
            _screen.AttachPresenter(this);
            _screen.Cameras = _manager.GetCameras().ToArray();
            _screen.Show();

        }


        public void Search()
        {
            var selectedCamera = this._screen.SelectedCamera;
            if (selectedCamera == null)
            {
                return;
            }


            _selectedCamera = this._screen.SelectedCamera;
            _range = this._screen.TimeRange;
            _scope = this._screen.SearchScope;

            _currentRange = new DateTimeRange(_range.From, _range.From.AddHours(1));

            var videos = FindFirstVideo();
            if (videos.Count == 0)
            {
                MessageBox.Show("在该时间段内，没有录制视频");
                return;
            }


            _range.From = videos.First().CapturedAt;
            _range.To = videos.Last().CapturedAt;

            UpdateCurrentRange(videos);

            SearchAsync();


        }

        private void SearchAsync()
        {
            _screen.Busy = true;

            Task searcher = Task.Factory.StartNew(DoSearch, CancellationToken.None);
            searcher.ContinueWith(ant =>
            {
                var ignore = ant.Exception;
                _screen.Busy = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }
        public void PlayVideo()
        {
            var p = this._screen.SelectedVideoFile;
            if (p == null) return;
            if (string.IsNullOrEmpty(p.Path))
                return;
            if (!System.IO.File.Exists(p.Path))
                return;

            this._screen.PlayVideoInPlace(p.Path);
        }

        public void ShowRelatedFaces()
        {
            var range = GetSelectedVideoTimeRange();

            var query = from p in _portraits
                        where p.CaptureTime >= range.From && p.CaptureTime < range.To
                        select p;

            foreach (var portrait in query)
            {
                _screen.AddFace(portrait);
            }
        }

        public void ShowRelatedLicenseplates()
        {
            //var range = GetSelectedVideoTimeRange();

            //var queryCretia = GetLicensePlateQueryCretia(range);

            //var t = Task.Factory.StartNew(() => _licensePlateRepository.GetLicensePlates(queryCretia).ToList());

            //var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            //t.ContinueWith(result =>
            //{
            //    _screen.ClearLicenseplatesList();

            //    foreach (var licensePlateInfo in result.Result)
            //    {
            //        _screen.AddLicensePlate(licensePlateInfo);
            //    }
            //}, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, taskScheduler);
        }

        private DateTimeRange GetSelectedVideoTimeRange()
        {
            var video = _screen.SelectedVideoFile;

            var from = video.CapturedAt.RoundToMinute();
            var to = from.AddMinutes(1);

            return new DateTimeRange(from, to);
        }

        public void NextPage()
        {
            if (_selectedCamera == null)
                return;

            if (_currentRange.To.AddHours(1) > _range.To)
            {
                return;
            }

            _currentRange.From = _currentRange.From.AddHours(1);
            _currentRange.To = _currentRange.To.AddHours(1);

            UpdateScreenTimeRange();
            SearchAsync();
        }

        public void PreviousPage()
        {
            if (_selectedCamera == null)
                return;

            if (_currentRange.From.AddHours(-1) < _range.From)
            {
                return;
            }

            _currentRange.From = _currentRange.From.AddHours(-1);
            _currentRange.To = _currentRange.To.AddHours(-1);

            UpdateScreenTimeRange();
            SearchAsync();
        }

        public void FirstPage()
        {
            if (_selectedCamera == null)
                return;

            _currentRange.From = _range.From;
            _currentRange.To = _currentRange.From.AddHours(1);

            UpdateScreenTimeRange();

            SearchAsync();
        }

        public void LastPage()
        {
            if (_selectedCamera == null)
                return;


            _currentRange.To = _range.To;
            _currentRange.From = _range.To.AddHours(-1);

            UpdateScreenTimeRange();
            SearchAsync();
        }

        private void DoSearch()
        {

            var videos =
            new FileSystemStorage(
                Properties.Settings.Default.OutputPath).
                VideoFilesBetween(_selectedCamera.Id,
                                  _currentRange.From, _currentRange.To).ToList();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var range = new DateTimeRange(_currentRange.From, _currentRange.To.AddMinutes(1));

            watch.Stop();
            System.Diagnostics.Debug.WriteLine("frames search took " + watch.Elapsed);

            watch.Start();

            _portraits = new XPCollection<Portrait>();
            var cretia = CriteriaOperator.Parse("CaptureTime >= ? and CaptureTime < ?",
                                                            range.From, range.To);
            _portraits.Criteria = cretia;
            _portraits.Load();

            var gq = from  item in _portraits
                     group item by item.CaptureTime.Date.AddHours(item.CaptureTime.Hour).AddMinutes(item.CaptureTime.Minute) into g
                     orderby g.Key ascending
                     select g;
           
            watch.Stop();
            System.Diagnostics.Debug.WriteLine("portraits search took " + watch.Elapsed);

            this._screen.ClearAll();

            foreach (var v in videos)
            {
                if (v.CapturedAt.Ticks < _currentRange.From.Ticks || v.CapturedAt.Ticks > _currentRange.To.Ticks)
                {
                    continue;
                }

                v.HasFaceCaptured = gq.Where(g => g.Key == v.CapturedAt).Count() != 0;

                if ((_scope & SearchScope.FaceCapturedVideo)
                    == SearchScope.FaceCapturedVideo)
                {
                    if (v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((_scope & SearchScope.MotionWithoutFaceVideo)
                    == SearchScope.MotionWithoutFaceVideo)
                {
                    if (v.HasMotionDetected && !v.HasFaceCaptured)
                    {
                        _screen.AddVideo(v);
                    }
                }

                if ((_scope & SearchScope.MotionLessVideo)
                    == SearchScope.MotionLessVideo)
                {
                    if (!v.HasFaceCaptured &&
                        !v.HasMotionDetected)
                    {
                        _screen.AddVideo(v);
                    }
                }

            }

        }

        private SearchCretia GetLicensePlateQueryCretia(DateTimeRange range)
        {
            var cretia = new SearchCretia();
            cretia.AddCretia(lpi => lpi.CaptureTime >= range.From && lpi.CaptureTime <= range.To);
            return cretia;
        }

        private void UpdateScreenTimeRange()
        {
            _screen.CurrentRange = _currentRange;
        }

        private List<Video> FindFirstVideo()
        {
            var videos =
                new FileSystemStorage(
                    Properties.Settings.Default.OutputPath).
                    VideoFilesBetween(_selectedCamera.Id,
                                      _range.From, _range.To).ToList();


            return videos;
        }

        private void UpdateCurrentRange(List<Video> videos)
        {
            _currentRange.From = videos.First().CapturedAt;
            _currentRange.To = _currentRange.From.AddHours(1);

            UpdateScreenTimeRange();
        }
    }
}
