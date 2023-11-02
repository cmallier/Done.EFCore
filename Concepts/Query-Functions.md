# Query Functions

Function Mappings of the `Microsoft SQL Server`


## Aggregate

| C#                           | Sql             |
|------------------------------|-----------------|
| group.Max( x => x.Property ) | MAX( Property ) |
| group.Min( x => x.Property ) | MIN( Property ) |
| group.SUM( x => x.Property ) | SUM( Property ) |
| ...                          | ...             |


## Binary

| C#                    | Sql                           |
|-----------------------|-------------------------------|
| bytes.Contains(value) | CHARINDEX(@value, @bytes) > 0 |
| ...                   | ...                           |


## Conversion

| C#                      | Sql                              |
|-------------------------|----------------------------------|
| Convert.ToString(value) | CONVERT(nvarchar(max), @value)   |
| dateOnly.ToString()     | CONVERT(varchar(100), @dateOnly) |
| ...                     | ...                              |


## Date and Time

| C#                       | Sql                              |
|--------------------------|----------------------------------|
| DateTime.Now             | GETDATE()                        |
| dateTime.AddHours(value) | DATEADD(hour, @value, @dateTime) |
| ...                      | ...                              |

## Numeric

| C#            | Sql          |
|---------------|--------------|
| Math.Floor(d) | FLOOR(@d)    |
| Math.Round(d) | ROUND(@d, 0) |
| ...           | ...          |



## .Net -> Sql Translation

.Net
``` csharp
EF.Functions.Contains(propertyReference, searchCondition)
```
->
Sql
``` sql
CONTAINS( @propertyReference, @searchCondition )
```

``` csharp
var blogs = dbContext.Blogs
                     .Where( blog => EF.Functions.Like( blog.Name, "%EF%") )
                     .ToList();
```




https://learn.microsoft.com/en-us/ef/core/providers/sql-server/functions
