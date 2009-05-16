namespace RemoteImaging
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.browseForOutputFolder = new System.Windows.Forms.Button();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.browseForUploadFolder = new System.Windows.Forms.Button();
            this.textBoxUploadFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.linkLabelConfigCamera = new System.Windows.Forms.LinkLabel();
            this.dataGridCameras = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Appearance.BackColor = System.Drawing.Color.White;
            this.groupControl1.Appearance.Options.UseBackColor = true;
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Controls.Add(this.pictureBox1);
            this.groupControl1.Location = new System.Drawing.Point(-6, -16);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(574, 87);
            this.groupControl1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "在这里设置系统参数，如上传目录，图片转存目录等等";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(111, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置系统参数";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(43, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 77);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 262);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.browseForOutputFolder);
            this.tabPage1.Controls.Add(this.textBoxOutputFolder);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.browseForUploadFolder);
            this.tabPage1.Controls.Add(this.textBoxUploadFolder);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(486, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "目录";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(37, 191);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(63, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(380, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "注意: 图片输出目录和摄像头上传目录不要选择同一个目录。";
            // 
            // browseForOutputFolder
            // 
            this.browseForOutputFolder.Location = new System.Drawing.Point(381, 131);
            this.browseForOutputFolder.Name = "browseForOutputFolder";
            this.browseForOutputFolder.Size = new System.Drawing.Size(75, 23);
            this.browseForOutputFolder.TabIndex = 5;
            this.browseForOutputFolder.Text = "浏览";
            this.browseForOutputFolder.UseVisualStyleBackColor = true;
            this.browseForOutputFolder.Click += new System.EventHandler(this.browseForOutputFolder_Click);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "OutputPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(37, 133);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(328, 20);
            this.textBoxOutputFolder.TabIndex = 4;
            this.textBoxOutputFolder.Text = global::RemoteImaging.Properties.Settings.Default.OutputPath;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "图片输出目录:";
            // 
            // browseForUploadFolder
            // 
            this.browseForUploadFolder.Location = new System.Drawing.Point(381, 63);
            this.browseForUploadFolder.Name = "browseForUploadFolder";
            this.browseForUploadFolder.Size = new System.Drawing.Size(75, 23);
            this.browseForUploadFolder.TabIndex = 2;
            this.browseForUploadFolder.Text = "浏览";
            this.browseForUploadFolder.UseVisualStyleBackColor = true;
            this.browseForUploadFolder.Click += new System.EventHandler(this.browseForUploadFolder_Click);
            // 
            // textBoxUploadFolder
            // 
            this.textBoxUploadFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::RemoteImaging.Properties.Settings.Default, "ImageUploadPool", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxUploadFolder.Location = new System.Drawing.Point(37, 65);
            this.textBoxUploadFolder.Name = "textBoxUploadFolder";
            this.textBoxUploadFolder.Size = new System.Drawing.Size(328, 20);
            this.textBoxUploadFolder.TabIndex = 1;
            this.textBoxUploadFolder.Text = global::RemoteImaging.Properties.Settings.Default.ImageUploadPool;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "摄像头图片上传目录:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.linkLabelConfigCamera);
            this.tabPage2.Controls.Add(this.dataGridCameras);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(486, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "摄像头";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // linkLabelConfigCamera
            // 
            this.linkLabelConfigCamera.AutoSize = true;
            this.linkLabelConfigCamera.Location = new System.Drawing.Point(374, 16);
            this.linkLabelConfigCamera.Name = "linkLabelConfigCamera";
            this.linkLabelConfigCamera.Size = new System.Drawing.Size(91, 13);
            this.linkLabelConfigCamera.TabIndex = 1;
            this.linkLabelConfigCamera.TabStop = true;
            this.linkLabelConfigCamera.Text = "设置摄像头参数";
            this.linkLabelConfigCamera.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelConfigCamera_LinkClicked);
            // 
            // dataGridCameras
            // 
            this.dataGridCameras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridCameras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.ID,
            this.IP});
            this.dataGridCameras.Location = new System.Drawing.Point(16, 32);
            this.dataGridCameras.Name = "dataGridCameras";
            this.dataGridCameras.Size = new System.Drawing.Size(453, 181);
            this.dataGridCameras.TabIndex = 0;
            // 
            // name
            // 
            this.name.HeaderText = "名称";
            this.name.Name = "name";
            // 
            // ID
            // 
            this.ID.HeaderText = "编号";
            this.ID.Name = "ID";
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(343, 347);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(424, 347);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(502, 382);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "系统设置";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridCameras)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button browseForOutputFolder;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button browseForUploadFolder;
        private System.Windows.Forms.TextBox textBoxUploadFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dataGridCameras;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.LinkLabel linkLabelConfigCamera;
    }
}