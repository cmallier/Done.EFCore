using InheritanceCopibecApp;
using InheritanceCopibecApp.Entities;
using Microsoft.EntityFrameworkCore;

using ( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Console.WriteLine( "------------------------------------------------------" );
    Console.WriteLine( "-- Add Range" );


    context.Paritions.Add(
        new Partition
        {
            //PublicationOeuvreId = 1,
            Titre = "Partition 1",
            Statut = "Statut 1",

            NombrePages = 1
        }
    );


    context.Add(
        new Partition
        {
            //PublicationOeuvreId = 1,
            Titre = "Partition 2",
            Statut = "Statut 2",

            NombrePages = 2
        }
    );


    context.PublicationOeuvres.Add(
        new Partition
        {
            //PublicationOeuvreId = 1,
            Titre = "Partition 3",
            Statut = "Statut 3",

            NombrePages = 3
        }
    );

    context.SaveChanges();
}


/*
 PublicationOeuvre
   - Parition
     - NombrePages
*/


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using ( var context = new AppDbContext() )
{
    PublicationOeuvre publicationOeuvre = context.PublicationOeuvres.Where( x => x.PublicationOeuvreId == 1 )
                                                                    .TagWith( "-----Result 1-----" )
                                                                    .First();

    List<Partition> partitions = context.Paritions.Where( x => x.Titre.StartsWith( "Partition 1" ) )
                                                  .TagWith( "-----Result2-----" )
                                                  .ToList();
}