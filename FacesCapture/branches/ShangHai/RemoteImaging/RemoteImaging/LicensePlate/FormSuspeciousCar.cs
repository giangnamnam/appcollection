using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging.LicensePlate
{
    public partial class FormSuspeciousCar : DevExpress.XtraEditors.XtraForm
    {
        private SuspectCarFormPresenter _presenter;


        public SuspeciousCarAlermInfo DataContext
        {
            set
            {
                LicensePlate.Helper.UpdateSuspectCarAlerm(this.suspectCarControl1, value);
            }
        }

 

        public FormSuspeciousCar()
        {
            InitializeComponent();
        }

        public void AttachPresenter(SuspectCarFormPresenter presenter)
        {
            if (presenter == null) throw new ArgumentNullException("presenter");

            _presenter = presenter;
        }

        private void muteButton_Click(object sender, EventArgs e)
        {
            _presenter.Mute();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            _presenter.ConfirmAlarm();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _presenter.DiscardAlerm();
        }
    }
}
