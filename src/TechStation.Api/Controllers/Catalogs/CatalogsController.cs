using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Catalogs;
using TechStation.Service.Interfaces.Catalogs;
using TechStation.Service.Services.Categories;

namespace TechStation.Api.Controllers.Catalogs;

public class CatalogsController : BaseController
{
    private readonly ICatalogService catalogService;
    public CatalogsController(ICatalogService catalogService)
    {
        this.catalogService = catalogService;
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] CatalogForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await catalogService.AddAsync(dto)
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Kategoriyalarni olish
        var catalogs = await catalogService.RetrieveAllAsync(@params);

        // Jami kategoriyalar sonini olish
        var totalCount = await catalogService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: kategoriyalar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                catalogs,
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
            Data = await catalogService.RetrieveByIdAsync(id)
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await catalogService.RemoveAsync(id)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] CatalogForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await catalogService.ModifyAsync(id, dto)
        });
    }
}
