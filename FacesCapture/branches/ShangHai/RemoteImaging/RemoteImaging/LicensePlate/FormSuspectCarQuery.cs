using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;

namespace RemoteImaging.LicensePlate
{
    public partial class FormSuspectCarQuery : XtraForm
    {
        private readonly LicensePlateRepository _licensePlateRepository;
        private readonly IMessageBoxService _messageBoxService;

        public FormSuspectCarQuery()
        {
            InitializeComponent();
        }

        public FormSuspectCarQuery(LicensePlateRepository licensePlateRepository,
                                   IMessageBoxService messageBoxService)
            : this()
        {
            if (messageBoxService == null) throw new ArgumentNullException("messageBoxService");
            _licensePlateRepository = licensePlateRepository;
            _messageBoxService = messageBoxService;
        }

        private void sbSearch_Click(object sender, EventArgs e)
        {
            var infos = _licensePlateRepository.GetCarAlermHandleInfo();

            var converted = from c in infos
                            select new SuspectCarQueryInfo(c.AlermInfo)
                                       {
                                           ProcessBehavior = c.ProcessBehavior,
                                           ProcessTime = c.HandleTime,
                                       };

            carGrid.DataSource = converted.ToList();
        }

        private void carGridView_RowClick(object sender, RowClickEventArgs e)
        {
            ShowDetail(e.RowHandle);
        }

        private void ShowDetail(int rowHandle)
        {
            var row = carGridView.GetRow(rowHandle) as SuspectCarQueryInfo;
            if (row != null)
            {
                try
                {
                    LicensePlate.Helper.UpdateSuspectCarAlerm(this.suspectCarControl1, row.AlermInfo);
                }
                catch (Exception)
                {
                    _messageBoxService.ShowError("图片丢失或损坏！");
                }

            }
        }

        private void carGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.ShowDetail(e.FocusedRowHandle);
        }


        private class SuspectCarQueryInfo
        {
            public SuspectCarQueryInfo(SuspeciousCarAlermInfo alermInfo)
            {
                AlermInfo = alermInfo;
            }

            public SuspeciousCarAlermInfo AlermInfo { get; private set; }

            public string LicenseNumber
            {
                get
                {
                    return AlermInfo.ReportedCarInfo.LicensePlateNumber;
                }
            }

            public DateTime CaptureTime
            {
                get { return AlermInfo.CapturedLicenseInfo.CaptureTime; }
            }

            public DateTime ProcessTime { get; set; }
            public ProcessBehavior ProcessBehavior { get; set; }
            public string ProcessBehaviorDescription { get { return ProcessBehavior.GetDescription(); } }
        }

    }



}