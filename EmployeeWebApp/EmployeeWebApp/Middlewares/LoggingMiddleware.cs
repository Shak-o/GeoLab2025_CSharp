namespace EmployeeWebApp.MiddleWares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // requestis nawili
        var startTime = DateTime.UtcNow;
        
        _logger.LogInformation(
            "Incoming Request: {Method} {Path} from {IP}",
            context.Request.Method,
            context.Request.Path,
            context.Connection.RemoteIpAddress);
        
        await _next(context);
        
        // pasuxis nawili
        var duration = DateTime.UtcNow - startTime;
        
        _logger.LogInformation(
            "Outgoing Response: {StatusCode} - Duration: {Duration}ms",
            context.Response.StatusCode,
            duration.TotalMilliseconds);
    }
}