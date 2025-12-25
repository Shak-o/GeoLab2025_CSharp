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
    private const string FileUrl = "C:\\Apps\\Projects\\EmployeeWebApp\\EmployeeWebApp\\Data\\employees.txt";
    // CREATE
    [HttpPost("add-employee")]
    public ActionResult AddNewEmployee(Employee employee)
    {
        var textInformation = System.IO.File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        if (employeeList == null)
        {
            employeeList = new List<Employee>();
        }
        if (employeeList.Any(x => x.IdNumber == employee.IdNumber))
        {
            return Conflict();
        }
        employeeList.Add(employee);
        
        var serialized = JsonSerializer.Serialize(employeeList);
        System.IO.File.WriteAllText(FileUrl, serialized);
        return Ok();
    }
    
    // READ
    [HttpGet("get-employees")]
    public List<Employee> GetEmployees()
    {
        var textInformation = System.IO.File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        return employeeList;
    }

    [HttpGet("get-employees/{idNumber}")]
    public ActionResult GetEmployeeByIdNumber(string idNumber)
    {
        var textInformation = System.IO.File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        if (employeeList == null)
            return NotFound();
        
        var employee = employeeList.FirstOrDefault(x => x.IdNumber == idNumber);
        return Ok(employee);
    }

    [HttpPost("calculate-salary/{idNumber}")]
    public ActionResult CalculateSalary(string idNumber)
    {
        var employee = Storage.Employees.Where(x => x.IdNumber == idNumber).Select(x => new
        {
            Salary = x.Rate * x.WorkHours,
            Name = x.Name
        });
        return Ok(employee);
    }

    // DELETE
    [HttpDelete("delete-employee/{idNumber}")]
    public ActionResult DeleteEmployee(string idNumber)
    {
        var employeeToDelete = Storage.Employees.FirstOrDefault(x => x.IdNumber == idNumber);
        if (employeeToDelete == null)
        {
            return NotFound();
        }
        
        Storage.Employees.Remove(employeeToDelete);
        return Ok();
    }

    // UPDATE
    [HttpPut("update-employee")]
    public ActionResult UpdateEmployee(Employee employee)
    {
        var textInformation = System.IO.File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        if (employeeList == null)
            return NotFound();

        var existing = employeeList.FirstOrDefault(x => x.IdNumber == employee.IdNumber);
        if (existing != null)
            employeeList.Remove(existing);
        else
            return NotFound();

        employeeList.Add(employee);
        var serialized = JsonSerializer.Serialize(employeeList);
        System.IO.File.WriteAllText(FileUrl, serialized);
        return Ok();
    }
}