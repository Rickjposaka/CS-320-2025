using Microsoft.AspNetCore.Mvc;  

namespace MyCookBookApi.Controllers  
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRecipe()
        {
            var recipe = new
            {
                id = "1",
                name = "Pancakes",
                category = "Breakfast",
                steps = new[]
                {
                    "Mix all ingredients in a bowl.",
                    "Heat a pan and pour the batter.",
                    "Cook until golden brown on both sides."
                },
                ingredients = new[] { "Flour", "Milk", "Egg", "Butter", "Sugar" }
            };
            return Ok(recipe);
        }
    }  
}  
