# Microsoft SQL Server - EF Core - Database Provider

Microsoft SQL Server (2012 onwards)

``` bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.Data.SqlClient
```

- Temporal tables
  - History table
- Value Generation
  - Identity
  - Sequences
  - GUID
  - RowVersions (Concurency token)
- Function mappings
  - Aggregate
    - Sum, Max, Min
  - Date and Time
    - Now, AddHours
  - ToString
    - Contains, Like, FreeText, Len, Substring
- Columns
  - Unicode
  - Sparse
- Indexes
  - Clustering
  - Fill factor
  - Online Creation
- Memory-Optimized Tables
- Hierarchical Data
  - Tree Structure (EF 8.0)
- Spatial data
  - Geography
- Azure SQL Database
  - ServiceTier, DatabaseMaxSize, PerformanceLevel

Resources

https://learn.microsoft.com/en-us/ef/core/providers/sql-server/?tabs=dotnet-core-cli


## Temporal table

**SQL Server temporal tables** automatically **keep track of all data ever stored in a table**, even after that data has been updated or deleted. This is achieved by creating a parallel "history table" into which timestamped historical data is stored whenever a change is made to the main table.

Specialized query operators:

- TemporalAsOf
- TemporalBetween
- TemporalContainedIn
- TemporalFromTo


## Value Generation

### Identity columns

``` csharp
.UseIdentityColumn(seed: 10, increment: 10);
```

### Sequences

``` csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configuring sequence settings
    modelBuilder.HasSequence<int>( "OrderNumbers" )
                .StartsAt(1000)
                .IncrementsBy(5);;

    // Configuring property to use sequence
    modelBuilder.Entity<Order>()
                .Property( o => o.OrderNo )
                .HasDefaultValueSql( "NEXT VALUE FOR OrderNumbers" );
}
```

### GUID

For GUID primary keys, the provider automatically generates optimal sequential values, similar to SQL Server's NEWSEQUENTIALID function


### RowVersions

Concurrency token

``` csharp
.IsRowVersion();
```
