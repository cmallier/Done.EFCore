# Table Splitting (table sharing)

- 1 Table
- 2 (or more) Entities

- Entity types need to be mapped to the same table
- Have the primary keys mapped to the same columns
- At least one relationship configured between the primary key of one entity type and another in the same table.


``` csharp
public class Order
{
    public int Id { get; set; }
    public OrderStatus? Status { get; set; }
    public DetailedOrder DetailedOrder { get; set; } = null!;
}

public class DetailedOrder
{
    public int Id { get; set; }
    public OrderStatus? Status { get; set; }
    public string? BillingAddress { get; set; }
    public string? ShippingAddress { get; set; }
    public byte[] Version { get; set; } = null!;
}

modelBuilder.Entity<DetailedOrder>(
    x =>
    {
        x.ToTable( "Orders" );                   // Same Table

        x.Property( o => o.Status )             // Can duplicate
         .HasColumnName( "Status" );
    } );

modelBuilder.Entity<Order>(
    x =>
    {
        x.ToTable( "Orders" );                  // Same Table

        x.Property( o => o.Status )             // Can duplicate
          .HasColumnName( "Status" );

        x.HasOne( o => o.DetailedOrder )
         .WithOne()
         .HasForeignKey<DetailedOrder>( o => o.Id );

        x.Navigation( o => o.DetailedOrder )
         .IsRequired();
    } );
```




| Id | Status | BillingAddress           | ShippingAddress        | Version            |
|----|--------|--------------------------|------------------------|--------------------|
| 1  | 0      | 11 Wall Street, New York | 221 B Baker St, London | 0x00000000000007D1 |




``` sql

--  Order order = context.Orders
--                       .Include( x => x.DetailedOrder )
--                       .First( x => x.Id == 1 );

SELECT TOP(1) o.[Id], o.[Status], o.[Version], od.[Id], od.[BillingAddress], od.[ShippingAddress], od.[Status], od.[Version]
FROM Orders AS o
LEFT JOIN Orders AS od ON o.Id = od.Id -- Join on the same table
WHERE o.Id = 1


```


## Resources

- https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting#table-splitting
