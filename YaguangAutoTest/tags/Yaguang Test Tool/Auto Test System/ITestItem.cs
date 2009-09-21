using System;
using System.Collections.Generic;
using System.Text;

namespace Yaguang.VJK3G.Test
{
    public abstract class ITestItem
    {

        public Func<float, float, bool> Comparer
        {
            get;
            set;
        }

        public Func<IList<string>, float> DataPicker
        {
            get;
            set;
        }

        public float Threshold
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
            this.TheValue = this.DataPicker(this.Values);
            this.Passed = this.Comparer(this.TheValue, this.Threshold);
        }

        public void Run()
        {
            this.BeforeTest();

            this.DoTest();

            this.AfterTest();

            this.MakeVerdict();
        }

        public virtual void Setup()
        {

        }

        public abstract void DoTest();

        public virtual void BeforeTest()
        {

        }

        public virtual void AfterTest()
        {


        }

    }
}
