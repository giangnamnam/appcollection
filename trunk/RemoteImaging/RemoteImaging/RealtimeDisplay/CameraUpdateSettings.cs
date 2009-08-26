using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using System.Net;
using System.Reflection;
using System.Xml;
using System.IO.Ports;
using System.Threading;

namespace RemoteImaging.RealtimeDisplay
{
    public class CameraUpdateSettings
    {
       SerialPort sp;
       private BrightType Model{get;set;}
       private string Ip { get; set; }
       private string ComName { get; set; }
       
        public CameraUpdateSettings(string name,BrightType brighMode,string ip)
        {
            Model = brighMode;
            Ip = ip;
            ComName = name;
            Hashtable hsAll = new Hashtable();
            
            MemberInfo[] mebInfo = typeof(BrightType).GetMembers();
            foreach (MemberInfo mi in mebInfo)
            {
                if (mi.MemberType == MemberTypes.Field)
                {
                    string types = mi.Name.ToString();
                    
                    if (!types.Equals("value__"))
                    {
                       Hashtable temphs = new Hashtable();
                       temphs = InitHashTable(types);
                       hsAll.Add(types, temphs);
                    }
                }
            }
            this.ListSettings = hsAll;
            sp = new SerialPort(ComName);
        }

       private Hashtable InitHashTable(string type)
       {
           XmlDocument xmlDoc = new XmlDocument();
           xmlDoc.Load("CamSettingsConfig.xml");
           var camsElements = xmlDoc.SelectNodes("/SettingItem/" + type + "/illumination");

           Hashtable listStr = new Hashtable();
           foreach (XmlNode xn in camsElements)
           {
               CameraParam cp = new CameraParam()
               {
                   iris = Convert.ToInt32(xn.Attributes["iris"].Value),
                   shutter = Convert.ToInt32(xn.Attributes["shutter"].Value),
                   agc = Convert.ToInt32(xn.Attributes["agc"].Value)
               };
               listStr.Add(Model.ToString() + xn.Attributes["value"].Value, cp);
           }
           return listStr;
       }

        /// <summary>
        /// 打开端口
        /// </summary>
       private void OpenPort()
       {
           try
           {
               if (sp.IsOpen)
               {
                   sp.Close();
               }
               sp.Open();
           }
           catch (Exception ex)
           {
               Console.Write("Erro Message:" + ex.Message);
           }
       }

       /// <summary>
        /// 读取端口
        /// </summary>
        /// <param name="modle">BrightType</param>
        /// <param name="Ip">摄像机的Ip</param>
       public void ReadPort(object obj)
       {
           OpenPort();
           int count = 0;
           int[] arrInt = new int[5];
           while (true)
           {
               try
               {
                   int temp = Convert.ToInt32(sp.ReadLine());
                   if (count == 5)
                   { 
                       int good =(GetBestValue(arrInt.ToArray()) / 4 + 5)/10*10;//获得中间值
                       //Console.WriteLine("good data:" + good.ToString());
                       CameraParam setVal = new CameraParam();
                       setVal = GetSettingString(Model.ToString() + good.ToString(), Model.ToString());//获得相匹配的字符串
                       if (setVal != null) this.SettingCamera(setVal, Ip); //设置相机
                       count = 0;
                       sp.Close();
                       Thread.Sleep(2500);
                   }
                   else
                   {
                       arrInt[count++] = temp;
                   }
               }
               catch (Exception ex)
               {
                   Console.WriteLine("Erro Message:" + ex.Message);
               }
               Thread.Sleep(1500);
               OpenPort();
           }
       }

       private int GetBestValue(Array temp)
       {
           Array.Sort(temp);
           return Convert.ToInt32(temp.GetValue(2));
       }

       private Hashtable ListSettings { get; set; }

        /// <summary>
        /// 获得设置参数
        /// </summary>
        /// <param name="matchVal">要匹配的参数</param>
        /// <param name="type">BrightType</param>
        /// <returns></returns>
       private CameraParam GetSettingString(string matchVal, string type)
       {
           Hashtable hs = (Hashtable)ListSettings[type];
           object objCp = hs[matchVal];
           return (objCp != null) ? (CameraParam)objCp : null;
       }

        /// <summary>
        /// 设置照相机
        /// </summary>
        /// <param name="cp"></param>
        /// <param name="Ip"></param>
       private void SettingCamera(CameraParam cp, string Ip)
       {
           SetCamera sd = new SetCamera(cp, Ip);
           sd.Connect();
       }
    }


    public enum BrightType
    {
        /// <summary>
        /// 室内顺光
        /// </summary>
        Indoor_Front=0,
        /// <summary>
        /// 室内逆光
        /// </summary>
        Indoor_Back = 1,
        /// <summary>
        /// 室外
        /// </summary>
        Outdoor=2,
        /// <summary>
        /// 红外
        /// </summary>
        Infrared
    }

    /// <summary>
    /// 光圈级别，快门速度，AGC电子灵敏度
    /// </summary>
    public class CameraParam
    {
        /// <summary>
        /// Initializes a new instance of the CameraParam class.
        /// </summary>
        /// <param name="iris"></param>
        /// <param name="shutter"></param>
        /// <param name="agc"></param>
        public CameraParam()
        {

        }

        public CameraParam(int tIris, int tShutter, int tAgc)
        {
            this.iris = tIris;
            this.shutter = tShutter;
            this.agc = tAgc;
        }
        public int iris;
        public int shutter;
        public int agc;
    }

    /// <summary>
    /// 设置相机
    /// </summary>
    public class SetCamera
    { 
        Encoding encoding = Encoding.Default;
        CameraParam cp =new CameraParam();
        string url = "";
        public SetCamera(CameraParam ccp, string Ip)
        {
            cp = ccp;
            url = string.Format("http://{0}",Ip);
        }

        /// <summary>
        /// 连接摄像机
        /// </summary>
        public void Connect()
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            httpRequest.Credentials = new NetworkCredential("admin", "admin");
            httpRequest.ProtocolVersion = new Version(1, 1);
            httpRequest.AllowAutoRedirect = true;
            httpRequest.CookieContainer = new CookieContainer();
            httpRequest.PreAuthenticate = true;
            httpRequest.KeepAlive = true;

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            cookie = new CookieContainer();
            cookie = httpRequest.CookieContainer;
            httpResponse.Close();

            if (cookie.Count > 0) 
            {
                SendHttpRequest();
            }
        }

        /// <summary>
        /// 修改摄像机参数
        /// </summary>
        public void SendHttpRequest()
        {
            string fds =string.Format("{0}/cgi-bin/camera_quality.cgi?view_sw=1&iris_sw=1&sense_up=0&auto_level={1}&shutter_sw=1&short_speed={2}&agc_sw=1&max_gain=1&digital_gain={3}", url,cp.iris,cp.shutter,cp.agc);
            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(fds);
            httpRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            httpRequest.Credentials = new NetworkCredential("admin", "admin");
            httpRequest.UserAgent = "ImgCtrl\r\n";
            httpRequest.CookieContainer = cookie;
            httpRequest.Method = "POST";
            httpRequest.PreAuthenticate = true;
            httpRequest.Accept = "*/*";
            httpRequest.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse hwr = (HttpWebResponse)httpRequest.GetResponse();
            hwr.Close();
            Console.WriteLine("修改成功！！");
        }

        CookieContainer cookie;

    }

}
