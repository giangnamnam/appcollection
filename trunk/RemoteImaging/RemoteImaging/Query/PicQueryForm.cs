using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteImaging.Query
{
    public partial class PicQueryForm : Form
    {
        public PicQueryForm()
        {
            InitializeComponent();
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            this.bestPicListView.Clear();
            this.imageList1.Images.Clear();

            string cameraID = this.comboBox1.Text;
            if (cameraID == "" || cameraID == null)
            {
                MessageBox.Show("请选择要查询的摄像头ID", "警告");
                return;
            }

            
            //judge the input validation
            DateTime date1 = this.dateTimePicker1.Value;
            DateTime date2 = this.dateTimePicker2.Value;
            DateTime time1 = this.timeEdit1.Time;
            DateTime time2 = this.timeEdit2.Time;             

            DateTime dateTime1 = new DateTime(date1.Year,date1.Month,date1.Day,time1.Hour,time1.Minute,time1.Second);
            DateTime dateTime2 = new DateTime(date2.Year,date2.Month,date2.Day,time2.Hour,time2.Minute,time2.Second);
            if (dateTime1 >= dateTime2)
            {
                MessageBox.Show("时间起点不应该大于或者等于时间终点，请重新输入！","警告");
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

            string[] files = imageSearch.SearchImages(startDir, endDir, RemoteImaging.Query.ImageDirSys.SearchType.PicType);
            if (files == null)
            {
                MessageBox.Show("没有搜索到满足条件的图片！","警告");
                return;
            }

            this.bestPicListView.Scrollable = true;
            this.bestPicListView.MultiSelect = false;
            this.bestPicListView.View = View.LargeIcon;
            this.bestPicListView.LargeImageList = imageList1;

            for (int i = 0; i < files.Length; i++)
            {
                this.imageList1.Images.Add(Image.FromFile(files[i]));
                this.bestPicListView.Items.Add(System.IO.Path.GetFileName(files[i]), i);
            }

        }

        private void bestPicListView_ItemActivate(object sender, System.EventArgs e)
        {
            this.secPicListView.Clear();
            this.imageList2.Images.Clear();

            Query.ImageSearch imageSearch = new ImageSearch();
            string[] files = imageSearch.SelectedBestImageChanged(this.bestPicListView.FocusedItem.Text);
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
                this.secPicListView.Items.Add(System.IO.Path.GetFileNameWithoutExtension(files[i]), i);
            }

            string filePath = RemoteImaging.Query.ImageDirSys.BeginDir + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(0, 2) + "\\" +
                              (2000 + int.Parse(this.bestPicListView.FocusedItem.Text.Substring(3, 2))).ToString() + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(5, 2) + "\\" +
                              this.bestPicListView.FocusedItem.Text.Substring(7, 2) + "\\" + Query.ImageDirSys.IconPath + "\\" + this.bestPicListView.FocusedItem.Text;

            //show modify icon
            if (File.Exists(filePath))
            {
                this.pictureBox1.Image = Image.FromFile(filePath);
            }
            //

            //detail infomation
            Camera camera = new Camera();
            camera.ID = 01;
            camera.Name = "四川大学南大门摄像头";
            this.gotPlaceTxt.Text = "四川大学南大门摄像头";

            string focusedFileName = this.bestPicListView.FocusedItem.Text;
            this.gotTimeTxt.Text = (2000 + int.Parse(focusedFileName.Substring(3, 2))).ToString() + "年" + //year
                                   focusedFileName.Substring(5, 2) + "月" + //month
                                   focusedFileName.Substring(7, 2) + "日" + //day
                                   focusedFileName.Substring(9, 2) + "时" + //hour
                                   focusedFileName.Substring(11, 2) + "分" + //minute
                                   focusedFileName.Substring(13, 2) + "妙";//second

            //
        }

        private void bestPicListView_DoubleClick(object sender, System.EventArgs e)
        {
            //MessageBox.Show("double click");
        }

        private void secPicListView_ItemActive(object sender, System.EventArgs e)
        {
            string filePath = RemoteImaging.Query.ImageDirSys.BeginDir + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(0, 2) + "\\" +
                              (2000 + int.Parse(this.secPicListView.FocusedItem.Text.Substring(3, 2))).ToString() + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(5, 2) + "\\" +
                              this.secPicListView.FocusedItem.Text.Substring(7, 2) + "\\" + Query.ImageDirSys.BeginDir + "\\" + this.secPicListView.FocusedItem.Text;

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
    }
}
