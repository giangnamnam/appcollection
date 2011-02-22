using System;

namespace RemoteImaging.LicensePlate
{
    public class ReportedCarInfo
    {
        public DateTime SetupTime { get; set; }
        public ReportedCarMissingType CarMissingType { get; set; }
        public string CarMissingDescription { get { return CarMissingType.GetDescription(); } }
        public string LicenseNumber { get; set; }
        public string Description { get; set; }

        public ReportedCarInfo()
        {
            SetupTime = DateTime.Now;
        }
    }


    public enum ReportedCarMissingType
    {
        Stolen,
        Missing,
    }
}