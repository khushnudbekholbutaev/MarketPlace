using TechStation.Service.DTOs.Auths;

namespace TechStation.Service.Interfaces.Auths;

public interface IRefreshTokenService
{
    Task<LoginResultDto> RefreshTokenAsync(RefreshTokenDto dto);
}
