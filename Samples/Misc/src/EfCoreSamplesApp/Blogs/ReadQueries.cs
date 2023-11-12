using EfCoreSamplesApp.Entities;

namespace EfCoreSamplesApp.Blogs;

internal sealed class ReadQueries
{
    private readonly Blog_OneToMany_BasicDbContext _dbContext;

    public ReadQueries( Blog_OneToMany_BasicDbContext appDbContext )
    {
        _dbContext = appDbContext;
    }

    public void EnsureCreated()
    {
        bool created = _dbContext.Database.EnsureCreated();        // False if database exists
                                                                   // If database exists, it will not be recreated
                                                                   // if database does not exist, it will be created
        Console.WriteLine( $"Database created: {created}" );
    }

    public void ReadBlog()
    {
        Console.WriteLine( "ReadBlog" );

        Blog blog = _dbContext.Blogs.First();

        Console.WriteLine( $"Blog: {blog.BlogId} {blog.Url} {blog.Rating}" );
    }
}
