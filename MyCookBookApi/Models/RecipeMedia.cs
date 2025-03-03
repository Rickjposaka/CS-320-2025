namespace MyCookBookApi.Models
{
    public class RecipeMedia
    {
        public string Url { get; set; } = string.Empty; // Ensure it is not null
        public string Type { get; set; } = string.Empty; // "image" or "video"
        public int Order { get; set; } // Display order
    }
}
