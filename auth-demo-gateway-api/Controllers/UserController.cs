using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using auth_demo_gateway_api.Policies;
using auth_demo_gateway_api.Services;
using auth_demo_gateway_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace auth_demo_gateway_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserColourService _userColourService;

        public UserController(IUserService userService, IUserColourService userColourService)
        {
            _userService = userService;
            _userColourService = userColourService;
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

        [HttpGet("GetCurrentUserGroups")]
        [Authorize(Policy = nameof(Policy.Administrator))]
        [ProducesResponseType(typeof(IEnumerable<Group>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetCurrentUserGroups()
        {
            try
            {
                var currentUserGroups = await _userService.GetCurrentUserGroups();

                return Ok(currentUserGroups);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
        }

        [HttpGet("GetCurrentUsersFavouriteColour")]
        [ProducesResponseType(typeof(FavouriteColours), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> GetCurrentUsersFavouriteColour()
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser();

                var usersFavouriteColour =
                    await _userColourService.GetUserFavouriteColour(currentUser.Id);

                return Ok(usersFavouriteColour);
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace);
            }
        }
    }
}