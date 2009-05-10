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

namespace RemoteImaging.RealtimeDisplay
{
    public partial class MainForm : Form, IImageScreen
    {
        public MainForm()
        {
            InitializeComponent();
        }

        #region IImageScreen Members

        public Camera SelectedCamera
        {
            get
            {
                return this.navBarGroupCameras.SelectedLink.Item.Tag as Camera;
            }

        }

        public ImageDetail SelectedImage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ImageDetail BigImage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IImageScreenObserver Observer { get; set; }

        public void ShowImages(ImageDetail[] images)
        {
            ImageCell[] cells = new ImageCell[images.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                Image img = Image.FromFile(images[i].FullPath);
                string text = images[i].CaptureTime.ToString();
                ImageCell newCell = new ImageCell() { Image = img, Path = images[i].FullPath, Text = text, Tag = null };
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

        private void extractIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //IIconExtractor extractor = new IconExtractor();

            //string[] files = Directory.GetFiles(@"d:\20090505");
            //ImageClassifier classifier = new ImageClassifier();
            //ImageDetail[] imgs = new ImageDetail[1] { new ImageDetail(files[0]) };
            //classifier.ClassifyImages(imgs);
            //string parentOfBigPicFolder = Directory.GetParent(imgs[0].Path).Parent.FullName.ToString();
            //string destFolder = Path.Combine(parentOfBigPicFolder, Properties.Settings.Default.IconDirectoryName);
            //if (!Directory.Exists(destFolder))
            //{
            //    Directory.CreateDirectory(destFolder);
            //}

            //extractor.ExtractIcons(imgs[0].FullPath, destFolder);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            IIconExtractor extractor = new IconExtractor();
            extractor.Initialize();

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
                this.navBarGroupCameras.ItemLinks.Clear();

                foreach (Camera cam in value)
	            {
                    this.navBarGroupCameras.ItemLinks.Add(
                        new NavBarItemLink(
                            new NavBarItem(){ Caption = cam.Description, SmallImageIndex = 0, Tag = cam }));
	            }
                
            }
        }

        #endregion

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Camera[] cams = new Camera[] { 
                new Camera() { Description = "南门", ID = 3, IpAddress = "192.168.1.1" },
                new Camera() { Description = "北门", ID = 3, IpAddress = "192.168.1.1" },
                new Camera() { Description = "西门", ID = 3, IpAddress = "192.168.1.1" },
            };
            this.Cameras = cams;
        }
    }
}
