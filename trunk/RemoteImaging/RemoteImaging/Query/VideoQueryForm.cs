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
            string cameraID = this.comboBox1.Text;

            if (cameraID == "" || cameraID == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID","警告");
                return;
            }

            string dataStr1 = this.dateTimePicker1.Text;
            int indexYear1 = dataStr1.IndexOf("年");
            int indexMonth1 = dataStr1.IndexOf("月");
            int indexDay1 = dataStr1.IndexOf("日");

            string year1 = dataStr1.Substring(0, 4);
            string month1 = int.Parse(dataStr1.Substring(indexYear1 + 1, indexMonth1 - indexYear1 - 1)).ToString("D2");
            string day1 = int.Parse(dataStr1.Substring(indexMonth1 + 1, indexDay1 - indexMonth1 - 1)).ToString("D2");

            string timeStr1 = this.timeEdit1.Text;
            int index1 = timeStr1.IndexOf(":");
            int index2 = timeStr1.LastIndexOf(":");

            string hour1 = timeStr1.Substring(0, index1);
            string minute1 = timeStr1.Substring(index1 + 1, index2 - index1 - 1);
            string second1 = timeStr1.Substring(index2 + 1, timeStr1.Length - index2 - 1);

            string dataStr2 = this.dateTimePicker2.Text;
            int indexYear2 = dataStr2.IndexOf("年");
            int indexMonth2 = dataStr2.IndexOf("月");
            int indexDay2 = dataStr2.IndexOf("日");

            string year2 = dataStr2.Substring(0, 4);
            string month2 = int.Parse(dataStr2.Substring(indexYear2 + 1, indexMonth2 - indexYear2 - 1)).ToString("D2");
            string day2 = int.Parse(dataStr2.Substring(indexMonth2 + 1, indexDay2 - indexMonth2 - 1)).ToString("D2");

            string timeStr2 = this.timeEdit2.Text;
            int index3 = timeStr2.IndexOf(":");
            int index4 = timeStr2.LastIndexOf(":");

            string hour2 = timeStr2.Substring(0, index3);
            string minute2 = timeStr2.Substring(index3 + 1, index4 - index3 - 1);
            string second2 = timeStr2.Substring(index4 + 1, timeStr2.Length - index4 - 1);

            //judge the input validation
            DateTime dateTime1 = new DateTime(int.Parse(year1), int.Parse(month1), int.Parse(day1), int.Parse(hour1), int.Parse(minute1), int.Parse(second1));
            DateTime dateTime2 = new DateTime(int.Parse(year2), int.Parse(month2), int.Parse(day2), int.Parse(hour2), int.Parse(minute2), int.Parse(second2));
            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！", "警告");
                return;
            }
            //

            this.listView1.Clear();

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
