using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteImaging.Core;
using System.IO;
using ImageProcess;
using OpenCvSharp;

namespace RemoteImaging
{
    public static class FileSystemStorage
    {
        private static string MinutesFolderNameFor(DateTime dt)
        {
            return dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2");
        }


        private static string RootFolderForCamera(int cameraID)
        {
            return Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));
        }


        public static int GetFreeDiskSpaceMB(string drive)
        {
            DriveInfo driveInfo = new DriveInfo(drive);
            long FreeSpace = driveInfo.AvailableFreeSpace;

            FreeSpace /= 1024 * 1024;

            return (int) FreeSpace;
        }


        public static void SaveFrame(Frame frame)
        {
            IplImage ipl = new IplImage(frame.IplPtr);
            ipl.IsEnabledDispose = false;

            string path = frame.GetFileName();
            DateTime dt = DateTime.FromBinary(frame.timeStamp);

            string root = Path.Combine(Properties.Settings.Default.OutputPath,
                      frame.cameraID.ToString("d2"));

            string folder = FileSystemStorage.BuildDestDirectory(root, dt, Properties.Settings.Default.BigImageDirectoryName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            path = Path.Combine(folder, path);
            ipl.SaveImage(path);
        }


        public static string GetFacePath(Frame frame, DateTime timeStamp, int sequence)
        {
            string folderFace = FileSystemStorage.GetOrCreateFolderForFacesAt(frame.cameraID, timeStamp);

            string faceFileName = FileSystemStorage.GetFaceFileName(frame.GetFileName(), sequence);

            string facePath = Path.Combine(folderFace, faceFileName);
            return facePath;
        }

        private static string GetFaceFileName(string bigImagePath, int indexOfFace)
        {
            int idx = bigImagePath.IndexOf('.');
            string faceFileName = bigImagePath.Insert(idx, "-" + indexOfFace.ToString("d4"));
            return faceFileName;
        }

        private static string FolderPathForFaceAt(int camID, DateTime dt)
        {
            string folderForFaces = BuildDestDirectory(RootFolderForCamera(camID),
                                                dt, Properties.Settings.Default.IconDirectoryName);
            return folderForFaces;
        }

        private static string GetOrCreateFolderForFacesAt(int camID, DateTime dt)
        {
            string folderForFaces = FolderPathForFaceAt(camID, dt);

            if (!Directory.Exists(folderForFaces))
                Directory.CreateDirectory(folderForFaces);

            return folderForFaces;
        }


        public static bool FacesCapturedAt(int camID, DateTime time)
        {
            string path = FolderPathForFaceAt(camID, time);

            return Directory.Exists(path);
        }


        public static string BuildDestDirectory(string outputPathRoot,
           DateTime dt,
           string subFoldername
           )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(dt.Year.ToString("D4"));
            sb.Append(Path.AltDirectorySeparatorChar);
            sb.Append(dt.Month.ToString("D2"));
            sb.Append(Path.AltDirectorySeparatorChar);
            sb.Append(dt.Day.ToString("D2"));
            sb.Append(Path.AltDirectorySeparatorChar);
            if (!string.IsNullOrEmpty(subFoldername))
            {
                sb.Append(subFoldername);
                sb.Append(Path.AltDirectorySeparatorChar);
            }
            string temp = MinutesFolderNameFor(dt);
            sb.Append(temp);
            sb.Append(Path.AltDirectorySeparatorChar);
            string destPath = Path.Combine(outputPathRoot, sb.ToString());
            return destPath;
        }

        public static string BigImgPathFor(ImageDetail img)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(img.Name);
            int idx = nameWithoutExtension.LastIndexOf('-');
            nameWithoutExtension = nameWithoutExtension.Remove(idx);

            string bigPicName = nameWithoutExtension + Path.GetExtension(img.Name);
            string bigPicFolder = Directory.GetParent(img.ContainedBy).ToString();

            bigPicFolder = bigPicFolder.Replace(Properties.Settings.Default.IconDirectoryName, "");

            bigPicFolder = Path.Combine(bigPicFolder, Properties.Settings.Default.BigImageDirectoryName);
            bigPicFolder = Path.Combine(bigPicFolder, MinutesFolderNameFor(img.CaptureTime));
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }


        public static string VideoFilePathNameAt(DateTime time, int camID)
        {
            string rootFolder = Path.Combine(Properties.Settings.Default.OutputPath,
                            camID.ToString("D2"));

            DateTime utcTime = time.ToUniversalTime();
            string relPath = RelativePathNameForVideoFile(utcTime);

            string videoFilePath = Path.Combine(rootFolder, relPath);
            return videoFilePath;
        }
        public static string[] FindVideos(ImageDetail img)
        {
            string videoFilePath = VideoFilePathNameAt(img.CaptureTime, img.FromCamera);
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
                string relativePath = RelativePathNameForVideoFile(startUTC);
                string path = Path.Combine(rootFolder, relativePath);
                if (File.Exists(path))
                {
                    files.Add(path);
                }

                startUTC = startUTC.AddMinutes(1);

            }

            return files.ToArray();

        }

        private static string RelativePathNameForVideoFile(DateTime utcTime)
        {
            string relativePath = string.Format(@"NORMAL\{0:D4}{1:D2}{2:D2}\{3:D2}\{4:D2}.m4v",
                            utcTime.Year, utcTime.Month, utcTime.Day, utcTime.Hour, utcTime.Minute);
            return relativePath;
        }
    }
}
