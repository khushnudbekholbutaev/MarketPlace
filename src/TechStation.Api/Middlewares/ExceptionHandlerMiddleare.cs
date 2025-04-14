using Microsoft.AspNetCore.Diagnostics;
using TechStation.Api.Helpers;
using TechStation.Service.Exceptions;

namespace TechStation.Api.Middlewares;

public class ExceptionHandlerMiddleare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleare> logger;

    public ExceptionHandlerMiddleare(RequestDelegate next, ILogger<ExceptionHandlerMiddleare> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (TechStationException ex)
        {
            this.logger.LogError(ex.Message);
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError($"{ex}\n\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = 500,
                Message = ex.Message,
            });
        }
    }
}
