using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Sockets;

namespace Yaguang.VJK3G.IO
{
    public class StringStreamStub : IStringStream
    {
        private TcpClient tcp;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;


        public StringStreamStub()
        {
            tcp = new TcpClient("localhost", 8000);
            NetworkStream ns = tcp.GetStream();
            reader = new System.IO.StreamReader(ns, Encoding.ASCII);
            writer = new System.IO.StreamWriter(ns, Encoding.ASCII);
            writer.NewLine = "\n";
        }

        #region IStringStream Members

        public string ReadString()
        {
            return reader.ReadLine();
        }

        public void WriteString(string data)
        {
            writer.WriteLine(data);
            writer.Flush();
        }

        public string Query(string queryString)
        {
            this.WriteString(queryString);
            return this.ReadString();
        }

        #endregion
    }

    public class StringStreamStubServer : IStringStream
    {
        private TcpClient tcp;
        private System.IO.StreamReader reader;
        private System.IO.StreamWriter writer;


        public StringStreamStubServer()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 8000);

            tcp = listener.AcceptTcpClient();
            NetworkStream ns = tcp.GetStream();
            reader = new System.IO.StreamReader(ns, Encoding.ASCII);
            writer = new System.IO.StreamWriter(ns, Encoding.ASCII);
            writer.NewLine = "\n";
        }

        #region IStringStream Members

        public string ReadString()
        {
            return reader.ReadLine();
        }

        public void WriteString(string data)
        {
            writer.WriteLine(data);
        }

        public string Query(string queryString)
        {
            this.WriteString(queryString);
            return this.ReadString();
        }

        #endregion
    }


}
