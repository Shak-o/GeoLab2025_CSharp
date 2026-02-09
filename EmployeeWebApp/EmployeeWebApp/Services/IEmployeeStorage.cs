using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public interface IEmployeeStorage
{
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string idNumber);
}