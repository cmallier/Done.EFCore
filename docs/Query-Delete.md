# Query Delete


## Standard

``` csharp
using( var context = new BloggingContext() )
{
    Blog blog = context.Blogs.Single(b => b.Url == "http://example.com/blog");

    context.Blogs.Remove(blog);

    context.SaveChanges();
}
```


## Bulk

⚠️ EF Core 7.0

``` csharp
context.Blogs
       .Where( x => x.Rating < 3 )
       .ExecuteDelete();
```

``` sql
delete from b
from Blogs as b
where b.Rating < 3
```
