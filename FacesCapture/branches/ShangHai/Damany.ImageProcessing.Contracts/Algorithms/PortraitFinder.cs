using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Util.Extensions;
using ImageProcess;
using OpenCvSharp;

namespace Damany.Imaging.Processors
{
    using Damany.Imaging.Common;

    public class PortraitFinder
    {
        private System.Threading.CancellationToken _cancellationToken;
        public IEnumerable<Damany.Imaging.Common.IFacePostFilter> PostFilters { get; set; }
        public System.Threading.CancellationTokenSource TokenSource { get; set; }
        private System.Drawing.Rectangle _roi;

        public ConfigurationHandlers.FaceSearchConfigSectionHandler Configuration
        {
            set
            {
                if (value.MaxFaceWidth <= value.MinFaceWidth)
                {
                    throw new ArgumentException("MaxFaceWidth must be bigger than  MinFaceWidth");
                }

                var ratio = (double)value.MaxFaceWidth / value.MinFaceWidth;
                this.searcher.SetFaceParas(value.MinFaceWidth, ratio);
            }
        }

        public System.Drawing.Rectangle ROI
        {
            set
            {
                searcher.SetRoi(value.X, value.Y, value.Width, value.Height);
                _roi = value;
            }
        }

        public PortraitFinder()
        {
            this.searcher = new FaceSearchWrapper.FaceSearch();

            PostFilters = new List<IFacePostFilter>(0);
        }

        public List<Portrait> ProcessFrame(Frame motionFrame, System.Threading.CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
            var portraits = HandleMotionFrame(motionFrame);
            return portraits;
        }

        private List<Portrait> PostProcessPortraits(List<Portrait> portraits)
        {
            for (var i = 0; i < portraits.Count; i++)
            {
                if (!IsFace(portraits[i]))
                {
                    portraits[i].Dispose();
                    portraits[i] = null;
                }
            }

            return portraits.Where(p => p != null).ToList();
        }

        bool IsFace(Portrait portrait)
        {
            foreach (var facePostFilter in PostFilters)
            {
                if (!facePostFilter.IsFace(portrait))
                {
                    return false;
                }
            }

            return true;
        }



        public List<Portrait> HandleMotionFrame(Frame motionFrame)
        {
            return this.SearchIn(motionFrame);
        }



        private List<Portrait> SearchIn(Frame motionFrame)
        {

            var faces = this.searcher.SearchFace(motionFrame.GetImage());

            if (faces.Length == 0)
            {
                motionFrame.Dispose();
                return new List<Portrait>(0);
            }

            var portraitQuery = from f in faces
                                let faceImg = motionFrame.GetImage().GetSub(f.Bounds)
                                select new Portrait(faceImg)
                                {
                                    FaceBounds = f.FaceBoundsInPortrait,
                                    Frame = motionFrame.Clone(),
                                    CapturedAt = motionFrame.CapturedAt,
                                    CapturedFrom = motionFrame.CapturedFrom,
                                };


            return portraitQuery.ToList();

        }


        private static OpenCvSharp.CvRect FrameToPortrait(OpenCvSharp.CvRect bounds, OpenCvSharp.CvRect faceBounds)
        {
            faceBounds.X -= bounds.X;
            faceBounds.Y -= bounds.Y;

            return faceBounds;
        }

        private static List<Portrait> ExpandPortraitsList(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFoundFrameQuery = from m in motionFrames
                                          join p in portraits
                                            on m.Guid equals p.BaseFrame.guid
                                          select new { Frame = m, Portraits = p, };

            var expanedPortraits = from frame in portraitFoundFrameQuery
                                   from p in frame.Portraits.Portraits
                                   select new Portrait(p.Face)
                                   {
                                       FaceBounds = FrameToPortrait(p.FacesRect, p.FacesRectForCompare),
                                       Frame = frame.Frame.Clone(),
                                       CapturedAt = frame.Frame.CapturedAt,
                                       CapturedFrom = frame.Frame.CapturedFrom,
                                   };


            return expanedPortraits.ToList();
        }

        private static IEnumerable<Frame> GetFacelessFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var noPortraitFrameQuery = from m in motionFrames
                                       where !portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                       select m;

            return noPortraitFrameQuery;
        }

        private static IEnumerable<Frame> GetFaceFrames(IEnumerable<Frame> motionFrames, IEnumerable<Target> portraits)
        {
            var portraitFrameQuery = from m in motionFrames
                                     where portraits.Any(t => t.BaseFrame.guid.Equals(m.Guid))
                                     select m;

            return portraitFrameQuery;
        }



        FaceSearchWrapper.FaceSearch searcher;
    }
}
