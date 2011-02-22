namespace Damany.RemoteImaging.Common.Forms
{
    partial class FaceCompare
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.compareButton = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new DevExpress.XtraEditors.GroupControl();
            this.currentPic = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new DevExpress.XtraEditors.GroupControl();
            this.groupBox4 = new DevExpress.XtraEditors.GroupControl();
            this.radioGroup2 = new DevExpress.XtraEditors.RadioGroup();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTo = new DevExpress.XtraEditors.TimeEdit();
            this.searchFrom = new DevExpress.XtraEditors.TimeEdit();
            this.choosePic = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new DevExpress.XtraEditors.GroupControl();
            this.targetPic = new System.Windows.Forms.PictureBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.faceList = new Damany.RemoteImaging.Common.Controls.DoubleBufferedListView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.compareButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.choosePic);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 812);
            this.panel1.TabIndex = 0;
            // 
            // compareButton
            // 
            this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.compareButton.Enabled = false;
            this.compareButton.Location = new System.Drawing.Point(73, 586);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(87, 25);
            this.compareButton.TabIndex = 5;
            this.compareButton.Text = "比对";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(7, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 68);
            this.label3.TabIndex = 4;
            this.label3.Text = "选定一张照片后,从数据库中比较与该照片相似的图片";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.currentPic);
            this.groupBox3.Location = new System.Drawing.Point(3, 629);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 180);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.Text = "待比较图片";
            // 
            // currentPic
            // 
            this.currentPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPic.Location = new System.Drawing.Point(2, 23);
            this.currentPic.Name = "currentPic";
            this.currentPic.Size = new System.Drawing.Size(222, 155);
            this.currentPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentPic.TabIndex = 0;
            this.currentPic.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.searchTo);
            this.groupBox2.Controls.Add(this.searchFrom);
            this.groupBox2.Location = new System.Drawing.Point(3, 339);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 223);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.Text = "比对参数";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.radioGroup2);
            this.groupBox4.Location = new System.Drawing.Point(7, 126);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 89);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.Text = "准确度";
            // 
            // radioGroup2
            // 
            this.radioGroup2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup2.Location = new System.Drawing.Point(2, 23);
            this.radioGroup2.Name = "radioGroup2";
            this.radioGroup2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup2.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "高"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "中"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "低")});
            this.radioGroup2.Size = new System.Drawing.Size(205, 64);
            this.radioGroup2.TabIndex = 5;
            this.radioGroup2.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "结束时间:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始时间:";
            // 
            // searchTo
            // 
            this.searchTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTo.EditValue = new System.DateTime(2010, 3, 19, 0, 0, 0, 0);
            this.searchTo.Location = new System.Drawing.Point(10, 98);
            this.searchTo.Name = "searchTo";
            this.searchTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchTo.Properties.Mask.EditMask = "f";
            this.searchTo.Size = new System.Drawing.Size(209, 21);
            this.searchTo.TabIndex = 1;
            // 
            // searchFrom
            // 
            this.searchFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchFrom.EditValue = new System.DateTime(2010, 3, 19, 0, 0, 0, 0);
            this.searchFrom.Location = new System.Drawing.Point(10, 43);
            this.searchFrom.Name = "searchFrom";
            this.searchFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.searchFrom.Properties.Mask.EditMask = "f";
            this.searchFrom.Size = new System.Drawing.Size(209, 21);
            this.searchFrom.TabIndex = 0;
            // 
            // choosePic
            // 
            this.choosePic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.choosePic.Location = new System.Drawing.Point(73, 285);
            this.choosePic.Name = "choosePic";
            this.choosePic.Size = new System.Drawing.Size(87, 25);
            this.choosePic.TabIndex = 1;
            this.choosePic.Text = "浏览";
            this.choosePic.Click += new System.EventHandler(this.choosePic_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.targetPic);
            this.groupBox1.Location = new System.Drawing.Point(0, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 198);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.Text = "选定目标";
            // 
            // targetPic
            // 
            this.targetPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.targetPic.Location = new System.Drawing.Point(2, 23);
            this.targetPic.Name = "targetPic";
            this.targetPic.Size = new System.Drawing.Size(229, 173);
            this.targetPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.targetPic.TabIndex = 0;
            this.targetPic.TabStop = false;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(233, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 812);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Jpeg 文件|*.jpg";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // faceList
            // 
            this.faceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.faceList.LargeImageList = this.imageList1;
            this.faceList.Location = new System.Drawing.Point(2, 23);
            this.faceList.Name = "faceList";
            this.faceList.Size = new System.Drawing.Size(651, 787);
            this.faceList.TabIndex = 2;
            this.faceList.UseCompatibleStateImageBehavior = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.faceList);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(236, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(655, 812);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "比较结果";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barStaticItem1});
            this.barManager1.MaxItemId = 1;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "就绪";
            this.barStaticItem1.Id = 0;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(891, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 812);
            this.barDockControlBottom.Size = new System.Drawing.Size(891, 30);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 812);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(891, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 812);
            // 
            // FaceCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 842);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FaceCompare";
            this.ShowInTaskbar = false;
            this.Text = "人脸比对查询";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FaceCompare_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox4)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panel1;
        private DevExpress.XtraEditors.GroupControl groupBox2;
        private DevExpress.XtraEditors.SimpleButton choosePic;
        private DevExpress.XtraEditors.GroupControl groupBox1;
        private System.Windows.Forms.PictureBox targetPic;
        private DevExpress.XtraEditors.GroupControl groupBox3;
        private System.Windows.Forms.PictureBox currentPic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.TimeEdit searchTo;
        private DevExpress.XtraEditors.TimeEdit searchFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Splitter splitter1;
        private Damany.RemoteImaging.Common.Controls.DoubleBufferedListView faceList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.SimpleButton compareButton;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.GroupControl groupBox4;
        private DevExpress.XtraEditors.RadioGroup radioGroup2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}