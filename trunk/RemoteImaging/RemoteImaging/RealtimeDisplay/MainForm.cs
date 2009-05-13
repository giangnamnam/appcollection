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

            for (int i = 0; i < 5; i++)
            {
                this.squareNumber.Items.Add(i+1);
            }

            this.squareNumber.SelectedItem = Properties.Settings.Default.ColumnNumber;
            Camera[] cams = new Camera[Configuration.Instance.Cameras.Count];
            Configuration.Instance.Cameras.CopyTo(cams, 0);
            this.Cameras = cams;

        }

        private Camera getSelCamera()
        {
            if (this.cameraComboBox.ComboBox.SelectedItem != null)
            {
                Camera cam = this.cameraComboBox.ComboBox.SelectedItem as Camera;
                return cam;

            }

            return null;
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
                        img = new ImageDetail(c.Path);
                    }
                    
                }

                return img;
            }

        }

        public ImageDetail BigImage
        {
            set
            {
                Image img = Image.FromFile(value.FullPath);
                this.pictureEdit1.Image = img;
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
                this.cameraComboBox.ComboBox.DataSource = value;
                this.cameraComboBox.ComboBox.DisplayMember = "Name";
                this.cameraComboBox.SelectedIndex = 0;
            }
        }

        #endregion

        private void testToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Camera[] cams = new Camera[] { 
                new Camera() { Name = "南门", ID = 3, IpAddress = "192.168.1.1" },
                new Camera() { Name = "北门", ID = 3, IpAddress = "192.168.1.1" },
                new Camera() { Name = "西门", ID = 3, IpAddress = "192.168.1.1" },
            };
            this.Cameras = cams;
        }

        private void squareListView1_SelectedCellChanged(object sender, EventArgs e)
        {
            if (this.Observer != null)
            {
                this.Observer.SelectedImageChanged();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Camera[] cams = new Camera[] {
                new Camera() { Name = "所有", ID = -1, IpAddress = "192.168.1.1" },
                new Camera() { Name = "南门", ID = 1, IpAddress = "192.168.1.1" },
                new Camera() { Name = "北门", ID = 2, IpAddress = "192.168.1.1" },
                new Camera() { Name = "西门", ID = 3, IpAddress = "192.168.1.1" },
            };
            this.Cameras = cams;
        }

        private void squareNumber_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.squareNumber.Text))
            {
                return;
            }

            int n = 0;
            
            if (int.TryParse(this.squareNumber.Text, out n))
            {
                if (n < 1)
                {
                    MessageBox.Show("数字应该 > 0");
                    return;
                }

                if (n == this.squareListView1.Count)
                {
                    return;
                }

                this.squareListView1.Count = n;
            }
            else
            {
                MessageBox.Show("无效输入, 应该输入数字, 且数字 >= 1");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            new RemoteImaging.Query.QueryForm().ShowDialog(this);
        }

        private void optionsButton_Click(object sender, EventArgs e)
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

                    this.Cameras = frm.Cameras.ToArray<Camera>();
                }
            }
        }

        private void squareNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.squareListView1.Count = (int) this.squareNumber.SelectedItem;
        }

      
 
    }
}
