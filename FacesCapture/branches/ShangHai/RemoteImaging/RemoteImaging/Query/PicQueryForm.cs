using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Damany.PC.Domain;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using RemoteImaging.Core;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : DevExpress.XtraEditors.XtraForm, IPicQueryScreen
    {
        private readonly FileSystemStorage _videoRepository;
        private readonly ConfigurationManager _manager;
        private XPPageSelector _pageSelector;

        public PicQueryForm(FileSystemStorage videoRepository, ConfigurationManager manager)
        {
            _videoRepository = videoRepository;
            _manager = manager;

            InitializeComponent();

            navigator.NavigatableControl = new NullNavigator();

            var now = DateTime.Now;
            this.timeFrom.EditValue = now.AddMinutes(-10);
            this.timeTo.EditValue = now;

            galleryControl1.Gallery.ItemCheckedChanged += Gallery_ItemCheckedChanged;

            this.PageSize = 50;
        }

        void Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
        {
            var portrait = e.Item.Tag as Damany.PortraitCapturer.DAL.DTO.Portrait;
            if (portrait != null)
            {
                wholePicture.Image = portrait.Frame.ImageCopy;
                currentFace.Image = portrait.ImageCopy;
            }
        }

        public bool IsBusy { set { this.UseWaitCursor = value; } }

        void facesListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.SelectedItemChanged();
        }

        public void AttachPresenter(IPicQueryPresenter presenter)
        {
            this.presenter = presenter;
        }

        public void SetCameras(IList<Damany.PC.Domain.CameraInfo> cameras)
        {

            if (cameras == null)
                throw new ArgumentNullException("cameras", "cameras is null.");

        }

        private void ShowUserError(string msg)
        {
            MessageBox.Show(this,
                                msg,
                                this.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
        }

        private void UpdatePagesLabel()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(this.UpdatePagesLabel));
                return;
            }

            this.currPage.Text = string.Format("第 {0}/{1} 页", currentPage, totalPage);
        }


        private void ClearCurPageList()
        {
            //this.facesListView.Clear();
            //this.faceImageList.Images.Clear();
        }

        private void ClearLists()
        {
            this.imageList2.Images.Clear();
            this.currentFace.Image = null;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            UpdateCretia();
            ReLoad();

            if (_pageSelector.PageCount > 0)
            {
                _pageSelector.CurrentPage = 0;
                UpdatePageGallery();
                UpdatePageIndicator();
            }
        }


        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            //this.facesListView.Clear();
            //this.faceImageList.Images.Clear();
            //this.imageList2.Images.Clear();


            this.Close();
        }

        private void secPicListView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void PicQueryForm_Load(object sender, EventArgs e)
        {
            UpdateCretia();
            ReLoad();
        }

        private void toolStripButtonFirstPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToFirst();


        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToPrev();

        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToNext();
        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {
            this.presenter.NavigateToLast();
        }


        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            var checkedItems = galleryControl1.Gallery.GetCheckedItems();
            if (checkedItems.Count > 0)
            {
                var item = checkedItems[0].Tag as Damany.PortraitCapturer.DAL.DTO.Portrait;
                VideoPlayer.PlayRelatedVideo(item.CaptureTime, item.ImageSourceId);
            }
        }

        private void SaveSelectedImage()
        {
            //if ((this.facesListView.Items.Count <= 0) || (this.facesListView.FocusedItem == null)) return;
            //var portrait = this.facesListView.FocusedItem.Tag as Damany.Imaging.Common.Portrait;

            //using (SaveFileDialog saveDialog = new SaveFileDialog())
            //{
            //    saveDialog.RestoreDirectory = true;
            //    saveDialog.Filter = "Jpeg 文件|*.jpg";
            //    if (saveDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        portrait.GetIpl().SaveImage(saveDialog.FileName);

            //    }
            //}
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveSelectedImage();
        }


        public int PageSize
        {
            get
            {
                return this.pageSize;
            }
            set
            {

                //this.pageSizeCombo.SelectedItem = value;
                this.pageSize = value;
            }
        }

        private string[] imagesFound = new string[0];
        private int currentPage;
        private int totalPage;
        private int pageSize;
        private IPicQueryPresenter presenter;


        #region IPicQueryScreen Members


        public void Clear()
        {
            if (InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Clear));
                return;
            }

            //this.facesListView.Items.Clear();
            //this.faceImageList.Images.Clear();
        }

        public void AddItem(Damany.Imaging.Common.Portrait item)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action = this.AddItem;
                this.BeginInvoke(action, item);
                return;
            }

            this.faceImageList.Images.Add(item.GetIpl().ToBitmap());

            var lvi = new ListViewItem
            {
                Tag = item,
                Text = item.CapturedAt.ToString(),
                ImageIndex = this.faceImageList.Images.Count - 1
            };

            //this.facesListView.Items.Add(lvi);
        }

        public void EnableSearchButton(bool enable)
        {
            if (InvokeRequired)
            {
                Action<bool> action = this.EnableSearchButton;
                this.BeginInvoke(action, enable);
                return;
            }

            this.queryBtn.Enabled = enable;
        }

        public void EnableNavigateButtons(bool enable)
        {
            if (InvokeRequired)
            {
                Action<bool> action = this.EnableNavigateButtons;
                this.BeginInvoke(action, enable);
                return;
            }


            //this.navigator.Enabled = enable;

        }

        public Damany.Util.DateTimeRange TimeRange
        {
            get
            {
                return new Damany.Util.DateTimeRange(
                    (DateTime)this.timeFrom.EditValue,
                    (DateTime)this.timeTo.EditValue
                    );
            }
            set
            {

            }
        }

        public Damany.Imaging.Common.Portrait SelectedItem
        {
            get;
            set;
        }

        public Damany.PC.Domain.Destination SelectedCamera
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public Damany.PC.Domain.CameraInfo[] Cameras
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }

        public string[] Machines
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }

        public void Show()
        {
            this.ShowDialog();
        }

        public void ShowUserIsBusy(bool busy)
        {
            Cursor.Current = busy ? Cursors.WaitCursor : Cursors.Default;
        }


        public Damany.Imaging.Common.Portrait CurrentPortrait
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {

            }
        }

        public Image CurrentBigPicture
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.wholePicture.Image = value;
            }
        }


        public int CurrentPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.currentPage = value;
                this.UpdatePagesLabel();

            }
        }

        public int TotalPage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                this.totalPage = value;
                this.UpdatePagesLabel();
            }
        }


        public void ShowStatus(string status)
        {
            if (InvokeRequired)
            {
                Action<string> action = this.ShowStatus;

                this.BeginInvoke(action, status);
                return;
            }

            this.status.Caption = status;
        }

        #endregion

        #region IPicQueryScreen Members


        public string SelectedMachine
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        private void navigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                case NavigatorButtonType.Custom:
                    break;
                case NavigatorButtonType.First:
                    if (_pageSelector.PageCount > 0)
                    {
                        _pageSelector.CurrentPage = 0;
                        UpdatePageGallery();
                        UpdatePageIndicator();
                    }
                    break;
                case NavigatorButtonType.PrevPage:
                    if (_pageSelector.CurrentPage > 0)
                    {
                        _pageSelector.CurrentPage--;
                        UpdatePageGallery();
                        UpdatePageIndicator();

                    }
                    break;
                case NavigatorButtonType.Prev:
                    break;
                case NavigatorButtonType.Next:
                    break;
                case NavigatorButtonType.NextPage:
                    if (_pageSelector.CurrentPage < _pageSelector.PageCount - 1)
                    {
                        _pageSelector.CurrentPage++;
                        UpdatePageGallery();
                        UpdatePageIndicator();
                    }

                    break;
                case NavigatorButtonType.Last:
                    if (_pageSelector.PageCount > 0)
                    {
                        _pageSelector.CurrentPage = _pageSelector.PageCount - 1;
                        UpdatePageGallery();
                        UpdatePageIndicator();
                    }
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

        private void UpdatePageIndicator()
        {
            string lbl = string.Format("第{0}/{1}页", _pageSelector.CurrentPage + 1, _pageSelector.PageCount);
            currPage.Text = lbl;
        }


        private void UpdatePageGallery()
        {

            this.galleryControl1.Gallery.BeginUpdate();
            this.galleryControl1.Gallery.Groups.Clear();

            var gq = from Damany.PortraitCapturer.DAL.DTO.Portrait item in portraits
                     group item by item.CaptureTime.Date.AddHours(item.CaptureTime.Hour) into g
                     orderby g.Key ascending
                     select g;

            foreach (var group in gq)
            {
                var gg = new GalleryItemGroup();
                gg.Caption = group.Key.ToString("yyyy年MM月dd日 HH:00") + " - " + group.Key.AddHours(1).ToString("yyyy年MM月dd日 HH:00");

                foreach (var item in group)
                {
                    var gi = new GalleryItem(item.Thumbnail, item.CaptureTime.ToShortTimeString(), null);
                    gi.Hint = item.CaptureTime.ToString();
                    gi.Tag = item;
                    gg.Items.Add(gi);
                }

                this.galleryControl1.Gallery.Groups.Add(gg);
            }

            galleryControl1.Gallery.EndUpdate();
        }

        public DevExpress.Xpo.XPPageSelector CreatePageSelector()
        {
            var ps = new XPPageSelector();
            ps.Collection = portraits;
            ps.PageSize = 50;

            return ps;
        }

        private void UpdateCretia()
        {
            var cretia = CriteriaOperator.Parse("CaptureTime >= ? and CaptureTime < ?",
                                                            timeFrom.EditValue, timeTo.EditValue);

            portraits.Criteria = cretia;
        }

        private void ReLoad()
        {
            _pageSelector = CreatePageSelector();
            _pageSelector.CurrentPage = 0;
            UpdatePageGallery();
            UpdatePageIndicator();
        }


    }
}
