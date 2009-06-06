using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace Yaguang.VJK3G.GUI
{

    using IO;
    using Properties;
    using Test;
    using Instrument;



    public partial class MainForm : Form
    {

        private IStringStream avStream;
        private IStringStream oscStream;


        private IList<ITestItem> TestItems;

        private IList<ITestItem> _recheckItems = new List<ITestItem>();

        public MainForm()
        {
            InitializeComponent();

            this.TestItems = new List<ITestItem>();
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UpdateControlState();
        }


        private TestItemOnAVWithModifier TXChaSun;
        private ITestItem TXZhuBo;
        private TestItemOnAVWithModifier RXChaSun;
        private ITestItem RXZhuBo;
        private TestItemOnAVWithModifier RXGeLiDu;
        private TestItemOnAVWithModifier TXGeLiDu;
        private TestItemOnAVWithModifier TXPowerResist;
        private TestItemOnAVWithModifier RXPowerResist;
        private ITestItem SwitchSpeed;



        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OptionsForm op = new OptionsForm();
            op.ShowDialog();

        }



        private void InitTestItem()
        {


            TXChaSun = new TestItemOnAVWithModifier(SwitchSetting.TXChaSunZhuBo,
                Settings.Default.chOfTxChaSun.ToString());
            TXChaSun.DataPicker = Helper.Max;
            TXChaSun.Comparer = Helper.BEThan;
            TXChaSun.Threshold = float.Parse(Settings.Default.ThreshChaSun);
            TXChaSun.Name = "TX插损";
            TXChaSun.Modifier = Helper.MinusBaseForTxChaSun;

            TXZhuBo = new TestItemOnAV(SwitchSetting.TXChaSunZhuBo,
                Settings.Default.chOfTXZhuBo.ToString());
            TXZhuBo.DataPicker = Helper.Max;
            TXZhuBo.Comparer = Helper.LEThan;
            TXZhuBo.Threshold = float.Parse(Settings.Default.ThreshZhuBo);
            TXZhuBo.Name = "TX驻波";


            RXChaSun = new TestItemOnAVWithModifier(SwitchSetting.RXChaSunZhuBo,
                Settings.Default.chOfRxChaSun.ToString());
            RXChaSun.DataPicker = Helper.Max;
            RXChaSun.Comparer = Helper.BEThan;
            RXChaSun.Threshold = float.Parse(Settings.Default.ThreshChaSun);
            RXChaSun.Name = "RX插损";
            RXChaSun.Modifier = Helper.MinusBaseForTxChaSun;

            RXZhuBo = new TestItemOnAV(SwitchSetting.RXChaSunZhuBo,
                Settings.Default.chOfTXZhuBo.ToString());
            RXZhuBo.DataPicker = Helper.Max;
            RXZhuBo.Comparer = Helper.LEThan;
            RXZhuBo.Threshold = float.Parse(Settings.Default.ThreshZhuBo);
            RXZhuBo.Name = "RX驻波";

            RXGeLiDu = new TestItemOnAVWithModifier(SwitchSetting.RXGeLiDu,
                Settings.Default.chOfRxGeLiDu.ToString());
            RXGeLiDu.DataPicker = Helper.Min;
            RXGeLiDu.Comparer = Helper.AbsBEThan;
            RXGeLiDu.Threshold = float.Parse(Settings.Default.ThreshRxGeLiDu);
            RXGeLiDu.Name = "RX隔离度";
            RXGeLiDu.Modifier = Helper.MinusBaseForTxChaSun;

            TXGeLiDu = new TestItemOnAVWithModifier(SwitchSetting.TXGeLiDu,
                Settings.Default.chOfTxGeLiDu.ToString());
            TXGeLiDu.DataPicker = Helper.Min;
            TXGeLiDu.Comparer = Helper.AbsBEThan;
            TXGeLiDu.Threshold = float.Parse(Settings.Default.ThreshGeLiDu);
            TXGeLiDu.Name = Settings.Default.LabelTXGeLiDu;
            TXGeLiDu.Modifier = Helper.MinusBaseForTxChaSun;

            TXPowerResist = new TestItemOnAVWithModifier(SwitchSetting.TXPowerResist,
                Settings.Default.chOfTxNaiGongLu.ToString());
            TXPowerResist.DataPicker = Helper.PowerResist;
            TXPowerResist.Comparer = Helper.BEThan;
            TXPowerResist.Modifier = Helper.PlusAbsBaseForTXPowerResist;
            TXPowerResist.Threshold = float.Parse(Settings.Default.ThreshPowerResist);
            TXPowerResist.Name = "TX耐功率";

            RXPowerResist = new TestItemOnAVWithModifier(SwitchSetting.RXPowerResist,
                Settings.Default.chOfRxNaiGongLu.ToString());
            RXPowerResist.DataPicker = Helper.PowerResist;
            RXPowerResist.Comparer = Helper.BEThan;
            RXPowerResist.Modifier = Helper.PlusAbsBaseForRXPowerResist;
            RXPowerResist.Threshold = float.Parse(Settings.Default.ThreshPowerResist);
            RXPowerResist.Name = "RX耐功率";



            SwitchSpeed = new TestItemOnOSC(SwitchSetting.SwitchSpeed);
            SwitchSpeed.DataPicker = Helper.Max;
            SwitchSpeed.Comparer = Helper.LEThan;
            SwitchSpeed.Threshold = float.Parse(Settings.Default.ThreshSwitchSpeed);
            SwitchSpeed.Name = Settings.Default.LabelSwitchSpeed;


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (InitSCAndGPIB())
            {
                this.InitDevice();

                this.timer1.Enabled = true;
                this.timer2.Enabled = true;
                SwitchController.Default.SwitchChanged += new EventHandler(sc_SwitchChanged);
            }

        }




        void sc_SwitchChanged(object sender, EventArgs e)
        {
            int code = SwitchController.Default.ControlCodes[SwitchController.Default.CurrentSwitch];
            if (InvokeRequired)
            {
                Action<int> d = this.UpdateControlCodeStatusLabel;
                this.Invoke(d, code);
            }
            else
            {
                this.UpdateControlCodeStatusLabel(code);
            }


        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void buttonDeviceAddress_Click(object sender, EventArgs e)
        {
            DeviceAddressForm device = new DeviceAddressForm();
            device.textBoxAVAddress.Text = Settings.Default.AVGPIBAddress;
            device.textBoxOSCAddress.Text = Settings.Default.OSCGPIBAddress;
            device.ShowDialog();
            if (device.DialogResult == DialogResult.OK)
            {
                Settings.Default.AVGPIBAddress = device.textBoxAVAddress.Text;
                Settings.Default.OSCGPIBAddress = device.textBoxOSCAddress.Text;
            }
        }



        private void ToolStripMenuItemOptions_Click(object sender, EventArgs e)
        {
            var form = new OptionsForm();
            form.ShowDialog(this);
        }


        private bool OpenSwitch(int id)
        {
            try
            {
                SwitchController.Open(id);
                SwitchController.Default.Initialize();
            }
            catch (CustomException e)
            {
                return false;
            }

            return true;
        }

        private bool OpenAV()
        {
            try
            {
                this.avStream = GPIB.Open(Settings.Default.AVGPIBAddress);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.avStream = null;
                return false;
            }

            return true;
        }


        private bool OpenOSC()
        {
            try
            {
                this.oscStream = GPIB.Open(Settings.Default.OSCGPIBAddress);
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.oscStream = null;
                return false;
            }

            return true;
        }

        private void EnableStartButton(bool enable)
        {
            this.toolStripButtonStart.Enabled = Enabled;
        }

        private void EnableStopButton(bool enable)
        {
            this.toolStripButtonStop.Enabled = Enabled;
        }

        private void EnableTestToolMenuItem(bool enable)
        {
            this.ToolStripMenuItemTestTool.Enabled = enable;
        }

        private void InitDevice()
        {
            NetworkAnalyzer.Default.WorkerStream = this.avStream;
            NetworkAnalyzer.Default.Init();

            if (Settings.Default.AutoRecall)
            {
                NetworkAnalyzer.Default.Recall(Settings.Default.AVPresetScript);
            }

            if (this.oscStream != null)
            {
                Oscillograph.Default.WorkerStream = this.oscStream;

                Oscillograph.Default.Init();
            }


            this.EnableTestToolMenuItem(true);
        }

        public bool InitSCAndGPIB()
        {
            if (!this.OpenSwitch(Settings.Default.SwitchControlAddr))
            {
                MessageBox.Show("打开开关控制器错误，请检查设备后重新启动程序");
                goto FAIL;
            }

            if (!this.OpenAV())
            {
                MessageBox.Show("打开矢网错误，请检查设备后重新启动程序");
                goto FAIL;
            }

            if (Settings.Default.ItemSwitchSpeedChecked)
            {
                if (!this.OpenOSC())
                {
                    MessageBox.Show("打开示波器错误，请检查设备后重新启动程序");
                    goto FAIL;
                }
            }

            this.EnableStartButton(true);
            return true;

        FAIL:
            return false;

        }


        private void SetTCStatusLabel(ToolStripStatusLabel lbl, int state, string txt)
        {
            lbl.Text = txt;
            lbl.BackColor = state == 0 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }


        private void UpdateTCState(int tcID, int state)
        {
            string txt = "TC" + tcID.ToString() + ": " + state.ToString();
            SetTCStatusLabel(tcID == 1 ? this.toolStripStatusLabelTC1 : this.toolStripStatusLabelTC2, state, txt);
        }

        private void UpdateControlState()
        {
            this.toolStripButtonStop.Enabled = this.backgroundWorker1.IsBusy;
        }

        private void UpdateControlCodeStatusLabel(int code)
        {
            string strCode = code.ToString("X4");
            string txt = "SWITCH: " + strCode;

            this.toolStripStatusLabelSWITCH.Text = txt;
        }

        private void Switch_Changed(object sender, EventArgs e)
        {


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.UpdateTCState(1, SwitchController.Default.TC1);
            this.UpdateTCState(2, SwitchController.Default.TC2);
        }

        private FormTestTool testForm;

        private void ToolStripMenuItemTestTool_Click(object sender, EventArgs e)
        {
            if (this.testForm == null)
            {
                this.testForm = new FormTestTool();
            }

            this.testForm.ShowDialog(this);
        }



        private void toolStripButtonStart_Click(object sender, EventArgs e)
        {

            this.InitTestItem();

            this.BuildUpTestItems();

            Helper.SetupMarks();

            if (!this.backgroundWorker1.IsBusy)
            {
                this.backgroundWorker1.RunWorkerAsync();
            }

            this._run = true;

            this.UpdateControlState();
        }

        private void RunItem(ITestItem item, bool report)
        {
            if (report)
            {
                this.backgroundWorker1.ReportProgress(0, item);
            }

            item.Setup();

            if (report)
            {
                this.backgroundWorker1.ReportProgress(50, item);
            }

            item.Run();

            if (report)
            {
                this.backgroundWorker1.ReportProgress(100, item);
            }

        }

        private bool _run = true;

        private IList<ITestItem> _results = new List<ITestItem>();

        bool RecheckItems()
        {
            foreach (ITestItem item in this._recheckItems)
            {
                this.RunItem(item, false);
                if (!item.Passed)
                {
                    return false;
                }
            }

            return true;
        }

        DialogResult AskForChoice(string text, string caption)
        {
            if (InvokeRequired)
            {
                Func<string, string, DialogResult> d =
                    (t, c) => MessageBox.Show(this,
                                      t,
                                      c,
                                      MessageBoxButtons.OKCancel,
                                      MessageBoxIcon.Question);
                return (DialogResult)this.Invoke(d, text, caption);
            }
            else
            {
                return MessageBox.Show(this, text, caption, MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Information);
            }
        }



        void ShowToolForm()
        {
            if (InvokeRequired)
            {
                Action action = () => new FormToolDetect().ShowDialog(this);
                this.Invoke(action);
            }
            else
            {
                new FormToolDetect().ShowDialog(this);
            }

        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string msg = "请更换元件";
            bool round1passed = true;
            while (true)
            {
                DialogResult result = AskForChoice(msg, "等待指令");

                if (result != DialogResult.OK)
                {
                    return;
                }

                _results.Clear();

                if (_run)
                {
                    ShowToolForm();
                }

                round1passed = true;
                foreach (ITestItem item in this.TestItems)
                {
                    if (!_run)
                    {
                        break;
                    }

                    this.RunItem(item, true);
                    _results.Add(item);

                    if (!item.Passed)
                    {
                        msg = "元件不合格. 请更换下一个元件";
                        round1passed = false;
                        break;
                    }

                }

                if (round1passed)
                {
                    if (!RecheckItems())
                    {
                        msg = "元件在耐功率测试后，插损不合格. 请更换下一个元件";
                    }
                    else
                    {
                        msg = "元件合格. 请更换下一个元件";
                    }
                }

                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;
                this.backgroundWorker1.ReportProgress(100, null);
            }


        }


        private void BuildUpTestItems()
        {
            this.TestItems.Clear();
            this._recheckItems.Clear();

            if (Settings.Default.ItemTXChaSunChecked)
            {
                this.TestItems.Add(TXChaSun);
            }

            if (Settings.Default.ItemTXZhuBoChecked)
            {
                this.TestItems.Add(TXZhuBo);
            }

            if (Settings.Default.ItemRXChaSunChecked)
            {
                this.TestItems.Add(RXChaSun);
            }

            if (Settings.Default.ItemRXZhuBoChecked)
            {
                this.TestItems.Add(RXZhuBo);
            }

            if (Settings.Default.ItemRXGeLiDuChecked)
            {
                this.TestItems.Add(RXGeLiDu);
            }

            if (Settings.Default.ItemTXGeLiDuChecked)
            {
                this.TestItems.Add(TXGeLiDu);
            }

            if (Settings.Default.ItemTXNaiGongLu)
            {
                this.TestItems.Add(TXPowerResist);
            }

            if (Settings.Default.ItemRXNaiGongLuChecked)
            {
                this.TestItems.Add(RXPowerResist);
            }

            if (Settings.Default.ItemRXNaiGongLuChecked
                || Settings.Default.ItemTXNaiGongLu)
            {
                this._recheckItems.Add(RXChaSun);
                this._recheckItems.Add(TXChaSun);
            }

            if (Settings.Default.ItemSwitchSpeedChecked)
            {
                this.TestItems.Add(SwitchSpeed);
            }


        }



        private void UpdateCurrentItemDisplay(int percentage, ITestItem item)
        {
            string txt = item.Name;
            Color c = new Color();

            if (percentage == 0)
            {
                txt += "测试中......";
                c = System.Drawing.SystemColors.ControlText;
            }
            else if (percentage == 100)
            {
                txt += ": " + item.TheValue.ToString();
                c = item.Passed ? System.Drawing.SystemColors.ControlText : Helper.WarningColor();

            }

            this.labelCurItemName.Text = txt;

            this.labelCurItemName.ForeColor = c;

        }

        private bool RoundPassed(IList<ITestItem> round)
        {

            foreach (var item in round)
            {
                if (!item.Passed)
                {
                    return false;
                }
            }

            return true;
        }

        private ListViewItem BuildListViewItem()
        {
            ListViewItem.ListViewSubItem subNo = new ListViewItem.ListViewSubItem();
            subNo.Text = (this.listView1.Items.Count + 1).ToString();

            ListViewItem.ListViewSubItem subRxChaSun = new ListViewItem.ListViewSubItem();
            subRxChaSun.Name = Settings.Default.LabelRXChaSun;

            ListViewItem.ListViewSubItem subRxZhuBo = new ListViewItem.ListViewSubItem();
            subRxZhuBo.Name = Settings.Default.LabelRXZhuBo;

            ListViewItem.ListViewSubItem subRxGeLiDu = new ListViewItem.ListViewSubItem();
            subRxGeLiDu.Name = Settings.Default.LabelRXGeLiDu;

            ListViewItem.ListViewSubItem subRxPowerResist = new ListViewItem.ListViewSubItem();
            subRxPowerResist.Name = Settings.Default.LabelRxNaiGongLu;

            ListViewItem.ListViewSubItem subTxChaSun = new ListViewItem.ListViewSubItem();
            subTxChaSun.Name = Settings.Default.LabelTXChaSun;

            ListViewItem.ListViewSubItem subTxZhuBo = new ListViewItem.ListViewSubItem();
            subTxZhuBo.Name = Settings.Default.LabelTXZhuBo;

            ListViewItem.ListViewSubItem subTxGeLiDu = new ListViewItem.ListViewSubItem();
            subTxGeLiDu.Name = Settings.Default.LabelTXGeLiDu;

            ListViewItem.ListViewSubItem subTxPowerResist = new ListViewItem.ListViewSubItem();
            subTxPowerResist.Name = Settings.Default.LabelTxNaiGongLu;

            ListViewItem.ListViewSubItem subSwitchSpeed = new ListViewItem.ListViewSubItem();
            subSwitchSpeed.Name = Settings.Default.LabelSwitchSpeed;

            ListViewItem item = new ListViewItem
                (new ListViewItem.ListViewSubItem[]
                {
                    subNo,
                    subRxChaSun,
                    subRxZhuBo,
                    subRxGeLiDu,
                    subRxPowerResist,
                    subTxChaSun,
                    subTxZhuBo,
                    subTxGeLiDu,
                    subTxPowerResist,
                    subSwitchSpeed
                }, -1);

            foreach (ITestItem t in this._results)
            {
                item.SubItems[t.Name].Text = t.TheValue.ToString(Helper.FloatFormat);
            }

            if (!RoundPassed(this.TestItems))
            {
                item.ForeColor = Helper.WarningColor();
            }



            return item;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null)// one round is done
            {
                ListViewItem item = this.BuildListViewItem();
                this.listView1.Items.Add(item);
                item.EnsureVisible();
            }
            else
            {
                ITestItem item = e.UserState as ITestItem;

                if (e.ProgressPercentage == 0)
                {
                    //this.UpdateCurrentItemDisplay(e.ProgressPercentage, item);

                }
                else if (e.ProgressPercentage == 50)
                {
                }
                else if (e.ProgressPercentage == 100)
                {
                    this.UpdateCurrentItemDisplay(e.ProgressPercentage, item);
                }


            }

        }

        private void toolStripButtonCali_Click(object sender, EventArgs e)
        {

        }

        private void listView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //this.listView1.Columns[e.ColumnIndex].
            //Settings.Default[ColRXChaSunWidth
        }

        private void 关于VJK3GToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox abt = new AboutBox();
            abt.ShowDialog(this);
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            this._run = false;
        }


        private void SaveTestHistory()
        {
            if (this.saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                using (System.IO.StreamWriter sw =
                    new System.IO.StreamWriter(this.saveFileDialog1.OpenFile(), Encoding.Unicode))
                {
                    string s = "";

                    for (int i = 0; i < this.listView1.Columns.Count; i++)
                    {
                        s += this.listView1.Columns[i].Text + "\t";
                    }

                    sw.WriteLine(s);

                    for (int i = 0; i < this.listView1.Items.Count; i++)
                    {
                        s = "";
                        foreach (ListViewItem.ListViewSubItem sub in this.listView1.Items[i].SubItems)
                        {
                            s += sub.Text + "\t";
                        }

                        sw.WriteLine(s);
                    }

                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveTestHistory();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Dispose();
            if (Properties.Settings.Default.FirstRun)
            {

                MessageBox.Show("第一次运行程序， 请先配置频点并校正系统");
                OptionsForm f = new OptionsForm();
                f.ShowDialog(this);
                Properties.Settings.Default.FirstRun = false;
            }


        }


    }
}