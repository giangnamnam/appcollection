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

        public static string[] FindVideos(int cameraID, DateTime startLocalTime, DateTime endLocalTime)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));

            DateTime startUTC = startLocalTime.ToUniversalTime();
            DateTime endUTC = endLocalTime.ToUniversalTime();
            List<string> files = new List<string>();

            while (startUTC <= endUTC)
            {
                string relativePath = BuildRelativePathForVideoFile(startUTC);
                string path = Path.Combine(rootFolder, relativePath);
                if (File.Exists(path))
                {
                    files.Add(path);
                }

                startUTC = startUTC.AddMinutes(1);

            }

            return files.ToArray();

        }

        private static string BuildRelativePathForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }
    }
}
