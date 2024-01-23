using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PracticeCA.Domain;

namespace PracticeCA.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    private void ConfigureModel(ModelBuilder modelBuilder)
    {
        // Seed data
        // https://rehansaeed.com/migrating-to-entity-framework-core-seed-data/
        /* Eg.

        modelBuilder.Entity<Car>().HasData(
        new Car() { CarId = 1, Make = "Ferrari", Model = "F40" },
        new Car() { CarId = 2, Make = "Ferrari", Model = "F50" },
        new Car() { CarId = 3, Make = "Lamborghini", Model = "Countach" });
        */
    }
}

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseSqlServer("Data Source=DUNGPHAM;User ID=sa;Password=123456;Initial Catalog=PracticeCA;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}