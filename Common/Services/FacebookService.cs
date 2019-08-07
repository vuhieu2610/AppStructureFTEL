using EntityData;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppOutSideAPI.Services
{
    public class FacebookService
    {
        private readonly HttpClient _httpClient;

        public FacebookService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/v3.3/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<User> GetUserFromFacebookAsync(string facebookToken)
        {
            var result = await GetDataAsync<dynamic>(facebookToken, "me", "fields=id,first_name,last_name,email,picture.type(large)");
            User user = null;

            if (result != null)
            {
                user = new User()
                {
                    Username = result.id,
                    FirstName = result.first_name,
                    LastName = result.last_name,
                    Email = result.email,
                    AvatarUrl = result.picture.data.url,
                    Role = "1"
                };
            }
            return user;
        }

        public async Task<User> GetLoginInfoFromFacebookAsync(string facebookToken)
        {
            var result = await GetDataAsync<dynamic>(facebookToken, "me", "fields=id,email");
            User user = null;
            if (result != null)
            {
                user = new User()
                {
                    Username = result.id,
                    Email = result.email,
                    Role = "1"
                };
            }
            return user;
        }

        private async Task<T> GetDataAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
