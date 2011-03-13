using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL.DTO;
using DevExpress.Xpo;
using FaceProcessingWrapper;
using System.Threading.Tasks;
using Portrait = Damany.Imaging.Common.Portrait;

namespace RemoteImaging
{
    public class FaceComparer
    {
        private int _featurePointsCount = 68;
        private int _alignedFaceImageSize = 70;

        private TargetPerson[] _targets;
        private FaceRecoWrapper _faceComparer;

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


        public FaceComparer()
        {
            FacePreFilter = new List<IFaceSatisfyCompareCretia>(0);
            this.goSignal = new AutoResetEvent(false);

            Mediator.Instance.RegisterHandler("SuspectsLibChanged", (Action<object>) SuspectsLibChanged);
        }


        public void InitializeAsync()
        {
            if (!_initialized)
            {
                EventAggregator.PublishIsBusyEvent(true);

                var worker = Task.Factory.StartNew(() =>
                                          {
                                              var pois = LoadPersonOfInterests();
                                              var modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "model.txt");
                                              var classifierPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_alt2.xml");
                                              var comparer = FaceRecoWrapper.FromModel(modelPath, classifierPath);
                                              _faceComparer = comparer;

                                              Targets = pois;

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

        private TargetPerson[] LoadPersonOfInterests()
        {
            using (var session = new Session())
            {
                var xpc = new XPCollection<TargetPerson>();
                xpc.Load();
                return xpc.ToArray();
            }
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
                    _sensitivity = value;
                }
            }
        }

        private object _targetLock = new object();
        private TargetPerson[] Targets
        {
            get
            {
                lock (_targetLock)
                {
                    return _targets;
                }
            }
            set
            {
                lock (_targetLock)
                {
                    _targets = value;
                }
            }
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


                foreach (var portrait in portraits)
                {
                    var fs = new FaceSpecification();
                    var canCompare = _faceComparer.CalcFeature(portrait.GetIpl(), fs);
                    if (canCompare)
                    {
                        var tgs = Targets;
                        foreach (var targetPerson in tgs)
                        {
                            _tokenSource.Token.ThrowIfCancellationRequested();
                            var ts = new FaceSpecification();
                            ts.EyebrowRatio = targetPerson.EyebrowRatio;
                            ts.EyebrowShape = targetPerson.EyebrowRatio;
                            ts.Features = targetPerson.FeaturePoints;
                            var compareResults = _faceComparer.CmpFace(fs, ts);
                            if (compareResults > 70)
                            {
                                var fdr = new PersonOfInterestDetectionResult()
                                              {
                                                  Similarity = compareResults,
                                                  Suspect = portrait,
                                                  Target = targetPerson
                                              };
                                Mediator.Instance.NotifyColleagues(fdr);
                            }

                        }
                    }


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

        private void SuspectsLibChanged(object state)
        {
            var tgs = this.LoadPersonOfInterests();
            Targets = tgs;
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