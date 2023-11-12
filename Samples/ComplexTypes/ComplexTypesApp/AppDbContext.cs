using ComplexTypes.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace StaticApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=ComplexTypesApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();


    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Customer>()
                    .ComplexProperty( e => e.Address );

        modelBuilder.Entity<Order>( b =>
        {
            b.ComplexProperty( e => e.BillingAddress );
            b.ComplexProperty( e => e.ShippingAddress );
        } );
    }
}
