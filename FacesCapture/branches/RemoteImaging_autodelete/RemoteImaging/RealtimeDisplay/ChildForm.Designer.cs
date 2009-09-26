namespace RemoteImaging.RealtimeDisplay
{
    partial class ChildForm
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
            this.searchFaceViewer1 = new RemoteImaging.Controls.SearchFaceViewer();
            this.SuspendLayout();
            // 
            // searchFaceViewer1
            // 
            this.searchFaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchFaceViewer1.Location = new System.Drawing.Point(0, 0);
            this.searchFaceViewer1.Name = "searchFaceViewer1";
            this.searchFaceViewer1.Size = new System.Drawing.Size(856, 502);
            this.searchFaceViewer1.TabIndex = 0;
            // 
            // ChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 502);
            this.Controls.Add(this.searchFaceViewer1);
            this.Name = "ChildForm";
            this.Text = "ChildForm";
            this.ResumeLayout(false);

        }

        #endregion

        private RemoteImaging.Controls.SearchFaceViewer searchFaceViewer1;

    }
}