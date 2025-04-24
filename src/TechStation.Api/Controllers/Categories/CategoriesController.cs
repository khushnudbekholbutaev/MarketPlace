using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Categories;
using TechStation.Service.Interfaces.Categories;

namespace TechStation.Api.Controllers.Categories;

public class CategoriesController : BaseController
{
    private readonly ICategoryService categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }
    [HttpPost]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> InsertAsync([FromBody] CategoryForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await categoryService.AddAsync(dto)
        });
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Kategoriyalarni olish
        var categories = await categoryService.RetrieveAllAsync(@params);

        // Jami kategoriyalar sonini olish
        var totalCount = await categoryService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: kategoriyalar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                categories,
                paginationMetaData
            }
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await categoryService.RetrieveByIdAsync(id)
        });
    }
    [HttpDelete("{id}")]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> DeteleAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await categoryService.RemoveAsync(id)
        });
    }
    [HttpPut("{id}")]
    //[Authorize(Roles = "admin,superAdmin")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CategoryForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await categoryService.ModifyAsync(id, dto)
        });
    }
}
