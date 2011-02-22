using System;
using System.IO;
using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{

    public class Video : XPObject
    {
        public string Path;
        public DateTime CaptureTime;
        public int CameraId;

        public Video()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public Video(Session session)
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

            if (File.Exists(Path))
                Util.IO.FileHelper.EnsureDelete(Path);

            //delete .idv file
            var idvFile = Path.Replace(".m4v", ".idv");
            if (File.Exists(idvFile))
                Util.IO.FileHelper.EnsureDelete(idvFile);
        }

    }

}