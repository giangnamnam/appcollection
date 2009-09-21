using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Mutex mu = new System.Threading.Mutex(false, "mutex demo");
            if (!mu.WaitOne(TimeSpan.FromSeconds(1), false))
            {
                MessageBox.Show("already running...");
                return;

            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LiveCamera());
        }
    }
}
