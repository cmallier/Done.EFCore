namespace EfCoreSamplesApp.Entities.TableSpliting;

public class DetailedOrder
{
    public int Id { get; set; }
    public OrderStatus? Status { get; set; }
    public string? BillingAddress { get; set; }
    public string? ShippingAddress { get; set; }
    public byte[] Version { get; set; } = null!;
}
