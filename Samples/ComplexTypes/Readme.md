# ComplexTypes

- Objects that are structured to hold multiple values, but the object has no key defining identity. For example, Address, Coordinate.
- EF Core 8


Note :

- **Owned types** can be used, but since owned types are actually **entity types**, they have semantics based on a **key value**, even when that key value is hidden.
- Immutability is often natural when a type is a good candidate for being a complex type





## Immutability
``` csharp
public record Address( string Line1, string? Line2, string City, string Country, string PostCode )

// or
public readonly record struct Address( string Line1, string? Line2, string City, string Country, string PostCode );

// or

public record Address
{
    public Address( string line1, string? line2, string city, string country, string postCode )
    {
        Line1 = line1;
        Line2 = line2;
        City = city;
        Country = country;
        PostCode = postCode;
    }

    public string Line1 { get; init; }
    public string? Line2 { get; init; }
    public string City { get; init; }
    public string Country { get; init; }
    public string PostCode { get; init; }
}
```


## Resources
- https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/whatsnew#value-objects-using-complex-types
