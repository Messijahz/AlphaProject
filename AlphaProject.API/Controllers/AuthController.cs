using AlphaProject.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

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

    [HttpPost("signout")]
    [Authorize]
    public async Task<IActionResult> LogOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
