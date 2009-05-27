using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MyControls
{
    public partial class SquareListView : UserControl
    {
        public SquareListView()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.Padding = new Padding(3);

            refreshTimer.Interval = 1000;
            refreshTimer.Enabled = false;
            refreshTimer.AutoReset = true;
            refreshTimer.Elapsed += refreshTimer_Elapsed;

            this.AutoDisposeImage = true;

            this.MaxCountOfCells = 25;
            this.cells = new List<Cell>(this.MaxCountOfCells);
            this.numOfColumns = 2;

            this.PopulateCellList();
            this.CalcLayout();

            this.Resize += SquareListView_Resize;
        }

        


        private void FireSelectedCellChanged()
        {
            if (SelectedCellChanged != null)
            {
                SelectedCellChanged(this, EventArgs.Empty);
            }
        }



        void refreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (imgQueue.Count <= 0)
                {
                    this.refreshTimer.Enabled = false;
                    return;
                }

                RepositionCursor();

                Cell dstCell = this.cells[cursor];
                ImageCell imgToShow = this.imgQueue.Dequeue();

                if (this.AutoDisposeImage && dstCell.Image != null)
                {
                    dstCell.Image.Dispose();
                }

                dstCell.Image = imgToShow.Image;
                dstCell.Text = imgToShow.Text;
                dstCell.Path = imgToShow.Path;

                this.Invalidate(Rectangle.Round(dstCell.Rec));
                cursor++;
            }
            catch (InvalidOperationException ex)// the queue is empty
            {
                this.refreshTimer.Enabled = false;
            }

            System.Diagnostics.Debug.WriteLine("tick");

        }

        public bool AutoDisposeImage { get; set; }


        public void ShowImages(ImageCell[] imgs)
        {
            Array.ForEach(imgs, imgQueue.Enqueue);

            if (imgQueue.Count > 0 && this.Visible)
            {
                refreshTimer.Enabled = true;
                System.Diagnostics.Debug.WriteLine("tick");
            }
        }


        
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Gray, ButtonBorderStyle.Solid);
        }


        private void CalcLayout()
        {
            int width = this.ClientRectangle.Width / this.NumberOfColumns;
            int height = this.ClientRectangle.Height / this.NumberOfColumns;

            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                for (int j = 0; j < this.NumberOfColumns; j++)
                {
                    int idx = j * this.NumberOfColumns + i;
                    this.cells[idx].Rec = new Rectangle(i * width + this.Padding.Left,
                        j * height + this.Padding.Top,
                        width - this.Padding.Horizontal,
                        height - this.Padding.Vertical);
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < this.CellsCount; i++)
            {
                Cell c = this.cells[i];
                if (e.ClipRectangle.IntersectsWith(Rectangle.Round(c.Rec)))
                {
                    c.Paint(e.Graphics, this.Font);
                }
            }

        }

        private Cell CellFromPoint(Point pt)
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
            Cell c = CellFromPoint(e.Location);
            if (c != null)
            {
                if (LastSelectedCell != c)
                {
                    LastSelectedCell = c;
                }

                if (SelectedCell != null)
                {
                    SelectedCell.Selected = false;
                    this.Invalidate(Rectangle.Round(SelectedCell.Rec));
                }

                c.Selected = true;
                this.Invalidate(Rectangle.Round(c.Rec));

                SelectedCell = c;

                FireSelectedCellChanged();
            }
        }

        void PopulateCellList()
        {
            int length = this.MaxCountOfCells;
            for (int i = 0; i < length; i++)
            {
                cells.Add(new Cell());
            }
        }

        void SquareListView_Resize(object sender, EventArgs e)
        {
            this.CalcLayout();
            this.Invalidate();
        }

        void RepositionCursor()
        {
            if (cursor > this.CellsCount - 1)
            {
                cursor = 0;
            }
        }


        public int NumberOfColumns
        {
            get
            {
                return numOfColumns;
            }
            set
            {
                if (numOfColumns == value)
                    return;

                if (value * value > this.MaxCountOfCells)
                {
                    throw new ArgumentOutOfRangeException(@"NumberOfColumns",
                        @"Total Number of Cells > Max Number of cells");
                }

                numOfColumns = value;

                if (this.SelectedCell != null)
                {
                    this.SelectedCell.Selected = false;
                }

                this.CalcLayout();
                this.Invalidate();
            }
        }



        public Cell LastSelectedCell { get; private set; }
        public Cell SelectedCell { get; set; }
        public int CellsCount { get { return this.NumberOfColumns * this.NumberOfColumns; } }
        public int MaxCountOfCells { get; set; }


        public event EventHandler SelectedCellChanged;


        int cursor = 0;
        IList<Cell> cells;
        System.Timers.Timer refreshTimer = new System.Timers.Timer();
        Queue<ImageCell> imgQueue = new Queue<ImageCell>();
        private int numOfColumns;
    }
}
