using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OneToManyApp.Entities;

namespace OneToManyApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=StaticApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          //.UseSqlServer( @"Server=Desktop-Home; Initial Catalog=OneToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .UseSqlServer( @"Server=Laptop-Work; Initial Catalog=OneToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();

    public DbSet<Tag> Tags => Set<Tag>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
