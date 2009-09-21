using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Instrument
{
    public abstract class GPIBDeviceBase
    {

        protected GPIBDeviceBase(IO.IStringStream stream)
        {
            this.WorkerStream = stream;
            this.InitCommands = new List<string>();

//             if (Yaguang.VJK3G.GUI.Program.Debug)
//             {
//                 this.WorkerStream = new IO.StringStreamStub();
//             }
//             else
//             {
//                 this.WorkerStream = IO.GPIB.Open(this.gpibAddress);
//             }
        }
    
        public IList<string> InitCommands
        {
            get;
            set;
        }

        public IO.IStringStream WorkerStream
        {
            get;
            set;
        }

        public void ExecuteCommand(string data)
        {
            this.WorkerStream.WriteString(data);
        }

        public string ReadString()
        {
            return this.WorkerStream.ReadString();
        }

        public string Query(string query)
        {
            this.WorkerStream.WriteString(query);
            return this.WorkerStream.ReadString();
        }

        public void Init()
        {
            foreach (string s in this.InitCommands)
            {
                this.ExecuteCommand(s);
            }
        }
        
    }
}
