namespace RemoteImaging
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
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
            this.envModes = new DevExpress.XtraEditors.RadioGroup();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
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
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.browseForVideoDnTool = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.browseForFTPConsole = new System.Windows.Forms.Button();
            this.ftpExePath = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.VideoDnExePath = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.envModes.Properties)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Location = new System.Drawing.Point(-6, -15);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(574, 80);
            this.groupControl1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(110, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(293, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "在这里设置系统参数，如上传目录，图片转存目录等等";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(108, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置系统参数";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(39, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(5, 71);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 313);
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
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(486, 288);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "目录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(37, 228);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(66, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "注意: 图片输出目录和摄像头上传目录不要选择同一个目录。";
            // 
            // browseForOutputFolder
            // 
            this.browseForOutputFolder.Location = new System.Drawing.Point(381, 121);
            this.browseForOutputFolder.Name = "browseForOutputFolder";
            this.browseForOutputFolder.Size = new System.Drawing.Size(75, 21);
            this.browseForOutputFolder.TabIndex = 5;
            this.browseForOutputFolder.Text = "浏览";
            this.browseForOutputFolder.UseVisualStyleBackColor = true;
            this.browseForOutputFolder.Click += new System.EventHandler(this.browseForOutputFolder_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "OutputPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(37, 123);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(328, 21);
            this.textBoxOutputFolder.TabIndex = 4;
            this.textBoxOutputFolder.Text = global::RemoteImaging.Properties.Settings.Default.OutputPath;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "图片输出目录:";
            // 
            // browseForUploadFolder
            // 
            this.browseForUploadFolder.Location = new System.Drawing.Point(381, 58);
            this.browseForUploadFolder.Name = "browseForUploadFolder";
            this.browseForUploadFolder.Size = new System.Drawing.Size(75, 21);
            this.browseForUploadFolder.TabIndex = 2;
            this.browseForUploadFolder.Text = "浏览";
            this.browseForUploadFolder.UseVisualStyleBackColor = true;
            this.browseForUploadFolder.Click += new System.EventHandler(this.browseForUploadFolder_Click);
            // 
            // textBoxUploadFolder
            // 
            this.textBoxUploadFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "ImageUploadPool", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUploadFolder.Location = new System.Drawing.Point(37, 60);
            this.textBoxUploadFolder.Name = "textBoxUploadFolder";
            this.textBoxUploadFolder.Size = new System.Drawing.Size(328, 21);
            this.textBoxUploadFolder.TabIndex = 1;
            this.textBoxUploadFolder.Text = global::RemoteImaging.Properties.Settings.Default.ImageUploadPool;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "摄像头图片上传目录:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabelConfigCamera);
            this.tabPage2.Controls.Add(this.dataGridCameras);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(486, 288);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "摄像头";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabelConfigCamera
            // 
            this.linkLabelConfigCamera.AutoSize = true;
            this.linkLabelConfigCamera.Location = new System.Drawing.Point(374, 15);
            this.linkLabelConfigCamera.Name = "linkLabelConfigCamera";
            this.linkLabelConfigCamera.Size = new System.Drawing.Size(89, 12);
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
            this.dataGridCameras.Location = new System.Drawing.Point(16, 30);
            this.dataGridCameras.Name = "dataGridCameras";
            this.dataGridCameras.RowTemplate.Height = 23;
            this.dataGridCameras.Size = new System.Drawing.Size(449, 228);
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
            this.tabPage3.Controls.Add(this.envModes);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label22);
            this.tabPage3.Controls.Add(this.label23);
            this.tabPage3.Controls.Add(this.label24);
            this.tabPage3.Controls.Add(this.label19);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.label6);
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
            this.tabPage3.Controls.Add(this.label28);
            this.tabPage3.Controls.Add(this.label29);
            this.tabPage3.Controls.Add(this.label31);
            this.tabPage3.Controls.Add(this.label30);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(486, 288);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "人像截取";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(21, 219);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 28;
            this.label26.Text = "环境模式";
            // 
            // envModes
            // 
            this.envModes.Location = new System.Drawing.Point(78, 241);
            this.envModes.Name = "envModes";
            this.envModes.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.envModes.Properties.Appearance.Options.UseBackColor = true;
            this.envModes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.envModes.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "顺光"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "逆光"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "强逆光")});
            this.envModes.Size = new System.Drawing.Size(319, 26);
            this.envModes.TabIndex = 26;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(368, 180);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 25;
            this.label20.Text = "高：";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(261, 180);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 12);
            this.label21.TabIndex = 23;
            this.label21.Text = "宽：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(148, 180);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(29, 12);
            this.label22.TabIndex = 21;
            this.label22.Text = "顶：";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(51, 180);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(29, 12);
            this.label23.TabIndex = 19;
            this.label23.Text = "左：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(21, 155);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(77, 12);
            this.label24.TabIndex = 18;
            this.label24.Text = "人像搜索区域";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(238, 117);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 15;
            this.label19.Text = "最大脸宽：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(67, 117);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 12);
            this.label18.TabIndex = 13;
            this.label18.Text = "最小脸宽：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 92);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 12);
            this.label11.TabIndex = 11;
            this.label11.Text = "人像截取阈值(像素)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(368, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "下：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(261, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "上：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "右：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "左：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "人像扩展比例";
            // 
            // textBox11
            // 
            this.textBox11.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "SrchRegionHeight", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox11.Location = new System.Drawing.Point(400, 177);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(51, 21);
            this.textBox11.TabIndex = 24;
            this.textBox11.Text = global::RemoteImaging.Properties.Settings.Default.SrchRegionHeight;
            // 
            // textBox12
            // 
            this.textBox12.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "SrchRegionWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox12.Location = new System.Drawing.Point(294, 177);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(51, 21);
            this.textBox12.TabIndex = 22;
            this.textBox12.Text = global::RemoteImaging.Properties.Settings.Default.SrchRegionWidth;
            // 
            // textBox13
            // 
            this.textBox13.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "SrchRegionTop", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox13.Location = new System.Drawing.Point(180, 177);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(51, 21);
            this.textBox13.TabIndex = 20;
            this.textBox13.Text = global::RemoteImaging.Properties.Settings.Default.SrchRegionTop;
            // 
            // textBox14
            // 
            this.textBox14.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "SrchRegionLeft", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox14.Location = new System.Drawing.Point(83, 177);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(51, 21);
            this.textBox14.TabIndex = 16;
            this.textBox14.Text = global::RemoteImaging.Properties.Settings.Default.SrchRegionLeft;
            // 
            // maxFaceWidth
            // 
            this.maxFaceWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "MaxFaceWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.maxFaceWidth.Location = new System.Drawing.Point(308, 114);
            this.maxFaceWidth.Name = "maxFaceWidth";
            this.maxFaceWidth.Size = new System.Drawing.Size(51, 21);
            this.maxFaceWidth.TabIndex = 14;
            this.maxFaceWidth.Text = global::RemoteImaging.Properties.Settings.Default.MaxFaceWidth;
            // 
            // minFaceWidth
            // 
            this.minFaceWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "MinFaceWidth", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.minFaceWidth.Location = new System.Drawing.Point(137, 114);
            this.minFaceWidth.Name = "minFaceWidth";
            this.minFaceWidth.Size = new System.Drawing.Size(51, 21);
            this.minFaceWidth.TabIndex = 12;
            this.minFaceWidth.Text = global::RemoteImaging.Properties.Settings.Default.MinFaceWidth;
            // 
            // bottomExtRatio
            // 
            this.bottomExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconBottomExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.bottomExtRatio.Location = new System.Drawing.Point(400, 45);
            this.bottomExtRatio.Name = "bottomExtRatio";
            this.bottomExtRatio.Size = new System.Drawing.Size(51, 21);
            this.bottomExtRatio.TabIndex = 8;
            this.bottomExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconBottomExtRatio;
            // 
            // topExtRatio
            // 
            this.topExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconTopExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.topExtRatio.Location = new System.Drawing.Point(294, 45);
            this.topExtRatio.Name = "topExtRatio";
            this.topExtRatio.Size = new System.Drawing.Size(51, 21);
            this.topExtRatio.TabIndex = 6;
            this.topExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconTopExtRatio;
            // 
            // rightExtRatio
            // 
            this.rightExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconRightExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.rightExtRatio.Location = new System.Drawing.Point(180, 45);
            this.rightExtRatio.Name = "rightExtRatio";
            this.rightExtRatio.Size = new System.Drawing.Size(51, 21);
            this.rightExtRatio.TabIndex = 4;
            this.rightExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconRightExtRatio;
            // 
            // leftExtRatio
            // 
            this.leftExtRatio.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "IconLeftExtRatio", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.leftExtRatio.Location = new System.Drawing.Point(83, 45);
            this.leftExtRatio.Name = "leftExtRatio";
            this.leftExtRatio.Size = new System.Drawing.Size(51, 21);
            this.leftExtRatio.TabIndex = 0;
            this.leftExtRatio.Text = global::RemoteImaging.Properties.Settings.Default.IconLeftExtRatio;
            // 
            // label28
            // 
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label28.Location = new System.Drawing.Point(49, 30);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(410, 2);
            this.label28.TabIndex = 29;
            // 
            // label29
            // 
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label29.Location = new System.Drawing.Point(49, 99);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(410, 2);
            this.label29.TabIndex = 30;
            // 
            // label31
            // 
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label31.Location = new System.Drawing.Point(49, 224);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(410, 2);
            this.label31.TabIndex = 32;
            // 
            // label30
            // 
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label30.Location = new System.Drawing.Point(49, 161);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(410, 2);
            this.label30.TabIndex = 31;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.browseForVideoDnTool);
            this.tabPage4.Controls.Add(this.label25);
            this.tabPage4.Controls.Add(this.browseForFTPConsole);
            this.tabPage4.Controls.Add(this.ftpExePath);
            this.tabPage4.Controls.Add(this.label27);
            this.tabPage4.Controls.Add(this.VideoDnExePath);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(486, 288);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "工具";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // browseForVideoDnTool
            // 
            this.browseForVideoDnTool.Location = new System.Drawing.Point(374, 149);
            this.browseForVideoDnTool.Name = "browseForVideoDnTool";
            this.browseForVideoDnTool.Size = new System.Drawing.Size(75, 21);
            this.browseForVideoDnTool.TabIndex = 11;
            this.browseForVideoDnTool.Text = "浏览";
            this.browseForVideoDnTool.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(27, 136);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(107, 12);
            this.label25.TabIndex = 9;
            this.label25.Text = "视频下载工具目录:";
            // 
            // browseForFTPConsole
            // 
            this.browseForFTPConsole.Location = new System.Drawing.Point(374, 58);
            this.browseForFTPConsole.Name = "browseForFTPConsole";
            this.browseForFTPConsole.Size = new System.Drawing.Size(75, 21);
            this.browseForFTPConsole.TabIndex = 8;
            this.browseForFTPConsole.Text = "浏览";
            this.browseForFTPConsole.UseVisualStyleBackColor = true;
            // 
            // ftpExePath
            // 
            this.ftpExePath.Location = new System.Drawing.Point(30, 60);
            this.ftpExePath.Name = "ftpExePath";
            this.ftpExePath.Size = new System.Drawing.Size(328, 21);
            this.ftpExePath.TabIndex = 7;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(27, 45);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(101, 12);
            this.label27.TabIndex = 6;
            this.label27.Text = "FTP管理工具目录:";
            // 
            // VideoDnExePath
            // 
            this.VideoDnExePath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "VideoDnTool", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.VideoDnExePath.Location = new System.Drawing.Point(30, 150);
            this.VideoDnExePath.Name = "VideoDnExePath";
            this.VideoDnExePath.Size = new System.Drawing.Size(328, 21);
            this.VideoDnExePath.TabIndex = 10;
            this.VideoDnExePath.Text = global::RemoteImaging.Properties.Settings.Default.VideoDnTool;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(344, 398);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 21);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(425, 398);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
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
            this.textBox5.Size = new System.Drawing.Size(51, 21);
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
            this.textBox6.Size = new System.Drawing.Size(51, 21);
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
            this.textBox7.Size = new System.Drawing.Size(51, 21);
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
            this.textBox8.Size = new System.Drawing.Size(51, 21);
            this.textBox8.TabIndex = 0;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(504, 422);
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.envModes.Properties)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox bottomExtRatio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox topExtRatio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox rightExtRatio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button browseForVideoDnTool;
        private System.Windows.Forms.TextBox VideoDnExePath;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button browseForFTPConsole;
        private System.Windows.Forms.TextBox ftpExePath;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private DevExpress.XtraEditors.RadioGroup envModes;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
    }
}