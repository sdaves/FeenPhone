﻿using Alienseed.BaseNetworkServer.Network.Telnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace FeenPhone.Server.Telnet
{
    internal class TelnetServer : BaseTelnetServer<TelNetState>
    {
        protected override TelNetState NetstateFactory(System.Net.Sockets.NetworkStream stream, System.Net.EndPoint ep)
        {
            return new TelNetState(stream, ep as IPEndPoint);
        }
    }
}
