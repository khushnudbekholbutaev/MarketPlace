using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Favourites;
using TechStation.Service.Interfaces.Favourites;
using TechStation.Service.Services.Catalogs;
using TechStation.Service.Services.Favourites;

namespace TechStation.Api.Controllers.Favourites;

public class FavouritesController : BaseController
{
    private readonly IFavouriteService favouriteService;

    public FavouritesController(IFavouriteService favouriteService)
    {
        this.favouriteService = favouriteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Favouritelarni olish
        var favourites = await favouriteService.RetrieveAllAsync(@params);

        // Jami Favouritelar sonini olish
        var totalCount = await favouriteService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: Favouritelar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                favourites,
                paginationMetaData
            }
        };

        return Ok(response);
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddFavourite([FromBody] FavouriteForCreationDto dto, [FromQuery] bool token)
    {
        var result = await favouriteService.AddAsync(dto, token);
        if (result == null)
            return BadRequest("Failed to add favourite item.");

        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await favouriteService.RetrieveByIdAsync(id)
        });
    }
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveFavourite(long id, [FromQuery] bool token)
    {
        var result = await favouriteService.RemoveAsync(id, token);
        if (!result)
            return BadRequest("Failed to remove favourite item or token is false.");

        return Ok(new { Message = "Favourite successfully deleted" });
    }
    [HttpDelete("clear")]
    public async Task<IActionResult> ClearAllFavourites([FromQuery] bool clear)
    {
        var result = await favouriteService.ClearAllFavouritesAsync(clear);
        if (!result)
            return BadRequest("Failed to clear favourites or no items to clear.");

        return Ok("All favourites cleared successfully.");
    }
}
