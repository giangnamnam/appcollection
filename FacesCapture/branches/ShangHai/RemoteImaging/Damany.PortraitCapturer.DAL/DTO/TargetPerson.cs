using System.Collections.Generic;
using System.IO;
using Damany.Util.IO;
using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{
    public class TargetPerson : CapturedImageObject
    {
        public TargetPerson()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public TargetPerson(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }


        protected override void OnSaved()
        {
            base.OnSaved();

            if (!IsDeleted)
            {
                if (!System.IO.File.Exists(FeatureFilePath))
                {
                    var dir = System.IO.Path.GetDirectoryName(FeatureFilePath);
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    using (var writer = new System.IO.StreamWriter(FeatureFilePath))
                    {
                        foreach (float t in _featurePoints)
                        {
                            writer.WriteLine(t);
                        }

                        writer.Dispose();
                    }
                }
            }
            else
            {
                if (File.Exists(FeatureFilePath))
                {
                    FileHelper.EnsureDelete(FeatureFilePath);
                }
            }
        }

        public string FeatureFilePath;
        public int EyebrowRatio;
        public int EyeBrowShape;

        [NonPersistent]
        [Delayed]
        public float[] FeaturePoints
        {
            get
            {
                if (_featurePoints == null)
                {
                    var feature = new List<float>();
                    using (var reader = new System.IO.StreamReader(FeatureFilePath))
                    {
                        while (true)
                        {
                            var line = reader.ReadLine();
                            if (line == null)
                                break;

                            var f = float.Parse(line);
                            feature.Add(f);
                        }

                        reader.Dispose();
                    }

                    _featurePoints = feature.ToArray();
                }

                return _featurePoints;
            }
            set { _featurePoints = value; }
        }

        private float[] _featurePoints;

    }
}