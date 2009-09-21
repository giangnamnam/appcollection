using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.IO;

namespace Yaguang.VJK3G.Instrument
{
    public class Oscillograph : GPIBDeviceBase
    {
        private static Oscillograph _instance = null;

        public static Oscillograph Default
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Oscillograph(null);
                }

                return _instance;
            }
        }


        public string ReadMeasure(string id)
        {
            string cmd = "Measurement:Meas" + id + ":TheValue?";

            string result = this.Query(cmd);
            string fResult = Helper.ScientificToFloat(result);

            return fResult;
        }

        private Oscillograph(IO.IStringStream stream) : base(stream)
        {
            IList<string> initCommands = new List<string>()
            {
                {"Hdr Off"},
                {"Measurement:Meas1:State OFF"},
                {"Measurement:Meas2:State OFF"},
                {"Measurement:Meas3:State OFF"},
                {"Measurement:Meas4:State OFF"},
                {"Measurement:Meas1:Type Delay"},
                {"Measurement:Meas1:Delay:Direction Forwards"},
                {"Measurement:Meas1:Delay:Edge1 Rise"},
                {"Measurement:Meas1:Delay:Edge2 Rise"},
                {"Measurement:Meas1:Source CH1"},
                {"Measurement:Meas1:Source2 CH2"},
                {"Measurement:Meas1:State ON"},
                {"Measurement:Meas2:Type Delay"},
                {"Measurement:Meas2:Delay:Direction Forwards"},
                {"Measurement:Meas2:Delay:Edge1 Fall"},
                {"Measurement:Meas2:Delay:Edge2 Fall"},
                {"Measurement:Meas2:Source CH1"},
                {"Measurement:Meas2:Source2 CH2"},
                {"Measurement:Meas2:State ON"}
            };

            this.InitCommands = initCommands;
        }
    }
}
