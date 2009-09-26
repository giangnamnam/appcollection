using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RemoteImaging.Core;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {
        private string[] imagesFound = new string[0];
        private int currentPage;
        private int totalPage;
        public int PageSize { get; set; }

        public PicQueryForm()
        {
            InitializeComponent();


            foreach (Camera camera in Configuration.Instance.Cameras)
            {
                this.comboBox1.Items.Add(camera.ID.ToString());
            }

            this.PageSize = 20;
        }

        private void UpdatePagesLabel()
        {
            this.toolStripLabelCurPage.Text = string.Format("第{0}/{1}页", currentPage, totalPage);
        }

        private int CalcPagesCount()
        {
            totalPage = imagesFound.Length / PageSize;

            if (totalPage == 0) totalPage = 1;

            return totalPage;


        }

        void ShowCurrentPage()
        {
            bestPicListView.BeginUpdate();

            ClearCurPageList();

            for (int i = (currentPage - 1) * PageSize;
                (i < currentPage * PageSize) && (i < imagesFound.Length);
                ++i)
            {
                this.imageList1.Images.Add(Image.FromFile(imagesFound[i]));
                string text = System.IO.Path.GetFileName(imagesFound[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = imagesFound[i],
                    Text = text,
                    ImageIndex = i % PageSize
                };
                this.bestPicListView.Items.Add(item);
            }

            bestPicListView.EndUpdate();

        }

        private void ClearCurPageList()
        {
            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();
        }

        private void ClearLists()
        {
            ClearCurPageList();
            this.secPicListView.Clear();
            this.imageList2.Images.Clear();
            this.pictureBox1.Image = null;
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            ClearLists();

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }
            string cameraID = int.Parse(this.comboBox1.Text).ToString("D2");


            //judge the input validation
            DateTime date1 = this.dateTimePicker1.Value;
            DateTime date2 = this.dateTimePicker2.Value;
            DateTime time1 = this.timeEdit1.Time;
            DateTime time2 = this.timeEdit2.Time;

            DateTime dateTime1 = new DateTime(date1.Year, date1.Month, date1.Day, time1.Hour, time1.Minute, time1.Second);
            DateTime dateTime2 = new DateTime(date2.Year, date2.Month, date2.Day, time2.Hour, time2.Minute, time2.Second);
            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！", "警告");
                return;
            }
            /////
            string year1 = dateTime1.Year.ToString("D4");
            string month1 = dateTime1.Month.ToString("D2");
            string day1 = dateTime1.Day.ToString("D2");
            string hour1 = dateTime1.Hour.ToString("D2");
            string minute1 = dateTime1.Minute.ToString("D2");
            string second1 = dateTime1.Second.ToString("D2");

            string year2 = dateTime2.Year.ToString("D4");
            string month2 = dateTime2.Month.ToString("D2");
            string day2 = dateTime2.Day.ToString("D2");
            string hour2 = dateTime2.Hour.ToString("D2");
            string minute2 = dateTime2.Minute.ToString("D2");
            string second2 = dateTime2.Second.ToString("D2");

            Query.ImageDirSys startDir = new ImageDirSys(cameraID, year1, month1, day1, hour1, minute1, second1);
            Query.ImageDirSys endDir = new ImageDirSys(cameraID, year2, month2, day2, hour2, minute2, second2);
            Query.ImageSearch imageSearch = new ImageSearch();

            imagesFound = imageSearch.SearchImages(startDir, endDir, RemoteImaging.Query.ImageDirSys.SearchType.PicType);

            if (imagesFound.Length == 0)
            {
                MessageBox.Show(this, "未找到图片");
                return;
            }


            CalcPagesCount();
            currentPage = 1;
            UpdatePagesLabel();


            if (imagesFound == null)
            {
                MessageBox.Show("没有搜索到满足条件的图片！", "警告");
                return;
            }

            this.bestPicListView.Scrollable = true;
            this.bestPicListView.MultiSelect = false;
            this.bestPicListView.View = View.LargeIcon;
            this.bestPicListView.LargeImageList = imageList1;


            ShowCurrentPage();

            Cursor.Current = Cursors.Default;


        }
        //以前
        //02_090702150918-0001.jpg -->大图片
        //02_090702152518-0006-0000.jpg--> 小图片

        //现在 
        //02_090807144104343.jpg-->大图片
        //02_090807144104343-0000.jpg-->小图片
        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            string filePath = this.bestPicListView.FocusedItem.Tag as string;

            //show modify icon
            if (File.Exists(filePath))
            {
                this.pictureBox1.Image = Image.FromFile(filePath);
            }

            //detail infomation
            ImageDetail imgInfo = ImageDetail.FromPath(filePath);

            string captureLoc = string.Format("抓拍地点: {0}", imgInfo.FromCamera);
            this.labelCaptureLoc.Text = captureLoc;

            string captureTime = string.Format("抓拍时间: {0}", imgInfo.CaptureTime);
            this.labelCaptureTime.Text = captureTime;

            this.PopulateBigPicList(Path.GetFileName(filePath));
        }


        private void PopulateBigPicList(string iconFile)
        {
            this.secPicListView.Clear();
            this.imageList2.Images.Clear();

            Query.ImageSearch imageSearch = new ImageSearch();
            string[] files = imageSearch.SelectedBestImageChanged(iconFile);
            if (files == null)
            {
                MessageBox.Show("没有搜索到对应的二级图片", "警告");
                return;
            }

            this.secPicListView.Scrollable = true;
            this.secPicListView.MultiSelect = false;
            this.secPicListView.View = View.LargeIcon;
            this.secPicListView.LargeImageList = imageList2;


            for (int i = 0; i < files.Length; i++)
            {
                this.imageList2.Images.Add(Image.FromFile(files[i]));
                string text = System.IO.Path.GetFileName(files[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = files[i],
                    Text = text,
                    ImageIndex = i
                };
                this.secPicListView.Items.Add(item);
            }
        }

        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {
            string filePath = this.secPicListView.FocusedItem.Tag as string;

            if (File.Exists(filePath))
            {
                this.pictureBox1.Image = Image.FromFile(filePath);
            }

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();
            this.secPicListView.Clear();
            this.imageList2.Images.Clear();


            this.Close();
        }

        private void secPicListView_DoubleClick(object sender, EventArgs e)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = RemoteImaging.Core.ImageDetail.FromPath(this.secPicListView.FocusedItem.Tag as string);
            detail.Show(this);
        }

        private void PicQueryForm_Load(object sender, EventArgs e)
        {
            this.toolStripComboBoxPageSize.Text = this.PageSize.ToString();
        }

        private void toolStripButtonFirstPage_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripButtonPrePage_Click(object sender, EventArgs e)
        {
            --currentPage;

            if (currentPage <= 0)
            {
                currentPage = 1;
                return;
            }

            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripButtonNextPage_Click(object sender, EventArgs e)
        {
            ++currentPage;

            if (currentPage > totalPage)
            {
                currentPage = totalPage;
                return;
            }

            ShowCurrentPage();
            UpdatePagesLabel();
        }

        private void toolStripButtonLastPage_Click(object sender, EventArgs e)
        {
            currentPage = totalPage;

            ShowCurrentPage();
            UpdatePagesLabel();

        }

        private void toolStripComboBoxPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pageSize = (string)this.toolStripComboBoxPageSize.SelectedItem;

            // if (string.IsNullOrEmpty(pageSize)) return;

            int sz = int.Parse(pageSize);

            this.PageSize = sz;

            CalcPagesCount();
            currentPage = 1;
            UpdatePagesLabel();

            ShowCurrentPage();

        }

        private void toolStripButtonPlayVideo_Click(object sender, EventArgs e)
        {
            if (this.bestPicListView.SelectedItems.Count != 1) return;

            string imgPath = this.bestPicListView.SelectedItems[0].Tag as string;

            ImageDetail imgInfo = ImageDetail.FromPath(imgPath);

            string[] videos = VideoSearch.FindVideos(imgInfo);

            if (videos.Length == 0)
            {
                MessageBox.Show("未找到相关视频");
                return;
            }

            VideoPlayer.PlayVideosAsync(videos);

        }
    }
}
