﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace RemoteControlService
{
    public class BindingFactory
    {
        public static NetTcpBinding CreateNetTcpBinding()
        {
            int messageSize = 5 * 1024 * 1024;

            XmlDictionaryReaderQuotas readerQuotas =
                new XmlDictionaryReaderQuotas();
            readerQuotas.MaxArrayLength = messageSize;


            NetTcpBinding tcpBinding = new NetTcpBinding(SecurityMode.None);
            tcpBinding.MaxReceivedMessageSize = messageSize;
            tcpBinding.ReaderQuotas = readerQuotas;

            return tcpBinding;
        }
    }
}
