using TechStation.Domain.Enums;

namespace TechStation.Service.DTOs.UserRoles;

public class UserRoleForUpdateDto
{
    public long UserId { get; set; }
    public Role Role { get; set; }
}
