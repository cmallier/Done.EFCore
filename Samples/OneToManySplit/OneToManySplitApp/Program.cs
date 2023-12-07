using Microsoft.EntityFrameworkCore;
using OneToManyApp;
using OneToManyApp.Entities;

using ( var context = new AppDbContext() )
{
    //context.Database.EnsureDeleted();
    //context.Database.EnsureCreated();

    ///*
    //select * from Livres
    //select * from Tags
    //*/

    //Livre livre1 = new()
    //{
    //    Titre = "Titre1",
    //    Tags = new List<Tag>()
    //    {
    //        new() { Nom = "Tag11" },
    //        new() { Nom = "Tag12" },
    //        new() { Nom = "Tag13" },
    //    }
    //};
    //Livre livre2 = new()
    //{
    //    Titre = "Titre2",
    //    Tags = new List<Tag>()
    //    {
    //        new() { Nom = "Tag21" },
    //        new() { Nom = "Tag22" },
    //        new() { Nom = "Tag23" },
    //    }
    //};
    //Livre livre3 = new()
    //{
    //    Titre = "Titre3",
    //    Tags = new List<Tag>()
    //    {
    //        new() { Nom = "Tag31" },
    //        new() { Nom = "Tag32" },
    //        new() { Nom = "Tag33" },
    //    }
    //};

    //context.AddRange( livre1, livre2, livre3 );
    //context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using ( var context = new AppDbContext() )
{
    List<Livre> result1 = context.Livres
                                 .Where( l => l.Titre.Contains( "Titre" ) )
                                 .Include( l => l.Tags )
                                 .TagWith( "----- Result 1 -----" )
                                 .Skip( 0 )
                                 .Take( 1 )
                                 .ToList();

    Console.WriteLine( $"Result 1: {result1.Count}" );

    Console.WriteLine( $"{result1[0].Titre}" );
    Console.WriteLine( $"{result1[0].Tags.Count}" );
}