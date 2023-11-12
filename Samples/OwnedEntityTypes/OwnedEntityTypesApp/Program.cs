using Microsoft.EntityFrameworkCore;
using OwnedEntityTypes.Entities;
using StaticApp;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Order order = new()
    {
        ShippingAddress = new()
        {
            Street = "123 Main Street",
            City = "Seattle"
        }
    };

    context.Add( order );

    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Order order = context.Orders
                         .TagWith( "----- Result1 -----" )
                         .First( x => x.Id == 1 );
}
