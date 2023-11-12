using EfCoreSamplesApp.Entities.TableSpliting;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamplesApp;

internal class Application
{
    private readonly TableSplittingContext context;

    public Application( TableSplittingContext appDbContext )
    {
        this.context = appDbContext;
    }

    public async Task RunAsync()
    {
        Console.WriteLine( "Application.Run" );

        await TableSplitting();

    }

    private async Task Blog_OneToMany()
    {
        //Console.WriteLine( "Application.Run" );

        //ReadQueries readQueries = new( appDbContext );

        //readQueries.EnsureCreated();
        //readQueries.ReadBlog();
    }

    private async Task TableSplitting()
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

        using( var context = new TableSplittingContext() )
        {
            Order order = context.Orders.First( x => x.Id == 1 );
        }

        using( var context = new TableSplittingContext() )
        {
            Order order = context.Orders
                                 .Include( x => x.DetailedOrder )
                                 .First( x => x.Id == 1 );
        }


        using( var context = new TableSplittingContext() )
        {
            int pendingCount = context.Orders.Count( o => o.Status == OrderStatus.Pending );
            Console.WriteLine( $"Current number of pending orders: {pendingCount}" );
        }

        using( var context = new TableSplittingContext() )
        {
            DetailedOrder order = context.DetailedOrders.First( o => o.Status == OrderStatus.Pending );
            Console.WriteLine( $"First pending order will ship to: {order.ShippingAddress}" );
        }
    }
}