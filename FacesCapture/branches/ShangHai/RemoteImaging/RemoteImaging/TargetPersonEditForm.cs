using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL.DTO;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using FaceProcessingWrapper;

namespace RemoteImaging
{
    public partial class TargetPersonEditForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly IMessageBoxService _messageBoxService;
        private UnitOfWork _uow;
        private XPCollection<Damany.PortraitCapturer.DAL.DTO.TargetPerson> _targets;
        private DevExpress.Utils.WaitDialogForm _waitForm;
        private static FaceProcessingWrapper.FaceRecoWrapper _faceComparer;
        private bool _isDirty;
        private bool _isUserCanceling;

        public TargetPersonEditForm()
        {
            InitializeComponent();
            galleryControl1.Gallery.ItemCheckedChanged +=
                (s, e) => removeTarget.Enabled = galleryControl1.Gallery.Groups[0].GetCheckedItems().Count > 0;


            _waitForm = new WaitDialogForm("正在初始化，请稍候...", "请稍候");
            if (_faceComparer == null)
            {
                _waitForm.Caption = "正在初始化人像比对模块...";
                var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model.txt");
                var classifierPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_alt2.xml");
                var comparer = FaceRecoWrapper.FromModel(modelPath, classifierPath);
                _faceComparer = comparer;
            }

            _waitForm.Caption = "正在载入目标人像库...";
            LoadTargets();
        }

        public TargetPersonEditForm(IMessageBoxService messageBoxService)
            : this()
        {
            if (messageBoxService == null) throw new ArgumentNullException("messageBoxService");
            _messageBoxService = messageBoxService;
        }

        private void LoadTargets()
        {
            if (_uow == null)
            {
                _uow = new UnitOfWork();
                _targets = new XPCollection<TargetPerson>(_uow);

            }

            _targets.LoadAsync(LoadTargetCallBack);
        }

        void LoadTargetCallBack(ICollection[] result, Exception ex)
        {
            var t = result[0];
            foreach (TargetPerson p in t)
            {
                var item = AddGalleryItem(p.ImageCopy);
                item.Tag = p;
            }

            _waitForm.Hide();
        }

        private void addNewTarget_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _waitForm.Show();
                _waitForm.Caption = "正在计算特征值...";

                var img = OpenCvSharp.IplImage.FromFile(openFileDialog1.FileName);
                var fs = new FaceProcessingWrapper.FaceSpecification();
                var suc = _faceComparer.CalcFeature(img, fs);
                _waitForm.Hide();
                if (!suc)
                {
                    _messageBoxService.ShowError("人像不满足比对要求，请重新选择人像。");
                    return;
                }

                var target = new TargetPerson(_uow);
                target.EyeBrowShape = fs.EyebrowShape;
                target.EyebrowRatio = fs.EyebrowRatio;
                target.FeaturePoints = fs.Features;
                var path = System.IO.Path.Combine(Properties.Settings.Default.OutputPath,
                                                  @"TargetFeature\" + Guid.NewGuid() + ".txt");
                var dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                target.FeatureFilePath = path;
                var imgPath = path.Replace(".txt", ".jpg");
                img.SaveImage(imgPath);
                target.ImagePath = imgPath;

                var item = AddGalleryItem(img.ToBitmap());
                item.Tag = target;
                _isDirty = true;
            }
        }

        private GalleryItem AddGalleryItem(Image img)
        {
            var item = new GalleryItem(img, null, null);
            galleryControl1.Gallery.Groups[0].Items.Add(item);
            return item;
        }

        private void TargetPersonEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isUserCanceling)
            {
                if (_isDirty)
                {
                    var result = _messageBoxService.ShowQuestion("你编辑了目标库，保存你所做的修改吗？");
                    if (result == DialogResult.Yes)
                    {
                        DeleteAndCommitChanges();
                    }
                }
            }
        }

        private void removeTarget_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var items = galleryControl1.Gallery.Groups[0].GetCheckedItems();
            if (items.Count > 0)
            {
                foreach (var galleryItem in items)
                {
                    var t = galleryItem.Tag as TargetPerson;
                    if (t != null)
                    {
                        t.Delete();
                        _isDirty = true;
                    }
                    galleryControl1.Gallery.Groups[0].Items.Remove(galleryItem);
                }
            }
        }

        private void DeleteAndCommitChanges()
        {
            _uow.CommitChanges();
            Mediator.Instance.NotifyColleagues<object>("SuspectsLibChanged", null);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            _isUserCanceling = false;
            DeleteAndCommitChanges();
            _isDirty = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _isUserCanceling = true;
        }
    }
}