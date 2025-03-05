using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MyCookBookApi.Models
{
    public class Recipe
    {
        public string RecipeId { get; set; } = Guid.NewGuid().ToString(); // Ensure non-null value
        public string Name { get; set; } = string.Empty; // Default to empty string
        public string TagLine { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public List<string> Instructions { get; set; } = new List<string>();
        public List<string> Ingredients { get; set; } = new List<string>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CategoryType>? Categories { get; set; } = new List<CategoryType>();

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<RecipeMedia>? Media { get; set; } = new List<RecipeMedia>();
    }
}
