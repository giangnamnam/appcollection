using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net.Sockets;
using System.Drawing;

namespace RemoteImaging.RealtimeDisplay
{
    class LiveClient
    {
        TcpClient client;
        BinaryFormatter formatter;

        public event EventHandler<ImageCapturedEventArgs> ImageReceived;


        public object Tag { get; set; }

        public LiveClient(TcpClient client)
        {
            this.client = client;
            formatter = new BinaryFormatter();
        }


        void FireImageReceivedEvent(Image img)
        {
            if (this.ImageReceived != null)
            {
                ImageCapturedEventArgs args = new ImageCapturedEventArgs
                {
                    ImageCaptured = img,
                };

                this.ImageReceived(this, args);
            }
        }


        public void Start()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(DoReceiveImage);

        }

        void DoReceiveImage(object state)
        {
            try
            {
                while (true)
                {
                    Image img = (Image)formatter.Deserialize(client.GetStream());
                    this.FireImageReceivedEvent(img);
                }

            }
            catch
            {


            }
        }
    }
}
