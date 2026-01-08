namespace EmployeeWebApp.Services;

public class EmployeeCacheService
{
    private List<string> employeeIdNumbers = new List<string>();

    public void AddIdNumber(string idNumber)
    {
        employeeIdNumbers.Add(idNumber);
    }

    public List<string> GetEmployeeIdNumbers()
    {
        return employeeIdNumbers;
    }
}