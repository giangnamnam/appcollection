using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Test
{
    using Predicators;

    public abstract class TestItemBase
    {

        public event EventHandler BeforeSetup;
        public event EventHandler AfterSetup;
        public event EventHandler BeforeReadData;
        public event DataReadEventHandler DataRead;
        public DataPickers.PickData Pick { get; set; }

        public IPredicator Predicator
        {
            get;
            set;
        }

        public float TheValue
        {
            get;
            set;
        }

        public bool Passed
        {
            get;
            set;
        }

        public string[] OriginalValues
        {
            get;
            set;
        }

        public IList<string> Values
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public void MakeVerdict()
        {
            this.TheValue = this.Pick(this.Values);

            this.Passed = this.Predicator.Predicate(this.Values);
        }

        public void Run()
        {
            this.FireBoforeReadDataEvent();

            this.ReadData();

            this.OriginalValues = new string[this.Values.Count];
            for (int i = 0; i < this.Values.Count; i++)
            {
                this.OriginalValues[i] = string.Copy(this.Values[i]);
            }

            this.FireAfterReadDataEvent();

            this.MakeVerdict();
        }


        public abstract void DoSetup();

        public void Setup()
        {
            FireBoforeSetupEvent();

            this.DoSetup();

            FireAfterSetupEvent();
        }

        public abstract void ReadData();

        void FireBoforeSetupEvent()
        {
            if (BeforeSetup != null)
            {
                BeforeSetup(this, EventArgs.Empty);
            }
        }

        void FireAfterSetupEvent()
        {
            if (AfterSetup != null)
            {
                AfterSetup(this, EventArgs.Empty);
            }
        }

        void FireBoforeReadDataEvent()
        {
            if (BeforeReadData != null)
            {
                BeforeReadData(this, EventArgs.Empty);
            }
        }

        void FireAfterReadDataEvent()
        {
            if (DataRead != null)
            {
                DataRead(this, new DataReadEventArgs(this.Values));
            }
        }


    }
}
