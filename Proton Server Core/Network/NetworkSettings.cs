using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Proton_Server_Core.Network
{
    public class NetworkSettings
    {
        public IPAddress IPAddress { get; set; }
        public IPAddress[] AllowedHosts { get; set; }
        public int Port { get; set; }
        public int ConnectionLimit { get; set; }
        public bool Cryptography { get; set; }
    }
}
