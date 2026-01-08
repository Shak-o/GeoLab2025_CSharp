using EmployeeWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers;

[ApiController]
[Route("lifetimes")]
public class LifeTimesController : ControllerBase
{
    private readonly TransientService transientService1;
    private readonly TransientService transientService2;
    private readonly ScopedService scopedService1;
    private readonly ScopedService scopedService2;
    private readonly SingletonService singletonService1;
    private readonly SingletonService singletonService2;

    public LifeTimesController(TransientService transientService1, TransientService transientService2,
        ScopedService scopedService1, ScopedService scopedService2, SingletonService singletonService1,
        SingletonService singletonService2)
    {
        this.transientService1 = transientService1;
        this.transientService2 = transientService2;
        this.scopedService1 = scopedService1;
        this.scopedService2 = scopedService2;
        this.singletonService1 = singletonService1;
        this.singletonService2 = singletonService2;
    }

    [HttpGet("initialize")]
    public ActionResult InitializeServices()
    {
        return Ok(new
        {
            Transient1 = transientService1.ServiceId,
            Transient2 = transientService2.ServiceId,
            Scoped1 = scopedService1.ServiceId,
            Scoped2 = scopedService2.ServiceId,
            Singleton1 = singletonService1.ServiceId,
            Singleton2 = singletonService2.ServiceId
        });
    }
}