using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class SuspeciousCarAlermInfo
    {
        public LicensePlateCheckResult ReportedCarInfo { get; set; }
        public LicensePlateInfo CapturedLicenseInfo { get; set; }
    }
}
