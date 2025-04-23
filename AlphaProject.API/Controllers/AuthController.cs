using AlphaProject.API.Models;
using AlphaProject.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AlphaProject.API.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;


    #region Constructor
    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    #endregion


    #region Local Identity (Register, Login, Logout)

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
            return RedirectToAction("Index", "Projects");
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
    #endregion


    #region External Authentication


    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError(string.Empty, "Provider is required.");
            return View("Login");
        }

        var redirectUrl = Url.Action("ExternalSignInCallback", "Auth", new { returnUrl })!;
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError(string.Empty, $"External provider error: {remoteError}");
            return View("Login");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return RedirectToAction("Login");
        }

        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (signInResult.Succeeded)
        {
            return RedirectToAction("Index", "Projects");
        }
        else
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            try
            {
                firstName = info.Principal.FindFirstValue(ClaimTypes.GivenName)!;
                lastName = info.Principal.FindFirstValue(ClaimTypes.Surname)!;
            }
            catch
            {

            }

            string email = info.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{info.LoginProvider.ToLower()}_{email.ToLower()}";

            var user = new AppUser { UserName = username, Email = email, FullName = $"{firstName} {lastName}" };

            var identityResult = await _userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await _userManager.AddLoginAsync(user, info);
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Projects");
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Login");
        }
    }
    #endregion
}