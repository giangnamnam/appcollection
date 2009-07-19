using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using JSZN.Net;

namespace JSZN.Component
{
    public partial class SanyoNetCamera : System.ComponentModel.Component
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
            string uri = string.Format("http://{0}", IPAddress);

            HttpWebRequest reqAuthorize = (HttpWebRequest)HttpWebRequest.Create(uri);
            reqAuthorize.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            reqAuthorize.ProtocolVersion = new Version(1, 1);
            reqAuthorize.Credentials = new NetworkCredential(UserName, Password);
            reqAuthorize.CookieContainer = new CookieContainer();
            reqAuthorize.KeepAlive = true;

            HttpWebResponse reply = (HttpWebResponse)reqAuthorize.GetResponse();
            cookies = reqAuthorize.CookieContainer;

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


        class Header
        {
            byte[] buffer = new byte[32];

            public Command Cmd { get; set; }
            public MAC Mac { get; set; }
            public int SeqNo { get; set; }
            public int Ver { get; set; }
            public int PackNo { get; set; }
            public int TotalNumOfPackets { get; set; }

            public byte[] GetBytes()
            {
                return buffer;
            }

        }


        CookieContainer cookies;
    }
}
