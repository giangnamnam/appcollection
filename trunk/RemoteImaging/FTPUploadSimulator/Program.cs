using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTPUploadSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            string targetFolder = args[0];
            int camID = int.Parse(args[1]);
            int count = int.Parse(args[2]);

            while (true)
            { 
                DateTime dt = DateTime.Now;

                for (int i = 0; i < count; i++)
                {
                    string fileName = string.Format("{0:D2}_{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}{6:D2}-{7:D4}.jpg", 
                        camID,
                        dt.Year-2000, 
                        dt.Month,
                        dt.Day,
                        dt.Hour,
                        dt.Minute,
                        dt.Second,
                        i);

                    string path = System.IO.Path.Combine(targetFolder, fileName);
                    System.IO.FileStream file = System.IO.File.Create(path);
                    file.Close();

                }

                System.Threading.Thread.Sleep(3000);
                
            }
        }
    }
}
