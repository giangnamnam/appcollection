using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Damany.PC.Domain;
using Damany.RemoteImaging.Common.Forms;
using Damany.Util;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using RemoteImaging.Core;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Damany.RemoteImaging.Common;
using RemoteImaging.Extensions;
using RemoteImaging.LicensePlate;
using Portrait = Damany.PortraitCapturer.DAL.DTO.Portrait;

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : DevExpress.XtraEditors.XtraForm, IVideoQueryScreen
    {
        private readonly IMessageBoxService _messageBoxService;
        BindingList<Video> _videos = new BindingList<Video>();
        BindingList<LicensePlateInfo> _licensePlates = new BindingList<LicensePlateInfo>();
        private GalleryItemGroup _galleryGroup;
        private XPCollection<Portrait> _portraits;
        private System.Threading.CancellationTokenSource _cts;

        public VideoQueryForm()
        {
            InitializeComponent();

            PopulateSearchScope();
            setListViewColumns();


            rand = new Random((int)DateTime.Now.Ticks);

            var now = DateTime.Now;
            this.timeTO.EditValue = now;
            this.timeFrom.EditValue = now.AddDays(-1);

            var videoNavigator = new NullNavigator();
            this.controlNavigator1.NavigatableControl = videoNavigator;

            videoGrid.DataSource = _videos;
            //licenseplatesGrid.DataSource = _licensePlates;

            faceGalleryControl.Gallery.ItemCheckedChanged += Gallery_ItemCheckedChanged;
            _galleryGroup = new GalleryItemGroup();
            faceGalleryControl.Gallery.Groups.Add(_galleryGroup);
        }

        public VideoQueryForm(IMessageBoxService messageBoxService)
            : this()
        {
            if (messageBoxService == null)
                throw new ArgumentNullException("messageBoxService", "messageBoxService is null.");
            _messageBoxService = messageBoxService;
        }

        void Gallery_ItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            var items = faceGalleryControl.Gallery.GetCheckedItems();
            if (items.Count > 0)
            {
                var item = items[0].Tag as Damany.PortraitCapturer.DAL.DTO.Portrait;
                if (item != null && item.Frame != null)
                {
                    wholePicture.Image = item.Frame.ImageCopy;
                }
            }
        }


        public void SetCameras(IList<Damany.PC.Domain.CameraInfo> cameras)
        {
            if (cameras == null)
                throw new ArgumentNullException("cameras", "cameras is null.");

            this.cameraIds.Properties.DataSource = cameras;
        }

        private void PopulateSearchScope()
        {
            var searchTypes = new List<SearchCategory>();
            searchTypes.Add(new SearchCategory { Name = "全部", Scope = SearchScope.All });
            searchTypes.Add(new SearchCategory { Name = "有人像视频", Scope = SearchScope.FaceCapturedVideo });
            searchTypes.Add(new SearchCategory { Name = "有动态无人像视频", Scope = SearchScope.MotionWithoutFaceVideo });
            searchTypes.Add(new SearchCategory { Name = "无动态视频", Scope = SearchScope.MotionLessVideo });
        }


        private void UpdateCurrentVideoPage()
        {
            ClearAll();

            foreach (Damany.PortraitCapturer.DAL.DTO.Video v in videoCollection)
            {
                var video = new Video();
                video.CapturedAt = v.CaptureTime;
                video.Path = v.Path;
                this.AddVideo(video);
            }

            _cts = new CancellationTokenSource();
            _worker = Task.Factory.StartNew(UpdateFaceStatus, _cts.Token);
            _worker.ContinueWith(ant =>
                                     {
                                         var exception = ant.Exception;
                                         if (!(exception.InnerException is OperationCanceledException))
                                         {
                                             _messageBoxService.ShowError("系统发生异常，请重试。");
                                         }
                                     }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private void UpdateCurrentPage()
        {
            UpdateNavigateButtons();
            UpdateCurrentVideoPage();
            UpdateCurrentPageIndicator();
        }
        private void queryBtn_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
            this.ClearAll();

            var c = CriteriaOperator.Parse("CaptureTime >= ? And CaptureTime <= ?",
                                           this.TimeRange.From, this.TimeRange.To);
            videoCollection.Criteria = c;

            pageSelector = new XPPageSelector();
            pageSelector.PageSize = 50;
            pageSelector.CurrentPage = 0;
            pageSelector.Collection = videoCollection;

            UpdateCurrentPage();
        }

        private void UpdateCurrentPageIndicator()
        {
            if (pageSelector != null)
            {
                if (pageSelector.PageCount > 0)
                {
                    currentPageIndicator.Text = string.Format("第 {0}/{1} 页", pageSelector.CurrentPage + 1,
                                                              pageSelector.PageCount);
                }
            }
        }

        private void UpdateNavigateButtons()
        {
            if (pageSelector != null)
            {
                controlNavigator1.Buttons.NextPage.Enabled = pageSelector.PageCount > 0 && pageSelector.CurrentPage < pageSelector.PageCount - 1;
                controlNavigator1.Buttons.PrevPage.Enabled = pageSelector.PageCount > 0 && pageSelector.CurrentPage > 0;
                controlNavigator1.Buttons.First.Enabled = pageSelector.PageCount > 0 && pageSelector.CurrentPage != 0;
                controlNavigator1.Buttons.Last.Enabled = pageSelector.PageCount > 0 && pageSelector.CurrentPage != pageSelector.PageCount - 1;
            }
        }

        private void UpdateFaceStatus()
        {
            if (_videos.Count > 0)
            {
                var min = _videos.Min(v => v.CapturedAt);
                var max = _videos.Max(v => v.CapturedAt);

                _portraits = new XPCollection<Portrait>();
                var cretia = CriteriaOperator.Parse("CaptureTime >= ? and CaptureTime < ?",
                                                    min, max);
                _portraits.Criteria = cretia;
                _portraits.Load();

                var gq = from Portrait item in _portraits
                         group item by item.CaptureTime.Date.AddHours(item.CaptureTime.Hour).AddMinutes(item.CaptureTime.Minute)
                             into g
                             orderby g.Key ascending
                             select g;

                foreach (var v in _videos)
                {
                    var video = v;
                    _cts.Token.ThrowIfCancellationRequested();
                    var hasFace = gq.Where(g => g.Key == video.CapturedAt).Count() != 0;
                    Action ac = () => video.HasFaceCaptured = hasFace;
                    this.BeginInvoke(ac);
                }
            }
        }

        private void setListViewColumns()//添加ListView行头
        {

        }


        private void playVideoAndRelatedPic()
        {
            _galleryGroup.Items.Clear();
            _presenter.PlayVideo();
            ShowRelatedFaces(SelectedVideoFile.CapturedAt);
        }

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            axVLCPlugin21.StopPlaying();

        }

        private void picList_DoubleClick(object sender, EventArgs e)
        {

        }


        internal class SearchCategory
        {
            public string Name { get; set; }
            public Query.SearchScope Scope { get; set; }
        }



        #region IVideoQueryScreen Members

        public Damany.Util.DateTimeRange TimeRange
        {
            get { return new DateTimeRange((DateTime)this.timeFrom.EditValue, (DateTime)this.timeTO.EditValue); }
        }

        public DateTimeRange CurrentRange
        {
            set
            {
                if (InvokeRequired)
                {
                    Action<DateTimeRange> action = range => { this.CurrentRange = range; };
                    this.BeginInvoke(action, value);
                    return;
                }
            }
        }

        public SearchScope SearchScope
        {
            get
            {

                return Query.SearchScope.All;
            }
        }

        public CameraInfo SelectedCamera
        {
            get
            {
                if (this.cameraIds.EditValue != null)
                {
                    return (CameraInfo)this.cameraIds.EditValue;
                }

                return null;
            }
        }

        public CameraInfo[] Cameras
        {
            set
            {
                this.cameraIds.Properties.DataSource = value;
                this.cameraIds.Properties.DisplayMember = "Name";

                if (value != null)
                {
                    this.cameraIds.EditValue = value.Length > 0 ? value[0] : null;
                }

            }
        }

        public bool Busy
        {
            set
            {
                Action<bool> ac = busy =>
                                      {
                                          this.ShowBusyIndicator(busy);
                                          if (busy == false)
                                          {
                                          }
                                      };
                this.BeginInvoke(ac, value);

            }
        }

        private void ShowBusyIndicator(bool isbusy)
        {
            Application.UseWaitCursor = isbusy;
            if (isbusy)
            {
                if (_busyIndicator == null)
                {
                    _busyIndicator = new ProgressForm();
                    _busyIndicator.Text = "请稍候...";
                    _busyIndicator.ShowDialog(this);
                }

            }
            else
            {

                if (_busyIndicator != null)
                {
                    _busyIndicator.Close();
                    _busyIndicator = null;
                }
            }
        }

        public void ClearAll()
        {
            if (InvokeRequired)
            {
                Action ac = ClearAll;
                this.BeginInvoke(ac);
                return;
            }

            _videos.Clear();
            _licensePlates.Clear();

            this.ClearFacesList();
        }

        public void ClearFacesList()
        {
            this.faceImageList.Images.Clear();
            _galleryGroup.Items.Clear();
        }

        public void ClearLicenseplatesList()
        {
            _licensePlates.Clear();
        }

        public void AddFace(Damany.PortraitCapturer.DAL.DTO.Portrait p)
        {
            var item = new GalleryItem();
            item.Image = p.ImageCopy;
            item.Hint = p.CaptureTime.ToString();
            item.Tag = p;

            _galleryGroup.Items.Add(item);
        }

        private Random rand;

        public void AddVideo(RemoteImaging.Core.Video v)
        {
            if (InvokeRequired)
            {
                Action<RemoteImaging.Core.Video> ac = AddVideo;
                this.BeginInvoke(ac, v);
                return;
            }

            _videos.Add(v);

        }

        public void AddLicensePlate(LicensePlateInfo licensePlateInfo)
        {
            _licensePlates.Add(licensePlateInfo);
        }

        public void AttachPresenter(IVideoQueryPresenter presenter)
        {
            this._presenter = presenter;
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(this, msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public new void Show()
        {
            ShowDialog(Application.OpenForms[0]);
        }

        #endregion

        private IVideoQueryPresenter _presenter;

        #region IVideoQueryScreen Members


        public void PlayVideoInPlace(string videoPath)
        {
            axVLCPlugin21.PlayFile(videoPath);
        }

        #endregion

        #region IVideoQueryScreen Members


        public Video SelectedVideoFile
        {
            get;
            set;
        }

        #endregion

        private Damany.RemoteImaging.Common.Forms.ProgressForm _busyIndicator;
        private Task _worker;
        private XPPageSelector pageSelector;

        private void controlNavigator1_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                case NavigatorButtonType.Custom:
                    break;
                case NavigatorButtonType.First:
                    if (pageSelector != null)
                    {
                        pageSelector.CurrentPage = 0;
                        UpdateCurrentPage();
                    }
                    e.Handled = true;
                    break;
                case NavigatorButtonType.PrevPage:
                    if (pageSelector != null)
                    {
                        pageSelector.CurrentPage--;
                        UpdateCurrentPage();
                    }
                    e.Handled = true;
                    break;
                case NavigatorButtonType.Prev:
                    break;
                case NavigatorButtonType.Next:
                    break;
                case NavigatorButtonType.NextPage:
                    if (pageSelector != null)
                    {
                        pageSelector.CurrentPage++;
                        UpdateCurrentPage();
                    }
                    e.Handled = true;
                    break;
                case NavigatorButtonType.Last:
                    if (pageSelector != null)
                    {
                        pageSelector.CurrentPage = pageSelector.PageCount - 1;
                        UpdateCurrentPage();
                    }
                    e.Handled = true;
                    break;
                case NavigatorButtonType.Append:
                    break;
                case NavigatorButtonType.Remove:
                    break;
                case NavigatorButtonType.Edit:
                    break;
                case NavigatorButtonType.EndEdit:
                    break;
                case NavigatorButtonType.CancelEdit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ShowRelatedFaces(DateTime selectedTime)
        {

            var query = from p in _portraits
                        where p.CaptureTime >= selectedTime && p.CaptureTime <= selectedTime.AddMinutes(1)
                        select p;

            foreach (var portrait in query)
            {
                AddFace(portrait);
            }
        }

        private void chartControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void videoGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var hi = videoGridView.CalcHitInfo(e.Location);

            if (hi.InRow)
            {
                var v = videoGridView.GetRow(hi.RowHandle) as Video;
                if (v != null)
                {
                    SelectedVideoFile = v;
                    playVideoAndRelatedPic();
                }

            }
        }

        private void licenseplatesGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //var row = licenseplatesGridView.GetRow(e.RowHandle);
            //if (row != null)
            //{
            //    var lpi = row as LicensePlateInfo;
            //    if (lpi != null)
            //    {
            //        var img = lpi.LoadImage();
            //        bigImage.Picture = img;
            //    }
            //}
        }

        private void faceList_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void VideoQueryForm_Load(object sender, EventArgs e)
        {
            var oldestQuery = new XPQuery<Damany.PortraitCapturer.DAL.DTO.Video>(session1);
            var oldest = (from v in oldestQuery
                          orderby v.CaptureTime ascending
                          select v).FirstOrDefault();

            var newest = (from v in oldestQuery
                          orderby v.CaptureTime descending
                          select v).FirstOrDefault();
            var range = string.Format(" ({0} —— {1})", oldest.CaptureTime, newest.CaptureTime);
            groupControl1.Text += range;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _videos[0].HasFaceCaptured = true;
        }
    }


    internal class FaceCaptureCount
    {
        public DateTime CaptureAt
        {
            get;
            set;
        }

        public int Count
        {
            get;
            set;
        }

        public Video Video
        {
            get;
            set;
        }
    }
}
