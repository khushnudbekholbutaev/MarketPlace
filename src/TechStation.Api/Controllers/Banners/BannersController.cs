using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        var result = await bannerService.RetrieveAllAsync(@params);
        return Ok(result);
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
    //[Authorize(Roles = "admin,superAdmin")]
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
    //[Authorize(Roles = "admin,superAdmin")]
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
    //[Authorize(Roles = "admin,superAdmin")]
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
