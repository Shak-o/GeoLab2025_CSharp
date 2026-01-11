namespace EmployeeWebApp.Models;

public class Employee
{
    public string Name { get; set; }
    public string IdNumber { get; set; } // 00100090029
    public int Age { get; set; }
    public string LastName { get; set; }
    public string Location { get; set; }
    public int LeavesTaken { get; set; }
    public decimal Rate { get; set; }
    public decimal WorkHours { get; set; }
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