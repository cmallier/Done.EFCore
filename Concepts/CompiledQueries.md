# Compiled Queries


``` csharp
using( var dbContext = new DbContext() )
{
    int id = 42;
    Post post = dbContext.Posts.Single( x => x.Id == id )

}
```


``` csharp
using( var dbContext = new DbContext() )
{
    Post post = _postById( dbContext, 42 );
}

// Compiled query
private static Func<DbContext, int, Post> _postById
    = EF.CompileQuery( ( DbContext dbContext, int id )
        => dbContext.Posts.Single( x => x.Id == id ) );
```


![Alt text](./images/EfCoreQueryPipeline.png)
![Alt text](./images/EfCoreQueryPipeline2.png)
