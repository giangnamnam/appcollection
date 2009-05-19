using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using ImageProcess;

namespace RemoteImaging.RealtimeDisplay
{
    public class Presenter : IImageScreenObserver
    {
        IImageScreen screen;
        ImageUploadWatcher uploadWatcher;

        IIconExtractor extractor;

        /// <summary>
        /// Initializes a new instance of the Presenter class.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="uploadWatcher"></param>
        public Presenter(IImageScreen screen,
            ImageUploadWatcher uploadWatcher,
            IIconExtractor extractor)
        {
            this.screen = screen;
            this.uploadWatcher = uploadWatcher;
            this.extractor = extractor;

            this.screen.Observer = this;
            this.uploadWatcher.ImagesUploaded += uploadWatcher_ImagesUploaded;
        }


        public void Start()
        {
            this.uploadWatcher.Start();
        }

        private string PrepareDestFolder(ImageDetail imgToProcess)
        {
            string parentOfBigPicFolder = Directory.GetParent(imgToProcess.ContainedBy).FullName;
            string destFolder = Path.Combine(parentOfBigPicFolder, Properties.Settings.Default.IconDirectoryName);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            return destFolder;
        }

        private ImageDetail[] BuildIconImages(string destFolder, ImageDetail[] bigImgs, string iconFilesString)
        {
            string[] iconFiles = iconFilesString.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            IDictionary<int, IList<string>> iconGroup = new Dictionary<int, IList<string>>();

            //分组
            for (int i = 0; i < iconFiles.Length; i += 2)
            {
                int idxOfBigPic = int.Parse(iconFiles[i + 1]);
                if (!iconGroup.ContainsKey(idxOfBigPic))
                {
                    iconGroup.Add(idxOfBigPic, new List<string>());
                }

                iconGroup[idxOfBigPic].Add(iconFiles[i]);
            }

            IList<ImageDetail> returnImgs = new List<ImageDetail>();
            foreach (var iconSubGroup in iconGroup)
            {
                for (int i = 0; i < iconSubGroup.Value.Count; i++)
                {
                    int idx = iconSubGroup.Key;
                    string bigPicPath = bigImgs[idx].Path;
                    string bigpicExtension = Path.GetExtension(bigPicPath);
                    string bigPicNameWithoutExtention = Path.GetFileNameWithoutExtension(bigPicPath);
                    string iconFileName = string.Format("{0}-{1:d4}{2}", bigPicNameWithoutExtention, i, bigpicExtension);
                    string iconFilePath = Path.Combine(destFolder, iconFileName);

                    //rename file
                    string origIconFilePath = Path.Combine(destFolder, iconSubGroup.Value[i]);
                    File.Move(origIconFilePath, iconFilePath);
                    returnImgs.Add(ImageDetail.FromPath(iconFilePath));
                }
            }

            ImageDetail[] returnImgsArray = new ImageDetail[returnImgs.Count];
            returnImgs.CopyTo(returnImgsArray, 0);

            return returnImgsArray;
        }

        private ImageDetail[] ExtractIcons(ImageDetail[] imgs)
        {
            string destFolder = PrepareDestFolder(imgs[0]);
            extractor.SetOutputDir(destFolder);
            Array.ForEach<ImageDetail>(imgs, img => extractor.AddInImage(img.Path));

            string iconFilesString = extractor.SelectBestImage();

            ImageDetail[] iconImgs = BuildIconImages(destFolder, imgs, iconFilesString);

            System.Diagnostics.Debug.WriteLine("icon imgs:");
            Array.ForEach(iconImgs, img => System.Diagnostics.Debug.WriteLine(img.Path));

            return iconImgs;
        }


        void uploadWatcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            ImageDetail[] imgsToProcess = args.Images;

            ImageClassifier.ClassifyImages(imgsToProcess);

            System.Func<ImageDetail[], ImageDetail[]> func = (imgs) => ExtractIcons(imgs);

            IAsyncResult result = func.BeginInvoke(imgsToProcess, null, null);
            while (!result.IsCompleted)
            {
                //System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
                System.Diagnostics.Debug.WriteLine(DateTime.Now.ToString());
            }

            ImageDetail[] iconImgs = func.EndInvoke(result);

            if (iconImgs.Length == 0)
            {
                return;
            }

            if (screen.SelectedCamera != null
                && screen.SelectedCamera.ID == iconImgs[0].FromCamera)
            {
                screen.ShowImages(iconImgs);
            }


        }

        #region IImageScreenObserver Members

        public void SelectedCameraChanged()
        {
            throw new NotImplementedException();
        }

        private static string BuildFolderPath(ImageDetail img)
        {
            string nameWithoutExtension = Path.GetFileNameWithoutExtension(img.Name);
            int idx = nameWithoutExtension.LastIndexOf('-');
            nameWithoutExtension = nameWithoutExtension.Remove(idx);

            string bigPicName = nameWithoutExtension + Path.GetExtension(img.Name);
            string bigPicFolder = Directory.GetParent(img.ContainedBy).ToString();
            bigPicFolder = Path.Combine(bigPicFolder, Properties.Settings.Default.BigImageDirectoryName);
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }
        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.Path))
            {
                string bigPicPathName = BuildFolderPath(img);
                ImageDetail bigImageDetail = ImageDetail.FromPath(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }
}
