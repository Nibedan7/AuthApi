using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuthUi.ApiHelper
{
    
    public class ApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiHelper(string baseUrl, string token)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;

            // Add the JWT token to the request headers
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        // GET request method
        public async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");

            if (!response.IsSuccessStatusCode)
            {
                // Handle errors here, you can throw exceptions or return default values.
                throw new Exception($"Error fetching data from API. Status code: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        // POST request method
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error posting data to API. Status code: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        // PUT request method
        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{endpoint}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating data. Status code: {response.StatusCode}");
            }

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        // DELETE request method
        public async Task DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{endpoint}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting data. Status code: {response.StatusCode}");
            }
        }
    }
}
