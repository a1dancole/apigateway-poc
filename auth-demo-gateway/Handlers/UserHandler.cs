using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using auth_demo_gateway.Graph;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace auth_demo_gateway.Handlers
{
    public class UserHandler : DelegatingHandler
    {
        private readonly IGraphService _graphService;
        private readonly IHttpContextAccessor _context;

        public UserHandler(IGraphService graphService, IHttpContextAccessor context)
        {
            _graphService = graphService;
            _context = context;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var user = await _graphService.GetCurrentUser(_context.HttpContext);

            request.Headers.Add("WD_USER", JsonConvert.SerializeObject(user));

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
