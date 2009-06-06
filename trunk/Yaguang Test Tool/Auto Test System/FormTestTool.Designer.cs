namespace Yaguang.VJK3G.GUI
{
    partial class FormTestTool
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
            this.buttonSendCommand = new System.Windows.Forms.Button();
            this.textBoxReceive = new System.Windows.Forms.TextBox();
            this.radioButtonRXChaSun = new System.Windows.Forms.RadioButton();
            this.comboBoxCMDHistory = new System.Windows.Forms.ComboBox();
            this.buttonSendControlCode = new System.Windows.Forms.Button();
            this.radioButtonTXChaSun = new System.Windows.Forms.RadioButton();
            this.radioButtonRXGeLiDu = new System.Windows.Forms.RadioButton();
            this.radioButtonRXPowerResist = new System.Windows.Forms.RadioButton();
            this.radioButtonSwitchSpeed = new System.Windows.Forms.RadioButton();
            this.comboBoxCtrlCodeHistory = new System.Windows.Forms.ComboBox();
            this.checkBoxAV = new System.Windows.Forms.CheckBox();
            this.checkBoxOsc = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonJiaju = new System.Windows.Forms.RadioButton();
            this.radioButtonZeroState = new System.Windows.Forms.RadioButton();
            this.radioButtonTXPowerResist = new System.Windows.Forms.RadioButton();
            this.radioButtonTXGeLiDu = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonSetPWM = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMValue = new System.Windows.Forms.ComboBox();
            this.comboBoxNValue = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSendCommand
            // 
            this.buttonSendCommand.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSendCommand.Location = new System.Drawing.Point(12, 286);
            this.buttonSendCommand.Name = "buttonSendCommand";
            this.buttonSendCommand.Size = new System.Drawing.Size(75, 23);
            this.buttonSendCommand.TabIndex = 5;
            this.buttonSendCommand.Text = "发送";
            this.buttonSendCommand.UseVisualStyleBackColor = true;
            this.buttonSendCommand.Click += new System.EventHandler(this.buttonSendCommand_Click);
            // 
            // textBoxReceive
            // 
            this.textBoxReceive.Location = new System.Drawing.Point(12, 326);
            this.textBoxReceive.Multiline = true;
            this.textBoxReceive.Name = "textBoxReceive";
            this.textBoxReceive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReceive.Size = new System.Drawing.Size(315, 165);
            this.textBoxReceive.TabIndex = 7;
            // 
            // radioButtonRXChaSun
            // 
            this.radioButtonRXChaSun.AutoSize = true;
            this.radioButtonRXChaSun.Location = new System.Drawing.Point(26, 62);
            this.radioButtonRXChaSun.Name = "radioButtonRXChaSun";
            this.radioButtonRXChaSun.Size = new System.Drawing.Size(59, 16);
            this.radioButtonRXChaSun.TabIndex = 0;
            this.radioButtonRXChaSun.TabStop = true;
            this.radioButtonRXChaSun.Text = "RX插损";
            this.radioButtonRXChaSun.UseVisualStyleBackColor = true;
            this.radioButtonRXChaSun.CheckedChanged += new System.EventHandler(this.radioButtonRXChaSun_CheckedChanged);
            // 
            // comboBoxCMDHistory
            // 
            this.comboBoxCMDHistory.FormattingEnabled = true;
            this.comboBoxCMDHistory.Location = new System.Drawing.Point(102, 286);
            this.comboBoxCMDHistory.Name = "comboBoxCMDHistory";
            this.comboBoxCMDHistory.Size = new System.Drawing.Size(396, 20);
            this.comboBoxCMDHistory.TabIndex = 12;
            // 
            // buttonSendControlCode
            // 
            this.buttonSendControlCode.Location = new System.Drawing.Point(90, 191);
            this.buttonSendControlCode.Name = "buttonSendControlCode";
            this.buttonSendControlCode.Size = new System.Drawing.Size(75, 23);
            this.buttonSendControlCode.TabIndex = 2;
            this.buttonSendControlCode.Text = "发送";
            this.buttonSendControlCode.UseVisualStyleBackColor = true;
            this.buttonSendControlCode.Click += new System.EventHandler(this.buttonSendControlCode_Click);
            // 
            // radioButtonTXChaSun
            // 
            this.radioButtonTXChaSun.AutoSize = true;
            this.radioButtonTXChaSun.Location = new System.Drawing.Point(120, 62);
            this.radioButtonTXChaSun.Name = "radioButtonTXChaSun";
            this.radioButtonTXChaSun.Size = new System.Drawing.Size(59, 16);
            this.radioButtonTXChaSun.TabIndex = 3;
            this.radioButtonTXChaSun.TabStop = true;
            this.radioButtonTXChaSun.Text = "TX插损";
            this.radioButtonTXChaSun.UseVisualStyleBackColor = true;
            this.radioButtonTXChaSun.CheckedChanged += new System.EventHandler(this.radioButtonTXChaSun_CheckedChanged);
            // 
            // radioButtonRXGeLiDu
            // 
            this.radioButtonRXGeLiDu.AutoSize = true;
            this.radioButtonRXGeLiDu.Location = new System.Drawing.Point(26, 93);
            this.radioButtonRXGeLiDu.Name = "radioButtonRXGeLiDu";
            this.radioButtonRXGeLiDu.Size = new System.Drawing.Size(71, 16);
            this.radioButtonRXGeLiDu.TabIndex = 4;
            this.radioButtonRXGeLiDu.TabStop = true;
            this.radioButtonRXGeLiDu.Text = "RX隔离度";
            this.radioButtonRXGeLiDu.UseVisualStyleBackColor = true;
            this.radioButtonRXGeLiDu.CheckedChanged += new System.EventHandler(this.radioButtonRXGeLiDu_CheckedChanged);
            // 
            // radioButtonRXPowerResist
            // 
            this.radioButtonRXPowerResist.AutoSize = true;
            this.radioButtonRXPowerResist.Location = new System.Drawing.Point(26, 126);
            this.radioButtonRXPowerResist.Name = "radioButtonRXPowerResist";
            this.radioButtonRXPowerResist.Size = new System.Drawing.Size(83, 16);
            this.radioButtonRXPowerResist.TabIndex = 5;
            this.radioButtonRXPowerResist.TabStop = true;
            this.radioButtonRXPowerResist.Text = "RX通过功率";
            this.radioButtonRXPowerResist.UseVisualStyleBackColor = true;
            this.radioButtonRXPowerResist.CheckedChanged += new System.EventHandler(this.radioButtonRXPowerResist_CheckedChanged);
            // 
            // radioButtonSwitchSpeed
            // 
            this.radioButtonSwitchSpeed.AutoSize = true;
            this.radioButtonSwitchSpeed.Location = new System.Drawing.Point(26, 156);
            this.radioButtonSwitchSpeed.Name = "radioButtonSwitchSpeed";
            this.radioButtonSwitchSpeed.Size = new System.Drawing.Size(71, 16);
            this.radioButtonSwitchSpeed.TabIndex = 6;
            this.radioButtonSwitchSpeed.TabStop = true;
            this.radioButtonSwitchSpeed.Text = "开关速度";
            this.radioButtonSwitchSpeed.UseVisualStyleBackColor = true;
            this.radioButtonSwitchSpeed.CheckedChanged += new System.EventHandler(this.radioButtonSwitchSpeed_CheckedChanged);
            // 
            // comboBoxCtrlCodeHistory
            // 
            this.comboBoxCtrlCodeHistory.FormattingEnabled = true;
            this.comboBoxCtrlCodeHistory.Location = new System.Drawing.Point(80, 220);
            this.comboBoxCtrlCodeHistory.Name = "comboBoxCtrlCodeHistory";
            this.comboBoxCtrlCodeHistory.Size = new System.Drawing.Size(98, 20);
            this.comboBoxCtrlCodeHistory.TabIndex = 7;
            // 
            // checkBoxAV
            // 
            this.checkBoxAV.AutoSize = true;
            this.checkBoxAV.Location = new System.Drawing.Point(34, 47);
            this.checkBoxAV.Name = "checkBoxAV";
            this.checkBoxAV.Size = new System.Drawing.Size(48, 16);
            this.checkBoxAV.TabIndex = 0;
            this.checkBoxAV.Text = "矢网";
            this.checkBoxAV.UseVisualStyleBackColor = true;
            // 
            // checkBoxOsc
            // 
            this.checkBoxOsc.AutoSize = true;
            this.checkBoxOsc.Location = new System.Drawing.Point(34, 102);
            this.checkBoxOsc.Name = "checkBoxOsc";
            this.checkBoxOsc.Size = new System.Drawing.Size(60, 16);
            this.checkBoxOsc.TabIndex = 1;
            this.checkBoxOsc.Text = "示波器";
            this.checkBoxOsc.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonJiaju);
            this.groupBox1.Controls.Add(this.radioButtonZeroState);
            this.groupBox1.Controls.Add(this.radioButtonTXPowerResist);
            this.groupBox1.Controls.Add(this.radioButtonTXGeLiDu);
            this.groupBox1.Controls.Add(this.radioButtonRXGeLiDu);
            this.groupBox1.Controls.Add(this.comboBoxCtrlCodeHistory);
            this.groupBox1.Controls.Add(this.radioButtonRXChaSun);
            this.groupBox1.Controls.Add(this.buttonSendControlCode);
            this.groupBox1.Controls.Add(this.radioButtonSwitchSpeed);
            this.groupBox1.Controls.Add(this.radioButtonTXChaSun);
            this.groupBox1.Controls.Add(this.radioButtonRXPowerResist);
            this.groupBox1.Location = new System.Drawing.Point(28, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 250);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "开关控制";
            // 
            // radioButtonJiaju
            // 
            this.radioButtonJiaju.AutoSize = true;
            this.radioButtonJiaju.Location = new System.Drawing.Point(120, 30);
            this.radioButtonJiaju.Name = "radioButtonJiaju";
            this.radioButtonJiaju.Size = new System.Drawing.Size(71, 16);
            this.radioButtonJiaju.TabIndex = 11;
            this.radioButtonJiaju.TabStop = true;
            this.radioButtonJiaju.Text = "夹具确认";
            this.radioButtonJiaju.UseVisualStyleBackColor = true;
            this.radioButtonJiaju.CheckedChanged += new System.EventHandler(this.radioButtonJiaju_CheckedChanged);
            // 
            // radioButtonZeroState
            // 
            this.radioButtonZeroState.AutoSize = true;
            this.radioButtonZeroState.Location = new System.Drawing.Point(26, 30);
            this.radioButtonZeroState.Name = "radioButtonZeroState";
            this.radioButtonZeroState.Size = new System.Drawing.Size(47, 16);
            this.radioButtonZeroState.TabIndex = 10;
            this.radioButtonZeroState.TabStop = true;
            this.radioButtonZeroState.Text = "初始";
            this.radioButtonZeroState.UseVisualStyleBackColor = true;
            this.radioButtonZeroState.CheckedChanged += new System.EventHandler(this.radioButtonZeroState_CheckedChanged);
            // 
            // radioButtonTXPowerResist
            // 
            this.radioButtonTXPowerResist.AutoSize = true;
            this.radioButtonTXPowerResist.Location = new System.Drawing.Point(120, 125);
            this.radioButtonTXPowerResist.Name = "radioButtonTXPowerResist";
            this.radioButtonTXPowerResist.Size = new System.Drawing.Size(83, 16);
            this.radioButtonTXPowerResist.TabIndex = 9;
            this.radioButtonTXPowerResist.TabStop = true;
            this.radioButtonTXPowerResist.Text = "TX通过功率";
            this.radioButtonTXPowerResist.UseVisualStyleBackColor = true;
            this.radioButtonTXPowerResist.CheckedChanged += new System.EventHandler(this.radioButtonTXPowerResist_CheckedChanged);
            // 
            // radioButtonTXGeLiDu
            // 
            this.radioButtonTXGeLiDu.AutoSize = true;
            this.radioButtonTXGeLiDu.Location = new System.Drawing.Point(120, 93);
            this.radioButtonTXGeLiDu.Name = "radioButtonTXGeLiDu";
            this.radioButtonTXGeLiDu.Size = new System.Drawing.Size(71, 16);
            this.radioButtonTXGeLiDu.TabIndex = 8;
            this.radioButtonTXGeLiDu.TabStop = true;
            this.radioButtonTXGeLiDu.Text = "TX隔离度";
            this.radioButtonTXGeLiDu.UseVisualStyleBackColor = true;
            this.radioButtonTXGeLiDu.CheckedChanged += new System.EventHandler(this.radioButtonTXGeLiDu_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonSetPWM);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxMValue);
            this.groupBox2.Controls.Add(this.comboBoxNValue);
            this.groupBox2.Location = new System.Drawing.Point(359, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(136, 250);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PWM";
            // 
            // buttonSetPWM
            // 
            this.buttonSetPWM.Location = new System.Drawing.Point(34, 177);
            this.buttonSetPWM.Name = "buttonSetPWM";
            this.buttonSetPWM.Size = new System.Drawing.Size(75, 23);
            this.buttonSetPWM.TabIndex = 4;
            this.buttonSetPWM.Text = "设置";
            this.buttonSetPWM.UseVisualStyleBackColor = true;
            this.buttonSetPWM.Click += new System.EventHandler(this.buttonSetPWM_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "占空";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "周期";
            // 
            // comboBoxMValue
            // 
            this.comboBoxMValue.FormattingEnabled = true;
            this.comboBoxMValue.Location = new System.Drawing.Point(30, 129);
            this.comboBoxMValue.Name = "comboBoxMValue";
            this.comboBoxMValue.Size = new System.Drawing.Size(80, 20);
            this.comboBoxMValue.TabIndex = 1;
            // 
            // comboBoxNValue
            // 
            this.comboBoxNValue.FormattingEnabled = true;
            this.comboBoxNValue.Location = new System.Drawing.Point(30, 50);
            this.comboBoxNValue.Name = "comboBoxNValue";
            this.comboBoxNValue.Size = new System.Drawing.Size(79, 20);
            this.comboBoxNValue.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxOsc);
            this.groupBox3.Controls.Add(this.checkBoxAV);
            this.groupBox3.Location = new System.Drawing.Point(359, 326);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 163);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "接收设备";
            // 
            // FormTestTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 501);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboBoxCMDHistory);
            this.Controls.Add(this.textBoxReceive);
            this.Controls.Add(this.buttonSendCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormTestTool";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "测试工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTestTool_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSendCommand;
        private System.Windows.Forms.TextBox textBoxReceive;
        private System.Windows.Forms.RadioButton radioButtonRXChaSun;
        private System.Windows.Forms.ComboBox comboBoxCMDHistory;
        private System.Windows.Forms.Button buttonSendControlCode;
        private System.Windows.Forms.RadioButton radioButtonSwitchSpeed;
        private System.Windows.Forms.RadioButton radioButtonRXPowerResist;
        private System.Windows.Forms.RadioButton radioButtonRXGeLiDu;
        private System.Windows.Forms.RadioButton radioButtonTXChaSun;
        private System.Windows.Forms.ComboBox comboBoxCtrlCodeHistory;
        private System.Windows.Forms.CheckBox checkBoxAV;
        private System.Windows.Forms.CheckBox checkBoxOsc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMValue;
        private System.Windows.Forms.ComboBox comboBoxNValue;
        private System.Windows.Forms.Button buttonSetPWM;
        private System.Windows.Forms.RadioButton radioButtonTXGeLiDu;
        private System.Windows.Forms.RadioButton radioButtonJiaju;
        private System.Windows.Forms.RadioButton radioButtonZeroState;
        private System.Windows.Forms.RadioButton radioButtonTXPowerResist;
    }
}

