using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Drawing;

namespace RemoteImaging.ImportPersonCompare
{


    public class PersonInfoHandleXml
    {
        string FileName = Properties.Settings.Default.ImpPersonConfigure;
        private static PersonInfoHandleXml personInfoxml = null;
        public PersonInfoHandleXml()
        {
            this.LevelList = Properties.Settings.Default.SimilarityLevel.Split('|');
        }

        public static PersonInfoHandleXml GetInstance()
        {
            if (personInfoxml == null)
            {
                personInfoxml = new PersonInfoHandleXml();
            }
            return personInfoxml;
        }

        private string[] levelList = null;
        private string[] LevelList
        {
            set
            {
                string[] strLevel = (string[])value;
                if (levelList == null)
                {
                    levelList = new string[strLevel.Length];
                }
                levelList.CopyTo(strLevel, 0);
            }
        }

        public string[] GetRangeByLevel(int level)
        {
            if (levelList != null)
            {
                string[] temp = levelList[level].Split(',');
                return temp;
            }
            else
                return null;
        }

        public void WriteInfo(PersonInfo info)
        {
            XDocument xDoc = XDocument.Load(FileName);
            xDoc.Root.Add(new XElement("person",
                new XAttribute("id", info.ID),
                new XAttribute("name", info.Name),
                new XAttribute("sex", info.Sex),
                new XAttribute("age", info.Age),
                new XAttribute("card", info.CardId),
                new XAttribute("filename", info.FileName),
                new XAttribute("similarity", info.Similarity)));
            xDoc.Save(FileName);
        }

        public bool HasCurInfoNode(string cardid)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            XmlNodeList resNodeList = doc.SelectNodes("//person[@card=\"" + cardid + "\"]");
            return resNodeList.Count > 0 ? true : false;
        }

        /// <summary>
        /// 传入的文件名 091031113752226-0001.jpg
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns></returns>
        public PersonInfo ReadInfo(string fileName)
        {
            string temp = "";
            if (fileName.EndsWith(".jpg"))
                temp = ".jpg";
            else if (fileName.EndsWith(".bmp"))
                temp = ".bmp";
            else
                return null;
            string filename = fileName.Substring(0, 15) + temp;
            XmlDocument doc = new XmlDocument();
            doc.Load(FileName);
            XmlNode resNode = doc.SelectSingleNode("//person[@filename=\"" + filename + "\"]");
            if (resNode != null)
            {
                PersonInfo personinfo = new PersonInfo();
                personinfo.ID = resNode.Attributes["id"].Value.ToString();
                personinfo.Name = resNode.Attributes["name"].Value.ToString();
                personinfo.Sex = resNode.Attributes["sex"].Value.ToString();
                personinfo.Age = Convert.ToInt32(resNode.Attributes["age"].Value.ToString());
                personinfo.CardId = resNode.Attributes["card"].Value.ToString();
                //personinfo.FileName = resNode.Attributes["filename"].Value.ToString();
                personinfo.FileName = fileName;//传入的要比对的文件名
                personinfo.Similarity = Convert.ToInt32(resNode.Attributes["similarity"].Value);
                return personinfo;
            }
            else
                return null;

        }
    }

    //重要 50-58
    //一般 58-68
    //普通 68-100

    public class PersonInfo
    {
        public PersonInfo() { }
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 相似度  范围值
        /// </summary>
        public int Similarity { get; set; }
    }
}
