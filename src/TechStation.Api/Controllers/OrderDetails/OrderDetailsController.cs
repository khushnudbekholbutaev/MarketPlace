using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.OrderDetails;
using TechStation.Service.Interfaces.OrderDetails;

namespace TechStation.Api.Controllers.OrderDetails;

public class OrderDetailsController : BaseController
{
    private readonly IOrderDetailService orderDetailService;

    public OrderDetailsController(IOrderDetailService orderDetailService)
    {
        this.orderDetailService = orderDetailService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // Buyurtma tafsilotlarini (OrderDetail) olish
        var orderDetails = await orderDetailService.RetrieveAllAsync(@params);

        // Jami buyurtma tafsilotlari sonini olish
        var totalCount = await orderDetailService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: buyurtma tafsilotlari ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                orderDetails,
                paginationMetaData
            }
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await orderDetailService.RetrieveByIdAsync(id)
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] OrderDetailForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await orderDetailService.AddAsync(dto)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(long id, [FromBody] OrderDetailForUpdateDto dto)
    {
        return Ok(new Response 
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await orderDetailService.ModifyAsync(id, dto)
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await orderDetailService.RemoveAsync(id)
        });
    }
}
