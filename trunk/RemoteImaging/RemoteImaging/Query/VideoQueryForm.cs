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
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.listView1.Clear();

            string cameraID = this.comboBox1.Text;

            if (cameraID == "" || cameraID == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID","警告");
                return;
            }


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

            string[] files = imageSearch.SearchImages(startDir, endDir, RemoteImaging.Query.ImageDirSys.SearchType.VideoType);
            if (files == null)
            {
                MessageBox.Show("没有搜索到满足条件的视频！", "警告");
                return;
            }

            this.listView1.Scrollable = true;
            this.listView1.MultiSelect = false;
            this.listView1.View = View.LargeIcon;
            this.listView1.LargeImageList = imageList1;

            for (int i = 0; i < files.Length; i++)
            {
                this.listView1.Items.Add(System.IO.Path.GetFileName(files[i]), 0);
            }

        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.listView1.Clear();
            this.imageList1.Images.Clear();

            this.Close();
        }
    }
}
