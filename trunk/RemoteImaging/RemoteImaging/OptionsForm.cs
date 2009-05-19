using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace RemoteImaging
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
            InitCamDatagridView();
        }


        public float LeftExtRatio
        {
            get { return float.Parse(this.leftExtRatio.Text); }
        }

        public float RightExtRatio
        {
            get { return float.Parse(this.rightExtRatio.Text); }
        }

        public float TopExtRatio
        {
            get { return float.Parse(this.topExtRatio.Text); }
        }

        public float BottomExtRatio
        {
            get { return float.Parse(this.bottomExtRatio.Text); }
        }

        public int MinFaceWidth { get { return int.Parse(this.minFaceWidth.Text); } }

        public int MaxFaceWidth { get { return int.Parse(this.maxFaceWidth.Text); } }

        public float DownSamplingRatio { get { return float.Parse(this.downSamplingRatio.Text); } }

        private void InitCamDatagridView()
        {
            InitCamList();

            this.dataGridCameras.AutoGenerateColumns = false;
            this.dataGridCameras.Columns[0].DataPropertyName = "Name";
            this.dataGridCameras.Columns[1].DataPropertyName = "ID";
            this.dataGridCameras.Columns[2].DataPropertyName = "IpAddress";

        }


        private void InitCamList()
        {
            XDocument camXMLDoc = XDocument.Load(Properties.Settings.Default.CamConfigFile);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            camList.Clear();
            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                camList.Add(
                    new Camera() { ID = id, IpAddress = camElement.Attribute("ip").Value, Name = camElement.Attribute("name").Value });
            }
        }
        private void browseForUploadFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.ImageUploadPool = dlg.SelectedPath;
            }

        }

        private void browseForOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.OutputPath = dlg.SelectedPath;
            }
        }

        public IList<Camera> Cameras
        {
            get
            {
                IList<Camera> cams = new List<Camera>();

                foreach (Camera item in camList)
                {
                    cams.Add(item);
                }

                return cams;
            }

            set
            {
                camList.Clear();
                foreach (Camera item in value)
                {
                    camList.Add(item);
                }

                bs = new BindingSource();
                bs.DataSource = camList;

                this.dataGridCameras.DataSource = bs;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void SaveCamConfig()
        {

        }


        private void linkLabelConfigCamera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (bs.Current == null)
            {
                return;
            }

            Camera cam = bs.Current as Camera;
            if (string.IsNullOrEmpty(cam.IpAddress))
            {
                return;
            }

            using (FormConfigCamera form = new FormConfigCamera())
            {
                StringBuilder sb = new StringBuilder(form.Text);
                sb.Append("-[");
                sb.Append(cam.IpAddress);
                sb.Append("]");

                form.Navigate(cam.IpAddress);
                form.Text = sb.ToString();
                form.ShowDialog(this);
            }
        }


        private BindingList<Camera> camList =
            new BindingList<Camera>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {

        }
    }
}
