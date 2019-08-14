using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace auth_demo_gateway_api.HttpClients
{
    public class InternalHttpClient
    {
        public HttpClient Client { get; }

        public InternalHttpClient(HttpClient client, IHttpContextAccessor context)
        {
            Client = client;

            var bearerToken = context.HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault(h => h.StartsWith("bearer ", StringComparison.InvariantCultureIgnoreCase));

            if (bearerToken != null)
                client.DefaultRequestHeaders.Add("Authorization", bearerToken);
        }
    }
}
