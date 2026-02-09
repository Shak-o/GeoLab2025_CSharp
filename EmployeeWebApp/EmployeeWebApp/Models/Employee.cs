namespace EmployeeWebApp.Models;

public class Employee
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public Department? Department { get; set; }
    public string Name { get; set; }
    public string IdNumber { get; set; } // 00100090029
    public int Age { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }  
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}

public class Country
{
    public string Name { get; set; }
    public Bank Bank { get; set; }

    public Country(Bank bank)
    {
        Bank = bank;
    }
}


public class Bank
{
    public string Name { get; set; }
    public Currency Currency { get; set; }

    public Bank(Currency currency)
    {
        Currency = currency;
    }
}


public class Currency
{
    public string Name { get; set; }
}