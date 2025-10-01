using ComputerInventoryAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace ComputerInventoryAPI.Services.AuthServices;

public interface IAuthService
{
    Task<bool> RegisterUser(LoginDto user);
    Task<bool> LoginUser(LoginDto user);
    string GenerateTokenString(IdentityUser user);
}