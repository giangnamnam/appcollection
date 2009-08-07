//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using System.Threading;
//using System.Timers;
//using System.Net;
//using System.Net.Sockets;

//namespace RemoteImaging
//{
//    public class CheckLiveCamera
//    {
//        public CheckLiveCamera()
//        {

//        }

//        private Camera camera;

//        public CheckLiveCamera(Camera cameraddd)
//        {
//            System.Timers.Timer time = new System.Timers.Timer();
//            time.Elapsed += new ElapsedEventHandler(elapsedMethod);
//            time.Interval = 1000;
//            time.Enabled = true;
//            camera = cameraddd;
//            string strMAC = "";
//            string strIP = "";
//            byte[] resBuffer = new byte[512];

//            while (true)
//            {
//                resBuffer = recive();

//                strMAC = string.Format("{0:x000}.{1:x000}.{2:x000}.{3:x000}.{4:x000}.{5:x000}", resBuffer[2].ToString("X"), resBuffer[3].ToString("X"), resBuffer[4].ToString("X"), resBuffer[5].ToString("X"), resBuffer[6].ToString("X"), resBuffer[7].ToString("X"));

//                strIP = string.Format("{0:x2}.{1:x000}.{2:x000}.{3:x000}", resBuffer[32].ToString(), resBuffer[33].ToString(), resBuffer[34].ToString(), resBuffer[35].ToString());

//                if (strMAC != "")
//                {
//                    bool res = false;
//                    if (camera.Mac.Equals(strMAC))
//                    {
//                        Camera resCam = new Camera();
//                        resCam.ID = camera.ID;
//                        resCam.IpAddress = strIP;
//                        resCam.Name = camera.Name;
//                        resCam.Status = true;
//                        resCam.Mac = camera.Mac;
//                        LineCamera = resCam;
//                        res = true;
//                    }
//                    else
//                    {
//                        Camera resCam = new Camera();
//                        resCam.ID = camera.ID;
//                        resCam.IpAddress = camera.IpAddress;
//                        resCam.Name = camera.Name;
//                        resCam.Status = false;
//                        resCam.Mac = camera.Mac;
//                        LineCamera = resCam;
//                        res = true;
//                    }
//                    if (res) break;
//                }
//                else
//                {
//                    time.Start();
//                }
//            }
//        }

//        //发送请求
//        private void send(byte[] byteTemp)
//        {
//            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//            IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 10001);//255.255.255.255

//            byte[] buffer = new byte[512];

//            buffer[0] = 0x40;
//            buffer[1] = 0x31;
//            buffer[2] = byteTemp[0];
//            buffer[3] = byteTemp[1];
//            buffer[4] = byteTemp[2];
//            buffer[5] = byteTemp[3];
//            buffer[6] = byteTemp[4];
//            buffer[7] = byteTemp[5];//0001
//            buffer[8] = 0x00;
//            buffer[9] = 0x01;
//            buffer[10] = 0x00;
//            buffer[11] = 0x00;
//            buffer[12] = 0x00;
//            buffer[13] = 0x01;
//            buffer[14] = 0x00;
//            buffer[15] = 0x01;

//            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
//            sock.SendTo(buffer, iep1);
//            sock.Close();

//            Thread.Sleep(2000);
//        }

//        //发送请求
//        private void send()
//        {
//            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//            IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 10001);//255.255.255.255

//            byte[] buffer = new byte[512];

//            buffer[0] = 0x40;
//            buffer[1] = 0x31;
//            buffer[2] = 0x00;
//            buffer[3] = 0x00;
//            buffer[4] = 0x00;
//            buffer[5] = 0x00;
//            buffer[6] = 0x00;
//            buffer[7] = 0x00;
//            buffer[8] = 0x00;
//            buffer[9] = 0x01;
//            buffer[10] = 0x00;
//            buffer[11] = 0x00;
//            buffer[12] = 0x00;
//            buffer[13] = 0x01;
//            buffer[14] = 0x00;
//            buffer[15] = 0x01;

//            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
//            sock.SendTo(buffer, iep1);
//            sock.Close();

//            Thread.Sleep(2000);
//        }

//        //接收请求
//        private byte[] recive()
//        {
//            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

//            IPEndPoint iep = new IPEndPoint(IPAddress.Any, 10000);

//            sock.Bind(iep);

//            EndPoint ep = (EndPoint)iep;

//            Console.WriteLine("Ready to receive…");

//            byte[] resBuffer = new byte[512];
//            int recv = sock.ReceiveFrom(resBuffer, ref ep);

//            sock.Close();
//            return resBuffer;
//        }

//        //将字符串格式的MAC转为 byte[]
//        private byte[] GetByte(string macStr)
//        {
//            byte[] byteMac = null;
//            if ((macStr != null))
//            {
//                string[] strMac = macStr.ToString().Split('.');
//                byteMac = new byte[strMac.Length];
//                for (int i = 0; i < strMac.Length; i++)
//                {
//                    byteMac[i] = Convert.ToByte(Convert.ToInt32(strMac[i].ToString(), 16));
//                }
//            }
//            return byteMac;
//        }

//        public Camera LineCamera { get; set; }

//        private void elapsedMethod(object sender, ElapsedEventArgs eargs)
//        {
//            send(GetByte(camera.Mac));
//        }
//    }
//}

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

                strIP = string.Format("{0:x2}.{1:x000}.{2:x000}.{3:x000}", resBuffer[32].ToString(), resBuffer[33].ToString(), resBuffer[34].ToString(), resBuffer[35].ToString());

                if (strMAC != "")
                {
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
                            config.Save();
                        }
                    }
                }
            }
        }
    }
}