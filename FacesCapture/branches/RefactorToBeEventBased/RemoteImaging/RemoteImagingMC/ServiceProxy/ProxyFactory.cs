using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using RemoteControlService;
using System.Xml;

namespace RemoteImaging.ServiceProxy
{
    public class ProxyFactory
    {

        public static IServiceFacade CreateProxy(string uri)
        {
            EndpointAddress ep = new EndpointAddress(uri);

            NetTcpBinding tcpBinding = BindingFactory.CreateNetTcpBinding();


            IServiceFacade proxy = ChannelFactory<IServiceFacade>.CreateChannel(tcpBinding, ep);
            return proxy;
        }

    }
}
