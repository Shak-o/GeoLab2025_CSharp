namespace EmployeeWebApp.Models;

public class Employee
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public string Name { get; set; }
    public string IdNumber { get; set; } 
    public int Age { get; set; }
    public string LastName { get; set; }
    public string Location { get; set; }
    public decimal Rate { get; set; }
    public decimal WorkHours { get; set; }
}

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BankId { get; set; }
    public Bank Bank { get; set; }

    public Country()
    {
        
    }
    public Country(Bank bank)
    {
        Bank = bank;
    }
}


public class Bank
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }

    public Bank()
    {
        
    }
    public Bank(Currency currency)
    {
        Currency = currency;
    }
}


public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; }
}