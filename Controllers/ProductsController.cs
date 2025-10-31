using Firmeza.WebApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firmeza.WebApplication.Controllers;

[Authorize(Roles = "Admin")]
public class ProductsController : Controller
{
    private readonly ProductService _service;
    public ProductsController(ProductService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Index(string? q)
        => View(await _service.SearchAsync(q));

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(string name, decimal unitPrice)
    {
        var result = await _service.CreateAsync(name, unitPrice);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.ErrorMessage!);
            return View();
        }
        return RedirectToAction(nameof(Index));
    }
}
