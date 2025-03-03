using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyCookBookApp.Models;
using System.Collections.Generic;

namespace MyCookBookApp.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Existing method to retrieve recipes
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:5164/api/recipe");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipe>>(json) ?? new List<Recipe>();
        }

        // New method to search recipes
        public async Task<List<Recipe>> SearchRecipesAsync(string query)
        {
            // Create request data for the search
            var payload = new { Query = query };
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            // Send POST request
            var response = await _httpClient.PostAsync("http://localhost:5164/api/recipe/search", content);

            // Check the status code
            response.EnsureSuccessStatusCode();

            // Deserialize the response and return
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Recipe>>(responseString) ?? new List<Recipe>();
        }
    }
}

