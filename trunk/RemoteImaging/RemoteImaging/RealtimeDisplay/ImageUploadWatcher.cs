using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RemoteImaging.RealtimeDisplay
{
    public class ImageUploadWatcher
    {
        public event ImageUploadHandler ImagesUploaded;

        private FileSystemWatcher _Watcher;

        public string PathToWatch { get; set; }

        public void Start()
        {
            this._Watcher = new FileSystemWatcher();
            this._Watcher.Path = this.PathToWatch;
            this._Watcher.Filter = "*.jpg";
            this._Watcher.Created += new FileSystemEventHandler(File_Created);
            this._Watcher.EnableRaisingEvents = true;
        }

        IDictionary<int, IList<ImageDetail>> cameraImagesQueue
            = new Dictionary<int, IList<ImageDetail>>();

        private void InitCameraQueue(int cameraID)
        {
            if (!cameraImagesQueue.ContainsKey(cameraID))
            {
                cameraImagesQueue[cameraID] = new List<ImageDetail>();
            }
        }

        private bool ShouldFireEvent(ImageDetail img)
        {
            bool shouldFireEvent = false;
            foreach (ImageDetail item in cameraImagesQueue[img.FromCamera])
            {
                if (item.CaptureTime != img.CaptureTime)
                {
                    shouldFireEvent = true;
                    break;
                }
            }
            return shouldFireEvent;
        }
        private ImageDetail[] MoveImages(int cameraID)
        {
            int count = cameraImagesQueue[cameraID].Count;
            ImageDetail[] images = new ImageDetail[count];
            cameraImagesQueue[cameraID].CopyTo(images, 0);
            cameraImagesQueue[cameraID].Clear();

            return images;
        }

        private void FireEvent(ImageDetail img)
        {
            bool shouldFireEvent = ShouldFireEvent(img);
            if (shouldFireEvent)
            {

                if (this.ImagesUploaded != null)
                {  
                    ImageDetail[] imgs = MoveImages(img.FromCamera);

                    ImageUploadEventArgs args = new ImageUploadEventArgs();
                    args.CameraID = img.FromCamera;
                    args.Images = imgs;

                    this.ImagesUploaded(this, args);

                }

            }
        }

        void File_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                ImageDetail img = new ImageDetail(e.FullPath);

                InitCameraQueue(img.FromCamera);
                FireEvent(img);
                cameraImagesQueue[img.FromCamera].Add(img);
                
            }
        }
    }
}
