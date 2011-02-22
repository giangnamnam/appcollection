using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.SS.ProfileReader;

using System.IO;
namespace RemoteImaging
{
    class ImageConfig
    {
      
        private static string ConfigPath
        {
            get
            {
                string charSign = "\\";
                string configFile = "ImageConfig.txt";
                if (AppDomain.CurrentDomain.BaseDirectory.EndsWith(charSign))
                {
                    return AppDomain.CurrentDomain.BaseDirectory + configFile;
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory + "\\" + configFile;
                }
            }
        }
        public static void WriteLog(string errMessage)
        {
            string logFile = "errorlog.txt";
            string charSign = "\\";
            try
            {
                if (AppDomain.CurrentDomain.BaseDirectory.EndsWith(charSign))
                {
                    logFile = AppDomain.CurrentDomain.BaseDirectory + logFile;
                }
                else
                {
                    logFile = AppDomain.CurrentDomain.BaseDirectory + "\\" + logFile;
                }
                FileStream fs = new FileStream(logFile, FileMode.Append, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(errMessage);
                    sw.Flush();
                }
            }
            catch (Exception ex)
            {
            }
        }
        public static int PageSize
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "PageSize", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "PageSize"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 100;
                }
            }
        }
        public static int BaudRate
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "BaudRate", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "BaudRate"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 2400;
                }
            }
        }
        /*
         WaitTime=1800
ActionTime=380
         */
        public static int ActionTime
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "ActionTime", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return int.Parse(_profile.GetValue("Setting", "ActionTime").ToString());
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 380;
                }
            }
        }
        public static int WaitTime
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "WaitTime", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return int.Parse( _profile.GetValue("Setting", "WaitTime").ToString());
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 1800;
                }
            }
        }

        public static string  Direction
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "Direction", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return  _profile.GetValue("Setting", "Direction").ToString();
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return "";
                }
            }
        }
        public static int DataBit
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "DataBit", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "DataBit"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 8;
                }
            }
        }
        //Parity
        public static int Parity
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "Parity", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "Parity"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 0;
                }
            }
        }
        public static int StopBit
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "StopBit", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "StopBit"));
                }
                catch (Exception ex)
                {
                   // logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 1;
                }
            }
        }
        public static int ReadTimeout
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "ReadTimeout", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "ReadTimeout"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 1000;
                }
            }
        }
        public static int WriteTimeout
        {
            set
            {
                Ini _profile = new Ini(ConfigPath);
                _profile.SetValue("Setting", "WriteTimeout", value.ToString());
            }

            get
            {
                Ini _profile = new Ini(ConfigPath);
                try
                {
                    return Convert.ToInt32(_profile.GetValue("Setting", "WriteTimeout"));
                }
                catch (Exception ex)
                {
                    //logger.Error(ex.Message + " :" + ex.StackTrace);
                    return 1000;
                }
            }
        }

    }
}
