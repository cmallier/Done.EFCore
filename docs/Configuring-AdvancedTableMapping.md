# Configuring-AdvancedTableMapping

1. Table splitting (Table sharing)
2. Entity splitting (EF7)
3. Table-specific facet configuration (EF7)




## 1. Table splitting (Table sharing)

### 1.1 Use case

Using only a **subset** of the columns in the table for greater performance or encapsulation.

1 Table -> 2 (or more) Entities


## 1.2 Structure

Sql Table : Orders


| Id  | Status  | BillingAddress | ShippingAddress | Version |
|-----|---------|----------------|-----------------|---------|
| 1   | Pending | Address 1      | Address 2       | 1       |
| ... |         | ...            | ...             | ...     |



C#
``` csharp
public class Order
{
    public int Id { get; set; }
    public OrderStatus? Status { get; set; }
    public DetailedOrder DetailedOrder { get; set; } // <-
}

public class DetailedOrder
{
    public int Id { get; set; }
    public OrderStatus? Status { get; set; }
    public string BillingAddress { get; set; }
    public string ShippingAddress { get; set; }
    public byte[] Version { get; set; }
}
```

``` csharp
Order order = context.Orders
                     .First( x => x.Id == 1 );

// select top(1) Id, Status, Version
// from Orders
// where Id = 1

// order.Id = 1;
// order.Status = OrderStatus.Pending;
// order.DetailedOrder = null;

Order order = context.Orders
                     .Include( x => x.DetailedOrder )
                     .First( x => x.Id == 1 );

// select top(1) [o].[Id], [o].[Status], [o].[Version],
//              [o0].[Id], [o0].[BillingAddress], [o0].[ShippingAddress], [o0].[Status], [o0].[Version]
// from [Orders] AS [o]
// left join [Orders] AS [o0] ON [o].[Id] = [o0].[Id]
// where [o].[Id] = 1
```


## 1.3 Configuration

- Mapped to the same table
- Primary keys mapped to the same column
- At least one relationship configured between the primary key of one entity type and another in the same table

##  1.4 Example

https://github.com/dotnet/EntityFramework.Docs/tree/main/samples/core/Modeling/TableSplitting



## 2. Entity splitting

### 2.1 Use case

Entity -> 2 (or more) tables

If all tables are always used together

## 2.2 Structure


Sql Table : Customers

| Id  | Name |
|-----|------|
| 1   | Jack |
| ... | ...  |

Sql Table : PhoneNumbers

| CustomerId | PhoneNumber  |
|------------|--------------|
| 1          | 555-123-4567 |
| ...        | ...          |

Sql Table : Addresses

| CustomerId | Street | City     | State | ZipCode |
|------------|--------|----------|-------|---------|
| 1          | 1 road | Montreal | QC    | H1H1H1  |
| ...        | ...    | ...      | ...   | ...     |


C#
``` csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string Street { get; set; }
    public string State { get; set; }
    public string? ZipCode { get; set; }
}
```


### 2.3 Configuration

.SplitToTable( ... )

``` csharp
modelBuilder.Entity<Customer>()
            .ToTable( "Customers" )
            .SplitToTable( "PhoneNumbers", tableBuilder =>
            {
                tableBuilder.Property(customer => customer.Id).HasColumnName("CustomerId");
                tableBuilder.Property(customer => customer.PhoneNumber);
            }
            .SplitToTable( "Addresses", tableBuilder =>
            {
                tableBuilder.Property(customer => customer.Id).HasColumnName("CustomerId");
                tableBuilder.Property(customer => customer.Street);
                tableBuilder.Property(customer => customer.City);
                tableBuilder.Property(customer => customer.PostCode);
                tableBuilder.Property(customer => customer.Country);
            });
```


## 2.4
https://learn.microsoft.com/en-us/ef/core/modeling/table-splitting#entity-splitting



## 3. Table-specific facet configuration

# Use case
In some mapping (exp Hierarchies) -> Allows columns to have different names

Sql

``` sql
TABLE Animals
Id
Breed

TABLE Cats
CatId
EducationalLevel

TABLE Dogs
DogId
FavoriteToy
```

C#

``` csharp
public abstract class Animal
{
    public int Id { get; set; }
    public string Breed { get; set; } = null!;
}

public class Cat : Animal
{
    public string? EducationalLevel { get; set; }
    public string Breed { get; set; } = null!;
}

public class Dog : Animal
{
    public string? FavoriteToy { get; set; }
}
```

# 3.1 Configuration

``` csharp
modelBuilder.Entity<Cat>()
            .ToTable( "Cats", tableBuilder =>
            {
                tableBuilder.Property( cat => cat.Id ).HasColumnName("CatId")
                builder.Property(cat => cat.Breed).HasColumnName("CatBreed");
            });

```
