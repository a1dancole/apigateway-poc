using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace auth_demo_gateway_api_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserColourController : ControllerBase
    {
        private readonly List<FavouriteColours> _userFavouriteColours = new List<FavouriteColours>
        {
            new FavouriteColours
            {
                UserId = "0473ea46-52f4-43d4-a286-aea01385979b",
                FavouriteColour = "Blue"
            },
            new FavouriteColours
            {
                UserId = "4e446641-2dde-4da8-8eac-febc0414eafb",
                FavouriteColour = "Pink"
            }
        };

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FavouriteColours), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCurrentUser(string id)
        {
            var response = _userFavouriteColours.Find(o => o.UserId == id);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }

    public class FavouriteColours
    {
        public string UserId { get; set; }
        public string FavouriteColour { get; set; }
    }
}
