using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SandboxSqliteApp.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();

    public string DbPath { get; }

    public AppDbContext()
    {
        Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        string path = Environment.GetFolderPath( folder );
        DbPath = Path.Join( path, "SandboxSqlite.db" );
    }

    protected override void OnConfiguring( DbContextOptionsBuilder options )
    {
        options.UseSqlite( $"Data Source={DbPath}" );
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.LogTo( Console.WriteLine, LogLevel.Information );
    }
}
