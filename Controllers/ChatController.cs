using Firmeza.WebApplication.Interfaces;
using Firmeza.WebApplication.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Firmeza.WebApplication.Controllers;

[Authorize(Roles = "Admin")] // Ajusta según tu HU
public class ChatController : Controller
{
    private readonly IAiChatService _ai;
    public ChatController(IAiChatService ai) => _ai = ai;

    [HttpGet] public IActionResult Index() => View(model: "");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessages.RequiredName.Replace("Name","Message"));
            return View(model: "");
        }
        try
        {
            var answer = await _ai.AskAsync(userMessage);
            return View(model: answer);
        }
        catch
        {
            ModelState.AddModelError(string.Empty, ErrorMessages.Unexpected);
            return View(model: "");
        }
    }
}
