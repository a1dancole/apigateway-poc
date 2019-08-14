using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using auth_demo_gateway.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using ClientCredential = Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential;

namespace auth_demo_gateway.Graph
{
    public class GraphService : IGraphService
    {
        private readonly GraphConfig _config;
        private readonly IDistributedCache _cache;
        public GraphService(IConfiguration configuration, IDistributedCache cache)
        {
            _config = new GraphConfig
            {
                ClientId = configuration.GetValue<string>("Graph:ClientId"),
                Resource = configuration.GetValue<Uri>("Graph:Resource"),
                Secret = configuration.GetValue<string>("Graph:Secret"),
                Tenant = configuration.GetValue<string>("Graph:Tenant")
            };

            _cache = cache;
        }

        public async Task<UserWithGroups> GetCurrentUser(HttpContext context)
        {
            var currentUserId = context.User.Claims.FirstOrDefault(o => o.Type == ClaimTypesExtended.ObjectIdentifier).Value ?? throw new UnauthorizedAccessException();

            var cachedResponse = await GetCachedUser(currentUserId);

            if (cachedResponse != null) return cachedResponse;

            var token = await GetGraphToken();

            var client = new GraphServiceClient(new DelegateAuthenticationProvider(async (request) =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }));

            var user = await client.Users[currentUserId].Request().GetAsync();
            var groups = await GetGroupsForUser(user);

            var response = new UserWithGroups
            {
                User = user,
                Groups = groups
            };

            await _cache.SetStringAsync(currentUserId, JsonConvert.SerializeObject(response));

            return await Task.FromResult(response);
        }

        private async Task<string> GetGraphToken()
        {
            var authority = $"https://login.microsoftonline.com/{_config.Tenant}";
            var authContext = new AuthenticationContext(authority);
            var credentials = new ClientCredential(_config.ClientId, _config.Secret);
            var authResult = await authContext.AcquireTokenAsync(_config.Resource.AbsoluteUri, credentials);

            return await Task.FromResult(authResult.AccessToken);
        }

        private async Task<IEnumerable<Group>> GetGroupsForUser(User currentUser)
        {
            var token = await GetGraphToken();

            var client = new GraphServiceClient(new DelegateAuthenticationProvider(async (request) =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }));

            return await GetGroupDirectoryObjectsForUser(client, currentUser);
        }

        private async Task<IEnumerable<Group>> GetGroupDirectoryObjectsForUser(GraphServiceClient client, User user)
        {
            var groupObjectIds = await client.Users[user.Id].GetMemberGroups(true).Request().PostAsync();
            var groups = await client.DirectoryObjects.GetByIds(groupObjectIds).Request().PostAsync();

            return await Task.FromResult(groups.ToList().OfType<Group>());
        }

        private async Task<UserWithGroups> GetCachedUser(string key)
        {
            var result = await _cache.GetStringAsync(key);
            return await Task.FromResult(result != null ? JsonConvert.DeserializeObject<UserWithGroups>(result) : null);
        }
    }
}
