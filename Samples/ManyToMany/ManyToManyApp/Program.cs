using InheritanceApp;
using InheritanceApp.Entities;
using Microsoft.EntityFrameworkCore;

using ( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    /*
     select* from PublicationOeuvre
     select* from PublicationOeuvreNote
     select* from Note
    */

    await context.Database.ExecuteSqlRawAsync( "delete from PublicationOeuvreNote" );
    await context.Database.ExecuteSqlRawAsync( "delete from Note" );
    await context.Database.ExecuteSqlRawAsync( "delete from PublicationOeuvre" );


    PublicationOeuvre publicationOeuvre1 = new()
    {
        Titre = "Titre1",
        Notes = [
            new Note() { Contenu = "Note1-1", DateCreation = new DateTime( 2023, 10, 15 ), UtilisateurCreationId = 1 },
            new Note() { Contenu = "Note1-2", DateCreation = new DateTime( 2023, 10, 16 ), UtilisateurCreationId = 2 },
        ]
    };

    PublicationOeuvre publicationOeuvre2 = new()
    {
        Titre = "Titre2",
        Notes = [
            new Note() { Contenu = "Note2-1", DateCreation = new DateTime( 2023, 11, 15 ), UtilisateurCreationId = 3 },
            new Note() { Contenu = "Note2-2", DateCreation = new DateTime( 2023, 11, 16 ), UtilisateurCreationId = 4 },
        ]
    };

    context.PublicationOeuvres.AddRange( publicationOeuvre1, publicationOeuvre2 );

    context.SaveChanges();
}

Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using ( var context = new AppDbContext() )
{
    PublicationOeuvre result1 = context.PublicationOeuvres.Where( x => x.Titre == "Titre1" )
                                                          .Include( x => x.Notes )
                                                          .TagWith( "----- Result 1 -----" )
                                                          .First();

    Console.WriteLine( $"{result1.Titre} Notes: {result1.Notes.Count()}" );


    List<Note> notes = context.Notes.Where( x => x.PublicationOeuvres.Any( p => p.PublicationOeuvreId == 1 ) )
                                    .TagWith( "----- Result 2 -----" )
                                    .ToList();

    foreach ( Note note in notes )
    {
        Console.WriteLine( $"{note.Contenu}" );
    }
}