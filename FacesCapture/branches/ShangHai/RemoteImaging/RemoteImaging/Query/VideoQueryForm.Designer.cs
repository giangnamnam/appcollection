using System;

namespace RemoteImaging.Query
{
    partial class VideoQueryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoQueryForm));
            this.timeTO = new DevExpress.XtraEditors.TimeEdit();
            this.timeFrom = new DevExpress.XtraEditors.TimeEdit();
            this.faceImageList = new System.Windows.Forms.ImageList(this.components);
            this.imageListIcon = new System.Windows.Forms.ImageList(this.components);
            this.controlNavigator1 = new DevExpress.XtraEditors.ControlNavigator();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cameraIds = new DevExpress.XtraEditors.LookUpEdit();
            this.queryBtn = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.videoGrid = new DevExpress.XtraGrid.GridControl();
            this.videoGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditFace = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEditCar = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.status = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.wholePicture = new DevExpress.XtraEditors.PictureEdit();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.faceGalleryControl = new DevExpress.XtraBars.Ribbon.GalleryControl();
            this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.currentPageIndicator = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.session1 = new DevExpress.Xpo.Session();
            this.videoCollection = new DevExpress.Xpo.XPCollection();
            ((System.ComponentModel.ISupportInitialize)(this.timeTO.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIds.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.videoGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.faceGalleryControl)).BeginInit();
            this.faceGalleryControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // timeTO
            // 
            this.timeTO.EditValue = new System.DateTime(2010, 4, 6, 0, 0, 0, 0);
            this.timeTO.Location = new System.Drawing.Point(626, 42);
            this.timeTO.Name = "timeTO";
            this.timeTO.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeTO.Properties.Mask.EditMask = "F";
            this.timeTO.Size = new System.Drawing.Size(219, 21);
            this.timeTO.TabIndex = 22;
            // 
            // timeFrom
            // 
            this.timeFrom.EditValue = new System.DateTime(2010, 4, 6, 0, 0, 0, 0);
            this.timeFrom.Location = new System.Drawing.Point(348, 42);
            this.timeFrom.Name = "timeFrom";
            this.timeFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.timeFrom.Properties.Mask.EditMask = "F";
            this.timeFrom.Size = new System.Drawing.Size(219, 21);
            this.timeFrom.TabIndex = 21;
            // 
            // faceImageList
            // 
            this.faceImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.faceImageList.ImageSize = new System.Drawing.Size(80, 60);
            this.faceImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListIcon
            // 
            this.imageListIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcon.ImageStream")));
            this.imageListIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcon.Images.SetKeyName(0, "FaceIcon.gif");
            this.imageListIcon.Images.SetKeyName(1, "png-0005.png");
            this.imageListIcon.Images.SetKeyName(2, "png-0652.png");
            // 
            // controlNavigator1
            // 
            this.controlNavigator1.Buttons.Append.Visible = false;
            this.controlNavigator1.Buttons.CancelEdit.Visible = false;
            this.controlNavigator1.Buttons.Edit.Visible = false;
            this.controlNavigator1.Buttons.EndEdit.Visible = false;
            this.controlNavigator1.Buttons.First.Hint = "跳至第1页";
            this.controlNavigator1.Buttons.Last.Hint = "跳至最后一页";
            this.controlNavigator1.Buttons.Next.Visible = false;
            this.controlNavigator1.Buttons.NextPage.Hint = "后一页";
            this.controlNavigator1.Buttons.Prev.Visible = false;
            this.controlNavigator1.Buttons.PrevPage.Hint = "前一页";
            this.controlNavigator1.Buttons.Remove.Visible = false;
            this.controlNavigator1.Location = new System.Drawing.Point(6, 5);
            this.controlNavigator1.Name = "controlNavigator1";
            this.controlNavigator1.Size = new System.Drawing.Size(175, 28);
            this.controlNavigator1.TabIndex = 31;
            this.controlNavigator1.Text = "controlNavigator1";
            this.controlNavigator1.ButtonClick += new DevExpress.XtraEditors.NavigatorButtonClickEventHandler(this.controlNavigator1_ButtonClick);
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(2, 23);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(336, 210);
            this.axVLCPlugin21.TabIndex = 30;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(29, 44);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "摄像机";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(313, 45);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 25;
            this.labelControl3.Text = "从：";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(594, 45);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 26;
            this.labelControl4.Text = "到：";
            // 
            // cameraIds
            // 
            this.cameraIds.Location = new System.Drawing.Point(75, 41);
            this.cameraIds.Name = "cameraIds";
            this.cameraIds.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cameraIds.Size = new System.Drawing.Size(161, 21);
            this.cameraIds.TabIndex = 27;
            // 
            // queryBtn
            // 
            this.queryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.queryBtn.Location = new System.Drawing.Point(861, 37);
            this.queryBtn.Name = "queryBtn";
            this.queryBtn.Size = new System.Drawing.Size(87, 27);
            this.queryBtn.TabIndex = 29;
            this.queryBtn.Text = "查询";
            this.queryBtn.Click += new System.EventHandler(this.queryBtn_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 86);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(987, 456);
            this.panelControl1.TabIndex = 32;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 2);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.videoGrid);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(983, 416);
            this.splitContainerControl1.SplitterPosition = 253;
            this.splitContainerControl1.TabIndex = 31;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // videoGrid
            // 
            this.videoGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoGrid.Location = new System.Drawing.Point(0, 0);
            this.videoGrid.MainView = this.videoGridView;
            this.videoGrid.MenuManager = this.barManager1;
            this.videoGrid.Name = "videoGrid";
            this.videoGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEditFace,
            this.repositoryItemCheckEditCar});
            this.videoGrid.Size = new System.Drawing.Size(253, 416);
            this.videoGrid.TabIndex = 0;
            this.videoGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.videoGridView});
            this.videoGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.videoGrid_MouseDoubleClick);
            // 
            // videoGridView
            // 
            this.videoGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn2});
            this.videoGridView.GridControl = this.videoGrid;
            this.videoGridView.Name = "videoGridView";
            this.videoGridView.OptionsBehavior.Editable = false;
            this.videoGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "录制时间";
            this.gridColumn1.DisplayFormat.FormatString = "F";
            this.gridColumn1.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn1.FieldName = "CapturedAt";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 178;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "有人像";
            this.gridColumn3.ColumnEdit = this.repositoryItemCheckEditFace;
            this.gridColumn3.FieldName = "HasFaceCaptured";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 59;
            // 
            // repositoryItemCheckEditFace
            // 
            this.repositoryItemCheckEditFace.AutoHeight = false;
            this.repositoryItemCheckEditFace.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditFace.Name = "repositoryItemCheckEditFace";
            this.repositoryItemCheckEditFace.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEditFace.PictureChecked")));
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "有车牌";
            this.gridColumn2.ColumnEdit = this.repositoryItemCheckEditCar;
            this.gridColumn2.FieldName = "HasLicenseplateCaptured";
            this.gridColumn2.Name = "gridColumn2";
            // 
            // repositoryItemCheckEditCar
            // 
            this.repositoryItemCheckEditCar.AutoHeight = false;
            this.repositoryItemCheckEditCar.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            this.repositoryItemCheckEditCar.Name = "repositoryItemCheckEditCar";
            this.repositoryItemCheckEditCar.PictureChecked = ((System.Drawing.Image)(resources.GetObject("repositoryItemCheckEditCar.PictureChecked")));
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
            this.status});
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
            this.barDockControlTop.Size = new System.Drawing.Size(987, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 542);
            this.barDockControlBottom.Size = new System.Drawing.Size(987, 28);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 542);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(987, 0);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 542);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Horizontal = false;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel1.Controls.Add(this.splitterControl1);
            this.splitContainerControl2.Panel1.Controls.Add(this.groupControl3);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl4);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(725, 416);
            this.splitContainerControl2.SplitterPosition = 235;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.wholePicture);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(345, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(380, 235);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "放大图片";
            // 
            // wholePicture
            // 
            this.wholePicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wholePicture.Location = new System.Drawing.Point(2, 23);
            this.wholePicture.MenuManager = this.barManager1;
            this.wholePicture.Name = "wholePicture";
            this.wholePicture.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.wholePicture.Size = new System.Drawing.Size(376, 210);
            this.wholePicture.TabIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(340, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(5, 235);
            this.splitterControl1.TabIndex = 1;
            this.splitterControl1.TabStop = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.axVLCPlugin21);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl3.Location = new System.Drawing.Point(0, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(340, 235);
            this.groupControl3.TabIndex = 0;
            this.groupControl3.Text = "视频播放：";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.faceGalleryControl);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl4.Location = new System.Drawing.Point(0, 0);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(725, 176);
            this.groupControl4.TabIndex = 0;
            this.groupControl4.Text = "人像列表：";
            // 
            // faceGalleryControl
            // 
            this.faceGalleryControl.Controls.Add(this.galleryControlClient1);
            this.faceGalleryControl.DesignGalleryGroupIndex = 0;
            this.faceGalleryControl.DesignGalleryItemIndex = 0;
            this.faceGalleryControl.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // faceGalleryControl
            // 
            this.faceGalleryControl.Gallery.ImageSize = new System.Drawing.Size(64, 64);
            this.faceGalleryControl.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
            this.faceGalleryControl.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.faceGalleryControl.Gallery.ShowGroupCaption = false;
            this.faceGalleryControl.Location = new System.Drawing.Point(2, 23);
            this.faceGalleryControl.Name = "faceGalleryControl";
            this.faceGalleryControl.Size = new System.Drawing.Size(721, 151);
            this.faceGalleryControl.TabIndex = 0;
            this.faceGalleryControl.Text = "faceGalleryControl";
            // 
            // galleryControlClient1
            // 
            this.galleryControlClient1.GalleryControl = this.faceGalleryControl;
            this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
            this.galleryControlClient1.Size = new System.Drawing.Size(700, 147);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.currentPageIndicator);
            this.panelControl2.Controls.Add(this.controlNavigator1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(2, 418);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(983, 36);
            this.panelControl2.TabIndex = 1;
            // 
            // currentPageIndicator
            // 
            this.currentPageIndicator.Location = new System.Drawing.Point(210, 12);
            this.currentPageIndicator.Name = "currentPageIndicator";
            this.currentPageIndicator.Size = new System.Drawing.Size(51, 14);
            this.currentPageIndicator.TabIndex = 32;
            this.currentPageIndicator.Text = "第 0/0 页";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.queryBtn);
            this.groupControl1.Controls.Add(this.cameraIds);
            this.groupControl1.Controls.Add(this.timeFrom);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.timeTO);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(987, 86);
            this.groupControl1.TabIndex = 31;
            this.groupControl1.Text = "查询条件";
            // 
            // videoCollection
            // 
            this.videoCollection.ObjectType = typeof(Damany.PortraitCapturer.DAL.DTO.Video);
            this.videoCollection.Session = this.session1;
            // 
            // VideoQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 570);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "VideoQueryForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "搜索视频";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoQueryForm_FormClosing);
            this.Load += new System.EventHandler(this.VideoQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeTO.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cameraIds.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.videoGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEditCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wholePicture.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.faceGalleryControl)).EndInit();
            this.faceGalleryControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.videoCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList faceImageList;
        private System.Windows.Forms.ImageList imageListIcon;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private DevExpress.XtraEditors.TimeEdit timeTO;
        private DevExpress.XtraEditors.TimeEdit timeFrom;
        private DevExpress.XtraEditors.ControlNavigator controlNavigator1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit cameraIds;
        private DevExpress.XtraEditors.SimpleButton queryBtn;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarStaticItem status;
        private DevExpress.XtraEditors.LabelControl currentPageIndicator;
        private DevExpress.XtraGrid.GridControl videoGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView videoGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditFace;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEditCar;
        private DevExpress.XtraEditors.PictureEdit wholePicture;
        private DevExpress.XtraBars.Ribbon.GalleryControl faceGalleryControl;
        private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
        private DevExpress.Xpo.Session session1;
        private DevExpress.Xpo.XPCollection videoCollection;
    }
}