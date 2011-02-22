using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using MiscUtil.IO;

namespace RemoteImaging.LicensePlate
{
    public class TcpLicenseplateRecognizerController : ILicenseplateRecognizerController
    {
        private readonly string _peerIp;
        private readonly int _port;

        public TcpLicenseplateRecognizerController(string peerIp, int port)
        {
            if (string.IsNullOrWhiteSpace(peerIp)) throw new ArgumentNullException("peerIp");

            _peerIp = peerIp;
            _port = port;
        }

        public void Start(Action<Exception> callback)
        {
            PackAndSend(1, callback);
        }

        public void Stop(Action<Exception> callback)
        {
            PackAndSend(0, callback);
        }

        private void PackAndSend(byte cmd, Action<Exception> callback)
        {
            var buff = GetData(cmd);
            SendData(buff, callback);
        }

        

        private byte[] GetData(byte cmd)
        {
            var stream = new System.IO.MemoryStream(16);
            var writer = new MiscUtil.IO.EndianBinaryWriter(new MiscUtil.Conversion.BigEndianBitConverter(), stream);

            writer.Write((uint)0);
            writer.Write(cmd);

            writer.Close();
            return stream.ToArray();
        }

        private void SendData(byte[] data, Action<Exception> callback)
        {
            var w = Task.Factory.StartNew(() =>
            {

                var tcpClient = new TcpClient();
                tcpClient.Connect(_peerIp, _port);

                tcpClient.GetStream().Write(data, 0, data.Length);

                tcpClient.Client.Shutdown(SocketShutdown.Both);
                tcpClient.Close();

            });

            w.ContinueWith(ant =>
            {
                Exception ex = null;
                if (ant.Exception != null)
                {
                    ex = ant.Exception.InnerException;
                }
                callback(ex);
            });

        }
    }
}