using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechStation.Domain.Configurations;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.Interfaces.Users;

namespace TechStation.Api.Controllers.Users;

public class UserRoleController : BaseController
{
    private readonly IUserRoleService userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        this.userRoleService = userRoleService;
    }
    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromForm] UserRoleForCreationDto dto)
    {
        var result = await userRoleService.AddUserRoleAsync(dto);
        return  Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var result = await userRoleService.GetAllUserRolesAsync(@params);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByRoleNameAsync(Role role)
    {
        var result = await userRoleService.GetUserRoleByRoleNameAsync(role);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        var result = await userRoleService.DeleteUserRoleAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserRoleForUpdateDto dto)
    {
        var result = await userRoleService.UpdateUserRoleAsync(id, dto);
        return Ok(result);
    }
}
