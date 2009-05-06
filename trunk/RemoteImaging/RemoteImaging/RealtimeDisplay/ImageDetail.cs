using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.RealtimeDisplay
{
    public class ImageDetail
    {
        public ImageDetail(string pathName)
        {
            this.FullPath = pathName;
            this.ParseName();
        }


        public DateTime CaptureTime { get; set; }

        public int FromCamera { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string FullPath { get; set; }

        private void ParseName()
        {
            this.Name = System.IO.Path.GetFileName(this.FullPath);
            this.Path = System.IO.Path.GetDirectoryName(this.FullPath);

            this.FromCamera = int.Parse(this.Name.Substring(0, 2));

            int year = int.Parse(this.Name.Substring(3, 2)) + 2000;
            int month = int.Parse(this.Name.Substring(5, 2));
            int day = int.Parse(this.Name.Substring(7, 2));
            int hour = int.Parse(this.Name.Substring(9, 2));
            int min = int.Parse(this.Name.Substring(11, 2));
            int sec = int.Parse(this.Name.Substring(13, 2));
            DateTime dt = new DateTime(year, month, day, hour, min, sec, 0);
            this.CaptureTime = dt;
        }
    }
}
