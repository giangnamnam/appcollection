using System;
using Damany.Util;
using OpenCvSharp;

namespace Damany.Imaging.Common
{
    public class PersonOfInterest : IEquatable<PersonOfInterest>
    {
        private OpenCvSharp.IplImage _ipl;
        private readonly IplImage _alignedImage;

        public PersonOfInterest()
        {
            this.Guid = System.Guid.NewGuid();
        }

        public PersonOfInterest(IplImage iplImage, IplImage alignedImage)
            : this()
        {
            _ipl = iplImage;
            _alignedImage = alignedImage;
        }

        public System.Drawing.Image GetImage()
        {
            return System.Drawing.Image.FromFile(ImageFilePath);
        }

        public IplImage GetIpl()
        {
            if (_ipl == null)
            {
                _ipl = IplImage.FromFile(ImageFilePath);
                _ipl.ROI = FaceRect;
            }
            return _ipl;
        }

        public IplImage GetAlignedImage()
        {
            return _alignedImage;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string SN { get; set; }
        public Guid Guid { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public string ImageFilePath { get; set; }
        public CvRect FaceRect { get; set; }


        public bool Equals(PersonOfInterest other)
        {
            return this.Guid.Equals(other.Guid);
        }
    }
}
