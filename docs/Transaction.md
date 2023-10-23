


``` csharp
using( var dbContext = new DbContent() )
{
    using( var transaction = DbContent.Database.BeginTransaction() )
    {
        try
        {
            context.Blogs.Add( new Blog { Id = 1, ... } );

            //dbContext.Database.ExecuteSqlRaw( "SET IDENTITY_INSERT dbo.Blogs ON;" );
            dbContext.SaveChanges();
            //dbContext.Database.ExecuteSqlRaw( "SET IDENTITY_INSERT dbo.Blogs OFF;" );
            transaction.Commit();
        }
        catch( Exception ex )
        {
            transaction.Rollback();
        }
    }
}
```
