using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Banners;
using TechStation.Service.Interfaces.Banners;
using TechStation.Service.Services.Brends;

namespace TechStation.Api.Controllers.Banners;

public class BannersController : BaseController
{
    private readonly IBannerService bannerService;

    public BannersController(IBannerService bannerService)
    {
        this.bannerService = bannerService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] string? bannerName,
        [FromQuery] string? nameType = "uz")
    {
        // Faqat 'uz' yoki 'ru' bo‘lishini tekshirish
        if (!string.IsNullOrEmpty(nameType) && nameType != "uz" && nameType != "ru")
        {
            return BadRequest(new { message = "nameType faqat 'uz' yoki 'ru' bo‘lishi mumkin" });
        }

        // Bannerlarni olish va filtrlash
        var banners = await bannerService.RetrieveAllAsync(@params, bannerName, nameType);

        // Jami bannerlar sonini olish
        var totalCount = await bannerService.CountAsync();

        // Pagination metadata yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                banners,
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
            Data = await bannerService.RetrieveByIdAsync(id)
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] BannerForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await bannerService.AddAsync(dto)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(long id,[FromBody] BannerForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await bannerService.ModifyAsync(id, dto)
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await bannerService.RemoveAsync(id)
        });
    }
}
