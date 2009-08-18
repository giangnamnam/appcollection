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

        #region 根据录像获得图片
        public static string[] getPicFiles(string path,string comId)
        {
            List<string> filesArr = new List<string>();
            DateTime dTime = getDateTimeStr(path);
            //string imgPath = "";
            string imgPath = Properties.Settings.Default.OutputPath + "\\" +
                int.Parse(comId).ToString("D2") + "\\" +
                dTime.Year + "\\" + dTime.Month.ToString("D2") + "\\" +
                dTime.Day.ToString("D2") + "\\" +
                Properties.Settings.Default.IconDirectoryName + "\\";
            if (Directory.Exists(imgPath))
            {
                string[] iconFiles = Directory.GetDirectories(imgPath);
                foreach (string fileStr in iconFiles)
                {
                    //D:\ImageOutput\02\2009\08\07\Icon\200908120809
                    string temp = fileStr.Substring(fileStr.Length - 12, 12);
                    int year = Convert.ToInt32(temp.Substring(0, 4));
                    int month = Convert.ToInt32(temp.Substring(4, 2));
                    int day = Convert.ToInt32(temp.Substring(6, 2));
                    int hour = Convert.ToInt32(temp.Substring(8, 2));
                    int minute = Convert.ToInt32(temp.Substring(10, 2));

                    DateTime tempTime = new DateTime(year, month, day, hour, minute, 0);
                    if (tempTime == dTime)
                    {
                        string[] files = Directory.GetFiles(fileStr);
                        foreach (string file in files)
                        {
                            string strExtName = Path.GetExtension(file);
                            if (strExtName.Equals(".jpg"))
                            {
                                filesArr.Add(file);
                            }
                        }
                    }
                }
            }

            string[] fileCollections = new string[filesArr.Count];
            for (int i = 0; i < filesArr.Count; i++)
            {
                fileCollections[i] = filesArr[i].ToString();
            }
            return fileCollections;
        }

        public static DateTime getDateTimeStr(string temp)
        {
            Int32 index = temp.IndexOf("NORMAL") + 7;
            string str = temp.Substring(index, 14);//20090629\06\00
            DateTime time = new DateTime(Convert.ToInt32(str.Substring(0, 4)), Convert.ToInt32(str.Substring(4, 2)),
                Convert.ToInt32(str.Substring(6, 2)), Convert.ToInt32(str.Substring(9, 2)), Convert.ToInt32(str.Substring(12, 2)), 0);
            time = time.ToLocalTime(); //time = time.AddHours(8);
            return time;
        }
        #endregion
    }
}
