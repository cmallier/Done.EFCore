using Microsoft.EntityFrameworkCore;
using StaticApp;
using StaticApp.Entities;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.AddRange( FakeData.Complete, FakeData.Complete );
    context.SaveChanges();

    context.AddRange( FakeData.Complete, FakeData.Complete );
    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    List<Livre> result1 = context.Livres
                                 .TagWith( "----- Result 1 -----" )
                                 .ToList();

    Console.WriteLine( $"Result 1: {result1.Count}" );
}