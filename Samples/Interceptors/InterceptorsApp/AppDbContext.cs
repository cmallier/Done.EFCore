﻿using InterceptorsApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace InterceptorsApp;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        => optionsBuilder
          //.UseSqlServer( @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=StaticApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .UseSqlServer( @"Server=Desktop-Home; Initial Catalog=InterceptorsApp; Integrated Security=True; Connect Timeout=30; Encrypt=False; TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False" )
          .LogTo( Console.WriteLine, new[] { RelationalEventId.CommandExecuted } )
          .EnableSensitiveDataLogging()
          .AddInterceptors( new UpdateAuditableInterceptor() );
    //.AddInterceptors( sp.GetRequiredService<UpdateAuditableInterceptor>() );

    // DbSets
    public DbSet<Livre> Livres => Set<Livre>();


    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Livre>().ToTable( "Livres" );
    }
}
