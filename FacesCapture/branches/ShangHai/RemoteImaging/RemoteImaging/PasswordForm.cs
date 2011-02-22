using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RemoteImaging
{
    public partial class PasswordForm : DevExpress.XtraEditors.XtraForm
    {
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "12345")
            {
                this.DialogResult = DialogResult.OK;
            }
        }


     
    }
}
