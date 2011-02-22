using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DevExpress.Xpo;

namespace Damany.PortraitCapturer.DAL.DTO
{

    public class CapturedImageObject : XPObject
    {
        public CapturedImageObject()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public CapturedImageObject(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            CaptureTime = DateTime.Now;
        }


        public DateTime CaptureTime;
        public string ImagePath;
        public int ImageSourceId;

        [NonPersistent]
        [Delayed]
        public Image ImageCopy
        {
            get
            {
                Image img;
                TryLoadImageCopy(out img);

                return img;
            }
        }


        private Image _thumbnail;

        [NonPersistent]
        [Delayed]
        public Image Thumbnail
        {
            get
            {
                if (!ImageFileExists) return null;

                if (_thumbnail == null)
                {
                    if (File.Exists(ThumbnailPath))
                    {
                        _thumbnail = AForge.Imaging.Image.FromFile(ThumbnailPath);
                    }
                    else
                    {
                        Image img;
                        if (TryLoadImageCopy(out img))
                        {
                            if (img.Width <= 500)
                            {
                                _thumbnail = img;
                            }
                            else
                            {
                                var af = (float)img.Height / img.Width;
                                _thumbnail = img.GetThumbnailImage(200, (int)(200 * af), null, IntPtr.Zero);
                                _thumbnail.Save(ThumbnailPath, ImageFormat.Jpeg);
                            }

                        }
                    }
                }

                return _thumbnail;

            }
        }

        [NonPersistent]
        public bool ImageFileExists
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return false;
                }
                return File.Exists(ImagePath);
            }
        }

        public bool TryLoadImageCopy(out Image image)
        {
            image = null;

            if (ImageFileExists)
            {
                try
                {
                    image = AForge.Imaging.Image.FromFile(ImagePath);
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();

            if (string.IsNullOrEmpty(ImagePath))
                return;

            DeleteFile(ImagePath);
            DeleteFile(ThumbnailPath);
        }

        private string ThumbnailPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                    return null;

                return ImagePath + ".thu";
            }
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                { }
            }
        }
    }

}