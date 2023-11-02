# Configuring

https://learn.microsoft.com/en-us/ef/core/modeling/


Ordre

`Fluent Api` > `Data Annotations` > `Conventions`


## 1. Built-in conventions
EF Core includes many model building conventions that are `enabled` by default.

See [Conventions](Conventions.md)

- PrimaryKey
- ForeignKey
- Table
- Schema
- Column
- DataType
- CascadeDelete
- ChangeTrackingStrategy


## 2. Specific


## 2.1 Data Annotations

``` csharp
[Table("Blogs")]
public class Blog
{
    [Column("blog_id")]
    public int BlogId { get; set; }

    [Required]
    public string Url { get; set; }
}
```


## 2.2 Fluent Api 👍

``` csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Blog>()
                .Property( b => b.Url )
                .IsRequired();
}
```

Grouping 👍
``` csharp
public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.Property( x => x.Url )
               .IsRequired();
    }
}

protected override void OnModelCreating( ModelBuilder modelBuilder )
{
    new BlogEntityTypeConfiguration().Configure( modelBuilder.Entity<Blog>() );
}
```


## Ressources

https://learn.microsoft.com/en-us/ef/core/modeling/
https://github.com/dotnet/EntityFramework.Docs/tree/main/samples/core/Modeling
