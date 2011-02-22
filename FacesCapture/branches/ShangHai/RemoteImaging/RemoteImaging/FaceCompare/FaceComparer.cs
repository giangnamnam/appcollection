using System;
using Damany.Imaging.Common;
using FaceProcessingWrapper;
using System.Windows.Forms;
using OpenCvSharp;

namespace RemoteImaging
{
    public class FaceComparer
    {
        private readonly IEventAggregator _eventAggregator;

        private FaceProcessingWrapper.FaceAlignmentWrapper _faceAlignmenter;
        private FaceProcessingWrapper.LbpWrapper _faceLbp;

        public FaceComparer(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            _eventAggregator = eventAggregator;
        }

        public void Init()
        {
            var modelPath = System.IO.Path.Combine(Application.StartupPath,
                                                   Properties.Settings.Default.FaceAlignModelFile);

            var classifierPath = System.IO.Path.Combine(Application.StartupPath,
                                                        Properties.Settings.Default.FaceAlignClassifierFile);

            _faceAlignmenter = new FaceAlignmentWrapper(modelPath, classifierPath, 68);


        }

        public float[] CompareFace(IplImage iplImage)
        {
            if (_faceAlignmenter == null)
                throw new Exception("Init() must be called first");

            var alignedFace = new IplImage(70, 70, BitDepth.U8, 1);
            _faceAlignmenter.RealTimeAlignment(iplImage, alignedFace);



        }
    }
}
