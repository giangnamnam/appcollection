using System;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateCheckService
    {
        void Check(string licensePlateNumber, Action<LicensePlateCheckResult> callback);
    }
}