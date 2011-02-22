using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace RemoteImaging.YunTai
{
    class ConnectionManager
    {
        private readonly Dictionary<Uri, System.IO.Stream> _connections
            = new Dictionary<Uri, Stream>();
        //
        public Stream GetConnection(Uri uri)
        {
            if (uri == null)
            {
                return null;
            }
            if (!_connections.ContainsKey(uri))
            {
                if (uri.Scheme == Uri.UriSchemeNetTcp)
                {
                    var tcpClient = new System.Net.Sockets.TcpClient();
                    tcpClient.Connect(uri.Host, uri.Port);
                    _connections.Add(uri, tcpClient.GetStream());
                    
                }
                else if (uri.Scheme == Uri.UriSchemeFile)
                {
                    //0
                    // stop bit 0
                    int dataBit = ImageConfig.DataBit;
                    int baudRate = ImageConfig.BaudRate;
                    System.IO.Ports.Parity parity = (System.IO.Ports.Parity)ImageConfig.Parity;
                    StopBits stopbit = (StopBits)ImageConfig.StopBit;
                    try
                    {
                        var com = new System.IO.Ports.SerialPort(uri.Host.ToUpper(), baudRate, parity, dataBit, stopbit);
                        com.WriteTimeout = ImageConfig.WriteTimeout;
                        com.ReadTimeout = ImageConfig.ReadTimeout;
                        com.Open();
                        _connections.Add(uri, com.BaseStream);
                         
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else 
                    throw new NotSupportedException("uri is not supported");
            }

            return _connections[uri];
        }
    }
}
