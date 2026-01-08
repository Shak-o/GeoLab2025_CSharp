namespace EmployeeWebApp.Services;

public class ScopedService
{
    private readonly Guid _serviceId;
    public ScopedService(ILogger<ScopedService> logger)
    {
        var guid = Guid.NewGuid();
        _serviceId = guid;
        logger.LogInformation($"ScopedService created with GUID: {guid}");
    }
    public Guid ServiceId => _serviceId;
}