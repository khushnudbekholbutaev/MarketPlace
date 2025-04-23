
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

//[Authorize]
[Authorize(Roles = "superAdmin")]
public class UserRoleController : BaseController
{
    private readonly IUserRoleService userRoleService;
    private readonly IRepository<UserRole> userRoleRepository;
    private readonly IRepository<User> userRepository;

    public UserRoleController(IUserRoleService userRoleService, IRepository<UserRole> userRoleRepository, IRepository<User> userRepository)
    {
        this.userRoleService = userRoleService;
        this.userRoleRepository = userRoleRepository;
        this.userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromForm] UserRoleForCreationDto dto)
    {
        var user = await userRepository.SelectByIdAsync(dto.UserId);

        if(user == null)
        {
            throw new TechStationException(404, "User not Found!");
        }
        user.Role = dto.Role;
        await userRepository.UpdateAsync(user);

        var result = await userRoleService.AddUserRoleAsync(dto);
        return Ok(result);
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
        var user = await userRepository.SelectByIdAsync(dto.UserId);

        if (user == null)
        {
            throw new TechStationException(404, "User not Found!");
        }
        user.Role = dto.Role;
        await userRepository.UpdateAsync(user);

        var result = await userRoleService.UpdateUserRoleAsync(id, dto);
        return Ok(result);
    }
}