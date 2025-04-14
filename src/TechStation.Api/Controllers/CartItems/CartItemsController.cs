using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.CartItems;
using TechStation.Service.Interfaces.CartItems;
using TechStation.Service.Services.CartItems;
using TechStation.Service.Services.Catalogs;

namespace TechStation.Api.Controllers.CartItems;

public class CartItemsController :  BaseController
{
    private readonly ICartItemService cartItemService;

    public CartItemsController(ICartItemService cartItemService)
    {
        this.cartItemService = cartItemService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // CartItemlarni olish
        var cartItems = await cartItemService.RetrieveAllAsync(@params);

        // Jami CartItemlar sonini olish
        var totalCount = await cartItemService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: CartItemlar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                cartItems,
                paginationMetaData
            }
        };

        return Ok(response);
    }
    [HttpPost("add-cart")]
    public async Task<IActionResult> AddCart(
    [FromBody] CartItemForCreationDto dto,
    [FromQuery] bool token,
    [FromQuery] string operation = null) // "+" yoki "-"
    {
        if (!token)
            return BadRequest("Unauthorized action");

        var result = await cartItemService.AddAsync(dto, token, operation);
        if (result is null)
            return BadRequest("Failed to add or update cart");

        return Ok(result);
    }



    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetByIdAsync([FromRoute] long id)
    //{
    //    return Ok(new Response
    //    {
    //        StatusCode = 200,
    //        Message = "Ok",
    //        Data = await cartItemService.RetrieveByIdAsync(id)
    //    });
    //}
    [HttpDelete("remove/{id}")]
    public async Task<IActionResult> RemoveCartItem(long id, [FromQuery] bool token)
    {
        var result = await cartItemService.RemoveAsync(id, token);

        if (!result)
            return BadRequest("Failed to remove cart item or token is false.");

        return Ok(new { Message = "Cart item successfully removed" });
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> ModifyAsync([FromRoute] long id , [FromBody] CartItemForUpdateDto dto)
    //{
    //    return Ok(new Response
    //    {
    //        StatusCode = 200,
    //        Message = "Ok",
    //        Data = await cartItemService.ModifyAsync(id, dto)
    //    });
    //}
    [HttpDelete("clear")]
    public async Task<IActionResult> ClearAllCartItems([FromQuery] bool clear)
    {
        var result = await cartItemService.ClearAllCartItemsAsync(clear);
        if (!result)
            return BadRequest("Failed to clear cart items or no items to clear.");

        return Ok("All cart items cleared successfully.");
    }

}
