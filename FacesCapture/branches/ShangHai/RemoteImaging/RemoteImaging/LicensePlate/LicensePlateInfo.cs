using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateInfo
    {
        public static string SubstituteImagePath { get; set; }

        public DateTime CaptureTime { get; set; }
        public string LicensePlateNumber { get; set; }
        public Color LicensePlateColor { get; set; }
        public string LicensePlateColorDescription { get { return LicensePlateColor.GetDescription(); } }

        public int CapturedFrom { get; set; }

        public Guid Guid { get; set; }

        private string _licensePlateImageFileAbsolutePath;
        public string LicensePlateImageFileAbsolutePath
        {
            get { return _licensePlateImageFileAbsolutePath; }
            set
            {
                _licensePlateImageFileAbsolutePath = System.IO.File.Exists(value)
                    ? value : SubstituteImagePath;
            }
        }

        public Rectangle LicensePlateRect { get; set; }

        public Bitmap LicensePlateImage
        {
            get
            {
                try
                {
                    var img = LoadImage();
                    var crop = Crop((Bitmap)img, LicensePlateRect);
                    return crop;
                }
                catch
                {
                    return null;
                }
            }
        }

        public Image LicensePlateImageThumbnail
        {
            get
            {
                try
                {
                    var img = LoadImage();
                    var ratio = (float)img.Height / img.Width;
                    var thumbNail = img.GetThumbnailImage(64, (int)(64 * ratio), null, IntPtr.Zero);
                    img.Dispose();

                    return thumbNail;
                }
                catch { return null; }

            }
        }

        private byte[] _imageData;
        public byte[] ImageData
        {
            get
            {
                if (_imageData == null)
                {
                    _imageData = System.IO.File.ReadAllBytes(LicensePlateImageFileAbsolutePath);
                }
                return _imageData;
            }
            set
            {
                _imageData = value;
            }
        }


        public LicensePlateInfo()
        {
            Guid = Guid.NewGuid();
        }


        public System.Drawing.Image LoadImage()
        {
            var stream = new System.IO.MemoryStream(ImageData);
            return System.Drawing.Image.FromStream(stream);
        }

        public static Bitmap Crop(Bitmap bitmap, Rectangle rect)
        {
            // create new bitmap with desired size and same pixel format
            var croppedBitmap = new Bitmap(rect.Width, rect.Height, bitmap.PixelFormat);

            // create Graphics "wrapper" to draw into our new bitmap
            // "using" guarantees a call to gfx.Dispose()
            using (var gfx = Graphics.FromImage(croppedBitmap))
            {
                // draw the wanted part of the original bitmap into the new bitmap
                gfx.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);
            }

            return croppedBitmap;
        }

    }
}
