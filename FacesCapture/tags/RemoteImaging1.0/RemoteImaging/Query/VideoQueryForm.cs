using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging.Query
{
    public partial class VideoQueryForm : Form
    {
        public VideoQueryForm()
        {
            InitializeComponent();
            foreach (Camera camera in Configuration.Instance.Cameras)
            {
                this.comboBox1.Items.Add(camera.ID.ToString());
            }

        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();

            if (this.comboBox1.Text == "" || this.comboBox1.Text == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }


            int cameraID = int.Parse(this.comboBox1.Text);


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


            string[] files = VideoSearch.FindVideos(cameraID, dateTime1, dateTime2);

            if (files == null)
            {
                MessageBox.Show("没有搜索到满足条件的视频！", "警告");
                return;
            }

            //this.picList.Scrollable = true;
            //this.picList.MultiSelect = false;
            //this.picList.View = View.LargeIcon;
            //this.picList.LargeImageList = imageList1;

            this.videoList.Items.Clear();
            foreach (var file in files)
            {
                this.videoList.Items.Add(file);
            }


        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();

            this.Close();
        }

        private void videoList_ItemActivate(object sender, EventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
            }

            this.axVLCPlugin21.playlist.items.clear();

            foreach (ListViewItem item in this.videoList.SelectedItems)
            {
                this.axVLCPlugin21.playlist.add(item.Text, null, null);
            }

            if (this.axVLCPlugin21.playlist.itemCount > 0)
            {
                this.axVLCPlugin21.playlist.play();
            }
        }

        private void VideoQueryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.axVLCPlugin21.playlist.isPlaying)
            {
                this.axVLCPlugin21.playlist.stop();
                System.Threading.Thread.Sleep(1000);
            }

            
        }
    }
}
