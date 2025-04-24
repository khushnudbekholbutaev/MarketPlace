using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.UserRoles;
using TechStation.Service.DTOs.Users;
using TechStation.Service.Interfaces.Users;
using TechStation.Service.Services.Users;

namespace TechStation.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }
    /// <summary>
    /// Handles HTTP POST requests to insert a new user into the database.
    /// </summary>
    /// <param name="dto">Data for creating the new user.</param>
    /// <returns>Returns an IActionResult with the result of the insertion operation.</returns>
    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] UserForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userService.AddAsync(dto)
        });
    }


    /// <summary>
    /// Handles HTTP GET requests to retrieve all users with optional pagination parameters.
    /// </summary>
    /// <param name="@params">Optional pagination parameters for controlling the result set.</param>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Foydalanuvchilarni (User) olish
        var users = await userService.RetrieveAllAsync(@params);

        // Jami foydalanuvchilar sonini olish
        var totalCount = await userService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: foydalanuvchilar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                users,
                paginationMetaData
            }
        };

        return Ok(response);
    }



    /// <summary>
    /// Handles HTTP GET requests to retrieve a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be retrieved.</param>
    /// <returns>Returns an IActionResult with the result of the retrieval operation.</returns>
    [HttpGet("{id}")]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userService.RetrieveByIdasync(id)
        });
    }


    /// <summary>
    /// Handles HTTP DELETE requests to remove a user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be removed.</param>
    /// <returns>Returns an IActionResult with the result of the removal operation.</returns>
    [HttpDelete("{id}")]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userService.RemoveAsync(id)
        });
    }
    /// <summary>
    /// Handles HTTP PUT requests to update an existing user by their unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the user to be updated.</param>
    /// <param name="dto">Data for updating the existing user.</param>
    /// <returns>Returns an IActionResult with the result of the update operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UserForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await userService.ModifyAsync(id, dto)
        });
    }

    [HttpPost("assign-role")]
    //[Authorize]
    public async Task<IActionResult> AssignRole([FromBody] UserRoleForCreationDto dto)
    {

        var result = await userService.AssignRoleToUser(dto);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Role assigned successfully",
            Data = result
        });
    }
}
