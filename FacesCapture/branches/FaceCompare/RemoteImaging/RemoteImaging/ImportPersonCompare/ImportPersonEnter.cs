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
        PersonInfoHandleXml perinfo =PersonInfoHandleXml.GetInstance();
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
                //文本文件|*.*|C#文件|*.cs|所有文件|*.*
                ofd.Filter = "Jpeg 文件|*.jpg|Bmp 文件|*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string temp = ofd.FileName;
                    if (temp.EndsWith(".jpg") || temp.EndsWith(".bmp"))
                    {
                        string name = ofd.SafeFileName;
                        picTargetPerson.Image = Image.FromFile(temp);
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
            string id = txtId.Text.ToString();
            string name = txtName.Text.ToString();
            string sex = rabMan.Checked ? "男" : "女";
            int age = Convert.ToInt32(txtAge.Text.ToString());
            string card = txtCard.Text.ToString();
            string filename = GetFileNameWithoutExtension();
            int similarity = cmbSimLevel.SelectedIndex;

            PersonInfo info = new PersonInfo();
            info.ID = id;
            info.Name = name;
            info.Sex = sex;
            info.Age = age;
            info.CardId = card;
            info.FileName = filename;
            info.Similarity = similarity;
            //if (perinfo.HasSameId(id))
            //{
            //    MessageBox.Show("已存在相同编号,请重新输入！","警告");
            //    txtId.Focus();
            //    return;
            //}

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

            IplBitmapComposite iplBitmap = IplBitmapComposite.From((Bitmap) this.picTargetPerson.Image);
            Image curImg = picTargetPerson.Image;
            string savePath = Path.Combine(FileSavePath, info.FileName + ".jpg");
            curImg.Save(savePath);


            //归一化
            OpenCvSharp.CvRect rect = new OpenCvSharp.CvRect(0, 0, iplBitmap.Ipl.Width, iplBitmap.Ipl.Height);
            OpenCvSharp.IplImage[] normalizedImages =
                Program.faceSearch.NormalizeImageForTraining(iplBitmap.Ipl, rect);

            for (int i = 0; i < normalizedImages.Length; ++i)
            {
                string normalizedFaceName = string.Format("{0}_{1:d4}.jpg", info.FileName, i);
                string fullPath = System.IO.Path.Combine(Properties.Settings.Default.FaceSampleLib, normalizedFaceName);
                normalizedImages[i].SaveImage(fullPath);
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

        private void addFinished_Click(object sender, EventArgs e)
        {
            string[] faceSamples = System.IO.Directory.GetFiles(Properties.Settings.Default.FaceSampleLib, "*.jpg");

            if (faceSamples.Length == 0) return;

            Bitmap bmp = (Bitmap)Bitmap.FromFile(faceSamples[0]);

            //训练 重新生成 人脸库
            FaceRecognition.FaceRecognizer.FaceTraining(bmp.Width, bmp.Height, Program.EigenNum);
            FaceRecognition.FaceRecognizer.FreeData();
            FaceRecognition.FaceRecognizer.InitData(faceSamples.Length, Program.ImageLen, Program.EigenNum);
        }
    }
}