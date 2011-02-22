using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Damany.PortraitCapturer.DAL.DTO;

namespace RemoteImaging
{
    public class VideoFileMonitor : IMyStartable
    {
        private readonly string _directoryToMonitor;
        private FileSystemWatcher _watcher;


        public VideoFileMonitor(string directoryToMonitor)
        {
            if (String.IsNullOrEmpty(directoryToMonitor))
                throw new ArgumentException("directoryToMonitor is null or empty.", "directoryToMonitor");
            if (!System.IO.Directory.Exists(directoryToMonitor))
            {
                throw new ArgumentException(directoryToMonitor + " doesn't exist.", "directoryToMonitor");
            }

            _directoryToMonitor = directoryToMonitor;
        }

        public void Start()
        {
            if (_watcher == null)
            {
                _watcher = new FileSystemWatcher(_directoryToMonitor, "*.m4v");
                _watcher.IncludeSubdirectories = true;
                _watcher.Created += _watcher_Created;
                _watcher.EnableRaisingEvents = true;
            }
        }

        void _watcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.Name.EndsWith(".m4v"))
            {
                //除去后缀.m4v
                int cameraId;
                DateTime timeLocal;
                if (Util.TryParsePath(e.FullPath, out cameraId, out timeLocal))
                {
                    using (var session = new DevExpress.Xpo.Session())
                    {
                        var dtoVideo = new Video(session);
                        dtoVideo.Path = e.FullPath;
                        dtoVideo.CaptureTime = timeLocal;
                        dtoVideo.CameraId = cameraId;
                        dtoVideo.Save();
                    }
                }
            }
        }

    }


    
}
