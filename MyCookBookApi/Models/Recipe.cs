using System.Text.Json.Serialization; // For JSON serialization
using System.Collections.Generic; // For using List<T> collections

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; } // Auto-generated unique ID
        public string Name { get; set; } // Recipe name
        public string TagLine { get; set; } // Short tagline for the recipe
        public string Summary { get; set; } // Brief description of the recipe
        public List<string> Instructions { get; set; } = new List<string>(); // Step-by-step instructions
        public List<string> Ingredients { get; set; } = new List<string>(); // List of ingredients

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryType>? Categories { get; set; } = new List<CategoryType>(); // Optional categories

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>(); // Optional media (images/videos)

        public Recipe() {} // Default constructor
    }
}
