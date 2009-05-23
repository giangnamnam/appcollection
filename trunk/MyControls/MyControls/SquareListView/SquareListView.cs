using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MyControls
{
    public partial class SquareListView : UserControl
    {
        public SquareListView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.NumberOfColumns = 2;
            this.Padding = new Padding(3);

            refreshTimer.Interval = 1000;
            refreshTimer.Enabled = false;
            refreshTimer.AutoReset = true;
            refreshTimer.Elapsed += refreshTimer_Elapsed;

            this.AutoDisposeImage = true;

            this.Resize += (sender, args) => { this.CalcCellSize(this.cells); this.Invalidate(); };
        }


        public event EventHandler SelectedCellChanged;

        private void FireSelectedCellChanged()
        {
            if (SelectedCellChanged != null)
            {
                SelectedCellChanged(this, EventArgs.Empty);
            }
        }


        int idx = 0;

        void refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (imgQueue.Count <= 0)
                {
                    this.refreshTimer.Enabled = false;
                    return;
                }

                Cell dstCell = this.cells[idx];
                ImageCell imgToShow = this.imgQueue.Dequeue();

                if (this.AutoDisposeImage && dstCell.Image != null)
                {
                    dstCell.Image.Dispose();
                }

                dstCell.Image = imgToShow.Image;
                dstCell.Text = imgToShow.Text;
                dstCell.Path = imgToShow.Path;
                //dstCell.Selected = false;
                //selectedCell = null;

                this.Invalidate(Rectangle.Round(dstCell.Rec));


                idx = ++idx % this.cells.Length;
            }
            catch (InvalidOperationException ex)// the queue is empty
            {
                this.refreshTimer.Enabled = false;
            }



            System.Diagnostics.Debug.WriteLine("tick");

        }

        public bool AutoDisposeImage { get; set; }

        System.Timers.Timer refreshTimer = new System.Timers.Timer();
        Queue<ImageCell> imgQueue = new Queue<ImageCell>();

        public void ShowImages(ImageCell[] imgs)
        {
            Array.ForEach(imgs, imgQueue.Enqueue);

            if (imgQueue.Count > 0 && this.Visible)
            {
                refreshTimer.Enabled = true;
                System.Diagnostics.Debug.WriteLine("tick");
            }
        }




        private int _Count;
        public int NumberOfColumns
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

                if (this.selectedCell != null)
                {
                    this.selectedCell.Selected = false;
                }
                this.CalculateLayout();

                this.Invalidate();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }

        Cell[] cells = new Cell[0];

        private void CalcCellSize(Cell[] cells)
        {
            int width = this.ClientRectangle.Width / this.NumberOfColumns;
            int height = this.ClientRectangle.Height / this.NumberOfColumns;

            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    int idx = j * this.NumberOfColumns + i;
                    Cell c = cells[idx];
                    c.Rec = new Rectangle(i * width + this.Padding.Left,
                        j * height + this.Padding.Top,
                        width - this.Padding.Horizontal,
                        height - this.Padding.Vertical);
                    //c.Text = DateTime.Now.ToString();
                }
            }
        }

        private Cell[] CreateNewCells()
        {
            Cell[] newCells = new Cell[this.NumberOfColumns * this.NumberOfColumns];
            for (int i = 0; i < newCells.Length; i++)
            {
                newCells[i] = new Cell();
            }

            CalcCellSize(newCells);
            MoveOldCells(newCells);

            return newCells;
        }


        private void MoveOldCells(Cell[] newCells)
        {
            if (this.cells != null)
            {
                int count = Math.Min(newCells.Length, cells.Length);
                for (int i = 0; i < count; i++)
                {
                    newCells[i].Path = cells[i].Path;
                    newCells[i].Image = cells[i].Image;
                    newCells[i].Text = cells[i].Text;
                    newCells[i].Selected = cells[i].Selected;
                }
            }
        }

        private Cell[] ReCalcLayOut()
        {
            CalcCellSize(this.cells);

            return this.cells;
        }

        private Cell[] CalculateLayout()
        {

            Cell[] newCells = CreateNewCells();

            MoveOldCells(newCells);

            if (this.idx > newCells.Length - 1)
            {
                this.idx = 0;
            }

            this.cells = newCells;

            return newCells;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Cell c in this.cells)
            {
                if (e.ClipRectangle.IntersectsWith(Rectangle.Round(c.Rec)))
                {
                    c.Paint(e.Graphics, this.Font);
                }
            }
        }

        private Cell FindCell(Point pt)
        {
            foreach (Cell c in this.cells)
            {
                if (c.Rec.Contains(pt))
                {
                    return c;
                }
            }

            return null;
        }
        private void SquareListView_MouseClick(object sender, MouseEventArgs e)
        {
            Cell c = FindCell(e.Location);
            if (c != null)
            {
                if (LastSelectedCell != c)
                {
                    LastSelectedCell = c;
                }

                if (selectedCell != null)
                {
                    selectedCell.Selected = false;
                    this.Invalidate(Rectangle.Round(selectedCell.Rec));
                }

                c.Selected = true;
                this.Invalidate(Rectangle.Round(c.Rec));

                selectedCell = c;

                FireSelectedCellChanged();
            }
        }


        public Cell LastSelectedCell { get; private set; }
        public Cell SelectedCell
        {
            get
            {
                return selectedCell;
            }
            set
            {
                selectedCell = value;
            }
        }
        private Cell selectedCell;
    }
}
