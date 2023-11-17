using InheritanceApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InheritanceApp;

public class AppDbContext : DbContext
{
    // Configure from Entities to use SqlServer with local Sql mdf file
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
          .UseSqlServer( @"Data Source=Laptop-Work; Initial Catalog=ManyToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=ManyToManyApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging();

    // DbSets
    public DbSet<PublicationOeuvre> PublicationOeuvres => Set<PublicationOeuvre>();
    public DbSet<Note> Notes => Set<Note>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PublicationOeuvre>().ToTable( "PublicationOeuvre" );

        // Version 1 - OK
        modelBuilder.Entity<PublicationOeuvre>()
                    .HasMany( e => e.Notes )
                    .WithMany( e => e.PublicationOeuvres )
                    .UsingEntity( joinEntityName: "PublicationOeuvreNote",
                        r => r.HasOne( typeof( Note ) ).WithMany().HasForeignKey( "NoteId" ).HasPrincipalKey( nameof( Note.NoteId ) ),
                        l => l.HasOne( typeof( PublicationOeuvre ) ).WithMany().HasForeignKey( "PublicationOeuvreId" ).HasPrincipalKey( nameof( PublicationOeuvre.PublicationOeuvreId ) ),
                        j => j.HasKey( "PublicationOeuvreId", "NoteId" )
                    );

        // Version 2
        // Not working because we don't follow the convention for the Keys inside the join table
        // NotesNoteId
        // PublicationOeuvresPublicationOeuvreId
        //modelBuilder.Entity<PublicationOeuvre>()
        //            .HasMany( e => e.Notes )
        //            .WithMany( e => e.PublicationOeuvres )
        //            .UsingEntity( joinEntityName: "PublicationOeuvreNote" );

        // Version 3 - Not working
        //modelBuilder.Entity<Note>()
        //            .HasOne( e => e.PublicationOeuvres )
        //            .WithMany( e => e.Notes )
        //            .UsingEntity( joinEntityName: "PublicationOeuvreNote",
        //                r => r.HasOne( typeof( Note ) ).WithMany().HasForeignKey( "NoteId" ).HasPrincipalKey( nameof( Note.NoteId ) ),
        //                l => l.HasOne( typeof( PublicationOeuvre ) ).WithMany().HasForeignKey( "PublicationOeuvreId" ).HasPrincipalKey( nameof( PublicationOeuvre.PublicationOeuvreId ) ),
        //                j => j.HasKey( "PublicationOeuvreId", "NoteId" )
        //            );


        modelBuilder.Entity<Note>().ToTable( "Note" );
    }
}
