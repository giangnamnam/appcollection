using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Damany.PC.Domain;
using System.Timers;
using System.Runtime.InteropServices;

namespace RemoteImaging
{
    public partial class OptionsForm : DevExpress.XtraEditors.XtraForm
    {
        public OptionsForm()
        {

            InitializeComponent();
            InitCamDatagridView();
            InitCameraMode();

        }

        public void AttachPresenter(OptionsPresenter presenter)
        {
            this._presenter = presenter;
        }


        private void InitCamDatagridView()
        {
            cameraType.Properties.Items.AddRange(Enum.GetNames(typeof(Damany.PC.Domain.CameraProvider)).ToArray());
        }

        Damany.RemoteImaging.Common.ConfigurationManager configManager
            = Damany.RemoteImaging.Common.ConfigurationManager.GetDefault();

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

        public IList<CameraInfo> Cameras
        {

            set
            {
                if (value != null && value.Count > 0)
                {
                    var cam = value[0];

                    cameraName.EditValue = cam.Name;
                    cameraNumber.EditValue = cam.Id;
                    cameraType.EditValue = cam.Provider.ToString();
                    loginUserName.EditValue = cam.LoginUserName;
                    loginPwd.EditValue = cam.LoginPassword;
                    cameraUri.EditValue = cam.Location == null ? null : cam.Location.ToString();
                    frameCaptureInterval.EditValue = cam.Interval;
                    yuntaiId.EditValue = cam.YunTaiId;
                    yuntaiUri.EditValue = cam.YunTaiUri == null ? null: cam.YunTaiUri.ToString();
                }
            }
            get
            {
                var cameras = new List<CameraInfo>();
                var cam = new CameraInfo();

                cam.Name =  (string) cameraName.EditValue;
                cam.Id = (int) cameraNumber.EditValue;

                cam.Provider = (CameraProvider) Enum.Parse(typeof(CameraProvider), (string) cameraType.EditValue);

                cam.LoginUserName = (string) loginUserName.EditValue;
                cam.LoginPassword = (string) loginPwd.EditValue;
                cam.Location = string.IsNullOrEmpty((string) cameraUri.EditValue) ? null : new Uri((string) cameraUri.EditValue);
                cam.Interval = (int) frameCaptureInterval.EditValue;
                cam.YunTaiId = (int) yuntaiId.EditValue;
                cam.YunTaiUri = string.IsNullOrEmpty((string) yuntaiUri.EditValue) ? null:  new Uri((string) yuntaiUri.EditValue);
                
                cameras.Add(cam);
                return cameras;
            }



        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
        }

        private void linkLabelConfigCamera_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var selectedCam = Cameras.FirstOrDefault();
            if (selectedCam != null)
            {
                using (var form = new FormConfigCamera())
                {
                    var sb = new StringBuilder(form.Text);
                    sb.Append("-[");
                    sb.Append(selectedCam.Location.ToString());
                    sb.Append("]");

                    form.Navigate(selectedCam.Location.ToString());
                    form.Text = sb.ToString();
                    form.ShowDialog(this);
                }
            }
        }



        private BindingList<CameraInfo> camList =
            new BindingList<CameraInfo>();

        private BindingSource bs;

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this._presenter.UpdateConfig();
            unitOfWork1.CommitChanges();
        }





        private void OptionsForm_Load(object sender, EventArgs e)
        {

        }




        #region 弹出窗口的操作
        public void ShowResDialog(int picIndex, string msg)
        {
            AlertSettingRes asr = new AlertSettingRes(msg, picIndex);
            asr.HeightMax = 169;
            asr.WidthMax = 175;
            asr.ShowDialog(this);
        }
        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Properties.Settings.Default.ComName = cmbComPort.Text;
        }




        private void btnBrowseSavePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.ShowNewFolderButton = true;
                dlg.RootFolder = Environment.SpecialFolder.MyComputer;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    Properties.Settings.Default.WarnPicSavePath = dlg.SelectedPath;
            }
        }

        private OptionsPresenter _presenter;
        private void browseForOutputFolder_Click_1(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                textBoxOutputFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void InitCameraMode()
        {
            var shutters = new List<Tuple<string, int>>();
            shutters.Add(new Tuple<string, int>("25", 0));
            shutters.Add(new Tuple<string, int>("50", 1));
            shutters.Add(new Tuple<string, int>("120", 2));
            shutters.Add(new Tuple<string, int>("250", 3));
            shutters.Add(new Tuple<string, int>("500", 4));
            shutters.Add(new Tuple<string, int>("1000", 5));
            shutters.Add(new Tuple<string, int>("2000", 6));
            shutters.Add(new Tuple<string, int>("4000", 7));

            shutterSpeed.Properties.DisplayMember = "Item1";
            shutterSpeed.Properties.ValueMember = "Item2";

            shutterSpeed.Properties.DataSource = shutters;

            for (int i = 0; i < 101; i++)
            {
                irisLevel.Properties.Items.Add(i);
            }
        }
    }
}