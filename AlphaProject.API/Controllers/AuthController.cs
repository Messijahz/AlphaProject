using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

public class AuthController : Controller
{
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }
}
