document.addEventListener("DOMContentLoaded", function () {
    const BASE_URL = `${window.location.origin}/Recipe`; // Dynamically set base URL
    let selectedCategories = [];

    // Mapping of category IDs to their names
    const categoryMapping = {
        0: "Breakfast", 1: "Lunch", 2: "Dinner", 3: "Dessert",
        4: "Snack", 5: "Vegan", 6: "Vegetarian", 7: "GlutenFree",
        8: "Keto", 9: "LowCarb", 10: "HighProtein"
    };

    fetchRecipes();

    // Handle category selection
    document.querySelectorAll("#categoryList .dropdown-item").forEach(item => {
        item.addEventListener("click", function (event) {
            event.preventDefault();
            let categoryValue = parseInt(this.getAttribute("data-value"), 10);
            let categoryText = this.innerText;

            if (selectedCategories.includes(categoryValue)) {
                selectedCategories = selectedCategories.filter(c => c !== categoryValue);
            } else {
                selectedCategories.push(categoryValue);
            }

            // Update button text
            document.getElementById("categoryDropdown").innerText =
                selectedCategories.length > 0
                    ? selectedCategories.map(id => document.querySelector(`[data-value="${id}"]`).innerText).join(", ")
                    : "Select Categories";

            // Update hidden input
            document.getElementById("categories").value = JSON.stringify(selectedCategories);
            console.log("Selected Categories:", selectedCategories);
        });
    });

    // Open the Add Recipe Modal
    document.getElementById("addRecipeBtn")?.addEventListener("click", function () {
        document.getElementById("addRecipeForm").reset();
        $('#addRecipeModal').modal('show');
    });

    // Save Recipe function
    document.getElementById("saveRecipe").addEventListener("click", function (event) {
        event.preventDefault();

        let recipe = {
            recipeId: "NewRecipe_Only",
            name: document.getElementById("recipeName").value.trim(),
            tagLine: document.getElementById("tagLine").value.trim(),
            summary: document.getElementById("summary").value.trim(),
            ingredients: document.getElementById("ingredients").value.split(",").map(i => i.trim()),
            instructions: document.getElementById("instructions").value.split("\n").map(i => i.trim()),
            categories: JSON.parse(document.getElementById("categories").value || "[]"),
            media: []
        };

        console.log("Final Recipe Object:", recipe);

        fetch(`${BASE_URL}/Add`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(recipe)
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    console.log("Recipe added successfully!");
                    let modal = new bootstrap.Modal(document.getElementById('addRecipeModal'));
                    modal.hide();
                    setTimeout(() => {
                        location.reload();
                    }, 500);
                } else {
                    alert("Failed to add recipe: " + data.message);
                }
            })
            .catch(error => console.error("Error adding recipe:", error));
    });

    // Fetch all recipes
    function fetchRecipes() {
        console.log("Fetching all recipes...");
        fetch(`${BASE_URL}/GetAll`)
            .then(response => response.json())
            .then(recipes => {
                console.log("Recipes loaded:", recipes);
                displayRecipes(recipes);
            })
            .catch(error => console.error("Fetch error:", error));
    }

    // Search for recipes
    window.searchRecipes = function () {
        let keyword = document.getElementById("searchInput").value.trim();
        if (!keyword) {
            fetchRecipes();
            return;
        }

        let searchRequest = { keyword: keyword, categories: [] };
        fetch(`${BASE_URL}/Search`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(searchRequest)
        })
            .then(response => response.json())
            .then(recipes => {
                displayRecipes(recipes);
            })
            .catch(error => console.error("Search error:", error));
    };

    // Handle Enter key for search
    window.handleEnter = function (event) {
        if (event.key === "Enter") {
            searchRecipes();
        }
    };

    // Show or hide the clear search button
    window.toggleClearButton = function () {
        let searchInput = document.getElementById("searchInput");
        let clearButton = document.getElementById("clearSearch");
        clearButton.style.display = searchInput.value ? "block" : "none";
    };

    // Clear search
    window.clearSearch = function () {
        document.getElementById("searchInput").value = "";
        document.getElementById("clearSearch").style.display = "none";
        fetchRecipes();
    };

    // Display recipes dynamically
    function displayRecipes(recipes) {
        let cardContainer = document.getElementById("recipeCards");
        cardContainer.innerHTML = "";

        recipes.forEach(recipe => {
            let imageUrl = recipe.media && recipe.media.length > 0 ? recipe.media[0].url : "placeholder.jpg";
            let categoriesText = (recipe.categories || []).map(catId => categoryMapping[catId] || "Unknown").join(" | ");

            let card = `<div class="mb-3">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="d-flex justify-content-between align-items-center">
                            <div>
                                <h5 class="card-title mb-0">${recipe.name}</h5>
                                <p class="text-muted mb-2">${recipe.tagLine || ""}</p>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-md-6">
                                <h6>Chef's Note</h6>
                                <p class="card-text">${recipe.summary || "No summary available."}</p>
                                <h6>Ingredients</h6>
                                <p class="card-text">${recipe.ingredients ? recipe.ingredients.join(", ") : "N/A"}</p>
                            </div>
                            <div class="col-md-6">
                                <h6>Instructions</h6>
                                <p class="card-text">${recipe.instructions ? recipe.instructions.join("<br>") : "N/A"}</p>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer text-muted text-left" style="font-size: 0.85rem;">
                        <b>Categories: </b>${categoriesText}
                    </div>
                </div>
            </div>`;
            cardContainer.innerHTML += card;
        });
    }
});
