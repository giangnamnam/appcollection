using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Damany.Imaging.Common;
using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class Frame : CapturedImageObject
    {
        public Frame()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Frame(Session session)
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


        [Association("Frame-Portraits")]
        public XPCollection<Portrait> Portraits
        {
            get { return GetCollection<Portrait>("Portraits"); }
        }
    }
}
