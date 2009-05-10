using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace RemoteImaging.RealtimeDisplay
{
    internal class IconExtractor : IIconExtractor
    {
        public string ExtractIcons(string bigImage, string destFolder)
        {
            string srcfileName = Path.GetFileName(bigImage);

            Random rand = new Random(DateTime.Now.Second);
            int count = rand.Next(5);

            StringBuilder sbReturn = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                string destFileName = Path.GetFileNameWithoutExtension(bigImage)
                    + "-" + i.ToString("D4")  + Path.GetExtension(bigImage);
                string destPathName = Path.Combine(destFolder, destFileName);

                Image img = Image.FromFile(bigImage);
                Graphics g = Graphics.FromImage(img);
                string text = DateTime.Now.ToString();
                
                g.DrawString(text,
                    new Font("arial", 50), 
                    Brushes.Black, 
                    0, 0);

                img.Save(destPathName);
                sbReturn.Append(destFileName);
                sbReturn.Append('\t');
            }

            return sbReturn.ToString();

        }

        #region IIconExtractor Members


        public void Initialize()
        {
            
        }

        #endregion
    }
}
