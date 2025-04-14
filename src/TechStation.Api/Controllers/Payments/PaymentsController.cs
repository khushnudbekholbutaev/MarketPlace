using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Payments;
using TechStation.Service.Interfaces.Payments;

namespace TechStation.Api.Controllers.Payments;

public class PaymentsController : BaseController
{
    private readonly IPaymentService paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
    {
        // To'lovlarni (Payment) olish
        var payments = await paymentService.RetrieveAllAsync(@params);

        // Jami to'lovlar sonini olish
        var totalCount = await paymentService.CountAsync();

        // PaginationMetaData modelini yaratish
        var paginationMetaData = new PaginationMetaData(totalCount, @params);

        // Natijani qaytarish: to'lovlar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
        var response = new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = new
            {
                payments,
                paginationMetaData
            }
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        var result = await paymentService.RetrieveByIdAsync(id);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] PaymentForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await paymentService.AddAsync(dto)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(long id, [FromBody] PaymentForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await paymentService.ModifyAsync(id,dto)
        });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await paymentService.RemoveAsync(id)
        });
    }
}
