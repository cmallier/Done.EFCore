using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectionApp.Entities;

namespace InheritanceApp;

public class AppDbContext : DbContext
{
    // Configure from Entities to use SqlServer with local Sql mdf file
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
          .UseSqlServer( @"Data Source=Laptop-Work; Initial Catalog=ProjectionApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=ManyToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();

    // DbSets
    public DbSet<Auteur> Auteurs => Set<Auteur>();
    public DbSet<Biographie> Biographies => Set<Biographie>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
