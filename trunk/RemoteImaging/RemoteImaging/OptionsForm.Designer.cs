﻿namespace RemoteImaging
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.browseForOutputFolder = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.browseForUploadFolder = new System.Windows.Forms.Button();
            this.textBoxUploadFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabelConfigCamera = new System.Windows.Forms.LinkLabel();
            this.dataGridCameras = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label26 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.downSamplingRatio = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.maxFaceWidth = new System.Windows.Forms.TextBox();
            this.minFaceWidth = new System.Windows.Forms.TextBox();
            this.bottomExtRatio = new System.Windows.Forms.TextBox();
            this.topExtRatio = new System.Windows.Forms.TextBox();
            this.rightExtRatio = new System.Windows.Forms.TextBox();
            this.leftExtRatio = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Location = new System.Drawing.Point(-6, -16);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(574, 87);
            this.groupControl1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "在这里设置系统参数，如上传目录，图片转存目录等等";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(113, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置系统参数";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(5, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 311);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.browseForOutputFolder);
            this.tabPage1.Controls.Add(this.textBoxOutputFolder);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.browseForUploadFolder);
            this.tabPage1.Controls.Add(this.textBoxUploadFolder);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(486, 285);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "目录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(66, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "注意: 图片输出目录和摄像头上传目录不要选择同一个目录。";
            // 
            // browseForOutputFolder
            // 
            this.browseForOutputFolder.Location = new System.Drawing.Point(381, 131);
            this.browseForOutputFolder.Name = "browseForOutputFolder";
            this.browseForOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.browseForOutputFolder.TabIndex = 5;
            this.browseForOutputFolder.Text = "浏览";
            this.browseForOutputFolder.UseVisualStyleBackColor = true;
            this.browseForOutputFolder.Click += new System.EventHandler(this.browseForOutputFolder_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "OutputPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(37, 133);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(328, 20);
            this.textBoxOutputFolder.TabIndex = 4;
            this.textBoxOutputFolder.Text = global::RemoteImaging.Properties.Settings.Default.OutputPath;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "图片输出目录:";
            // 
            // browseForUploadFolder
            // 
            this.browseForUploadFolder.Location = new System.Drawing.Point(381, 63);
            this.browseForUploadFolder.Name = "browseForUploadFolder";
            this.browseForUploadFolder.Size = new System.Drawing.Size(75, 23);
            this.browseForUploadFolder.TabIndex = 2;
            this.browseForUploadFolder.Text = "浏览";
            this.browseForUploadFolder.UseVisualStyleBackColor = true;
            this.browseForUploadFolder.Click += new System.EventHandler(this.browseForUploadFolder_Click);
            // 
            // textBoxUploadFolder
            // 
            this.textBoxUploadFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "ImageUploadPool", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUploadFolder.Location = new System.Drawing.Point(37, 65);
            this.textBoxUploadFolder.Name = "textBoxUploadFolder";
            this.textBoxUploadFolder.Size = new System.Drawing.Size(328, 20);
            this.textBoxUploadFolder.TabIndex = 1;
            this.textBoxUploadFolder.Text = global::RemoteImaging.Properties.Settings.Default.ImageUploadPool;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "摄像头图片上传目录:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabelConfigCamera);
            this.tabPage2.Controls.Add(this.dataGridCameras);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(486, 285);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "摄像头";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabelConfigCamera
            // 
            this.linkLabelConfigCamera.AutoSize = true;
            this.linkLabelConfigCamera.Location = new System.Drawing.Point(374, 16);
            this.linkLabelConfigCamera.Name = "linkLabelConfigCamera";
            this.linkLabelConfigCamera.Size = new System.Drawing.Size(91, 13);
            this.linkLabelConfigCamera.TabIndex = 1;
            this.linkLabelConfigCamera.TabStop = true;
            this.linkLabelConfigCamera.Text = "设置摄像头参数";
            this.linkLabelConfigCamera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelConfigCamera_LinkClicked);
            // 
            // dataGridCameras
            // 
            this.dataGridCameras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCameras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.ID,
            this.IP});
            this.dataGridCameras.Location = new System.Drawing.Point(16, 32);
            this.dataGridCameras.Name = "dataGridCameras";
            this.dataGridCameras.Size = new System.Drawing.Size(449, 247);
            this.dataGridCameras.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            // 
            // ID
            // 
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label26);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.downSamplingRatio);
            this.tabPage3.Controls.Add(this.textBox11);
            this.tabPage3.Controls.Add(this.textBox12);
            this.tabPage3.Controls.Add(this.textBox13);
            this.tabPage3.Controls.Add(this.textBox14);
            this.tabPage3.Controls.Add(this.maxFaceWidth);
            this.tabPage3.Controls.Add(this.minFaceWidth);
            this.tabPage3.Controls.Add(this.bottomExtRatio);
            this.tabPage3.Controls.Add(this.topExtRatio);
            this.tabPage3.Controls.Add(this.rightExtRatio);
            this.tabPage3.Controls.Add(this.leftExtRatio);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(486, 285);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "人像截取";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(21, 230);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 28;
            this.label26.Text = "降采样率：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(368, 184);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(31, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "高：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(261, 184);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(31, 13);
            this.label21.TabIndex = 23;
            this.label21.Text = "宽：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(148, 184);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(31, 13);
            this.label22.TabIndex = 21;
            this.label22.Text = "顶：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(51, 184);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(31, 13);
            this.label23.TabIndex = 19;
            this.label23.Text = "左：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(21, 157);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 13);
            this.label24.TabIndex = 18;
            this.label24.Text = "人像搜索区域";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(93, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(380, 2);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(238, 122);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 13);
            this.label19.TabIndex = 15;
            this.label19.Text = "最大脸宽：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(67, 122);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 13);
            this.label18.TabIndex = 13;
            this.label18.Text = "最小脸宽：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 95);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "人像截取阈值(像素)";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(93, 100);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(380, 2);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(368, 52);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(31, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "下：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "上：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "右：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "左：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "人像扩展比例";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(93, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 2);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // downSamplingRatio
            // 
            this.downSamplingRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "DownSampling", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.downSamplingRatio.Location = new System.Drawing.Point(93, 227);
            this.downSamplingRatio.Name = "downSamplingRatio";
            this.downSamplingRatio.Size = new System.Drawing.Size(51, 20);
            this.downSamplingRatio.TabIndex = 26;
            this.downSamplingRatio.Text = global::RemoteImaging.Properties.Settings.Default.DownSampling;
            // 
            // textBox11
            // 
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconBottomExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox11.Location = new System.Drawing.Point(400, 181);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(51, 20);
            this.textBox11.TabIndex = 24;
            this.textBox11.Text = global::RemoteImaging.Properties.Settings.Default.IconBottomExtRatio;
            // 
            // textBox12
            // 
            this.textBox12.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconTopExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox12.Location = new System.Drawing.Point(294, 181);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(51, 20);
            this.textBox12.TabIndex = 22;
            this.textBox12.Text = global::RemoteImaging.Properties.Settings.Default.IconTopExtRatio;
            // 
            // textBox13
            // 
            this.textBox13.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconRightExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox13.Location = new System.Drawing.Point(180, 181);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(51, 20);
            this.textBox13.TabIndex = 20;
            this.textBox13.Text = global::RemoteImaging.Properties.Settings.Default.IconRightExtRatio;
            // 
            // textBox14
            // 
            this.textBox14.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconLeftExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox14.Location = new System.Drawing.Point(83, 181);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(51, 20);
            this.textBox14.TabIndex = 16;
            this.textBox14.Text = global::RemoteImaging.Properties.Settings.Default.IconLeftExtRatio;
            // 
            // maxFaceWidth
            // 
            this.maxFaceWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "MaxFaceWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxFaceWidth.Location = new System.Drawing.Point(308, 119);
            this.maxFaceWidth.Name = "maxFaceWidth";
            this.maxFaceWidth.Size = new System.Drawing.Size(51, 20);
            this.maxFaceWidth.TabIndex = 14;
            this.maxFaceWidth.Text = global::RemoteImaging.Properties.Settings.Default.MaxFaceWidth;
            // 
            // minFaceWidth
            // 
            this.minFaceWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "MinFaceWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minFaceWidth.Location = new System.Drawing.Point(137, 119);
            this.minFaceWidth.Name = "minFaceWidth";
            this.minFaceWidth.Size = new System.Drawing.Size(51, 20);
            this.minFaceWidth.TabIndex = 12;
            this.minFaceWidth.Text = global::RemoteImaging.Properties.Settings.Default.MinFaceWidth;
            // 
            // bottomExtRatio
            // 
            this.bottomExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconBottomExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.bottomExtRatio.Location = new System.Drawing.Point(400, 49);
            this.bottomExtRatio.Name = "bottomExtRatio";
            this.bottomExtRatio.Size = new System.Drawing.Size(51, 20);
            this.bottomExtRatio.TabIndex = 8;
            this.bottomExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconBottomExtRatio;
            // 
            // topExtRatio
            // 
            this.topExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconTopExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.topExtRatio.Location = new System.Drawing.Point(294, 49);
            this.topExtRatio.Name = "topExtRatio";
            this.topExtRatio.Size = new System.Drawing.Size(51, 20);
            this.topExtRatio.TabIndex = 6;
            this.topExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconTopExtRatio;
            // 
            // rightExtRatio
            // 
            this.rightExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconRightExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rightExtRatio.Location = new System.Drawing.Point(180, 49);
            this.rightExtRatio.Name = "rightExtRatio";
            this.rightExtRatio.Size = new System.Drawing.Size(51, 20);
            this.rightExtRatio.TabIndex = 4;
            this.rightExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconRightExtRatio;
            // 
            // leftExtRatio
            // 
            this.leftExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconLeftExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.leftExtRatio.Location = new System.Drawing.Point(83, 49);
            this.leftExtRatio.Name = "leftExtRatio";
            this.leftExtRatio.Size = new System.Drawing.Size(51, 20);
            this.leftExtRatio.TabIndex = 0;
            this.leftExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconLeftExtRatio;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(343, 394);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(424, 394);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(21, 95);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 11;
            this.label12.Text = "人像截取阈值";
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(93, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(380, 2);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(370, 52);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "下:";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(400, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(51, 20);
            this.textBox5.TabIndex = 8;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(264, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 7;
            this.label14.Text = "上:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(294, 49);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(51, 20);
            this.textBox6.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(150, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "右:";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(180, 49);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(51, 20);
            this.textBox7.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(53, 52);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "左:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(21, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 13);
            this.label17.TabIndex = 2;
            this.label17.Text = "人像截取比例";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(93, 30);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(380, 2);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(83, 49);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(51, 20);
            this.textBox8.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(37, 247);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(45, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(504, 429);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button browseForOutputFolder;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseForUploadFolder;
        private System.Windows.Forms.TextBox textBoxUploadFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridCameras;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.LinkLabel linkLabelConfigCamera;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox maxFaceWidth;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox minFaceWidth;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bottomExtRatio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox topExtRatio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rightExtRatio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox leftExtRatio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox downSamplingRatio;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox14;
    }
}