using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.DTOs.Users;

namespace TechStation.Service.Interfaces.Users;

public interface IUserService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdasync(long id);
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id,UserForUpdateDto dto);
    Task<UserRoleForResultDto> AssignRoleToUser(UserRoleForCreationDto dto);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
