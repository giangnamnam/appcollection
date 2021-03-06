﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Threading;
using Damany.RemoteImaging.Net.Discovery;
using Emcaster.Sockets;
using Emcaster.Topics;

namespace RemoteImaging
{
    public class Configuration
    {
        UdpSource sendSocket;
        BatchWriter writer;
        TopicPublisher publisher;

        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        private Configuration(string brdcstip)
        {
            this.BroadcastIp = brdcstip;
            
        }

        public string BroadcastIp { get; set; }

        private void SendHostConfigQuery()
        {
            PgmSource sendSocket = new PgmSource("224.0.0.23", 7272);
            sendSocket.Start();

            BatchWriter asyncWriter = new BatchWriter(sendSocket, 1024 * 128);

            TopicPublisher publisher = new TopicPublisher(asyncWriter);
            publisher.Start();

            int sendTimeout = 1000;
            publish.PublishObject("Stock-Quotes-AAPL", 123.3, sendTimeout);
        }

        public void StartDiscovery()
        {
            List<Camera> lineCam = new List<Camera>();
            XDocument camXMLDoc = XDocument.Load(fileName);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                lineCam.Add(new Camera()
                {
                    ID = id,
                    IpAddress = camElement.Attribute("ip").Value,
                    Name = camElement.Attribute("name").Value
                });
            }
            //Cameras = lineCam;
        }

        public void Save()
        {
            XDocument doc = XDocument.Load(fileName);
            doc.Root.RemoveNodes();

//             foreach (Camera cam in Cameras)
//             {
//                 doc.Root.Add(new XElement("cam",
//                     new XAttribute("ip", cam.IpAddress),
//                     new XAttribute("name", cam.Name),
//                     new XAttribute("id", cam.ID)
//                    ));
//             }
// 
//             doc.Save(Properties.Settings.Default.CamConfigFile);
//             LoadConfig();
        }


        public HostConfiguration this[object id]
        {
            get
            {
                try
                {
                    return this.Hosts.First(h => h.ID.Equals(id));
                }
                catch (System.InvalidOperationException)
                {
                    return null;
                }

            }
            
        }

        public IList<HostConfiguration> Hosts
        {
            get;
            set;

        }

        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configuration();
                    instance.LoadConfig();
                }
                return instance;
            }
        }

        public string fileName = Properties.Settings.Default.CamConfigFile;

        private static Configuration instance;
        public Thread thread = null;
        //获得在线摄像机 
        public void GetLineCameras()
        {

        }

    }
}
