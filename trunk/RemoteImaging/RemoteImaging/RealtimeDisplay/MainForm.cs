using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyControls;
using System.IO;
using DevExpress.XtraNavBar;
using ImageProcess;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, IImageScreen
    {
        public MainForm()
        {
            InitializeComponent();

            Camera[] cams = new Camera[Configuration.Instance.Cameras.Count];
            Configuration.Instance.Cameras.CopyTo(cams, 0);
            this.Cameras = cams;

            Properties.Settings setting = Properties.Settings.Default;

            InitStatusBar();
            float left = float.Parse(setting.IconLeftExtRatio);
            float top = float.Parse(setting.IconTopExtRatio);
            float right = float.Parse(setting.IconRightExtRatio);
            float bottom = float.Parse(setting.IconBottomExtRatio);

            int minFaceWidth = int.Parse(setting.MinFaceWidth);
            int maxFaceWidth = int.Parse(setting.MaxFaceWidth);

            float ratio = (float)maxFaceWidth / minFaceWidth;

            SetupExtractor(left, right, top, bottom, minFaceWidth, ratio);

        }

        Camera allCamera = new Camera() { ID = -1 };

        private TreeNode getTopCamera()
        {
            TreeNode nd = this.cameraTree.SelectedNode;
            while (nd.Tag == null || !(nd.Tag is Camera))
            {
                nd = nd.Parent;
            }
            return nd;
        }

        private Camera getSelCamera()
        {
            if (this.cameraTree.SelectedNode == null
                || this.cameraTree.SelectedNode.Level == 0)
            {
                return allCamera;
            }

            TreeNode nd = getTopCamera();
            return nd.Tag as Camera;
        }


        #region IImageScreen Members

        public Camera SelectedCamera
        {
            get
            {
                if (this.InvokeRequired)
                {
                    System.Func<Camera> func = this.getSelCamera;
                    return this.Invoke(func) as Camera;
                }
                else
                {
                    return getSelCamera();
                }

            }

        }

        public ImageDetail SelectedImage
        {
            get
            {
                ImageDetail img = null;
                if (this.squareListView1.LastSelectedCell != null)
                {
                    Cell c = this.squareListView1.LastSelectedCell;
                    if (!string.IsNullOrEmpty(c.Path))
                    {
                        img = ImageDetail.FromPath(c.Path);
                    }

                }

                return img;
            }

        }

        public ImageDetail BigImage
        {
            set
            {
                Image img = Image.FromFile(value.Path);
                this.pictureEdit1.Image = img;
            }
        }

        public IImageScreenObserver Observer { get; set; }

        public void ShowImages(ImageDetail[] images)
        {
            ImageCell[] cells = new ImageCell[images.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Image.FromFile(images[i].Path);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].Path, Text = text, Tag = null };
                cells[i] = newCell;
            }

            this.squareListView1.ShowImages(cells);
        }

        #endregion

        private void showPicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] files
               = System.IO.Directory.GetFiles(@"d:\20090505");

            ImageCell[] cells = new ImageCell[100];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Image.FromFile(files[i]);
                Graphics g = Graphics.FromImage(img);
                string text = DateTime.Now.ToShortTimeString() + ":" + i.ToString();
                g.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, 0, 0);
                ImageCell newCell = new ImageCell() { Image = img, Path = "", Text = text, Tag = null };
                cells[i] = newCell;
            }

            this.squareListView1.ShowImages(cells);
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            IIconExtractor extractor = IconExtractor.Default;

            ImageUploadWatcher watcher =
                new ImageUploadWatcher() { PathToWatch = Properties.Settings.Default.ImageUploadPool, };

            Presenter p = new Presenter(this, watcher, extractor);
            p.Start();
        }

        #region IImageScreen Members

        public Camera[] Cameras
        {
            set
            {
                this.cameraTree.Nodes.Clear();

                TreeNode rootNode = new TreeNode()
                {
                    Text = "所有摄像头",
                    ImageIndex = 0,
                    SelectedImageIndex = 0
                };

                Array.ForEach(value, camera =>
                {
                    TreeNode camNode = new TreeNode()
                    {
                        Text = camera.Name,
                        ImageIndex = 1,
                        SelectedImageIndex = 1,
                        Tag = camera,
                    };

                    Action<string> setupCamera = (ip) =>
                    {
                        using (FormConfigCamera form = new FormConfigCamera())
                        {
                            StringBuilder sb = new StringBuilder(form.Text);
                            sb.Append("-[");
                            sb.Append(ip);
                            sb.Append("]");

                            form.Navigate(ip);
                            form.Text = sb.ToString();
                            form.ShowDialog(this);
                        }
                    };

                    TreeNode setupNode = new TreeNode()
                    {
                        Text = "设置",
                        ImageIndex = 2,
                        SelectedImageIndex = 2,
                        Tag = setupCamera,
                    };
                    TreeNode propertyNode = new TreeNode()
                    {
                        Text = "属性",
                        ImageIndex = 3,
                        SelectedImageIndex = 3,
                    };
                    TreeNode ipNode = new TreeNode()
                    {
                        Text = "IP地址:" + camera.IpAddress,
                        ImageIndex = 4,
                        SelectedImageIndex = 4
                    };
                    TreeNode idNode = new TreeNode()
                    {
                        Text = "编号:" + camera.ID.ToString(),
                        ImageIndex = 5,
                        SelectedImageIndex = 5
                    };


                    propertyNode.Nodes.AddRange(new TreeNode[] { ipNode, idNode });
                    camNode.Nodes.AddRange(new TreeNode[] { setupNode, propertyNode });
                    rootNode.Nodes.Add(camNode);
                    
                });

                this.cameraTree.Nodes.Add(rootNode);

                this.cameraTree.ExpandAll();
            }
        }

        #endregion


        private void squareListView1_SelectedCellChanged(object sender, EventArgs e)
        {
            if (this.Observer != null)
            {
                this.Observer.SelectedImageChanged();
            }
        }


        private void simpleButton3_Click(object sender, EventArgs e)
        {
            new RemoteImaging.Query.PicQueryForm().ShowDialog(this);
        }

        private static void SetupExtractor(float leftRatio,
            float rightRatio,
            float topRatio,
            float bottomRatio,
            int minFaceWidth,
            float maxFaceWidthRatio)
        {
            IconExtractor.Default.SetExRatio(topRatio,
                                    bottomRatio,
                                    leftRatio,
                                    rightRatio);

            IconExtractor.Default.SetFaceParas(minFaceWidth, maxFaceWidthRatio);
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }


        private void searchPic_Click(object sender, EventArgs e)
        {
            new RemoteImaging.Query.PicQueryForm().ShowDialog(this);
        }

        private void dnloadVideo_Click(object sender, EventArgs e)
        {

        }

        private void videoSearch_Click(object sender, EventArgs e)
        {
            new RemoteImaging.Query.VideoQueryForm().ShowDialog(this);
        }

        private void options_Click(object sender, EventArgs e)
        {
            using (OptionsForm frm = new OptionsForm())
            {
                IList<Camera> camCopy = new List<Camera>();

                foreach (Camera item in Configuration.Instance.Cameras)
                {
                    camCopy.Add(new Camera() { ID = item.ID, Name = item.Name, IpAddress = item.IpAddress });
                }


                frm.Cameras = camCopy;
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    Configuration.Instance.Cameras = frm.Cameras;
                    Configuration.Instance.Save();

                    Properties.Settings.Default.Save();

                    InitStatusBar();

                    this.Cameras = frm.Cameras.ToArray<Camera>();

                    float ratio = (float)frm.MaxFaceWidth / frm.MinFaceWidth;

                    SetupExtractor(frm.LeftExtRatio, frm.RightExtRatio, frm.TopExtRatio, frm.BottomExtRatio, frm.MinFaceWidth, ratio);
                }
            }

        }

        private void column1by1_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 1;

        }

        private void column2by2_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 2;
        }

        private void column3by3_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 3;
        }

        private void column4by4_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 4;
        }

        private void column5by5_Click(object sender, EventArgs e)
        {
            this.squareListView1.NumberOfColumns = 5;
        }

        private void InitStatusBar()
        {
            statusUploadFolder.Text = "上传目录：" + Properties.Settings.Default.ImageUploadPool;
            statusOutputFolder.Text = "输出目录：" + Properties.Settings.Default.OutputPath;
        }
        private void cameraTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
                return;

            Action<string> setupCamera = e.Node.Tag as Action<string>;
            if (setupCamera != null)
            {
                Camera cam = this.getTopCamera().Tag as Camera;
                setupCamera(cam.IpAddress);
            }

        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
            about.Dispose();
        }

        private void realTimer_Tick(object sender, EventArgs e)
        {
            statusTime.Text = DateTime.Now.ToString();
        }



    }
}
