namespace MultiPicListView.Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.multiPicListView1 = new SquareListView();
            this.SuspendLayout();
            // 
            // multiPicListView1
            // 
            this.multiPicListView1.Count = 3;
            this.multiPicListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.multiPicListView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.multiPicListView1.Location = new System.Drawing.Point(0, 0);
            this.multiPicListView1.Name = "multiPicListView1";
            this.multiPicListView1.Size = new System.Drawing.Size(758, 528);
            this.multiPicListView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 528);
            this.Controls.Add(this.multiPicListView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private SquareListView multiPicListView1;

    }
}

