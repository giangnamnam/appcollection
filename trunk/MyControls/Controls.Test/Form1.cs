using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyControls;


namespace Controls.Test
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

        private void button1_Click(object sender, EventArgs e)
        {
            string[] files
                = System.IO.Directory.GetFiles(@"d:\20090505");
            
            ImageCell[] cells = new ImageCell[246];
            for (int i = 0; i < 246; i++)
            {
                Image img = Image.FromFile(files[i]);
                Graphics g = Graphics.FromImage(img);
                string text = DateTime.Now.ToShortTimeString() + ":" + i.ToString(); 
                g.DrawString(text, SystemFonts.CaptionFont, Brushes.Black, 0, 0);
                ImageCell newCell = new ImageCell() { Image = img, Path = "", Text = text, Tag = null };
                cells[i] = newCell;
            }

            this.multiPicListView1.ShowImages(cells);
        }
    }
}
