namespace EmployeeWebApp.Services;

public class TestSingletonService
{
    private Guid _serviceId;
    
    public TestSingletonService()
    {
        _serviceId = Guid.NewGuid();
    }
    
    public Guid GetServiceId()
    {
        return _serviceId;
    }
}