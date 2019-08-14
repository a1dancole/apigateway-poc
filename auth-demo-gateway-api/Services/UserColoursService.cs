using System;
using System.Net.Http;
using System.Threading.Tasks;
using auth_demo_gateway_api.HttpClients;
using auth_demo_gateway_api.Services.Interfaces;
using Newtonsoft.Json;

namespace auth_demo_gateway_api.Services
{
    public class UserColoursService : IUserColourService
    {
        private readonly InternalHttpClient _httpClient;
        public UserColoursService(InternalHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FavouriteColours> GetUserFavouriteColour(string userId)
        {
            try
            {
                var request = await _httpClient.Client.GetAsync($"http://apigateway/usercolourapi/usercolour/{userId}", HttpCompletionOption.ResponseContentRead);
                var response = await request.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<FavouriteColours>(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw e;
            }
        }
    }

    public class FavouriteColours
    {
        public string UserId { get; set; }
        public string FavouriteColour { get; set; }
    }
}
