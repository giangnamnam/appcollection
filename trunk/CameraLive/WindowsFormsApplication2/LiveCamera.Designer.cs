namespace WindowsFormsApplication2
{
    partial class LiveCamera
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.framesPerSec = new System.Windows.Forms.Label();
            this.live = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureFiltered = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureFace = new System.Windows.Forms.PictureBox();
            this.sanyoNetCamera1 = new WindowsFormsApplication2.SanyoNetCamera(this.components);
            this.cameraIP = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.textBoxOutputFolder = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFiltered)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 226);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(181, 455);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "图片保存目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 411);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "获取频率：";
            // 
            // framesPerSec
            // 
            this.framesPerSec.AutoSize = true;
            this.framesPerSec.Location = new System.Drawing.Point(385, 411);
            this.framesPerSec.Name = "framesPerSec";
            this.framesPerSec.Size = new System.Drawing.Size(44, 15);
            this.framesPerSec.TabIndex = 14;
            this.framesPerSec.Text = "1 帧/秒";
            // 
            // live
            // 
            this.live.AutoSize = true;
            this.live.Checked = true;
            this.live.CheckState = System.Windows.Forms.CheckState.Checked;
            this.live.Location = new System.Drawing.Point(12, 244);
            this.live.Name = "live";
            this.live.Size = new System.Drawing.Size(74, 19);
            this.live.TabIndex = 15;
            this.live.Text = "实时显示";
            this.live.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "摄像头IP：";
            // 
            // pictureFiltered
            // 
            this.pictureFiltered.Location = new System.Drawing.Point(458, 12);
            this.pictureFiltered.Name = "pictureFiltered";
            this.pictureFiltered.Size = new System.Drawing.Size(417, 226);
            this.pictureFiltered.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureFiltered.TabIndex = 18;
            this.pictureFiltered.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(294, 455);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // pictureFace
            // 
            this.pictureFace.Location = new System.Drawing.Point(455, 261);
            this.pictureFace.Name = "pictureFace";
            this.pictureFace.Size = new System.Drawing.Size(419, 217);
            this.pictureFace.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureFace.TabIndex = 20;
            this.pictureFace.TabStop = false;
            // 
            // sanyoNetCamera1
            // 
            this.sanyoNetCamera1.IPAddress = "192.168.0.2";
            this.sanyoNetCamera1.Password = "admin";
            this.sanyoNetCamera1.UserName = "admin";
            // 
            // cameraIP
            // 
            this.cameraIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WindowsFormsApplication2.Properties.Settings.Default, "cameraIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cameraIP.Location = new System.Drawing.Point(12, 284);
            this.cameraIP.Name = "cameraIP";
            this.cameraIP.Size = new System.Drawing.Size(419, 21);
            this.cameraIP.TabIndex = 17;
            this.cameraIP.Text = global::WindowsFormsApplication2.Properties.Settings.Default.cameraIP;
            this.cameraIP.TextChanged += new System.EventHandler(this.cameraIP_TextChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::WindowsFormsApplication2.Properties.Settings.Default, "FramePerSec", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(92, 401);
            this.trackBar1.Maximum = 25;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(277, 48);
            this.trackBar1.TabIndex = 12;
            this.trackBar1.Value = global::WindowsFormsApplication2.Properties.Settings.Default.FramePerSec;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // textBoxOutputFolder
            // 
            this.textBoxOutputFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::WindowsFormsApplication2.Properties.Settings.Default, "OutputFolder", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxOutputFolder.Location = new System.Drawing.Point(12, 334);
            this.textBoxOutputFolder.Name = "textBoxOutputFolder";
            this.textBoxOutputFolder.Size = new System.Drawing.Size(419, 21);
            this.textBoxOutputFolder.TabIndex = 10;
            this.textBoxOutputFolder.Text = global::WindowsFormsApplication2.Properties.Settings.Default.OutputFolder;
            // 
            // LiveCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 527);
            this.Controls.Add(this.pictureFace);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureFiltered);
            this.Controls.Add(this.cameraIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.live);
            this.Controls.Add(this.framesPerSec);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOutputFolder);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "LiveCamera";
            this.Text = "LiveCamera";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFiltered)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureFace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private SanyoNetCamera sanyoNetCamera1;
        private System.Windows.Forms.TextBox textBoxOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label framesPerSec;
        private System.Windows.Forms.CheckBox live;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox cameraIP;
        private System.Windows.Forms.PictureBox pictureFiltered;
        private System.Windows.Forms.Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox pictureFace;
    }
}

