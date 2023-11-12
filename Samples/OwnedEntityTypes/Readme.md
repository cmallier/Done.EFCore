# OwnedEntityTypes

``` csharp
public class Order
{
    public int Id { get; set; }
    public StreetAddress ShippingAddress { get; set; } = default!;
}

public class StreetAddress
{
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
}
```



## Default

``` csharp

modelBuilder.Entity<Order>()
            .OwnsOne( x => x.ShippingAddress );
```

Id | ShippingAddress_Street | ShippingAddress_City
---|------------------------|---------------------
1 | 123 Main Street | Seattle


## Specify Columns

``` csharp
modelBuilder.Entity<Order>().OwnsOne(
    o => o.ShippingAddress,
    sa =>
    {
        sa.Property( p => p.Street ).HasColumnName( "ShipsToStreet" );
        sa.Property( p => p.City ).HasColumnName( "ShipsToCity" );
    } );
```


| Id | ShipsToStreet   | ShipsToCity |
|----|-----------------|-------------|
| 1  | 123 Main Street | Seattle     |


## Resources

- https://learn.microsoft.com/en-us/ef/core/modeling/owned-entities
- https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Modeling/OwnedEntities/OwnedEntityContext.cs
