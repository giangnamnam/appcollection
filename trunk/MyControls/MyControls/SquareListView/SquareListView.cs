using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MultiPicListView
{
    public partial class SquareListView : UserControl
    {
        public SquareListView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Count = 2;
            this.Padding = new Padding(3);

            this.Resize += (sender, args) => { this.CalculateLayout();  this.Invalidate(); };
        }

        private int _Count;
        public int Count
        {
            get
            {
                return _Count;
            }
            set
            {
                if (_Count == value)
                    return;
                _Count = value;
                this.CalculateLayout();
                this.Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        Cell[] cells = new Cell[0];

        private Cell[] CalculateLayout()
        {
            Cell[] cells = new Cell[this.Count*this.Count];

            int width = this.ClientRectangle.Width / this.Count;
            int height = this.ClientRectangle.Height / this.Count;

            for (int i = 0; i < this.Count; i++)
            {
                for (int j = 0; j < this.Count; j++)
                {
                    int idx = i * this.Count + j;
                    Cell c = new Cell();
                    c.Rec = new RectangleF(i * width + this.Padding.Left,
                        j * height + this.Padding.Top, 
                        width - this.Padding.Horizontal,
                        height - this.Padding.Vertical);
                    c.ImagePath = @"d:\1.jpg";
                    c.Text = DateTime.Now.ToString();

                    cells[idx] = c;
                }
            }

            this.cells = cells;

            return cells;

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Cell c in this.cells)
            {
                c.Paint(e.Graphics, this.Font);
            }
        }


    }
}
