using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace RemoteImaging.Core
{
    public class Video : INotifyPropertyChanged
    {
        public string Path { get; set; }
        public DateTime CapturedAt { get; set; }
        private bool _hasFaceCaptured;
        public bool HasFaceCaptured
        {
            get { return _hasFaceCaptured; }
            set
            {
                if (_hasFaceCaptured == value)
                    return;
                _hasFaceCaptured = value;
                FirePropertyChanged("HasFaceCaptured");
            }
        }

        public bool HasMotionDetected { get; set; }
        public bool HasLicenseplateCaptured { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;

        public void FirePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
