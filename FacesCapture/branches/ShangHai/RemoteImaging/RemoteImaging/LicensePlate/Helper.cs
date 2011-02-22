using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public static class Helper
    {
        public static void UpdateSuspectCarAlerm(Controls.SuspectCarControl suspectCarControl, SuspeciousCarAlermInfo value)
        {
            suspectCarControl.carImage.Image = value.CapturedLicenseInfo.LoadImage();
            suspectCarControl.licenseNumber.EditValue = value.CapturedLicenseInfo.LicensePlateNumber;
            suspectCarControl.captureTime.EditValue = value.CapturedLicenseInfo.CaptureTime;
            suspectCarControl.reportedMissingType.EditValue = value.ReportedCarInfo.CarInfo.CarMissingType.GetDescription();
            suspectCarControl.addTime.EditValue = value.ReportedCarInfo.CarInfo.SetupTime;

            suspectCarControl.gdViewer1.DisplayFromFile(value.CapturedLicenseInfo.LicensePlateImageFileAbsolutePath);

            var rect = value.CapturedLicenseInfo.LicensePlateRect;
            suspectCarControl.gdViewer1.ZoomArea(rect.X, rect.Y, rect.Width, rect.Height);

            suspectCarControl.gdViewer1.Select();
        }
    }
}
