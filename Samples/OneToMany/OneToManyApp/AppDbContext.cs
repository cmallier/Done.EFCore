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
          //.UseSqlServer( @"Server=Laptop-Work; Initial Catalog=OneToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();

    //public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<Auteur> Auteurs => Set<Auteur>();



    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        // Most one-to-many relationships in an Entity Framework Core model follow conventions and require no additional configuration

        // Case 1
        // Empty = OK

        // or
        //modelBuilder.Entity<Livre>()
        //            .HasMany( "Tags" )
        //            .WithOne( "Livre" )
        //            .HasForeignKey( "LivreId" );

        //modelBuilder.Entity<Livre>()
        //            .HasMany( "Tags" )
        //            .WithOne( "Livre" );

        //modelBuilder.Entity<Livre>()
        //            .HasMany( l => l.Tags )
        //            .WithOne( t => t.Livre );

        // 💀
        //modelBuilder.Entity<Livre>()
        //            .HasMany( "Tags" )              // CLR property name
        //            .WithOne()                      // Crash if not specified, EFCore thinks it's another relation
        //            .HasForeignKey( "LivreId" )     // ForeignKey of Tag
        //            .HasPrincipalKey( "LivreId" );  // PrimaryKey of Livre


        //modelBuilder.Entity<Livre>()
        //            .HasMany( l => l.Tags )
        //            .WithOne( t => t.Livre )
        //            .HasPrincipalKey( l => l.LivreId )
        //            .HasForeignKey( t => t.LivreId );

        //modelBuilder.Entity<Livre>()
        //            .HasMany( "Tags" )
        //            .WithOne( "Livre" )
        //            .HasPrincipalKey( "LivreId" )
        //            .HasForeignKey( "LivreId" );


        //modelBuilder.Entity<Livre>()
        //            .HasMany( l => l.Tags )
        //            .WithOne( t => t.Livre )
        //            .IsRequired();




        // Case 3 - No Key Specified in the CLR
        //modelBuilder.Entity<Tag>()
        //            .Property<int>( "TagId" )
        //            .UsePropertyAccessMode( PropertyAccessMode.Field );


        //modelBuilder.Entity<Livre>()
        //            .HasMany( l => l.Tags )
        //            .WithOne()
        //            .HasForeignKey( "LivreId" ) // ForeignKey of Tag
        //            .HasPrincipalKey( l => l.LivreId ); // PrimaryKey of Livre

        // or

        //modelBuilder.Entity<Livre>()
        //            .HasMany( "Tags" )              // CLR property name
        //            .WithOne()
        //            .HasForeignKey( "LivreId" )     // ForeignKey of Tag
        //            .HasPrincipalKey( "LivreId" );  // PrimaryKey of Livre




        // Default ForeighKey name: <NavigationPropertyName><PrincipalKeyPropertyName>  Exp: Livre_XLivreId

        // Foreign Key in the CLR
        //modelBuilder.Entity<Tag>().HasOne( t => t.Livre )
        //                          .WithMany( l => l.Tags )
        //                          .HasForeignKey( t => t.YLivreId );

        // Force Foreign Key name (Not in CLR)
        //modelBuilder.Entity<Tag>().HasOne( t => t.Livre )
        //                          .WithMany( l => l.Tags )
        //                          .HasForeignKey( "ForceForeignKeyName" );






        // One-to-one
        //modelBuilder.Entity<Livre>()
        //            .HasOne( l => l.Auteur )
        //            .WithOne( a => a.Livre )
        //            .HasForeignKey<Livre>( t => t.LivreId );

        //modelBuilder.Entity<Livre>()
        //            .HasOne( l => l.Auteur )
        //            .WithOne( a => a.LivreProp )                 // Property name
        //            .HasForeignKey( "Auteur", "LivreId" );   // ClassName of Foreign Key, ForeignKey inside Auteur


        modelBuilder.Entity<Livre>()
                    .HasOne( l => l.AuteurProp )
                    .WithOne( a => a.LivreProp )                 // Property name
                    .HasPrincipalKey( "Livre", "LivreId" )       // PrimaryKey of Livre
                    .IsRequired( true );
    }
}
