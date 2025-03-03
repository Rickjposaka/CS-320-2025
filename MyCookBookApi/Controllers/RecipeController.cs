using Microsoft.AspNetCore.Mvc;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using System.Collections.Generic;

namespace MyCookBookApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        // Constructor with dependency injection
        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // Get all recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAllRecipes()
        {
            return Ok(_recipeService.GetAllRecipes());
        }

        // Get a recipe by its ID
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(string id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        // Search for recipes based on the query
        [HttpPost("search")]
        public ActionResult<IEnumerable<Recipe>> SearchRecipes([FromBody] RecipeSearchRequest searchRequest)
        {
            if (searchRequest == null || string.IsNullOrWhiteSpace(searchRequest.Query))
            {
                return BadRequest("Invalid search request.");
            }

            // Ensure Categories is never null
            searchRequest.Categories ??= new List<CategoryType>();

            var recipes = _recipeService.SearchRecipes(searchRequest);
            return Ok(recipes);
        }

        // Add a new recipe
        [HttpPost]
        public ActionResult<Recipe> CreateRecipe([FromBody] Recipe recipe)
        {
            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
            {
                return BadRequest("Invalid recipe data.");
            }

            // Assign a unique ID to the recipe
            recipe.RecipeId = Guid.NewGuid().ToString();

            _recipeService.AddRecipe(recipe);

            return CreatedAtAction(nameof(GetRecipeById), new { id = recipe.RecipeId }, recipe);
        }
    }
}
