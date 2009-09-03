using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Timers;
using System.Text.RegularExpressions;

namespace RemoteImaging.Query
{
    public class ImageSearch
    {
        public string[] SearchImages(ImageDirSys startDir,ImageDirSys endDir,ImageDirSys.SearchType searchType)
        {
            int startYear = int.Parse(startDir.Year);
            int startMonth = int.Parse(startDir.Month);
            int startDay = int.Parse(startDir.Day);
            int startHour = int.Parse(startDir.Hour);
            int startMinute = int.Parse(startDir.Minute);
            int startSecond = int.Parse(startDir.Second);
            int endYear = int.Parse(endDir.Year);
            int endMonth = int.Parse(endDir.Month);
            int endDay = int.Parse(endDir.Day);
            int endHour = int.Parse(endDir.Hour);
            int endMinute = int.Parse(endDir.Minute);
            int endSecond = int.Parse(endDir.Second);

            string beginDir = ImageDirSys.BeginDir;
            string subSearchPath = "";
            if (searchType == ImageDirSys.SearchType.PicType)
            {
                subSearchPath = Query.ImageDirSys.IconPath;
            }
            else if (searchType == ImageDirSys.SearchType.VideoType)
            {
                subSearchPath = Query.ImageDirSys.VideoPath;
            }

            ArrayList fileList = new ArrayList();

            
            if (startYear == endYear &&
                startMonth == endMonth &&
                startDay == endDay)//int the same day
            {
                string searchPath = beginDir + "\\" +
                                    startDir.CameraID + "\\" +
                                    startDir.Year + "\\" +
                                    startDir.Month + "\\" +
                                    startDir.Day + "\\" + subSearchPath + "\\";
                if (Directory.Exists(searchPath))
                {
                    string[] files = Directory.GetFiles(searchPath);
                    foreach(string file in files)
                    {                  
                        DateTime dateTime1 = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
                        DateTime dateIime2 = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

                        string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                        if (IsValidImageFile(fileName))
                        {
                            int hour = int.Parse(fileName.Substring(9, 2));
                            int minute = int.Parse(fileName.Substring(11, 2));
                            int second = int.Parse(fileName.Substring(13, 2));

                            if (hour >= 0 && hour <= 23 &&
                                minute >= 0 && minute <= 59 &&
                                second >= 0 && second <= 59)
                            {

                                DateTime dateTime = new DateTime(startYear, startMonth, startDay, hour, minute, second);
                                if ((dateTime1 <= dateTime) && (dateTime <= dateIime2))
                                {
                                    fileList.Add(file);
                                }
                            }
                        }
                    }
                }                
            }
            else if (startYear == endYear &&
                     startMonth == endMonth &&
                     startDay < endDay)//accross the day
            {
                for (int i = startDay; i <= endDay; i++)
                {
                    string searchPath = beginDir + "\\" +
                                        startDir.CameraID + "\\" +
                                        startDir.Year + "\\" +
                                        startDir.Month + "\\" +
                                        i.ToString("D2") + "\\" + subSearchPath + "\\";

                    if (Directory.Exists(searchPath))
                    {
                        string[] files = Directory.GetFiles(searchPath);
                        foreach (string file in files)
                        {
                            DateTime dateTime1 = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
                            DateTime dateIime2 = new DateTime(endYear,endMonth,endDay,endHour,endMinute,endSecond);
                            
                            string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                            if (IsValidImageFile(fileName))
                            {
                                int hour = int.Parse(fileName.Substring(9, 2));
                                int minute = int.Parse(fileName.Substring(11, 2));
                                int second = int.Parse(fileName.Substring(13, 2));

                                if (hour >= 0 && hour <= 23 &&
                                   minute >= 0 && minute <= 59 &&
                                   second >= 0 && second <= 59)
                                {
                                    DateTime dateTime = new DateTime(startYear, startMonth, i, hour, minute, second);
                                    if ((dateTime1 <= dateTime) && (dateTime <= dateIime2))
                                    {
                                        fileList.Add(file);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (startYear == endYear &&
                     startMonth < endMonth)//accross the month
            {
                for (int i = startMonth; i <= endMonth; i++)
                {
                    int tmpDay1, tmpDay2;
                    if (i == startMonth)
                        tmpDay1 = startDay;
                    else
                        tmpDay1 = 1;
                    if (i == endMonth)
                        tmpDay2 = endDay;
                    else
                        tmpDay2 = 31;

                    for (int j = tmpDay1; j <= tmpDay2; j++)
                    {
                        string searchPath = beginDir + "\\" +
                                            startDir.CameraID + "\\" +
                                            startDir.Year + "\\" +
                                            i.ToString("D2") + "\\" +
                                            j.ToString("D2") + "\\" + subSearchPath + "\\";
                        if (Directory.Exists(searchPath))
                        {
                            string[] files = Directory.GetFiles(searchPath);
                            foreach (string file in files)
                            {
                                DateTime dateTime1 = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
                                DateTime dateIime2 = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

                                string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                                if (IsValidImageFile(fileName))
                                {
                                    int hour = int.Parse(fileName.Substring(9, 2));
                                    int minute = int.Parse(fileName.Substring(11, 2));
                                    int second = int.Parse(fileName.Substring(13, 2));

                                    if (hour >= 0 && hour <= 23 &&
                                        minute >= 0 && minute <= 59 &&
                                        second >= 0 && second <= 59)
                                    {

                                        DateTime dateTime = new DateTime(startYear, i, j, hour, minute, second);
                                        if ((dateTime1 <= dateTime) && (dateTime <= dateIime2))
                                        {
                                            fileList.Add(file);
                                        }

                                    }
                                }
                            }
                        }

                    }
                }
            }
            else if (startYear < endYear)//accross year
            {
                for(int i = startYear; i <= endYear; i++)
                {
                    int tmpMonth1,tmpMonth2;
                    if(i == startYear)
                        tmpMonth1 = startMonth;
                    else  
                        tmpMonth1 = 1;
                    if(i == endYear)
                        tmpMonth2 = endMonth;
                    else
                        tmpMonth2 = 12;


                    for (int j = tmpMonth1; j <= tmpMonth2; j++)
                    {
                        int tmpDay1,tmpDay2;
                        if(i == startYear)
                            tmpDay1 = startDay;
                        else
                            tmpDay1 = 1;
                        if(i == endYear)
                            tmpDay2 = endDay;
                        else
                            tmpDay2 = 31;

                        for(int k = tmpDay1; k <= tmpDay2; k++)
                        {
                            string searchPath = beginDir + "\\" +
                                                startDir.CameraID + "\\" +
                                                i.ToString() + "\\" +
                                                j.ToString("D2") + "\\" +
                                                k.ToString("D2") + "\\" + subSearchPath + "\\";
                            if(Directory.Exists(searchPath))
                            {
                                string[] files = Directory.GetFiles(searchPath);
                                
                                foreach (string file in files)
                                {
                                    DateTime dateTime1 = new DateTime(startYear, startMonth, startDay, startHour, startMinute, startSecond);
                                    DateTime dateIime2 = new DateTime(endYear, endMonth, endDay, endHour, endMinute, endSecond);

                                    string fileName = System.IO.Path.GetFileNameWithoutExtension(file);
                                    if (IsValidImageFile(fileName))
                                    {
                                        int hour = int.Parse(fileName.Substring(9, 2));
                                        int minute = int.Parse(fileName.Substring(11, 2));
                                        int second = int.Parse(fileName.Substring(13, 2));

                                        if (hour >= 0 && hour <= 23 &&
                                            minute >= 0 && minute <= 59 &&
                                            second >= 0 && second <= 59)
                                        {

                                            DateTime dateTime = new DateTime(i, j, k, hour, minute, second);
                                            if ((dateTime1 <= dateTime) && (dateTime <= dateIime2))
                                            {
                                                fileList.Add(file);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                      }
                    }
                }
            

            if (fileList.Count > 0)
            {
                string[] files = new string[fileList.Count];
                int i = 0;
                foreach (string str in fileList)
                {
                    files[i++] = str;
                }
                return files;
            }

            return null;
        }


        public string[] SelectedBestImageChanged(string selectedImageName)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(selectedImageName);
            if (!IsValidImageFile(fileName))
            {
                return null;
            }

            string searchPath = Query.ImageDirSys.BeginDir + "\\" +
                                fileName.Substring(0, 2) + "\\" +
                                (2000 + int.Parse(fileName.Substring(3, 2))).ToString() + "\\" +
                                fileName.Substring(5, 2) + "\\" +
                                fileName.Substring(7, 2) + "\\" + RemoteImaging.Query.ImageDirSys.BigIconPath + "\\";

            ArrayList fileList = new ArrayList();
            if (Directory.Exists(searchPath))
            {
                string[] files = Directory.GetFiles(searchPath);

                string subFileName1 = fileName.Substring(0, fileName.IndexOf('-'));
                foreach (string file in files)
                {
                    string fileName1 = System.IO.Path.GetFileNameWithoutExtension(file);
                    if (IsValidImageFile(fileName1))
                    {
                        string subFileName2 = fileName1.Substring(0, fileName1.IndexOf('-'));
                        if (subFileName1 == subFileName2)
                        {
                            fileList.Add(file);
                        }
                    }
                }
            }
            if (fileList.Count > 0)
            {
                string[] files = new string[fileList.Count];
                int i = 0;
                foreach (string str in fileList)
                {
                    files[i++] = str;
                }
                return files;
            }

            return null;
        }

        private bool IsValidImageFile(string fileName)
        {
            Regex fileNameRegex = new Regex("\\d{2}_\\d{12}-\\d{4}");
            Match m =fileNameRegex.Match(fileName);
            if(m.Success)
            {
                return true;
            
            }
 
            return false;
        }
    }
}
