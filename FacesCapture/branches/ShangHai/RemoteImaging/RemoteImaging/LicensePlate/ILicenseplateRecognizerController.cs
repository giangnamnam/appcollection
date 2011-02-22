using System;

namespace RemoteImaging.LicensePlate
{
    public interface ILicenseplateRecognizerController
    {
        void Start(Action<Exception> callback);
        void Stop(Action<Exception> callback);
    }
}