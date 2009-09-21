using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Test
{

    public class TestGroup
    {
        private List<ITestItem> activeItems;
        private List<ITestItem> items;
        private Port port;
        private DeviceType device;
        private string setupCommand;

        public TestGroup(DeviceType device, Port port, string setupCommand)
        {
            this.device = device;
            this.port = port;
            this.setupCommand = setupCommand;

            this.activeItems = new List<ITestItem>();
            this.items = new List<ITestItem>();
        }

        public void Add(ITestItem item)
        {
            this.items.Add(item);
            //item.OSC = this;
        }


        public string SetupCommand
        {
            get { return this.setupCommand; }
            set { this.setupCommand = value; }
        }

        public void RunTest(Test.TestRunner runner)
        {
            foreach (ITestItem item in this.items)
            {
//                 if (item.AV)
//                 {
//                     item.SetupSwitch(runner.SwitchController);
//                     item.SetupAV(runner.NetworkAnalyzer);
//                     item.SetupOSC(runner.Oscillograph);
// 
//                     item.RunTest(runner);
//                     item.ReadResult(runner);
//                 }
                
            }

        }

        public void Setup(Instrument.GPIBDeviceBase device)
        {
            //string cmd = "RECA" + this.setupCommand;
            //device.ExecuteCommand(cmd);
        }

        public List<ITestItem> Items
        {
            get
            {
                return this.items;
            }
        }

        public List<ITestItem> ActiveItems
        {
            get
            {
                return this.activeItems;
            }
        }

        public Port Port
        {
            get
            {
                return this.port;
            }
        }

        public DeviceType Device
        {
            get
            {
                return this.device;
            }
        }
    }
}
