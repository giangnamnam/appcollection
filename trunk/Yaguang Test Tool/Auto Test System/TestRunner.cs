using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.Instrument;

namespace Yaguang.VJK3G.Test
{
    public class TestRunner
    {
        private Test.TestGroup currentGroup;
        private Test.ITestItem currentItem;
        private Yaguang.VJK3G.Test.TestGroup[] groups;
        private Instrument.NetworkAnalyzer av;
        private Instrument.Oscillograph osc;
        private Instrument.SwitchController sc;

        public bool Run = true;


        private int i = 0;

        public TestRunner(SwitchController sc,
                          NetworkAnalyzer av,
                          Oscillograph osc)
        {
            this.av = av;
            this.osc = osc;
            this.sc = sc;
        }

        internal NetworkAnalyzer NetworkAnalyzer
        {
            get
            {
                return this.av;
            }
        }

        internal Oscillograph Oscillograph
        {
            get
            {
                return this.osc;
            }
        }

        internal SwitchController SwitchController
        {
            get
            {
                return this.sc;
            }
        }

        internal TestGroup[] Groups
        {
            get
            {
                return this.groups;
            }
        }

        public TestGroup CurrentGroup
        {
            get
            {
                return this.currentGroup;
            }
            set
            {
                this.currentGroup = value;
            }
        }

        public ITestItem CurrentItem
        {
            get
            {
                return this.currentItem;
            }
            set
            {
                this.currentItem = value;
            }
        }

        private Test.TestGroup GetNextGroup()
        {
            return this.groups[i++];
        }

        public void RunTestGroup(TestGroup group)
        {
            int nActive = 0;
            foreach (ITestItem item in group.Items)
            {
//                 if (item.AV)
//                 {
//                     ++nActive;
//                 }
            }

            if (nActive <= 0)
            {
                return;
            }

            switch (group.Device)
            {
                case DeviceType.AV:
                    this.av.SetupGroup(group);
                    break;
                case DeviceType.OSC:
                    this.osc.SetupGroup(group);
                    break;
            }

            group.RunTest(this);
        }

        public void RunTest(object state)
        {
            while(Run)
            {
                foreach (TestGroup group in this.groups)
                {
                    this.RunTestGroup(group);
                }

            }
            
        }

    }
}
