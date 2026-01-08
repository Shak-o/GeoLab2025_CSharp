using System.Text.Json;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public class EmployeeStorage : IEmployeeStorage
{
    private const string FileUrl = "Data/employees.txt";

    public void AddEmployee(Employee employee)
    {
        var textInformation = File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        if (employeeList == null)
        {
            employeeList = new List<Employee>();
        }
        
        employeeList.Add(employee);
        
        var serialized = JsonSerializer.Serialize(employeeList);
        File.WriteAllText(FileUrl, serialized);
    }

    public void UpdateEmployee(Employee employee)
    {
        var textInformation = File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        employeeList.Remove(employee);

        if (employeeList.Any(x => x.IdNumber == employee.IdNumber))
        {
            employee.IdNumber = employee.IdNumber;
            employee.Name = employee.Name;
            employee.LastName = employee.LastName;
            // ...
        }
        
        employeeList.Add(employee);
        var serialized = JsonSerializer.Serialize(employeeList);
        File.WriteAllText(FileUrl, serialized);
    }

    public List<Employee> GetEmployees()
    {
        var textInformation = File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        return employeeList;
    }

    public Employee GetEmployee(string idNumber)
    {
        var textInformation = File.ReadAllText(FileUrl);
        var employeeList = JsonSerializer.Deserialize<List<Employee>>(textInformation);
        return employeeList.FirstOrDefault(x => x.IdNumber == idNumber);
    }
}