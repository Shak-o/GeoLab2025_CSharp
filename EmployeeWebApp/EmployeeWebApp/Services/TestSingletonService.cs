namespace EmployeeWebApp.Services;

public class TestSingletonService
{
    private Guid _serviceId;
    private IServiceProvider _serviceProvider;
    
    public TestSingletonService(TestTransientService transientService, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _serviceId = Guid.NewGuid();
    }
    
    public Guid GetServiceId()
    {
        return _serviceId;
    }

    public void BackgroundJob()
    {
        while (true)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var transientService1 = scope.ServiceProvider.GetRequiredService<TestTransientService>();
                transientService1.GetServiceId();
            }
            Task.Delay(TimeSpan.FromHours(1)).Wait();
        }
    }
}