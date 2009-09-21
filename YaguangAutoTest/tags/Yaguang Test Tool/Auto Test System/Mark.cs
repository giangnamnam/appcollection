using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Instrument
{
    public class Mark
    {
        private string frequency;
        private string id;
        private Channel channel;
        private IO.IStringStream io;
        private bool enabled;

        public Mark(
            string id, 
            Channel channel, 
            IO.IStringStream io, 
            string frequency)
        {
            this.id = id;
            this.channel = channel;
            this.io = io;
            this.frequency = frequency;
        }
    
 
        public Channel Channel
        {
            get
            {
                return this.channel;
            }
        }

        public string ID
        {
            get
            {
                return this.id;
            }
 
        }

        public string Frequency
        {
            get
            {
                return this.frequency;
            }
            set
            {
                this.frequency = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        public string Read()
        {
            return this.io.Query(this.ToString() + "; outpmark;");
        }

        public override string ToString()
        {
            return "Mark" + this.id;
        }

        public void Setup()
        {
            string command = string.Empty;
            command = this.ToString() + " " + this.frequency;

            this.io.WriteString(command);
        }


    }
}
