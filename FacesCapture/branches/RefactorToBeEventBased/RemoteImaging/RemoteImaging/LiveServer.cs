using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace RemoteImaging
{
    public class LiveServer
    {
        TcpClient client;
        RealtimeDisplay.Presenter host;
        BinaryFormatter formatter;

        public LiveServer(TcpClient client, RealtimeDisplay.Presenter host)
        {
            this.client = client;
            this.host = host;
            formatter = new BinaryFormatter();
        }

        public void ImageCaptured(object sender, ImageCapturedEventArgs args)
        {
            try
            {
                var img = args.ImageCaptured;
                formatter.Serialize(client.GetStream(), img);

            }
            catch
            {
                host.RemoteListener(this);
            	
            }
        }
    }
}
