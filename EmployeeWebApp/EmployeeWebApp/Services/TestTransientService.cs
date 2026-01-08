namespace EmployeeWebApp.Services;

public class TestTransientService
{
    private Guid _serviceId;
    
    public TestTransientService()
    {
        _serviceId = Guid.NewGuid();
    }
    
    public Guid GetServiceId()
    {
        return _serviceId;
    }
}