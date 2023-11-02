# Setup - SqlServer


MyApp.Infrastructure



``` csharp
using EfCoreSamplesApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace EfCoreSamplesApp;

public class AppDbContext : DbContext
{
    // DbSet properties map to tables in the database
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public AppDbContext( DbContextOptions<AppDbContext> options ) : base( options )
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void ConfigureConventions( ModelConfigurationBuilder configurationBuilder )
    {
        configurationBuilder.Conventions.Remove( typeof( BackingFieldAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( ConcurrencyCheckAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( DatabaseGeneratedAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( DeleteBehaviorAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( EntityTypeConfigurationEntityTypeAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( ForeignKeyAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( IndexAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( InversePropertyAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( KeyAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( KeylessEntityTypeAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( MaxLengthAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( NavigationBackingFieldAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( NotMappedEntityTypeAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( NotMappedMemberAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( OwnedEntityTypeAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( PrecisionAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalColumnAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalColumnCommentAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalNavigationJsonPropertyNameAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalPropertyJsonPropertyNameAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalTableAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RelationalTableCommentAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RequiredNavigationAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( RequiredPropertyAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( StringLengthAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( TimestampAttributeConvention ) );
        configurationBuilder.Conventions.Remove( typeof( UnicodeAttributeConvention ) );

        base.ConfigureConventions( configurationBuilder );
    }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        if( optionsBuilder.IsConfigured )
        {
            return;
        }

        optionsBuilder.UseSqlServer( sqlServerOptions =>
        {
            sqlServerOptions.UseQuerySplittingBehavior( QuerySplittingBehavior.SingleQuery );
            sqlServerOptions.EnableRetryOnFailure( maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds( 30 ), errorNumbersToAdd: null );
        } );

        // Default behavior
        optionsBuilder.UseQueryTrackingBehavior( QueryTrackingBehavior.TrackAll ); // TrackAll, NoTracking,
        optionsBuilder.EnableDetailedErrors( true );
        optionsBuilder.EnableSensitiveDataLogging( true );

        // Configure Warnings
        optionsBuilder.ConfigureWarnings( warnings => warnings.Ignore( SqlServerEventId.ByteIdentityColumnWarning ) );
        optionsBuilder.ConfigureWarnings( warnings => warnings.Throw( SqlServerEventId.ColumnWithoutTypeWarning ) );
        optionsBuilder.ConfigureWarnings( warnings => warnings.Throw( RelationalEventId.KeyPropertiesNotMappedToTable ) );

        base.OnConfiguring( optionsBuilder );
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.HasDefaultSchema( "dbo" );

        // Azure SQL Database Options
        modelBuilder.HasServiceTier( "BusinessCritical" );
        modelBuilder.HasDatabaseMaxSize( "1 GB" );
        modelBuilder.HasPerformanceLevel( "BC_Gen4_1" );
        modelBuilder.HasPerformanceLevelSql( "ELASTIC_POOL ( name = myelasticpool )" );


        // Collation
        // https://learn.microsoft.com/en-us/ef/core/miscellaneous/collations-and-case-sensitivity
        modelBuilder.UseCollation( "Latin1_General_CI_AS" );


        // Sequence
        // https://learn.microsoft.com/en-us/ef/core/modeling/sequences
        modelBuilder.HasSequence<int>( "OrderNumbers", schema: "shared" ).StartsAt( 1000 ).IncrementsBy( 5 );


        modelBuilder.SharedTypeEntity<Dictionary<string, object>>( "Dictionary<string, object>", b =>
        {
            b.IndexerProperty<string>( "Key" );
            b.IndexerProperty<object>( "Value" );
        } );

        modelBuilder.SharedTypeEntity<Dictionary<string, object>>( name: "Blog", bb =>
        {
            bb.Property<int>( "BlogId" );
            bb.Property<string>( "Url" );
            bb.Property<DateTime>( "LastUpdated" );
        } );


        // Entities Configuration
        modelBuilder.ApplyConfiguration( new SomeEntityEntityTypeConfiguration() );
        modelBuilder.ApplyConfiguration<SomeEntity>( new SomeEntityEntityTypeConfiguration() );
        new SomeEntityEntityTypeConfiguration().Configure( modelBuilder.Entity<SomeEntity>() );
        modelBuilder.ApplyConfigurationsFromAssembly( typeof( AppDbContext ).Assembly );



        // Called fourth
        // 1. Fluent API here
        //modelBuilder.Entity<Blog>( x =>
        //{
        //    x.ToTable( "Blogs" );

        //    x.HasKey( b => b.BlogId );

        //    x.Property( b => b.Url )
        //     .IsRequired()
        //     .HasMaxLength( 150 );
        //} );

        // 2. Scan Assembly
        // modelBuilder.ApplyConfigurationsFromAssembly( typeof( AppDbContext ).Assembly );

        // 3. Specific EntityTypeConfiguration 👌
        //new BlogEntityTypeConfiguration().Configure( modelBuilder.Entity<Blog>() );

        base.OnModelCreating( modelBuilder );
    }
}

```
