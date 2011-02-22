using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.RemoteImaging.Common;
using Damany.Util;
using RemoteImaging.Extensions;

namespace RemoteImaging.LicensePlate
{
    public partial class FormLicensePlateQuery : DevExpress.XtraEditors.XtraForm, ILicenseplateSearchScreen
    {
        private readonly ConfigurationManager _configurationManager;
        private readonly FileSystemStorage _videoRepository;
        private ILicensePlateSearchPresenter _presenter;
        private readonly BindingList<LicenseplateInfoWithThumbnail> _licensePlates =
            new BindingList<LicenseplateInfoWithThumbnail>();

        public FormLicensePlateQuery(ConfigurationManager configurationManager, FileSystemStorage videoRepository)
        {
            if (configurationManager == null) throw new ArgumentNullException("configurationManager");
            if (videoRepository == null) throw new ArgumentNullException("videoRepository");

            _configurationManager = configurationManager;
            _videoRepository = videoRepository;
            InitializeComponent();

            InitLicenseplateDataTable();

            var now = DateTime.Now;
            to.EditValue = now;
            from.EditValue = now.AddHours(-1);

        }


        public bool IsBusy
        {
            set { this.UseWaitCursor = value; }
        }

        private void InitLicenseplateDataTable()
        {

            licensePlateLists.DataSource = _licensePlates;
        }

        public void AttachPresenter(ILicensePlateSearchPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException("presenter");

            _presenter = presenter;
        }

        public new void Show()
        {
            this.ShowDialog(Application.OpenForms[0]);
        }

        public void AddLicensePlateInfo(LicensePlateInfo licensePlateInfo)
        {
            var img = licensePlateInfo.LoadImage();
            var ratio = (float)img.Height / img.Width;
            var thumbNail = img.GetThumbnailImage(64, (int)(64 * ratio), null, IntPtr.Zero);
            img.Dispose();

            var lpin = new LicenseplateInfoWithThumbnail()
            {
                Thumbnail = thumbNail,
                LicenseplateNumber = licensePlateInfo.LicensePlateNumber,
                CaptureTime = licensePlateInfo.CaptureTime,
                Info = licensePlateInfo
            };


            if (InvokeRequired)
            {

                Action action = () => _licensePlates.Add(lpin);

                BeginInvoke(action);
                return;
            }

            _licensePlates.Add(lpin);
        }

        public void Clear()
        {
            if (InvokeRequired)
            {
                Action ac = this.Clear;

                this.BeginInvoke(ac);
                return;
            }

            _licensePlates.Clear();
        }

        public bool MatchLicenseNumber
        {
            get { return matchLicenseNumber.Checked; }
            set { throw new NotImplementedException(); }
        }

        public string LicenseNumber
        {
            get { return (string)this.licenseplateNumberToSearch.EditValue; }
            set { throw new NotImplementedException(); }
        }

        public bool MatchTimeRange
        {
            get { return matchTimeRange.Checked; }
            set { throw new NotImplementedException(); }
        }

        public DateTimeRange Range
        {
            get
            {
                var range = new Damany.Util.DateTimeRange((DateTime)from.EditValue, (DateTime)to.EditValue);
                return range;
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        private void searchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(licenseplateNumberToSearch.Text) && matchLicenseNumber.Checked)
            {
                MessageBox.Show(this, "车牌号为空");
                return;
            }

            _presenter.Search();
        }

        private void licensePlateList_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void mathTimeRange_CheckedChanged(object sender, EventArgs e)
        {
            from.Enabled = matchTimeRange.Checked;
            to.Enabled = matchTimeRange.Checked;
        }

        private void matchLicenseNumber_CheckedChanged(object sender, EventArgs e)
        {
            licenseplateNumberToSearch.Enabled = matchLicenseNumber.Checked;
        }


        private void playVideo_Click(object sender, EventArgs e)
        {
            var licenseplateInfoWithThumbnail = licensePlateListView.GetFocusedRow() as LicenseplateInfoWithThumbnail;
            if (licenseplateInfoWithThumbnail == null) return;

            VideoPlayer.PlayRelatedVideo(
                licenseplateInfoWithThumbnail.Info.CaptureTime,
                licenseplateInfoWithThumbnail.Info.CapturedFrom);
        }

        private void licensePlateListView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var row = licensePlateListView.GetFocusedRow() as LicenseplateInfoWithThumbnail;
            if (row == null) return;

            bigImage.Picture = row.Info.LoadImage();
            this.currentLicenseplateNumber.EditValue = row.Info.LicensePlateNumber;
            this.currentLicenseplateCaptureTime.EditValue = row.Info.CaptureTime;


        }

        private void playRelatedVideo_Click(object sender, EventArgs e)
        {
            var row = this.licensePlateListView.GetFocusedRow() as LicenseplateInfoWithThumbnail;
            if (row == null) return;

            VideoPlayer.PlayRelatedVideo(row.Info.CaptureTime, row.Info.CapturedFrom);
        }

        private void licensePlateListView_CustomDrawEmptyForeground(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            var dataSource = licensePlateListView.DataSource;

            var s = "未找到满足条件的车牌，请重新搜索";
            var r = new Rectangle(e.Bounds.Left + 5, e.Bounds.Top + 5, e.Bounds.Width - 5,
                                  e.Bounds.Height - 5);
            e.Graphics.DrawString(s, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), r);
        }

    }

    internal class LicenseplateInfoWithThumbnail
    {
        public Image Thumbnail { get; set; }
        public string LicenseplateNumber { get; set; }
        public DateTime CaptureTime { get; set; }
        public LicensePlateInfo Info { get; set; }
    }
}
