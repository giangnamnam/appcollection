using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yaguang.VJK3G.GUI
{

    using Properties;
    using Instrument;

    public partial class FormToolDetect : Form
    {
        public FormToolDetect()
        {
            InitializeComponent();
            this.timer1.Enabled = true;
        }


        private int _state = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this._state == 0)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.Start;

                if (SwitchController.Default.TC1 == int.Parse(Settings.Default.TC1Zero)
                    && SwitchController.Default.TC2 == int.Parse(Settings.Default.TC2Zero))
                {
                    this._state = 1;
                }
            }
            else if (this._state == 1)
            {
                SwitchController.Default.CurrentSwitch = SwitchSetting.ToolConfirm;
                if (SwitchController.Default.TC1 == int.Parse(Settings.Default.TC1Tool)
                    && SwitchController.Default.TC2 == int.Parse(Settings.Default.TC2Tool))
                {
                    //this.timer1.Dispose();
                    this.Dispose();
                }
            }
        }

        private void FormToolDetect_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Dispose();
        }
    }
}
