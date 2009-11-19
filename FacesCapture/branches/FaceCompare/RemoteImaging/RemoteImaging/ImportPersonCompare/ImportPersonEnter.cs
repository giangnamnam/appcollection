using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using OpenCvSharp;
using ImageProcess;
using RemoteImaging.Core;

namespace RemoteImaging.ImportPersonCompare
{
    public partial class ImportPersonEnter : Form
    {

        public ImportPersonEnter()
        {
            InitializeComponent();
            InitCotrol(false);
        }
        PersonInfoHandleXml perinfo = PersonInfoHandleXml.GetInstance();
        string FileSavePath = Properties.Settings.Default.ImpSelectPersonPath;
        protected void InitCotrol(bool statu)
        {
            if (!statu)
            {
                txtAge.Text = "";
                txtCard.Text = "";
                txtId.Text = "";
                txtName.Text = "";
                rabMan.Checked = true;
                if (picTargetPerson.Image != null)
                {
                    picTargetPerson.Image.Dispose();
                    picTargetPerson.Image = null;
                }
            }
            txtAge.Enabled = statu;
            txtCard.Enabled = statu;
            txtId.Enabled = statu;
            txtName.Enabled = statu;
            rabMan.Enabled = statu;
            rabWoman.Enabled = statu;
            cmbSimLevel.SelectedIndex = 0;
        }

        private void btnBrowseImpPerson_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.RestoreDirectory = true;
                ofd.Filter = "Jpeg 文件|*.jpg|Bmp 文件|*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string temp = ofd.FileName;
                    if (temp.EndsWith(".jpg") || temp.EndsWith(".bmp"))
                    {
                        string name = ofd.SafeFileName;
                        picTargetPerson.Image = Image.FromFile(temp);
                        picTargetPerson.Image.Tag = name;
                        InitCotrol(true);
                    }
                    else
                    {
                        MessageBox.Show("请选择以'.jpg'或者'.bmp'结尾的图片！", "提示");
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            String oldFileName = this.picTargetPerson.Image.Tag as string;

            String fileName = System.Guid.NewGuid().ToString().ToUpper() + System.IO.Path.GetExtension(oldFileName);

            //搜索人脸
            Frame fm = new Frame();
            fm.image = OpenCvSharp.IplImage.FromBitmap((Bitmap)this.picTargetPerson.Image);
            ImageProcess.Target[] facesFound = Program.faceSearch.SearchFacesFastMode(fm);

            if (facesFound.Length == 0
                || facesFound[0].Faces.Length != 1)
            {
                MessageBox.Show("搜索人脸未能完成");
                fm.image.Dispose();
                return;
            }

            OpenCvSharp.IplImage iplFace = facesFound[0].Faces[0];

            string savePath = Path.Combine(FileSavePath, fileName);
            fm.image.SaveImage(savePath);


            //归一化
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect(0, 0, iplFace.Width, iplFace.Height);
            OpenCvSharp.IplImage[] normalizedImages =
                Program.faceSearch.NormalizeImageForTraining(iplFace, rect);

            for (int i = 0; i < normalizedImages.Length; ++i)
            {
                string normalizedFaceName = string.Format("{0}_{1:d4}.jpg",
                    System.IO.Path.GetFileNameWithoutExtension(fileName), i);

                string fullPath = System.IO.Path.Combine(Properties.Settings.Default.FaceSampleLib,
                    normalizedFaceName);



                normalizedImages[i].SaveImage(fullPath);
            }

            string id = txtId.Text.ToString();
            string name = txtName.Text.ToString();
            string sex = rabMan.Checked ? "男" : "女";
            int age = Convert.ToInt32(txtAge.Text.ToString());
            string card = txtCard.Text.ToString();
            int similarity = cmbSimLevel.SelectedIndex;

            PersonInfo info = new PersonInfo();
            info.ID = id;
            info.Name = name;
            info.Sex = sex;
            info.Age = age;
            info.CardId = card;
            info.FileName = fileName;
            info.Similarity = similarity;


            if (perinfo.HasCurInfoNode(card))
            {
                string msg = string.Format("已经存在身份证号为{0}的目标人！", card);
                if (MessageBox.Show(msg, "是否添加该目标人？", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    perinfo.WriteInfo(info);
                else
                    return;
            }
            else
            {
                perinfo.WriteInfo(info);
            }

            //指定路径 文件名 保存到人脸库中
            if (!Directory.Exists(FileSavePath))
            {
                Directory.CreateDirectory(FileSavePath);
            }


            Array.ForEach(normalizedImages, ipl => ipl.Dispose());

            InitCotrol(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        //generic pic filename
        protected string GetFileNameWithoutExtension()
        {
            DateTime dtime = DateTime.Now;
            string year = dtime.Year.ToString();
            return string.Format("{0}{1}{2}{3}{4}{5}{6}", year.Substring(2, 2),
                                                                    dtime.Month.ToString("d2"),
                                                                    dtime.Day.ToString("d2"),
                                                                    dtime.Hour.ToString("d2"),
                                                                    dtime.Minute.ToString("d2"),
                                                                    dtime.Second.ToString("d2"),
                                                                    dtime.Millisecond.ToString("d3"));
        }

        private void ImportPersonEnter_Load(object sender, EventArgs e)
        {

        }

        private int GetFaceSamplesCount()
        {
            string[] faceSamples = System.IO.Directory.GetFiles(Properties.Settings.Default.FaceSampleLib, "*.jpg");
            return faceSamples.Length;
        }

       
        private void addFinished_Click(object sender, EventArgs e)
        {
            if (GetFaceSamplesCount() < Program.EigenNum)
            {
                MessageBox.Show(this, "样本数量不足, 请继续添加", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FormProgress form = new FormProgress();
            form.ShowDialog(this);
            SuspectsRepository.Instance.ReLoad();
           

        }

        
    }
}