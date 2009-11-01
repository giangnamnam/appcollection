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
using RemoteControlService;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {
        private int itemCount;
        private int currentPage;
        private int totalPage;
        public int PageSize { get; set; }

        int lastSpotID = 0;
        DateTime lastBeginTime = DateTime.Now;
        DateTime lastEndTime = DateTime.Now;
        RemoteControlService.IServiceFacade proxy;


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

            totalPage = (itemCount + PageSize - 1) / PageSize;

            return totalPage;
        }

        void ShowCurrentPage()
        {
            bestPicListView.BeginUpdate();

            ClearCurPageList();

            for (int i = (currentPage - 1) * PageSize;
                (i < currentPage * PageSize) && (i < itemCount);
                ++i)
            {
                ImagePair ip = proxy.GetFace(i);

                this.imageList1.Images.Add(ip.Face);
                string text = System.IO.Path.GetFileName(ip.Face.Tag as string);
                ListViewItem item = new ListViewItem()
                {
                    Tag = ip,
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
            this.imageList2.Images.Clear();
            this.pictureBoxFace.Image = null;
        }

        private DateTime CreateDateTime(
            DateTimePicker picker,
            DevExpress.XtraEditors.TimeEdit time)
        {
            DateTime date = picker.Value;
            DateTime t = time.Time;

            DateTime dt = new DateTime(date.Year, date.Month, date.Day, t.Hour, t.Minute, t.Second);

            return dt;
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


            int camID = int.Parse(this.comboBox1.Text);

            string cameraID = camID.ToString("D2");

            //judge the input validation
            DateTime dateTime1 = CreateDateTime(this.dateTimePicker1, timeEdit1);
            DateTime dateTime2 = CreateDateTime(this.dateTimePicker2, timeEdit2);

            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！", "警告");
                return;
            }

            string address = string.Format("net.tcp://{0}:8000/TcpService", Configuration.Instance.FindCameraByID(camID).IpAddress);

            this.proxy = ServiceProxy.ProxyFactory.CreateProxy(address);

            itemCount = proxy.BeginSearchFaces(2, dateTime1, dateTime2);

            if (itemCount == 0)
            {
                MessageBox.Show(this, "未找到图片");
                return;
            }


            CalcPagesCount();
            currentPage = 1;
            UpdatePagesLabel();


            if (itemCount == 0)
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
            ImagePair ip = this.bestPicListView.FocusedItem.Tag as ImagePair;

            this.pictureBoxFace.Image = ip.Face;

            //detail infomation
            ImageDetail imgInfo = ImageDetail.FromPath(ip.FacePath);

            string captureLoc = string.Format("抓拍地点: {0}", imgInfo.FromCamera);
            this.labelCaptureLoc.Text = captureLoc;

            string captureTime = string.Format("抓拍时间: {0}", imgInfo.CaptureTime);
            this.labelCaptureTime.Text = captureTime;


            this.pictureBoxWholeImg.Image = ip.BigImage;

        }


        private void PopulateBigPicList(string iconFile)
        {
            this.imageList2.Images.Clear();

            string[] files = ImageSearch.SelectedBestImageChanged(iconFile);
            if (files == null)
            {
                MessageBox.Show("没有搜索到对应的二级图片", "警告");
                return;
            }

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
            }
        }

        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {


        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();
            this.imageList2.Images.Clear();


            this.Close();
        }

        private void secPicListView_DoubleClick(object sender, EventArgs e)
        {

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

            string[] videos = FileSystemStorage.VideoFilesOfImage(imgInfo);

            if (videos.Length == 0)
            {
                MessageBox.Show("未找到相关视频");
                return;
            }

            VideoPlayer.PlayVideosAsync(videos);

        }

        private void SaveSelectedImage()
        {
            if ((this.bestPicListView.Items.Count <= 0) || (this.bestPicListView.FocusedItem == null)) return;
            string filePath = this.bestPicListView.FocusedItem.Tag as string;

            if (File.Exists(filePath))
            {
                this.pictureBoxFace.Image = Image.FromFile(filePath);
            }
            ImageDetail imgInfo = ImageDetail.FromPath(filePath);
            string bigImgPath = FileSystemStorage.BigImgPathForFace(imgInfo);

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.RestoreDirectory = true;
                saveDialog.Filter = "Jpeg 文件|*.jpg";
                //saveDialog.FileName = filePath.Substring(filePath.Length - 27, 27);
                string fileName = Path.GetFileName(filePath);
                saveDialog.FileName = fileName;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    if (pictureBoxFace.Image != null)
                    {
                        string path = saveDialog.FileName;
                        pictureBoxFace.Image.Save(path);
                        path = path.Replace(fileName, Path.GetFileName(bigImgPath));
                        pictureBoxWholeImg.Image.Save(path);
                    }
                }
            }
        }


        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveSelectedImage();
        }
    }
}
