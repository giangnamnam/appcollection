﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using System.Timers;

namespace RemoteImaging
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            InitCamDatagridView();

            this.envModes.SelectedIndex = Properties.Settings.Default.EnvMode;
        }

        private void InitCamDatagridView()
        {
            InitCamList();

            this.dataGridCameras.AutoGenerateColumns = false;
            this.dataGridCameras.Columns[0].DataPropertyName = "Name";
            this.dataGridCameras.Columns[1].DataPropertyName = "ID";
            this.dataGridCameras.Columns[2].DataPropertyName = "IpAddress";
        }


        private void InitCamList()
        {
            Configuration config = Configuration.Instance;

            camList.Clear();
            foreach (var cam in config.Cameras)
            {
                camList.Add(cam);
            }
        }
        private void browseForUploadFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.ImageUploadPool = dlg.SelectedPath;
            }

        }

        private void browseForOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.OutputPath = dlg.SelectedPath;
            }
        }

        public IList<Camera> Cameras
        {
            get
            {
                IList<Camera> cams = new List<Camera>();

                foreach (Camera item in camList)
                {
                    cams.Add(item);
                }

                return cams;
            }

            set
            {
                camList.Clear();
                foreach (Camera item in value)
                {
                    camList.Add(item);
                }

                bs = new BindingSource();
                bs.DataSource = camList;

                this.dataGridCameras.DataSource = bs;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void linkLabelConfigCamera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (bs.Current == null)
            {
                return;
            }

            Camera cam = bs.Current as Camera;
            if (string.IsNullOrEmpty(cam.IpAddress))
            {
                return;
            }

            using (FormConfigCamera form = new FormConfigCamera())
            {
                StringBuilder sb = new StringBuilder(form.Text);
                sb.Append("-[");
                sb.Append(cam.IpAddress);
                sb.Append("]");

                form.Navigate(cam.IpAddress);
                form.Text = sb.ToString();
                form.ShowDialog(this);
            }
        }



        private BindingList<Camera> camList =
            new BindingList<Camera>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.EnvMode = this.envModes.SelectedIndex;
        }


        #region 自定义设置按钮选中事件
        private void ckbImageAndVideo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbImageAndVideo.Checked)
            {
                textBox2.Enabled = true;
                rabOneDay.Enabled = false;
                rabTwoDay.Enabled = false;
                rabThrDay.Enabled = false;
            }
            else
            {
                textBox2.Enabled = false;
                rabOneDay.Enabled = true;
                rabTwoDay.Enabled = true;
                rabThrDay.Enabled = true;
            }
        }

        private void ckbDiskSet_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDiskSet.Checked)
            {
                textBox1.Enabled = true;
                comboBox1.Enabled = false;
                textBox1.Text = "";
            }
            else
            {
                comboBox1.Enabled = true;
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
        }
        #endregion

        FileHandle fileHandle = new FileHandle();

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 2;

            if (!fileHandle.IsXmlExists())
            {
                fileHandle.CreateXml();
                fileHandle.CreateElement(SaveNodeType.Video, "3", DateTime.Now.ToShortDateString());
                fileHandle.CreateElement(SaveNodeType.WarnDisk, "500MB", DateTime.Now.ToShortDateString());

            }
            InitRadiobutton(SaveNodeType.Video, gBoxImgFileSet);
            InitRadiobutton(SaveNodeType.WarnDisk, gBoxDiskSet);
            textBox2.Enabled = false;
            textBox1.Enabled = false;
        }

        #region 设置保存
        public void setImageAndVideo() //磁盘空间报警   未完善设置
        {
            if (ckbImageAndVideo.Checked)
            {
                string val = textBox2.Text.Trim();
                bool res = false;
                try
                {
                    int temp = Convert.ToInt32(val);
                    res = true;
                }
                catch (Exception ex)
                {
                    val = "3";
                    return;
                }
                if (res)
                    fileHandle.CreateElement(SaveNodeType.Video, val, DateTime.Now.ToString());
            }
            else
            {
                string val = GetSelectedValue(SaveNodeType.Video, gBoxImgFileSet);
                fileHandle.CreateElement(SaveNodeType.Video, val, DateTime.Now.ToString());

            }
        }

        public void setFileStore()
        {
            #region
            //if (ckbDiskSet.Checked)
            //{
            //    string val = textBox1.Text.Trim();
            //    if ((val.Length < 3) || (!val.Substring(val.Length - 2, 2).Equals("MB")))
            //    {
            //        ShowResDialog(1, "参数错误，应以MB结尾！");
            //        return;
            //    }
            //    else
            //    {
            //        fileHandle.CreateElement(SaveNodeType.WarnDisk, val, DateTime.Now.ToString());
            //    }
            //}
            //else
            //{
            //    string val = comboBox1.Text;
            //    if ((val.Length < 3) || (!val.Substring(val.Length - 2, 2).Equals("MB")))
            //    {
            //        ShowResDialog(10, "参数错误，应以MB结尾！");
            //        return;
            //    }
            //    else
            //    {
            //        fileHandle.CreateElement(SaveNodeType.WarnDisk, val, DateTime.Now.ToString());
            //    }
            //}
            #endregion

            if (ckbDiskSet.Checked)
            {
                string val = textBox1.Text.Trim();
                fileHandle.CreateElement(SaveNodeType.WarnDisk, val, DateTime.Now.ToString());
            }
            else
            {
                string val = comboBox1.Text;
                if (val == "")
                    val = "500MB";
                fileHandle.CreateElement(SaveNodeType.WarnDisk, val, DateTime.Now.ToString());
            }
        }
        #endregion

        #region 弹出窗口的操作
        public void ShowResDialog(int picIndex, string msg)
        {
            AlertSettingRes asr = new AlertSettingRes(msg, picIndex);
            asr.HeightMax = 169;
            asr.WidthMax = 175;
            asr.ScrollShow();
        }
        #endregion

        //初始化控件
        private void InitRadiobutton(SaveNodeType nodeType, GroupBox box)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(FileHandle.xmlPath);
            XmlNode xRoot = xmlDoc.ChildNodes.Item(1);
            XmlNode xNode = xRoot.SelectSingleNode("/Root/" + nodeType.ToString() + "/Value");
            string val = xNode.FirstChild.Value;
            if (nodeType != SaveNodeType.WarnDisk)
            {

                foreach (Control control in box.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton rb = control as RadioButton;

                        if ((rb.Text.Substring(0, rb.Text.Length - 1).Equals(val)) && rb.Enabled)
                        {
                            rb.Checked = true;
                        }
                    }
                }
            }
            else
            {
                comboBox1.SelectedText = xNode.FirstChild.Value;
            }

        }

        //获得radiobutton选中的值
        private string GetSelectedValue(SaveNodeType nodeType, GroupBox temp)
        {
            string val = "";
            if (nodeType != SaveNodeType.WarnDisk)
            {
                foreach (Control control in temp.Controls)
                {
                    if (control is RadioButton)
                    {
                        RadioButton rabTemp = control as RadioButton;
                        if ((rabTemp.Enabled == true) && (rabTemp.Checked == true))
                            val = rabTemp.Text.Substring(0, rabTemp.Text.Length - 1);
                    }
                }
            }
            else
            {
                val = comboBox1.Text;
            }
            return val;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string text = textBox2.Text.Trim();

        }

        private void drawMotionRect_CheckedChanged(object sender, EventArgs e)
        {
            MotionDetect.MotionDetect.SetDrawRect(this.drawMotionRect.Checked);
        }



    }
}