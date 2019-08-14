using System;
using System.Linq;
using System.Threading.Tasks;
using auth_demo_gateway_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace auth_demo_gateway_api.Policies
{
    public class AdministratorRequirement : IAuthorizationRequirement { }
    public class Administrator : AuthorizationHandler<AdministratorRequirement>
    {
        private readonly IUserService _userService;
        public Administrator(IUserService userService)
        {
            _userService = userService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            AdministratorRequirement requirement)
        {
            try
            {
                var currentUserGroups = await _userService.GetCurrentUserGroups();

                if (currentUserGroups.Any(o => o.Id == "d9007e03-4cec-40d4-8047-baffaca27388" || o.DisplayName == "ApiDemo:Administrator"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    throw new UnauthorizedAccessException();
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}
