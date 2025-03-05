namespace MyCookBookApp.Models
{
    public class RecipeSearchRequest
    {
        public string Keyword { get; set; } = string.Empty; // Ensure it is never null
        public List<CategoryType> Categories { get; set; } = new List<CategoryType>();
    }
}

