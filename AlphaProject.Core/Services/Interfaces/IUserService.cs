namespace AlphaProject.Core.Services.Interfaces;

public interface IUserService
{
    Task<string?> GetCurrentUserFullNameAsync();
}
