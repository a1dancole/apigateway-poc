using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace auth_demo_gateway.Graph
{
    public interface IGraphService
    {
        Task<UserWithGroups> GetCurrentUser(HttpContext context);
    }
}