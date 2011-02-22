using System;
using System.Collections.Generic;
using System.Linq;
using Damany.Util;
using Db4objects.Db4o;
using Db4objects.Db4o.Linq;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateDataProvider : ILicensePlateDataProvider
    {
        private readonly string _outputDirectory;
        private readonly IEmbeddedObjectContainer _db4oContainer;
        private bool _isDirty = true;
        private List<ReportedCarInfo> _carList;

        public LicensePlateDataProvider(string outputDirectory)
        {
            if (string.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException("outputDirectory");

            if (!System.IO.Directory.Exists(outputDirectory))
                throw new System.IO.DirectoryNotFoundException("output directory of licenseplate not found");

            _outputDirectory = outputDirectory;

            var dbFilePath = System.IO.Path.Combine(_outputDirectory, "LicensePlates.db4o");
            _db4oContainer = Db4oEmbedded.OpenFile(dbFilePath);

        }


        public void Save(DtoLicensePlateInfo licensePlateInfo)
        {
            _db4oContainer.Store(licensePlateInfo);
            _db4oContainer.Commit();
        }


        public IEnumerable<DtoLicensePlateInfo> GetLicensePlatesBetween(int cameraId, DateTimeRange dateTimeRange)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {

                                                                     var match = l.CaptureTime >= dateTimeRange.From &&
                                                                                 l.CaptureTime <= dateTimeRange.To;

                                                                     if (cameraId != -1)
                                                                     {
                                                                         match = match && l.CapturedFrom == cameraId;
                                                                     }
                                                                                 ;
                                                                     return match;
                                                                 });
        }

        public IEnumerable<DtoLicensePlateInfo> GetRecordsFor(string licensePlateNumber)
        {
            return _db4oContainer.Query<DtoLicensePlateInfo>(l =>
                                                                 {
                                                                    return l.LicensePlateNumber.ToUpper().Contains(
                                                                             licensePlateNumber.ToUpper());
                                                                 });

        }

        public IEnumerable<DtoLicensePlateInfo> GetLicensePlates(Predicate<DtoLicensePlateInfo> predicate)
        {
            var query = from DtoLicensePlateInfo dto in _db4oContainer
                        where predicate(dto)
                        select dto;

            return query;
        }

        public List<ReportedCarInfo> GetReportedCarInfos()
        {
            var query = from ReportedCarInfo c in _db4oContainer
                        select c;

            _carList = query.ToList();

            return _carList;

    }

        public ReportedCarInfo GetReportedCarInfoByNumber(string number)
        {

            var copy = from ReportedCarInfo info in _db4oContainer
                       where string.Compare(info.LicenseNumber, number, true) == 0
                       select info;

            return copy.FirstOrDefault();
        }

        public void Save(List<ReportedCarInfo> newCarInfos, List<ReportedCarInfo> removedInfos)
        {
            var oldInfos = _db4oContainer.Query<ReportedCarInfo>();
            foreach (var item in removedInfos)
            {
                _db4oContainer.Delete(item);
            }

            foreach (var newCarInfo in newCarInfos)
            {
                _db4oContainer.Store(newCarInfo);
            }

            _db4oContainer.Commit();
            _isDirty = true;
        }

        public List<SuspectCarAlermHandleInfo> GetCarAlermHandleInfo()
        {
            var q = from SuspectCarAlermHandleInfo a in _db4oContainer
                    select a;

            return q.ToList();
        }

        public void Save(SuspectCarAlermHandleInfo suspectCarAlermHandleInfo)
        {
            _db4oContainer.Store(suspectCarAlermHandleInfo);
        }
    }
}