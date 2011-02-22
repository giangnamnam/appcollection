using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class Portrait : CapturedImageObject
    {
        public Portrait()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Portrait(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            if (Frame != null)
            {
                Frame.Delete();
            }
        }

        [Persistent]
        public OpenCvSharp.CvRect FaceBounds;

        public CapturedImageObject Frame;
    }
}
