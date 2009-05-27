using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemoteImaging.RealtimeDisplay
{
    public static class ImageClassifier
    {
        private static string BuildDestDirectory(string outputPathRoot,
            string subFoldername,
            ImageDetail image)
        {
            StringBuilder sb = new StringBuilder();
            DateTime dt = image.CaptureTime;
            sb.Append(image.FromCamera.ToString("D2"));
            sb.Append(Path.AltDirectorySeparatorChar);
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

            string destPath = Path.Combine(outputPathRoot, sb.ToString());
            return destPath;
        }

        public static void ClassifyImages(ImageDetail[] images)
        {
            string outputPathRoot = Properties.Settings.Default.OutputPath;
            foreach (ImageDetail image in images)
            {
                string destDirectory = BuildDestDirectory(outputPathRoot, Properties.Settings.Default.BigImageDirectoryName, image);
                if (!Directory.Exists(destDirectory))
                {
                    Directory.CreateDirectory(destDirectory);
                }
                image.MoveTo(destDirectory);
            }
        }
    }
}
