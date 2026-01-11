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

    public EmployeeController(EmployeeService service)
    {
        _service = service;
    }
    
    // CREATE
    [HttpPost("add-employee")]
    public ActionResult AddNewEmployee(Employee employee)
    {
        try
        {
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
    
    [HttpPost("set-leave")]
    public ActionResult SetLeave(string idNumber, int leaves)
    {
        try
        {
            _service.TakeLeave(idNumber, leaves);
            return Ok();
        }
        catch (Exception ex)
        {
            if (ex.Message == "NotFound")
            {
                return NotFound();
            }
            if (ex.Message == "ValidationError")
            {
                return BadRequest(new {Title = "ValidationError", Details = "Exceeds available leave days", Status = 400, Code = "LeaveDaysExceeded"});
            }
            return BadRequest();
        }
    }
}