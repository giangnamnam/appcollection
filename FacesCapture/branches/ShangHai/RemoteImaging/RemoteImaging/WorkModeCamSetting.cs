using System;
using DevExpress.Xpo;

namespace RemoteImaging
{

    public class WorkModeCamSetting : XPObject
    {
        private int _irisLevel = 50;
        private int _shutterSpeed = 250 ;
        private bool _digitalGain = false;
        private string _modeName = string.Empty;

        public int IrisLevel
        {
            get { return _irisLevel; }
            set { SetPropertyValue("IrisLevel", ref _irisLevel, value); }
        }


        public int ShutterSpeed
        {
            get { return _shutterSpeed; }
            set { SetPropertyValue("ShutterSpeed", ref _shutterSpeed, value); }
        }

        public bool DigitalGain
        {
            get { return _digitalGain; }
            set { SetPropertyValue("DigitalGain", ref _digitalGain, value); }
        }

        public string ModeName
        {
            get { return _modeName; }
            set { SetPropertyValue("ModeName", ref _modeName, value); }
        }

        public WorkModeCamSetting()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public WorkModeCamSetting(Session session)
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

       
    }

}