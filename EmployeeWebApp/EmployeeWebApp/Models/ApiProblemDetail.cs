namespace EmployeeWebApp.Models;

public class ApiProblemDetail
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set;  }
    public string Details { get; set; }
    public string Instance { get; set; }
}