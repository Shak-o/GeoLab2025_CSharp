using EmployeeWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebApp.Persistance;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}