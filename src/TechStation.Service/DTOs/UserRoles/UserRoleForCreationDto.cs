using TechStation.Domain.Enums;

namespace TechStation.Service.DTOs.UserRoles;

public class UserRoleForCreationDto
{
    public long UserId { get; set; }
    public Role Role { get; set; }
}
