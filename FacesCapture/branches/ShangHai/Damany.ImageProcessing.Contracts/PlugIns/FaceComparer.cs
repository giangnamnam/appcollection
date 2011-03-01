using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Damany.Imaging.Common;
using FaceProcessingWrapper;
using MiscUtil;
using Damany.Imaging.Extensions;
using OpenCvSharp;
using System.Threading.Tasks;

namespace Damany.Imaging.PlugIns
{
    public class FaceComparer
    {
        private int _featurePointsCount = 68;
        private int _alignedFaceImageSize = 70;

        private IEnumerable<PersonOfInterest> _personsOfInterests;
        private readonly IRepositoryFaceComparer _comparer;
        private FaceAlignmentWrapper _faceAlignmenter;
        private IlluminationWrapper _illuminator;

        private bool _initialized = false;
        private bool _started = false;
        private float _sensitivity = 0;


        public IEnumerable<IFaceSatisfyCompareCretia> FacePreFilter { get; set; }
        public float Threshold { get; set; }
        public IEventAggregator EventAggregator { get; set; }

        public string FaceAlignmentModelPath { get; set; }
        public string FaceAlignClassifierPath { get; set; }

        public string FaceRepositoryPath { get; set; }
        public string IlluminateReferenceFilePath { get; set; }


        public FaceComparer(
                            IEnumerable<PersonOfInterest> personsOfInterests,
                            IRepositoryFaceComparer comparer
                            )
        {
            _personsOfInterests = personsOfInterests;
            _comparer = comparer;
            if (comparer == null) throw new ArgumentNullException("comparer");

            FacePreFilter = new List<IFaceSatisfyCompareCretia>(0);
            this.goSignal = new AutoResetEvent(false);

        }


        public void InitializeAsync()
        {
            return;

            if (!_initialized)
            {
                EventAggregator.PublishIsBusyEvent(true);

                var worker = Task.Factory.StartNew(() =>
                                          {
                                              var referenceJpg =
                                               IplImage.FromFile(IlluminateReferenceFilePath);
                                              _illuminator = new IlluminationWrapper(referenceJpg);

                                              _faceAlignmenter = new FaceAlignmentWrapper(FaceAlignmentModelPath,
                                                                                          FaceAlignClassifierPath,
                                                                                          _featurePointsCount);

                                              var pois = LoadPersonOfInterests();
                                              _comparer.Load(pois.ToList());

                                              this._personsOfInterests = pois;

                                              this.Run = true;
                                              _initialized = true;
                                          });

                worker.ContinueWith(ant =>
                                        {
                                            EventAggregator.PublishIsBusyEvent(false);
                                            if (ant.Exception != null)
                                            {
                                                var msg = "人脸识别库初始化失败\r\n" + ant.Exception.InnerException.Message;
                                                System.Windows.Forms.MessageBox.Show(msg);
                                            }

                                        });

            }
        }

        private List<PersonOfInterest> LoadPersonOfInterests()
        {
            var personOfInterests = new List<PersonOfInterest>();

            var faceImagesFiles = from f in System.IO.Directory.EnumerateFiles(FaceRepositoryPath, "*.jpg")
                                  let ptFile = f + ".pts"
                                  where System.IO.File.Exists(ptFile)
                                  select f;

            foreach (var faceImagesFile in faceImagesFiles)
            {
                try
                {
                    var pointFileName = faceImagesFile + ".pts";
                    CvPoint[] points = ReadFeaturePoints(pointFileName);
                    var org = IplImage.FromFile(faceImagesFile);

                    IplImage alignedFace;
                    var result = PreProcess(org, out alignedFace, points);

                    if (result)
                    {
                        var poi = new PersonOfInterest(org, alignedFace);
                        personOfInterests.Add(poi);
                    }
                }
                catch (System.Exception ex)
                {

                }
            }

            return personOfInterests;
        }

        private bool PreProcess(IplImage org, out IplImage processed, CvPoint[] points = null)
        {
            IplImage illuminated = IlluminateImage(org);
            IplImage alignedFace;
            var result = AlignFace(illuminated, out alignedFace, points);
            illuminated.Dispose();
            processed = alignedFace;

            return result;
        }

        private IplImage IlluminateImage(IplImage org)
        {
            var processed = (IplImage)org.Clone();
            _illuminator.Process(org, processed);
            return processed;
        }

        private bool AlignFace(IplImage original, out IplImage aligned, CvPoint[] points = null)
        {
            System.Diagnostics.Debug.WriteLine(original.Size.ToString());


            var tmp = OpenCvSharp.Cv.CreateImage(new CvSize(_alignedFaceImageSize, _alignedFaceImageSize),
                                                     BitDepth.U8, 1);

            var succeed = points != null ?
                _faceAlignmenter.LibFaceAlignment(original, tmp, points) :
                _faceAlignmenter.RealTimeAlignment(original, tmp);

            if (!succeed)
            {
                tmp.Dispose();
                tmp = null;
            }

            aligned = tmp;
            return succeed;
        }

