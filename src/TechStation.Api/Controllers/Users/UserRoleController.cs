using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Controllers;
using TechStation.Domain.Configurations;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.Interfaces.Users;
using TechStation.Service.Exceptions;
using TechStation.Data.IRepositories;
using TechStation.Domain.Entities;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class UserRoleController : BaseController
{
    private readonly IUserRoleService userRoleService;
    private readonly IRepository<UserRole> userRoleRepository;

    public UserRoleController(IUserRoleService userRoleService, IRepository<UserRole> userRoleRepository)
    {
        this.userRoleService = userRoleService;
        this.userRoleRepository = userRoleRepository;
    }

    private async Task<bool> IsUserInRoleAsync(long userId, Role role)
    {
        var userRoles = await userRoleRepository.SelectAll()
            .Where(ur => ur.UserId == userId && ur.Role == role)
            .ToListAsync();

        return userRoles.Any();
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromForm] UserRoleForCreationDto dto)
    {
        var currentUserId = GetCurrentUserId();
        if (!(await IsUserInRoleAsync(currentUserId, Role.admin) || await IsUserInRoleAsync(currentUserId, Role.superAdmin)))
        {
            return Unauthorized("You do not have permission to perform this action.");
        }

        var result = await userRoleService.AddUserRoleAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var currentUserId = GetCurrentUserId();
        if (!(await IsUserInRoleAsync(currentUserId, Role.admin) || await IsUserInRoleAsync(currentUserId, Role.superAdmin)))
        {
            return Unauthorized("You do not have permission to perform this action.");
        }

        var result = await userRoleService.GetAllUserRolesAsync(@params);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByRoleNameAsync(Role role)
    {
        var currentUserId = GetCurrentUserId();
        if (!(await IsUserInRoleAsync(currentUserId, Role.admin) || await IsUserInRoleAsync(currentUserId, Role.superAdmin)))
        {
            return Unauthorized("You do not have permission to perform this action.");
        }

        var result = await userRoleService.GetUserRoleByRoleNameAsync(role);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        var currentUserId = GetCurrentUserId();
        if (!(await IsUserInRoleAsync(currentUserId, Role.admin) || await IsUserInRoleAsync(currentUserId, Role.superAdmin)))
        {
            return Unauthorized("You do not have permission to perform this action.");
        }

        var result = await userRoleService.DeleteUserRoleAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserRoleForUpdateDto dto)
    {
        var currentUserId = GetCurrentUserId();
        if (!(await IsUserInRoleAsync(currentUserId, Role.admin) || await IsUserInRoleAsync(currentUserId, Role.superAdmin)))
        {
            return Unauthorized("You do not have permission to perform this action.");
        }

        var result = await userRoleService.UpdateUserRoleAsync(id, dto);
        return Ok(result);
    }

    private long GetCurrentUserId()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
        return long.Parse(userIdClaim?.Value ?? "0");
    }
}
