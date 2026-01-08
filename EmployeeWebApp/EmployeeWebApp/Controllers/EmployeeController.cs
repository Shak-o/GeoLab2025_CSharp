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

    public EmployeeController(EmployeeService service, ILogger<EmployeeController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    // CREATE
    [HttpPost("add-employee")]
    public ActionResult AddNewEmployee(Employee employee)
    {
        try
        {
            _logger.LogInformation($"Adding employee with IdNumber: {employee.IdNumber}");
            _logger.LogInformation("Adding employee with IdNumber: {IdNumber}", employee.IdNumber);
            _service.AddNewEmployee(employee);
            return Ok();
        }
        catch (Exception ex)
        {
            switch (ex.Message)
            {
                case "Conflict":
                    return Conflict(new {Title = "CustomError", Details = "Error Detials", Status = 409, Code = "EmployeeAlreadyExists"});
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
}