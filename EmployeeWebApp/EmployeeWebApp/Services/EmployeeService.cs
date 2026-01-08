using System.Text.Json;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public class EmployeeService
{
    private ILogger<EmployeeService> _logger;
    private IEmployeeStorage _employeeStorage;
    private EmployeeCacheService _cacheService;
    
    public EmployeeService(ILogger<EmployeeService> logger, IEmployeeStorage employeeStorage, EmployeeCacheService cacheService)
    {
        _cacheService = cacheService;
        _logger = logger;
        _employeeStorage = employeeStorage;
    }

    public List<string> GetEmployeeIdNumbers()
    {
        return _cacheService.GetEmployeeIdNumbers();
    }
    
    public void AddNewEmployee(Employee employee)
    {
        var employeeList = _employeeStorage.GetEmployees();
        if (employeeList.Any(x => x.IdNumber == employee.IdNumber))
        {
            _logger.LogWarning($"Employee with IdNumber {employee.IdNumber} already exists");
            throw new Exception("Conflict");
        }
        
        _employeeStorage.AddEmployee(employee);
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