using FunkoProject.Common.Atributes;
using FunkoProject.Exceptions;

namespace FunkoProject.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BadRequestException badRequestException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(badRequestException.Message);
        }
        catch (NotFoundException notFoundExceptions)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFoundExceptions.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}