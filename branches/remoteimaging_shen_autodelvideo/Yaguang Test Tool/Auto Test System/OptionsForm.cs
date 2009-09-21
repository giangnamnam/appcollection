using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Yaguang.VJK3G.GUI
{

    using Properties;
    using Instrument;

    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();

            UpdateControlState();

            Settings.Default.PropertyChanged += new PropertyChangedEventHandler(Default_PropertyChanged);
        }

        private void UpdateControlState()
        {
            this.buttonCalibrateTxChaSun.Enabled = Instrument.NetworkAnalyzer.Default.WorkerStream != null;
        }

        private bool _propertyChanged = false;

        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this._propertyChanged = true;
        }


        public Instrument.NetworkAnalyzer AV
        {
            get;
            set;
        }




        private void buttonOK_Click(object sender, EventArgs e)
        {

            this.Close();

            if (this._propertyChanged)
            {
                Settings.Default.Save();
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();

            if (this._propertyChanged)
            {
                Settings.Default.Reload();
            }


        }



        private void buttonCalcTxChaSun_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Helper.SetupMarks();
                Test.TestItemBase item;
                item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.TxChaSun, false);
                item.Setup();
                item.Run();
                Settings.Default.STDTXChaSun1 = item.OriginalValues[0];
                Settings.Default.STDTXChaSun2 = item.OriginalValues[1];
                Settings.Default.STDTXChaSun3 = item.OriginalValues[2];
            }
            finally
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
                this.Cursor = Cursors.Default;
            }

        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            this._propertyChanged = false;
        }

        private void buttonRevertToDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "所有设定值都将重置为缺省值, 确定要继续吗?", "恢复缺省值",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
            {
                Settings.Default.Reset();
            }

        }

        private void ShowAdvancedOptions(bool show)
        {
            this.labelAVGPIBAddr.Visible = show;
            this.textBoxAVGPIBAddr.Visible = show;


            this.labelSCAddr.Visible = show;
            this.numericUpDownSCAddr.Visible = show;

            this.TCStateGroup.Visible = show;
            this.CtrlCodeGroup.Visible = show;

        }

        private void buttonAdvanced_Click(object sender, EventArgs e)
        {

        }

        private void advancedOptions_CheckedChanged(object sender, EventArgs e)
        {
            if (advancedOptions.Checked)
            {
                MessageBox.Show(this, "不要轻易改变高级选项", "显示高级选项",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.ShowAdvancedOptions(advancedOptions.Checked);
        }

        private void buttonModifyThreshold_Click(object sender, EventArgs e)
        {
            FormAskForPWD form = new FormAskForPWD();

            DialogResult result = form.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                if (form.textBoxPWD.Text == Properties.Settings.Default.PassWord)
                {
                    textBoxChaSunThresh.Enabled = true;
                    textBoxPowerResist.Enabled = true;
                    textBoxTxGeLiDuThresh.Enabled = true;
                    textBoxSTDRxGeLiDU.Enabled = true;
                }
                else
                {
                    MessageBox.Show("密码无效", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void checkBoxSysPowerCalib_CheckedChanged(object sender, EventArgs e)
        {
            SwitchSetting ctrlCode = this.checkBoxSysPowerCalib.Checked ?
                SwitchSetting.SysPowerCalib : SwitchSetting.Start;

            SwitchController.Default.CurrentSwitch = ctrlCode;
        }

        private void buttonCalTxPower_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Helper.SetupMarks();
                Test.TestItemBase item;
                item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.TxPower, false);
                item.Setup();
                item.Run();
                Settings.Default.STDTXNaiGongLu = item.OriginalValues[0];
            }
            finally
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonCalRxChaSun_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Helper.SetupMarks();
                Test.TestItemBase item;
                item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.RxChaSun, false);
                item.Setup();
                item.Run();
                Settings.Default.STDRXChaSun1 = item.OriginalValues[0];
                Settings.Default.STDRXChaSun2 = item.OriginalValues[1];
                Settings.Default.STDRXChaSun3 = item.OriginalValues[2];

            }
            finally
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
                this.Cursor = Cursors.Default;
            }
        }

        private void textBoxTxGeLiDuThresh_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxSTDRxGeLiDU_TextChanged(object sender, EventArgs e)
        {

        }

    }
}