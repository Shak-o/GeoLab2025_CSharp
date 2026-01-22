using System.Text.Json;
using EmployeeWebApp.Models;
using EmployeeWebApp.Options;
using Microsoft.Extensions.Options;

namespace EmployeeWebApp.Services;

public class EmployeeService
{
    private ILogger<EmployeeService> _logger;
    private IEmployeeStorage _employeeStorage;
    private EmployeeCacheService _cacheService;
    private IOptions<EmployeeOptions> _options;

    public EmployeeService(ILogger<EmployeeService> logger, IEmployeeStorage employeeStorage,
        EmployeeCacheService cacheService,  IOptions<EmployeeOptions> options)
    {
        _cacheService = cacheService;
        _options = options;
        _logger = logger;
        _employeeStorage = employeeStorage;
    }

    public List<string> GetEmployeeIdNumbers()
    {
        return _cacheService.GetEmployeeIdNumbers();
    }

    public async Task AddNewEmployeeAsync(Employee employee)
    {
        var minAge = _options.Value.EmployeeMinAge;
        var maxAge = _options.Value.EmployeeMaxAge;
        
        // var minAge = _configuration.GetValue<int>("EmployeeOption:EmployeeMinAge");
        // var maxAge = _configuration.GetValue<int>("EmployeeOption:EmployeeMaxAge");
        // variant 2
        // var section = _configuration.GetSection("EmployeeOption");
        // var minAge2 = section["EmployeeMinAge"];
        // var maxAge2 = section["EmployeeMaxAge"];
        
        if (employee.Age < minAge || employee.Age > maxAge)
        {
            throw new Exception("ValidationError invalid age");
        }
        var employeeList = _employeeStorage.GetEmployees();
        if (employeeList.Any(x => x.IdNumber == employee.IdNumber))
        {
            _logger.LogWarning($"Employee with IdNumber {employee.IdNumber} already exists");
            throw new Exception("Conflict");
        }

        await _employeeStorage.AddEmployeeAsync(employee);
        _cacheService.AddIdNumber(employee.IdNumber);
    }

    public List<Employee> GetEmployees()
    {
        var employeeList = _employeeStorage.GetEmployees();
        if (employeeList == null)
        {
            throw new Exception("NotFound");
        }

        return employeeList;
    }

    public Employee? GetEmployeeByIdNumber(string idNumber)
    {
        var employee = _employeeStorage.GetEmployee(idNumber);
        return employee;
    }

    public decimal CalculateSalary(string employeeId)
    {
        var employee = GetEmployeeByIdNumber(employeeId);
        var salary = employee.Rate * employee.WorkHours;
        return salary;
    }

    public void DeleteEmployee(string id)
    {
        // copy paste from controller
    }

    public void UpdateEmployee(string id)
    {
        // copy paste from controller
    }
}