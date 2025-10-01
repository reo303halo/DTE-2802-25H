using ComputerInventoryAPI.Models;
using ComputerInventoryAPI.Services.AuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ComputerInventoryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, UserManager<IdentityUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> RegisterUser(LoginDto user)
    {
        var identityUser = await _userManager.FindByNameAsync(user.Username);
        if (identityUser != null)
        {
            return Conflict(new
            {
                IsSuccess = false,
                Message = "User already exists. Please log in instead."
            });
        }

        if (await _authService.RegisterUser(user))
        {
            return Ok(new
            {
                IsSuccess = true,
                Message = "User registered successfully!"
            });
        }
        
        return BadRequest(new
        {
            IsSuccess = false,
            Message = "Invalid username or password." // Or any other proper message. Ops...something went wrong...?
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUser(LoginDto user)
    {
        var identityUser = await _userManager.FindByNameAsync(user.Username);
        if (identityUser == null)
        {
            return BadRequest(new
            {
                IsSuccess = false,
                Message = "User does not exist."
            });
        }
        
        if (!ModelState.IsValid)
        {
            return BadRequest(new
            {
                IsSuccess = false,
                Message = "Invalid ModelState..."
            });
        }

        if (!await _authService.LoginUser(user))
        {
            return BadRequest(new
            {
                IsSuccess = false,
                Message = "Invalid username or password."
            });
        }

        return Ok(new
        {
            IsSuccess = true,
            Token = _authService.GenerateTokenString(identityUser),
            Message = "User successfully logged in!"
        });
    }
}