# EntitySplitting

- 1 Entity to 2 (or more) Tables
- If all tables are always used together

## Limitations
- Entity splitting can't be used for entity types in hierarchies.
- For any row in the main table there must be a row in each of the split tables (the fragments are not optional).


##

``` csharp
public class Customer
{
    public int Id { get; set; }                     // Table : Customer, Addresses, PhoteNumbers
    public string Name { get; set; }                // Table : Customer
    public string? PhoneNumber { get; set; }        // Table : PhoteNumbers
    public string Street { get; set; }              // Table : Addresses
    public string City { get; set; }                // Table : Addresses
    public string? PostCode { get; set; }           // Table : Addresses
    public string Country { get; set; }             // Table : Addresses
}


modelBuilder.Entity<Customer>(
    entityBuilder =>
    {
        entityBuilder
            .ToTable( "Customers" )
            .SplitToTable(
                "PhoneNumbers",
                tableBuilder =>
                {
                    tableBuilder.Property( customer => customer.Id ).HasColumnName( "CustomerId" );
                    tableBuilder.Property( customer => customer.PhoneNumber );
                } )
            .SplitToTable(
                "Addresses",
                tableBuilder =>
                {
                    tableBuilder.Property( customer => customer.Id ).HasColumnName( "CustomerId" );
                    tableBuilder.Property( customer => customer.Street );
                    tableBuilder.Property( customer => customer.City );
                    tableBuilder.Property( customer => customer.PostCode );
                    tableBuilder.Property( customer => customer.Country );
                } );
    } );
```

Customer

| Id | Name     |
|----|----------|
| 1  | John Doe |


Addresses

| CustomerId | Street      | City   | PostCode | Country |
|------------|-------------|--------|----------|---------|
| 1          | Main Street | London | 12345    | UK      |

PhoteNumbers

| CustomerId | PhoneNumber |
|------------|-------------|
| 1          | 123456789   |

## Resources

 - https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting#entity-splitting
