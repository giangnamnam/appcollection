using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.Imaging.PlugIns;
using Damany.RemoteImaging.Common;
using Damany.RemoteImaging.Common.Presenters;
using Damany.Windows.Form;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using MiscUtil;
using RemoteImaging.Core;
using Damany.Component;
using System.Threading;
using RemoteImaging.ImportPersonCompare;
using RemoteImaging.LicensePlate;
using RemoteImaging.Query;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Damany.PC.Domain;
using RemoteImaging.YunTai;
using Frame = Damany.Imaging.Common.Frame;
using Portrait = Damany.Imaging.Common.Portrait;
using System.Linq;
using DevExpress.Xpo;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm,
        IImageScreen,
        LicensePlate.ILicensePlateObserver,
        YunTai.INavigationScreen
    {

        private int _countMaxTime = 0;
        private int _countWaitTime = 0;
        private SplashForm splash = new SplashForm();
        private CameraInfo CurrentCamera = null;
        private BindingList<LicensePlateInfo> _licensePlates = new BindingList<LicensePlateInfo>();
        private const string TagName = "tag";



        public Func<FaceComparePresenter> CreateFaceCompare { get; set; }

        private IEventAggregator _eventAggregator;
        public IEventAggregator EventAggregator
        {
            get { return _eventAggregator; }
            set
            {
                _eventAggregator = value;
                if (_eventAggregator != null)
                {
                    _eventAggregator.PortraitFound += (o, e) => this.HandlePortrait(e.Value);
                    _eventAggregator.FaceMatchFound += (o, e) => this.ShowSuspects(e.Value);
                    _eventAggregator.IsBusyChanged += new EventHandler<EventArgs<bool>>(_eventAggregator_IsBusyChanged);
                }
            }
        }

        void _eventAggregator_IsBusyChanged(object sender, EventArgs<bool> e)
        {
            Action doIt = () => this.progressStatusBarItem.Visibility = e.Value == true ? BarItemVisibility.Always :
            BarItemVisibility.Never;

            this.BeginInvoke(doIt);
        }

        public MainForm()
        {
            //splash.TopMost = false;
            //splash.Show();
            //splash.Update();

            InitializeComponent();

            if (!string.IsNullOrEmpty(Program.directory))
            {
                this.Text += "-[" + Program.directory + "]";
            }

            InitStatusBar();

            WireUpNavigationControlEventHandler();

            InitLicensePlateGrid();

            zoomFactor.Edit.EditValueChanging += Edit_EditValueChanging;
            faceGalleryControl.Gallery.ItemCheckedChanged += Gallery_ItemCheckedChanged;

            EnterFullScreenMode(true);

            Application.Idle += new EventHandler(Application_Idle);
        }

        private void EnterFullScreenMode(bool enter)
        {
            if (enter)
            {
                Gma.UserActivityMonitor.HookManager.KeyDown += HookManager_KeyDown;
                FormHelper.ShowStartMenu(false);
            }
            else
            {
                Gma.UserActivityMonitor.HookManager.KeyDown -= HookManager_KeyDown;
                FormHelper.ShowStartMenu(true);
            }

            this.FormBorderStyle = enter ? FormBorderStyle.None : FormBorderStyle.Sizable;
        }

        void HookManager_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void Gallery_ItemCheckedChanged(object sender, GalleryItemEventArgs e)
        {
            var portrait = e.Item.Tag as Portrait;
            ShowPreviewImage(portrait);
        }

        void Edit_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            faceGalleryControl.Gallery.BeginUpdate();

            try
            {
                var sz = (int)e.NewValue;
                faceGalleryControl.Gallery.ImageSize = new Size(sz, sz);
            }
            finally
            {
                faceGalleryControl.Gallery.EndUpdate();
            }
        }

        private void InitLicensePlateGrid()
        {
            //licensePlatesGrid.DataSource = _licensePlates;
        }


        private void WireUpNavigationControlEventHandler()
        {
            //this.eightWayNavigator1.left.MouseDown += left_MouseDown;
            //this.eightWayNavigator1.left.MouseUp += leftUp_MouseUp;

            //this.eightWayNavigator1.right.MouseDown += right_MouseDown;
            //this.eightWayNavigator1.right.MouseUp += right_MouseUp;

            //this.eightWayNavigator1.up.MouseDown += new MouseEventHandler(up_MouseDown);
            //this.eightWayNavigator1.up.MouseUp += new MouseEventHandler(up_MouseUp);

            //this.eightWayNavigator1.Rightdown.MouseDown += new MouseEventHandler(rightDown_MouseDown);
            //this.eightWayNavigator1.Rightdown.MouseUp += new MouseEventHandler(rightDown_MouseUp);

            //this.eightWayNavigator1.Down.MouseDown += new MouseEventHandler(down_MouseDown);
            //this.eightWayNavigator1.Down.MouseUp += new MouseEventHandler(down_MouseUp);
            //this.eightWayNavigator1.leftDown.MouseDown += new MouseEventHandler(leftDown_MouseDown);
            //this.eightWayNavigator1.leftDown.MouseUp += new MouseEventHandler(leftDown_MouseUp);
            //this.eightWayNavigator1.leftUp.MouseDown += new MouseEventHandler(leftUp_MouseDown);
            //this.eightWayNavigator1.leftUp.MouseUp += new MouseEventHandler(leftUp_MouseUp);
            //this.eightWayNavigator1.rightUp.MouseDown += new MouseEventHandler(rightUp_MouseDown);
            //this.eightWayNavigator1.rightUp.MouseUp += new MouseEventHandler(rightUp_MouseUp);
            //this.eightWayNavigator1.btnDefaultPos.Click += new EventHandler(btnDefaultPos_Click);
            //this.eightWayNavigator1.center.Click += new EventHandler(center_Click);

            //this.camNav1.btnAAperture.MouseDown += new MouseEventHandler(btnAAperture_MouseDown);
            //this.camNav1.btnAAperture.MouseUp += new MouseEventHandler(btnAAperture_MouseUp);
            //this.camNav1.btnAFocus.MouseDown += new MouseEventHandler(btnAFocus_MouseDown);
            //this.camNav1.btnAFocus.MouseUp += new MouseEventHandler(btnAFocus_MouseUp);
            //this.camNav1.btnAHighlghts.MouseDown += new MouseEventHandler(btnAHighlghts_MouseDown);
            //this.camNav1.btnAHighlghts.MouseUp += new MouseEventHandler(btnAHighlghts_MouseUp);
            //this.camNav1.btnAWipers.MouseDown += new MouseEventHandler(btnAWipers_MouseDown);
            //this.camNav1.btnAWipers.MouseUp += new MouseEventHandler(btnAWipers_MouseUp);
            //this.camNav1.btnCAperture.MouseDown += new MouseEventHandler(btnCAperture_MouseDown);
            //this.camNav1.btnCAperture.MouseUp += new MouseEventHandler(btnCAperture_MouseUp);
            //this.camNav1.btnCFocus.MouseDown += new MouseEventHandler(btnCFocus_MouseDown);
            //this.camNav1.btnCFocus.MouseUp += new MouseEventHandler(btnCFocus_MouseUp);
            //this.camNav1.btnCHighlghts.MouseDown += new MouseEventHandler(btnCHighlghts_MouseDown);
            //this.camNav1.btnCHighlghts.MouseUp += new MouseEventHandler(btnCHighlghts_MouseUp);
            //this.camNav1.btnCWipers.MouseDown += new MouseEventHandler(btnCWipers_MouseDown);
            //this.camNav1.btnCWipers.MouseUp += new MouseEventHandler(btnCWipers_MouseUp);
            //this.camNav1.btnDefaultPos.Click += new EventHandler(btnDefaultPos_Click);
            //this.camNav1.btnReturnDefaultPos.Click += new EventHandler(btnReturnDefaultPos_Click);
        }

        void center_Click(object sender, EventArgs e)
        {
            //_navController.NavStop();
            _navController.NavReturnDefultPos();

        }
        #region yuntai event
        void btnReturnDefaultPos_Click(object sender, EventArgs e)
        {
            _navController.NavReturnDefultPos();

        }

        void btnDefaultPos_Click(object sender, EventArgs e)
        {
            _navController.NavDefultPos();
        }

        void btnCWipers_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnCWipers_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraSwitchOff();
        }

        void btnCHighlghts_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnCHighlghts_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraZoomTele();

        }

        void btnCFocus_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnCFocus_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraFocusOut();
        }

        void btnCAperture_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnCAperture_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraIrisClose();
        }

        void btnAWipers_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnAWipers_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraSwitchOn();
        }

        void btnAHighlghts_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnAHighlghts_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraZoomWide();
        }

        void btnAFocus_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnAFocus_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraFocusIn();
        }

        void btnAAperture_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
        }

        void btnAAperture_MouseDown(object sender, MouseEventArgs e)
        {

            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraIrisOpen();
        }

        void ReturnDefultPos_Click(object sender, EventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavReturnDefultPos();
        }
        void StartMaxTimer()
        {
            _countMaxTime = 0;
            tmMaxTime.Enabled = true;
            tmWait.Enabled = false;
            _countWaitTime = 0;
        }
        void StartWaitTimer()
        {
            _countWaitTime = 0;
            tmWait.Enabled = true;

        }
        void FocusOut_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void FocusOut_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraFocusOut();
            StartMaxTimer();

        }

        void FocusIn_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void FocusIn_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavCameraFocusIn();
            StartMaxTimer();
        }

        void rightUp_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void rightUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavRightUp();
            StartMaxTimer();
        }

        void leftUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavLeftUp();
            StartMaxTimer();
        }

        void rightDown_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void rightDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavRightDown();
            StartMaxTimer();
        }

        void leftDown_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void leftDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavLeftDown();
            StartMaxTimer();
        }
        void leftUp_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void left_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            _navController.NavLeft();
            StartMaxTimer();
        }

        void center_MouseUp(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //_navController.NavStop();
            //StartWaitTimer();
        }

        void center_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //_navController.NavDefultPos();
            //StartMaxTimer();
        }

        void down_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void down_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavDown();
            StartMaxTimer();
        }

        void up_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void up_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavUp();
            StartMaxTimer();
        }

        void right_MouseUp(object sender, MouseEventArgs e)
        {
            _navController.NavStop();
            StartWaitTimer();
        }

        void right_MouseDown(object sender, MouseEventArgs e)
        {
            if (SelectedCamera() == null)
            {
                MessageBox.Show("请先选择一个摄像头", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            _navController.NavRight();
            StartMaxTimer();
        }

        #endregion

        public MainForm(Func<IPicQueryPresenter> picQueryPresenterCreator,
                        Func<IVideoQueryPresenter> createVideoQueryPresenter,
                        Func<FaceComparePresenter> createFaceCompare,
                        Func<OptionsForm> createOptionsForm,
                        Func<OptionsPresenter> createOptionsPresenter,
                        Func<FullVideoScreen> fullScreenMethod,
                        IMessageBoxService messageBoxService,
                        ConfigurationManager configurationManager,
                        FileSystemStorage videoRepository
                        )
            : this()
        {
            if (fullScreenMethod == null) throw new ArgumentNullException("fullScreenMethod");
            if (messageBoxService == null) throw new ArgumentNullException("messageBoxService");

            CreateFaceCompare = createFaceCompare;
            this.picPresenterCreator = picQueryPresenterCreator;
            _createVideoQueryPresenter = createVideoQueryPresenter;
            _createFaceCompare = createFaceCompare;
            this._createOptionsPresenter = createOptionsPresenter;
            _fullScreenMethod = fullScreenMethod;
            _messageBoxService = messageBoxService;
            _configurationManager = configurationManager;
            this._createOptionsForm = createOptionsForm;
            _videoRepository = videoRepository;

            configurationManager.ConfigurationChanged += configurationManager_ConfigurationChanged;

        }

        void configurationManager_ConfigurationChanged(object sender, EventArgs e)
        {
            this.Cameras = Damany.RemoteImaging.Common.ConfigurationManager.GetDefault().GetCameras().ToArray();
        }

        public LicensePlate.ILicensePlateEventPublisher LicensePlateEventPublisher
        {
            set
            {
                _licensePlateEventPublisher = value;
                _licensePlateEventPublisher.RegisterForLicensePlateEvent(this);
            }
        }

        public void ShowMessage(string msg)
        {
            if (InvokeRequired)
            {
                Action<string> action = this.ShowMessage;
                this.BeginInvoke(action, msg);
                return;
            }

            MessageBox.Show(msg, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void AttachController(MainController controller)
        {
            this.controller = controller;
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (this.splash != null)
            {
                this.splash.Dispose();
            }
        }




        private TreeNode getTopCamera(TreeNode node)
        {
            if (node.Parent == null) return node;

            while (node.Tag == null || !(node.Tag is CameraInfo))
            {
                node = node.Parent;
            }
            return node;
        }


        #region IImageScreen Members

        public CameraInfo GetSelectedCamera()
        {
            return CurrentCamera;
        }


        private CapturedObject _selectedObject;
        public CapturedObject SelectedObject
        {
            get { return _selectedObject; }

        }

        public Frame BigImage
        {
            set
            {
                if (InvokeRequired)
                {
                    Action action = delegate
                                        {
                                            this.BigImage = value;
                                        };
                    this.BeginInvoke(action);
                    return;
                }

                this.bigImage.Picture = value.GetImage().ToBitmap();
                this.bigImage.Tag = value;

            }
        }

        public IImageScreenObserver Observer { get; set; }

        private void ShowLiveFace(ImageDetail[] images)
        {
            if (images.Length == 0) return;

            this.bigImage.Picture = (Bitmap)Damany.Util.Extensions.MiscHelper.FromFileBuffered(images.Last().Path);
        }


        public void ShowImages(ImageDetail[] images)
        {
            ImageCell[] cells = new ImageCell[images.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(images[i].Path);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].Path, Text = text, Tag = null };
                cells[i] = newCell;
            }

            ShowLiveFace(images);

            // this.squareListView1.ShowImages(cells);

        }

        #endregion


        private CameraInfo GetLastSelectedCamera()
        {
            int lastId = Properties.Settings.Default.LastSelCamID;
            return Damany.RemoteImaging.Common.ConfigurationManager.GetDefault().GetCameraById(lastId);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            UpdateClosedWindowMenu();

            this.controller.Start();

        }

        #region IImageScreen Members

        public CameraInfo[] Cameras
        {
            set
            {
                if (value == null)
                {
                    return;
                }

                if (value.Length == 0)
                {
                    return;

                }

                this.CurrentCamera = value[0];
            }
        }

        #endregion


        private void squareListView1_SelectedCellChanged(object sender, EventArgs e)
        {
            //if (this.squareListView1.SelectedCell == null) return;

            //var portrait = this.squareListView1.SelectedCell.Tag as Portrait;
            //if (portrait == null) return;

            //ShowBigImage(portrait);

            //_selectedObject = portrait;
        }

        private void ShowBigImage(Portrait portrait)
        {
            this.BigImage = portrait.Frame.Clone();
            currLicenseCaptureTime.EditValue = portrait.CapturedAt;
        }


        private void ShowSearchPicture()
        {
            var p = this.picPresenterCreator();
            p.Start();
        }


        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        [DllImport("user32.dll", EntryPoint = "BringWindowToTop")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BringWindowToTop([In()] IntPtr hWnd);

        /// Return Type: BOOL->int
        ///hWnd: HWND->HWND__*
        ///nCmdShow: int
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow([In()] IntPtr hWnd, int nCmdShow);

        System.Diagnostics.Process videoDnTool;

        private void dnloadVideo_Click(object sender, EventArgs e)
        {
            if (videoDnTool != null && !videoDnTool.HasExited)
            {
                //restore window and bring it to top
                ShowWindow(videoDnTool.MainWindowHandle, 9);
                BringWindowToTop(videoDnTool.MainWindowHandle);
                return;
            }

            videoDnTool = System.Diagnostics.Process.Start(Properties.Settings.Default.VideoDnTool);
            videoDnTool.EnableRaisingEvents = true;
            videoDnTool.Exited += videoDnTool_Exited;

        }

        void videoDnTool_Exited(object sender, EventArgs e)
        {
            videoDnTool = null;
        }


        private void ShowSearchVideoForm()
        {
            var p = _createVideoQueryPresenter();
            p.Start();
        }

        Thread thread = null;
        string tempComName = "";
        int tempModel = 0;

        private void ShowOptionsForm()
        {
            var p = this._createOptionsPresenter();
            p.Start();
        }

        private void column1by1_Click(object sender, EventArgs e)
        {
            //this.squareListView1.NumberOfColumns = 1;
        }

        private void column2by2_Click(object sender, EventArgs e)
        {
            //this.squareListView1.NumberOfColumns = 2;
        }

        private void column3by3_Click(object sender, EventArgs e)
        {
            //this.squareListView1.NumberOfColumns = 3;
        }

        private void column4by4_Click(object sender, EventArgs e)
        {
            //this.squareListView1.NumberOfColumns = 4;
        }

        private void column5by5_Click(object sender, EventArgs e)
        {
            //this.squareListView1.NumberOfColumns = 5;
        }

        private void InitStatusBar()
        {
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        long lastFreeSpaceBytes;


        private void statusOutputFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"explorer.exe",
                Properties.Settings.Default.OutputPath);
        }

        private void statusUploadFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"explorer.exe",
               Properties.Settings.Default.ImageUploadPool);
        }

        private void cameraTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }


        #region IImageScreen Members


        public bool ShowProgress
        {
            set
            {

            }
        }

        public void StepProgress()
        {


        }

        #endregion

        PerformanceCounter cpuCounter;
        PerformanceCounter ramCounter;

        private string getCurrentCpuUsage()
        {
            return String.Format("{0:F0}%", cpuCounter.NextValue());
        }

        private string getAvailableRAM()
        {
            return String.Format("{0}MB", ramCounter.NextValue());
        }

        private void ShowDetailPic(ImageDetail img)
        {
        }

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {
            //if (this.pictureEdit1.Tag == null)
            //{
            //    return;
            //}

            //ImageDetail img = this.pictureEdit1.Tag as ImageDetail;

            //ShowDetailPic(img);

        }

        private void ShowPreviewImage(Portrait portrait)
        {
            ShowBigImage(portrait);

            _selectedObject = portrait;
        }


        private void squareListView1_CellDoubleClick(object sender, CellDoubleClickEventArgs args)
        {

        }




        private void playRelateVideo_Click(object sender, EventArgs e)
        {
            controller.PlayVideo();
        }


        string videoPlayerPath;

        public void StartRecord(CameraInfo cam)
        {
            var axCamImgCtrl = this.axCamImgCtrl1;

            if (InvokeRequired)
            {
                Action<CameraInfo, AxIMGCTRLLib.AxCamImgCtrl> action = StartRecordInternal;

                this.BeginInvoke(action, cam, axCamImgCtrl);
                return;
            }

            StartRecordInternal(cam, axCamImgCtrl);
        }

        private void StartRecordInternal(CameraInfo cam, AxIMGCTRLLib.AxCamImgCtrl axCamImgCtrl)
        {
            axCamImgCtrl.CamImgCtrlStop();

            System.Diagnostics.Debug.WriteLine(axCamImgCtrl.Tag);

            axCamImgCtrl.ImageFileURL = @"liveimg.cgi";
            axCamImgCtrl.ImageType = @"MPEG";
            axCamImgCtrl.CameraModel = 1;
            axCamImgCtrl.CtlLocation = @"http://" + cam.Location.Host;
            axCamImgCtrl.uid = "guest";
            axCamImgCtrl.pwd = "guest";
            axCamImgCtrl.RecordingFolderPath
                = Path.Combine(Util.GetVideoOutputPath(), cam.Id.ToString("D2"));
            axCamImgCtrl.RecordingFormat = 0;
            axCamImgCtrl.UniIP = this.axCamImgCtrl1.CtlLocation;
            axCamImgCtrl.UnicastPort = 3939;
            axCamImgCtrl.MulticastPort = 34344;
            axCamImgCtrl.MCIP = "239.136.50.230";
            axCamImgCtrl.ComType = 2; //multi:1, udp:2, http:0

            if (Properties.Settings.Default.Live)
            {
                axCamImgCtrl.CamImgCtrlStart();
                axCamImgCtrl.CamImgRecStart();

            }
        }

        private void OnConnectionFinished(object ex)
        {


        }


        int? lastSelCamID = null;

        private void cameraTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                CurrentCamera = e.Node.Tag as CameraInfo;
                _navController.CurrentCamera = CurrentCamera;
                //  MessageBox.Show(CurrentCamera.Location.ToString() + " " + CurrentCamera.Name);
            }


        }

        private void axCamImgCtrl1_InfoChanged(object sender, AxIMGCTRLLib._ICamImgCtrlEvents_InfoChangedEvent e)
        {
            Debug.WriteLine("========info changed" + e.infoConn.AlarmInfo);
        }

        private void enhanceImg_Click(object sender, EventArgs e)
        {

        }

        private void CenterLiveControl()
        {


        }
        private void panelControl1_SizeChanged(object sender, EventArgs e)
        {
            CenterLiveControl();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SkinHelper.InitSkinPopupMenu(skinsLink);

            if (File.Exists(_layoutXml))
            {
                try
                {
                    dockManager1.RestoreFromXml(GetLayoutPath());
                }
                catch { }
            }

            this.switchMode.EditValue = Properties.Settings.Default.WorkingMode;

            CenterLiveControl();
        }

        void testButton_Click(object sender, EventArgs e)
        {
            _videoRepository.DeleteMostOutDatedDataForDay(0, 1);
        }

        private void tsbFileSet_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (var dlg = new PasswordForm())
            {
                var dr = dlg.ShowDialog(this);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }


            EnterFullScreenMode(false);
            controller.Stop();

            if ((thread != null) && (thread.IsAlive))
            {
                thread.Abort();
            }

            if (lastSelCamID != null)
            {
                Properties.Settings.Default.LastSelCamID = (int)this.lastSelCamID;
            }
            try
            {
                _navController.NavReturnDefultPos();
            }
            catch { }

            Properties.Settings.Default.WorkingMode = (int)switchMode.EditValue;
            Debug.WriteLine(Properties.Settings.Default.WorkingMode);
            Properties.Settings.Default.Save();

            dockManager1.SaveLayoutToXml(GetLayoutPath());
        }

        private string GetLayoutPath()
        {
            return Path.Combine(Application.StartupPath, _layoutXml);
        }

        private void tsbMonitoring_Click(object sender, EventArgs e)
        {
            Monitoring monitoring = new Monitoring();
            monitoring.ShowDialog(this);
        }

        private void SetupCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ViewCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.controller.Start();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            DialogResult res = MessageBox.Show("设置背景后将影响人脸识别准确度, 你确认要设置背景吗?", "警告",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (res != DialogResult.Yes) return;

        }


        #region IImageScreen Members


        public void ShowSuspects(Damany.Imaging.Common.PersonOfInterestDetectionResult result)
        {
            if (InvokeRequired)
            {
                Action<Damany.Imaging.Common.PersonOfInterestDetectionResult> action = this.ShowSuspects;

                this.BeginInvoke(action, result);
                return;
            }

            alertForm.AddSuspects(result);

        }


        #endregion

        private void faceRecognize_CheckedChanged(object sender, EventArgs e)
        {
        }


        public void HandlePortrait(Portrait portrait)
        {
            var clone = portrait.Clone();

            Action action = () =>
                                {
                                    var group = faceGalleryControl.Gallery.Groups[0];
                                    if (group.Items.Count > 20)
                                    {
                                        group.Items.Remove(group.Items[group.Items.Count - 1]);
                                    }

                                    var caption = clone.CapturedAt.ToShortTimeString();
                                    var item = new GalleryItem(clone.GetIpl().ToBitmap(), caption, null);
                                    item.Tag = clone;
                                    this.faceGalleryControl.Gallery.Groups[0].Items.Insert(0, item);

                                    ShowBigImage(clone);
                                };

            if (InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action();
            }


        }

        public void Stop()
        {
        }

        public string Description
        {
            get { throw new NotImplementedException(); }
        }

        public string Author
        {
            get { throw new NotImplementedException(); }
        }

        public Version Version
        {
            get { throw new NotImplementedException(); }
        }

        public bool WantCopy
        {
            get { return true; }
        }

        public bool WantFrame
        {
            get { return false; }
        }

        public event EventHandler<EventArgs<Exception>> Stopped;

        private MainController controller;
        private Func<OptionsPresenter> _createOptionsPresenter;
        private readonly Func<FormSuspectCarManager> _formSuspectCarManagerFactory;
        private readonly Func<FormSuspectCarQuery> _formSuspectCarQueryFactory;
        private readonly Func<FullVideoScreen> _fullScreenMethod;
        private readonly Func<ILicensePlateSearchPresenter> _licensePlateSearchFactory;
        private readonly ILicenseplateRecognizerController _lprController;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ConfigurationManager _configurationManager;
        private readonly FileSystemStorage _videoRepository;
        private readonly LicensePlateRepository _suspectCarRepository;
        private Func<OptionsForm> _createOptionsForm;

        private void faceRecognize_Click(object sender, EventArgs e)
        {
            var p = _createFaceCompare();
            p.ComparerSensitivity = Properties.Settings.Default.LbpThreshold;

            var thresholds = new float[]
                                 {
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityHi,
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityMiddle,
                                     Properties.Settings.Default.HistoryFaceCompareSensitivityLow
                                 };

            p.Thresholds = thresholds;
            p.Start();

        }

        private void faceLibBuilder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("FaceLibraryBuilder.exe");
        }

        ImportPersonCompare.ImmediatelyModel alertForm = new ImportPersonCompare.ImmediatelyModel();

        private void alermForm_Click(object sender, EventArgs e)
        {
            this.alertForm.Show(this);
        }




        public ConfigurationSectionHandlers.ButtonsVisibleSectionHandler ButtonsVisible
        {
            set
            {
                addSuspectsButton.Visibility = value.HumanFaceLibraryButtonVisible ? BarItemVisibility.Always : BarItemVisibility.Never;
                manualFaceCompare.Visibility = value.CompareFaceButtonVisible ? BarItemVisibility.Always : BarItemVisibility.Never;
                suspeciousPersonAlerm.Visibility = value.ShowAlermFormButtonVisible ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }


        private void axCamImgCtrl1_Enter(object sender, EventArgs e)
        {

        }

        private void licensePlateList_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {

        }

        public void LicensePlateCaptured(LicensePlateInfo licensePlateInfo)
        {
            //if (InvokeRequired)
            //{
            //    Action<LicensePlateInfo> action = LicensePlateCaptured;
            //    this.Invoke(action, licensePlateInfo);
            //    return;
            //}

            //var camInfo = _configurationManager.GetCameraById(licensePlateInfo.CapturedFrom);
            //var cameraName = "未知摄像头";
            //if (camInfo != null)
            //{
            //    cameraName = camInfo.Name ?? "未知摄像头";
            //}


            //if (_licensePlates.Count > Properties.Settings.Default.MaxmumLicenseplates)
            //{
            //    _licensePlates.RemoveAt(0);
            //}

        }

        private LicensePlate.ILicensePlateEventPublisher _licensePlateEventPublisher;

        private void searchLicensePlates_Click(object sender, EventArgs e)
        {
            var presenter = _licensePlateSearchFactory();
            presenter.Start();
        }

        public void AttachController(NavigationController controller)
        {
            if (controller == null) throw new ArgumentNullException("controller");
            _navController = controller;
        }

        public CameraInfo SelectedCamera()
        {
            return GetSelectedCamera();
        }


        private Func<RemoteImaging.IPicQueryPresenter> picPresenterCreator;
        private readonly Func<IVideoQueryPresenter> _createVideoQueryPresenter;
        private readonly Func<FaceComparePresenter> _createFaceCompare;
        private Func<RemoteImaging.IPicQueryScreen> picScreenCreator;
        private NavigationController _navController;
        private readonly string _layoutXml = "Layout.xml";


        private void showCarPic_Click(object sender, EventArgs e)
        {

        }

        private void carPicture_DoubleClick(object sender, EventArgs e)
        {

        }

        private void tmWait_Tick(object sender, EventArgs e)
        {
            _countWaitTime = _countWaitTime + 1;
            if (_countWaitTime >= ImageConfig.WaitTime)
            {
                _countWaitTime = 0;
                tmWait.Enabled = false;
                _navController.NavReturnDefultPos();

            }
        }

        private void tmMaxTime_Tick(object sender, EventArgs e)
        {
            _countMaxTime = _countMaxTime + 1;
            if (_countMaxTime >= ImageConfig.ActionTime)
            {
                _countMaxTime = 0;
                tmMaxTime.Enabled = false;
                _navController.NavStop();

            }
        }

        private void dockPanel4_Click(object sender, EventArgs e)
        {

        }

        private void dockPanel4_Container_Paint(object sender, PaintEventArgs e)
        {

        }


        private void settingsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowOptionsForm();
        }

        private void searchPicture_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowSearchPicture();
        }

        private void searchVideo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowSearchVideoForm();
        }


        private void licensePlatesGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            //if (e.FocusedRowHandle < 0)
            //{
            //    return;
            //}

            //var licenseplate = licensePlatesGridView.GetRow(e.FocusedRowHandle) as LicensePlateInfo;
            //if (licenseplate != null)
            //{


            //    var source = new dummyFrameSource();
            //    source.Id = licenseplate.CapturedFrom;

            //    _selectedObject = new CapturedObject { CapturedAt = licenseplate.CaptureTime, CapturedFrom = source, Guid = licenseplate.Guid };

        }

        private void optionsButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ShowOptionsForm();
        }

        private void playRelatedVideo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.controller.PlayVideo();
        }


        class dummyFrameSource : IFrameStream
        {
            public void Initialize()
            {
                throw new NotImplementedException();
            }

            public void Connect()
            {
                throw new NotImplementedException();
            }

            public void Close()
            {
                throw new NotImplementedException();
            }

            public Frame RetrieveFrame()
            {
                throw new NotImplementedException();
            }

            public int Id
            {
                get;
                set;
            }

            public string Description
            {
                get { throw new NotImplementedException(); }
            }
        }


        private bool _videoIsFull = true;

        private void switchView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            _videoIsFull = !_videoIsFull;
            this.SuspendLayout();

            axCamImgCtrl1.CamImgCtrlStop();

            if (_videoIsFull)
            {
                axCamImgCtrl1.Parent = mainPanel;
                currentLicensePlateLayoutControl.Parent = dockPanelZoomPic;
                axCamImgCtrl1.Height = mainPanel.ClientSize.Width;
                axCamImgCtrl1.Width = mainPanel.ClientSize.Height;

                mainPanel.Text = "实时视频";
                dockPanelZoomPic.Text = "放大图片";

            }
            else
            {
                axCamImgCtrl1.Parent = dockPanelZoomPic;
                axCamImgCtrl1.Height = dockPanelZoomPic.ClientSize.Height;
                axCamImgCtrl1.Width = dockPanelZoomPic.ClientSize.Width;

                mainPanel.Text = "放大图片";
                dockPanelZoomPic.Text = "实时视频";


                currentLicensePlateLayoutControl.Parent = mainPanel;
            }

            axCamImgCtrl1.CamImgCtrlStart();


            this.ResumeLayout();

            return;

            _videoIsFull = !_videoIsFull;

            axCamImgCtrl1.Dock = _videoIsFull ? DockStyle.Fill : DockStyle.None;

            if (!_videoIsFull)
            {
                axCamImgCtrl1.Width = 170;
                axCamImgCtrl1.Height = 120;
                axCamImgCtrl1.Location = new Point(0, 0);
            }

            //liveFullImage.Dock = _videoIsFull ? DockStyle.None : DockStyle.Fill;
            //if (_videoIsFull)
            //{
            //    liveFullImage.Width = 170;
            //    liveFullImage.Height = 120;
            //    liveFullImage.Location = new Point(0, 0);
            //}

            //if (_videoIsFull)
            //{
            //    liveFullImage.BringToFront();
            //}
            //else
            //{
            //    axCamImgCtrl1.BringToFront();
            //}


        }

        private void axCamImgCtrl1_Resize(object sender, EventArgs e)
        {




        }


        private void licensePlatesGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void searchLicensePlate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            searchLicensePlates_Click(null, EventArgs.Empty);
        }

        private void fullScreen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            var fullScreen = _fullScreenMethod();
            ChangeParentTo(fullScreen);

            fullScreen.ShowDialog();

            ChangeParentTo(mainPanel);

        }


        private void ChangeParentTo(Control parent)
        {
            axCamImgCtrl1.CamImgCtrlStop();
            axCamImgCtrl1.Parent = null;

            axCamImgCtrl1.Width = parent.ClientSize.Width;
            axCamImgCtrl1.Height = parent.ClientSize.Height;
            axCamImgCtrl1.Parent = parent;

            this.axCamImgCtrl1.CamImgCtrlStart();
        }

        private void suspectCarManage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var form = _formSuspectCarManagerFactory();
            form.CarInfos = _suspectCarRepository.GetReportedCarInfos();
            var result = form.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                _suspectCarRepository.Save(form.CarInfos, form.RemovedInfos);
            }
        }

        private void UpdateClosedWindowMenu()
        {
            barButtonItemFaceList.Down = dockPanelFaceList.Visible;
            barButtonItemBigPicture.Down = dockPanelZoomPic.Visible;
        }

        private void viewsBar_Popup(object sender, EventArgs e)
        {
            UpdateClosedWindowMenu();
        }

        private void barButtonItemCameraControl_DownChanged(object sender, ItemClickEventArgs e)
        {
            var item = sender as BarButtonItem;
        }

        private void barButtonItemBigPicture_DownChanged(object sender, ItemClickEventArgs e)
        {
            var item = sender as BarButtonItem;
            dockPanelZoomPic.Visible = item.Down;
        }

        private void barButtonItemFaceList_DownChanged(object sender, ItemClickEventArgs e)
        {
            var item = sender as BarButtonItem;
            dockPanelFaceList.Visible = item.Down;

        }

        private void barButtonItemCarList_DownChanged(object sender, ItemClickEventArgs e)
        {
            var item = sender as BarButtonItem;

        }

        private void suspectCarQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            var form = _formSuspectCarQueryFactory();
            form.ShowDialog(this);

        }

        private void setRectangleButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var path = System.IO.Path.Combine(Application.StartupPath, Properties.Settings.Default.RectangleSetterPath);
            Process.Start(path);

        }

        private void barEditItem3_EditValueChanged(object sender, EventArgs e)
        {
            _eventAggregator.PublishSwitchMotionDetectorEvent();

            var camConfig = _configurationManager.GetCameras().FirstOrDefault();
            if (camConfig == null) return;

            var modeName = ((int)switchMode.EditValue) == 0 ? "室内模式" : "室外模式";

            var modesQuery = new XPQuery<WorkModeCamSetting>(new Session());

            var selectedConfig = modesQuery.Where(cfg => cfg.ModeName == modeName).FirstOrDefault();

            if (selectedConfig == null)
            {
                return;
            }

            var worker = Task.Factory.StartNew(() =>
                                                             {
                                                                 var sanyocamera = new Damany.Component.SanyoNetCamera();
                                                                 sanyocamera.UserName = "admin";
                                                                 sanyocamera.Password = "admin";
                                                                 sanyocamera.IPAddress = camConfig.Location.Host;
                                                                 sanyocamera.Connect();

                                                                 sanyocamera.SetIris(IrisMode.Manual,
                                                                                     selectedConfig.IrisLevel);
                                                                 sanyocamera.SetShutter(
                                                                     ShutterMode.Short,
                                                                     selectedConfig.ShutterSpeed);

                                                                 sanyocamera.SetAGCMode(true, selectedConfig.DigitalGain);

                                                             });
            worker.ContinueWith(ant =>
                                 {
                                     if (ant.Exception != null)
                                     {
                                         var error = ant.Exception;
                                         _messageBoxService.ShowError("设置摄像头参数失败! 请确认摄像机已经正确连接。");
                                     }
                                 },
                                 CancellationToken.None,
                                 TaskContinuationOptions.OnlyOnFaulted,
                             TaskScheduler.FromCurrentSynchronizationContext());


        }


        private void ShowMb(Exception err)
        {
            if (err == null)
            {
                _messageBoxService.ShowInfo("设置成功");
            }
            else
            {
                _messageBoxService.ShowError("设置失败!\r\n\r\n" + err.Message);
            }
        }

        private void manualFaceCompare_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.faceRecognize_Click(sender, null);
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.alermForm_Click(sender, null);
        }

        private void addSuspectsButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            var dr = this.openFileDialog1.ShowDialog(this);
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                var destFileName = string.Format("FaceLib\\{0}.jpg", Guid.NewGuid());
                var destFile = System.IO.Path.Combine(Application.StartupPath, destFileName);

                System.IO.File.Copy(this.openFileDialog1.FileName, destFile);

                var appPath = System.IO.Path.Combine(Application.StartupPath, "annotation.exe");
                appPath = "\"" + appPath + "\"";
                destFile = "\"" + destFile + "\"";

                Process.Start(appPath, destFile);

            }
        }

        private void showTimeCaption_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            faceGalleryControl.Gallery.ShowItemText = showTimeCaption.Checked;
        }

        private void barButtonItem2_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            var form = new FormRoi();
            form.ShowDialog(this);
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.controller.SetupRoi();
        }

        private void exitSystem_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}
