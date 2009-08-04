using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RemoteImaging
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class.
        /// </summary>
        public Configuration()
        {
            lineCameras();
            #region
            //Thread thread = new Thread(new ParameterizedThreadStart(lineCameras));
            //thread.Start();
            //Thread.Sleep(3000);

            //XDocument camXMLDoc = XDocument.Load(Properties.Settings.Default.CamConfigFile);
            //var camsElements = camXMLDoc.Root.Descendants("cam");

            //Cameras = new List<Camera>();
            //foreach (XElement camElement in camsElements)
            //{
            //    int id = int.Parse((string)camElement.Attribute("id"));
            //    Cameras.Add(
            //        new Camera() { ID = id, IpAddress = camElement.Attribute("ip").Value, Name = camElement.Attribute("name").Value });
            //}
            #endregion
        }

        public void Save()
        {
            XDocument doc = XDocument.Load(Properties.Settings.Default.CamConfigFile);
            doc.Root.RemoveNodes();

            foreach (Camera cam in Cameras)
            {
                doc.Root.Add(new XElement("cam",
                    new XAttribute("ip", cam.IpAddress),
                    new XAttribute("name", cam.Name),
                    new XAttribute("id", cam.ID),
                    new XAttribute("MAC", cam.Mac)));
            }

            doc.Save(Properties.Settings.Default.CamConfigFile);
        }

        public IList<Camera> Cameras
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
                }
                return instance;
            }
        }



        private static Configuration instance;

        //获得在线摄像机 
        private void lineCameras()
        {
            List<Camera> lineCam = new List<Camera>();
            List<Camera> trueLineCamera = new List<Camera>();
            XDocument camXMLDoc = XDocument.Load(Properties.Settings.Default.CamConfigFile);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                lineCam.Add(new Camera() { ID = id, IpAddress = camElement.Attribute("ip").Value, Name = camElement.Attribute("name").Value, Mac = camElement.Attribute("MAC").Value, });

            }

            foreach (Camera camera in lineCam)
            {
                CheckLiveCamera gs = new CheckLiveCamera(camera);
                Camera gsCam = new Camera();
                gsCam = gs.LineCamera;
                trueLineCamera.Add(gsCam);

            }
            Cameras = trueLineCamera;
        }

    }
}
