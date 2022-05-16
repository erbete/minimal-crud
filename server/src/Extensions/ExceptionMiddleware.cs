using System.Net;

namespace CRUD.Extensions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostApplicationLifetime _lifetime;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostApplicationLifetime lifetime)
    {
        _next = next;
        _logger = logger;
        _lifetime = lifetime;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            // log error
            _logger.LogError($"Unexpected Error: {ex}");

            // respond with 500 internal error
            await HandleExceptionAsync(httpContext, ex);

            // shutdown the server
            _lifetime.StopApplication();
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response
            .WriteAsJsonAsync(new ErrorResult(context.Response.StatusCode, "Internal Server Error"));
    }

    private record ErrorResult(int Status, string Title);
}
