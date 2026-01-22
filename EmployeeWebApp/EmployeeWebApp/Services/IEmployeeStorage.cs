using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public interface IEmployeeStorage
{
    Task AddEmployeeAsync(Employee employee);
    void UpdateEmployee(Employee employee);
    List<Employee> GetEmployees();
    Employee GetEmployee(string idNumber);
}