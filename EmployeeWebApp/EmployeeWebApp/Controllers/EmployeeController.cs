using System.Text.Json;
using EmployeeWebApp.Application.Empoyees;
using EmployeeWebApp.Domain.Entities;
using EmployeeWebApp.Persistance;
using EmployeeWebApp.Persistance.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task AddNewEmployee(Employee employee)
    {
        _logger.LogDebug("Adding new employee with id {IdNumber}", employee.IdNumber);
        _logger.LogDebug($"Adding new employee with id {employee.IdNumber}");
        await _service.AddNewEmployeeAsync(employee);
    }

    // READ
    [HttpGet("get-employees")]
    public async Task<ActionResult> GetEmployees()
    {
        try
        {
            var employees = await _service.GetEmployees();
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
    public async Task<ActionResult> GetEmployeeByIdNumber(string idNumber)
    {
        return Ok(await _service.GetEmployeeByIdNumber(idNumber));
    }

    [HttpGet("id-numbers")]
    public List<string> GetEmployeeIdNumbers()
    {
        return _service.GetEmployeeIdNumbers();
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
    //
    // [HttpPut("update-employee")]
    // public async Task<ActionResult> UpdateEmployee(int id, string? name, string? lastName)
    // {
    //     var existingRecord = await _dbContext.Employees
    //         .AsNoTracking()
    //         .FirstOrDefaultAsync(x => x.Id == id);
    //     if (existingRecord == null)
    //         throw new Exception("NotFound");
    //
    //     if (name != null)
    //         existingRecord.Name = name;
    //     
    //     if (lastName != null)
    //         existingRecord.LastName = lastName;
    //     
    //     _dbContext.Employees.Update(existingRecord);
    //     await _dbContext.SaveChangesAsync();
    //     
    //     return Ok();
    // }
    //
    // [HttpGet("employees-by-hiredate")]
    // public async Task<List<Employee>> GetEmployeesByHireDate([FromQuery]DateTime date)
    // {
    //     var employees =await _dbContext.Employees.Where(x => x.HireDate >= date).ToListAsync();
    //     
    //     return employees;
    // }
    //
    // [HttpGet("random")]
    // public async Task RandomMethod()
    // {
    //     var count = await _dbContext.Employees.CountAsync();
    //     var countFiltered = await _dbContext.Employees.CountAsync(x => x.Age > 25);
    //     var any = await _dbContext.Employees.AnyAsync(x => x.HireDate > DateTime.Now);
    //
    //     var names = new List<string>(){"lasha", "giorgi", "roham"};
    //     var filteredByNames = await _dbContext.Employees.Where(x => names.Contains(x.Name)).ToListAsync();
    //
    //     var employeeExistsByName = await _dbContext.Employees.Where(x => x.Name + "123" == "shako").ToListAsync(); // selects whole table
    //
    //     var nameContains = await _dbContext.Employees.Where(x => x.Name.Contains("a")).ToListAsync();
    // }
    //
}