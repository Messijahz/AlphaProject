using AlphaProject.Core.Models;
using AlphaProject.Core.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AlphaProject.API.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<string?> GetCurrentUserFullNameAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        return user?.FullName;
    }
}
