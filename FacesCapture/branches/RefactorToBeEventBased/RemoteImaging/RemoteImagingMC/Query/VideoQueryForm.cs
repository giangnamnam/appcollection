using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RemoteImaging.Core;
using RemoteControlService;
using System.Diagnostics;

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : Form
    {
        private IServiceFacade proxy;
        public VideoQueryForm()
        {
            InitializeComponent();

            this.comboBox1.DataSource = Configuration.Instance.Cameras;
            this.comboBox1.DisplayMember = "Name";

           
            setListViewColumns();
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }

            Camera selectedCamera = this.comboBox1.SelectedItem as Camera;


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

            string address = string.Format("net.tcp://{0}:8000/TcpService", selectedCamera.IpAddress);

            proxy = ServiceProxy.ProxyFactory.CreateProxy(address);


            Video[] videos = proxy.SearchVideos(selectedCamera.ID, dateTime1, dateTime2);

            if (videos.Length == 0)
            {
                MessageBox.Show("没有搜索到满足条件的视频！", "警告");
                return;
            }

            this.videoList.Items.Clear();

            foreach (Video v in videos)
            {
                string videoPath = v.Path;
                DateTime dTime = ImageSearch.getDateTimeStr(videoPath);//"2009-6-29 14:00:00"
                ListViewItem lvl = new ListViewItem();
                lvl.Text = dTime.ToString();
                lvl.SubItems.Add(videoPath);
                lvl.Tag = videoPath;

                if (this.faceCapturedRadioButton.Checked)
                {
                    if (v.HasFaceCaptured)
                    {
                        lvl.ImageIndex = 0;
                        videoList.Items.Add(lvl);
                    }
                }

                if (this.allVideoRadioButton.Checked)
                {
                    if (v.HasFaceCaptured)
                        lvl.ImageIndex = 0;
                    else
                        lvl.ImageIndex = 1;
                    videoList.Items.Add(lvl);
                }


            }
        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("抓拍时间", 150);
            videoList.Columns.Add("视频文件", 150);
            faceCapturedRadioButton.Checked = true;
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();
            this.Close();
        }


        private void ReceiveVideoStream()
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
            }

            this.axVLCPlugin21.playlist.items.clear();

            int idx = this.axVLCPlugin21.playlist.add(@"udp://@239.255.12.12", null, "-vvv");

            this.axVLCPlugin21.playlist.playItem(idx);
        }

        private void videoList_ItemActivate(object sender, EventArgs e)
        {
            bindPiclist();

            if (proxy == null) return;
            if (this.videoList.SelectedItems.Count == 0) return;
            if (string.IsNullOrEmpty(VideoPlayer.ExePath))
            {
                MessageBox.Show("请安装vlc播放器");
                return;
            }

            ListViewItem item = this.videoList.SelectedItems[0];

            proxy.BroadcastVideo(item.Tag as string);

            ReceiveVideoStream();
        }


        #region 绑定picList()

        void bindPiclist()
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();

            DateTime time = ImageSearch.getDateTimeStr(videoList.FocusedItem.Tag as string);

            Camera selectedCamera = this.comboBox1.SelectedItem as Camera;

            Bitmap[] faces = proxy.FacesCapturedAt(selectedCamera.ID, time);
            if (faces.Length == 0) return;

            foreach (var bmp in faces)
            {
                this.imageList1.Images.Add(bmp);
                //string text = System.IO.Path.GetFileName(fileArr[i]);
                ListViewItem item = new ListViewItem
                {
                    //Tag = fileArr[i].ToString(),
                    //Text = text,
                    ImageIndex = this.imageList1.Images.Count-1,
                };
                this.picList.Items.Add(item);
            }

            this.picList.Scrollable = true;
            this.picList.MultiSelect = false;
            this.picList.View = View.LargeIcon;
            this.picList.LargeImageList = imageList1;
        }

        #endregion

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }

            if (proxy != null)
            {
                proxy.KillPlayer();
            }

        }

        private void picList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("图片路径："+this.picList.FocusedItem.Tag.ToString());
            ShowDetailPic(ImageDetail.FromPath(this.picList.FocusedItem.Tag.ToString()));
        }

        private void ShowDetailPic(ImageDetail img)
        {
            FormDetailedPic detail = new FormDetailedPic();
            detail.Img = img;
            detail.ShowDialog(this);
            detail.Dispose();
        }
    }
}
