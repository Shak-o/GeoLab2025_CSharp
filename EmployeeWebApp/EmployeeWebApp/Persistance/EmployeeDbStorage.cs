using EmployeeWebApp.Models;
using EmployeeWebApp.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApp.Persistance;

public class EmployeeDbStorage : IEmployeeStorage
{
    private readonly IConfiguration _configuration;
    private readonly EmployeeDbContext _dbContext;
    
    public EmployeeDbStorage(IConfiguration configuration, EmployeeDbContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }
    
    // public async Task AddEmployeeAsync(Employee employee)
    // {
    //     var now = DateTime.Now;
    //     
    //     var connectionString = _configuration.GetConnectionString("EmployeeDb");
    //     using var sqlConnection = new SqlConnection(connectionString);
    //     await sqlConnection.OpenAsync();
    //
    //     var command = new SqlCommand(
    //             $"INSERT INTO Employees (FirstName, LastName, Email, PersonalId, Age, HireDate, Salary) VALUES ('{employee.Name}', '{employee.LastName}', '{employee.Email}', '{employee.IdNumber}', {employee.Age}, '{now}', {employee.Salary})", sqlConnection);
    //
    //     await command.ExecuteNonQueryAsync();
    // }

    public async Task AddEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
    }

    public Task UpdateEmployee(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        return _dbContext.SaveChangesAsync();
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var result = await _dbContext.Employees.ToListAsync();
        return result;
    }
    
    // public async Task<List<Employee>> GetEmployees()
    // {
    //     var list = new List<Employee>();
    //     var connectionString = _configuration.GetConnectionString("EmployeeDb");
    //     using var sqlConnection = new SqlConnection(connectionString);
    //     await sqlConnection.OpenAsync();
    //
    //     var command = new SqlCommand(
    //         """
    //                   SELECT [Id]
    //                       ,[FirstName]
    //                       ,[LastName]
    //                       ,[Email]
    //                       ,[PersonalId]
    //                       ,[Age]
    //                       ,[HireDate]
    //                       ,[Salary]
    //                   FROM [dbo].[Employees]
    //              """, sqlConnection);
    //
    //     var  reader = await command.ExecuteReaderAsync();
    //
    //     while (await reader.ReadAsync())
    //     {
    //         var employee = new Employee
    //         {
    //             Id = reader.GetInt32(0),
    //             Name = reader.GetString(1),
    //             LastName = reader.GetString(2),
    //             Email = reader.GetString(3),
    //             IdNumber = reader.GetString(4),
    //             Age = reader.GetInt32(5),
    //             HireDate = reader.GetDateTime(6),
    //             Salary = reader.GetDecimal(7)
    //         };
    //         list.Add(employee);
    //     }
    //     
    //     return list;
    // }

    public async Task<Employee> GetEmployee(string idNumber)
    {
        var result = await _dbContext.Employees.FirstOrDefaultAsync(x => x.IdNumber == idNumber);
        return result;
    }
}