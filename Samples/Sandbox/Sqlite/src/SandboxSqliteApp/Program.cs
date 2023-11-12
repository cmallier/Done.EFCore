using Microsoft.EntityFrameworkCore;
using SandboxSqliteApp.Entities;

using( AppDbContext dbContext = new() )
{
    Console.WriteLine( $"Database path: {dbContext.DbPath}" );

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}





// Add
using( AppDbContext dbContext = new() )
{
    Blog blog = new()
    {

        Url = "https://blog1.com"
    };

    blog.Posts.Add( new Post
    {
        Title = "Post 1",
        Content = "Content 1"
    } );

    dbContext.Blogs.Add( blog );
    dbContext.SaveChanges();
}


// Get
using( AppDbContext dbContext = new() )
{
    Blog blog1 = dbContext.Blogs.Include( x => x.Posts ).First();
    Console.WriteLine( $"Id: {blog1.BlogId} Url: {blog1.Url}, Posts: {blog1.Posts.Count}" );
}


using( AppDbContext dbContext = new() )
{
    string sql = dbContext.Blogs.Where( x => x.A == x.B ).ToQueryString();
    Blog blog1 = dbContext.Blogs.Where( x => x.A == x.B ).First();
}

Console.WriteLine( "End" );
