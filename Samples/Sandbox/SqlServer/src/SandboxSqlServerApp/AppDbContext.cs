using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SandboxSqlServerApp.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Post> Posts => Set<Post>();

    public string DbPath { get; }

    public AppDbContext()
    {
        Environment.SpecialFolder folder = Environment.SpecialFolder.UserProfile;
        string path = Environment.GetFolderPath( folder );
        DbPath = Path.Join( path, "SandboxSqlServer.mdf" );
    }

    protected override void OnConfiguring( DbContextOptionsBuilder options )
    {
        options.UseSqlServer( "Server=(localdb)\\mssqllocaldb; Database=SandboxSqlServer; Trusted_Connection=True;" );
        //options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
        options.LogTo( Console.WriteLine, LogLevel.Debug );
        options.LogTo( Console.WriteLine, new[]
        {
            CoreEventId.ContextDisposed,
            CoreEventId.ContextInitialized,
            RelationalEventId.CommandExecuted,
            RelationalEventId.TransactionStarted,
            RelationalEventId.TransactionCommitted
        } );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
    }
}
