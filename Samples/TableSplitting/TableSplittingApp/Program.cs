using Microsoft.EntityFrameworkCore;
using StaticApp;
using TableSplitting.Entities;

using( var context = new AppDbContext() )
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    context.Add(
        new Order
        {
            Status = OrderStatus.Pending,
            DetailedOrder = new DetailedOrder
            {
                Status = OrderStatus.Pending,
                ShippingAddress = "221 B Baker St, London",
                BillingAddress = "11 Wall Street, New York"
            }
        } );

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

using( var context = new AppDbContext() )
{
    Order order = context.Orders
                         .Include( x => x.DetailedOrder )
                         .TagWith( "----- Result2 -----" )
                         .First( x => x.Id == 1 );
}

using( var context = new AppDbContext() )
{
    int pendingCount = context.Orders
                              .TagWith( "----- Result3 -----" )
                              .Count( o => o.Status == OrderStatus.Pending );


    Console.WriteLine( $"Current number of pending orders: {pendingCount}" );
}

using( var context = new AppDbContext() )
{
    DetailedOrder order = context.DetailedOrders
                                 .TagWith( "----- Result4 -----" )
                                 .First( o => o.Status == OrderStatus.Pending );

    Console.WriteLine( $"First pending order will ship to: {order.ShippingAddress}" );
}