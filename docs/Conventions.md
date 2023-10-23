# Conventions

Conventions are a set of rules hard-baked into Entity Framework Core




## Primary Key

By convention, a property named `Id` or `<type name>Id` will be configured as the primary key of an entity.

``` csharp
public class Blog
{
    public int Id { get; set; }     // Primary Key by default
    public int BlogId { get; set; } // Primary Key by default)
}
```


## Foreign Key

By convention, a property named `<navigation property name><primary key property name>` or `<primary key property name>` will be configured as the foreign key property of an entity.

``` csharp
public class Author
{
    public int AuthorId { get; set; }
    ...
}

public class Book
{
    public Author Writer { get; set; }  // WriterAuthorId - required
    public Author Author { get; set; }  // AuthorAuthorId - required
    public int AuthorId { get; set; }   // AuthorId - required
    public Author? Author { get; set; }  // AuthorAuthorId - optional
    ...
    // Shadow property

}
```


## Table
`public DbSet<Book> Books { get; set; }` -> `Books` by default


## Schema

`dbo` schema by default


## Columns

`<PropertyName>` by default


## Data type

<https://www.sqlite.org/datatype3.html>

- `string` -> `nvarchar(max)` by default
- `<type>` -> Database provider specific


## Indexes

An index is created for each property that is configured as a foreign key.


## Microsoft.EntityFrameworkCore.Metadata.Conventions


Il y a plusieurs conventions.


- CascadeDeleteConvention
- ...
- CosmosKeyDiscoveryConvention
- EntitySplittingConvention
- ForeignKeyIndexConvention
- NonNullableNavigationConvention
- RelationalMaxIdentifierLengthConvention
- ...

https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.conventions?view=efcore-7.0


On peut ajouter, modifier ou supprimer des conventions.

``` csharp
protected override void ConfigureConventions( ModelConfigurationBuilder configurationBuilder )
{
    configurationBuilder.Conventions.Remove( typeof( ForeignKeyIndexConvention ) );
}
```



## Ressources
https://www.learnentityframeworkcore.com/conventions
https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.metadata.conventions?view=efcore-7.0
