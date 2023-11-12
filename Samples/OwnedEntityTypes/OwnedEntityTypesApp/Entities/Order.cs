namespace OwnedEntityTypes.Entities;

public class Order
{
    public int Id { get; set; }
    public StreetAddress ShippingAddress { get; set; } = default!;
}

public class OrderDetails
{
    public DetailedOrder Order { get; set; }
    public StreetAddress BillingAddress { get; set; } = default!;
    public StreetAddress ShippingAddress { get; set; } = default!;
}

public class DetailedOrder
{
    public int Id { get; set; }
    public OrderDetails OrderDetails { get; set; } = default!;
    public OrderStatus Status { get; set; }
}

public class StreetAddress
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
}

public enum OrderStatus
{
    Pending,
    Shipped
}