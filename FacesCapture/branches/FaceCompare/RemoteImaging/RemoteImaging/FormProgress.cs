using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    public partial class FormProgress : Form
    {
        public FormProgress()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = (this.progressBar1.Value+1) % this.progressBar1.Maximum;
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            shouldCancel = true;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += this.UpdateFaceSample;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.RunWorkerAsync();

            Cursor.Current = Cursors.WaitCursor;
            
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.timer1.Dispose();
            shouldCancel = false;
            Cursor.Current = Cursors.Default;

            this.Close();


            MessageBox.Show("特征库生成完毕", "成功", 
                 MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        bool shouldCancel;

        private int GetFaceSamplesCount()
        {
            string[] faceSamples = System.IO.Directory.GetFiles(Properties.Settings.Default.FaceSampleLib, "*.jpg");
            return faceSamples.Length;
        }


        private void UpdateFaceSample(object sender, DoWorkEventArgs args)
        {
            //训练 重新生成 人脸库
            FaceRecognition.FaceRecognizer.FaceTraining(100, 100, Program.EigenNum);
            FaceRecognition.FaceRecognizer.FreeData();
            FaceRecognition.FaceRecognizer.InitData(GetFaceSamplesCount(), Program.ImageLen, Program.EigenNum);
        }

        private void FormProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = shouldCancel;
        }
    }
}
