using System;
using System.Collections.Generic;

namespace RemoteImaging.LicensePlate
{
    class SuspectLicensePlateChecker : ILicensePlateObserver
    {
        private readonly ILicensePlateCheckService _licensePlateCheckService;
        private readonly ILicensePlateEventPublisher _licensePlateEventPublisher;
        private readonly IEnumerable<INotifySuspeciousLicensePlate> _suspeciousLicensePlateObserver;

        public SuspectLicensePlateChecker(ILicensePlateCheckService licensePlateCheckService,
                                          ILicensePlateEventPublisher licensePlateEventPublisher,
                                          IEnumerable<INotifySuspeciousLicensePlate> suspeciousLicensePlateObserver)
        {
            if (licensePlateCheckService == null) 
                throw new ArgumentNullException("licensePlateCheckService");
            if (licensePlateEventPublisher == null) throw new ArgumentNullException("licensePlateEventPublisher");
            if (suspeciousLicensePlateObserver == null)
                throw new ArgumentNullException("suspeciousLicensePlateObserver");

            _licensePlateCheckService = licensePlateCheckService;
            _suspeciousLicensePlateObserver = suspeciousLicensePlateObserver;

            _licensePlateEventPublisher = licensePlateEventPublisher;
            _licensePlateEventPublisher.RegisterForLicensePlateEvent(this);
        }


        public void LicensePlateCaptured(LicensePlateInfo licensePlateInfo)
        {
            _licensePlateCheckService.Check(licensePlateInfo.LicensePlateNumber,
                result=>
                {
                    if (result.IsSuspecious)
                    {
                        var alerm = new SuspeciousCarAlermInfo()
                                        {
                                            CapturedLicenseInfo = licensePlateInfo,
                                            ReportedCarInfo = result
                                        };

                        foreach (var l in _suspeciousLicensePlateObserver)
                        {
                            try
                            {
                                l.Notify(alerm);
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
                            }
                           
                        }
                    }
                });
        }
    }
}