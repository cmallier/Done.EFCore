using EfCoreSamplesApp.Entities.TableSpliting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class TableSplittingContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<DetailedOrder> DetailedOrders { get; set; }

    public TableSplittingContext()
    {

    }

    public TableSplittingContext( DbContextOptions<TableSplittingContext> options ) : base( options )
    {

    }

    protected override void OnConfiguring( DbContextOptionsBuilder options )
    {
        string connectionString = $"Server=DESKTOP-HOME; Database=EfCoreSamplesApp; Trusted_Connection=True; MultipleActiveResultSets=true; TrustServerCertificate=True;";
        options.UseSqlServer( connectionString );

        options.EnableDetailedErrors( true );
        options.EnableSensitiveDataLogging( true );

        options.LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } );

        base.OnConfiguring( options );
    }

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