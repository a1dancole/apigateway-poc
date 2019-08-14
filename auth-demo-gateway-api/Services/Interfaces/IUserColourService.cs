using System.Threading.Tasks;

namespace auth_demo_gateway_api.Services.Interfaces
{
    public interface IUserColourService
    {
        Task<FavouriteColours> GetUserFavouriteColour(string userId);
    }
}