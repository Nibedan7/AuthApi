using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
namespace AuthUi.ApiHelper
{

    public class UserAuthApiHelper
    {
        private readonly HttpClient _httpClient;

        public UserAuthApiHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Register method
        public async Task<bool> RegisterAsync(string name, string email, string password)
        {
            var registerModel = new
            {
                Name = name,
                Email = email,
                Password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");  
            var response = await _httpClient.PostAsync("https://localhost:7259/api/UserAuth/Register", content);

            return response.IsSuccessStatusCode;
        }

        // Login method
        public async Task<string> LoginAsync(string email, string password)
        {
            var loginModel = new
            {
                Email = email,
                Password = password
            };

            var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7259/api/UserAuth/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // Extract token from the response (you can use a custom model here)
                dynamic jsonResponse = JsonConvert.DeserializeObject(result);
                return jsonResponse.token;
            }

            return null;
        }

        // Logout method
        public async Task<bool> LogoutAsync()
        {
            var response = await _httpClient.PostAsync("https://localhost:7259/api/UserAuth/Logout", null);
            return response.IsSuccessStatusCode;
        }
    }
}
