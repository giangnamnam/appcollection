using System;

namespace RemoteImaging.LicensePlate
{
    public interface INotifySuspeciousLicensePlate
    {
        void Notify(SuspeciousCarAlermInfo alerm);
    }
}