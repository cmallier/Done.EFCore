using Microsoft.EntityFrameworkCore;
using ValueConversionsApp;
using ValueConversionsApp.Entities;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Livre livre = new() { Titre = "L'île mystérieuse", IsAvailable = true, IsActive = true, Genre = Genre.Roman, Currency = Currency.PoundsSterling, Price = new Dollars( 3m ) };

    context.Add( livre );
    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Livre livre = context.Livres
                         .TagWith( "----- Result 1 -----" )
                         .First();

    Console.WriteLine( $"Result 1: {livre.Genre}" );
}


// Genre - Enum
// 'String' will be stored in database
