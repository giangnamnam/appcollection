namespace RemoteImaging.Query
{
    partial class PicQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PicQueryForm));
            this.timeFrom = new DevExpress.XtraEditors.TimeEdit();
            this.timeTo = new DevExpress.XtraEditors.TimeEdit();
            this.faceImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.currentFace = new DevExpress.XtraEditors.PictureEdit();
            this.portraits = new DevExpress.Xpo.XPCollection();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.status = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel3 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.queryBtn = new DevExpress.XtraEditors.SimpleButton();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.wholePicture = new DevExpress.XtraEditors.PictureEdit();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.galleryControl1 = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.currPage = new DevExpress.XtraEditors.LabelControl();
            this.navigator = new DevExpress.XtraEditors.ControlNavigator();
            this.playRelatedVideo = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentFace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel3.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanel2.SuspendLayout();
            this.dockPanel2_Container.SuspendLayout();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).BeginInit();
            this.galleryControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeFrom
            // 
            this.timeFrom.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeFrom.Location = new System.Drawing.Point(51, 20);
            this.timeFrom.Name = "timeFrom";
            this.timeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeFrom.Properties.Mask.EditMask = "F";
            this.timeFrom.Size = new System.Drawing.Size(206, 21);
            this.timeFrom.TabIndex = 7;
            // 
            // timeTo
            // 
            this.timeTo.EditValue = new System.DateTime(2009, 5, 7, 0, 0, 0, 0);
            this.timeTo.Location = new System.Drawing.Point(352, 20);
            this.timeTo.Name = "timeTo";
            this.timeTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeTo.Properties.Mask.EditMask = "F";
            this.timeTo.Size = new System.Drawing.Size(198, 21);
            this.timeTo.TabIndex = 8;
            // 
            // faceImageList
            // 
            this.faceImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.faceImageList.ImageSize = new System.Drawing.Size(80, 60);
            this.faceImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageList2
            // 
            this.imageList2.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList2.ImageSize = new System.Drawing.Size(80, 60);
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // currentFace
            // 
            this.currentFace.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.portraits, "ImageCopy", true));
            this.currentFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentFace.Location = new System.Drawing.Point(0, 0);
            this.currentFace.MenuManager = this.barManager1;
            this.currentFace.Name = "currentFace";
            this.currentFace.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.currentFace.Size = new System.Drawing.Size(341, 196);
            this.currentFace.TabIndex = 4;
            // 
            // portraits
            // 
            this.portraits.ObjectType = typeof(Damany.PortraitCapturer.DAL.DTO.Portrait);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.status,
            this.barButtonItem1,
            this.barButtonItem2});
            this.barManager1.MaxItemId = 3;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.status)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // status
            // 
            this.status.Caption = "就绪";
            this.status.Id = 0;
            this.status.Name = "status";
            this.status.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(918, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 546);
            this.barDockControlBottom.Size = new System.Drawing.Size(918, 27);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 546);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(918, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 546);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel3,
            this.panelContainer1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // dockPanel3
            // 
            this.dockPanel3.Controls.Add(this.dockPanel3_Container);
            this.dockPanel3.Dock = DevExpress.XtraBars.Docking.DockingStyle.Top;
            this.dockPanel3.FloatVertical = true;
            this.dockPanel3.ID = new System.Guid("63335e45-2cfc-4788-abb7-e0cc172b6540");
            this.dockPanel3.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3.Name = "dockPanel3";
            this.dockPanel3.Options.AllowFloating = false;
            this.dockPanel3.Options.ShowAutoHideButton = false;
            this.dockPanel3.Options.ShowCloseButton = false;
            this.dockPanel3.OriginalSize = new System.Drawing.Size(200, 96);
            this.dockPanel3.Size = new System.Drawing.Size(918, 96);
            this.dockPanel3.Text = "搜索条件";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.labelControl6);
            this.dockPanel3_Container.Controls.Add(this.timeTo);
            this.dockPanel3_Container.Controls.Add(this.timeFrom);
            this.dockPanel3_Container.Controls.Add(this.labelControl5);
            this.dockPanel3_Container.Controls.Add(this.queryBtn);
            this.dockPanel3_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(910, 69);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(322, 23);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 11;
            this.labelControl6.Text = "到：";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(16, 24);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(24, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "从：";
            // 
            // queryBtn
            // 
            this.queryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBtn.Location = new System.Drawing.Point(806, 14);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(87, 27);
            this.queryBtn.TabIndex = 9;
            this.queryBtn.Text = "查询";
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // panelContainer1
            // 
            this.panelContainer1.Controls.Add(this.dockPanel2);
            this.panelContainer1.Controls.Add(this.dockPanel1);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.panelContainer1.ID = new System.Guid("fe5214ec-e932-4925-ba0b-91e301ff69aa");
            this.panelContainer1.Location = new System.Drawing.Point(569, 96);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(349, 200);
            this.panelContainer1.Size = new System.Drawing.Size(349, 450);
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel2
            // 
            this.dockPanel2.Controls.Add(this.dockPanel2_Container);
            this.dockPanel2.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel2.ID = new System.Guid("bbe3b38f-0089-4a8d-a0d6-7f49fecb50e7");
            this.dockPanel2.Location = new System.Drawing.Point(0, 0);
            this.dockPanel2.Name = "dockPanel2";
            this.dockPanel2.Options.ShowCloseButton = false;
            this.dockPanel2.OriginalSize = new System.Drawing.Size(407, 220);
            this.dockPanel2.Size = new System.Drawing.Size(349, 223);
            this.dockPanel2.Text = "放大";
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Controls.Add(this.currentFace);
            this.dockPanel2_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(341, 196);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel1.ID = new System.Guid("7c1444f7-4c06-4293-acc6-c4a6e7b06b5a");
            this.dockPanel1.Location = new System.Drawing.Point(0, 223);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Options.ShowCloseButton = false;
            this.dockPanel1.OriginalSize = new System.Drawing.Size(349, 226);
            this.dockPanel1.Size = new System.Drawing.Size(349, 227);
            this.dockPanel1.Text = "全景";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.wholePicture);
            this.dockPanel1_Container.Location = new System.Drawing.Point(4, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(341, 200);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // wholePicture
            // 
            this.wholePicture.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.portraits, "Frame.ImageCopy", true));
            this.wholePicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wholePicture.Location = new System.Drawing.Point(0, 0);
            this.wholePicture.Name = "wholePicture";
            this.wholePicture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.wholePicture.Size = new System.Drawing.Size(341, 200);
            this.wholePicture.TabIndex = 0;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "相关视频";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "另存图片";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
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
            this.galleryControl1.Gallery.HoverImageSize = new System.Drawing.Size(128, 128);
            this.galleryControl1.Gallery.ImageSize = new System.Drawing.Size(64, 64);
            this.galleryControl1.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.galleryControl1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.galleryControl1.Location = new System.Drawing.Point(2, 40);
            this.galleryControl1.LookAndFeel.SkinName = "Darkroom";
            this.galleryControl1.Name = "galleryControl1";
            this.galleryControl1.Size = new System.Drawing.Size(565, 370);
            this.galleryControl1.TabIndex = 10;
            this.galleryControl1.Text = "galleryControl1";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.galleryControl1;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(544, 366);
            // 
            // panelControl1
            // 
            this.panelControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelControl1.Controls.Add(this.currPage);
            this.panelControl1.Controls.Add(this.navigator);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 410);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(565, 38);
            this.panelControl1.TabIndex = 1;
            // 
            // currPage
            // 
            this.currPage.Location = new System.Drawing.Point(208, 10);
            this.currPage.Name = "currPage";
            this.currPage.Size = new System.Drawing.Size(51, 14);
            this.currPage.TabIndex = 34;
            this.currPage.Text = "第 0/0 页";
            // 
            // navigator
            // 
            this.navigator.Buttons.Append.Visible = false;
            this.navigator.Buttons.CancelEdit.Visible = false;
            this.navigator.Buttons.Edit.Visible = false;
            this.navigator.Buttons.EndEdit.Visible = false;
            this.navigator.Buttons.First.Hint = "跳至第1页";
            this.navigator.Buttons.Last.Hint = "跳至最后一页";
            this.navigator.Buttons.Next.Visible = false;
            this.navigator.Buttons.NextPage.Hint = "后一页";
            this.navigator.Buttons.Prev.Visible = false;
            this.navigator.Buttons.PrevPage.Hint = "前一页";
            this.navigator.Buttons.Remove.Visible = false;
            this.navigator.Location = new System.Drawing.Point(6, 6);
            this.navigator.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.navigator.Name = "navigator";
            this.navigator.Size = new System.Drawing.Size(181, 28);
            this.navigator.TabIndex = 32;
            this.navigator.Text = "controlNavigator1";
            this.navigator.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.navigator_ButtonClick);
            // 
            // playRelatedVideo
            // 
            this.playRelatedVideo.Location = new System.Drawing.Point(14, 5);
            this.playRelatedVideo.Name = "playRelatedVideo";
            this.playRelatedVideo.Size = new System.Drawing.Size(87, 27);
            this.playRelatedVideo.TabIndex = 7;
            this.playRelatedVideo.Text = "相关视频";
            this.playRelatedVideo.Click += new System.EventHandler(this.toolStripButtonPlayVideo_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.galleryControl1);
            this.panelControl4.Controls.Add(this.panelControl2);
            this.panelControl4.Controls.Add(this.panelControl1);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl4.Location = new System.Drawing.Point(0, 96);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(569, 450);
            this.panelControl4.TabIndex = 23;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.playRelatedVideo);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(565, 38);
            this.panelControl2.TabIndex = 11;
            // 
            // PicQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 573);
            this.Controls.Add(this.panelControl4);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dockPanel3);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PicQueryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "人像搜索";
            this.Load += new System.EventHandler(this.PicQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentFace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel3.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            this.dockPanel3_Container.PerformLayout();
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel2.ResumeLayout(false);
            this.dockPanel2_Container.ResumeLayout(false);
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.galleryControl1)).EndInit();
            this.galleryControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit timeFrom;
        private DevExpress.XtraEditors.TimeEdit timeTo;
        private System.Windows.Forms.ImageList faceImageList;
        private System.Windows.Forms.ImageList imageList2;
        private DevExpress.XtraEditors.SimpleButton queryBtn;
        private DevExpress.XtraEditors.PictureEdit wholePicture;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.ControlNavigator navigator;
        private DevExpress.XtraBars.BarStaticItem status;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl currPage;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraEditors.SimpleButton playRelatedVideo;
        private DevExpress.XtraEditors.PictureEdit currentFace;
        private DevExpress.Xpo.XPCollection portraits;
        private DevExpress.XtraBars.Ribbon.GalleryControl galleryControl1;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel3;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
    }
}