using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers;

[ApiController]
[Route("employees")]
public class ContextTestController : ControllerBase
{
    [HttpPost("test")]
    public void GetData([FromBody]ContextTestData data)
    {
        HttpContext.Response.StatusCode = 200;
        HttpContext.Response.ContentType = "text/plain";
        HttpContext.Response.WriteAsync(data.Name + " " + data.LastName).GetAwaiter().GetResult();
    }
}

public class ContextTestData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName  { get; set; }
}