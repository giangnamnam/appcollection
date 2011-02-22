using System;
using System.Linq;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateCheckService : ILicensePlateCheckService
    {
        private readonly LicensePlateRepository _suspectCarRepository;


        public LicensePlateCheckService(LicensePlateRepository suspectCarRepository)
        {
            if (suspectCarRepository == null) throw new ArgumentNullException("suspectCarRepository");
            _suspectCarRepository = suspectCarRepository;
        }

        public void Check(string licensePlateNumber, Action<LicensePlateCheckResult> callback)
        {
            var query = _suspectCarRepository.GetReportedCarInfoByNumber(licensePlateNumber);

            var result =  new LicensePlateCheckResult()
                            {
                                IsSuspecious = false,
                                LicensePlateNumber = licensePlateNumber
                            };

            if (query != null)
            {
                result.CarInfo = query;
                result.IsSuspecious = true;
            }

            callback(result);
            
        }
    }
}