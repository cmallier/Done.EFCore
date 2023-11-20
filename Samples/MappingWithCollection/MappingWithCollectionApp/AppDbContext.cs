using MappingWithCollection.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MappingWithCollection;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=StaticApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          //.UseSqlServer( @"Server=Desktop-Home; Initial Catalog=MappingApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .UseSqlServer( @"Server=Laptop-Work; Initial Catalog=MappingWithCollection; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();
    public DbSet<Tag> Tags => Set<Tag>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livre>().ToTable( "Livres" );
        modelBuilder.Entity<Tag>().ToTable( "Tags" );

        // Backed field
        //modelBuilder.Entity<Livre>().Property( b => b.Url )
        //                            .HasField( "_url" );

    }
}
