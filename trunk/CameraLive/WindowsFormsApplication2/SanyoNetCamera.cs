using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class SanyoNetCamera : Component
    {
        public SanyoNetCamera()
        {
            InitializeComponent();
        }

        public static void SearchCamersAsync()
        {

        }

        public SanyoNetCamera(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string IPAddress { get; set; }

        public void Connect()
        {
            string uri = string.Format("http://{0}/cgi-bin/lang.cgi", IPAddress);

            HttpWebRequest reqAuthorize = (HttpWebRequest)HttpWebRequest.Create(uri);
            reqAuthorize.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            reqAuthorize.ProtocolVersion = new Version(1, 1);
            reqAuthorize.Credentials = new NetworkCredential(UserName, Password);
            reqAuthorize.CookieContainer = new CookieContainer();
            reqAuthorize.KeepAlive = true;

            HttpWebResponse reply = (HttpWebResponse)reqAuthorize.GetResponse();
            cookies = reqAuthorize.CookieContainer;

        }


        string[] files;
        int idx = 0;

        public void Init()
        {
            files = Directory.GetFiles(@"D:\pictures in hall");


        }

        public byte[] GetImageFake()
        {
            if (idx > files.Length - 1) return new byte[0];

            string file = files[idx++];

            return File.ReadAllBytes(file);



        }

        public byte[] GetImage()
        {
            string uri = string.Format("http://{0}/liveimg.cgi", IPAddress);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
            req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            req.Credentials = new NetworkCredential(UserName, Password);
            req.ProtocolVersion = new Version(1, 1);
            req.CookieContainer = cookies;
            req.KeepAlive = true;

            HttpWebResponse reply = (HttpWebResponse)req.GetResponse();

            long len = reply.ContentLength;
            byte[] buff = new byte[len];

            Stream stream = reply.GetResponseStream();

            long count = 0;

            do
            {
                count += stream.Read(buff, (int)count, (int)(len - count));
            } while (count < len);

            return buff;
        }

        enum Command
        {
            SearchStart, SearchReply, SearchReplyConfirm
        }

        class MAC
        {
            byte[] buffer = new byte[6];
            public MAC(byte b0, byte b1, byte b2, byte b3, byte b4, byte b5)
            {
                buffer[0] = b0;
                buffer[1] = b1;
                buffer[2] = b2;
                buffer[3] = b3;
                buffer[4] = b4;
                buffer[5] = b5;
            }

            public override string ToString()
            {
                return string.Format("{0:d2}:{1:d2}:{2:d2}:{3:d2}:{4:d2}:{5:d2}",
                    buffer[0], buffer[1], buffer[2], buffer[3], buffer[4], buffer[5]);
            }

            public byte[] GetBytes()
            {
                return buffer;
            }
        }

        class Header
        {
            byte[] buffer = new byte[32];
            public Command Cmd { get; set; }
            //public 

        }


        CookieContainer cookies;
    }
}
