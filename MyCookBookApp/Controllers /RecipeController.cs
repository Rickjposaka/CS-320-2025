    using Microsoft.AspNetCore.Mvc;
    using MyCookBookApp.Services;
    using System.Threading.Tasks;

    namespace MyCookBookApp.Controllers
    {

        public class RecipeController : Controller
        {
            private readonly RecipeService _recipeService;

            public RecipeController(RecipeService recipeService)
            {
                _recipeService = recipeService;
            }

            // Display all recipes
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var recipes = await _recipeService.GetRecipesAsync();
                return View(recipes);
            }

            // Search action to fetch filtered recipes
            [HttpGet("api/Recipe/search")]
            public async Task<IActionResult> Search(string query)
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return RedirectToAction("Index");
                }

                var recipes = await _recipeService.SearchRecipesAsync(query);
                return View("Index", recipes);
            }
        }
    }
