namespace EmployeeWebApp.Services;

public class SingletonService
{
    private readonly Guid _serviceId;
    public SingletonService(ILogger<SingletonService> logger)
    {
        var guid = Guid.NewGuid();
        _serviceId = guid;
        logger.LogInformation($"SingletonService created with GUID: {guid}");
    }
    public Guid ServiceId => _serviceId;
}
