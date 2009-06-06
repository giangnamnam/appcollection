using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaguang.VJK3G.Instrument;
using Yaguang.VJK3G.Properties;

namespace Yaguang.VJK3G.Test
{
    using Predicators;

    public static class TestItemFactory
    {
        public static TestItemBase CreateTestItem(TestItemType type, bool addEventHandler)
        {
            switch (type)
            {
                case TestItemType.RxChaSun:
                    TestItemBase RXChaSun =
                        new TestItemOnAV(SwitchSetting.RXChaSunZhuBo, Settings.Default.chOfRxChaSun.ToString());
                    RXChaSun.Pick = DataPickers.ValuePicker.PickMin;
                    RXChaSun.Predicator = new RxChaSunPredicator();
                    RXChaSun.Name = "RX插损";
                    if (addEventHandler)
                    {
                        RXChaSun.DataRead += RXChaSun_DataRead;

                    }

                    return RXChaSun;
                    break;
                case TestItemType.TxChaSun:
                    TestItemBase TXChaSun =
                        new TestItemOnAV(SwitchSetting.TXChaSunZhuBo, Settings.Default.chOfTxChaSun.ToString());
                    TXChaSun.Pick = DataPickers.ValuePicker.PickMin;
                    TXChaSun.Predicator = new TxChaSunPredicator();
                    TXChaSun.Name = "TX插损";
                    if (addEventHandler)
                    {

                        TXChaSun.DataRead += TXChaSun_DataRead;

                    }

                    return TXChaSun;
                    break;
                case TestItemType.TxPower:
                    TestItemBase TXPowerResist =
                        new TxPowerTestItem(SwitchSetting.TXPowerResist, Settings.Default.chOfTxNaiGongLu.ToString());
                    TXPowerResist.Pick = DataPickers.ValuePicker.PickMin;
                    TXPowerResist.Predicator = new TxPowerPredicator();
                    if (addEventHandler)
                    {
                        TXPowerResist.DataRead += new DataReadEventHandler(TXPowerResist_DataRead);
                        TXPowerResist.DataRead += new DataReadEventHandler(ShutDownShuanJianLast);
                    }

                    TXPowerResist.Name = "TX耐功率";
                    TXPowerResist.BeforeSetup += new EventHandler(OpenShuaiJianFirst);

                    return TXPowerResist;
                    break;
                case TestItemType.RxGeLi:
                    TestItemBase RXGeLiDu =
                        new TestItemOnAV(SwitchSetting.RXGeLiDu, Settings.Default.chOfRxGeLiDu.ToString());
                    RXGeLiDu.Pick = DataPickers.ValuePicker.PickMin;
                    RXGeLiDu.Predicator = new Predicators.RxGeLiPredicator();
                    RXGeLiDu.Name = "RX隔离度";
                    if (addEventHandler)
                    {
                        RXGeLiDu.DataRead += new DataReadEventHandler(RXGeLiDu_DataRead);

                    }

                    return RXGeLiDu;
                    break;
                default:
                    throw new NotSupportedException("test item type not supprted");
            }
        }

        static void OpenShuaiJianFirst(object sender, EventArgs e)
        {
            SwitchController.Default.CurrentSwitch = SwitchSetting.PreTxPower;
            Helper.Sleep(Properties.Settings.Default.ItemSwitchHoldTime);
        }

        private static void ShutDownShuanJianLast(object sender, DataReadEventArgs args)
        {
            SwitchController.Default.CurrentSwitch = SwitchSetting.PostTxPower;
            Helper.Sleep(Properties.Settings.Default.ItemSwitchHoldTime);
        }

        static void TXPowerResist_DataRead(object sender, DataReadEventArgs args)
        {
            float v = float.Parse(args.Data[0]);
            float std = float.Parse(Properties.Settings.Default.STDTXNaiGongLu);
            float externalValue = float.Parse(Properties.Settings.Default.ExternalTxPowerCaliValue);
            float calibValue = externalValue + v - std;

            string txt = string.Format("calib txpower: {0}+{1}-{2}={3}", externalValue,
                v, std, calibValue);

            System.Diagnostics.Debug.WriteLine(txt);

            args.Data[0] = calibValue.ToString("F2");
        }

        static void RXGeLiDu_DataRead(object sender, DataReadEventArgs args)
        {
            Helper.MinusBaseForRxChaSun(args.Data);
        }

        static void TXChaSun_DataRead(object sender, DataReadEventArgs args)
        {
            Helper.MinusBaseForTxChaSun(args.Data);
        }

        static void RXChaSun_DataRead(object sender, DataReadEventArgs args)
        {
            Helper.MinusBaseForRxChaSun(args.Data);
        }

    }
}
