namespace EmployeeWebApp.Services;

public class TransientService
{
    private readonly Guid _serviceId;
    public TransientService(ILogger<TransientService> logger)
    {
        var guid = Guid.NewGuid();
        _serviceId = guid;
        logger.LogInformation($"TransientService created with GUID: {guid}");
    }
    
    public Guid ServiceId => _serviceId;
}