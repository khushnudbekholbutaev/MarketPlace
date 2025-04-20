using Microsoft.AspNetCore.Mvc;
using TechStation.Service.DTOs.Auths;
using TechStation.Service.Interfaces.Auths;

namespace TechStation.Api.Controllers.Auths;

public class AuthController : BaseController
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }
    [HttpPost("authenticate")]

    public async Task<IActionResult> PostAsync(LoginDto dto)
    {
        var token = await authService.AuthenticateAsync(dto);
        return Ok(token);
    }

    [HttpPost("logout/{id}")]
    public async Task<IActionResult> LogoutAsync([FromBody] long userId)
    {
        await authService.LogoutAsync(userId);
        return Ok(new { Message = "User logged out successfully." });
    }
}
