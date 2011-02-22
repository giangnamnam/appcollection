using System;
using System.Collections.Generic;
using System.IO;
using Damany.PortraitCapturer.DAL.DTO;
using DevExpress.Xpo;

namespace RemoteImaging
{
    class VideoFileWalker : IMyStartable
    {
        private readonly string _directoryToWalk;

        public VideoFileWalker(string directoryToWalk)
        {
            if (String.IsNullOrEmpty(directoryToWalk))
                throw new ArgumentException("directoryToWalk is null or empty.", "directoryToWalk");
            if (!Directory.Exists(directoryToWalk))
                throw new ArgumentException(directoryToWalk + "doesn't exist.", "directoryToWalk");

            _directoryToWalk = directoryToWalk;
        }

        public void Start()
        {
            System.Threading.Tasks.Task.Factory.StartNew(DoWalk);
        }

        private void DoWalk()
        {
            try
            {
                using (var uow = new UnitOfWork())
                {
                    var filesToBeAdded = new List<string>();
                    foreach (var m4VFile in Directory.EnumerateFiles(
                        _directoryToWalk, "*.m4v", SearchOption.AllDirectories))
                    {
                        var c = DevExpress.Data.Filtering.CriteriaOperator.Parse("Path = ?", m4VFile);
                        var v = uow.FindObject(typeof(Video), c);
                        if (v == null)
                        {
                            filesToBeAdded.Add(m4VFile);
                        }
                    }

                    foreach (var newFilePath in filesToBeAdded)
                    {
                        int cameraId;
                        DateTime time;
                        if (Util.TryParsePath(newFilePath, out cameraId, out time))
                        {
                            var nv = new Video(uow);
                            nv.Path = newFilePath;
                            nv.CaptureTime = time;
                            nv.CameraId = cameraId;
                            nv.Save();
                        }
                    }
                    uow.CommitChanges();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}