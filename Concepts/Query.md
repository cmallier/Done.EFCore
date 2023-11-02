# Query


## Single vs Split


Single (Default)

```csharp
using( var dbContext = new AppDbContext() )
{
    var blogs = dbContext.Blogs
                         .Include(blog => blog.Posts)
                         .ThenInclude(post => post.Author)
                         .AsSingleQuery()               // If Default is SplitQuery
                         .ToList();
}
```

``` sql
select
  blog.Id,
  blog.Name,
  post.Title,
  post.Content,
  author.Id,
from
  Blogs blog
  left join Posts post on blog.Id = post.BlogId
  left join Authors author on post.AuthorId = author.Id
order by
  blog.Id,
  post.Id
```

⚠️ Could result as a Cartesian explosion 🤯

Split

```csharp
using( var dbContext = new AppDbContext() )
{
    var blogs = dbContext.Blogs
                         .Include(blog => blog.Posts)
                         .AsSplitQuery()            // If Default is SplitQuery
                         .ToList();
}
```


``` sql
select
  blog.Id,
  blog.Name
from
  Blogs blog
order by
  blog.Id

------

select
  post.Title,
  post.Content,
  blog.Id         // Link
from
  Blogs blog
  left join Posts post on blog.Id = post.BlogId
```



📓 One-to-one related entities are always loaded via JOINs in the same query, as it has no performance impact.
