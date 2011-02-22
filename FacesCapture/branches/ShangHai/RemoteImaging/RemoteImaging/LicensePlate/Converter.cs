using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace RemoteImaging.LicensePlate
{
    public static class Converter
    {
        public static string GetDescription(this ReportedCarMissingType type)
        {
            switch (type)
            {
                case ReportedCarMissingType.Missing:
                    return "丢失";
                case ReportedCarMissingType.Stolen:
                    return "盗抢";
            }

            return "未定义";
        }


        public static string GetDescription(this ProcessBehavior b)
        {
            switch (b)
            {
                case ProcessBehavior.Confirmed:
                    return "确认";
                case ProcessBehavior.Ignored:
                    return "忽略";
            }

            return "未定义"; 
        }

        public static string GetDescription(this System.Drawing.Color c)
        {
            if (c == System.Drawing.Color.Blue)
            {
                return "蓝";
            }

            if (c == System.Drawing.Color.Black)
            {
                return "黑";
            }

            if (c == System.Drawing.Color.Yellow)
            {
                return "黄";
            }

            if (c == System.Drawing.Color.White)
            {
                return "白";
            }

            return "未定义";
        }
    }
}
