using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace RemoteImaging.LicensePlate
{
    public partial class FormSuspectCarManager : XtraForm
    {
        private readonly LicensePlateRepository _suspectCarRepository;
        private MyBindingList<ReportedCarInfo> _carInfos;
        private List<ReportedCarInfo> _removedCarInfos = new List<ReportedCarInfo>();

        public List<ReportedCarInfo> CarInfos
        {
            get { return _carInfos.ToList(); }
            set
            {
                _carInfos = new MyBindingList<ReportedCarInfo>(value);
                _carInfos.BeforeRemove += _carInfos_BeforeRemove;

                suspectCarGrid.DataSource = _carInfos;
            }
        }

        void _carInfos_BeforeRemove(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                var item = _carInfos[e.NewIndex];
                _removedCarInfos.Add(item);
            }
        }

        public List<ReportedCarInfo> RemovedInfos
        {
            get { return _removedCarInfos; }
        }

        public FormSuspectCarManager()
        {
            InitializeComponent();

            var enums = from object t in Enum.GetValues(typeof(LicensePlate.ReportedCarMissingType))
                        select new { Name = ((ReportedCarMissingType)t).GetDescription(), Value = t };

            carMissingType.DisplayMember = "Name";
            carMissingType.ValueMember = "Value";


            carMissingType.DataSource = enums.ToList();
        }



        private void suspectCarGridView_InitNewRow(object sender,
            DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            var row = (ReportedCarInfo)suspectCarGridView.GetRow(e.RowHandle);
            row.SetupTime = DateTime.Now;
        }
    }
}