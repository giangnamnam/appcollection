using System;
using System.Collections.Generic;
using System.Text;

//VISA COM
using Ivi.Visa.Interop;

namespace Yaguang.VJK3G.IO
{
    class GPIB : IStringStream
    {
        private IVisaSession Session = null;
        private IMessage Message = null;
        private IFormattedIO488 FIO488 = null;
        private static IResourceManager GRM;

        private GPIB()
        {
            
        }

        public static GPIB Open(string GPIBAddress)
        {
            if (GRM == null)
            {
                GRM = new Ivi.Visa.Interop.ResourceManager();
            }

            GPIB gpib = new GPIB();
            
            gpib.Session = GRM.Open(GPIBAddress,
                                   AccessMode.NO_LOCK,
                                    2000,
                                    "");

            gpib.FIO488 = new FormattedIO488();
            gpib.Message = (IMessage) gpib.Session;
            gpib.FIO488.IO = gpib.Message;

            return gpib;
        }

        public string ReadString()
        {
            string data = "";
            try
            {
                data = this.FIO488.ReadString();
            }
            catch (System.Exception e)
            {
            	
            }
            
            return data;
        }

        public void WriteString(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                this.FIO488.WriteString(data, true);
            }

        }


        #region IStringStream Members


        public string Query(string queryString)
        {
            this.WriteString(queryString);
            return this.ReadString();
        }

        #endregion
    }
}
