using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;


namespace Yaguang.VJK3G.GUI
{

    using IO;
    using Properties;
    using Test;
    using Instrument;



    public partial class MainForm : Form
    {

        private IStringStream avStream;


        private IList<TestItemBase> TestItems;

        private IList<TestItemBase> _recheckItems = new List<TestItemBase>();

        public MainForm()
        {
            InitializeComponent();

            this.TestItems = new List<TestItemBase>();
            this.backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UpdateControlState();
        }


        private TestItemBase TXChaSun;
        private TestItemBase RXChaSun;
        private TestItemBase RXGeLiDu;
        private TestItemBase TXPowerResist;

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
            TXChaSun = TestItemFactory.CreateTestItem(TestItemType.TxChaSun, true);
            RXChaSun = TestItemFactory.CreateTestItem(TestItemType.RxChaSun, true);
            RXGeLiDu = TestItemFactory.CreateTestItem(TestItemType.RxGeLi, true);
            TXPowerResist = TestItemFactory.CreateTestItem(TestItemType.TxPower, true);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (InitSCAndGPIB())
            {
                this.InitDevice();

                if (!Program.Debug)
                {
                    this.timer1.Enabled = true;
                    this.timer2.Enabled = true;
                }


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
                if (Program.Debug)
                {
                    this.avStream = new AVStub();
                }
                else
                {
                    this.avStream = GPIB.Open(Settings.Default.AVGPIBAddress);
                }
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                this.avStream = null;
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


            this.EnableTestToolMenuItem(true);
        }

        public bool InitSCAndGPIB()
        {
            if (!this.OpenSwitch(Settings.Default.SwitchControlAddr))
            {
                MessageBox.Show("打开开关控制器错误，请检查设备后重新启动程序");
                if (!Program.Debug)
                {
                    goto FAIL;
                }

            }

            if (!this.OpenAV())
            {
                MessageBox.Show("打开矢网错误，请检查设备后重新启动程序");
                if (!Program.Debug)
                {
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

        private void RunItem(TestItemBase item, bool report)
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

            System.Diagnostics.Debug.WriteLine(string.Format("the value:{0}", item.TheValue));

            if (report)
            {
                this.backgroundWorker1.ReportProgress(100, item);
            }

        }

        private bool _run = true;

        private IList<TestItemBase> _results = new List<TestItemBase>();

        bool RecheckItems()
        {
            foreach (TestItemBase item in this._recheckItems)
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

                if (_run && !Program.Debug && Properties.Settings.Default.waitForToolConfirm)
                {
                    ShowToolForm();
                }

                round1passed = true;
                foreach (TestItemBase item in this.TestItems)
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

            if (Settings.Default.ItemRXChaSunChecked)
            {
                this.TestItems.Add(RXChaSun);
            }

            if (Settings.Default.ItemRXGeLiDuChecked)
            {
                this.TestItems.Add(RXGeLiDu);
            }

            if (Settings.Default.ItemTXNaiGongLu)
            {
                this.TestItems.Add(TXPowerResist);
            }

            if (Settings.Default.ItemRXNaiGongLuChecked
                || Settings.Default.ItemTXNaiGongLu)
            {
                this._recheckItems.Add(TestItemFactory.CreateTestItem(TestItemType.TxChaSun, true));
                this._recheckItems.Add(TestItemFactory.CreateTestItem(TestItemType.RxChaSun, true));
            }


        }



        private void UpdateCurrentItemDisplay(int percentage, TestItemBase item)
        {
            string txt = item.Name;
            Color c = Color.Empty;

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

            Debug.WriteLine(string.Format("the value on GUI:{0}", txt));

            this.labelCurItemName.Text = txt;
            this.labelCurItemName.ForeColor = c;


        }

        private bool RoundPassed(IList<TestItemBase> round)
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

            ListViewItem.ListViewSubItem subRxGeLiDu = new ListViewItem.ListViewSubItem();
            subRxGeLiDu.Name = Settings.Default.LabelRXGeLiDu;

            ListViewItem.ListViewSubItem subTxChaSun = new ListViewItem.ListViewSubItem();
            subTxChaSun.Name = Settings.Default.LabelTXChaSun;

            ListViewItem.ListViewSubItem subTxPowerResist = new ListViewItem.ListViewSubItem();
            subTxPowerResist.Name = Settings.Default.LabelTxNaiGongLu;


            ListViewItem item = new ListViewItem
                (new ListViewItem.ListViewSubItem[]
                {
                    subNo,
                    subRxChaSun,
                    subRxGeLiDu,
                    subTxChaSun,
                    subTxPowerResist,
                }, -1);

            foreach (TestItemBase t in this._results)
            {
                item.SubItems[t.Name].Text = t.TheValue.ToString(Helper.FloatFormat);
                Debug.WriteLine(string.Format("{0}:{1}", t.Name, t.TheValue));
            }

            if (!RoundPassed(this.TestItems))
            {
                item.ForeColor = Helper.WarningColor();
            }

            return item;
        }

        private void AddTestResult()
        {
            ListViewItem item = this.BuildListViewItem();
            this.listView1.Items.Add(item);
            item.EnsureVisible();
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState == null)// one round is done
            {
                bool passed = this.RoundPassed(this.TestItems);
                if (passed)
                {
                    AddTestResult();
                }
                else
                {
                    if (Properties.Settings.Default.ShowNotPassedRecord)
                    {
                        AddTestResult();
                    }
                }
            }
            else
            {
                TestItemBase item = e.UserState as TestItemBase;

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