using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using auth_demo_gateway_angularspa.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace auth_demo_gateway_angularspa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCurrentUser()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser();
                return Ok(currentUser);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}