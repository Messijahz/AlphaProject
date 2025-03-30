using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

[Route("admin")]
public class AdminController : Controller
{
    [Route("members")]
    public IActionResult Members()
    {
        return View();
    }

    [Route("projects")]
    public IActionResult Projects()
    {
        return View();
    }
}
