// Agrega temporalmente en HomeController para probar la BD y borra luego.
using Firmeza.WebApplication.Data;
using Microsoft.AspNetCore.Mvc;

namespace Firmeza.WebApplication.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    public HomeController(AppDbContext db) => _db = db;

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public async Task<IActionResult> HealthDb()
    {
        // Simple DB ping: returns "OK" if DB is reachable.
        var ok = await _db.Database.CanConnectAsync();
        return Content(ok ? "DB OK" : "DB FAIL");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => Problem(detail: "An unexpected error occurred.");
}
