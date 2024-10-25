using Microsoft.EntityFrameworkCore;
using SkiNet_API;
using SkiNet_API.Data;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using SkiNet_API.Repos;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<StoreContext>();
    await StoreContextSeed.SeedProductsAsync(context);
}

 app.Run();
