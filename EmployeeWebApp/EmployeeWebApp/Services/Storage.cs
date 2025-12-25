using EmployeeWebApp.Models;

namespace EmployeeWebApp.Services;

public static class Storage
{
    public static List<Employee> Employees { get; set; } = new List<Employee>();
}