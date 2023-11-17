using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OwnedEntityTypes.Entities;

namespace StaticApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        optionsBuilder.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=OwnedEntityTypesApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
                      .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
                      .EnableSensitiveDataLogging();
    }

    // DbSets
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Order>()
                    .OwnsOne( x => x.ShippingAddress );

        //modelBuilder.Entity<Order>()
        //            .OwnsOne( typeof( StreetAddress ), "ShippingAddress" );
        //modelBuilder.Entity<Order>().OwnsOne(
        //   o => o.ShippingAddress,
        //   sa =>
        //   {
        //       sa.Property( p => p.Street ).HasColumnName( "ShipsToStreet" );
        //       sa.Property( p => p.City ).HasColumnName( "ShipsToCity" );
        //   } );
    }
}
