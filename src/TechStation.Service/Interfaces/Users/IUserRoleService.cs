using TechStation.Domain.Configurations;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.UserRoles;

namespace TechStation.Service.Interfaces.Users;

public interface IUserRoleService
{
    Task<bool> DeleteUserRoleAsync(long id);
    Task<UserRoleForResultDto> AddUserRoleAsync(UserRoleForCreationDto userRole);
    Task<IEnumerable<UserRoleForResultDto>> GetUserRoleByRoleNameAsync(Role role);
    Task<IEnumerable<UserRoleForResultDto>> GetAllUserRolesAsync(PaginationParams @params);
    Task<UserRoleForResultDto> UpdateUserRoleAsync(long id, UserRoleForUpdateDto userRole);
}
