using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OneToManyApp.Entities;

namespace OneToManyApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=StaticApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .UseSqlServer( @"Server=Desktop-Home; Initial Catalog=OneToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();
    public DbSet<Tag> Tags => Set<Tag>();



    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Livre>().ToTable( "Livres" );
        modelBuilder.Entity<Livre>().HasKey( t => t.XLivreId );


        modelBuilder.Entity<Tag>().ToTable( "Tags" );
        modelBuilder.Entity<Tag>().HasKey( t => t.XTagId );


        // Default ForeighKey name: <NavigationPropertyName><PrincipalKeyPropertyName>  Exp: Livre_XLivreId

        // Foreign Key in the CLR
        //modelBuilder.Entity<Tag>().HasOne( t => t.Livre )
        //                          .WithMany( l => l.Tags )
        //                          .HasForeignKey( t => t.YLivreId );

        // Force Foreign Key name (Not in CLR)
        //modelBuilder.Entity<Tag>().HasOne( t => t.Livre )
        //                          .WithMany( l => l.Tags )
        //                          .HasForeignKey( "ForceForeignKeyName" );

    }
}
