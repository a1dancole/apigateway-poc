using System;

namespace auth_demo_gateway.Graph
{
    public class GraphConfig
    {
        public string Tenant { get; set; }
        public Uri Resource { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
    }
}
