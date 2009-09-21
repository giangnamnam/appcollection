using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Yaguang.VJK3G.GUI
{
    static class Program
    {
        public static bool Debug = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] parms)
        {
            Debug = parms.Length > 0;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string msg = string.Format("程序发生了异常, 请重新启动程序.\r\n异常信息为: {0}", ex.Message);
                MessageBox.Show(msg);
                Application.Exit();
            }

        }
    }
}