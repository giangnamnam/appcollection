using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace RemoteImaging.ImportPersonCompare
{
    public class SuspectsRepository
    {
        protected Dictionary<string, PersonInfo> storage
            = new Dictionary<string, PersonInfo>();

        private static SuspectsRepository instance;

        public static SuspectsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Load();
                }

                return instance;
            }
        }

        private static void loadInternal(Dictionary<string, PersonInfo> repo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Properties.Settings.Default.ImpPersonConfigure);

            XmlNodeList nodes = doc.SelectNodes("//person");

            foreach (XmlNode n in nodes)
            {
                PersonInfo p = new PersonInfo();
                p.ID = n.Attributes["id"].Value.ToString();
                p.Name = n.Attributes["name"].Value.ToString();
                p.Sex = n.Attributes["sex"].Value.ToString();
                p.Age = Convert.ToInt32(n.Attributes["age"].Value.ToString());
                p.CardId = n.Attributes["card"].Value.ToString();
                p.FileName = n.Attributes["filename"].Value.ToString();
                p.Similarity = Convert.ToInt32(n.Attributes["similarity"].Value);

                repo[p.FileName] = p;
            }
        }

        public static SuspectsRepository Load()
        {
            SuspectsRepository repo = new SuspectsRepository();

            loadInternal(repo.storage);
            return repo;
        }

        public void ReLoad()
        {
            this.storage.Clear();

            loadInternal(this.storage);
        }

        public bool Contains(string id)
        {
            return storage.ContainsKey(id);

        }

        public IEnumerable<PersonInfo> Peoples
        {
            get
            {
                return storage.Values.ToArray();
            }
        }


        public PersonInfo this[string idx]
        {
            get { return storage[idx]; }
        }



    }
}
