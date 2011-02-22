using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Damany.Imaging.Common;

namespace RemoteImaging.RealtimeDisplay
{
    public partial class FullVideoScreen : Form
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _autoSizePicture = false;
        private Size _pictueBoxSize;

        public FullVideoScreen(IEventAggregator eventAggregator):this()
        {
            _eventAggregator = eventAggregator;
        }

        public FullVideoScreen()
        {
            InitializeComponent();
        }

        private void FullVideoScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void exitFullScreen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FullVideoScreen_Load(object sender, EventArgs e)
        {
            _eventAggregator.PortraitFound += _eventAggregator_PortraitFound;

            _pictueBoxSize = pictureBox1.ClientSize;

            pictureBox1.MouseEnter += (s, arg) =>
                                          {
                                              _autoSizePicture = true;

                                              if (pictureBox1.Image != null)
                                              {
                                                  pictureBox1.ClientSize = pictureBox1.Image.Size;
                                                  MovePictureBox();
                                              }

                                              
                                          };
            pictureBox1.MouseLeave += (s, arg) =>
                                          {
                                              _autoSizePicture = false;
                                              pictureBox1.ClientSize = _pictueBoxSize;
                                              MovePictureBox();
                                          };
        }

        void _eventAggregator_PortraitFound(object sender, MiscUtil.EventArgs<Portrait> e)
        {
            var bmp = e.Value.GetIpl().ToBitmap();

            this.BeginInvoke(new Action(()=>
                                            {
                                                if (this.pictureBox1.Image != null)
                                                {
                                                    var save = this.pictureBox1.Image;
                                                    this.pictureBox1.Image = null;
                                                    save.Dispose();
                                                }

                                                if (_autoSizePicture)
                                                {
                                                    this.pictureBox1.ClientSize = bmp.Size;
                                                    MovePictureBox();
                                                }

                                                this.pictureBox1.Image = bmp;
                                                this.pictureBox1.Visible = true;
                                            }));

        }

        private void FullVideoScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            _eventAggregator.PortraitFound -= _eventAggregator_PortraitFound;
        }

        private void MovePictureBox()
        {
            var l = new Point(this.ClientSize.Width - this.pictureBox1.Width - 2,
                      this.ClientSize.Height - this.pictureBox1.Height - 2);

            this.pictureBox1.Location = l;
        }
    }
}
