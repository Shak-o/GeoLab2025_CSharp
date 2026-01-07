using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public interface IEmployeeStorage
{
    void AddEmployee(Employee employee);
    void UpdateEmployee(Employee employee);
    List<Employee> GetEmployees();
    Employee GetEmployee(string idNumber);
}