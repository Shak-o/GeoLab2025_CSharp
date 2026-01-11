namespace EmployeeWebApp.Middlewares;

public class BasicLogger
{
    private readonly RequestDelegate _next;
    private readonly ILogger<BasicLogger> _logger;
    
    public BasicLogger(RequestDelegate next, ILogger<BasicLogger> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Incoming request: {context.Request.Method} {context.Request.Path}");
        
        await _next(context);
        
        _logger.LogInformation($"Outgoing response: {context.Response.StatusCode}");
    }
}