namespace RemoteImaging.LicensePlate
{
    partial class FormLicensePlateQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLicensePlateQuery));
            this.to = new DevExpress.XtraEditors.TimeEdit();
            this.from = new DevExpress.XtraEditors.TimeEdit();
            this.licensePlateImageList = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.licenseplateNumberToSearch = new DevExpress.XtraEditors.MRUEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.matchTimeRange = new DevExpress.XtraEditors.CheckEdit();
            this.matchLicenseNumber = new DevExpress.XtraEditors.CheckEdit();
            this.licensePlateLists = new DevExpress.XtraGrid.GridControl();
            this.licensePlateListView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.layoutViewColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemPictureEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bigImage = new QAlbum.ScalablePictureBox();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.currentLicenseplateCaptureTime = new DevExpress.XtraEditors.TextEdit();
            this.currentLicenseplateNumber = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.playRelatedVideo = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.licenseplateNumberToSearch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchTimeRange.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchLicenseNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateListView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentLicenseplateCaptureTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentLicenseplateNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // to
            // 
            this.to.EditValue = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            this.to.Location = new System.Drawing.Point(626, 40);
            this.to.Name = "to";
            this.to.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.to.Properties.Mask.EditMask = "F";
            this.to.Size = new System.Drawing.Size(201, 21);
            this.to.TabIndex = 4;
            // 
            // from
            // 
            this.from.EditValue = new System.DateTime(2010, 5, 4, 0, 0, 0, 0);
            this.from.Location = new System.Drawing.Point(392, 40);
            this.from.Name = "from";
            this.from.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.from.Properties.Mask.EditMask = "F";
            this.from.Size = new System.Drawing.Size(199, 21);
            this.from.TabIndex = 2;
            // 
            // licensePlateImageList
            // 
            this.licensePlateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("licensePlateImageList.ImageStream")));
            this.licensePlateImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.licensePlateImageList.Images.SetKeyName(0, "BlueDot.gif");
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 339);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(952, 3);
            this.splitter1.TabIndex = 7;
            this.splitter1.TabStop = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.licenseplateNumberToSearch);
            this.groupControl1.Controls.Add(this.simpleButton1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.to);
            this.groupControl1.Controls.Add(this.matchTimeRange);
            this.groupControl1.Controls.Add(this.matchLicenseNumber);
            this.groupControl1.Controls.Add(this.from);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(952, 84);
            this.groupControl1.TabIndex = 8;
            this.groupControl1.Text = "查询条件";
            // 
            // licenseplateNumberToSearch
            // 
            this.licenseplateNumberToSearch.Location = new System.Drawing.Point(76, 40);
            this.licenseplateNumberToSearch.Name = "licenseplateNumberToSearch";
            this.licenseplateNumberToSearch.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.licenseplateNumberToSearch.Size = new System.Drawing.Size(133, 21);
            this.licenseplateNumberToSearch.TabIndex = 6;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(846, 39);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "查询";
            this.simpleButton1.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(597, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "到：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(362, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "从：";
            // 
            // matchTimeRange
            // 
            this.matchTimeRange.EditValue = true;
            this.matchTimeRange.Location = new System.Drawing.Point(286, 41);
            this.matchTimeRange.Name = "matchTimeRange";
            this.matchTimeRange.Properties.Caption = "时间段";
            this.matchTimeRange.Size = new System.Drawing.Size(68, 19);
            this.matchTimeRange.TabIndex = 2;
            // 
            // matchLicenseNumber
            // 
            this.matchLicenseNumber.Location = new System.Drawing.Point(12, 41);
            this.matchLicenseNumber.Name = "matchLicenseNumber";
            this.matchLicenseNumber.Properties.Caption = "车牌号";
            this.matchLicenseNumber.Size = new System.Drawing.Size(107, 19);
            this.matchLicenseNumber.TabIndex = 0;
            // 
            // licensePlateLists
            // 
            this.licensePlateLists.Dock = System.Windows.Forms.DockStyle.Fill;
            this.licensePlateLists.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.licensePlateLists.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.licensePlateLists.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.licensePlateLists.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.licensePlateLists.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.licensePlateLists.Location = new System.Drawing.Point(2, 57);
            this.licensePlateLists.MainView = this.licensePlateListView;
            this.licensePlateLists.Name = "licensePlateLists";
            this.licensePlateLists.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemPictureEdit1});
            this.licensePlateLists.Size = new System.Drawing.Size(948, 178);
            this.licensePlateLists.TabIndex = 9;
            this.licensePlateLists.UseEmbeddedNavigator = true;
            this.licensePlateLists.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.licensePlateListView});
            // 
            // licensePlateListView
            // 
            this.licensePlateListView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.layoutViewColumn1,
            this.gridColumn1,
            this.gridColumn2});
            this.licensePlateListView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.licensePlateListView.GridControl = this.licensePlateLists;
            this.licensePlateListView.Name = "licensePlateListView";
            this.licensePlateListView.OptionsBehavior.Editable = false;
            this.licensePlateListView.OptionsView.RowAutoHeight = true;
            this.licensePlateListView.OptionsView.ShowGroupPanel = false;
            this.licensePlateListView.CustomDrawEmptyForeground += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.licensePlateListView_CustomDrawEmptyForeground);
            this.licensePlateListView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.licensePlateListView_FocusedRowChanged);
            // 
            // layoutViewColumn1
            // 
            this.layoutViewColumn1.Caption = "抓拍图片";
            this.layoutViewColumn1.ColumnEdit = this.repositoryItemPictureEdit1;
            this.layoutViewColumn1.FieldName = "Thumbnail";
            this.layoutViewColumn1.Name = "layoutViewColumn1";
            this.layoutViewColumn1.Visible = true;
            this.layoutViewColumn1.VisibleIndex = 0;
            // 
            // repositoryItemPictureEdit1
            // 
            this.repositoryItemPictureEdit1.Name = "repositoryItemPictureEdit1";
            this.repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "车牌号码";
            this.gridColumn1.FieldName = "LicenseplateNumber";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "抓拍时间";
            this.gridColumn2.DisplayFormat.FormatString = "F";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn2.FieldName = "CaptureTime";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            // 
            // bigImage
            // 
            this.bigImage.Location = new System.Drawing.Point(6, 6);
            this.bigImage.Name = "bigImage";
            this.bigImage.Size = new System.Drawing.Size(940, 193);
            this.bigImage.TabIndex = 10;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.currentLicenseplateCaptureTime);
            this.layoutControl1.Controls.Add(this.bigImage);
            this.layoutControl1.Controls.Add(this.currentLicenseplateNumber);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 84);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsItemText.TextAlignMode = DevExpress.XtraLayout.TextAlignMode.AlignInGroups;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(952, 255);
            this.layoutControl1.TabIndex = 11;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // currentLicenseplateCaptureTime
            // 
            this.currentLicenseplateCaptureTime.Location = new System.Drawing.Point(82, 228);
            this.currentLicenseplateCaptureTime.Name = "currentLicenseplateCaptureTime";
            this.currentLicenseplateCaptureTime.Properties.DisplayFormat.FormatString = "F";
            this.currentLicenseplateCaptureTime.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.currentLicenseplateCaptureTime.Properties.ReadOnly = true;
            this.currentLicenseplateCaptureTime.Size = new System.Drawing.Size(864, 21);
            this.currentLicenseplateCaptureTime.StyleController = this.layoutControl1;
            this.currentLicenseplateCaptureTime.TabIndex = 12;
            // 
            // currentLicenseplateNumber
            // 
            this.currentLicenseplateNumber.Location = new System.Drawing.Point(82, 203);
            this.currentLicenseplateNumber.Name = "currentLicenseplateNumber";
            this.currentLicenseplateNumber.Properties.ReadOnly = true;
            this.currentLicenseplateNumber.Size = new System.Drawing.Size(864, 21);
            this.currentLicenseplateNumber.StyleController = this.layoutControl1;
            this.currentLicenseplateNumber.TabIndex = 11;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(952, 255);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.bigImage;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(944, 197);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Bottom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.currentLicenseplateNumber;
            this.layoutControlItem2.CustomizationFormText = "车牌号：";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 197);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(944, 25);
            this.layoutControlItem2.Text = "车牌号：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(72, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.currentLicenseplateCaptureTime;
            this.layoutControlItem3.CustomizationFormText = "抓拍时间：";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 222);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(944, 25);
            this.layoutControlItem3.Text = "抓拍时间：   ";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(72, 14);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.licensePlateLists);
            this.groupControl2.Controls.Add(this.panelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(0, 342);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(952, 237);
            this.groupControl2.TabIndex = 12;
            this.groupControl2.Text = "车牌列表";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.playRelatedVideo);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(2, 23);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(948, 34);
            this.panelControl1.TabIndex = 10;
            // 
            // playRelatedVideo
            // 
            this.playRelatedVideo.Image = ((System.Drawing.Image)(resources.GetObject("playRelatedVideo.Image")));
            this.playRelatedVideo.Location = new System.Drawing.Point(10, 6);
            this.playRelatedVideo.Name = "playRelatedVideo";
            this.playRelatedVideo.Size = new System.Drawing.Size(75, 23);
            this.playRelatedVideo.TabIndex = 0;
            this.playRelatedVideo.Text = "相关视频";
            this.playRelatedVideo.Click += new System.EventHandler(this.playRelatedVideo_Click);
            // 
            // FormLicensePlateQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 579);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupControl2);
            this.Name = "FormLicensePlateQuery";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "车牌号查询";
            ((System.ComponentModel.ISupportInitialize)(this.to.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.from.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.licenseplateNumberToSearch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchTimeRange.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.matchLicenseNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateLists)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.licensePlateListView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemPictureEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentLicenseplateCaptureTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.currentLicenseplateNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TimeEdit from;
        private DevExpress.XtraEditors.TimeEdit to;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList licensePlateImageList;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit matchLicenseNumber;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckEdit matchTimeRange;
        private DevExpress.XtraGrid.GridControl licensePlateLists;
        private DevExpress.XtraEditors.MRUEdit licenseplateNumberToSearch;
        private QAlbum.ScalablePictureBox bigImage;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit currentLicenseplateCaptureTime;
        private DevExpress.XtraEditors.TextEdit currentLicenseplateNumber;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.Repository.RepositoryItemPictureEdit repositoryItemPictureEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView licensePlateListView;
        private DevExpress.XtraGrid.Columns.GridColumn layoutViewColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton playRelatedVideo;
    }
}