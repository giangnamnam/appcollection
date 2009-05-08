using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiPicListView.Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Cell c = new Cell();
            c.Rec = this.ClientRectangle;
            c.Text = "No Name";
            c.Paint(e.Graphics, this.Font);

        }
    }
}
