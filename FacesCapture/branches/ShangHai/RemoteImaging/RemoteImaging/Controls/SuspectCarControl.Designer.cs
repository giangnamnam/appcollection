namespace RemoteImaging.Controls
{
    partial class SuspectCarControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.addTime = new DevExpress.XtraEditors.TextEdit();
            this.reportedMissingType = new DevExpress.XtraEditors.TextEdit();
            this.licenseNumber = new DevExpress.XtraEditors.TextEdit();
            this.reportedCarType = new DevExpress.XtraEditors.TextEdit();
            this.gdViewer1 = new GdPicture.GdViewer();
            this.captureTime = new DevExpress.XtraEditors.TextEdit();
            this.carImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportedMissingType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.licenseNumber.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportedCarType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.carImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.addTime);
            this.layoutControl1.Controls.Add(this.reportedMissingType);
            this.layoutControl1.Controls.Add(this.licenseNumber);
            this.layoutControl1.Controls.Add(this.reportedCarType);
            this.layoutControl1.Controls.Add(this.gdViewer1);
            this.layoutControl1.Controls.Add(this.captureTime);
            this.layoutControl1.Controls.Add(this.carImage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(739, 423);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // addTime
            // 
            this.addTime.Location = new System.Drawing.Point(413, 390);
            this.addTime.Name = "addTime";
            this.addTime.Size = new System.Drawing.Size(314, 21);
            this.addTime.StyleController = this.layoutControl1;
            this.addTime.TabIndex = 11;
            // 
            // reportedMissingType
            // 
            this.reportedMissingType.Location = new System.Drawing.Point(413, 365);
            this.reportedMissingType.Name = "reportedMissingType";
            this.reportedMissingType.Size = new System.Drawing.Size(314, 21);
            this.reportedMissingType.StyleController = this.layoutControl1;
            this.reportedMissingType.TabIndex = 9;
            // 
            // licenseNumber
            // 
            this.licenseNumber.Location = new System.Drawing.Point(413, 290);
            this.licenseNumber.Name = "licenseNumber";
            this.licenseNumber.Size = new System.Drawing.Size(314, 21);
            this.licenseNumber.StyleController = this.layoutControl1;
            this.licenseNumber.TabIndex = 8;
            // 
            // reportedCarType
            // 
            this.reportedCarType.Location = new System.Drawing.Point(413, 340);
            this.reportedCarType.Name = "reportedCarType";
            this.reportedCarType.Size = new System.Drawing.Size(314, 21);
            this.reportedCarType.StyleController = this.layoutControl1;
            this.reportedCarType.TabIndex = 6;
            // 
            // gdViewer1
            // 
            this.gdViewer1.AllowDrop = true;
            this.gdViewer1.AnimateGIF = false;
            this.gdViewer1.BackColor = System.Drawing.Color.Black;
            this.gdViewer1.BackgroundImage = null;
            this.gdViewer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gdViewer1.ContinuousViewMode = true;
            this.gdViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.gdViewer1.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityBicubicHQ;
            this.gdViewer1.DisplayQualityAuto = false;
            this.gdViewer1.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.gdViewer1.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.gdViewer1.EnableMenu = true;
            this.gdViewer1.EnableMouseWheel = true;
            this.gdViewer1.ForceScrollBars = false;
            this.gdViewer1.ForceTemporaryModeForImage = false;
            this.gdViewer1.ForceTemporaryModeForPDF = false;
            this.gdViewer1.ForeColor = System.Drawing.Color.Black;
            this.gdViewer1.Gamma = 1F;
            this.gdViewer1.IgnoreDocumentResolution = false;
            this.gdViewer1.KeepDocumentPosition = false;
            this.gdViewer1.Location = new System.Drawing.Point(12, 12);
            this.gdViewer1.LockViewer = false;
            this.gdViewer1.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.gdViewer1.MouseMode = GdPicture.ViewerMouseMode.MouseModePan;
            this.gdViewer1.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.gdViewer1.Name = "gdViewer1";
            this.gdViewer1.OptimizeDrawingSpeed = false;
            this.gdViewer1.PdfDisplayFormField = true;
            this.gdViewer1.RectBorderColor = System.Drawing.Color.Black;
            this.gdViewer1.RectBorderSize = 1;
            this.gdViewer1.RectIsEditable = true;
            this.gdViewer1.ScrollBars = false;
            this.gdViewer1.ScrollLargeChange = ((short)(50));
            this.gdViewer1.ScrollSmallChange = ((short)(1));
            this.gdViewer1.SilentMode = false;
            this.gdViewer1.Size = new System.Drawing.Size(333, 399);
            this.gdViewer1.TabIndex = 12;
            this.gdViewer1.Zoom = 1D;
            this.gdViewer1.ZoomMode = GdPicture.ViewerZoomMode.ZoomMode100;
            this.gdViewer1.ZoomStep = 25;
            // 
            // captureTime
            // 
            this.captureTime.Location = new System.Drawing.Point(413, 315);
            this.captureTime.Name = "captureTime";
            this.captureTime.Size = new System.Drawing.Size(314, 21);
            this.captureTime.StyleController = this.layoutControl1;
            this.captureTime.TabIndex = 5;
            // 
            // carImage
            // 
            this.carImage.Location = new System.Drawing.Point(349, 12);
            this.carImage.Name = "carImage";
            this.carImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.carImage.Size = new System.Drawing.Size(378, 274);
            this.carImage.StyleController = this.layoutControl1;
            this.carImage.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem5,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(739, 423);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.gdViewer1;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(337, 403);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.carImage;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(337, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(382, 278);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.reportedCarType;
            this.layoutControlItem3.CustomizationFormText = "报告车型：";
            this.layoutControlItem3.Location = new System.Drawing.Point(337, 328);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(382, 25);
            this.layoutControlItem3.Text = "布控车型：";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.reportedMissingType;
            this.layoutControlItem4.CustomizationFormText = "布控类型：";
            this.layoutControlItem4.Location = new System.Drawing.Point(337, 353);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(382, 25);
            this.layoutControlItem4.Text = "布控类型：";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.addTime;
            this.layoutControlItem7.CustomizationFormText = "布控时间：";
            this.layoutControlItem7.Location = new System.Drawing.Point(337, 378);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(382, 25);
            this.layoutControlItem7.Text = "布控时间：";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.licenseNumber;
            this.layoutControlItem5.CustomizationFormText = "车牌号码：";
            this.layoutControlItem5.Location = new System.Drawing.Point(337, 278);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(382, 25);
            this.layoutControlItem5.Text = "车牌号码：";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(60, 14);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.captureTime;
            this.layoutControlItem2.CustomizationFormText = "抓拍时间：";
            this.layoutControlItem2.Location = new System.Drawing.Point(337, 303);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(382, 25);
            this.layoutControlItem2.Text = "抓拍时间：";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(60, 14);
            // 
            // SuspectCarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.LookAndFeel.SkinName = "Darkroom";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.Name = "SuspectCarControl";
            this.Size = new System.Drawing.Size(739, 423);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.addTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportedMissingType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.licenseNumber.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.reportedCarType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.captureTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.carImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        public DevExpress.XtraEditors.TextEdit licenseNumber;
        public DevExpress.XtraEditors.TextEdit reportedCarType;
        public DevExpress.XtraEditors.TextEdit captureTime;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        public GdPicture.GdViewer gdViewer1;
        public DevExpress.XtraEditors.PictureEdit carImage;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        public DevExpress.XtraEditors.TextEdit addTime;
        public DevExpress.XtraEditors.TextEdit reportedMissingType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
    }
}
