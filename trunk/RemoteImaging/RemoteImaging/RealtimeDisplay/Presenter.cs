﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

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
            string parentOfBigPicFolder = Directory.GetParent(imgToProcess.Path).Parent.FullName.ToString();
            string destFolder = Path.Combine(parentOfBigPicFolder, Properties.Settings.Default.IconDirectoryName);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }
            return destFolder;
        }

        private ImageDetail[] BuildIconImages(string destFolder, string iconFilesString)
        {
            string[] iconFiles = iconFilesString.Split(new char[]{'\t'}, StringSplitOptions.RemoveEmptyEntries);
            ImageDetail[] iconImgs = new ImageDetail[iconFiles.Length];
            for (int i = 0; i < iconImgs.Length; i++)
            {
                string destPathName = Path.Combine(destFolder, iconFiles[i]);
                iconImgs[i] = new ImageDetail(destPathName);
            }

            return iconImgs;
        }

        private ImageDetail[] ExtractIcons(ImageDetail img)
        {
            string destFolder = PrepareDestFolder(img);
            string iconFilesString = extractor.ExtractIcons(img.FullPath, destFolder);

            ImageDetail[] iconImgs = BuildIconImages(destFolder, iconFilesString);
            return iconImgs;
        }

        delegate ImageDetail[] ExtractIconsMethod(ImageDetail img);

        void uploadWatcher_ImagesUploaded(object Sender, ImageUploadEventArgs args)
        {
            ImageClassifier.ClassifyImages(args.Images);

            foreach (ImageDetail img in args.Images)
	        {
                ExtractIconsMethod del = (imgs) => ExtractIcons(imgs);

                IAsyncResult result = del.BeginInvoke(img, null, null);
                while (!result.IsCompleted)
                {
                    System.Windows.Forms.Application.DoEvents();
                    System.Diagnostics.Debug.WriteLine("pump message");
                }
                ImageDetail[] iconImgs = del.EndInvoke(result);

                foreach (ImageDetail icon in iconImgs)
                {
                    if (screen.SelectedCamera != null 
                        && screen.SelectedCamera.ID == icon.FromCamera)
                    {
                        screen.ShowImages(iconImgs);
                    }
                }
                
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
            string bigPicFolder = Directory.GetParent(img.Path).ToString();
            bigPicFolder = Path.Combine(bigPicFolder, Properties.Settings.Default.BigImageDirectoryName);
            string bigPicPathName = Path.Combine(bigPicFolder, bigPicName);
            return bigPicPathName;
        }
        public void SelectedImageChanged()
        {
            ImageDetail img = this.screen.SelectedImage;
            if (img != null && !string.IsNullOrEmpty(img.FullPath))
            {
                string bigPicPathName = BuildFolderPath(img);
                ImageDetail bigImageDetail = new ImageDetail(bigPicPathName);
                this.screen.BigImage = bigImageDetail;

            }
        }

        #endregion
    }
}
