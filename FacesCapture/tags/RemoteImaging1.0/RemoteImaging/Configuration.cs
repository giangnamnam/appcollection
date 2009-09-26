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
            XDocument camXMLDoc = XDocument.Load(Properties.Settings.Default.CamConfigFile);
            var camsElements = camXMLDoc.Root.Descendants("cam");

            Cameras = new List<Camera>();
            foreach (XElement camElement in camsElements)
            {
                int id = int.Parse((string)camElement.Attribute("id"));
                Cameras.Add(
                    new Camera() { ID = id, IpAddress = camElement.Attribute("ip").Value, Name = camElement.Attribute("name").Value });
            }


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
                    new XAttribute("id", cam.ID)));
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

    }
}
