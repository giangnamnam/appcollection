using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Damany.Util.Extensions;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateUploadMonitor : ILicensePlateEventGenerator
    {
        public delegate LicensePlateUploadMonitor Factory(string pathToMonitor);

        public LicensePlateUploadMonitor(string pathToMonitor,
                                         ILicensePlateEventPublisher plateEventPublisher)
        {
            if (pathToMonitor == null) throw new ArgumentNullException("pathToMonitor");
            if (!System.IO.Directory.Exists(pathToMonitor))
                throw new System.IO.DirectoryNotFoundException(pathToMonitor + " not found");


            _plateEventPublisher = plateEventPublisher;
            Configuration = new LicenseParsingConfig();
            _pathToMonitor = pathToMonitor;
        }

        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = Configuration.ScanInterval * 1000;
                _timer.AutoReset = false;
                _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
                _timer.Enabled = true;
            }

        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ScanDirectory();
        }

        private void ScanDirectory()
        {
            var files = System.IO.Directory.GetFiles(_pathToMonitor, Configuration.Filter);
            foreach (var file in files)
            {
                ProcessFile(file);
            }

            _timer.Enabled = true;
        }


        private void ProcessFile(string filePath)
        {
            try
            {
                var licensePlateInfo = ParsePath(filePath);
                licensePlateInfo.CapturedFrom = CameraId;
                licensePlateInfo.ImageData = File.ReadAllBytes(filePath);
                _plateEventPublisher.PublishLicensePlate(licensePlateInfo);

                File.Delete(filePath);
            }
            catch (FormatException)
            { }
            catch (System.IO.IOException)
            { }

        }

        private LicensePlateInfo ParsePath(string fullPath)
        {
            try
            {
                var fileName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
                var strings = fileName.Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length < Configuration.LeastSectionCount) throw new FormatException("file naming format incorrect");

                var licensePlateInfo = new LicensePlateInfo();

                licensePlateInfo.CaptureTime = new FileInfo(fullPath).CreationTime;
                licensePlateInfo.ImageData = File.ReadAllBytes(fullPath);

                var number = strings[Configuration.LicensePlateSectionIndex];
                licensePlateInfo.LicensePlateNumber = number;

                ParseLicensePlateRect(strings, licensePlateInfo);

                licensePlateInfo.LicensePlateColor = ParseColor(strings);

                return licensePlateInfo;
            }
            catch (System.Exception ex)
            {
                throw new FormatException("file naming format incorrect", ex);
            }
        }

        private Color ParseColor(string[] strings)
        {
            Color color = Color.Empty;
            switch (strings[Configuration.PlateColorIndex])
            {
                case "0":
                    color = Color.Blue;
                    break;
                case "1":
                    color = Color.Black;
                    break;
                case "2":
                    color = Color.Yellow;
                    break;
                case "3":
                    color = Color.White;
                    break;
            }

            return color;
        }

        private void ParseLicensePlateRect(string[] strings, LicensePlateInfo licensePlateInfo)
        {
                if (Configuration.LicensePlatePositionIndex > -1)
                {
                    var positionString = strings[Configuration.LicensePlatePositionIndex];
                    var poisitionStrings = positionString.Split(new[] { Configuration.LicensePlateCoordinateSeparator },
                                                           StringSplitOptions.RemoveEmptyEntries);

                var position = ParseRectangle(poisitionStrings);

                licensePlateInfo.LicensePlateRect = position;
            }
        }

        private static Rectangle ParseRectangle(string[] poisitionStrings)
            {
            var position = new System.Drawing.Point(int.Parse(poisitionStrings[0]), int.Parse(poisitionStrings[1]));
            var w = int.Parse(poisitionStrings[2]);
            var h = int.Parse(poisitionStrings[3]);

            var rectangle = new Rectangle(position.X, position.Y, w, h);

            return rectangle;
            }

        private bool IsInterestedEvent(FileSystemEventArgs e)
        {
            return e.ChangeType == WatcherChangeTypes.Created
                   && File.Exists(e.FullPath);
        }

        public int CameraId { get; set; }

        public LicenseParsingConfig Configuration { get; set; }


        private readonly ILicensePlateEventPublisher _plateEventPublisher;
        private readonly string _pathToMonitor;
        private Timer _timer;


    }
}
