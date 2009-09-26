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

            string mbSN = GetUniqID();
            string encodedSN = EncryptService.Encode(mbSN);

            string decoded = EncryptService.Decode(key);

            return encodedSN.Equals(key);
        }



        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }

        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }


        public static string GetUniqID()
        {
            return cpuId();
        }
    }
}
