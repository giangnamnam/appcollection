using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Timers;
using System.Net;
using System.Net.Sockets;

namespace RemoteImaging
{
    public class CheckLiveCamera
    {
        public CheckLiveCamera(List<Camera> tt,Configuration configd)
        {
            listCamera = new List<Camera>();
            listCamera = tt;

            trueCamera = new Camera[listCamera.Count];
            listCamera.CopyTo(trueCamera);

            config = new Configuration();
            config = configd;
        }
        private Camera[] trueCamera;
        private List<Camera> listCamera;

        private System.Timers.Timer time;
        Configuration config = null;

        public CheckLiveCamera()
        {
        }

        //发送请求
        private void send()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 10001);//255.255.255.255

            byte[] buffer = new byte[512];

            buffer[0] = 0x40;
            buffer[1] = 0x31;
            buffer[2] = 0x00;
            buffer[3] = 0x00;
            buffer[4] = 0x00;
            buffer[5] = 0x00;
            buffer[6] = 0x00;
            buffer[7] = 0x00;
            buffer[8] = 0x00;
            buffer[9] = 0x01;
            buffer[10] = 0x00;
            buffer[11] = 0x00;
            buffer[12] = 0x00;
            buffer[13] = 0x01;
            buffer[14] = 0x00;
            buffer[15] = 0x01;

            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            int ByteNumber = 0;

            try
            {
                ByteNumber = sock.SendTo(buffer, iep1);
            }
            catch (Exception ex)
            {
                return;
            }
            sock.Close();
        }

        //接收请求
        private byte[] recive()
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.ReceiveTimeout = 3000;
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 10000);

            sock.Bind(iep);
            EndPoint ep = (EndPoint)iep;

            byte[] resBuffer = new byte[512];

            try
            {
                int resInt = sock.ReceiveFrom(resBuffer, ref ep);
                sock.Close();
                return resBuffer;
            }
            catch (Exception ex)
            {
                return resBuffer;
            }
            finally
            {
                sock.Close();
            }
        }

        //将字符串格式的MAC转为 byte[]
        private byte[] GetByte(string macStr)
        {
            byte[] byteMac = null;
            if ((macStr != null))
            {
                string[] strMac = macStr.ToString().Split('.');
                byteMac = new byte[strMac.Length];
                for (int i = 0; i < strMac.Length; i++)
                {
                    byteMac[i] = Convert.ToByte(Convert.ToInt32(strMac[i].ToString(), 16));
                }
            }
            return byteMac;
        }

        private void elapsedMethod(object sender, ElapsedEventArgs eargs)
        {
            send();
        }

        public void Run(object obj)
        {
            time = new System.Timers.Timer();
            time.Elapsed += new ElapsedEventHandler(elapsedMethod);
            time.Interval = 2000;
            time.Enabled = true;
            string strMAC = "";
            string strIP = "";
            byte[] resBuffer = new byte[512];
            int count = 0;
            while (true)
            {
                resBuffer = recive();

                strMAC = string.Format("{0:x000}.{1:x000}.{2:x000}.{3:x000}.{4:x000}.{5:x000}", resBuffer[2].ToString("X"), resBuffer[3].ToString("X"), resBuffer[4].ToString("X"), resBuffer[5].ToString("X"), resBuffer[6].ToString("X"), resBuffer[7].ToString("X"));

                if (strMAC != "")
                {
                    strIP = string.Format("{0:x2}.{1:x000}.{2:x000}.{3:x000}", resBuffer[32].ToString(), resBuffer[33].ToString(), resBuffer[34].ToString(), resBuffer[35].ToString());
                    for (int i = 0; i < listCamera.Count; i++)
                    {
                        Camera cam = new Camera();
                        cam = listCamera[i];
                        if (cam.Mac.Equals(strMAC))
                        {
                            Camera resCam = new Camera();
                            resCam.ID = cam.ID;
                            resCam.IpAddress = strIP;
                            resCam.Name = cam.Name;
                            resCam.Status = true;
                            resCam.Mac = cam.Mac;
                            trueCamera[i] = resCam;
                            config.Cameras = trueCamera.ToList();
                            //config.Save();
                        }
                        else 
                        {
                            count++;
                        }
                    }
                }
            }
        }
    }
}