using System;
using System.Collections.Generic;
using System.Text;

using Yaguang.VJK3G.IO;
using Yaguang.VJK3G.Test;

namespace Yaguang.VJK3G.Instrument
{
    public class NetworkAnalyzer : GPIBDeviceBase
    {

        private string _activeChannel;


        public static NetworkAnalyzer Open(string gpibAddress)
        {
            if (instance == null)
            {
                //_instance = new NetworkAnalyzer(gpibAddress);

                //                 _instance.channels = new Channel[4];
                //                 for (int i = 0; i < _instance.channels.Length; ++i )
                //                 {
                //                     _instance.channels[i] = new Channel(_instance.io, (i + 1).ToString());
                //                     for (int j = 0; j < 5; ++j )
                //                     {
                //                         _instance.channels[i].Marks.Add(
                //                                                         new Mark(
                //                                                             (j+1).ToString(), 
                //                                                             _instance.channels[i], 
                //                                                             _instance.io, 
                //                                                             "")
                //                                                      );
                // 
                //                     }
                //                 }
            }

            return instance;
        }

        private NetworkAnalyzer(IO.IStringStream stream)
            : base(stream)
        {
            this._activeChannel = string.Empty;

            IList<string> initCmd = new List<string>()
            {
                //                 {"PRES"},
            };

            this.InitCommands = initCmd;
        }

        public string ActiveChannel
        {
            get
            {
                return this._activeChannel;
            }
            set
            {
                string cmd = "CHAN" + value + ";";
                this.ExecuteCommand(cmd);
                this._activeChannel = value;
            }
        }


        private static NetworkAnalyzer instance = null;


        public string ReadMark(string markID)
        {
            string cmd = "Mark" + markID + "; outpmark;";

            string data = this.Query(cmd);
            //data format (value1, value2, stimulas)
            //7.189189E+01,  0.000000E+00,   1.800000000E+09
            string[] datas = data.Split(',');
            if (datas.Length > 0)
            {
                return Helper.ScientificToFloat(datas[0]);
            }

            return string.Empty;
        }

        public void ClearMarks()
        {
            this._marks.Clear();
        }

        private IList<string> _marksSetCount = new List<string>();
        public void SetMark(string id, string value, FrequencyUnit unit)
        {
            string cmd = "Mark" + id + " " + value + unit.ToString();
            this.ExecuteCommand(cmd);

            if (!this._marksSetCount.Contains(id))
            {
                this._marksSetCount.Add(id);
                this.MarksCount++;
            }
        }

        public int MarksCount
        {
            get;
            set;
        }

        public void Recall(string number)
        {
            this.ExecuteCommand("RECA" + number);
        }


        private IList<string> _marks = new List<string>();

        public IList<string> Marks
        {
            get
            {
                this._marks.Clear();

                for (int i = 0; i < MarksCount; i++)
                {
                    this._marks.Add(ReadMark((i + 1).ToString()));
                }

                return this._marks;
            }

        }


        private static NetworkAnalyzer _instance;
        public static NetworkAnalyzer Default
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetworkAnalyzer(null);
                }

                return _instance;
            }
        }

    }
}
