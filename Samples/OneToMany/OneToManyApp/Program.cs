using Microsoft.EntityFrameworkCore;
using OneToManyApp;
using OneToManyApp.Entities;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    /*
    select * from Livres
    select * from Tags
    */

    Livre livre1 = new()
    {
        Titre = "Titre1",
        Tags = new List<Tag>()
        {
            new() { Nom = "Tag11" },
            new() { Nom = "Tag12" },
            new() { Nom = "Tag13" },
        }
    };
    Livre livre2 = new()
    {
        Titre = "Titre2",
        Tags = new List<Tag>()
        {
            new() { Nom = "Tag21" },
            new() { Nom = "Tag22" },
            new() { Nom = "Tag23" },
        }
    };

    context.AddRange( livre1, livre2 );
    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    List<Livre> result1 = context.Livres
                                 .Include( l => l.Tags )
                                 .TagWith( "----- Result 1 -----" )
                                 .ToList();

    Console.WriteLine( $"Result 1: {result1.Count}" );


    // SELECT t.XTagId, t.LivreXLivreId, t.Nom, t.YLivreId
    // FROM Tags AS t
    // WHERE t.YLivreId = 1

    List<Tag> tags = context.Tags
                            .Where( x => x.YLivreId == 1 )
                            .TagWith( "----- Result 2 -----" )
                            .ToList();


    // SELECT t.XTagId, t.LivreXLivreId, t.Nom, t.YLivreId, l.XLivreId, l.Titre
    // FROM Tags AS t
    // INNER JOIN Livres AS l ON t.LivreXLivreId = l.XLivreId
    // WHERE t.YLivreId = 1

    //List<Tag> tags = context.Tags
    //                        .Include( x => x.Livre )
    //                        .Where( x => x.YLivreId == 1 )
    //                        .TagWith( "----- Result 2 -----" )
    //                        .ToList();


    // Through navigation property
    // SELECT t.XTagId, t.LivreXLivreId, t.Nom, t.YLivreId
    // FROM Tags AS t
    // INNER JOIN Livres AS l ON t.LivreXLivreId = l.XLivreId
    // WHERE l.XLivreId = 1

    //List<Tag> tags = context.Tags
    //                        //.Include( x => x.Livre )
    //                        .Where( x => x.Livre.XLivreId == 1 )
    //                        .TagWith( "----- Result 2 -----" )
    //                        .ToList();

    // Shadow property
    // SELECT t.XTagId, t.ForceForeignKeyName, t.Nom
    // FROM Tags AS t
    // WHERE t.ForceForeignKeyName = 1

    //List<Tag> tags = context.Tags
    //                .Where( x => EF.Property<int>( x, "ForceForeignKeyName" ) == 1 )
    //                .TagWith( "----- Result 2 -----" )
    //                .ToList();


    foreach( Tag tag in tags )
    {
        Console.WriteLine( $"{tag.Nom} - {tag.Livre.Titre}" );
    }
}