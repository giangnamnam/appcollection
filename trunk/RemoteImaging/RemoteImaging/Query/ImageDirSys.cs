using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.Query
{
    public class ImageDirSys
    {
        public const string BeginDir = "D:\\pic\\";
        public const string BigIconPath = "BigIcon";
        public const string IconPath = "Icon";

        public ImageDirSys(string camera,string year,string month,string day,string hour,string minute,string second )
        {
            this.CameraID = camera;
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
        }

        public string CameraID
        {
            get;
            set;
        }
        public string Year
        {
            get;
            set;
        }
        public string Month
        {
            get;
            set;
        }
        public string Day
        {
            get; 
            set;
        }
        public string Hour
        {
            get;
            set;
        }
        public string Minute
        {
            get;
            set;
        }
        public string Second
        {
            get;
            set;
        }

    }
}
