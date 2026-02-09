using EmployeeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApp.Persistance;

public class EmployeeDbContext : DbContext // baza - EmployeeManagerDb
{

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) // pass settings to parent class
    {
        
    }
    
    public DbSet<Employee> Employees { get; set; } // Employees table
    public DbSet<Department> Departments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);
    }
}