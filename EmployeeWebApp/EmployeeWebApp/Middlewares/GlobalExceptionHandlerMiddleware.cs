using System.Text.Json;
using EmployeeWebApp.Exceptions;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.MiddleWares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApiException apiException)
        {
            context.Response.ContentType = "application/json";
            var problemDetails = new ApiProblemDetail
            {
                Type = apiException.Type,
                Title = apiException.Title,
                Status = apiException.Status,
                Details = apiException.Details,
                Instance = apiException.Instance,
            };
            
            context.Response.StatusCode = problemDetails.Status;
            var serialized = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(serialized);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            var problemDetails = new ApiProblemDetail
            {
                Type = "internal error",
                Title = "Internal server error",
                Status = 500,
                Details = "Internal server error",
                Instance = "Instance error",
            };
            
            context.Response.StatusCode = problemDetails.Status;
            var serialized = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(serialized);
        }
    }
}