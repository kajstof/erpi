using System.Net;
using System.Text.Json;
using Erpi.BuildingBlocks.Application;
using Erpi.BuildingBlocks.Domain;

namespace Erpi.Api.Middlewares;

internal class ApplicationAndDomainExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) when (ex is ApplicationLogicException or DomainException)
        {
            string errorResponse = JsonSerializer.Serialize(new
            {
                type = ex is ApplicationLogicException
                    ? nameof(ApplicationLogicException)
                    : nameof(DomainException),
                code = ex.GetType().Name,
                description = ex.Message,
            });

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorResponse);
        }
    }
}