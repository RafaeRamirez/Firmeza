using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firmeza.WebApplication.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    [HttpGet] public IActionResult Index() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => Problem(detail: "An unexpected error occurred.");
}
