using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public class LicensePlateRepository : ILicensePlateObserver
    {
        private readonly string _outputDirectory;
        private readonly ILicensePlateDataProvider _dataProvider;
        private readonly Mapper _mapper;

        public LicensePlateRepository(string outputDirectory, ILicensePlateDataProvider dataProvider)
        {
            if (outputDirectory == null) throw new ArgumentNullException("outputDirectory");
            if (!System.IO.Directory.Exists(outputDirectory))
            {
                throw new System.IO.DirectoryNotFoundException("output directory of license plate image does't exist");
            }
            if (dataProvider == null) throw new ArgumentNullException("dataProvider");

            _outputDirectory = outputDirectory;
            _dataProvider = dataProvider;
            _mapper = new Mapper(outputDirectory);
        }


        public void Save(LicensePlateInfo licensePlateInfo)
        {
            var dto = _mapper.ToDto(licensePlateInfo);

            var absolutePath = System.IO.Path.Combine(_outputDirectory, dto.LicensePlateImageFileRelativePath);

            var dir = System.IO.Directory.GetParent(absolutePath).ToString();
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }

            System.IO.File.WriteAllBytes(absolutePath, licensePlateInfo.ImageData);
            licensePlateInfo.LicensePlateImageFileAbsolutePath = absolutePath;
            _dataProvider.Save(dto);
        }


        public IEnumerable<LicensePlateInfo> GetLicensePlates(SearchCretia searchCretia)
        {
            var dtos = _dataProvider.GetLicensePlates(searchCretia.MatchWith);
            return dtos.Select(dto => _mapper.ToBusinessObject(dto));
        }

        public IEnumerable<LicensePlateInfo> GetRecordsFor(string licensePlateNumber)
        {
            var dtos = _dataProvider.GetRecordsFor(licensePlateNumber);
            return dtos.Select(dto => _mapper.ToBusinessObject(dto));
        }

        public List<ReportedCarInfo> GetReportedCarInfos()
        {
            return _dataProvider.GetReportedCarInfos();
        }

        public ReportedCarInfo GetReportedCarInfoByNumber(string number)
        {
            return _dataProvider.GetReportedCarInfoByNumber(number);

        }

        public void Save(List<ReportedCarInfo> newCarInfos, List<ReportedCarInfo> removedCarInfos)
        {
            _dataProvider.Save(newCarInfos, removedCarInfos);
        }


        public void LicensePlateCaptured(LicensePlateInfo licensePlateInfo)
        {
            Save(licensePlateInfo);
        }

        public ILicensePlateEventPublisher Publisher
        {
            set
            {
                value.RegisterForLicensePlateEvent(this);
            }
        }

        public List<SuspectCarAlermHandleInfo> GetCarAlermHandleInfo()
        {
            return _dataProvider.GetCarAlermHandleInfo();
    }



        public  void Save(SuspectCarAlermHandleInfo suspectCarAlermHandleInfo)
        {
            _dataProvider.Save(suspectCarAlermHandleInfo);
            
        }


    }
}
