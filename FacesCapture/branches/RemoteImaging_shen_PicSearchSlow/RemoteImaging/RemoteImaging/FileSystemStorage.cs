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
        private static string MinutesFolderFor(DateTime dt)
        {
            return dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2");
        }


        private static string RootFolderForCamera(int cameraID)
        {
            return Path.Combine(Properties.Settings.Default.OutputPath, cameraID.ToString("D2"));
        }


        public static void SaveFrame(Frame frame)
        {
            IplImage ipl = new IplImage(frame.image);
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
            string folderFace = FileSystemStorage.FolderForFaces(frame.cameraID, timeStamp);

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

        private static string FolderForFaces(int camID, DateTime dt)
        {
            string folderForFaces = BuildDestDirectory(RootFolderForCamera(camID),
                                    dt, Properties.Settings.Default.IconDirectoryName);

            if (!Directory.Exists(folderForFaces))
                Directory.CreateDirectory(folderForFaces);

            return folderForFaces;
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
            string temp = MinutesFolderFor(dt);
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
            bigPicFolder = Path.Combine(bigPicFolder, MinutesFolderFor(img.CaptureTime));
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }
    }
}
