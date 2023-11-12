using EfCoreSamplesApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EfCoreSamplesApp;

internal class Blog_OneToMany_BasicDbContext : DbContext
{
    // DbSet properties map to tables in the database
    public DbSet<Blog> Blogs { get; set; }


    public Blog_OneToMany_BasicDbContext( DbContextOptions<Blog_OneToMany_BasicDbContext> options ) : base( options )
    {

    }

    protected override void OnConfiguring( DbContextOptionsBuilder options )
    {
        base.OnConfiguring( options );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );
    }
}
