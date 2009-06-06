using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Yaguang.VJK3G.Instrument;
using System.Collections;

namespace Yaguang.VJK3G.GUI
{
    public partial class FormTestTool : Form
    {
        public FormTestTool()
        {
            InitializeComponent();
            UpdateControlsState();
        }

        private void UpdateControlsState()
        {
            this.checkBoxOsc.Checked = Oscillograph.Default.WorkerStream != null;
            this.checkBoxOsc.Enabled = Oscillograph.Default.WorkerStream != null;
        }


        private void radioButtonRXChaSun_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                SwitchController.Default.WriteControlCode(0);
                SwitchController.Default.CurrentSwitch = SwitchSetting.RXChaSunZhuBo;
            }
        }

        private void radioButtonTXChaSun_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                SwitchController.Default.WriteControlCode(0);
                SwitchController.Default.CurrentSwitch = SwitchSetting.TXChaSunZhuBo;
            }

        }

        private void radioButtonRXGeLiDu_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                SwitchController.Default.WriteControlCode(0);
                SwitchController.Default.CurrentSwitch = SwitchSetting.RXGeLiDu;
            }

        }



        private void radioButtonSwitchSpeed_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                SwitchController.Default.WriteControlCode(0);
                SwitchController.Default.CurrentSwitch = SwitchSetting.SwitchSpeed;
            }

        }

        private void buttonSendControlCode_Click(object sender, EventArgs e)
        {
            if (this.comboBoxCtrlCodeHistory.Text == "")
            {
                return;
            }

            string text = this.comboBoxCtrlCodeHistory.Text;
            int Code = int.Parse(text);
            if (!this.comboBoxCtrlCodeHistory.Items.Contains(text))
            {
                this.comboBoxCtrlCodeHistory.Items.Add(text);
            }

            int OldCode = SwitchController.Default.ReadControlCode();

            SwitchController.Default.WriteControlCode(Code);

            int NewCode = SwitchController.Default.ReadControlCode();

        }

        private void buttonSetPWM_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.comboBoxMValue.Text)
                || string.IsNullOrEmpty(this.comboBoxNValue.Text))
            {
                return;
            }

            string textN = this.comboBoxNValue.Text;
            string textM = this.comboBoxMValue.Text;

            int N = int.Parse(textN);
            int M = int.Parse(textM);

            SwitchController.Default.PWMPeriod = N;
            SwitchController.Default.PWMLowTTL = M;

            if (!this.comboBoxNValue.Items.Contains(textN))
            {
                this.comboBoxNValue.Items.Add(textN);
            }

            if (!this.comboBoxMValue.Items.Contains(textM))
            {
                this.comboBoxMValue.Items.Add(textM);
            }

        }

        private void ReadData()
        {
            string RetTextFromAV = null;
            string RetTextFromOsc = null;
            string RetText = "";

            if (this.checkBoxAV.Checked)
            {
                //RetTextFromAV = this.av.ReadString();
                RetTextFromAV = RetTextFromAV.Insert(0, "AV Returns: \n");
            }

            if (this.checkBoxOsc.Checked)
            {
                RetTextFromOsc = Oscillograph.Default.ReadString();
                RetTextFromOsc = RetTextFromOsc.Insert(0, "OSC Returns: \n");
            }

            if (RetTextFromAV != null)
            {
                RetText += "AV Returns: \n" + RetTextFromAV;

            }

            if (RetTextFromOsc != null)
            {
                RetText += "\nOSC Returns: \n" + RetTextFromOsc;
            }

            this.textBoxReceive.Text = RetText;


        }

        private void buttonSendCommand_Click(object sender, EventArgs e)
        {
            string text = this.comboBoxCMDHistory.Text;
            bool IsQuery = text.EndsWith("?");
            string RetTextFromAV = null;
            string RetTextFromOsc = null;
            string RetText = "";

            if (this.checkBoxAV.Checked)
            {
                //this.av.WriteString(text);
                if (IsQuery)
                {
                    RetTextFromAV = NetworkAnalyzer.Default.Query(text);

                }
                else
                {
                    NetworkAnalyzer.Default.ExecuteCommand(text);
                }
            }

            if (this.checkBoxOsc.Checked)
            {

                if (IsQuery)
                {
                    RetTextFromOsc = Oscillograph.Default.Query(text);
                }
                else
                {
                    Oscillograph.Default.ExecuteCommand(text);
                }
            }

            if (RetTextFromAV != null)
            {
                RetText += "AV Returns: \n" + RetTextFromAV;

            }

            if (RetTextFromOsc != null)
            {
                RetText += "\nOSC Returns: \n" + RetTextFromOsc;
            }

            this.textBoxReceive.Text = RetText;

            if (!this.comboBoxCMDHistory.Items.Contains(text))
            {
                this.comboBoxCMDHistory.Items.Add(text);
            }
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            this.ReadData();

        }

        private void radioButtonZeroState_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonZeroState.Checked)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
            }
            

        }

        private void radioButtonJiaju_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonJiaju.Checked)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.ToolConfirm;
            }

        }

        private void radioButtonTXGeLiDu_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonTXGeLiDu.Checked)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.TXGeLiDu;
            }

        }

        private void radioButtonRXPowerResist_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonRXPowerResist.Checked)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.RXPowerResist;
            }

        }

        private void radioButtonTXPowerResist_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButtonTXPowerResist.Checked)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.TXPowerResist;
            }

        }

        private void FormTestTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
        }

    }
}