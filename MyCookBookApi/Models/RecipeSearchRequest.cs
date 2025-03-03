using System.Collections.Generic;
using MyCookBookApi.Models;

namespace MyCookBookApi.Models
{
    public class RecipeSearchRequest
    {
        public string Keyword { get; set; } = string.Empty; // 🔹 Search keyword, default empty string
        public List<CategoryType> Categories { get; set; } = new List<CategoryType>(); // 🔹 Categories for filtering
    }
}
