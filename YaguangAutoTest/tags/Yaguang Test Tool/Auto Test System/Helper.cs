using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace Yaguang.VJK3G
{
    using Properties;
    using Instrument;

    class Helper
    {
        public static string FloatFormat = "F2";

        public static void Sleep(int msec)
        {
            System.Threading.Thread.Sleep(msec);
        }

        public static float Max(IList<string> values)
        {
            float max = float.Parse(values[0]);

            for (int i = 1; i < values.Count; i++)
            {
                float f = float.Parse(values[i]);
                if (f > max)
                {
                    max = f;
                }
            }

            return max;
        }


        public static float PowerResist(IList<string> values)
        {
            float f = float.Parse(values[1]);

            return f;
        }

        public static bool BEThan(float value, float std)
        {
            return value >= std;
        }

        public static bool AbsBEThan(float value, float std)
        {
            return Math.Abs(value) >= Math.Abs(std);
        }

        public static bool LEThan(float value, float std)
        {
            return value <= std;
        }

        public static IList<string> MinusBaseForRxChaSun(IList<string> values)
        {
            return MinusBase(values, "STDRXChaSun");
        }

        public static IList<string> MinusBaseForTxChaSun(IList<string> values)
        {
            return MinusBase(values, "STDTXChaSun");
        }


        public static IList<string> PlusAbsBaseForRXPowerResist(IList<string> values)
        {
            string baseName = Properties.Settings.Default.STDTXChaSun2;
            return InternalPlusAbsBaseForPowerResist(values, baseName);
        }

        public static IList<string> PlusAbsBaseForTXPowerResist(IList<string> values)
        {
            string baseName = Properties.Settings.Default.STDTXChaSun2;
            return InternalPlusAbsBaseForPowerResist(values, baseName);
        }

        public static IList<string> InternalPlusAbsBaseForPowerResist(IList<string> values, string baseName)
        {
            float b = Math.Abs(float.Parse(baseName));
            float f;
            for (int i = 0; i < values.Count; i++)
            {
                f = float.Parse(values[i]) + b;
                values[i] = f.ToString(Helper.FloatFormat);
            }

            return values;
        }

        public static IList<string> MinusBaseForTxGeLiDu(IList<string> values)
        {
            return MinusBase(values, "STDTXGeLiDu");
        }

        public static System.Drawing.Color WarningColor()
        {
            return System.Drawing.Color.Red;
        }

        public static IList<string> MinusBaseForRxGeLiDu(IList<string> values)
        {
            return MinusBase(values, "STDRXGeLiDu");
        }

        public static IList<string> MinusBase(IList<string> values, string baseName)
        {
            float f;
            for (int i = 0; i < values.Count; i++)
            {
                float v = float.Parse(values[i]);
                string stdvalueName = (string)Settings.Default[baseName + (i + 1).ToString()];
                float std = float.Parse(stdvalueName);
                f = v - std;
                values[i] = f.ToString(Helper.FloatFormat);

                string txt = string.Format("calibrate: {0} - {1} = {2}", v, std, f);
                System.Diagnostics.Debug.WriteLine(txt);
            }

            return values;
        }

        public static float Min(IList<string> values)
        {
            float min = float.Parse(values[0]);

            for (int i = 1; i < values.Count; i++)
            {
                float f = float.Parse(values[i]);
                if (f < min)
                {
                    min = f;
                }
            }

            return min;
        }

        public static void SetBit(ref int value, int bitNo)
        {
            int Mask = 0x1 << bitNo;

            value |= Mask;

        }

        public static void ClearBit(ref int value, int bitNo)
        {
            int Mask = 0x1 << bitNo;

            value &= ~Mask;
        }

        public static string ScientificToFloat(string scientific)
        {
            float f = float.Parse(scientific);

            string strFloat = string.Format("{0:F2}", f);
            return strFloat;
        }

        public static CustomException NewCustomException(string stringID)
        {
            throw new CustomException(stringID);
        }

        public static CustomException NewCustomException(string stringID,
            Exception innerException)
        {
            throw new CustomException(stringID);
        }

        public static void CopyMarks(IList<string> to, IList<string> from)
        {
            to.Clear();

            foreach (var v in from)
            {
                to.Add(v);
            }
        }

        public static IList<string> TestItemOnAV(SwitchSetting ss, int ch)
        {
            SwitchController.Default.CurrentSwitch = ss;
            System.Threading.Thread.Sleep(1000);

            NetworkAnalyzer.Default.ActiveChannel = ch.ToString();

            return NetworkAnalyzer.Default.Marks;

        }

        public static void Calibrate(object state)
        {
            IList<string> marks = null;
            Test.TestItemBase item = null;

            item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.TxChaSun, false);
            item.Setup();
            item.Run();
            Settings.Default.STDTXChaSun1 = item.OriginalValues[0];
            Settings.Default.STDTXChaSun2 = item.OriginalValues[1];
            Settings.Default.STDTXChaSun3 = item.OriginalValues[2];


            item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.RxChaSun, false);
            item.Setup();
            item.Run();
            Settings.Default.STDRXChaSun1 = item.OriginalValues[0];
            Settings.Default.STDRXChaSun2 = item.OriginalValues[1];
            Settings.Default.STDRXChaSun3 = item.OriginalValues[2];

            item = Test.TestItemFactory.CreateTestItem(Yaguang.VJK3G.Test.TestItemType.TxPower, false);
            item.Setup();
            item.Run();
            Settings.Default.STDTXNaiGongLu = item.OriginalValues[0];

        }

        public static void ShowErrorMessageBox(string msg)
        {
            MessageBox.Show(msg,
                ResourceManager.Instance.GetString("StringErrorTitle"),
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        public static void SetupMarks()
        {
            NetworkAnalyzer.Default.ClearMarks();

            NetworkAnalyzer.Default.SetMark("1", Settings.Default.FreqMark1, FrequencyUnit.GHz);
            NetworkAnalyzer.Default.SetMark("2", Settings.Default.FreqMark2, FrequencyUnit.GHz);
            NetworkAnalyzer.Default.SetMark("3", Settings.Default.FreqMark3, FrequencyUnit.GHz);

            //tx�͹���, 2G�̶�Ƶ��
            NetworkAnalyzer.Default.SetOrphanMark("4", Settings.Default.TxPowerFreq, FrequencyUnit.GHz);
        }
    }
}
