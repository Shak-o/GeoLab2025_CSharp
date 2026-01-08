using EmployeeWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers;

[ApiController]
[Route("testlifetimes")]
public class LifeTimeTestController : ControllerBase
{
    private TestScopedService _testScopedService1;
    private TestScopedService _testScopedService2;
    private TestTransientService _testTransientService1;
    private TestTransientService _testTransientService2;
    private TestSingletonService _testSingletonService1;
    private TestSingletonService _testSingletonService2;

    public LifeTimeTestController(TestScopedService testScopedService1, TestScopedService testScopedService2,
        TestTransientService testTransientService1, TestTransientService testTransientService2,
        TestSingletonService testSingletonService1, TestSingletonService testSingletonService2)
    {
        _testScopedService1 = testScopedService1;
        _testScopedService2 = testScopedService2;
        _testTransientService1 = testTransientService1;
        _testTransientService2 = testTransientService2;
        _testSingletonService1 = testSingletonService1;
        _testSingletonService2 = testSingletonService2;
    }

    [HttpGet("test")]
    public ActionResult Test()
    {
        return Ok(new
        {
            Scoped1 = _testScopedService1.GetServiceId(),
            Scoped2 = _testScopedService2.GetServiceId(),
            Transient1 = _testTransientService1.GetServiceId(),
            Transient2 = _testTransientService2.GetServiceId(),
            Singleton1 = _testSingletonService1.GetServiceId(),
            Singleton2 = _testSingletonService2.GetServiceId()
        });
    }
}