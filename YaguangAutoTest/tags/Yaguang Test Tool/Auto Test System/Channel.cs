using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.IO;

namespace Yaguang.VJK3G.Instrument
{
    public class Channel
    {
        private  List<Mark> marks = new List<Mark>(5);
        private IStringStream io;
        private bool activated;
        private string id;

        public Channel(IStringStream io, string id)
        {
            this.io = io;
            this.id = id;
        }
    
      
        public IList<Mark> Marks
        {
            get
            {
                return this.marks;
            }
         }

        
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
         }

        public void Activate()
        {
            this.io.WriteString("CHAN" + this.id.ToString());
            if (!this.activated)
            {
                
                foreach (Mark m in this.marks)
                {
                    if (m.Enabled)
                    {
                        m.Setup();
                    }
                    
                }

                this.activated = true;
            }
        }

        public void TestRunner()
        {
            throw new System.NotImplementedException();
        }
    }
}
