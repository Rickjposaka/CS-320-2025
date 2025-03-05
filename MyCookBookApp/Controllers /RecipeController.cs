using Microsoft.AspNetCore.Mvc;
using MyCookBookApp.Services;
using System.Threading.Tasks;
using MyCookBookApp.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyCookBookApp.Controllers
{
    [ApiController]
    [Route("Recipe")] // Defines the route for this controller
    public class RecipeController : Controller
    {
        private readonly RecipeService _recipeService;

        public RecipeController(RecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        // Displays the Recipe Index Page
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        // Retrieves all recipes from the API
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetRecipesAsync();
            return Json(recipes); // Returns data in JSON format
        }

        // Retrieves a recipe by its ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(string id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound(new { success = false, message = "Recipe not found" });
            }
            return Json(recipe);
        }

        // Searches for recipes based on a keyword
        [HttpPost("Search")]
        public async Task<IActionResult> SearchRecipes([FromBody] RecipeSearchRequest searchRequest)
        {
            if (searchRequest == null || string.IsNullOrWhiteSpace(searchRequest.Keyword))
            {
                return BadRequest(new { success = false, message = "Invalid search request" });
            }

            var recipes = await _recipeService.SearchRecipesAsync(searchRequest);
            return Json(recipes);
        }

        // Adds a new recipe to the system
        [HttpPost("Add")]
        public async Task<IActionResult> AddRecipe([FromBody] Recipe recipe)
        {
            Console.WriteLine("Received Recipe: " + JsonConvert.SerializeObject(recipe));

            if (recipe == null || string.IsNullOrWhiteSpace(recipe.Name))
            {
                return BadRequest(new { success = false, message = "Invalid recipe data" });
            }

            bool added = await _recipeService.AddRecipeAsync(recipe);
            return Json(new { success = added, message = added ? "Recipe added successfully" : "Failed to add recipe" });
        }
    }
}
