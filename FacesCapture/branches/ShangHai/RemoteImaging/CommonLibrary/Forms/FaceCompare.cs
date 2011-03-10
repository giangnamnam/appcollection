using System;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Damany.RemoteImaging.Common.Presenters;
using Damany.Imaging.Extensions;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using FaceSearchWrapper;

namespace Damany.RemoteImaging.Common.Forms
{
    public partial class FaceCompare : DevExpress.XtraEditors.XtraForm
    {
        private readonly ConfigurationManager _manager;
        private static FaceProcessingWrapper.FaceRecoWrapper _faceComparer;
        private DevExpress.Utils.WaitDialogForm _waitForm;
        private System.Threading.CancellationTokenSource _cts;
        private Tuple<string, int>[] _accuracies;
        private long _currentAccuracy;

        public FaceCompare()
        {
            InitializeComponent();
            _accuracies = new[]{
                new Tuple<string, int>("低", 60),
                new Tuple<string, int>("中", 65),
                new Tuple<string, int>("高", 70),
            };

            _currentAccuracy = 65;

            this.searchFrom.EditValue = DateTime.Now.AddDays(-1);
            this.searchTo.EditValue = DateTime.Now;

            this.targetPic.Paint += new PaintEventHandler(targetPic_Paint);
            this.compareButton.Click += new EventHandler(compareButton_Click);

            if (_faceComparer == null)
            {
                _waitForm = new WaitDialogForm("初始化人脸特征库...", "请稍候");
                _faceComparer = FaceProcessingWrapper.FaceRecoWrapper.FromModel("model.txt",
                                                                            "haarcascade_frontalface_alt2.xml");
            }
        }


        public FaceCompare(ConfigurationManager manager)
            : this()
        {
            _manager = manager;
        }



        void compareButton_Click(object sender, EventArgs e)
        {
            ShowCompareStatus(true);
            ShowCompareButton(false);

            var creteria = DevExpress.Data.Filtering.CriteriaOperator.Parse(
                "CaptureTime >= ? && CaptureTime <= ?",
                this.searchFrom.EditValue, this.searchTo.EditValue);
            this.faceCollection.Criteria = creteria;
            this.faceCollection.LoadingEnabled = true;
            this.faceCollection.LoadAsync(loadFaceCallback);
        }

