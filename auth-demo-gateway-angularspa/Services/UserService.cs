using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_demo_gateway_angularspa.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace auth_demo_gateway_angularspa.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _context;
        public UserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public async Task<User> GetCurrentUser()
        {
            var currentUser = await GetCurrentUserWithGroups();

            return await Task.FromResult(currentUser.User);
        }

        public async Task<IEnumerable<Group>> GetCurrentUserGroups()
        {
            var currentUser = await GetCurrentUserWithGroups();

            return await Task.FromResult(currentUser.Groups);
        }

        private async Task<UserWithGroups> GetCurrentUserWithGroups()
        {
            var authHeader = _context.HttpContext.Request.GetTypedHeaders().Headers["WD_USER"].ToString();

            if (string.IsNullOrEmpty(authHeader)) throw new UnauthorizedAccessException($"Invalid Headers. This request has not been authenticated correctly.");

            var applicationUser = JsonConvert.DeserializeObject<UserWithGroups>(authHeader);
            return await Task.FromResult(applicationUser);
        }
    }
}
