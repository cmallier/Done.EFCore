using InheritanceCopibecApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InheritanceCopibecApp;

public class AppDbContext : DbContext
{
    // Configure from Entities to use SqlServer with local Sql mdf file
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=InheritanceApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .UseSqlServer( @"Data Source=Laptop-Work; Initial Catalog=InheritanceCopibecApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();


    // DbSets
    public DbSet<PublicationOeuvre> PublicationOeuvres => Set<PublicationOeuvre>();                  // abstract
    public DbSet<Partition> Paritions => Set<Partition>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH
        // 1 table per Hierarchy

        // Configuration
        // Empty = Tph by default
        // or
        // modelBuilder.Entity<Animal>().UseTphMappingStrategy();



        // TPT
        // 1 table per Type

        modelBuilder.Entity<PublicationOeuvre>()
                    .ToTable( "PublicationOeuvres" )
                    .UseTptMappingStrategy();

        modelBuilder.Entity<Partition>().ToTable( "Partitions" );

        // modelBuilder.Entity<Animal>().ToTable( "Animals" ).UseTptMappingStrategy()
        // or (map each tables)
        // modelBuilder.Entity<Animal>().ToTable( "Animals" );
        // modelBuilder.Entity<FarmAnimal>().ToTable( "FarmAnimals" );
        // modelBuilder.Entity<Pet>().ToTable( "Pets" );
        // modelBuilder.Entity<Cat>().ToTable( "Cats" );
        // modelBuilder.Entity<Dog>().ToTable( "Dogs" );



        // TPC
        // 1 table per Concrete

        //modelBuilder.HasSequence<int>( "AnimalIds" )
        //            .StartsAt( 1 )
        //            .IncrementsBy( 1 );

        //modelBuilder.Entity<Animal>()
        //            .UseTpcMappingStrategy()
        //            .Property( x => x.Id )
        //            .HasDefaultValueSql( "NEXT VALUE FOR [AnimalIds]" );
    }
}
