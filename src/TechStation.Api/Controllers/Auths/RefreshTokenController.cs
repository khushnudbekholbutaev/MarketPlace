using Microsoft.AspNetCore.Mvc;
using TechStation.Service.DTOs.Auths;
using TechStation.Service.Interfaces.Auths;

namespace TechStation.Api.Controllers.Auths;

public class RefreshTokenController : BaseController
{
    private readonly IRefreshTokenService refreshTokenService;

    public RefreshTokenController(IRefreshTokenService refreshTokenService)
    {
        this.refreshTokenService = refreshTokenService;
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync(RefreshTokenDto dto)
    {
        var token = await this.refreshTokenService.RefreshTokenAsync(dto);
        return Ok(token);
    }
}
