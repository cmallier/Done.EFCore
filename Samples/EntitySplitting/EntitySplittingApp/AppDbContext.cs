using EntitySplitting.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace StaticApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=EntitySplittingApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Customer> Customers => Set<Customer>();



    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Customer>(
            entityBuilder =>
            {
                entityBuilder
                    .ToTable( "Customers" )
                    .SplitToTable(
                        "PhoneNumbers",
                        tableBuilder =>
                        {
                            tableBuilder.Property( customer => customer.Id ).HasColumnName( "CustomerId" );
                            tableBuilder.Property( customer => customer.PhoneNumber );
                        } )
                    .SplitToTable(
                        "Addresses",
                        tableBuilder =>
                        {
                            tableBuilder.Property( customer => customer.Id ).HasColumnName( "CustomerId" );
                            tableBuilder.Property( customer => customer.Street );
                            tableBuilder.Property( customer => customer.City );
                            tableBuilder.Property( customer => customer.PostCode );
                            tableBuilder.Property( customer => customer.Country );
                        } );
            } );
    }
}
