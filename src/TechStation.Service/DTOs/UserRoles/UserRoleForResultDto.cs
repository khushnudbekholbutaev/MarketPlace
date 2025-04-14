using TechStation.Domain.Enums;

namespace TechStation.Service.DTOs.UserRoles;

public class UserRoleForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public Role Role { get; set; }
}
