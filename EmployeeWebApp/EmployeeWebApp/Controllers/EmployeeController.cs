using System.Text.Json;
using EmployeeWebApp.Models;
using EmployeeWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers;

// CRUD
[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private EmployeeService _service;
    private ILogger<EmployeeController> _logger;
    private readonly IConfiguration _configuration;
    
    public EmployeeController(EmployeeService service, ILogger<EmployeeController> logger, IConfiguration configuration)
    {
        _service = service;
        _logger = logger;
        _configuration = configuration;
    }
    
    // CREATE
    [HttpPost("add-employee")]
    public async Task<ActionResult> AddNewEmployee(Employee employee)
    {
        try
        {
            _logger.LogDebug("Adding new employee with id {IdNumber}", employee.IdNumber);
            _logger.LogDebug($"Adding new employee with id {employee.IdNumber}");
            await _service.AddNewEmployeeAsync(employee);
            return Ok();
        }
        catch (Exception ex)
        {
            switch (ex.Message)
            {
                case "Conflict":
                {
                    _logger.LogWarning($"Employee with this id already exists {employee.IdNumber}");
                    return Conflict(new
                    {
                        Title = "CustomError", Details = "Error Detials", Status = 409, Code = "EmployeeAlreadyExists"
                    });
                }
                default:
                    return BadRequest();
            }
        }
    }
    
    // READ
    [HttpGet("get-employees")]
    public ActionResult GetEmployees()
    {
        try
        {
            var employees = _service.GetEmployees();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            switch (ex.Message)
            {
                case "Conflict":
                    return Conflict();
                case "NotFound":
                    return NotFound();
                default:
                    return BadRequest();
            }
        }
    }

    [HttpGet("get-employees/{idNumber}")]
    public ActionResult GetEmployeeByIdNumber(string idNumber)
    {
       return Ok(_service.GetEmployeeByIdNumber(idNumber));
    }

    [HttpGet("id-numbers")]
    public List<string> GetEmployeeIdNumbers()
    {
        return _service.GetEmployeeIdNumbers();
    }

    [HttpPost("calculate-salary/{idNumber}")]
    public ActionResult CalculateSalary(string idNumber)
    {
        return Ok(_service.CalculateSalary(idNumber));
    }

    // DELETE
    [HttpDelete("delete-employee/{idNumber}")]
    public ActionResult DeleteEmployee(string idNumber)
    {
        _service.DeleteEmployee(idNumber);
        return Ok();
    }

    [HttpPost("secured-endpoint")]
    public ActionResult SecuredEndpoint(string userPassword)
    {
        var storedPassword = _configuration["Password"];
        if (userPassword != storedPassword)
        {
            return Unauthorized();
        }

        return Ok("You gained access to secured endpoint");
    }
}