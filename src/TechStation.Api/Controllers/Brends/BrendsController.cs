using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Brends;
using TechStation.Service.DTOs.Products;
using TechStation.Service.Interfaces.Brends;

namespace TechStation.Api.Controllers.Brends;

public class BrendsController : BaseController
{
    private readonly IBrendService brendService;

    public BrendsController(IBrendService brendService)
    {
        this.brendService = brendService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Brendlarni olish
        var brands = await brendService.RetrieveAllAsync(@params);

        // Jami brendlar sonini olish
        var totalCount = await brendService.CountAsync();  // Brendlar sonini olish uchun brendService'dan foydalanamiz

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: brendlar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                brands,
                paginationMetaData
            }
        };

        return Ok(response);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await brendService.RetrieveByIdAsync(id)
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] BrendForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await brendService.AddAsync(dto)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(long id, [FromBody] BrendForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok", 
            Data = await brendService.ModifyAsync(id, dto)
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await brendService.RemoveAsync(id)
        });
    }

    [HttpGet("search-pruducts-by-BrandName")]
    public async Task<IActionResult> RetrieveAllProdutsByBrandAsync(string searchTerm)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await brendService.RetrieveAllProdutsByBrandAsync(searchTerm)
        });
    }


}
