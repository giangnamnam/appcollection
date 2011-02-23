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
    public partial class FormRoi : Form
    {
        private Damany.Windows.Form.PictureBox _pictureBox;
        private Rectangle _roi;
        public FormRoi()
        {
            InitializeComponent();
            CreatePictureBox();
        }

        public Image Image
        {
            set { _pictureBox.Image = value; }
        }

        public Rectangle Roi
        {
            set
            {
                _pictureBox.Clear();
                _pictureBox.AddRectangle(value);
                _roi = value;
            }
            get { return _roi; }
        }

        private void FormRoi_Load(object sender, EventArgs e)
        {

        }
        private void CreatePictureBox()
        {
            _pictureBox = new Damany.Windows.Form.PictureBox();
            _pictureBox.FigureDrawn += pb_FigureDrawn;
            _pictureBox.Dock = DockStyle.Fill;
            _pictureBox.DrawRectangle = true;
            this.Controls.Add(_pictureBox);
            _pictureBox.BringToFront();
        }

        void pb_FigureDrawn(object sender, Damany.Windows.Form.DrawFigureEventArgs e)
        {
            Roi = e.Rectangle;
        }

        private void revertColor_CheckedChanged(object sender, EventArgs e)
        {
            _pictureBox.DrawingColor = revertColor.Checked ? Color.White : Color.Black;
        }
    }
}
