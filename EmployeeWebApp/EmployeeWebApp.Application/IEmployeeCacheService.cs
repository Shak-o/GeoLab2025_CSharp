namespace EmployeeWebApp.Application;

public interface IEmployeeCacheService
{
    void AddIdNumber(string idNumber);
    List<string> GetEmployeeIdNumbers();
}