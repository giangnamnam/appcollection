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
using System.Net.Sockets;

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
            Header h = new Header(Command.IpQueryStart, MAC.BroadCast);
            byte[] data = BitConverter.GetBytes(3);

            UdpClient udp = new UdpClient(CameraPort);
            udp.EnableBroadcast = true;

        }

        private void SendCommand(UdpClient udp, Header hdr, byte[] data)
        {
            byte[] hdrBytes = hdr.GetBytes();
            udp.Send(hdrBytes, hdrBytes.Length);

            udp.Send(data, data.Length);
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

        enum Command : ushort
        {
            IpQueryStart = 0x4031, IpQueryReply = 0x0031, IpQueryReplyConfirm = 0x8031,
        }


        class Header
        {
            byte[] buffer = new byte[32];

            public Header(Command cmd, MAC destMAC)
            {
                this.Cmd = cmd;
                this.Mac = destMAC;

                this.PackNo = 1;
                this.TotalNumOfPackets = 1;
                this.SeqNo = 1;
            }

            public Command Cmd
            {
                get { return (Command)this.GetUshort(0); }
                set { this.SetShort((ushort)value, 0); }
            }

            public MAC Mac
            {
                get { return new MAC(buffer, 2); }
                set { value.GetBytes().CopyTo(buffer, 2); }
            }

            public ushort SeqNo
            {
                get { return this.GetUshort(8); }
                set { this.SetShort(value, 8); }
            }

            public ushort Ver
            {
                get { return this.GetUshort(10); }
                set { this.SetShort(value, 10); }
            }

            public ushort PackNo
            {
                get { return this.GetUshort(12); }
                set { this.SetShort(value, 12); }
            }

            public ushort TotalNumOfPackets
            {
                get { return this.GetUshort(14); }
                set { this.SetShort(value, 14); }
            }

            public byte[] GetBytes()
            {
                return (byte[])buffer.Clone();
            }

            public void Parse(byte[] buffer, int startIndex)
            {
                buffer.CopyTo(this.buffer, startIndex);
            }

            private void SetShort(ushort value, int index)
            {
                byte[] bytes = BitConverter.GetBytes(value);
                bytes.CopyTo(buffer, index);
            }

            private ushort GetUshort(int index)
            {
                return BitConverter.ToUInt16(buffer, index);
            }

        }

        static int CameraPort = 10001;
        static int PCPort = 10000;

        CookieContainer cookies;
    }
}
