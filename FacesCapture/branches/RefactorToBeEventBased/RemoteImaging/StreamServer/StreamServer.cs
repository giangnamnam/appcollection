using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using RemoteControlService;


namespace StreamServer
{
    [ServiceBehavior(
        InstanceContextMode=InstanceContextMode.Single,
        IncludeExceptionDetailInFaults=true
        )]
    public partial class StreamServer : Form, RemoteControlService.IStreamPlayer
    {
        public StreamServer()
        {
            InitializeComponent();

            this.listBox1.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string baseAddress = string.Format("net.tcp://{0}:8001", System.Net.IPAddress.Any);

            Uri netTcpBaseAddress = new Uri(baseAddress);
            ServiceHost host = new ServiceHost(this, netTcpBaseAddress);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();

            host.AddServiceEndpoint(typeof(RemoteControlService.IStreamPlayer),
                tcpBinding, "TcpService");

            host.Open();


        }

        private void StreamServer_Shown(object sender, EventArgs e)
        {
#if DEBUG
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
#endif

        }

        #region IStreamPlayer Members

        private void Log(string msg)
        {
            this.listBox1.Items.Insert(0, msg);
        }
        public void Stop()
        {
            Log("stop");

            this.axVLCPlugin21.playlist.stop();
        }

        public bool StreamVideo(string path)
        {
            Log("play " + path);

            string mrl = string.Format("file://{0}", path); 
            string[] options =  new string[] {"-vvv", ":sout=udp:239.255.12.12", ":ttl=1"};


            this.axVLCPlugin21.playlist.items.clear();
            int idx = this.axVLCPlugin21.playlist.add(mrl, null, options);
            this.axVLCPlugin21.playlist.playItem(idx);

            return true;
        }

        #endregion
    }
}
