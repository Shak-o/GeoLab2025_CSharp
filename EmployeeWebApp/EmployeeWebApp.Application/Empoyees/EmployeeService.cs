using EmployeeWebApp.Application.Exceptions;
using EmployeeWebApp.Application.Options;
using EmployeeWebApp.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EmployeeWebApp.Application.Empoyees;

public class EmployeeService
{
    private ILogger<EmployeeService> _logger;
    private IEmployeeStorage _employeeStorage;
    private IEmployeeCacheService _cacheService;
    private IOptions<EmployeeOptions> _options;

    public EmployeeService(ILogger<EmployeeService> logger, IEmployeeStorage employeeStorage,
        IEmployeeCacheService cacheService,  IOptions<EmployeeOptions> options)
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
            throw new ApiException("ValidationError", "ValidationProblem", 400, "User age is inappropriate", "/employee");
        }
        var employeeList = await _employeeStorage.GetEmployees();
        if (employeeList.Any(x => x.IdNumber == employee.IdNumber))
        {
            _logger.LogWarning($"Employee with IdNumber {employee.IdNumber} already exists");
            throw new ApiException("Conflict", "Conflict", 409, "User with same id number already exists", "/employees");
        }

        await _employeeStorage.AddEmployeeAsync(employee);
        _cacheService.AddIdNumber(employee.IdNumber);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employeeList = await _employeeStorage.GetEmployees();
        if (employeeList == null)
        {
            throw new Exception("NotFound");
        }

        return employeeList;
    }

    public async Task<Employee?> GetEmployeeByIdNumber(string idNumber)
    {
        var employee = await _employeeStorage.GetEmployee(idNumber);
        if (employee == null)
            throw new Exception("NotFound");
        return employee;
    }
    

    public void DeleteEmployee(string id)
    {
        throw new NotImplementedException();
    }

    public void UpdateEmployee(string id)
    {
        // copy paste from controller
    }
}