using BraReintFrontend.Data.Models;

namespace BraReintFrontend.Services.AuthServices;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginDto login);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
}