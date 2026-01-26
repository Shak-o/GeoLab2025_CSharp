namespace EmployeeWebApp.Exceptions;

public class ApiException : Exception
{
    public string Type { get; }
    public string Title { get; }
    public int Status { get; }
    public string Details { get; }
    public string Instance { get; }

    public ApiException(string type, string title, int status, string details, string instance) : base(details)
    {
        Type = type;
        Title = title;
        Status = status;
        Details = details;
        Instance = instance;
    }
}