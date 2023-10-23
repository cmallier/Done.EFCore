# Inheritance

- Entity type hierarchy mapping
- Table-per-hierarchy and discriminator configuration (TPH)
- Table-per-type configuration (TPT)
- Table-per-concrete-type configuration (TPC)


Resources : https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#summary-and-guidance



## Entity type hierarchy mapping

``` csharp
internal class MyContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<RssBlog> RssBlogs { get; set; }
}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
}

public class RssBlog : Blog
{
    public string RssUrl { get; set; }
}
```


## Table-per-hierarchy and discriminator configuration (TPH)

- By default, EF maps inheritance to a **single table**
- A **discriminator** column is used to identify which type each row represents


| BlogId | Type (Discriminator) | Url | RssUrl    |
|--------|----------------------|-----|-----------|
| 1      | Blog                 | ... | null      |
| 2      | RssBlog              | ... | SomeValue |
| 3      | Blog                 | ... | null      |


``` csharp
modelBuilder.Entity<Blog>()
            .HasDiscriminator<string>( "Type" )
            .HasValue<Blog>("Blog")
            .HasValue<RssBlog>("RssBlog")
            .IsComplete(false);
```

For derived entities, which use the TPH pattern, EF Core adds a predicate over discriminator column in the query


## Table-per-type configuration (TPT)

- Each type in the hierarchy gets its own table in the database
- In many cases, less performance when compared to TPH

Table : Blogs

| BlogId | Url  |
|--------|------|
| 1      |      |
| 2      | ...  |

Table : RssBlogs

| BlogId | RssUrl |
|--------|--------|
| 1      |        |
| 2      | ...    |


``` csharp
modelBuilder.Entity<Blog>().ToTable( "Blogs" );
modelBuilder.Entity<RssBlog>().ToTable( "RssBlogs" );

// ou
modelBuilder.Entity<Blog>().UseTptMappingStrategy()

```


## Table-per-concrete-type configuration (TPC)

- All the types are mapped to individual table
- EF 7.0
- This addresses some common performance issues with the TPT strategy


Table : Blogs

| BlogId | Url  |
|--------|------|
| 1      |      |
| 2      | ...  |

Table : RssBlogs

| BlogId | Url | RssUrl |
|--------|-----|--------|
| 1      |     |        |
| 2      | ... | ...    |

``` csharp
modelBuilder.Entity<Blog>().UseTpcMappingStrategy()
                           .ToTable("Blogs");

modelBuilder.Entity<RssBlog>()
            .ToTable("RssBlogs");

```


⚠️ Primary key values are generated and managed is a bit more complicated for TPC
https://learn.microsoft.com/en-us/ef/core/modeling/inheritance#key-generation
