using EntitySplitting.Entities;
using Microsoft.EntityFrameworkCore;
using StaticApp;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    Customer customer = new( name: "John Doe", street: "Main Street", city: "London", postCode: "12345", country: "UK" )
    {
        PhoneNumber = "123456789"
    };

    context.Add( customer );

    context.SaveChanges();
}


Console.WriteLine( "------------------------------------------------------" );
Console.WriteLine( "-- Results" );

using( var context = new AppDbContext() )
{
    Customer customer = context.Customers
                               .TagWith( "----- Result1 -----" )
                               .First( x => x.Id == 1 );
}
