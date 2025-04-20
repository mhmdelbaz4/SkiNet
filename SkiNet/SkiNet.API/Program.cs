using API.Middlewares;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.RegisterInfrastructureServices();
builder.Services.AddCors();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});
//app.UseGlobalException();
app.UseAuthorization();
app.MapControllers();

try
{
    using  (var scope = app.Services.CreateScope())
    {
        AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await SeedDataContext.SeedDataAsync(context);
    }
}catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

app.Run();
app.Run();
