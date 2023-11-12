using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StaticApp.Entities;

namespace StaticApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=StaticApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();



    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Livre>().ToTable( "Livres" );
    }
}
