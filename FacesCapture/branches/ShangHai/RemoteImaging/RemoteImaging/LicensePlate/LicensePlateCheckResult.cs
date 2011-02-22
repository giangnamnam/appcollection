namespace RemoteImaging.LicensePlate
{
    public class LicensePlateCheckResult
    {
        public string LicensePlateNumber { get; set; }
        public bool IsSuspecious { get; set; }
        public ReportedCarInfo CarInfo { get; set; }
    }
}