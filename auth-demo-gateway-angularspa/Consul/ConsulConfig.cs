﻿using System;

namespace auth_demo_gateway_angularspa.Consul
{
    public class ConsulConfig
    {
        public Uri ServiceDiscoveryAddress { get; set; }
        public Uri ServiceAddress { get; set; }
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
    }
}
