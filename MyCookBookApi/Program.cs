using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using MyCookBookApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Register services and repositories
builder.Services.AddSingleton<IRecipeRepository, MockRecipeRepository>(); // In-memory recipe storage
builder.Services.AddScoped<IRecipeService, RecipeService>(); // Service to manage recipes

// Add controllers and configure JSON serialization for enums
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Convert enums to string in JSON responses
});

// Enable API documentation with Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // Enable Swagger UI in development mode
}

// app.UseHttpsRedirection(); // Optional: Enable if HTTPS redirection is required

app.UseRouting(); // Enable routing
app.UseAuthorization(); // Enable authorization

// Configure controller routes
app.MapControllers();

app.Run();
