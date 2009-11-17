using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Windows.Forms;
using FaceRecognition;

namespace RemoteImaging.ImportPersonCompare
{
    public class AnalysePersons
    {
        PersonInfoHandleXml persons;
        public AnalysePersons()
        {
            persons = PersonInfoHandleXml.GetInstance();
        }

        /// <summary>
        /// 分析人脸比对后的图片 并添加到集合中
        /// </summary>
        /// <param name="similarityList">SiilarityMat[] 人脸识别后的集合</param>
        /// <returns> List<ImportantPersonDetail> </returns>
        public List<ImportantPersonDetail> FilterSimilarity(RecognizeResult[] similarityList)
        {
            List<ImportantPersonDetail> listPersonDetail = new List<ImportantPersonDetail>();
            foreach (var sm in similarityList)
            {
                ImportantPersonDetail detail = new ImportantPersonDetail(persons);
                detail.Similarity = sm;
                if (detail.State)
                {
                    listPersonDetail.Add(detail);
                }
            }
            return listPersonDetail;
        }
    }

    public class ImportantPersonDetail
    {
        /// <summary>
        /// 是否是犯罪嫌疑人
        /// </summary>
        public bool State { get; set; }

        public PersonInfo Info { get; private set; }

        public string[] SimilarityRange { get; private set; }

        private FaceRecognition.RecognizeResult sm;
        public FaceRecognition.RecognizeResult Similarity
        {
            get { return sm; }
            set
            {
                sm = value;
                PersonInfo info = persons.ReadInfo(sm.fileName);  // 查看犯罪分子列表中是否包含训练后当前人物信息
                if (info != null)
                {
                    string[] temp = persons.GetRangeByLevel(info.Similarity); // 获得当前人物 相似度级别中的范围 
                    string[] p = new string[temp.Length];
                    temp.CopyTo(p, 0);
                    if (p.Length > 0)
                    {
                        float x = Convert.ToSingle(p[0]);
                        float y = Convert.ToSingle(p[1]);

                        if (sm.similarity >= x && sm.similarity < y)
                        {
                            this.Info = info;
                            this.SimilarityRange = p;
                            this.State = true;
                        }
                    }
                }
            }
        }
        PersonInfoHandleXml persons;
        public ImportantPersonDetail(PersonInfoHandleXml infohandle)
        {
            persons = infohandle;
        }

    }
}