﻿using System;
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
            setListViewColumns();
        }

        ImageSearch imgSearch = new ImageSearch();
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

            this.videoList.Items.Clear();
            
            foreach (string file in files)
            {
                DateTime dTime =imgSearch.getDateTimeStr(file);//"2009-6-29 14:00:00"
                ListViewItem lvl = new ListViewItem();

                #region
                if (radioButton1.Checked == true)
                {
                    if (imgSearch.getPicFiles(file, this.comboBox1.Text, true).Length > 0)
                    {
                        lvl.Text = file;
                        lvl.SubItems.Add(dTime.ToString());
                        videoList.Items.Add(lvl);
                        lvl.ImageIndex = 0;
                    }
                }

                if (radioButton2.Checked == true)
                {
                    lvl.Text = file;
                    lvl.SubItems.Add(dTime.ToString());
                    videoList.Items.Add(lvl);
                    if (imgSearch.getPicFiles(file, this.comboBox1.Text, true).Length > 0)
                        lvl.ImageIndex = 0;
                    else
                        lvl.ImageIndex = 1;
                }
                #endregion
            }
        }

        private void setListViewColumns()//添加ListView行头
        {
            videoList.Columns.Add("视频文件", 250);
            videoList.Columns.Add("抓拍时间", 120);
            radioButton1.Checked = true;
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

            bindPiclist();
        }


        #region 绑定picList()

        void bindPiclist()
        {
            this.picList.Clear();
            this.imageList1.Images.Clear();
            string[] fileArr =imgSearch. getPicFiles(videoList.FocusedItem.Text, this.comboBox1.Text,true);//得到图片路径
            if (fileArr.Length == 0)
            {
                MessageBox.Show("没有符合的图片", "警告");
                return;
            }
            for (int i = 0; i < fileArr.Length; ++i)
            {
                this.imageList1.Images.Add(Image.FromFile(fileArr[i]));
                string text = System.IO.Path.GetFileName(fileArr[i]);
                ListViewItem item = new ListViewItem()
                {
                    Tag = fileArr[i].ToString(),
                    Text = text,
                    ImageIndex = i
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
