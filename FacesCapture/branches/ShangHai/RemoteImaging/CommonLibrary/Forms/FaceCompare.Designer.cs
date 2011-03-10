

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
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.compareButton = new DevExpress.XtraEditors.SimpleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new DevExpress.XtraEditors.GroupControl();
            this.currentPic = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.searchTo = new DevExpress.XtraEditors.TimeEdit();
            this.searchFrom = new DevExpress.XtraEditors.TimeEdit();
            this.choosePic = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new DevExpress.XtraEditors.GroupControl();
            this.targetPic = new System.Windows.Forms.PictureBox();
            this.cancelButton = new DevExpress.XtraEditors.SimpleButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.barManager1 = new DevExpress.XtraBars.BarManager();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.accuracyLabel = new DevExpress.XtraBars.BarButtonItem();
            this.accuracyTrackContainer = new DevExpress.XtraBars.PopupControlContainer();
            this.accuracyTrackBar = new DevExpress.XtraEditors.TrackBarControl();
            this.counter = new DevExpress.XtraBars.BarStaticItem();
            this.progressBar = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.repositoryItemTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemTrackBar();
            this.faceCollection = new DevExpress.Xpo.XPCollection();
            this.popupContainerControl1 = new DevExpress.XtraEditors.PopupContainerControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackContainer)).BeginInit();
            this.accuracyTrackContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).BeginInit();
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
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(233, 814);
            this.panel1.TabIndex = 0;
            // 
            // compareButton
            // 
            this.compareButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.compareButton.Enabled = false;
            this.compareButton.Location = new System.Drawing.Point(73, 516);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(87, 25);
            this.compareButton.TabIndex = 5;
            this.compareButton.Text = "比对";
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
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
            this.groupBox3.Location = new System.Drawing.Point(3, 553);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 255);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.Text = "待比较图片";
            // 
            // currentPic
            // 
            this.currentPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPic.Location = new System.Drawing.Point(2, 23);
            this.currentPic.Name = "currentPic";
            this.currentPic.Size = new System.Drawing.Size(222, 230);
            this.currentPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentPic.TabIndex = 0;
            this.currentPic.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.searchTo);
            this.groupBox2.Controls.Add(this.searchFrom);
            this.groupBox2.Location = new System.Drawing.Point(3, 339);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 140);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.Text = "比对参数";
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
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(73, 516);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(87, 25);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "取消";
            this.cancelButton.Visible = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(233, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 814);
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
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.galleryControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(236, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(655, 814);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "比较结果";
            // 
            // galleryControl1
            // 
            this.galleryControl1.Controls.Add(this.galleryControlClient1);
            this.galleryControl1.DesignGalleryGroupIndex = 0;
            this.galleryControl1.DesignGalleryItemIndex = 0;
            this.galleryControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // galleryControl1
            // 
            galleryItemGroup1.Caption = "Group1";
            this.galleryControl1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(88, 88);
            this.galleryControl1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.galleryControl1.Gallery.ShowGroupCaption = false;
            this.galleryControl1.Gallery.ShowItemText = true;
            this.galleryControl1.Location = new System.Drawing.Point(2, 23);
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(651, 789);
            this.galleryControl1.TabIndex = 0;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(630, 785);
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
            this.progressBar,
            this.counter,
            this.barSubItem2,
            this.accuracyLabel});
            this.barManager1.MaxItemId = 9;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemTrackBar1});
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
            new DevExpress.XtraBars.LinkPersistInfo(this.accuracyLabel, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.counter),
            new DevExpress.XtraBars.LinkPersistInfo(this.progressBar)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // accuracyLabel
            // 
            this.accuracyLabel.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.accuracyLabel.Caption = "准确度：中";
            this.accuracyLabel.DropDownControl = this.accuracyTrackContainer;
            this.accuracyLabel.Hint = "调整比对的准确度";
            this.accuracyLabel.Id = 8;
            this.accuracyLabel.Name = "accuracyLabel";
            // 
            // accuracyTrackContainer
            // 
            this.accuracyTrackContainer.AutoSize = true;
            this.accuracyTrackContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.accuracyTrackContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.accuracyTrackContainer.Controls.Add(this.labelControl3);
            this.accuracyTrackContainer.Controls.Add(this.labelControl2);
            this.accuracyTrackContainer.Controls.Add(this.labelControl1);
            this.accuracyTrackContainer.Controls.Add(this.accuracyTrackBar);
            this.accuracyTrackContainer.Location = new System.Drawing.Point(0, 0);
            this.accuracyTrackContainer.Manager = this.barManager1;
            this.accuracyTrackContainer.Name = "accuracyTrackContainer";
            this.accuracyTrackContainer.Size = new System.Drawing.Size(48, 153);
            this.accuracyTrackContainer.TabIndex = 10;
            this.accuracyTrackContainer.Visible = false;
            this.accuracyTrackContainer.CloseUp += new System.EventHandler(this.accuracyTrackContainer_CloseUp);
            // 
            // accuracyTrackBar
            // 
            this.accuracyTrackBar.EditValue = 1;
            this.accuracyTrackBar.Location = new System.Drawing.Point(0, 0);
            this.accuracyTrackBar.MenuManager = this.barManager1;
            this.accuracyTrackBar.Name = "accuracyTrackBar";
            this.accuracyTrackBar.Properties.Maximum = 2;
            this.accuracyTrackBar.Properties.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.accuracyTrackBar.Size = new System.Drawing.Size(45, 150);
            this.accuracyTrackBar.TabIndex = 0;
            this.accuracyTrackBar.Value = 1;
            // 
            // counter
            // 
            this.counter.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.counter.Caption = "已比对： 0  待比对： 0";
            this.counter.Id = 2;
            this.counter.Name = "counter";
            this.counter.TextAlignment = System.Drawing.StringAlignment.Near;
            this.counter.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // progressBar
            // 
            this.progressBar.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.progressBar.Caption = "progress";
            this.progressBar.Edit = this.repositoryItemProgressBar1;
            this.progressBar.EditValue = "0";
            this.progressBar.Id = 1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBar.Width = 152;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(891, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 814);
            this.barDockControlBottom.Size = new System.Drawing.Size(891, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 814);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(891, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 814);
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "barSubItem2";
            this.barSubItem2.Id = 4;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // repositoryItemTrackBar1
            // 
            this.repositoryItemTrackBar1.Name = "repositoryItemTrackBar1";
            // 
            // faceCollection
            // 
            this.faceCollection.LoadingEnabled = false;
            this.faceCollection.ObjectType = typeof(Damany.PortraitCapturer.DAL.DTO.Portrait);
            // 
            // popupContainerControl1
            // 
            this.popupContainerControl1.Location = new System.Drawing.Point(159, 488);
            this.popupContainerControl1.Name = "popupContainerControl1";
            this.popupContainerControl1.Size = new System.Drawing.Size(152, 205);
            this.popupContainerControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(33, 5);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(12, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "高";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(33, 69);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "中";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(33, 130);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(12, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "低";
            // 
            // FaceCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 842);
            this.Controls.Add(this.accuracyTrackContainer);
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
            this.Load += new System.EventHandler(this.FaceCompare_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackContainer)).EndInit();
            this.accuracyTrackContainer.ResumeLayout(false);
            this.accuracyTrackContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accuracyTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.faceCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupContainerControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DevExpress.XtraEditors.SimpleButton compareButton;
        private System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.Xpo.XPCollection faceCollection;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.BarEditItem progressBar;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.BarStaticItem counter;
        private DevExpress.XtraEditors.SimpleButton cancelButton;
        private DevExpress.XtraBars.BarButtonItem accuracyLabel;
        private DevExpress.XtraBars.PopupControlContainer accuracyTrackContainer;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemTrackBar repositoryItemTrackBar1;
        private DevExpress.XtraEditors.PopupContainerControl popupContainerControl1;
        private DevExpress.XtraEditors.TrackBarControl accuracyTrackBar;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}