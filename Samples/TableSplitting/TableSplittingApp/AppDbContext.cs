using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TableSplitting.Entities;

namespace StaticApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          .UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=TableSplittingApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<DetailedOrder> DetailedOrders => Set<DetailedOrder>();



    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<DetailedOrder>(
            x =>
            {
                x.ToTable( "Orders" );

                x.Property( o => o.Status )
                 .HasColumnName( "Status" );
            } );

        modelBuilder.Entity<Order>(
            x =>
            {
                x.ToTable( "Orders" );

                x.Property( o => o.Status )
                  .HasColumnName( "Status" );

                x.HasOne( o => o.DetailedOrder )
                 .WithOne()
                 .HasForeignKey<DetailedOrder>( o => o.Id );

                x.Navigation( o => o.DetailedOrder )
                 .IsRequired();
            } );


        // ConcurrencyToken
        modelBuilder.Entity<Order>()
                    .Property<byte[]>( "Version" )
                    .IsRowVersion()
                    .HasColumnName( "Version" );

        modelBuilder.Entity<DetailedOrder>()
                    .Property( o => o.Version )
                    .IsRowVersion()
                    .HasColumnName( "Version" );
    }
}
