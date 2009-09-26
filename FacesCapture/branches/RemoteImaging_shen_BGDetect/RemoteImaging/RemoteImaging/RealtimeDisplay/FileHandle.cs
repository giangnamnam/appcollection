using System.Data;
using System.Configuration;
using System.Web;
using System.Management;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml;
using System;
using System.Timers;
using System.Xml.Linq;
using System.Collections.Generic;

namespace RemoteImaging
{
    public class FileHandle
    {
        public static readonly string xmlPath = "SetVal.xml";

        #region 删除图片和录像
        public void DelVidAndImg()
        {
            string fileUrl = Properties.Settings.Default.OutputPath;//D:\ImageOutPut

            string sday = Properties.Settings.Default.SaveDay;

            DateTime endTime = DateTime.Now.AddDays(-Convert.ToDouble((sday.EndsWith("true") ? sday.Substring(0, sday.Length - 4) : sday.Substring(0, sday.Length - 5)))).ToLocalTime();


            if (Directory.Exists(fileUrl))
            {
                string[] camIdArr = Directory.GetDirectories(fileUrl);
                foreach (string cam in camIdArr)
                {
                    string temp = cam.Substring(cam.Length - 2, 2);//获得Camera编号
                    string[] files = Directory.GetDirectories(cam);

                    foreach (string file in files)  //year  and NORMAL
                    {
                        if (file.Substring(file.Length - 6, 6).Equals("NORMAL"))//删除 视频
                        {
                            string[] videoFile = Directory.GetDirectories(file);
                            foreach (string vidFile in videoFile)
                            {
                                string stime = vidFile.Substring(vidFile.Length - 8, 8);
                                string year = stime.Substring(0, 4);
                                string month = stime.Substring(4, 2);
                                string day = stime.Substring(6, 2);
                                //具体小时
                                DateTime curTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                                DateTime localTime = curTime.ToLocalTime();
                                if (localTime < endTime)
                                    DeleteFolder(vidFile);
                            }

                        }
                        else    //删除 图片
                        {
                            string[] monthFile = Directory.GetDirectories(file);
                            foreach (string mon in monthFile)
                            {
                                string[] dayFile = Directory.GetDirectories(mon);
                                foreach (string day in dayFile)
                                {
                                    string stime = day.Substring(day.Length - 10, 10);//2009\\09\\01
                                    string year = stime.Substring(0, 4);
                                    string month = stime.Substring(5, 2);
                                    string tday = stime.Substring(8, 2);
                                    DateTime curTime = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(tday));
                                    DateTime localTime = curTime.ToLocalTime();
                                    if (localTime < endTime)
                                        DeleteFolder(day);
                                }
                            }

                        }

                    }
                }
            }

        }
        #endregion

        #region 磁盘空间报警
        public void DiskWarn()
        {

            string outPutPath = Properties.Settings.Default.OutputPath;
            string upLoadPool = Properties.Settings.Default.ImageUploadPool;
            string temp = "";// Properties.Settings.Default.DiskWarningSize;
            UInt64 diskSize = Convert.ToUInt64(temp.EndsWith("true") ? temp.Substring(0, temp.Length - 4) : temp.Substring(0, temp.Length - 5));
            if (Directory.Exists(outPutPath) && Directory.Exists(upLoadPool))
            {
                string diskName = string.Format("\"{0}\"", "" + outPutPath.Substring(0, 2) + "");

                UInt64 allCount = DiskSize(diskName, "Size");//"\"D:\"" -- > "D:"  1//disk all size

                UInt64 freeCount = DiskSize(diskName, "FreeSpace");

                if (freeCount < diskSize * 1.5)
                {
                    System.Timers.Timer timeCheck = new System.Timers.Timer();
                    timeCheck.Elapsed += new ElapsedEventHandler(timeCheck_Elapsed);
                    timeCheck.Interval = 360000;
                    timeCheck.Enabled = true;
                }
            }
        }

        private void timeCheck_Elapsed(object source, ElapsedEventArgs args)
        {
            string temp = "";// Properties.Settings.Default.DiskWarningSize;
            UInt64 diskSize = Convert.ToUInt64(temp.EndsWith("true") ? temp.Substring(0, temp.Length - 4) : temp.Substring(0, temp.Length - 5));

            string diskPath = Properties.Settings.Default.OutputPath.Substring(0, 2);
            string diskName = string.Format("\"{0}\"", "" + diskPath + "");

            UInt64 freeCount = DiskSize(diskName, "FreeSpace");

            if (freeCount < diskSize)
            {
                ShowResDialog(0, "磁盘空间不足！！");
            }
        }
        #endregion

        #region 弹出窗口
        /// <summary>
        /// 弹出窗口
        /// </summary>
        /// <param name="picIndex">图片索引</param>
        /// <param name="msg">显示消息</param>
        public void ShowResDialog(int picIndex, string msg)
        {
            AlertSettingRes asr = new AlertSettingRes(msg, picIndex);
            asr.HeightMax = 169;
            asr.WidthMax = 175;
            asr.ShowDialog();
        }
        #endregion

        #region 删除无效视频

        Timer intTime;
        int time = 3600000;
        bool ckChange = false;
        RemoteImaging.Query.ImageSearch imgSearch = new RemoteImaging.Query.ImageSearch();

