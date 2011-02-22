using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public interface ILicensePlateDataProvider
    {
        void Save(DtoLicensePlateInfo licensePlateInfo);

        IEnumerable<DtoLicensePlateInfo> GetLicensePlatesBetween(int cameraId, Damany.Util.DateTimeRange dateTimeRange);
        IEnumerable<DtoLicensePlateInfo> GetRecordsFor(string licensePlateNumber);
        IEnumerable<DtoLicensePlateInfo> GetLicensePlates(Predicate<DtoLicensePlateInfo> predicate);

        List<ReportedCarInfo> GetReportedCarInfos();
        ReportedCarInfo GetReportedCarInfoByNumber(string number);
        void Save(List<ReportedCarInfo> newCarInfos, List<ReportedCarInfo> removedInfos);

        List<SuspectCarAlermHandleInfo> GetCarAlermHandleInfo();
        void Save(SuspectCarAlermHandleInfo suspectCarAlermHandleInfo);
    }
}
