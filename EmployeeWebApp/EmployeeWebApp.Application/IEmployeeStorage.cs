using EmployeeWebApp.Domain.Entities;

namespace EmployeeWebApp.Application;

public interface IEmployeeStorage
{
    Task AddEmployeeAsync(Employee employee);
    Task UpdateEmployee(Employee employee);
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string idNumber);
}