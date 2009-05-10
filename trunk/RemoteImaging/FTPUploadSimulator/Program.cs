using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FTPUploadSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetFolder = args[0];
            int camID = int.Parse(args[1]);
            int count = int.Parse(args[2]);

            string[] files = Directory.GetFiles(@"d:\20090505");

            foreach (string file in files)
            {
                string destPathName = Path.Combine(@"d:\UploadPool", Path.GetFileName(file));
                File.Copy(file, destPathName);
                System.Threading.Thread.Sleep(1000);
            }
            
        }
    }
}
