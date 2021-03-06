﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace auth_demo_gateway_angularspa.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
        Task<IEnumerable<Group>> GetCurrentUserGroups();
    }
}