        void loadFaceCallback(ICollection[] result, Exception ex)
        {
            if (ex != null)
            {
                MessageBox.Show(this, "系统发生异常，请重试。", this.Text);
                return;
            }

            _cts = new CancellationTokenSource();
            var worker = Task.Factory.StartNew(CompareFaces, result, _cts.Token);
            worker.ContinueWith(ant =>
                                    {
                                        if (ant.Exception != null)
                                        {
                                            if (!(ant.Exception.InnerException is OperationCanceledException))
                                            {
                                                MessageBox.Show("系统发生异常，请重试。");
                                            }
                                        }

                                        ShowCompareStatus(false);
                                        ShowCompareButton(true);

                                    }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        public void AttachPresenter(FaceComparePresenter presenter)
        {
            this.presenter = presenter;
        }

        void targetPic_Paint(object sender, PaintEventArgs e)
        {
        }

        private void choosePic_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
            {
                return;
            }

            var img = Damany.Util.Extensions.MiscHelper.FromFileBuffered(this.openFileDialog1.FileName);
            this.ipl = OpenCvSharp.IplImage.FromBitmap((Bitmap)img);

            var fs = new FaceProcessingWrapper.FaceSpecification();
            var suc = _faceComparer.CalcFeature(this.ipl, fs);

            if (!suc)
            {
                MessageBox.Show("人像不满足比对要求，请重新选择人像。",
                    this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.targetPic.Image = img;


            this.targetPic.Invalidate();
            this.compareButton.Enabled = true;
        }

        public DateTime SearchFrom
        {
            get
            {
                return (DateTime)this.searchFrom.EditValue;
            }
        }

        public DateTime SearchTo
        {
            get
            {
                return (DateTime)this.searchTo.EditValue;
            }
        }

        public Image CurrentImage
        {
            set
            {
                if (InvokeRequired)
                {
                    Action<Image> action = img => this.CurrentImage = img;

                    this.BeginInvoke(action, value);
                    return;
                }


                if (this.currentPic.Image != null)
                {
                    this.currentPic.Image.Dispose();
                }

                this.currentPic.Image = value;
            }
        }

        public OpenCvSharp.IplImage Image
        {
            get
            {
                return this.ipl;
            }
        }

        public Rectangle FaceRect
        {
            get
            {
                return this.faceRect;
            }
        }

        public void ClearFaceList()
        {
            this.galleryControl1.Gallery.Groups[0].Items.Clear();
            this.imageList1.Images.Clear();
        }

        public void AddPortrait(Damany.Imaging.Common.Portrait p)
        {
            if (this.InvokeRequired)
            {
                Action<Damany.Imaging.Common.Portrait> action
                    = this.AddPortrait;

                this.BeginInvoke(action, p);
                return;
            }

            this.imageList1.Images.Add(p.GetIpl().ToBitmap());

            var item = new ListViewItem();
            item.Text = (_manager.GetName(p.CapturedFrom.Id) ?? string.Empty) + " " + p.CapturedAt.ToString();
            item.ImageIndex = this.imageList1.Images.Count - 1;


            p.Dispose();
        }


        public void SetStatusText(string msg)
        {
            if (InvokeRequired)
            {
                Action<string> action = SetStatusText;
                this.BeginInvoke(action, msg);
                return;
            }

        }



        private void FaceCompare_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        void CompareFaces(object result)
        {
            var collections = ((ICollection[])result)[0];

            var targetFs = new FaceProcessingWrapper.FaceSpecification();
            _faceComparer.CalcFeature(OpenCvSharp.IplImage.FromBitmap((Bitmap)this.targetPic.Image), targetFs);

            this.RunInUIThread(() =>
            {
                repositoryItemProgressBar1.Maximum = collections.Count;
                this.ClearFaceList();
            });

            int count = 0;
            foreach (Damany.PortraitCapturer.DAL.DTO.Portrait face in collections)
            {
                _cts.Token.ThrowIfCancellationRequested();

                var curPortrait = face;
                var curFaceImg = curPortrait.ImageCopy;
                var clone = curFaceImg.Clone();
                this.currentPic.RunInUIThread(() =>
                                                  {
                                                      this.currentPic.Image = (Image)clone;
                                                      currentPic.Refresh();
                                                  });

                var curFs = new FaceProcessingWrapper.FaceSpecification();
                var clone2 = curFaceImg.Clone();
                var suc = _faceComparer.CalcFeature(OpenCvSharp.IplImage.FromBitmap((Bitmap)clone2), curFs);
                ((IDisposable)clone2).Dispose();
                if (suc)
                {
                    var sim = _faceComparer.CmpFace(targetFs, curFs);
                    if (sim > Interlocked.Read(ref _currentAccuracy))
                    {
                        var clone3 = curFaceImg.Clone();
                        this.galleryControl1.RunInUIThread(() =>
                        {
                            var item = new GalleryItem((Image)clone3,
                                curPortrait.CaptureTime.ToShortDateString(),
                                curPortrait.CaptureTime.ToShortTimeString());
                            item.Hint = curPortrait.CaptureTime.ToString();
                            this.galleryControl1.Gallery.Groups[0].Items.Add(item);
                        });
                    }
                }

                var c = ++count;
                this.RunInUIThread(() =>
                                       {
                                           progressBar.EditValue = c;
                                           var msg = string.Format("已比对： {0}， 待比对： {1}", c, collections.Count - c);
                                           counter.Caption = msg;
                                       });
            }

        }
        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.presenter.ThresholdChanged();
        }

        private Rectangle faceRect;
        private FaceComparePresenter presenter;
        private OpenCvSharp.IplImage ipl;
        private bool started;
        private FaceSearchWrapper.FaceSearch searcher = new FaceSearch();

        private void FaceCompare_Load(object sender, EventArgs e)
        {
            if (_waitForm != null)
            {
                this._waitForm.Dispose();
            }

        }

        private void ShowCompareStatus(bool show)
        {
            progressBar.Visibility = show ? BarItemVisibility.Always : BarItemVisibility.Never;
            counter.Visibility = show ? BarItemVisibility.Always : BarItemVisibility.Never;
        }

        private void ShowCompareButton(bool show)
        {
            compareButton.Visible = show;
            compareButton.Enabled = show;
            cancelButton.Visible = !show;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                _cts.Cancel();
        }

        private void accuracyTrackContainer_CloseUp(object sender, EventArgs e)
        {
            var sel = (int)accuracyTrackBar.EditValue;
            var msg = string.Format("准确度：{0}", _accuracies[sel].Item1);
            accuracyLabel.Caption = msg;

            Interlocked.Exchange(ref _currentAccuracy, _accuracies[sel].Item2);
        }






    }
}
