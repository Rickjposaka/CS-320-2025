using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using MyCookBookApi.Models;
using MyCookBookApi.Services;
using MyCookBookApi.Repositories;
using Microsoft.OpenApi.Models; // ✅ Add this for Swagger

var builder = WebApplication.CreateBuilder(args);

// Register Services and Repositories
builder.Services.AddSingleton<IRecipeRepository, MockRecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

// Add Swagger Support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyCookBook API", Version = "v1" });
});

// Add controllers and ensure enums are properly serialized as strings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();  // ✅ Enable Swagger
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyCookBook API v1"));
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
