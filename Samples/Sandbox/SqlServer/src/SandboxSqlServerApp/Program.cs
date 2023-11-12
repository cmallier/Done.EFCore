using SandboxSqlServerApp.Entities;

using( AppDbContext dbContext = new() )
{
    Console.WriteLine( $"Database path: {dbContext.DbPath}" );

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}

// Add
using( AppDbContext dbContext = new() )
{
    Blog blog1 = new()
    {
        PublicId = Guid.NewGuid(),
        CreatedOnDateTime = DateTime.Now,
        IsActive = true,
        SomeEnum = SomeEnum.A,
        Url = "https://blog1.com"
    };

    //Blog blog2 = new()
    //{
    //    PublicId = Guid.NewGuid(),
    //    CreatedOnDateTime = DateTime.Now,
    //    IsActive = true,
    //    SomeEnum = SomeEnum.B,
    //    Url = "https://blog2.com"
    //};

    blog1.Posts.Add( new Post
    {
        Title = "Post 1",
        Content = "Content 1"
    } );

    dbContext.Blogs.Add( blog1 );

    dbContext.SaveChanges();
}



#region ToQueryString
//using( AppDbContext dbContext = new() )
//{
//    string sql = dbContext.Blogs.Where( x => x.A == x.B ).ToQueryString();
//    Blog blog1 = dbContext.Blogs.Where( x => x.A == x.B ).First();
//}
#endregion


#region Eager loading
//using( AppDbContext dbContext = new() )
//{
//    Blog blog1 = dbContext.Blogs
//                          .Include( x => x.Posts )
//                          .First();
//    Console.WriteLine( $"Id: {blog1.BlogId} Url: {blog1.Url}, Posts: {blog1.Posts.Count}" );
//}
#endregion


#region Explicit loading
//using( AppDbContext dbContext = new() )
//{
//    Blog blog = dbContext.Blogs.Single( x => x.BlogId == 1 );

//    dbContext.Entry( blog ).Collection( x => x.Posts ).Load();
//}
#endregion


Console.WriteLine( "End" );
