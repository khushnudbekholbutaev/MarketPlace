using TechStation.Domain.Enums;
using TechStation.Service.DTOs.Users;

namespace TechStation.Service.DTOs.UserRoles;

public class UserRoleForResultDto
{
    public Role Role { get; set; }
    public long UserId { get; set; }
    public UserForResultDto User { get; set; }
}
