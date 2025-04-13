using AlphaProject.API.Models;
using AlphaProject.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.Controllers;


public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(SignInFormModel formData)
    {
        if (!ModelState.IsValid)
        {
            return View(formData);
        }


        var result = await _signInManager.PasswordSignInAsync(formData.Email, formData.Password, isPersistent: false, lockoutOnFailure: false);


        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(formData);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        var formData = new SignUpFormModel();
        return View(formData);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(SignUpFormModel formData)
    {
        if (!ModelState.IsValid)
        {
            return View(formData);
        }

        var newUser = new AppUser
        {
            UserName = formData.Email,
            Email = formData.Email,
            FullName = formData.FullName,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false
        };

        var result = await _userManager.CreateAsync(newUser, formData.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Login", "Auth");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(formData);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Auth");
    }
}
