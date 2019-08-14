﻿using System.Collections.Generic;
using Microsoft.Graph;

namespace auth_demo_gateway.Graph
{
    public class UserWithGroups
    {
        public User User { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
