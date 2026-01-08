namespace EmployeeWebApp.Services;

public class TestScopedService
{
    private Guid _serviceId;
    
    public TestScopedService()
    {
        _serviceId = Guid.NewGuid();
    }
    
    public Guid GetServiceId()
    {
        return _serviceId;
    }
}