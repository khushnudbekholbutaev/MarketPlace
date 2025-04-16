using Microsoft.AspNetCore.Mvc;
using TechStation.Api.Helpers;
using TechStation.Domain.Configurations;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.Products;
using TechStation.Service.Interfaces.Products;
using TechStation.Service.Services.Products;

namespace TechStation.Api.Controllers.Products;

public class ProductsController : BaseController
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    /// <summary>
    /// Mahsulotlarni olish uchun API.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllProductsAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] bool sort = false,
        [FromQuery] string productName = null)
    {
        try
        {
            // Mahsulotlarni olish
            var products = await productService.RetrieveAllAsync(@params, minPrice, maxPrice, sort, productName);

            // Jami mahsulotlar sonini olish
            var totalCount = await productService.CountAsync();

            // PaginationMetaData modelini yaratish
            var paginationMetaData = new PaginationMetaData(totalCount, @params);

            // Natijani qaytarish: mahsulotlar ro'yxati va pagination ma'lumotlarini bitta ob'ektga qo'shish
            var response = new Response
            {
                StatusCode = 200,
                Message = "Ok",
                Data = new
                {
                    products,
                    paginationMetaData
                }
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            // Xatolik yuz berganda
            var errorResponse = new Response
            {
                StatusCode = 500,
                Message = ex.Message,
                Data = null
            };
            return StatusCode(500, errorResponse);
        }
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await productService.RetrieveByIdAsync(id)
        });
    }
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] ProductForCreationDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await productService.AddAsync(dto)
        });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyAsync(long id, [FromBody] ProductForUpdateDto dto)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await productService.ModifyAsync(id,dto)
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Ok",
            Data = await productService.RemoveAsync(id)
        });
    }
    [HttpGet("search")]
    public async Task<ActionResult<List<ProductForResultDto>>> SearchProducts(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest(new { message = "Search term cannot be empty" });
        }

        var result = await productService.SearchByProductNameAsync(searchTerm);

        return Ok(result);
    }
    [HttpGet("sot-price")]
    public async Task<ActionResult<List<ProductForResultDto>>> SortByProductPrice([FromQuery] long price, [FromQuery] SortPrice sort)
    {
        var result = await productService.SortByPriceAsync(price, sort);
        return Ok(result);
    }
}
