using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using Encryptor;

namespace RemoteImaging
{
    public static class Util
    {
        private static string GetKeyFile()
        {
            string keyFile = Path.Combine(Application.StartupPath, "key");
            return keyFile;
        }


        public static void WriteKey(string key)
        {
            string file = GetKeyFile();

            FileStream fs = File.OpenWrite(file);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(key);
            sw.Close();
        }

        public static bool VerifyKey()
        {
            string keyFile = GetKeyFile();
            if (!File.Exists(keyFile)) return false;

            string key = File.ReadAllText(keyFile);

            string mbSN = GetMainboardSN();
            string encodedSN = EncryptService.Encode(mbSN);

            string decoded = EncryptService.Decode(key);

            return encodedSN.Equals(key);
        }




        public static string GetMainboardSN()
        {
            string strbNumber = string.Empty;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
            foreach (ManagementObject mo in mos.Get())
            {
                strbNumber = mo["SerialNumber"].ToString();
                break;
            }

            return strbNumber;
        }
    }
}
