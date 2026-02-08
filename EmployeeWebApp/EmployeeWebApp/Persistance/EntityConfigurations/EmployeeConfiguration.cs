using EmployeeWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeWebApp.Persistance.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.IdNumber).HasMaxLength(255).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(255).IsRequired();
        builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
    }
}