        private CvPoint[] ReadFeaturePoints(string pointFileName)
        {
            var points = new List<CvPoint>();

            using (var fileStream = System.IO.File.OpenText(pointFileName))
            {
                fileStream.ReadLine();
                fileStream.ReadLine();
                fileStream.ReadLine();

                for (int i = 0; i < _featurePointsCount; i++)
                {
                    var line = fileStream.ReadLine();
                    CvPoint point = ParseLine(line);
                    points.Add(point);
                }

            }

            return points.ToArray();

        }

        private CvPoint ParseLine(string line)
        {
            var splits = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var x = int.Parse(splits[0]);
            var y = int.Parse(splits[1]);
            return new CvPoint(x, y);
        }


        public void Start()
        {
            if (!_started)
            {
                _workerTask = Task.Factory.StartNew(this.WorkerThread, _tokenSource.Token,
                                                    TaskCreationOptions.LongRunning, TaskScheduler.Default);
                _started = true;
            }

        }

        public void ProcessPortraits(IList<Portrait> portraits)
        {
            if (!_initialized) return;

            var clone = portraits.Select(p => p.Clone()).ToList();
            this.Enqueue(clone);
            goSignal.Set();

        }

        public void SignalToStop()
        {
            _tokenSource.Cancel();
            goSignal.Set();
        }

        public void WaitForStop()
        {
            if (_workerTask != null && _started)
            {

                _workerTask.Wait();

                var list = portraitsQueue.ToList();
                portraitsQueue.Clear();
                list.ForEach(l => l.ToList().ForEach(p => p.Dispose()));
            }
        }


        public float Sensitivity
        {
            set
            {
                if (_sensitivity != value)
                {
                    _comparer.SetSensitivity(value);
                    _sensitivity = value;
                }
            }
        }

        public event EventHandler<MiscUtil.EventArgs<PersonOfInterestDetectionResult>> PersonOfInterestDected;

        private void InvokePersonOfInterestDected(PersonOfInterestDetectionResult e)
        {
            var handler = PersonOfInterestDected;
            if (handler != null) handler(this, new EventArgs<PersonOfInterestDetectionResult>(e));
        }

        private void WorkerThread()
        {
            while (true)
            {
                _tokenSource.Token.ThrowIfCancellationRequested();

                this.goSignal.WaitOne();

                var portraits = this.Dequeue();
                if (portraits == null)
                {
                    continue;
                }

                if (portraits.Count == 0)
                {
                    return;
                }

                IEnumerable<Portrait> filtered = FiltPortraits(portraits);

                foreach (var portrait in filtered)
                {
                    _tokenSource.Token.ThrowIfCancellationRequested();

                    IplImage aligned;
                    var alignSuccess = PreProcess(portrait.GetIpl(), out aligned);
                    if (!alignSuccess) continue;

                    var compareResults = _comparer.CompareTo(aligned);
                    aligned.Dispose();

                    var matches = from r in compareResults
                                  where r.Similarity > Threshold
                                  orderby r.Similarity descending
                                  select r;

                    var top = matches;//.Take(1);
                    NotifyListeners(portrait, top);

                }
            }

        }

        private void NotifyListeners(Portrait portrait, IEnumerable<RepositoryCompareResult> matches)
        {
            foreach (var match in matches)
            {
                var args = new PersonOfInterestDetectionResult
                               {
                                   Details = match.PersonInfo,
                                   Portrait = portrait,
                                   Similarity = match.Similarity
                               };

                if (EventAggregator != null)
                {
                    EventAggregator.PublishFaceMatchEvent(args);
                }
            }
        }

        private IEnumerable<Portrait> FiltPortraits(IList<Portrait> portraits)
        {
            var filteredList = new List<Portrait>();

            foreach (var current in portraits)
            {
                if (!this.CanProceedToCompare(current))
                {
                    current.Dispose();
                }
                else
                {
                    filteredList.Add(current);
                }
            }

            return filteredList;
        }

        private bool CanProceedToCompare(Portrait portrait)
        {
            foreach (var facePreFilter in FacePreFilter)
            {
                if (!facePreFilter.CanSatisfy(portrait))
                {
                    return false;
                }
            }

            return true;
        }

        private void Enqueue(IList<Portrait> portraits)
        {
            if (portraits.Count == 0)
            {
                return;
            }


            lock (queueLocker)
            {
                if (portraitsQueue.Count == 100)
                {
                    return;
                }

                portraitsQueue.Enqueue(portraits);
                goSignal.Set();
            }
        }

        private IList<Portrait> Dequeue()
        {
            lock (queueLocker)
            {
                if (portraitsQueue.Count > 0)
                {
                    return portraitsQueue.Dequeue();
                }

                return null;
            }
        }

        private bool Run
        {
            get
            {
                lock (runLocker)
                {
                    return run;
                }
            }
            set
            {
                lock (runLocker)
                {
                    this.run = value;
                }
            }
        }



        private Queue<IList<Portrait>> portraitsQueue = new Queue<IList<Portrait>>();
        private object queueLocker = new object();

        private System.Threading.Tasks.Task _workerTask;
        private System.Threading.CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private AutoResetEvent goSignal;

        private bool run;
        private object runLocker = new object();
    }
}