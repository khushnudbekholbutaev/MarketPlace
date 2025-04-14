using TechStation.Service.DTOs.Auths;

namespace TechStation.Service.Interfaces.Auths;

public interface IAuthService
{
    Task<LoginResultDto> AuthenticateAsync(LoginDto dto);
}
