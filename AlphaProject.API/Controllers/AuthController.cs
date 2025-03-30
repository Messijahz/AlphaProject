using AlphaProject.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

public class AuthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        var formData = new SignUpFormModel();
        return View(formData);
    }

    [HttpGet("signout")]
    [Authorize]
    public async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
