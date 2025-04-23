using AlphaProject.API.ViewModels;
using AlphaProject.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlphaProject.API.ViewComponents;

public class UserAvatarViewComponent : ViewComponent
{
    private readonly UserManager<AppUser> _userManager;

    public UserAvatarViewComponent(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var user = await _userManager.GetUserAsync(HttpContext.User);
        var viewModel = new UserMenuViewModel
        {
            FullName = user?.FullName ?? "Guest",
            ProfileImageUrl = user?.ProfileImageUrl ?? "/images/profile-icon-small-1.svg"
        };
        return View(viewModel);
    }
}
