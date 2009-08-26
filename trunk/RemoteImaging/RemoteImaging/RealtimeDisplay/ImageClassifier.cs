﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using RemoteImaging.Core;

namespace RemoteImaging.RealtimeDisplay
{
    public static class ImageClassifier
    {
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
            string temp = dt.Year.ToString("D4") + dt.Month.ToString("D2") + dt.Day.ToString("D2") + dt.Hour.ToString("D2") + dt.Minute.ToString("D2");
            sb.Append(temp);
            sb.Append(Path.AltDirectorySeparatorChar);
            string destPath = Path.Combine(outputPathRoot, sb.ToString());
            return destPath;
        }

        public static void ClassifyImages(ImageDetail[] images)
        {
            //             string outputPathRoot = Properties.Settings.Default.OutputPath;
            //             foreach (ImageDetail image in images)
            //             {
            //                 string destDirectory = BuildDestDirectory(outputPathRoot, Properties.Settings.Default.BigImageDirectoryName, image);
            //                 if (!Directory.Exists(destDirectory))
            //                 {
            //                     Directory.CreateDirectory(destDirectory);
            //                 }
            //                 image.MoveTo(destDirectory);
            //             }
        }
    }
}
