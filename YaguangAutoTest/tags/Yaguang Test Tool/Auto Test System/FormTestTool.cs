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


        private void ReadData()
        {
            string RetTextFromAV = null;
            string RetText = "";

            if (this.checkBoxAV.Checked)
            {
                //RetTextFromAV = this.av.ReadString();
                RetTextFromAV = RetTextFromAV.Insert(0, "AV Returns: \n");
            }


            if (RetTextFromAV != null)
            {
                RetText += "AV Returns: \n" + RetTextFromAV;

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