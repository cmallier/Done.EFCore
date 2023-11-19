using InterceptorsApp;
using InterceptorsApp.Entities;
using Microsoft.EntityFrameworkCore;


// services.AddSingleton<UpdateAuditableInterceptor>();

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

    };

    context.Add( livre1 );
    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Livre livre = context.Livres
                                 .TagWith( "----- Result 1 -----" )
                                 .First();

    Console.WriteLine( $"Result 1: {livre.CreatedOnUtc}" );
}