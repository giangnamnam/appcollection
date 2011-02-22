using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteImaging.LicensePlate
{

    public class LicensePlateSearchPresenter : ILicensePlateSearchPresenter
    {
        private readonly ILicenseplateSearchScreen _screen;
        private readonly IMessageBoxService _messageBoxService;
        private readonly LicensePlateRepository _repository;

        public LicensePlateSearchPresenter(ILicenseplateSearchScreen screen, 
                                           IMessageBoxService messageBoxService,
                                           LicensePlateRepository repository)
        {
            if (screen == null) throw new ArgumentNullException("screen");
            if (messageBoxService == null) throw new ArgumentNullException("messageBoxService");
            if (repository == null) throw new ArgumentNullException("repository");

            _screen = screen;
            _messageBoxService = messageBoxService;
            _repository = repository;
        }


        public void Search()
        {
            
            var predicates = new SearchCretia();

            if (_screen.MatchLicenseNumber)
            {
                predicates.AddCretia(dto => dto.LicensePlateNumber.ToUpper().Contains(_screen.LicenseNumber.ToUpper()));
            }

            if (_screen.MatchTimeRange)
            {
                var range = _screen.Range;
                predicates.AddCretia(dto => dto.CaptureTime >= range.From && dto.CaptureTime <= range.To);
            }

            var searcher = Task.Factory.StartNew(() =>
                                                             {
                                                                 _screen.IsBusy = true;
                                                                 try
                                                                 {
                                                                     var query = _repository.GetLicensePlates(predicates);
                                                                     _screen.Clear();
                                                                     foreach (var licensePlateInfo in query)
                                                                     {
                                                                         _screen.AddLicensePlateInfo(licensePlateInfo);
                                                                     }
                                                                 }
                                                                 finally
                                                                 {
                                                                     _screen.IsBusy = false;
                                                                 }
                                                             });

            searcher.ContinueWith(ant =>
                                      {
                                          var ignore = ant.Exception;
                                          _messageBoxService.ShowError("搜索车牌的过程中发生错误，错误信息已经被记录在系统日志中，请联系技术支持。");
                                      },
                TaskContinuationOptions.OnlyOnFaulted);
        }

        public void Start()
        {
            _screen.AttachPresenter(this);
            _screen.Show();
        }
    }
}
