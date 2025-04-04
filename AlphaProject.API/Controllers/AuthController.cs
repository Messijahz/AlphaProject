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

    [HttpPost("register")]
    public async Task<IActionResult> Register(SignUpFormModel formData)
    {
        if (!ModelState.IsValid)
        {
            return View(formData);
        }

        return RedirectToAction("Login", "Auth");
    }

    [HttpGet("signout")]
    [Authorize]
    public new async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
