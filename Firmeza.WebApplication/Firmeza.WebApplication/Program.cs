using Firmeza.WebApplication.Data;
using Firmeza.WebApplication.Interfaces;
using Firmeza.WebApplication.Repositories;
using Firmeza.WebApplication.Services;
using Firmeza.WebApplication.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DEV: load .env (DotNetEnv). PROD: only environment variables.
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load(); // dotnet add package DotNetEnv
}

// Secure connection string: ENV -> appsettings -> error
string? cs =
    Environment.GetEnvironmentVariable("ConnectionStrings__Default")
    ?? builder.Configuration.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Missing DB connection string");

// DbContext (PostgreSQL) with retries
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(cs, npg => npg.EnableRetryOnFailure()));

// DI: repos, services, utils, AI client
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IStringSanitizer, StringSanitizer>();
builder.Services.AddHttpClient<IAiChatService, GeminiAiService>();

// MVC + Views
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
else app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Default route: Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
