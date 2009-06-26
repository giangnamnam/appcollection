using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemoteImaging
{
    using Core;

    public static class VideoSearch
    {
        public static string[] FindVideos(string rootFolder, ImageDetail img)
        {
            DateTime utcTime = img.CaptureTime.ToUniversalTime();
            string relPath = BuildRelativePathForVideoFile(utcTime);

            string videoFilePath = Path.Combine(rootFolder, relPath);
            if (File.Exists(videoFilePath))
            {
                string[] videos = new string[] { videoFilePath };
                return videos;
            }
            else
            {
                return new string[0];
            }
        }

        private static string BuildRelativePathForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }


    }
}
