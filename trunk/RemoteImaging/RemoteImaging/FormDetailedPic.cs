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
    public partial class FormDetailedPic : Form
    {
        public FormDetailedPic()
        {
            InitializeComponent();
        }


        public RealtimeDisplay.ImageDetail Img
        {
            set
            {
                this.pictureEdit1.Image = Image.FromFile(value.Path);
                this.captureTime.Text = value.CaptureTime.ToString();
            }
        }

        private void FormDetailedPic_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.pictureEdit1.Dispose();
        }
    }
}
