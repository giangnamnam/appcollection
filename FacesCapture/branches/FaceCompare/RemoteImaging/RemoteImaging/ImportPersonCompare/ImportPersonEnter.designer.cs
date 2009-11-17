namespace RemoteImaging.ImportPersonCompare
{
    partial class ImportPersonEnter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPersonEnter));
            this.picTargetPerson = new System.Windows.Forms.PictureBox();
            this.lblAge = new System.Windows.Forms.Label();
            this.s = new System.Windows.Forms.Label();
            this.lblId = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblCard = new System.Windows.Forms.Label();
            this.btnBrowseImpPerson = new System.Windows.Forms.Button();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtCard = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rabMan = new System.Windows.Forms.RadioButton();
            this.rabWoman = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSimLevel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addFinished = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picTargetPerson)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picTargetPerson
            // 
            this.picTargetPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picTargetPerson.Image = ((System.Drawing.Image)(resources.GetObject("picTargetPerson.Image")));
            this.picTargetPerson.Location = new System.Drawing.Point(3, 17);
            this.picTargetPerson.Name = "picTargetPerson";
            this.picTargetPerson.Size = new System.Drawing.Size(167, 185);
            this.picTargetPerson.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTargetPerson.TabIndex = 10;
            this.picTargetPerson.TabStop = false;
            // 
            // lblAge
            // 
            this.lblAge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(187, 172);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 12);
            this.lblAge.TabIndex = 13;
            this.lblAge.Text = "年龄";
            // 
            // s
            // 
            this.s.AutoSize = true;
            this.s.Location = new System.Drawing.Point(187, 104);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(29, 12);
            this.s.TabIndex = 12;
            this.s.Text = "姓名";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(187, 70);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(29, 12);
            this.lblId.TabIndex = 11;
            this.lblId.Text = "编号";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(187, 138);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(29, 12);
            this.lblSex.TabIndex = 14;
            this.lblSex.Text = "性别";
            // 
            // lblCard
            // 
            this.lblCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCard.AutoSize = true;
            this.lblCard.Location = new System.Drawing.Point(187, 206);
            this.lblCard.Name = "lblCard";
            this.lblCard.Size = new System.Drawing.Size(53, 12);
            this.lblCard.TabIndex = 15;
            this.lblCard.Text = "身份证号";
            // 
            // btnBrowseImpPerson
            // 
            this.btnBrowseImpPerson.Location = new System.Drawing.Point(252, 27);
            this.btnBrowseImpPerson.Name = "btnBrowseImpPerson";
            this.btnBrowseImpPerson.Size = new System.Drawing.Size(148, 23);
            this.btnBrowseImpPerson.TabIndex = 16;
            this.btnBrowseImpPerson.Text = "浏览重点目标人图片...";
            this.btnBrowseImpPerson.UseVisualStyleBackColor = true;
            this.btnBrowseImpPerson.Click += new System.EventHandler(this.btnBrowseImpPerson_Click);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(252, 67);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(167, 21);
            this.txtId.TabIndex = 17;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(252, 101);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 21);
            this.txtName.TabIndex = 18;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(252, 169);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(167, 21);
            this.txtAge.TabIndex = 20;
            // 
            // txtCard
            // 
            this.txtCard.Location = new System.Drawing.Point(252, 203);
            this.txtCard.Name = "txtCard";
            this.txtCard.Size = new System.Drawing.Size(167, 21);
            this.txtCard.TabIndex = 21;
            this.txtCard.Text = "12345678910111213";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(165, 280);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 22;
            this.btnOk.Text = "继续添加";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(349, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "退出";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rabMan
            // 
            this.rabMan.AutoSize = true;
            this.rabMan.Checked = true;
            this.rabMan.Location = new System.Drawing.Point(252, 136);
            this.rabMan.Name = "rabMan";
            this.rabMan.Size = new System.Drawing.Size(35, 16);
            this.rabMan.TabIndex = 24;
            this.rabMan.TabStop = true;
            this.rabMan.Text = "男";
            this.rabMan.UseVisualStyleBackColor = true;
            // 
            // rabWoman
            // 
            this.rabWoman.AutoSize = true;
            this.rabWoman.Location = new System.Drawing.Point(338, 136);
            this.rabWoman.Name = "rabWoman";
            this.rabWoman.Size = new System.Drawing.Size(35, 16);
            this.rabWoman.TabIndex = 25;
            this.rabWoman.Text = "女";
            this.rabWoman.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(187, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "相似度级别 ";
            // 
            // cmbSimLevel
            // 
            this.cmbSimLevel.FormattingEnabled = true;
            this.cmbSimLevel.Items.AddRange(new object[] {
            "重要",
            "一般",
            "普通"});
            this.cmbSimLevel.Location = new System.Drawing.Point(252, 237);
            this.cmbSimLevel.Name = "cmbSimLevel";
            this.cmbSimLevel.Size = new System.Drawing.Size(121, 20);
            this.cmbSimLevel.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(6, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 12);
            this.label2.TabIndex = 28;
            this.label2.Text = "提示：请选择重点目标人后，设置相应信息";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picTargetPerson);
            this.groupBox1.Location = new System.Drawing.Point(8, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(173, 205);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "重点人员图片显示";
            // 
            // addFinished
            // 
            this.addFinished.Location = new System.Drawing.Point(246, 280);
            this.addFinished.Name = "addFinished";
            this.addFinished.Size = new System.Drawing.Size(75, 23);
            this.addFinished.TabIndex = 30;
            this.addFinished.Text = "完成";
            this.addFinished.UseVisualStyleBackColor = true;
            this.addFinished.Click += new System.EventHandler(this.addFinished_Click);
            // 
            // ImportPersonEnter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 312);
            this.Controls.Add(this.addFinished);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbSimLevel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rabWoman);
            this.Controls.Add(this.rabMan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtCard);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.btnBrowseImpPerson);
            this.Controls.Add(this.lblCard);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblId);
            this.Controls.Add(this.s);
            this.Controls.Add(this.lblAge);
            this.Name = "ImportPersonEnter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重点目标人信息录入";
            this.Load += new System.EventHandler(this.ImportPersonEnter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTargetPerson)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picTargetPerson;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label s;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblCard;
        private System.Windows.Forms.Button btnBrowseImpPerson;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtCard;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rabMan;
        private System.Windows.Forms.RadioButton rabWoman;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSimLevel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addFinished;
    }
}