        /// <summary>
        /// 删除无效视频
        /// </summary>
        /// <param name="times">隔多少时间执行一次操作</param>
        public void DeleteInvalidVideo()
        {
            intTime = new Timer();
            intTime.Elapsed += new ElapsedEventHandler(intTime_Elapsed);
            intTime.Interval = 3000;
            intTime.Start();
            //DateTime dtime = DateTime.Now;
            //DeleteFile(dtime);
        }

        public void intTime_Elapsed(object source, ElapsedEventArgs args)
        {
            DateTime dtime = DateTime.Now;
            if (dtime.Second.ToString().Equals("00") && ckChange == true)
            {
                DeleteFile(dtime);
            }
            if (dtime.Second.ToString().Equals("00") && ckChange == false)
            {
                intTime.Stop();
                intTime.Interval = time;
                intTime.Start();
                ckChange = true;
                DeleteFile(dtime);
            }
        }

        private void DeleteFile(DateTime midTime)
        {
            //当前时间:2009-8-24 10:32:07
            DateTime begTime = midTime.AddHours(-1);//开始时间2009-8-24 9:32:07
            DateTime endTime = midTime.AddMinutes(-2);//结束时间2009-8-24 10:30:07
            ViladityVideo(begTime, endTime);
        }

        //找到无效视频 并且删除
        private void ViladityVideo(DateTime begTime, DateTime endTime)
        {
            string rootFile = Properties.Settings.Default.OutputPath;
            if (Directory.Exists(rootFile))
            {
                string[] cams = Directory.GetDirectories(rootFile);
                foreach (string camfile in cams)
                {
                    string[] resFiles = FileSystemStorage.FindVideos(Convert.ToInt32(camfile.Substring(camfile.Length - 2)), begTime, endTime);//1 获得在时间区域内的video的路径
                    List<string> invalidVideoArr = new List<string>(); //2 找出没video对应的录像  放入 InvalidVideoArr 
                    foreach (string strFile in resFiles)
                    {
                        if (imgSearch.getPicFiles(strFile, camfile.Substring(camfile.Length - 2), false).Length == 0)
                        {
                            invalidVideoArr.Add(strFile);
                        }
                    }
                    if (invalidVideoArr.Count > 0)//3 然后独立一个方法 删除InvalidVideoList里的无效视频
                    {
                        DeleteVideo(invalidVideoArr);
                    }

                }
            }
        }

        //删除无效视频
        private void DeleteVideo(List<string> invVideo)
        {
            foreach (string str in invVideo)
            {
                if (Directory.Exists(str.Substring(0, str.Length - 7)))
                {
                    FileDel(str);
                    FileDel(str.Replace("m4v", "idv"));
                }
            }

        }



        #endregion

        #region 构造函数
        public FileHandle()
        {

        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">物理路径</param>
        public static void FileDel(string Path)
        {
            File.Delete(Path);
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>  
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir, true); //删除已空文件夹                
            }
        }
        #endregion

        //取得disk大小
        public static UInt64 DiskSize(string path, string propertys)
        {
            ManagementObject size = new ManagementObject("win32_logicaldisk.deviceid=" + path);
            size.Get();
            UInt64 b = 1024;
            UInt64 a = (Convert.ToUInt64(size[propertys]) / b) / b;
            return a;
        }

        //获得磁盘剩余空间大小  'D:' || "D:"
        public static UInt64 GetDiskFreeSpaceSize(string disk)
        {
            WqlObjectQuery woq = new WqlObjectQuery("SELECT * FROM Win32_LogicalDisk WHERE DeviceID = " + disk + "");
            ManagementObjectSearcher mos = new ManagementObjectSearcher(woq);
            UInt64 a = 0;
            UInt64 b = 1024;
            foreach (ManagementObject mo in mos.Get())
            {
                //Console.WriteLine("Description: " + mo["Description"]);
                //Console.WriteLine("File system: " + mo["FileSystem"]);
                //Console.WriteLine("Free disk space: " + mo["FreeSpace"]);
                //Console.WriteLine("Size: " + mo["Size"]);
                a += (Convert.ToUInt64(mo["FreeSpace"]) / b) / b;

            }
            return a;
        }


        #region 获得指定文件夹的大小 -- 不用
        public static long GetSize(string path)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            long count = 0;
            foreach (System.IO.FileSystemInfo fi in dir.GetFileSystemInfos())
            {
                if (fi.Attributes.ToString().ToLower().Equals("directory"))
                {
                    count += GetSize(fi.FullName);
                }
                else
                {
                    System.IO.FileInfo finf = new System.IO.FileInfo(fi.FullName);
                    count += finf.Length;
                }
            }
            return count;
        }  //录像一天4320MB 约 4.2GB 一周 30GB

        public static long GetFileSize(string path)
        {
            long fileCount = 0;
            if (Directory.Exists(path))
            {
                string[] filePath = Directory.GetDirectories(path);
                foreach (string file in filePath)
                {
                    fileCount += GetSize(file);
                }
            }
            return fileCount;
        }
        #endregion
    }
    public enum SaveNodeType
    {
        Video, WarnDisk
    }
}