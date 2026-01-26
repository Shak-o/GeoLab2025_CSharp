using System.Text.Json;

namespace EmployeeWebApp.MiddleWares;

public class SimpleAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SimpleAuthMiddleware> _logger;

    public SimpleAuthMiddleware(RequestDelegate next, ILogger<SimpleAuthMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogCritical("Before next");
        // if we want to specify endpoints used in this middleware
        // if (!context.Request.Path.StartsWithSegments("/employee"))
        // {
        //     await _next(context);
        //     return;
        // }
        var authHeader = context.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authHeader))
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            var resp = new
            {
                Code = "Unauthorized",
                Title = "Invalid authorization header"
            };
            var json = JsonSerializer.Serialize(resp);
            await context.Response.WriteAsync(json);
            return;
        }
        await _next(context);
        
        _logger.LogCritical("After next");
    }
}